using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class SimuWavesOther
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
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

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

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;
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

            InfiniteTowerWaveCategory.WeightedWave ITHeresy = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHeresy, weight = 7f, prerequisites = SimuMain.StartWave11Prerequisite };
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
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveCoffee.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "AttackSpeedAndMoveSpeed";
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 3;

            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Caffeine";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies have increased movement and attack speed.";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 0.88f);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.3f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITCoffee = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveCoffee, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITCoffee);
            //
            //
            //Lepton
            GameObject InfiniteTowerWaveLepton = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;

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
            //Behemoth
            GameObject InfiniteTowerWaveBehemoth = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveItemBehemoth", true);
            GameObject InfiniteTowerWaveBehemothUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveItemBehemothUI", false);

            InfiniteTowerWaveBehemoth.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBehemothUI;
            InfiniteTowerWaveBehemoth.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
            InfiniteTowerWaveBehemoth.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveBehemoth.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBehemoth.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;

            InfiniteTowerWaveBehemoth.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "Behemoth";
            InfiniteTowerWaveBehemoth.GetComponent<SimulacrumGiveItemsOnStart>().count = 0;
            InfiniteTowerWaveBehemoth.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 1f;

            InfiniteTowerWaveBehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Volatility";
            InfiniteTowerWaveBehemothUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemy attacks cause explosions.";
            InfiniteTowerWaveBehemothUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveBehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveBehemothUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITBehemoth = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBehemoth, weight = 3f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBehemoth);
            //
            //
            //WaveBossShinyPearl
            GameObject InfiniteTowerWaveWaveBossShinyPearl = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossItemShinyPearl", true);
            GameObject InfiniteTowerWaveWaveBossShinyPearlUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossItemShinyPearlUI", false);

            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveWaveBossShinyPearlUI;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().baseCredits = 350;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;
            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = LegacyResourcesAPI.Load<ExplicitPickupDropTable>("DropTables/dtPearls"); ;

            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "ShinyPearl";
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().count = -3;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 3;

            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Irradiance";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies have all stats drastically increased.";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.9f, 0.9f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0.5f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 1f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITWaveBossShinyPearl = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveWaveBossShinyPearl, weight = 6f, prerequisites = SimuMain.StartWave30Prerequisite };
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
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;
            //
            GameObject PulseLunar = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MoonBatteryDesignPulse"), "ITPulseLunar", true);
            PulseLunar.GetComponent<PulseController>().finalRadius = 60;
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
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;
            //
            GameObject PulseVoid = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseVoid", true);
            PulseVoid.GetComponent<PulseController>().finalRadius = 60;
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
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;
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
            Pulse1.baseForce = -12000;
            Pulse1.pulseInterval = 1.3f;

            Pulse2 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = null;
            Pulse2.buffDuration = 0;
            Pulse2.pulsePrefab = PulseSuckInward;
            Pulse2.baseForce = -12000;
            Pulse2.pulseInterval = 1.3f;
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
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().baseCredits = 179;
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