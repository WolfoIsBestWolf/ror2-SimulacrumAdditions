using R2API;
using RoR2;
//using System;
using UnityEngine;

namespace SimulacrumAdditions
{
    public class Artifact_OnlyAugments
    {
        public static ArtifactDef ArtifactSimulacrum = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void MakeArtifact()
        {

            ArtifactSimulacrum.cachedName = "AAAAugmentsOnly";
            ArtifactSimulacrum.nameToken = "ARTIFACT_AUGMENTS_NAME";
            ArtifactSimulacrum.descriptionToken = "ARTIFACT_AUGMENTS_DESCRIPTION";
            ArtifactSimulacrum.smallIconSelectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/ArtifactOn.png");
            ArtifactSimulacrum.smallIconDeselectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/ArtifactOff.png");
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
            Constant.ITBasicWaves.GenerateWeightedSelection();
            Constant.ITBossWaves.GenerateWeightedSelection();
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactSimulacrum)
            {
                return;
            }
            On.RoR2.InfiniteTowerWaveCategory.GenerateWeightedSelection -= OnlySpecialWaves;
            Constant.ITBasicWaves.GenerateWeightedSelection();
            Constant.ITBossWaves.GenerateWeightedSelection();
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