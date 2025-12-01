using R2API;
using RoR2;
using RoR2.Hologram;
using System.Collections.Generic;
//using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class Artifact_RealStages
    {
        public static ArtifactDef ArtifactUseNormalStages = ScriptableObject.CreateInstance<ArtifactDef>();
        public static SceneCollection realityDestinations = ScriptableObject.CreateInstance<SceneCollection>();
        public static List<SceneDef> visitedScenes;
        //public static SceneDef arena = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/arena/arena.asset").WaitForCompletion();
        //public static SceneDef voidStage = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion();

        public static void MakeArtifact()
        {
            realityDestinations.name = "sgITArtifactReal";

            ArtifactUseNormalStages.cachedName = "AAAUseNormalStages";
            ArtifactUseNormalStages.nameToken = "ARTIFACT_NORMALSTAGES_NAME";
            ArtifactUseNormalStages.descriptionToken = "ARTIFACT_NORMALSTAGES_DESCRIPTION";
            ArtifactUseNormalStages.smallIconSelectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact2_On.png");
            ArtifactUseNormalStages.smallIconDeselectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact2_Off.png");
            ContentAddition.AddArtifactDef(ArtifactUseNormalStages);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;
            On.RoR2.Run.OnDisable += Run_OnDisable;



            SceneDef scene = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
            scene.validForRandomSelection = true;
            scene = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/helminthroost/helminthroost.asset").WaitForCompletion();
            scene.validForRandomSelection = true;

            On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnStartServer;
            On.RoR2.VoidStageMissionController.Start += VoidStageMissionController_Start;
            On.RoR2.AccessCodesMissionController.OnStartServer += AccessCodesMissionController_OnStartServer;
        }

        private static void AccessCodesMissionController_OnStartServer(On.RoR2.AccessCodesMissionController.orig_OnStartServer orig, AccessCodesMissionController self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(ArtifactUseNormalStages))
            {
                return;
            }
            orig(self);
        }

        private static void ArenaMissionController_OnStartServer(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(ArtifactUseNormalStages))
            {
                self.gameObject.SetActive(false);
                GameObject PortalArena = GameObject.Find("/PortalArena");
                if (PortalArena)
                {
                    PortalArena.transform.GetChild(0).gameObject.SetActive(false);
                }

                return;
            }
            orig(self);
        }

        public static void DoDestinations()
        {
            Debug.LogWarning("Artifact of Reality : Destination List Reset");

            List<SceneCollection.SceneEntry> newSceneEntry = new List<SceneCollection.SceneEntry>();
            for (int i = 0; i < SceneCatalog.allStageSceneDefs.Length; i++)
            {

                SceneDef def = SceneCatalog.allStageSceneDefs[i];
                if (def.validForRandomSelection && def.hasAnyDestinations)
                {
                    float weight = 1;
                    switch (def.cachedName)
                    {
                        case "golemplains":
                        case "goolake":
                        case "ancientloft":
                        case "frozenwall":
                        case "skymeadow":
                            weight = 0.5f;
                            break;
                        case "dampcavesimple":
                            weight = 0.7f;
                            break;
                        case "helminthroost":
                            weight = 1.5f;
                            break;
                    }

                    SceneCollection.SceneEntry newEntry = new SceneCollection.SceneEntry() { sceneDef = def, weight = weight };
                    newSceneEntry.Add(newEntry);
                }

            }
            SceneDef arena = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/arena/arena.asset").WaitForCompletion();
            SceneCollection.SceneEntry newEntry2 = new SceneCollection.SceneEntry() { sceneDef = arena, weight = 2f };
            newSceneEntry.Add(newEntry2);
            realityDestinations._sceneEntries = newSceneEntry.ToArray();


        }

        private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
            Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Random;

            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += RemoveSpawnPointsAndMonsters;
            SceneDirector.onGenerateInteractableCardSelection += AddAndRemoveInteractables;
            On.RoR2.DirectorCard.IsAvailable += Allow_Earlier_Spawns;
            On.RoR2.SceneDirector.Start += DoReality;
            On.RoR2.Run.PickNextStageScene += ChooseRealityStages;

            VoidSafeWard_Hooks.baseRadius += WConfig.ArtifactOfRealityBonusRadius.Value;
            visitedScenes = new List<SceneDef>();
            DoDestinations();
        }
        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            if (Run.instance)
            {
                RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
                Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Normal;
            }
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer -= RemoveSpawnPointsAndMonsters;
            SceneDirector.onGenerateInteractableCardSelection -= AddAndRemoveInteractables;
            On.RoR2.DirectorCard.IsAvailable -= Allow_Earlier_Spawns;
            On.RoR2.SceneDirector.Start -= DoReality;
            On.RoR2.Run.PickNextStageScene -= ChooseRealityStages;

            VoidSafeWard_Hooks.baseRadius -= WConfig.ArtifactOfRealityBonusRadius.Value;
        }

        private static void DoReality(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            orig(self);
            DoREALITY(self);
        }

        private static void DoREALITY(SceneDirector obj)
        {
            CombatDirector[] combatDirector = obj.gameObject.GetComponents<CombatDirector>();
            for (int i = 0; i < combatDirector.Length; i++)
            {
                Debug.Log(combatDirector[i]);
                combatDirector[i].enabled = false;
            }

            //Only find 1 instead of all
            PortalStatueBehavior firstNewt = Object.FindObjectOfType(typeof(PortalStatueBehavior)) as PortalStatueBehavior;
            if (firstNewt)
            {
                Material newMat = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/voidstage/matVoidCrystal.mat").WaitForCompletion();
                GameObject SupressorObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/VoidSuppressor");
                Transform newtHolder = firstNewt.transform.parent;

                for (int i = 0; i < newtHolder.childCount; i++)
                {
                    Transform NewtAltar = newtHolder.GetChild(i);
                    if (NewtAltar.GetComponent<PortalStatueBehavior>())
                    {
                        Debug.Log(NewtAltar);
                        NewtAltar.gameObject.SetActive(true);
                        if (NetworkServer.active)
                        {
                            GameObject VoidSuppressor = GameObject.Instantiate(SupressorObject, NewtAltar);
                            VoidSuppressor.transform.localPosition = new Vector3(0, -1.38f, 0);
                            PurchaseInteraction purch = VoidSuppressor.GetComponent<PurchaseInteraction>();
                            purch.costType = CostTypeIndex.None;
                            purch.cost = 0;
                            NetworkServer.Spawn(VoidSuppressor);
                        }
                        NewtAltar.GetChild(0).gameObject.SetActive(false);
                        NewtAltar.GetChild(1).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(2).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(3).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(4).GetComponent<MeshRenderer>().material = newMat;
                        GameObject.Destroy(NewtAltar.GetComponent<PurchaseInteraction>());
                        GameObject.Destroy(NewtAltar.GetComponent<HologramProjector>());
                        GameObject.Destroy(NewtAltar.GetComponent<PortalStatueBehavior>());
                    }
                }
            }

            if (NetworkServer.active)
            {
                Run.instance.PickNextStageScene(null);
            }
            //
            GameObject ITWeather = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/Weather, InfiniteTower.prefab").WaitForCompletion());

            Light ITSun = ITWeather.transform.GetChild(2).GetComponent<Light>();
            SetAmbientLight newLighting = ITWeather.transform.GetChild(0).GetComponent<SetAmbientLight>();
            newLighting.ambientIntensity = 1.2f; //1.5 default
            newLighting.ambientEquatorColor = newLighting.ambientSkyColor;
            newLighting.ambientGroundColor = new Color(0.6321f, 0.4323f, 0.6321f, 1);
            //newLighting.ambientSkyColor *= 1f;
            newLighting.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;


            string scene = SceneInfo.instance.sceneDef.baseSceneName;
            switch (scene)
            {
                case "blackbeach":
                case "rootjungle":
                    newLighting.ambientIntensity = 1.5f;
                    break;
                case "snowyforest":
                case "lakesnight":
                    ITSun.intensity = 1; //3 default
                    break;
                case "helminthroost":
                    ITWeather.transform.localPosition = new Vector3(-600, 0, 250);
                    GameObject Helminthroost = GameObject.Find("/HOLDER: Lighting/Weather, Helminthroost/");
                    if (Helminthroost)
                    {
                        Helminthroost.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    ITSun.intensity = 2; //3 default
                    break;
                case "frozenwall":
                    newLighting.ambientIntensity = 0.6f;
                    ITSun.intensity = 1; //3 default
                    break;
                case "wispgraveyard":
                    GameObject Weather = GameObject.Find("/Weather, Wispgraveyard");
                    if (Weather)
                    {
                        Weather.SetActive(false);
                    }
                    break;
                case "nest":
                    GameObject nestS = GameObject.Find("/HOLDER: Weather/Weather, Nest/Directional Light (SUN)");
                    if (nestS)
                    {
                        nestS.SetActive(false);
                    }
                    ITWeather.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case "ironalluvium":
                    GameObject ironalluviumS = GameObject.Find("/HOLDER: Weather/IronAlluvium_Weather/Directional Light (SUN)");
                    if (ironalluviumS)
                    {
                        ironalluviumS.SetActive(false);
                    }
                    ITWeather.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case "sulfurpools":
                case "ironalluvium2":
                case "village":
                case "villagenight":
                    ITWeather.transform.GetChild(2).gameObject.SetActive(false);
                    break;
                case "voidstage":
                    GameObject Void = GameObject.Find("/Weather, Void Stage");
                    if (Void)
                    {
                        Void.transform.GetChild(0).gameObject.SetActive(false);
                    }
                    break;
            }


            newLighting.ApplyLighting();
        }


        private static void VoidStageMissionController_Start(On.RoR2.VoidStageMissionController.orig_Start orig, VoidStageMissionController self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(ArtifactUseNormalStages))
            {
                self.batteryCount = 0;
            }
            orig(self);
        }

        private static void Run_OnDisable(On.RoR2.Run.orig_OnDisable orig, Run self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(ArtifactUseNormalStages))
            {
                RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
                Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Normal;
            }
            orig(self);
        }



        private static void ChooseRealityStages(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
        {
            if (self.ruleBook.stageOrder == StageOrder.Random)
            {

                if (visitedScenes.Count > 10)
                {
                    visitedScenes.Clear();
                    DoDestinations();
                }
                if (realityDestinations.sceneEntries.Length == 0)
                {
                    DoDestinations();
                }
                if (realityDestinations.sceneEntries.Length > 0)
                {
                    //
                    WeightedSelection<SceneDef> weightedSelection = new WeightedSelection<SceneDef>(24);
                    realityDestinations.AddToWeightedSelection(weightedSelection, new System.Func<SceneDef, bool>(self.CanPickStage));

                    //0 00-10, 1 10-20, 2 20-30

                    if (self.stageClearCount > 2)
                    {
                        SceneDef voidStage = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion();
                        weightedSelection.AddChoice(voidStage, 2f);
                    }

                    self.nextStageScene = weightedSelection.Evaluate(self.nextStageRng.nextNormalizedFloat);
                    visitedScenes.Add(self.nextStageScene);

                    for (int i = 0; i < realityDestinations._sceneEntries.Length; i++)
                    {
                        if (realityDestinations._sceneEntries[i].sceneDef == self.nextStageScene)
                        {
                            realityDestinations._sceneEntries[i].weight = 0;
                        }
                    }
                }
                else
                {
                    SceneDef[] array = SceneCatalog.allStageSceneDefs.Where(new System.Func<SceneDef, bool>(RealityValidSceneDefs)).ToArray();
                    self.nextStageScene = self.nextStageRng.NextElementUniform(array);
                }
                return;
            }
            orig(self, choices);
        }

        private static bool RealityValidSceneDefs(SceneDef sceneDef)
        {
            if (visitedScenes.Contains(sceneDef))
            {
                return false;
            }
            return sceneDef.hasAnyDestinations && sceneDef.validForRandomSelection;
        }


        private static bool Allow_Earlier_Spawns(On.RoR2.DirectorCard.orig_IsAvailable orig, DirectorCard self)
        {
            if (self.minimumStageCompletions > 2)
            {
                self.minimumStageCompletions = 2;
            }
            return orig(self);
        }

        private static void ArtifactInITOnly(On.RoR2.Run.orig_OverrideRuleChoices orig, Run self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);
            if (self && !(self is InfiniteTowerRun))
            {
                self.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindRuleDef("Artifacts.AAAUseNormalStages").FindChoice("Off"));
            }
        }

        private static void RemoveSpawnPointsAndMonsters(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            sceneDirector.teleporterSpawnCard = null;
            sceneDirector.monsterCredit = 0;
            sceneDirector.RemoveAllExistingSpawnPoints();
            orig(self, sceneDirector);

        }

        private static void AddAndRemoveInteractables(SceneDirector arg1, DirectorCardCategorySelection dccs)
        {
            int voidIndex = dccs.FindCategoryIndexByName("Void Stuff");
            if (voidIndex != -1)
            {
                dccs.categories[voidIndex].selectionWeight *= 2 + 1f;

                DirectorCard iscVoidChestSacrificeOn = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "5448ccdc4b91bd244a1631d60d24298d").WaitForCompletion(),
                    selectionWeight = 1,
                };
                DirectorCard iscVoidTriple = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion(),
                    selectionWeight = 15,
                };
                /*DirectorCard iscVoidSuppressorIT = new DirectorCard
                {
                    spawnCard = SimulacrumDCCS.iscVoidSuppressorIT,
                    selectionWeight = 15,
                };*/
                DirectorCard iscVoidCoinBarrelITSacrifice = new DirectorCard
                {
                    spawnCard = SimulacrumDCCS.iscVoidCoinBarrelITSacrifice,
                    selectionWeight = 6,
                };
                dccs.AddCard(voidIndex, iscVoidCoinBarrelITSacrifice);
                dccs.AddCard(voidIndex, iscVoidChestSacrificeOn);
                //dccs.AddCard(voidIndex, iscVoidSuppressorIT);
                dccs.AddCard(voidIndex, iscVoidTriple);
               
            }
            dccs.RemoveCardsThatFailFilter(trimmer);
        }

        public static System.Predicate<DirectorCard> trimmer = new System.Predicate<DirectorCard>(SimulacrumTrimmer);
        public static bool SimulacrumTrimmer(DirectorCard card)
        {
            GameObject prefab = card.GetSpawnCard().prefab;
            if ((card.GetSpawnCard() as InteractableSpawnCard).skipSpawnWhenDevotionArtifactEnabled)
            {
                if (!(RunArtifactManager.instance.IsArtifactEnabled(CU8Content.Artifacts.Devotion) || RunArtifactManager.instance.IsArtifactEnabled(Artifact_SimuDrones.ArtifactDef)))
                {
                    return false;
                }
            }
            return !(prefab.GetComponent<ShrineCombatBehavior>() | prefab.GetComponent<HalcyoniteShrineInteractable>() | prefab.GetComponent<OutsideInteractableLocker>() | prefab.GetComponent<ShrineBossBehavior>() | prefab.GetComponent<SeerStationController>() | prefab.GetComponent<PortalStatueBehavior>());
        }
    }


}
