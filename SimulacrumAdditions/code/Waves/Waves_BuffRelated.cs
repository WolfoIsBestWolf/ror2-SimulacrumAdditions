using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_BuffRelated
    {
        public static BuffDef bdSlippery;
        public static BuffDef bdBadLuck;

        internal static void MakeWaves()
        {
            Texture2D texITWaveDefaultWhite = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveBasicWhite.png");
            Sprite texITWaveDefaultWhiteS = Sprite.Create(texITWaveDefaultWhite, WRect.rec64, WRect.half);

            #region Void Bear Spam
            //VoidBear
            GameObject InfiniteTowerWaveVoidBear = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveVoidBear", true);
            GameObject InfiniteTowerWaveVoidBearUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerWaveVoidBearUI", false);

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 15;
            InfiniteTowerWaveVoidBear.AddComponent<SimuBuffWaveHelper>().variant = 0;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveVoidBearUI;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_VOIDBEAR";
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_VOIDBEAR";

            BuffDef BearVoidReady = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/BearVoidReady");
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BearVoidReady.iconSprite;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BearVoidReady.buffColor;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BearVoidReady.buffColor;
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.7f, 1f, 1);
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.85f, 0.55f, 1f, 1);
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.675f, 0.425f, 0.825f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicVoidBear = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidBear, weight = 5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicVoidBear);
            #endregion
            //
            #region Unused Blindness
            //Blindness Buff
            GameObject InfiniteTowerWaveBlindness = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveBlindness", true);
            GameObject InfiniteTowerWaveBlindnessUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveBlindnessUI", false);

            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBlindnessUI;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBlindness.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToEnemies = false;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().buffDef = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/Common/Buffs/bdBlinded.asset").WaitForCompletion();

            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BLIND";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BLIND";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);

            InfiniteTowerWaveCategory.WeightedWave Blindness = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBlindness, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Blindness);
            #endregion
            //
            #region Slippery Ground
            //Slippery Buff
            GameObject InfiniteTowerWaveSlippery = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveSlippery", true);
            GameObject InfiniteTowerWaveSlipperyUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveSlipperyUI", false);

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
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryUtility;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveSlippery.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().buffDef = bdSlippery;

            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SLIPPERY";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_SLIPPERY";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.7f, 0.9f);

            InfiniteTowerWaveCategory.WeightedWave Slippery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSlippery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Slippery);
            #endregion
            //
            #region No Procs
            //BadLuck Buff
            GameObject InfiniteTowerWaveBadLuck = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveBadLuck", true);
            GameObject InfiniteTowerWaveBadLuckUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveBadLuckUI", false);
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

            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BADLUCK";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BADLUCK";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveDefaultWhiteS;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;

            InfiniteTowerWaveCategory.WeightedWave BadLuck = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBadLuck, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(BadLuck);
            #endregion
            #region No Cooldowns for everyone
            //NoCooldowns Buff
            GameObject InfiniteTowerWaveNoCooldowns = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveNoCooldowns", true);
            GameObject InfiniteTowerWaveNoCooldownsUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveNoCooldownsUI", false);

            BuffDef NoCooldowns = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/NoCooldowns");

            InfiniteTowerWaveNoCooldowns.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveNoCooldownsUI;
            InfiniteTowerWaveNoCooldowns.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().addToEnemies = true;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimuBuffWaveHelper>().buffDef = NoCooldowns;

            InfiniteTowerWaveNoCooldowns.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "BoostAttackSpeed";
            InfiniteTowerWaveNoCooldowns.GetComponent<SimulacrumGiveItemsOnStart>().count = 3;
            InfiniteTowerWaveNoCooldowns.GetComponent<SimulacrumGiveItemsOnStart>().hideItem = true;

            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_NOCOOLDOWN";
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_NOCOOLDOWN";
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = NoCooldowns.iconSprite;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = NoCooldowns.buffColor;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = NoCooldowns.buffColor * 1.2f;
            InfiniteTowerWaveNoCooldownsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = NoCooldowns.buffColor * 0.9f;

            InfiniteTowerWaveCategory.WeightedWave NoCooldownsWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveNoCooldowns, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(NoCooldownsWave);
            #endregion
            //
            #region Free Drugs for everyone
            //LunarTonic Buff
            GameObject InfiniteTowerWaveLunarTonic = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveLunarTonic", true);
            GameObject InfiniteTowerWaveLunarTonicUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveLunarTonicUI", false);

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

            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_TONIC";
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_TONIC";
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = LunarTonic.iconSprite;
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = TonicColor;
            InfiniteTowerWaveLunarTonicUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = TonicColor;

            InfiniteTowerWaveCategory.WeightedWave LunarTonicWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarTonic, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(LunarTonicWave);
            #endregion
            //
            #region (Boss) Leeching
            //BossLeeching Buff
            GameObject InfiniteTowerWaveBossLeeching = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossLeeching", true);
            GameObject InfiniteTowerWaveBossLeechingUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveBossLeechingUI", false);

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


            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_LEECH";
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_LEECH";
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = LeechColor;
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = LeechColor;
            InfiniteTowerWaveBossLeechingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = LeechColor;
            
            InfiniteTowerWaveCategory.WeightedWave BossLeechingWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossLeeching, weight = 5f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(BossLeechingWave);
            #endregion
        }


    }

}