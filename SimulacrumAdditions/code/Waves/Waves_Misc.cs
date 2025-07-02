using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions.Waves
{
    public class Waves_Misc
    {
        public static InfiniteTowerExplicitSpawnWaveController DroneWave;

        internal static void MakeWaves()
        {
            #region Always Jumping / Bouncy Floor
            //AlwaysJumping Buff
            GameObject InfiniteTowerWaveAlwaysJumping = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveAlwaysJumping", true);
            GameObject InfiniteTowerWaveAlwaysJumpingUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveAlwaysJumpingUI", false);

            InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveAlwaysJumping>();
            //InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveBouncyProjectiles>();
            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryUtility;

            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveAlwaysJumpingUI;
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BOUNCY";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BOUNCY";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);

            InfiniteTowerWaveCategory.WeightedWave AlwaysJumpingWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveAlwaysJumping, weight = 2.5f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(AlwaysJumpingWave);
            #endregion

            #region All Credits instantly
            //Instant
            GameObject InfiniteTowerWaveSurprise = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveSurprise", true);
            GameObject InfiniteTowerWaveSurpriseUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveSurpriseUI", false);

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusGreen;
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicSurprise = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSurprise, weight = 4f, prerequisites = Constant.StartWave15Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicSurprise);
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
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 30 }
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


            GameObject WaveBoss_VoidElites = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_VoidElites", true);
            GameObject InfiniteTowerCurrentBossVoidEliteWaveUI = PrefabAPI.InstantiateClone(Constant.VoidWaveUI, "InfiniteTowerCurrentBossVoidEliteWaveUI", false);
            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscVoidInfestorIT;

            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 580;
            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.3f;
            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            WaveBoss_VoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;
            WaveBoss_VoidElites.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_VoidElites.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.Infestor;
            WaveBoss_VoidElites.AddComponent<SimuExplicitStats>().hpBonusMulti = 3;

            CombatDirector WaveVoidEliteDirector = WaveBoss_VoidElites.AddComponent<CombatDirector>();
            WaveVoidEliteDirector.monsterCredit = 550;
            WaveVoidEliteDirector.monsterCards = dccsVoidInfestorOnly;
            WaveVoidEliteDirector.skipSpawnIfTooCheap = false;
            WaveVoidEliteDirector.teamIndex = TeamIndex.Void;
            WaveVoidEliteDirector.moneyWaveIntervals = new RangeFloat[]
            {
               new RangeFloat { max = 1, min = 0.5f }
        };
            WaveVoidEliteDirector.creditMultiplier = 4;
            WaveVoidEliteDirector.maxSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.maxRerollSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minRerollSpawnInterval += 0.5f;

            WaveBoss_VoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.VoidTier2;
            WaveBoss_VoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITVoidInfestorWave;

            WaveBoss_VoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidEliteWaveUI;
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_INFESTORS";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_INFESTORS";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_VoidElites, weight = 8f, prerequisites = Constant.AfterWave5Prerequisite }; //7 normally
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossVoidElites);
            #endregion
            #region (Boss) Double Boss / Mountain
            //Mountain Boss
            GameObject InfiniteTowerWaveDoubleBoss = PrefabAPI.InstantiateClone(Constant.BossWave, "InfiniteTowerWaveDoubleBoss", true);
            GameObject InfiniteTowerWaveDoubleBossUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerWaveDoubleBossUI", false);

            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDoubleBossUI;
            //InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsAfterWave = 14;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 40;
            InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().eliteBias = 0f;
            //InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = true;
            InfiniteTowerWaveDoubleBoss.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITWaveTier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;


            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveBossMountain.png");
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_MOUNTAIN";
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_MOUNTAIN";

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.8f, 0.9f);
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.9f); //new Color(0.240566f, 0.8644956f, 1f);
            GameObject.Instantiate(InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerWaveDoubleBossUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITDoubleBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDoubleBoss, weight = 10f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITDoubleBoss);
            #endregion
            #region (Boss) Wave with Flame Drones
            //Wave with the drone in it
            GameObject WaveBoss_WithDrone = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_WithDrone", true);
            GameObject WaveBoss_DronesMachinesUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_WithDroneUI", false);

            WaveBoss_WithDrone.GetComponent<InfiniteTowerWaveController>().baseCredits = 450f;
            WaveBoss_WithDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossCategoryUtility;
            WaveBoss_WithDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_WithDrone.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 50;
            WaveBoss_WithDrone.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            DroneWave = WaveBoss_WithDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            WaveBoss_WithDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_DronesMachinesUI;

            SimuExplicitStats simuExplicitStats = WaveBoss_WithDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.8f;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            //WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Machines";
            WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FLAMEDRONES";
            WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FLAMEDRONES";

            WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            WaveBoss_DronesMachinesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITWaveBoss_WithDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_WithDrone, weight = 6f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITWaveBoss_WithDrone);
            #endregion

            #region (Boss) Eclipse 8
            //ITBossEclipse8
            GameObject WaveBoss_Eclipse8 = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_Eclipse8", true);
            GameObject WaveBoss_Eclipse8UI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_Eclipse8", false);

            WaveBoss_Eclipse8.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_Eclipse8UI;
            WaveBoss_Eclipse8.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            WaveBoss_Eclipse8.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            WaveBoss_Eclipse8.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            WaveBoss_Eclipse8.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier3;
            WaveBoss_Eclipse8.AddComponent<SimulacrumExtrasHelper>().newRadius = 50;
            WaveBoss_Eclipse8.AddComponent<SimulacrumEclipseWaveHelper>();

            WaveBoss_Eclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_ECLIPSE8";
            WaveBoss_Eclipse8UI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_ECLIPSE8";
            WaveBoss_Eclipse8UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveEclipse.png");
            WaveBoss_Eclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 0.95f);
            WaveBoss_Eclipse8UI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.1f, 0.15f, 0.4f);

            InfiniteTowerWaveCategory.WeightedWave ITBossEclipse8 = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_Eclipse8, weight = 4f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossEclipse8);
            #endregion

            #region MeridianStorm
            GameObject InfiniteTowerWaveMeridianStorm = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveMeridianStorm", true);
            GameObject InfiniteTowerWaveMeridianStormUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveMeridianStormUI", false);

            InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMeridianStormUI;
            //InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryHealing;

            InfiniteTowerWaveMeridianStorm.AddComponent<SimulacrumLightningStormWave>();


            InteractableSpawnCard Geode = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC2/iscGeode.asset").WaitForCompletion();
            Geode.orientToFloor = false;
            InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().rewardPickupPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FragmentPotentialPickup.prefab").WaitForCompletion();
            InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 4;
            InfiniteTowerWaveMeridianStorm.GetComponent<InfiniteTowerWaveController>().baseCredits += 10;

            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PMSTORM";
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PMSTORM";
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.52f, 0.79f, 1f);
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/Base/ShockNearby/bdTeslaField.asset").WaitForCompletion().iconSprite;
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.52f, 0.79f, 1f);
            InfiniteTowerWaveMeridianStormUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.52f, 0.79f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITMeridianStorm = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMeridianStorm, weight = 2f, prerequisites = Constant.DLC2_StartWave15Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITMeridianStorm);
            #endregion

            #region Knockback Strong
            //KnockbackFin
            GameObject InfiniteTowerWaveKnockback = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveKnockback", true);
            GameObject InfiniteTowerWaveKnockbackUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveKnockbackUI", false);

            InfiniteTowerWaveKnockback.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveKnockbackUI;
            InfiniteTowerWaveKnockback.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryUtility;

            //InfiniteTowerWaveKnockback.AddComponent<SimulacrumGiveItemsOnStart>().itemString = "KnockBackHitEnemies";
            //InfiniteTowerWaveKnockback.GetComponent<SimulacrumGiveItemsOnStart>().count = 1;
            InfiniteTowerWaveKnockback.AddComponent<Simulacrum_KnockbackWave>();

            InfiniteTowerWaveKnockbackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_KNOCKBACKFIN";
            InfiniteTowerWaveKnockbackUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_KNOCKBACKFIN";
            InfiniteTowerWaveKnockbackUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(219, 122, 173, 255);
            InfiniteTowerWaveKnockbackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(216, 95, 148, 255);
            InfiniteTowerWaveKnockbackUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(216, 95, 148, 255);

            InfiniteTowerWaveCategory.WeightedWave ITKnockback = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveKnockback, weight = 3f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITKnockback);
            #endregion
        }



    }

}