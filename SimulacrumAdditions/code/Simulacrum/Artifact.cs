using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class Artifact
    {
        public static ArtifactDef ArtifactSimulacrum = ScriptableObject.CreateInstance<ArtifactDef>();

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


            ArtifactSimulacrum.cachedName = "AAAAugmentsOnly";
            ArtifactSimulacrum.nameToken = "Artifact of Augments";
            ArtifactSimulacrum.descriptionToken = "All waves will be augmented waves.";
            ArtifactSimulacrum.smallIconSelectedSprite = ArtifactOnS;
            ArtifactSimulacrum.smallIconDeselectedSprite = ArtifactOffS;
            ContentAddition.AddArtifactDef(ArtifactSimulacrum);

            On.RoR2.Run.OverrideRuleChoices += ArtifactInITOnly;

            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;

            if (WConfig.cfgSacrificeBalance.Value)
            {
                On.RoR2.InfiniteTowerWaveController.DropRewards += (orig, self) =>
                {
                    bool sacrifice = RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.sacrificeArtifactDef);
                    SimulacrumExtrasHelper temp = self.GetComponent<SimulacrumExtrasHelper>();
                    if (sacrifice && self.rewardOptionCount > 1)
                    {
                        self.rewardOptionCount--;
                        if (temp && temp.rewardOptionCount > 1)
                        {
                            temp.rewardOptionCount--;
                        }
                    }
                    orig(self);
                };
                On.RoR2.Artifacts.SacrificeArtifactManager.OnArtifactEnabled += SacrificeArtifactManager_OnArtifactEnabled;
                On.RoR2.Artifacts.SacrificeArtifactManager.OnArtifactDisabled += SacrificeArtifactManager_OnArtifactDisabled;
            }
        }

        private static void SacrificeArtifactManager_OnArtifactEnabled(On.RoR2.Artifacts.SacrificeArtifactManager.orig_OnArtifactEnabled orig, RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            orig(runArtifactManager, artifactDef);
            if (!NetworkServer.active)
            {
                return;
            }
            if (artifactDef != RoR2Content.Artifacts.sacrificeArtifactDef)
            {
                return;
            }
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                Debug.LogWarning("Simulacrum : Added Sacrifice");
                On.RoR2.Util.GetExpAdjustedDropChancePercent += SimulacrumNerfSacrifice;
            }
        }

        private static float SimulacrumNerfSacrifice(On.RoR2.Util.orig_GetExpAdjustedDropChancePercent orig, float baseChancePercent, GameObject characterBodyObject)
        {
            return orig(baseChancePercent, characterBodyObject) * 0.7f;
        }

        private static void SacrificeArtifactManager_OnArtifactDisabled(On.RoR2.Artifacts.SacrificeArtifactManager.orig_OnArtifactDisabled orig, RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            orig(runArtifactManager, artifactDef);
            if (!NetworkServer.active)
            {
                return;
            }
            if (artifactDef != RoR2Content.Artifacts.sacrificeArtifactDef)
            {
                return;
            }
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                Debug.LogWarning("Simulacrum : Removed Sacrifice");
                On.RoR2.Util.GetExpAdjustedDropChancePercent -= SimulacrumNerfSacrifice;
            }
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
                self.weightedSelection.AddChoice(self.wavePrefabs[i].wavePrefab, self.wavePrefabs[i].weight + 1);
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