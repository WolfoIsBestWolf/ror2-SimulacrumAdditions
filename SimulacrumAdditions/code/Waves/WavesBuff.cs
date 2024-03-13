using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class WavesBuff
    {
        public static BuffDef bdSlippery;
        public static BuffDef bdBadLuck;

        internal static void MakeBuffWaves()
        {
            //VoidBear
            GameObject InfiniteTowerWaveVoidBear = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidBear", true);
            GameObject InfiniteTowerWaveVoidBearUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidBearUI", false);

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 15;
            InfiniteTowerWaveVoidBear.AddComponent<SimuBuffWaveHelper>().variant = 0;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveVoidBearUI;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Safety";
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies block the first instances of damage.";

            BuffDef BearVoidReady = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/BearVoidReady");
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BearVoidReady.iconSprite;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BearVoidReady.buffColor;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BearVoidReady.buffColor;
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.7f, 1f, 1);
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.85f, 0.55f, 1f, 1);
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.675f, 0.425f, 0.825f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicVoidBear = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidBear, weight = 5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicVoidBear);
            //
            //
            Texture2D texITWaveDefaultWhite = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveDefaultWhite.LoadImage(Properties.Resources.texITWaveDefaultWhite, true);
            texITWaveDefaultWhite.filterMode = FilterMode.Bilinear;
            Sprite texITWaveDefaultWhiteS = Sprite.Create(texITWaveDefaultWhite, WRect.rec64, WRect.half);

            //Blindness Buff
            GameObject InfiniteTowerWaveBlindness = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindness", true);
            GameObject InfiniteTowerWaveBlindnessUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindnessUI", false);

            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBlindnessUI;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBlindness.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToEnemies = false;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().buffDef = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/Common/Buffs/bdBlinded.asset").WaitForCompletion();

            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Blindness";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It becomes hard to see.";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);

            InfiniteTowerWaveCategory.WeightedWave Blindness = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBlindness, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Blindness);
            //
            //
            //Slippery Buff
            GameObject InfiniteTowerWaveSlippery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSlippery", true);
            GameObject InfiniteTowerWaveSlipperyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSlipperyUI", false);

            BuffDef Weak = RoR2.LegacyResourcesAPI.Load<BuffDef>("buffdefs/Weak");
            BuffDef bdOutOfCombatArmorBuff = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/OutOfCombatArmor/bdOutOfCombatArmorBuff.asset").WaitForCompletion();
            bdSlippery = ScriptableObject.CreateInstance<BuffDef>();
            bdSlippery.iconSprite = bdOutOfCombatArmorBuff.iconSprite;
            bdSlippery.buffColor = new Color(0.9f, 0.8f, 1f);
            bdSlippery.name = "bdITSlippery";
            bdSlippery.isDebuff = false;
            bdSlippery.canStack = false;
            bdSlippery.isCooldown = false;
            ContentAddition.AddBuffDef(bdSlippery);

            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSlipperyUI;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveSlippery.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().buffDef = bdSlippery;

            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Friction";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground turns slippery.";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.7f, 0.9f);

            InfiniteTowerWaveCategory.WeightedWave Slippery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSlippery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Slippery);
            //
            //
            //BadLuck Buff
            GameObject InfiniteTowerWaveBadLuck = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBadLuck", true);
            GameObject InfiniteTowerWaveBadLuckUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBadLuckUI", false);
            Color BadLuckColor = new Color(0.7f, 0.8f, 0.3f);

            bdBadLuck = ScriptableObject.CreateInstance<BuffDef>();
            bdBadLuck.iconSprite = Weak.iconSprite;
            bdBadLuck.buffColor = BadLuckColor;
            bdBadLuck.name = "bdITBadLuck";
            bdBadLuck.isDebuff = false;
            bdBadLuck.canStack = false;
            bdBadLuck.isCooldown = false;
            ContentAddition.AddBuffDef(bdBadLuck);

            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBadLuckUI;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBadLuck.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().addToEnemies = false;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().buffDef = bdBadLuck;

            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Misfortune";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Luck down.";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveDefaultWhiteS;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;

            InfiniteTowerWaveCategory.WeightedWave BadLuck = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBadLuck, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(BadLuck);
            //
            //
            //NoCooldowns Buff
            GameObject InfiniteTowerWaveNoCooldowns = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveNoCooldowns", true);
            GameObject InfiniteTowerWaveNoCooldownsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveNoCooldownsUI", false);

            BuffDef NoCooldowns = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/NoCooldowns");

            InfiniteTowerWaveNoCooldowns.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveNoCooldownsUI;
            InfiniteTowerWaveNoCooldowns.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().addToEnemies = true;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().buffDef = NoCooldowns;

            InfiniteTowerWaveNoCooldowns.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "BoostAttackSpeed";
            InfiniteTowerWaveNoCooldowns.GetComponent<SimulacrumGiveItemsOnStart>().count = 3;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimulacrumGiveItemsOnStart>().hideItem = true;

            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Frenzy";
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Skills have no cooldowns.";
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = NoCooldowns.iconSprite;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = NoCooldowns.buffColor;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = NoCooldowns.buffColor * 1.2f;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = NoCooldowns.buffColor * 0.9f;

            InfiniteTowerWaveCategory.WeightedWave NoCooldownsWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveNoCooldowns, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(NoCooldownsWave);
            //
            //
            //LunarTonic Buff
            GameObject InfiniteTowerWaveLunarTonic = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarTonic", true);
            GameObject InfiniteTowerWaveLunarTonicUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarTonicUI", false);

            BuffDef LunarTonic = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/TonicBuff");

            InfiniteTowerWaveLunarTonic.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLunarTonicUI;
            SimuBuffWaveHelper simuBuffWaveHelper = InfiniteTowerWaveLunarTonic.AddComponent<SimuBuffWaveHelper>();
            simuBuffWaveHelper.addToPlayer = true;
            simuBuffWaveHelper.addToEnemies = true;
            simuBuffWaveHelper.timed = true;
            simuBuffWaveHelper.duration = 14;
            simuBuffWaveHelper.buffDef = LunarTonic;

            InfiniteTowerWaveLunarTonic.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "TonicAffliction";
            InfiniteTowerWaveLunarTonic.GetComponent<SimulacrumGiveItemsOnStart>().count = 10;

            Color TonicColor = new Color(0.7f,0.8f,1f);

            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Tonic";
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have a short stat boost and are crippled afterwards.";
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = LunarTonic.iconSprite;
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = TonicColor;
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = TonicColor;

            InfiniteTowerWaveCategory.WeightedWave LunarTonicWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarTonic, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(LunarTonicWave);
            //
            //
            //BossLeeching Buff
            GameObject InfiniteTowerWaveBossLeeching = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossLeeching", true);
            GameObject InfiniteTowerWaveBossLeechingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossLeechingUI", false);

            BuffDef LifeSteal = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/LifeSteal");

            InfiniteTowerWaveBossLeeching.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossCategoryHealing;
            InfiniteTowerWaveBossLeeching.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            InfiniteTowerWaveBossLeeching.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossLeechingUI;
            simuBuffWaveHelper = InfiniteTowerWaveBossLeeching.AddComponent<SimuBuffWaveHelper>();
            simuBuffWaveHelper.addToPlayer = false;
            simuBuffWaveHelper.addToEnemies = true;
            simuBuffWaveHelper.buffDef = LifeSteal;

            Color LeechColor = new Color(0.9f,0.8f,0.3f);

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveBossLeeching.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.count = 1;
            simulacrumGiveItemsOnStart.itemString = "RepeatHeal";

            simulacrumGiveItemsOnStart = InfiniteTowerWaveBossLeeching.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.count = 1;
            simulacrumGiveItemsOnStart.itemString = "IncreaseHealing";

            simulacrumGiveItemsOnStart = InfiniteTowerWaveBossLeeching.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.count = 1;
            simulacrumGiveItemsOnStart.itemString = "BarrierOnOverHeal";


            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Leeching";
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters heal on hit and healing is increased.";
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = LeechColor;
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = LeechColor;
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = LeechColor;

            InfiniteTowerWaveCategory.WeightedWave BossLeechingWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossLeeching, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(BossLeechingWave);
        }

    }

}