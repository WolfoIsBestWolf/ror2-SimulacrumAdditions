using R2API;
using RoR2;
//using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class ArtifactReal
    {
        public static ArtifactDef ArtifactUseNormalStages = ScriptableObject.CreateInstance<ArtifactDef>();
        public static SceneCollection realityDestinations = ScriptableObject.CreateInstance<SceneCollection>();
        public static List<SceneDef> visitedScenes = new List<SceneDef>();
        public static SceneDef arena = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/Base/arena/arena.asset").WaitForCompletion();
        public static SceneDef voidStage = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidstage/voidstage.asset").WaitForCompletion();

        public static void MakeArtifact()
        {
            realityDestinations.name = "sgITArtifactReal";

            Rect rec = new Rect(0, 0, 64, 64);
            Texture2D ArtifactOn = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/Artifact2_On.png");
            ArtifactOn.filterMode = FilterMode.Trilinear;
            Sprite ArtifactOnS = Sprite.Create(ArtifactOn, rec, new Vector2(0, 0));

            Texture2D ArtifactOff = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/Artifact2_Off.png");
            ArtifactOff.filterMode = FilterMode.Trilinear;
            Sprite ArtifactOffS = Sprite.Create(ArtifactOff, rec, new Vector2(0, 0));


            ArtifactUseNormalStages.cachedName = "AAAUseNormalStages";
            ArtifactUseNormalStages.nameToken = "ARTIFACT_NORMALSTAGES_NAME";
            ArtifactUseNormalStages.descriptionToken = "ARTIFACT_NORMALSTAGES_DESCRIPTION";
            ArtifactUseNormalStages.smallIconSelectedSprite = ArtifactOnS;
            ArtifactUseNormalStages.smallIconDeselectedSprite = ArtifactOffS;
            ContentAddition.AddArtifactDef(ArtifactUseNormalStages);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;
            On.RoR2.Run.OnDisable += Run_OnDisable;

            Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion().directorCreditCost = 5;
            //Since we got Void Soupper in Dissim we gotta fix the vanilla up
            GameObject VoidSuppressorPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSuppressor/VoidSuppressor.prefab").WaitForCompletion();

            VoidSuppressorPrefab.GetComponent<PurchaseInteraction>().isShrine = true;
            VoidSuppressorPrefab.GetComponent<VoidSuppressorBehavior>().effectColor.a = 0.85f;
            VoidSuppressorPrefab.transform.GetChild(0).GetChild(7).GetChild(1).GetChild(1).gameObject.SetActive(true);
            VoidSuppressorPrefab.transform.GetChild(0).GetChild(7).GetChild(1).GetChild(0).localScale = new Vector3(1.5f, 1.5f, 1.5f);
            VoidSuppressorPrefab.transform.GetChild(0).GetChild(7);

            ItemDef ScrapWhiteSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapWhiteSuppressed.asset").WaitForCompletion();
            ItemDef ScrapGreenSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapGreenSuppressed.asset").WaitForCompletion();
            ItemDef ScrapRedSuppressed = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/ScrapVoid/ScrapRedSuppressed.asset").WaitForCompletion();

            ScrapWhiteSuppressed.pickupToken = "ITEM_SCRAPWHITE_PICKUP";
            ScrapGreenSuppressed.pickupToken = "ITEM_SCRAPGREEN_PICKUP";
            ScrapRedSuppressed.pickupToken = "ITEM_SCRAPRED_PICKUP";

            ScrapWhiteSuppressed.descriptionToken = "ITEM_SCRAPWHITE_DESC";
            ScrapGreenSuppressed.descriptionToken = "ITEM_SCRAPGREEN_DESC";
            ScrapRedSuppressed.descriptionToken = "ITEM_SCRAPRED_DESC";

            ScrapWhiteSuppressed.deprecatedTier = ItemTier.Tier1;
            ScrapGreenSuppressed.deprecatedTier = ItemTier.Tier2;
            ScrapRedSuppressed.deprecatedTier = ItemTier.Tier3;
            On.RoR2.UI.LogBook.LogBookController.BuildPickupEntries += (orig, expand) =>
            {
                ScrapWhiteSuppressed.deprecatedTier = ItemTier.NoTier;
                ScrapGreenSuppressed.deprecatedTier = ItemTier.NoTier;
                ScrapRedSuppressed.deprecatedTier = ItemTier.NoTier;
                var A = orig(expand);
                ScrapWhiteSuppressed.deprecatedTier = ItemTier.Tier1;
                ScrapGreenSuppressed.deprecatedTier = ItemTier.Tier2;
                ScrapRedSuppressed.deprecatedTier = ItemTier.Tier3;
                return A;
            };

            SceneDef scene = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/habitat/habitat.asset").WaitForCompletion();
            scene.validForRandomSelection = true;
            scene = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC2/helminthroost/helminthroost.asset").WaitForCompletion();
            scene.validForRandomSelection = true;

            On.RoR2.ArenaMissionController.OnStartServer += ArenaMissionController_OnStartServer;
            On.RoR2.VoidStageMissionController.Start += VoidStageMissionController_Start;

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
                            weight = 0.4f;
                            break;
                        case "dampcavesimple":
                            weight = 0.7f;
                            break;
                        case "helminthroost":
                            weight = 2;
                            break;
                    }


                    SceneCollection.SceneEntry newEntry = new SceneCollection.SceneEntry() { sceneDef = def, weight = weight } ;
                    newSceneEntry.Add(newEntry);
                }
        
            }
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
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += Server_IT_PrePopulateScene;
            SceneDirector.onGenerateInteractableCardSelection += RemoveUnneededInteractables;
            //PrePopuate does not get called on Client
            On.RoR2.SceneDirector.Start += SceneDirector_Start;


            On.RoR2.DirectorCard.IsAvailable += Allow_Earlier_Spawns;
            visitedScenes.Clear();
            VoidSafeWard_Hooks.baseRadius += WConfig.ArtifactOfRealityBonusRadius.Value;
            On.RoR2.Run.PickNextStageScene += RandomStages;
            DoDestinations();
        }

        private static void SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
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

            PortalStatueBehavior[] newtList2 = Object.FindObjectsOfType(typeof(PortalStatueBehavior)) as PortalStatueBehavior[];
            if (newtList2.Length > 0)
            {
                Material newMat = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/voidstage/matVoidCrystal.mat").WaitForCompletion();
                GameObject SupressorObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/VoidSuppressor");
                Transform MainTransform = newtList2[0].gameObject.transform.parent;

                for (int i = 0; i < MainTransform.childCount; i++)
                {
                    Transform NewtAltar = MainTransform.GetChild(i);
                    if (NewtAltar.GetComponent<PortalStatueBehavior>())
                    {
                        Debug.Log(NewtAltar);
                        NewtAltar.gameObject.SetActive(true);
                        if (NetworkServer.active)
                        {
                            GameObject VoidSuppressor = GameObject.Instantiate(SupressorObject, NewtAltar);
                            VoidSuppressor.transform.localPosition = new Vector3(0, -1.38f, 0);
                            NetworkServer.Spawn(VoidSuppressor);
                        }
                        NewtAltar.GetChild(0).gameObject.SetActive(false);
                        NewtAltar.GetChild(1).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(2).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(3).GetComponent<MeshRenderer>().material = newMat;
                        NewtAltar.GetChild(4).GetComponent<MeshRenderer>().material = newMat;
                        GameObject.Destroy(NewtAltar.GetComponent<PurchaseInteraction>());
                    }
                }
            }

            if (NetworkServer.active)
            {
                Run.instance.PickNextStageScene(null);
            }
            //
            GameObject ITWeather = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Weather, InfiniteTower.prefab").WaitForCompletion());

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

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer -= Server_IT_PrePopulateScene;
            SceneDirector.onGenerateInteractableCardSelection -= RemoveUnneededInteractables;
            On.RoR2.SceneDirector.Start -= SceneDirector_Start;
            if (Run.instance)
            {
                RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
                Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Normal;
            }
            On.RoR2.DirectorCard.IsAvailable -= Allow_Earlier_Spawns;
            VoidSafeWard_Hooks.baseRadius -= WConfig.ArtifactOfRealityBonusRadius.Value;
            On.RoR2.Run.PickNextStageScene -= RandomStages;
          
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

 

        private static void RandomStages(On.RoR2.Run.orig_PickNextStageScene orig, Run self, WeightedSelection<SceneDef> choices)
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
                        weightedSelection.AddChoice(voidStage, 3f);
                    }

                    /*
                    */
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
                    SceneDef[] array = SceneCatalog.allStageSceneDefs.Where(new System.Func<SceneDef, bool>(RealityValidSceneDefs)).ToArray<SceneDef>();
                    self.nextStageScene = self.nextStageRng.NextElementUniform<SceneDef>(array);
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


        private static void Server_IT_PrePopulateScene(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            sceneDirector.RemoveAllExistingSpawnPoints();
            orig(self, sceneDirector);
            sceneDirector.monsterCredit = 0;
            sceneDirector.teleporterSpawnCard = null;
        }

        private static void RemoveUnneededInteractables(SceneDirector arg1, DirectorCardCategorySelection dccs)
        {
            int voidIndex = dccs.FindCategoryIndexByName("Void Stuff");
            if (voidIndex != -1)
            {
                dccs.categories[voidIndex].selectionWeight *= 2 + 0.5f;

                DirectorCard ADVoidTriple = new DirectorCard
                {
                    spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion(),
                    selectionWeight = 15,
                };
                DirectorCard Barrel = new DirectorCard
                {
                    spawnCard = SimulacrumDCCS.SafteyBarrel,
                    selectionWeight = 6,
                };
                dccs.AddCard(voidIndex, Barrel);
                dccs.AddCard(voidIndex, ADVoidTriple);
            }
            dccs.RemoveCardsThatFailFilter(new System.Predicate<DirectorCard>(SimulacrumTrimmer));
        }

        public static bool SimulacrumTrimmer(DirectorCard card)
        {
            GameObject prefab = card.spawnCard.prefab;
            if (card.spawnCard.name.StartsWith("iscBroken") || card.spawnCard.name.EndsWith("Drone"))
            {
                return false;
            }
            return !(prefab.GetComponent<RoR2.ShrineCombatBehavior>() | prefab.GetComponent<RoR2.HalcyoniteShrineInteractable>() | prefab.GetComponent<RoR2.OutsideInteractableLocker>() | prefab.GetComponent<RoR2.ShrineBossBehavior>() | prefab.GetComponent<RoR2.SeerStationController>() | prefab.GetComponent<RoR2.PortalStatueBehavior>());
        }
    }


}
