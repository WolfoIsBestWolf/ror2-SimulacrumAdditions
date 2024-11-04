using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_Misc
    {
        public static InfiniteTowerExplicitSpawnWaveController DroneWave;

        internal static void MakeWaves()
        {
            #region Always Jumping / Bouncy Floor
            //AlwaysJumping Buff
            GameObject InfiniteTowerWaveAlwaysJumping = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveAlwaysJumping", true);
            GameObject InfiniteTowerWaveAlwaysJumpingUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveAlwaysJumpingUI", false);

            InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveAlwaysJumping>();
            //InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveBouncyProjectiles>();
            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryUtility;

            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveAlwaysJumpingUI;
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BOUNCY";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BOUNCY";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);

            InfiniteTowerWaveCategory.WeightedWave AlwaysJumpingWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveAlwaysJumping, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(AlwaysJumpingWave);
            #endregion

            #region All Credits instantly
            //Instant
            GameObject InfiniteTowerWaveSurprise = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveSurprise", true);
            GameObject InfiniteTowerWaveSurpriseUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveSurpriseUI", false);

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 2;

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSurpriseUI;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_ALLCREDITS";
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ALLCREDITS";
            //InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= 1.1f;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= 1.1f;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= 1.1f;

            InfiniteTowerWaveCategory.WeightedWave ITBasicSurprise = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSurprise, weight = 4f, prerequisites = SimuMain.StartWave15Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicSurprise);
            #endregion


            #region (Boss) Void Infestor Spam
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            //
            //
            //Infestor
            CharacterSpawnCard cscVoidInfestorIT;
            cscVoidInfestorIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/EliteVoid/cscVoidInfestor.asset").WaitForCompletion());
            cscVoidInfestorIT.name = "cscVoidInfestorIT";
            //cscVoidInfestorIT.directorCreditCost = 1;
            cscVoidInfestorIT.itemsToGrant = new ItemCountPair[] { 
                new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, 
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = 30 } 
            };
            DirectorCard SimuWaveVoidInfestor = new DirectorCard
            {
                spawnCard = cscVoidInfestorIT,
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCardCategorySelection dccsVoidInfestorOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsVoidInfestorOnly.AddCategory("Basic Monsters", 6);
            dccsVoidInfestorOnly.AddCard(0, SimuWaveVoidInfestor);
            dccsVoidInfestorOnly.name = "dccsVoidInfestorOnly";
            dccsVoidInfestorOnly.categories[0].cards[0].spawnCard = cscVoidInfestorIT;


            GameObject InfiniteTowerWaveBossVoidElites = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveBossVoidElites", true);
            GameObject InfiniteTowerCurrentBossVoidEliteWaveUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerCurrentBossVoidEliteWaveUI", false);
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscVoidInfestorIT;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 580;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.3f;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;
            InfiniteTowerWaveBossVoidElites.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            CombatDirector WaveVoidEliteDirector = InfiniteTowerWaveBossVoidElites.AddComponent<CombatDirector>();
            WaveVoidEliteDirector.monsterCredit = 550;
            WaveVoidEliteDirector.monsterCards = dccsVoidInfestorOnly;
            WaveVoidEliteDirector.skipSpawnIfTooCheap = false;
            WaveVoidEliteDirector.teamIndex = TeamIndex.Void;
            RangeFloat TempRangeFloatThing = new RangeFloat { max = 1, min = 0.5f };
            WaveVoidEliteDirector.moneyWaveIntervals = WaveVoidEliteDirector.moneyWaveIntervals.Add(TempRangeFloatThing);
            WaveVoidEliteDirector.creditMultiplier = 4;
            WaveVoidEliteDirector.maxSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.maxRerollSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minRerollSpawnInterval += 0.5f;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.VoidTier2;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITVoidInfestorWave;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidEliteWaveUI;
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_INFESTORS";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_INFESTORS";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidElites, weight = 8f, prerequisites = SimuMain.AfterWave5Prerequisite }; //7 normally
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossVoidElites);
            #endregion
            #region (Boss) Double Boss / Mountain
            //Mountain Boss
            GameObject InfiniteTowerWaveDoubleBoss = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBoss", true);
            GameObject InfiniteTowerWaveDoubleBossUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveDoubleBossUI", false);

            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDoubleBossUI;
            //InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsAfterWave = 14;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 40;
            InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().eliteBias = 0f;
            //InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = true;
            InfiniteTowerWaveDoubleBoss.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;

            Sprite texITWaveBossIconMountainS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveBossMountain.png"), WRect.rec64, WRect.half);

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconMountainS;
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_MOUNTAIN";
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_MOUNTAIN";

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.8f, 0.9f);
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.9f); //new Color(0.240566f, 0.8644956f, 1f);
            GameObject.Instantiate(InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerWaveDoubleBossUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITDoubleBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDoubleBoss, weight = 10f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITDoubleBoss);
            #endregion
            #region (Boss) Wave with Flame Drones
            //Wave with the drone in it
            GameObject InfiniteTowerWaveBossWithDrone = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveBossWithDrone", true);
            GameObject InfiniteTowerWaveBossDronesMachinesUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveBossWithDroneUI", false);

            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().baseCredits = 450f;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossCategoryUtility;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 50;
            InfiniteTowerWaveBossWithDrone.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            DroneWave = InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossDronesMachinesUI;

            SimuExplicitStats simuExplicitStats = InfiniteTowerWaveBossWithDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.8f;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            //InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Machines";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FLAMEDRONES";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FLAMEDRONES";

            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITInfiniteTowerWaveBossWithDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossWithDrone, weight = 6f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITInfiniteTowerWaveBossWithDrone);
            #endregion

            #region (Boss) Eclipse 8
            //ITBossEclipse8
            GameObject InfiniteTowerWaveBossEclipse8 = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEclipse8", true);
            GameObject InfiniteTowerWaveBossEclipse8UI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveBossEclipse8", false);

            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossEclipse8UI;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossEclipse8.AddComponent<SimulacrumExtrasHelper>().newRadius = 50;
            InfiniteTowerWaveBossEclipse8.AddComponent<SimulacrumEclipseWaveHelper>();

 
            Sprite texDifficultyEclipse8IconS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveEclipse.png"), WRect.rec64, WRect.half);

            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_ECLIPSE8";
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_ECLIPSE8";
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texDifficultyEclipse8IconS;
            //InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.5f, 0.8f);
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 0.95f);
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.1f, 0.15f, 0.4f);

            InfiniteTowerWaveCategory.WeightedWave ITBossEclipse8 = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEclipse8, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEclipse8);
            #endregion

            #region MeridianStorm
            GameObject InfiniteTowerWaveMeridianStorm = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveItemMeridianStorm", true);
            GameObject InfiniteTowerWaveMeridianStormUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveItemMeridianStormUI", false);

            InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMeridianStormUI;
            //InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryHealing;

            InfiniteTowerWaveMeridianStorm.AddComponent<SimulacrumLightningStormWave>();

            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PMSTORM";
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PMSTORM";
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.52f, 0.79f, 1f);
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/ShockNearby/bdTeslaField.asset").WaitForCompletion().iconSprite;
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.52f, 0.79f, 1f);
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.52f, 0.79f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITMeridianStorm = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMeridianStorm, weight = 2f, prerequisites = SimuMain.DLC2_StartWave15Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITMeridianStorm);
            #endregion
        }



    }

}