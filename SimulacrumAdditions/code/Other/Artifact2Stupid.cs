/*
using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class ArtifactStupid
    {
        public static ArtifactDef ArtifactUseNormalStages = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void MakeArtifact()
        {
            Rect rec = new Rect(0, 0, 64, 64);
            Texture2D ArtifactOn = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            ArtifactOn.filterMode = FilterMode.Trilinear;
            ArtifactOn.LoadImage(Properties.Resources.ArtifactOn, true);
            Sprite ArtifactOnS = Sprite.Create(ArtifactOn, rec, new Vector2(0, 0));

            Texture2D ArtifactOff = new Texture2D(64, 64, TextureFormat.RGBA32, false);
            ArtifactOff.filterMode = FilterMode.Trilinear;
            ArtifactOff.LoadImage(Properties.Resources.ArtifactOff, true);
            Sprite ArtifactOffS = Sprite.Create(ArtifactOff, rec, new Vector2(0, 0));


            ArtifactUseNormalStages.cachedName = "AAAUseNormalStages";
            ArtifactUseNormalStages.nameToken = "Artifact of Realism";
            ArtifactUseNormalStages.descriptionToken = "Normal stages will be used instead of Simulacrum Stages.";
            ArtifactUseNormalStages.smallIconSelectedSprite = ArtifactOnS;
            ArtifactUseNormalStages.smallIconDeselectedSprite = ArtifactOffS;
            ContentAddition.AddArtifactDef(ArtifactUseNormalStages);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;
        }

        private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
            Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Random;
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += InfiniteTowerRun_OnPrePopulateSceneServer;
            
            //Need to disable Teleporter
            //Need to disable Combat director
            //Need to spawn players on crab
            //Need to disable various useless interactables
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactUseNormalStages)
            {
                return;
            }
            RuleDef StageOrderRule = RuleCatalog.FindRuleDef("Misc.StageOrder");
            Run.instance.ruleBook.GetRuleChoice(StageOrderRule).extraData = StageOrder.Normal;
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer -= InfiniteTowerRun_OnPrePopulateSceneServer;
        }

        private static void InfiniteTowerRun_OnPrePopulateSceneServer(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            orig(self, sceneDirector);
            sceneDirector.monsterCredit = 0;
            GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Weather, InfiniteTower.prefab").WaitForCompletion());

        }

        private static void ArtifactInITOnly(On.RoR2.Run.orig_OverrideRuleChoices orig, Run self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);
            if (self && !(self is InfiniteTowerRun))
            {
                self.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindRuleDef("Artifacts.AAAUseNormalStages").FindChoice("Off"));
            }
        }
    }


}
*/