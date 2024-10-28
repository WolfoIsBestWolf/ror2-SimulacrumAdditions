using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class ArtifactAugments
    {
        public static ArtifactDef ArtifactSimulacrum = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void MakeArtifact()
        {
            Rect rec = new Rect(0, 0, 64, 64);
            Texture2D ArtifactOn = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/ArtifactOn.png");
            ArtifactOn.filterMode = FilterMode.Trilinear;
            Sprite ArtifactOnS = Sprite.Create(ArtifactOn, rec, new Vector2(0, 0));

            Texture2D ArtifactOff = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/ArtifactOff.png");
            ArtifactOff.filterMode = FilterMode.Trilinear;
            Sprite ArtifactOffS = Sprite.Create(ArtifactOff, rec, new Vector2(0, 0));


            ArtifactSimulacrum.cachedName = "AAAAugmentsOnly";
            ArtifactSimulacrum.nameToken = "ARTIFACT_AUGMENTS_NAME";
            ArtifactSimulacrum.descriptionToken = "ARTIFACT_AUGMENTS_DESCRIPTION";
            ArtifactSimulacrum.smallIconSelectedSprite = ArtifactOnS;
            ArtifactSimulacrum.smallIconDeselectedSprite = ArtifactOffS;
            ContentAddition.AddArtifactDef(ArtifactSimulacrum);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;
            On.RoR2.WeeklyRun.OverrideRuleChoices += WeeklyArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;
        }

        private static void WeeklyArtifactInITOnly(On.RoR2.WeeklyRun.orig_OverrideRuleChoices orig, WeeklyRun self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);
            self.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindRuleDef("Artifacts.AAAAugmentsOnly").FindChoice("Off"));
            self.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindRuleDef("Artifacts.AAAUseNormalStages").FindChoice("Off"));
        }

        private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactSimulacrum)
            {
                return;
            }
            On.RoR2.InfiniteTowerWaveCategory.GenerateWeightedSelection += OnlySpecialWaves;
            SimuMain.ITBasicWaves.GenerateWeightedSelection();
            SimuMain.ITBossWaves.GenerateWeightedSelection();
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactSimulacrum)
            {
                return;
            }
            On.RoR2.InfiniteTowerWaveCategory.GenerateWeightedSelection -= OnlySpecialWaves;
            SimuMain.ITBasicWaves.GenerateWeightedSelection();
            SimuMain.ITBossWaves.GenerateWeightedSelection();
        }

        public static void OnlySpecialWaves(On.RoR2.InfiniteTowerWaveCategory.orig_GenerateWeightedSelection orig, InfiniteTowerWaveCategory self)
        {
            self.weightedSelection.Clear();
            self.weightedSelection.AddChoice(self.wavePrefabs[0].wavePrefab, 0);
            for (int i = 1; i < self.wavePrefabs.Length; i++)
            {
                self.weightedSelection.AddChoice(self.wavePrefabs[i].wavePrefab, self.wavePrefabs[i].weight);
            }
            return;
        }

        private static void ArtifactInITOnly(On.RoR2.Run.orig_OverrideRuleChoices orig, Run self, RuleChoiceMask mustInclude, RuleChoiceMask mustExclude, ulong runSeed)
        {
            orig(self, mustInclude, mustExclude, runSeed);
            if (self && !(self is InfiniteTowerRun))
            {
                self.ForceChoice(mustInclude, mustExclude, RuleCatalog.FindRuleDef("Artifacts.AAAAugmentsOnly").FindChoice("Off"));
            }
        }
    }


}