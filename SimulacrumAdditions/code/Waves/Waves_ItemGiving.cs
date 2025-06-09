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
            GameObject InfiniteTowerWaveManyItems = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveManyItems", true);
            GameObject InfiniteTowerWaveManyItemsUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveManyItemsUI", false);

            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveManyItemsUI;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryDamage;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_MANYITEMS";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_MANYITEMS";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);
            /*InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.58f, 0.58f);*/

            InfiniteTowerWaveCategory.WeightedWave ManyItems = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveManyItems, weight = 6f, prerequisites = Const.StartWave30Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ManyItems);
            #endregion
            #region Random Heresy Items
            //Heresy
            //DO not change the name of this wave
            On.RoR2.GenericSkill.SetSkillOverride += FixHeresyForEnemies;
            GameObject InfiniteTowerWaveHeresy = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveHeresy", true);
            GameObject InfiniteTowerWaveHeresyUI = PrefabAPI.InstantiateClone(Const.LunarWaveUI, "InfiniteTowerWaveHeresyUI", false);

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier1;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveHeresy.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITHeresy;
            InfiniteTowerWaveHeresy.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;

 
            Color HeresyColor = new Color(0.4f, 0.45f, 0.79f, 1);
            //0.1882 0.498 1 1
            //0.298 0.3294 0.5647 1
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHeresyUI;
            //InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarHeresyS;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_HERESY";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_HERESY";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;

            InfiniteTowerWaveCategory.WeightedWave ITHeresy = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHeresy, weight = 8f, prerequisites = Const.StartWave11Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITHeresy);
            #endregion
            //
            #region Many Mocha 
            //Coffee
            GameObject InfiniteTowerWaveCoffee = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveItemCoffee", true);
            GameObject InfiniteTowerWaveCoffeeUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveItemCoffeeUI", false);

            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveCoffeeUI;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryDamage;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveCoffee.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "AttackSpeedAndMoveSpeed";
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().count = 6;
            InfiniteTowerWaveCoffee.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 2;

            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_COFFEE";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_COFFEE";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Const.texITWaveDefaultWhiteS;
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.3f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITCoffee = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveCoffee, weight = 3f, prerequisites = Const.AfterWave5Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITCoffee);
            #endregion
            
            #region Lepton Daisy
            //Lepton
            GameObject InfiniteTowerWaveLepton = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveItemLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveItemLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryHealing;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveLepton.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "TPHealingNova";
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().count = 0;
            InfiniteTowerWaveLepton.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.5f;

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLeptonUI;
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_LEPTON";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_LEPTON";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicLepton = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLepton, weight = 4f, prerequisites = Const.StartWave20Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicLepton);
            #endregion

            #region Shuriken
            GameObject InfiniteTowerWaveShuriken = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveItemShuriken", true);
            GameObject InfiniteTowerWaveShurikenUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveItemShurikenUI", false);

            InfiniteTowerWaveShuriken.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveShurikenUI;
            InfiniteTowerWaveShuriken.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryDamage;

            InfiniteTowerWaveShuriken.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "PrimarySkillShuriken";
            InfiniteTowerWaveShuriken.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveShuriken.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.2f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveShuriken.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 25;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SHURIKEN";
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_SHURIKEN";
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.54f, 0.912f, 0.312f); //0.54 0.912 0.312 1.2
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/PrimarySkillShuriken/bdPrimarySkillShurikenBuff.asset").WaitForCompletion().iconSprite;
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.54f, 0.912f, 0.312f);
            InfiniteTowerWaveShurikenUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.45f, 0.76f, 0.26f,1f);

            InfiniteTowerWaveCategory.WeightedWave ITShuriken = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveShuriken, weight = 2.5f, prerequisites = Const.StartWave15Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITShuriken);
            #endregion
            #region Elixir
            GameObject InfiniteTowerWaveElixir = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveItemElixir", true);
            GameObject InfiniteTowerWaveElixirUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveItemElixirUI", false);

            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveElixirUI;
            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryHealing;
            InfiniteTowerWaveElixir.GetComponent<InfiniteTowerWaveController>().baseCredits = 140f;

            InfiniteTowerWaveElixir.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "HealingPotion";
            InfiniteTowerWaveElixir.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveElixir.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.2f;

            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_ELIXIR";
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ELIXIR";
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.52f, 0.44f);
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Const.texITWaveDefaultWhiteS;
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.52f, 0.44f);
            InfiniteTowerWaveElixirUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.98f, 0.34f, 0.31f);

            InfiniteTowerWaveCategory.WeightedWave ITElixir = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveElixir, weight = 2f, prerequisites = Const.StartWave20Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITElixir);
            #endregion
            //
            #region (Boss) Behemoth
            //BossBehemoth
            GameObject WaveBoss_Behemoth = PrefabAPI.InstantiateClone(Const.BossWave, "InfiniteTowerWaveItemBossBehemoth", true);
            GameObject WaveBoss_BehemothUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveItemBossBehemothUI", false);

            WaveBoss_Behemoth.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_BehemothUI;

            WaveBoss_Behemoth.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossCategoryDamage;
            WaveBoss_Behemoth.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            WaveBoss_Behemoth.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "Behemoth";
            WaveBoss_Behemoth.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            WaveBoss_Behemoth.GetComponent<SimulacrumGiveItemsOnStart>().extraPer10Wave = 0.5f;

            simulacrumGiveItemsOnStart = WaveBoss_Behemoth.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 50;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_BEHEMOTH";
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_BEHEMOTH";
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            WaveBoss_BehemothUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITBossBehemoth = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_Behemoth, weight = 5f, prerequisites = Const.StartWave20Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossBehemoth);
            #endregion
            #region (Boss) Shiny Pearl (Stupid Idea)
            //WaveBossShinyPearl
            GameObject InfiniteTowerWaveWaveBossShinyPearl = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_ItemShinyPearl", true);
            GameObject InfiniteTowerWaveWaveBossShinyPearlUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "WaveBoss_ItemShinyPearlUI", false);

            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveWaveBossShinyPearlUI;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().baseCredits = 250;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 10;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveWaveBossShinyPearl.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossBonusRed;

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

            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_PEARL";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_PEARL";
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.9f, 0.9f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 0.5f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1f, 1f);
            InfiniteTowerWaveWaveBossShinyPearlUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 1f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITWaveBossShinyPearl = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveWaveBossShinyPearl, weight = 5f, prerequisites = Const.StartWave20Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITWaveBossShinyPearl);
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