using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
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
            WaveBoss_ScavLunar = PrefabAPI.InstantiateClone(Const.ScavWave, "WaveBoss_ScavLunar", true);
            GameObject InfiniteTowerCurrentBossScavLunarWaveUI = PrefabAPI.InstantiateClone(Const.ScavWaveUI, "InfiniteTowerCurrentBossScavLunarWaveUI", false);
            wave = WaveBoss_ScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            wave.baseCredits = 50;
            wave.linearCreditsPerWave = 4;
            wave.secondsBeforeSuddenDeath = 120;
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Const.dtITWaveTier3;
            wave.secondsAfterWave += 7;
            WaveBoss_ScavLunar.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            WaveBoss_ScavLunar.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITLunar;
            WaveBoss_ScavLunar.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;
            WaveBoss_ScavLunar.AddComponent<SimulacrumEliteWaves>().lunarOnly = true;
 
            SimuExplicitStats simuExplicitStats = WaveBoss_ScavLunar.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.4f;
            simuExplicitStats.hpBonusMulti = 0.4f;
            simuExplicitStats.halfOnNonFinal = false;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_SCAVLUNAR";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_SCAVLUNAR";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveScavLunar.png");
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ScavLunar, weight = ITSpecialBossWaveWeight * 3f, prerequisites = Const.StartWave35Prerequisite };
            //
#endregion
            #region Voidling
            //Voidling
            GameObject WaveBoss_VoidRaidCrab = PrefabAPI.InstantiateClone(Const.ScavWave, "WaveBoss_VoidRaidCrab", true);
            GameObject InfiniteTowerCurrentBossVoidRaidWaveUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerCurrentBossVoidRaidWaveUI", false);
            wave = WaveBoss_VoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscMiniVoidRaidCrabPhase3IT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase3.asset").WaitForCompletion());
            cscMiniVoidRaidCrabPhase3IT.name = "cscMiniVoidRaidCrabPhase3IT";
            cscMiniVoidRaidCrabPhase3IT.hullSize = HullClassification.Golem; //Bro refused to spawn so many times

            wave.spawnList[0].spawnCard = cscMiniVoidRaidCrabPhase3IT;
            wave.secondsBeforeSuddenDeath = 120;
            wave.baseCredits = 0;
            wave.linearCreditsPerWave = 0;
            wave.immediateCreditsFraction = 0.75f;
            WaveBoss_VoidRaidCrab.GetComponent<CombatDirector>().monsterCards = SuperMegaCrab.dccsVoidFamilyNoBarnacle;

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Const.dtITWaveTier3;
            wave.secondsAfterWave += 2;
            WaveBoss_VoidRaidCrab.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier2;
            WaveBoss_VoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITVoid;
            WaveBoss_VoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 160;

            simuExplicitStats = WaveBoss_VoidRaidCrab.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.3f;
            simuExplicitStats.hpBonusMulti = 1.15f;
            simuExplicitStats.halfOnNonFinal = true;

            WaveBoss_VoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_VOIDLING";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_VOIDLING";

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_VoidRaidCrab, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Const.StartWave50Prerequisite };
            //
#endregion
            #region Aurelionite
            //Gold Titan
            GameObject WaveBoss_TitanGold = PrefabAPI.InstantiateClone(Const.ScavWave, "WaveBoss_TitanGold", true);
            GameObject InfiniteTowerCurrentBossTitanGoldWaveUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossTitanGoldWaveUI", false);
            wave = WaveBoss_TitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            CharacterSpawnCard cscTitanGoldIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscTitanGoldIT.name = "cscTitanGoldIT";
            cscTitanGoldIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            wave.spawnList[0].spawnCard = cscTitanGoldIT;

            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Const.dtITWaveTier3;
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
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_TITANGOLD";
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_TITANGOLD";
 
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGoldTitan.png");
            //InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_TitanGold, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Const.StartWave25Prerequisite };
            #endregion
            #region Sots_FalseSon
            //SEEKERS FALSE SON
            GameObject WaveBoss_FalseSon = PrefabAPI.InstantiateClone(Const.ScavWave, "WaveBoss_FalseSon", true);
            GameObject InfiniteTowerCurrentBossFalseSonWaveUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossFalseSonWaveUI", false);
            wave = WaveBoss_FalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            CharacterSpawnCard cscFalseSonIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscFalseSonIT.name = "cscFalseSonIT";
            cscFalseSonIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossMaster.prefab").WaitForCompletion();
            cscFalseSonIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 0 } };
            WaveBoss_FalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscFalseSonIT;

            //Could make it a gold fragment but idk
            wave.rewardDisplayTier = ItemTier.Tier3;
            wave.rewardDropTable = Const.dtITBossBonusRed;
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
            simuExplicitStats.GetComponent<SimuExplicitStats>().hpBonusMulti = 2.5f;
            simuExplicitStats.GetComponent<SimuExplicitStats>().damageBonusMulti = 1f;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossFalseSonWaveUI;
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FALSESON";
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FALSESON";

           
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveFalseSon.png");
            //InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossFalseSon = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_FalseSon, weight = ITSpecialBossWaveWeight, prerequisites = Const.DLC2_StartWave30Prerequisite };
#endregion

            #region SuperRoboBall
            GameObject WaveBoss_SuperRoboBallBoss = PrefabAPI.InstantiateClone(Const.ScavWave, "WaveBoss_SuperRoboBallBoss", true);
            GameObject InfiniteTowerCurrentBossSuperRoboBallBossWaveUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerCurrentBossSuperRoboBallBossWaveUI", false);
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
            wave.rewardDropTable = Const.dtITWaveTier3;

            WaveBoss_SuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;
            WaveBoss_SuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            simuExplicitStats = WaveBoss_SuperRoboBallBoss.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 0.9f;
            simuExplicitStats.damageBonusMulti = 0.7f;
            simuExplicitStats.halfOnNonFinal = true;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_SUPERBALL";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_SUPERBALL";
 
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SuperRoboBallBoss, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = Const.StartWave25Prerequisite };
            #endregion

             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITBossFalseSon);
            ITBossTitanGold.weight = 0.3f;
            Const.ITSuperBossWaves.wavePrefabs = Const.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossTitanGold, ITBossFalseSon); //ITBossSuperRoboBallBoss

        }

    }

}