using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class ArtifactReal
    {
        public static ArtifactDef ArtifactUseNormalStages = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void MakeArtifact()
        {
            Rect rec = new Rect(0, 0, 64, 64);
            Texture2D ArtifactOn = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            ArtifactOn.filterMode = FilterMode.Trilinear;
            ArtifactOn.LoadImage(Properties.Resources.Artifact2_On, true);
            Sprite ArtifactOnS = Sprite.Create(ArtifactOn, rec, new Vector2(0, 0));

            Texture2D ArtifactOff = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            ArtifactOff.filterMode = FilterMode.Trilinear;
            ArtifactOff.LoadImage(Properties.Resources.Artifact2_Off, true);
            Sprite ArtifactOffS = Sprite.Create(ArtifactOff, rec, new Vector2(0, 0));


            ArtifactUseNormalStages.cachedName = "AAAUseNormalStages";
            ArtifactUseNormalStages.nameToken = "Artifact of Reality";
            ArtifactUseNormalStages.descriptionToken = "Normal stages will be used instead of Simulacrum stages.";
            ArtifactUseNormalStages.smallIconSelectedSprite = ArtifactOnS;
            ArtifactUseNormalStages.smallIconDeselectedSprite = ArtifactOffS;
            ContentAddition.AddArtifactDef(ArtifactUseNormalStages);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;


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
            On.RoR2.SceneDirector.Start += ArtifactReal_SceneDirector_Start;
            SceneDirector.onGenerateInteractableCardSelection += RemoveUnneededInteractables;
        }

        private static void ArtifactReal_SceneDirector_Start(On.RoR2.SceneDirector.orig_Start orig, SceneDirector self)
        {
            orig(self);

            GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Weather, InfiniteTower.prefab").WaitForCompletion());
            GameObject Weather = GameObject.Find("/Weather, Wispgraveyard");
            if (Weather)
            {
                Weather.SetActive(false);
            }

            CombatDirector[] combatDirector = self.gameObject.GetComponents<CombatDirector>();
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
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer -= Server_IT_PrePopulateScene;
            On.RoR2.SceneDirector.Start -= ArtifactReal_SceneDirector_Start;
            SceneDirector.onGenerateInteractableCardSelection -= RemoveUnneededInteractables;
            if (Run.instance)
            {
                RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
                Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Normal;
            }
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
            return !(prefab.GetComponent<RoR2.ShrineCombatBehavior>() | prefab.GetComponent<RoR2.OutsideInteractableLocker>() | prefab.GetComponent<RoR2.ShrineBossBehavior>() | prefab.GetComponent<RoR2.SeerStationController>() | prefab.GetComponent<RoR2.PortalStatueBehavior>());
        }
    }


}
