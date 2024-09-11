using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class WavesMiscSorted
    {
        internal static void MakeItemGivingWaves()
        {
            //
            //Don't change name
            //Many Items
            GameObject InfiniteTowerWaveManyItems = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItems", true);
            GameObject InfiniteTowerWaveManyItemsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItemsUI", false);

            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveManyItemsUI;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
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
            //
            //
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

            SimuMain.ITDamageDown.pickupIconSprite = texITWaveLunarHeresyS;
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
            //Automated ones
            //
            //Coffee
            GameObject InfiniteTowerWaveCoffee = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemCoffee", true);
            GameObject InfiniteTowerWaveCoffeeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemCoffeeUI", false);

            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveCoffeeUI;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
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
            //
            //
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
            //
            //
            //Lepton
            GameObject InfiniteTowerWaveLepton = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveLepton.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "TPHealingNova";
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().count = -1;
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 1;

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLeptonUI;
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Flowers";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters spawning may unleash a healing aura.";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicLepton = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLepton, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicLepton);
            //
            //
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
            //
            //
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
        }

        internal static void MakePulseWaves()
        {
            On.EntityStates.Missions.Moon.MoonBatteryComplete.OnEnter += (orig, self) =>
            {
                if (self.gameObject.GetComponent<SimulacrumPulseWave>())
                {
                    return;
                }
                orig(self);
            };


            //Lunar
            GameObject InfiniteTowerWavePulseLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseLunar", true);
            GameObject InfiniteTowerWavePulseLunarUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseLunarUI", false);

            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseLunar = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MoonBatteryDesignPulse"), "ITPulseLunar", true);
            PulseLunar.GetComponent<PulseController>().finalRadius = 75;
            PulseLunar.GetComponent<PulseController>().duration = 0.6f;
            PulseLunar.transform.GetChild(1).localScale *= 2;
            PulseLunar.transform.GetChild(2).localScale *= 2;
            PulseLunar.transform.GetChild(4).localScale *= 3;

            BuffDef Cripple = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/Cripple");
            InfiniteTowerWavePulseLunar.AddComponent<SimulacrumPulseWave>().buffDef = Cripple;
            InfiniteTowerWavePulseLunar.GetComponent<SimulacrumPulseWave>().buffDuration = 3;
            InfiniteTowerWavePulseLunar.GetComponent<SimulacrumPulseWave>().pulsePrefab = PulseLunar;
            InfiniteTowerWavePulseLunar.GetComponent<SimulacrumPulseWave>().baseForce = 7000;
            InfiniteTowerWavePulseLunar.GetComponent<SimulacrumPulseWave>().pulseInterval = 1.5f;
            //
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseLunarUI;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Design";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A crippling pulse is being emmited.";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Cripple.iconSprite;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Cripple.buffColor;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.55f,0.85f,0.95f);
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseLunar, weight = 4, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseLunar);
            //
            //
            //Void
            GameObject InfiniteTowerWavePulseVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseVoid", true);
            GameObject InfiniteTowerWavePulseVoidUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseVoidUI", false);

            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseVoid = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseVoid", true);
            PulseVoid.GetComponent<PulseController>().finalRadius = 75;
            PulseVoid.GetComponent<PulseController>().duration = 0.6f;
            Material matNewVoid = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulseVoid = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulseVoid.LoadImage(Properties.Resources.texRampPulseVoid, true);
            texRampPulseVoid.wrapMode = TextureWrapMode.Clamp;
            texRampPulseVoid.filterMode = FilterMode.Point;
            matNewVoid.SetTexture("_RemapTex", texRampPulseVoid);
            PulseVoid.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewVoid;
            PulseVoid.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(1f, 0.2f, 1f, 1f);
            PulseVoid.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.255f, 0.2f, 0.255f, 1f);
            PulseVoid.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.9137f, 0.3569f, 0.9137f, 0.5922f);

            BuffDef NullifyStack = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/NullifyStack");
            BuffDef Nullified = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/Nullified");
            SimulacrumPulseWave Pulse1 = InfiniteTowerWavePulseVoid.AddComponent<SimulacrumPulseWave>();
            Pulse1.buffDef = NullifyStack;
            Pulse1.buffDuration = 20;
            Pulse1.pulsePrefab = PulseVoid;
            Pulse1.baseForce = 3500;
            Pulse1.pulseInterval = 1.5f;

            SimulacrumPulseWave Pulse2 = InfiniteTowerWavePulseVoid.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = NullifyStack;
            Pulse2.buffDuration = 20;
            Pulse2.pulsePrefab = PulseVoid;
            Pulse2.baseForce = 3500;
            Pulse2.pulseInterval = 1.5f;
            Pulse2.affectedTeam = TeamIndex.Monster;
            //
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseVoidUI;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Nullifciation";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Nullifying grounded subjects.";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Nullified.iconSprite;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Nullified.buffColor;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Nullified.buffColor;
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseVoid, weight = 4, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseVoid);
            //
            //
            //Reverse Force
            GameObject InfiniteTowerWavePulseSuckInward = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseSuckInward", true);
            GameObject InfiniteTowerWavePulseSuckInwardUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseSuckInwardUI", false);

            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseSuckInward = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseSuckInward", true);
            PulseSuckInward.GetComponent<PulseController>().finalRadius = 100;
            PulseSuckInward.GetComponent<PulseController>().duration = 1f;
            PulseSuckInward.transform.localPosition = new Vector3(0, -2.5f, 0);
            Material matNewPulseSuckInward = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseSuckInward = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulsematNewPulseSuckInward.LoadImage(Properties.Resources.texRampPulseSuck, true);
            texRampPulsematNewPulseSuckInward.wrapMode = TextureWrapMode.Clamp;
            texRampPulsematNewPulseSuckInward.filterMode = FilterMode.Point;
            matNewPulseSuckInward.SetTexture("_RemapTex", texRampPulsematNewPulseSuckInward);
            PulseSuckInward.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewPulseSuckInward;
            PulseSuckInward.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.2f, 0.2f, 1f);
            PulseSuckInward.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.2f, 0.2f, 1f);
            PulseSuckInward.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.3f, 0.3f, 0.3f, 0.5922f);
            //
            BuffDef ElementalRingVoidCooldown = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/ElementalRingVoidCooldown");
            //
            Pulse1 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse1.buffDef = null;
            Pulse1.buffDuration = 0;
            Pulse1.pulsePrefab = PulseSuckInward;
            Pulse1.baseForce = -11000;
            Pulse1.pulseInterval = 1.15f;

            Pulse2 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = null;
            Pulse2.buffDuration = 0;
            Pulse2.pulsePrefab = PulseSuckInward;
            Pulse2.baseForce = -11000;
            Pulse2.pulseInterval = 1.15f;
            Pulse2.affectedTeam = TeamIndex.Monster;
            //
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseSuckInwardUI;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Attraction";
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Grounded monsters get pulled inwards.";
            //InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.55f, 0.55f, 0.5f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ElementalRingVoidCooldown.iconSprite;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = ElementalRingVoidCooldown.buffColor;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.6f, 0.6f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.3f, 0.3f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseSuckInward = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseSuckInward, weight = 4, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseSuckInward);

            //PulseNoHealing
            GameObject InfiniteTowerWavePulseNoHealing = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseNoHealing", true);
            GameObject InfiniteTowerWavePulseNoHealingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseNoHealingUI", false);

            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            ///
            GameObject PulseNoHealing = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseNoHealing", true);
            PulseNoHealing.GetComponent<PulseController>().finalRadius = 150;
            PulseNoHealing.GetComponent<PulseController>().duration = 1.5f;
            Material matNewPulseNoHealing = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseNoHealing = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulsematNewPulseNoHealing.LoadImage(Properties.Resources.texRampPulseNoHeal, true);
            texRampPulsematNewPulseNoHealing.wrapMode = TextureWrapMode.Clamp;
            texRampPulsematNewPulseNoHealing.filterMode = FilterMode.Point;
            matNewPulseNoHealing.SetTexture("_RemapTex", texRampPulsematNewPulseNoHealing);
            PulseNoHealing.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewPulseNoHealing;
            PulseNoHealing.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.6f, 0.2f, 1f);
            PulseNoHealing.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.15f, 0.255f, 0.15f, 1f);
            PulseNoHealing.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.7f, 0.2f, 0.58f);


            BuffDef HealingDisabled = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HealingDisabled");
            InfiniteTowerWavePulseNoHealing.AddComponent<SimulacrumPulseWave>().buffDef = HealingDisabled;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().buffDuration = 8;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().pulsePrefab = PulseNoHealing;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().baseForce = 0;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().pulseInterval = 1.5f;
            //
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseNoHealingUI;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Poison";
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your healing will be disabled if pulsed.";
            //InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.6f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = HealingDisabled.iconSprite;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HealingDisabled.buffColor;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.9f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.5f, 0.2f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseNoHealing = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseNoHealing, weight = 4, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseNoHealing);
        }

        internal static void MakeEquipmentWaves()
        {
            //Flight
            GameObject InfiniteTowerWaveJetpack = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFlight", true);
            GameObject InfiniteTowerWaveJetpackUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveJetpackUI", false);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveJetpack.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 0.75f;
            InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;
            InfiniteTowerWaveJetpack.AddComponent<SimuEquipmentWaveHelper>();
            InfiniteTowerWaveJetpack.GetComponent<SimuEquipmentWaveHelper>().variant = 0;
            InfiniteTowerWaveJetpack.AddComponent<SetGravity>().newGravity = -20;

            Texture2D texITWaveJetpack = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveJetpack.LoadImage(Properties.Resources.texITWaveJetpack, true);
            texITWaveJetpack.filterMode = FilterMode.Bilinear;
            Sprite texITWaveJetpackS = Sprite.Create(texITWaveJetpack, WRect.rec64, WRect.half);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveJetpackUI;
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Flight";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies gain flight.";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveJetpackS;
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(190, 158, 202, 255);
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(190, 158, 202, 255);

            InfiniteTowerWaveCategory.WeightedWave ITBasicJetpack = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveJetpack, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicJetpack);
            //
            //

            //Malachite Elites
            GameObject InfiniteTowerWaveMalachitesOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElites", true);
            GameObject InfiniteTowerWaveMalachitesOnlyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElitesUI", false);

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;  //Refuses to spawn anything on late waves if on
            InfiniteTowerWaveMalachitesOnly.AddComponent<SimuEquipmentWaveHelper>().variant = 2;

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMalachitesOnlyUI;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Malachite";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies spawn as weakened Malachites.";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            //InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITEliteMalachite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMalachitesOnly, weight = 4.5f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITEliteMalachite);
            //
            //
            //Battery
            GameObject InfiniteTowerWaveBattery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBattery", true);
            GameObject InfiniteTowerWaveBatteryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBatteryUI", false);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBattery.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBattery.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveBattery.AddComponent<SimuEquipmentWaveHelper>().variant = 1;

            Texture2D texItEquipmentBasic = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texItEquipmentBasic.LoadImage(Properties.Resources.texItEquipmentBasic, true);
            texItEquipmentBasic.filterMode = FilterMode.Bilinear;
            Sprite texItEquipmentBasicS = Sprite.Create(texItEquipmentBasic, WRect.rec64, WRect.half);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBatteryUI;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Battery";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Improper storage may cause serious harm.";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicBattery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBattery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicBattery);

            On.EntityStates.QuestVolatileBattery.CountDown.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.vfxInstances.Length == 0)
                {
                    if (!self.networkedBodyAttachment.attachedBody)
                    {
                        return;
                    }
                    if (EntityStates.QuestVolatileBattery.CountDown.vfxPrefab && self.attachedCharacterModel)
                    {
                        self.vfxInstances = new GameObject[1];
                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityStates.QuestVolatileBattery.CountDown.vfxPrefab, self.networkedBodyAttachment.attachedBody.coreTransform);
                        gameObject.transform.localPosition = Vector3.zero;
                        gameObject.transform.localRotation = Quaternion.identity;
                        gameObject.transform.localScale = new Vector3(2.5f / gameObject.transform.lossyScale.x, 2.5f / gameObject.transform.lossyScale.y, 2.5f / gameObject.transform.lossyScale.z);
                        if (gameObject.transform.localScale.z < 1)
                        {
                            gameObject.transform.localScale = new Vector3(1, 1, 1); //Idk dude
                        }
                        gameObject.transform.GetChild(0).GetComponent<Light>().range *= 3;
                        gameObject.transform.GetChild(0).GetComponent<Light>().intensity *= 3;
                        gameObject.transform.GetChild(0).GetComponent<LightIntensityCurve>().timeMax /= 2;
                        self.vfxInstances[0] = gameObject;
                    }
                }
            };

            On.EntityStates.QuestVolatileBattery.CountDown.Detonate += (orig, self) =>
            {
                if (!self.networkedBodyAttachment.attachedBody)
                {
                    return;
                }
                bool suicide = false;
                if (self.networkedBodyAttachment.attachedBody.teamComponent.teamIndex == TeamIndex.Monster)
                {
                    suicide = true;
                    float newHealth = (77 + 33 * TeamManager.instance.GetTeamLevel(TeamIndex.Player)) / 6; //*3
                    self.networkedBodyAttachment.attachedBody.baseMaxHealth = newHealth;
                    self.networkedBodyAttachment.attachedBody.levelMaxHealth = 0;
                    self.networkedBodyAttachment.attachedBody.maxHealth = newHealth;
                    self.networkedBodyAttachment.attachedBody.maxShield = 0;
                }
                orig(self);
                if (suicide && self.attachedHealthComponent)
                {
                    self.attachedHealthComponent.Suicide();
                }
            };
            //
            //
            //Goobo
            GameObject InfiniteTowerWaveGoobo = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveGoobo", true);
            GameObject InfiniteTowerWaveGooboUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveGooboUI", false);

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            //
            InfiniteTowerWaveGoobo.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
            InfiniteTowerWaveGoobo.AddComponent<DisableArtifactOfSwarms>();

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().baseCredits = 70;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            //InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 45;
            InfiniteTowerWaveGoobo.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveGoobo.AddComponent<SimuEquipmentWaveHelper>().variant = 3;

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveGooboUI;
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gummy";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters spawn gummy clones.";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0.5f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGoobo = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveGoobo, weight = 3f, prerequisites = SimuMain.StartWave15Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicGoobo);

            //FixGooboSwarms.Fix();

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