using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_ItemGiving
    {
        internal static void MakeWaves()
        {
            #region Stack existing items high
            //Don't change name
            //Many Items
            GameObject InfiniteTowerWaveManyItems = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItems", true);
            GameObject InfiniteTowerWaveManyItemsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItemsUI", false);

            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveManyItemsUI;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryDamage;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Hoarding";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters get higher stacks of items.";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);
            /*InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.58f, 0.58f);*/

            InfiniteTowerWaveCategory.WeightedWave ManyItems = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveManyItems, weight = 6f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ManyItems);
            #endregion
            #region Random Heresy Items
            //Heresy
            //DO not change the name of this wave
            On.RoR2.GenericSkill.SetSkillOverride += FixHeresyForEnemies;
            GameObject InfiniteTowerWaveHeresy = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveHeresy", true);
            GameObject InfiniteTowerWaveHeresyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveHeresyUI", false);

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveHeresy.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITHeresy;
            InfiniteTowerWaveHeresy.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;

            Texture2D texITWaveLunarHeresy = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarHeresy.LoadImage(Properties.Resources.texITWaveLunarHeresy, true);
            texITWaveLunarHeresy.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarHeresyS = Sprite.Create(texITWaveLunarHeresy, WRect.rec64, WRect.half);
 
            Color HeresyColor = new Color(0.4f, 0.45f, 0.79f, 1);
            //0.1882 0.498 1 1
            //0.298 0.3294 0.5647 1
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHeresyUI;
            //InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarHeresyS;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Heresy";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies gain heresy items for the wave.";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;

            InfiniteTowerWaveCategory.WeightedWave ITHeresy = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHeresy, weight = 8f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITHeresy);
            #endregion
            //
            #region Many Mocha 
            //Coffee
            GameObject InfiniteTowerWaveCoffee = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemCoffee", true);
            GameObject InfiniteTowerWaveCoffeeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemCoffeeUI", false);

            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveCoffeeUI;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryDamage;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveCoffee.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "AttackSpeedAndMoveSpeed";
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 3;

            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Caffeine";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies have increased movement and attack speed.";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 0.88f);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.3f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITCoffee = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveCoffee, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITCoffee);
            #endregion
            #region Many Knockback Fin
            //KnockbackFin
            GameObject InfiniteTowerWaveKnockbackFin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemKnockbackFin", true);
            GameObject InfiniteTowerWaveKnockbackFinUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemKnockbackFinUI", false);

            InfiniteTowerWaveKnockbackFin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveKnockbackFinUI;
            //InfiniteTowerWaveKnockbackFin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
            InfiniteTowerWaveKnockbackFin.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveKnockbackFin.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveKnockbackFin.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveKnockbackFin.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "KnockBackHitEnemies";
            InfiniteTowerWaveKnockbackFin.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveKnockbackFin.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 2;

            InfiniteTowerWaveKnockbackFinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Knockback";
            InfiniteTowerWaveKnockbackFinUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies knock you up into the air on hit.";
            InfiniteTowerWaveKnockbackFinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 0.88f);
            //InfiniteTowerWaveKnockbackFinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveKnockbackFinUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITKnockbackFin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveKnockbackFin, weight = 2f, prerequisites = SimuMain.StartWave20PrerequisiteDLC2 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITKnockbackFin);
            #endregion
            #region Lepton Daisy
            //Lepton
            GameObject InfiniteTowerWaveLepton = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryHealing;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveLepton.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "TPHealingNova";
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0;

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLeptonUI;
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Flowers";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters spawning may unleash a healing aura.";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicLepton = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLepton, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicLepton);
            #endregion
            //
            #region (Boss) Behemoth
            //BossBehemoth
            GameObject InfiniteTowerWaveBossBehemoth = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveItemBossBehemoth", true);
            GameObject InfiniteTowerWaveBossBehemothUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemBossBehemothUI", false);

            InfiniteTowerWaveBossBehemoth.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossBehemothUI;

            InfiniteTowerWaveBossBehemoth.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossCategoryDamage;
            InfiniteTowerWaveBossBehemoth.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            InfiniteTowerWaveBossBehemoth.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "Behemoth";
            InfiniteTowerWaveBossBehemoth.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveBossBehemoth.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.5f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveBossBehemoth.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 50;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveBossBehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Volatility";
            InfiniteTowerWaveBossBehemothUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemy attacks cause explosions.";
            InfiniteTowerWaveBossBehemothUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveBossBehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveBossBehemothUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITBossBehemoth = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossBehemoth, weight = 5f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossBehemoth);
            #endregion
            #region (Boss) Shiny Pearl (Stupid Idea)
            //WaveBossShinyPearl
            GameObject InfiniteTowerWaveWaveBossShinyPearl = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossItemShinyPearl", true);
            GameObject InfiniteTowerWaveWaveBossShinyPearlUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossItemShinyPearlUI", false);

            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveWaveBossShinyPearlUI;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().baseCredits = 250;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 10;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossBonusRed;

            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = LegacyResourcesAPI.Load<ExplicitPickupDropTable>("DropTables/dtPearls");

            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "ShinyPearl";
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 1;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 60;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Irradiance";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies have all stats increased.";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.9f, 0.9f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0.5f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 1f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITWaveBossShinyPearl = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveWaveBossShinyPearl, weight = 5f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITWaveBossShinyPearl);
            #endregion
        }


        private static void FixHeresyForEnemies(On.RoR2.GenericSkill.orig_SetSkillOverride orig, GenericSkill self, object source, RoR2.Skills.SkillDef skillDef, GenericSkill.SkillOverridePriority priority)
        {
            if (priority == GenericSkill.SkillOverridePriority.Replacement && !self.characterBody.isPlayerControlled && self.stateMachine)
            {
                //Debug.Log(source);
                EntityStateMachine stateMachine = self.stateMachine;
                orig(self, source, skillDef, priority);
                if (stateMachine)
                {
                    self.stateMachine = stateMachine;
                }
                return;
            }
            else
            {
                orig(self, source, skillDef, priority);
            }
        }

    }

}