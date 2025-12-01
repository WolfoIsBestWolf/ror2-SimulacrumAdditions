using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace SimulacrumAdditions.Waves
{
    public class Waves_SuperBoss
    {
        public static GameObject WaveBoss_ScavLunar;

        internal static void MakeWaves()
        {
            InfiniteTowerExplicitSpawnWaveController wave = null;
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            float ITSpecialBossWaveWeight = 2.5f;


            #region Twisted Scavengers
            //Scav Lunar
            WaveBoss_ScavLunar = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_ScavLunar", true);
            GameObject InfiniteTowerCurrentBossScavLunarWaveUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "InfiniteTowerCurrentBossScavLunarWaveUI", false);
            wave = WaveBoss_ScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            wave.baseCredits = 50;
            wave.linearCreditsPerWave = 4;
            wave.secondsBeforeSuddenDeath = 120;
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITWaveTier3;
            wave.secondsAfterWave += 7;
            WaveBoss_ScavLunar.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            WaveBoss_ScavLunar.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITLunar;
            WaveBoss_ScavLunar.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;
            WaveBoss_ScavLunar.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.Lunar;

            SimuExplicitStats simuExplicitStats = WaveBoss_ScavLunar.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.5f;
            simuExplicitStats.hpBonusMulti = 0.5f;
            simuExplicitStats.halfOnNonFinal = false;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_SCAVLUNAR";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_SCAVLUNAR";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveScavLunar.png");
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ScavLunar, weight = ITSpecialBossWaveWeight * 3f, prerequisites = Constant.StartWave35Prerequisite };
            //
            #endregion
            #region Voidling
            //Voidling
            GameObject WaveBoss_VoidRaidCrab = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_VoidRaidCrab", true);
            GameObject InfiniteTowerCurrentBossVoidRaidWaveUI = PrefabAPI.InstantiateClone(Constant.VoidWaveUI, "InfiniteTowerCurrentBossVoidRaidWaveUI", false);
            wave = WaveBoss_VoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscMiniVoidRaidCrabPhase3IT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase3.asset").WaitForCompletion());
            cscMiniVoidRaidCrabPhase3IT.name = "cscMiniVoidRaidCrabPhase3IT";
            cscMiniVoidRaidCrabPhase3IT.hullSize = HullClassification.Human; //Bro refused to spawn so many times

            wave.spawnList[0].spawnCard = cscMiniVoidRaidCrabPhase3IT;
            wave.secondsBeforeSuddenDeath = 120;
            wave.baseCredits = 0;
            wave.linearCreditsPerWave = 0;
            wave.immediateCreditsFraction = 0.75f;
            WaveBoss_VoidRaidCrab.GetComponent<CombatDirector>().monsterCards = SuperMegaCrab.dccsVoidFamilyNoBarnacle;

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITWaveTier3;
            wave.secondsAfterWave += 2;
            WaveBoss_VoidRaidCrab.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier2;
            WaveBoss_VoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITVoid;
            WaveBoss_VoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 160;

            simuExplicitStats = WaveBoss_VoidRaidCrab.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.5f;
            simuExplicitStats.hpBonusMulti = 2f; //What we aiming for?
            simuExplicitStats.halfOnNonFinal = true;

            WaveBoss_VoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_VOIDLING";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_VOIDLING";

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_VoidRaidCrab, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Constant.StartWave50Prerequisite };
            //
            #endregion
            #region Aurelionite
            //Gold Titan
            GameObject WaveBoss_TitanGold = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_TitanGold", true);
            GameObject InfiniteTowerCurrentBossTitanGoldWaveUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossTitanGoldWaveUI", false);
            wave = WaveBoss_TitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscTitanGoldIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscTitanGoldIT.name = "cscTitanGoldIT";
            cscTitanGoldIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            wave.spawnList[0].spawnCard = cscTitanGoldIT;

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITWaveTier3;
            WaveBoss_TitanGold.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;
            WaveBoss_TitanGold.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonsters.asset").WaitForCompletion();

            wave.immediateCreditsFraction = 0.15f;
            wave.baseCredits = 100;
            wave.linearCreditsPerWave = 2;
            wave.secondsBeforeSuddenDeath = 120f;

            simuExplicitStats = WaveBoss_TitanGold.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 1.2f;
            simuExplicitStats.damageBonusMulti = 0.7f;
            simuExplicitStats.halfOnNonFinal = true;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossTitanGoldWaveUI;
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_TITANGOLD";
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_TITANGOLD";

            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGoldTitan.png");
            //InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_TitanGold, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Constant.StartWave25Prerequisite };
            #endregion
            #region Sots_FalseSon
            //SEEKERS FALSE SON
            GameObject WaveBoss_FalseSon = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_FalseSon", true);
            GameObject InfiniteTowerCurrentBossFalseSonWaveUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossFalseSonWaveUI", false);
            wave = WaveBoss_FalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            CharacterSpawnCard cscFalseSonIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscFalseSonIT.name = "cscFalseSonIT";
            cscFalseSonIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossMaster.prefab").WaitForCompletion();
            cscFalseSonIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 0 } };
            WaveBoss_FalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscFalseSonIT;

            //Could make it a gold fragment but idk
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITBossBonusRed;
            wave.rewardOptionCount = 5;
            WaveBoss_FalseSon.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;

            WaveBoss_FalseSon.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/dccsShrineHalcyoniteActivationMonsterWave.asset").WaitForCompletion();

            wave.immediateCreditsFraction = 0.5f;
            wave.baseCredits = 10;
            wave.linearCreditsPerWave = 1;
            wave.secondsBeforeSuddenDeath = 120f;
            wave.rewardPickupPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FragmentPotentialPickup.prefab").WaitForCompletion();

            simuExplicitStats = WaveBoss_FalseSon.AddComponent<SimuExplicitStats>();
            simuExplicitStats.halfOnNonFinal = true;
            simuExplicitStats.GetComponent<SimuExplicitStats>().hpBonusMulti = 3f;
            simuExplicitStats.GetComponent<SimuExplicitStats>().damageBonusMulti = 0.8f;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossFalseSonWaveUI;
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FALSESON";
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FALSESON";


            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveFalseSon.png");
            //InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossFalseSon = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_FalseSon, weight = ITSpecialBossWaveWeight, prerequisites = Constant.DLC2_StartWave30Prerequisite };
            #endregion

            #region SuperRoboBall
            GameObject WaveBoss_SuperRoboBallBoss = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_SuperRoboBallBoss", true);
            GameObject InfiniteTowerCurrentBossSuperRoboBallBossWaveUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerCurrentBossSuperRoboBallBossWaveUI", false);
            wave = WaveBoss_SuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscSuperRoboBallBossIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscSuperRoboBallBoss.asset").WaitForCompletion());
            cscSuperRoboBallBossIT.name = "cscSuperRoboBallBossIT";
            cscSuperRoboBallBossIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            wave.spawnList[0].spawnCard = cscSuperRoboBallBossIT;

            wave.immediateCreditsFraction = 0.15f;
            wave.baseCredits = 100;
            wave.linearCreditsPerWave = 2; //Evens out at 400 for wave 50
            wave.secondsBeforeSuddenDeath = 120f;

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITWaveTier3;

            WaveBoss_SuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;
            WaveBoss_SuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            simuExplicitStats = WaveBoss_SuperRoboBallBoss.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 0.9f;
            simuExplicitStats.damageBonusMulti = 0.7f;
            simuExplicitStats.halfOnNonFinal = true;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_SUPERBALL";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_SUPERBALL";

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0f, 1f, 0.76f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SuperRoboBallBoss, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Constant.StartWave25Prerequisite };
            #endregion

            #region Solus Amalgamator
            //Amalgam +1 every 15? No Scaling?
            GameObject WaveBoss_SolusAmalgam = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_SolusAmalgam", true);
            GameObject WaveBoss_SolusAmalgamUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "WaveBoss_SolusAmalgamUI", false);
            wave = WaveBoss_SolusAmalgam.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscSolusAmalgamator = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "14bf22df446f37549aa65eb724c1ddda").WaitForCompletion());
            cscSolusAmalgamator.name = "cscSolusAmalgamatorIT";
            cscSolusAmalgamator.hullSize = HullClassification.Human;

            wave.spawnList[0].spawnCard = cscSolusAmalgamator;
            wave.baseCredits = 500;
            wave.immediateCreditsFraction = 0.25f;
  
            wave.rewardDisplayTier = ItemTier.Boss;
            wave.rewardDropTable = Constant.dtITSpecialBossYellow;
            wave.secondsAfterWave += 2;
            WaveBoss_SolusAmalgam.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            simuExplicitStats = WaveBoss_SolusAmalgam.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = -1f;
            simuExplicitStats.hpBonusMulti = -1f;
            simuExplicitStats.OneExtraSpawnStartingWave = 24; //15 : 1, 25 : 2; 40 : 3
            simuExplicitStats.ExtraSpawnPerWaves = 15;

            WaveBoss_SolusAmalgam.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_SolusAmalgamUI;
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DLC3_SOLUSAMALGAM";
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DLC3_SOLUSAMALGAM";
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveAC.png");
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.97f, 0.45f, 1);
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.97f, 0.45f, 1);
            WaveBoss_SolusAmalgamUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1f, 0.97f, 0.45f, 2) * 0.8f;
            InfiniteTowerWaveCategory.WeightedWave WaveBoss_SolusAmalgamIT = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SolusAmalgam, weight = 4f, prerequisites = Constant.DLC3_StartWave10Prerequisite };

            #endregion
            #region Vulture Hunter
            //Amalgam +1 every 15? No Scaling?
            GameObject WaveBoss_VultureHunter = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_VultureHunter", true);
            GameObject WaveBoss_VultureHunterUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "WaveBoss_VultureHunterUI", false);
            wave = WaveBoss_VultureHunter.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscVultureHunterator = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "c4db4ac813bcc2e41a4dd19401c36eb3").WaitForCompletion());
            cscVultureHunterator.name = "cscVultureHunterIT";
            cscVultureHunterator.hullSize = HullClassification.Human;

            wave.spawnList[0].spawnCard = cscVultureHunterator;
            wave.baseCredits = 100;
            wave.linearCreditsPerWave = 2;
            wave.immediateCreditsFraction = 0.05f;
            wave.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "17ceb46d5861cef4cb1a106de50812b4").WaitForCompletion(); //dccsConduitcanyonMonsters
            wave.rewardDisplayTier = ItemTier.Boss;
            wave.rewardDropTable = Constant.dtITSpecialBossYellow;
            wave.secondsAfterWave += 2;
            WaveBoss_VultureHunter.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            simuExplicitStats = WaveBoss_VultureHunter.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 1f;
            simuExplicitStats.hpBonusMulti = 1f;
  
            WaveBoss_VultureHunter.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_VultureHunterUI;
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DLC3_VULTUREHUNTER";
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DLC3_VULTUREHUNTER";
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveAC.png");
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.366f, 0.7f, 0.5f, 1);
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.366f, 0.7f, 0.5f, 1);
            WaveBoss_VultureHunterUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.366f, 0.7f, 0.5f, 2) * 0.8f;
            InfiniteTowerWaveCategory.WeightedWave WaveBoss_VultureHunterIT = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_VultureHunter, weight = 4f, prerequisites = Constant.DLC3_StartWave25Prerequisite };

            #endregion
            #region Solus Wing
            //Amalgam +1 every 15? No Scaling?
            GameObject WaveBoss_SolusWing = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_SolusWing", true);
            GameObject WaveBoss_SolusWingUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "WaveBoss_SolusWingUI", false);
            wave = WaveBoss_SolusWing.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscSolusWing = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "a80a3fe518fea1a4b8b09791483f486d").WaitForCompletion());
            cscSolusWing.name = "cscSolusWingIT";
            cscSolusWing.hullSize = HullClassification.BeetleQueen;
            cscSolusWing.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;

            wave.spawnList[0].spawnCard = cscSolusWing;
            wave.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "c1ed67f5f16b8ad499d4b2976054eda8").WaitForCompletion(); //dccsSolutionalhauntMonsters

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Constant.dtITWaveTier3;
            wave.secondsAfterWave += 2;
            WaveBoss_SolusWing.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
            WaveBoss_SolusWing.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITTechnology;
            WaveBoss_SolusWing.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

            simuExplicitStats = WaveBoss_SolusWing.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.5f;
            simuExplicitStats.hpBonusMulti = 0.5f;
 
            WaveBoss_SolusWing.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_SolusWingUI;
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DLC3_SOLUSWING";
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DLC3_SOLUSWING";
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveAC.png");
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.1f, 0.77f, 0.9f, 1);
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.1f, 0.77f, 0.9f, 1);
            WaveBoss_SolusWingUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.1f, 0.77f, 0.9f, 1) * 0.8f;
            //InfiniteTowerWaveCategory.WeightedWave WaveBoss_SolusWingIT = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SolusWing, weight = ITSpecialBossWaveWeight, prerequisites = Constant.DLC3_StartWave50Prerequisite };

            #endregion


            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(WaveBoss_SolusAmalgamIT, WaveBoss_VultureHunterIT/*, WaveBoss_SolusWingIT*/, ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITBossFalseSon);
            Constant.ITSuperBossWaves.wavePrefabs = Constant.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossFalseSon/*, WaveBoss_SolusWingIT*/);

        }

    }

}