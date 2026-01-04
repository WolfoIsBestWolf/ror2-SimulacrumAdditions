using R2API;
using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace SimulacrumAdditions.Waves
{
    public class Waves_ItemGiving
    {
        internal static void MakeWaves()
        {

            #region Stack existing items high
            //Don't change name
            //Many Items
            GameObject InfiniteTowerWaveManyItems = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveManyItems", true);
            GameObject InfiniteTowerWaveManyItemsUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveManyItemsUI", false);
            InfiniteTowerWaveManyItems.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.TooManyItems;

            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveManyItemsUI;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryDamage;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;


            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_MANYITEMS";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_MANYITEMS";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.7f, 0.4f, 0);
            /*InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.58f, 0.58f);*/

            InfiniteTowerWaveCategory.WeightedWave ManyItems = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveManyItems, weight = 6f, prerequisites = Constant.StartWave30Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ManyItems);
            #endregion
            #region Random Heresy Items
            //Heresy
            //DO not change the name of this wave

            GameObject InfiniteTowerWaveHeresy = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveHeresy", true);
            GameObject InfiniteTowerWaveHeresyUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "InfiniteTowerWaveHeresyUI", false);
            InfiniteTowerWaveHeresy.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.HeresyItems;

            //InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            //InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveHeresy.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITHeresy;
            InfiniteTowerWaveHeresy.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;

            //InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;


            Color HeresyColor = new Color(0.4f, 0.45f, 0.79f, 1);
            //0.1882 0.498 1 1
            //0.298 0.3294 0.5647 1
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHeresyUI;
            //InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarHeresyS;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_HERESY";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_HERESY";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = HeresyColor;

            InfiniteTowerWaveCategory.WeightedWave ITHeresy = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHeresy, weight = 8f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITHeresy);
            #endregion
            //
            #region Many Mocha 
            //Coffee
            GameObject InfiniteTowerWaveCoffee = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveItemCoffee", true);
            GameObject InfiniteTowerWaveCoffeeUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveItemCoffeeUI", false);

            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveCoffeeUI;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryDamage;
            //InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            //InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;

            InfiniteTowerWaveCoffee.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "AttackSpeedAndMoveSpeed";
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 2;

            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_COFFEE";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_COFFEE";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Constant.texITWaveDefaultWhiteS;
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.6f, 0.3f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITCoffee = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveCoffee, weight = 3f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITCoffee);
            #endregion

            #region Lepton Daisy
            //Lepton
            GameObject InfiniteTowerWaveLepton = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveItemLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveItemLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryHealing;
            //InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;

            InfiniteTowerWaveLepton.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "TPHealingNova";
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().count = 0;
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.5f;

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLeptonUI;
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_LEPTON";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_LEPTON";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.6f, 0.8f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicLepton = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLepton, weight = 4f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicLepton);
            #endregion

            #region Shuriken
            GameObject InfiniteTowerWaveShuriken = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveItemShuriken", true);
            GameObject InfiniteTowerWaveShurikenUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveItemShurikenUI", false);

            InfiniteTowerWaveShuriken.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveShurikenUI;
            InfiniteTowerWaveShuriken.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryDamage;

            InfiniteTowerWaveShuriken.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "PrimarySkillShuriken";
            InfiniteTowerWaveShuriken.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveShuriken.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.2f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveShuriken.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 25;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SHURIKEN";
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_SHURIKEN";
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.54f, 0.912f, 0.312f); //0.54 0.912 0.312 1.2
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/PrimarySkillShuriken/bdPrimarySkillShurikenBuff.asset").WaitForCompletion().iconSprite;
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.54f, 0.912f, 0.312f);
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.45f, 0.76f, 0.26f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITShuriken = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveShuriken, weight = 2.5f, prerequisites = Constant.StartWave15Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITShuriken);
            #endregion
            #region Elixir
            GameObject InfiniteTowerWaveElixir = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveItemElixir", true);
            GameObject InfiniteTowerWaveElixirUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveItemElixirUI", false);

            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveElixirUI;
            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryHealing;
            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().baseCredits = 140f;

            InfiniteTowerWaveElixir.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "HealingPotion";
            InfiniteTowerWaveElixir.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveElixir.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.2f;

            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_ELIXIR";
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ELIXIR";
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.52f, 0.44f);
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Constant.texITWaveDefaultWhiteS;
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 0.52f, 0.44f);
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.98f, 0.34f, 0.31f);

            InfiniteTowerWaveCategory.WeightedWave ITElixir = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveElixir, weight = 2f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITElixir);
            #endregion
            //
            #region (Boss) Behemoth
            //BossBehemoth
            GameObject WaveBoss_Behemoth = PrefabAPI.InstantiateClone(Constant.BossWave, "InfiniteTowerWaveItemBossBehemoth", true);
            GameObject WaveBoss_BehemothUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerWaveItemBossBehemothUI", false);

            WaveBoss_Behemoth.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_BehemothUI;

            WaveBoss_Behemoth.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossCategoryDamage;
            WaveBoss_Behemoth.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            WaveBoss_Behemoth.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "Behemoth";
            WaveBoss_Behemoth.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            WaveBoss_Behemoth.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.5f;

            simulacrumGiveItemsOnStart = WaveBoss_Behemoth.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 50;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_BEHEMOTH";
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_BEHEMOTH";
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f, 0.5f);
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITBossBehemoth = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_Behemoth, weight = 5f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossBehemoth);
            #endregion
            #region (Boss) Shiny Pearl (Stupid Idea)
            //WaveBossShinyPearl
            GameObject InfiniteTowerWaveWaveBossShinyPearl = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_ItemShinyPearl", true);
            GameObject InfiniteTowerWaveWaveBossShinyPearlUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_ItemShinyPearlUI", false);

            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveWaveBossShinyPearlUI;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().baseCredits = 250;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 10;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossBonusRed;

            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = LegacyResourcesAPI.Load<ExplicitPickupDropTable>("DropTables/dtPearls");

            InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "ShinyPearl";
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().count = 4;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 1;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveWaveBossShinyPearl.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 60;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_PEARL";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_PEARL";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.9f, 0.9f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 0.5f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.7f, 1f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITWaveBossShinyPearl = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveWaveBossShinyPearl, weight = 5f, prerequisites = Constant.StartWave35Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITWaveBossShinyPearl);
            #endregion
        }



    }

}