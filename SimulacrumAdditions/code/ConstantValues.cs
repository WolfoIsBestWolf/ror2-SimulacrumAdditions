using R2API;
using RoR2;
using RoR2.ExpansionManagement;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WolfoFixes;

namespace SimulacrumAdditions
{
    public static class Constant
    {
        public static GameObject BasicWave = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion();
        public static GameObject BasicWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion();

        public static GameObject BossWave = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion();
        public static GameObject BossWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion();

        public static GameObject ScavWave = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion();
        public static GameObject ScavWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion();

        public static GameObject ArtifactWave = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion();
        public static GameObject ArtifactWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion();

        public static GameObject LunarWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion();
        public static GameObject VoidWaveUI = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion();

        public static Sprite texITWaveDefaultWhiteS;


        public static int SimuEndingStartAtXWaves;
        public static int SimuEndingEveryXWaves;
        public static int SimuEndingWaveRest;
        public static int SimuForcedBossStartAtXWaves;
        public static int SimuForcedBossEveryXWaves;
        public static int SimuForcedBossWaveRest;

        public static float BasicWaveWeight = 100;
        public static float BasicBossWaveWight = 80;
        public static float DefaultWeightMultiplier1 = 0.75f;
        public static float DefaultWeightMultiplier2 = 0.25f;

        //CommonWaveCategory
        //BossWaveCategory
        public static InfiniteTowerWaveCategory ITBasicWaves = Addressables.LoadAssetAsync<InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/CommonWaveCategory.asset").WaitForCompletion();
        public static InfiniteTowerWaveCategory ITBossWaves = Addressables.LoadAssetAsync<InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/BossWaveCategory.asset").WaitForCompletion();
        public static InfiniteTowerWaveCategory ITSuperBossWaves = ScriptableObject.CreateInstance<InfiniteTowerWaveCategory>();
        public static InfiniteTowerWaveCategory ITModSupportWaves = ScriptableObject.CreateInstance<InfiniteTowerWaveCategory>();
        //Would need to be the first in the Array to work normally

        public static GameEndingDef InfiniteTowerEnding = ScriptableObject.CreateInstance<GameEndingDef>();
        public static InteractableSpawnCard iscSimuExitPortal;
        public static GameObject VoidTeleportOutEffect = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/ExtraLifeVoid/VoidRezEffect.prefab").WaitForCompletion(), "VoidTeleportOutEffect", false);

        public static ItemTierDef ItemOrangeTierDef;
        //
        //Does this need to be in the Simu File 
        public static AdvancedPickupDropTable dtAISafeRandomVoid = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

        //Wave Prerequesites
        public static InfiniteTowerWaveCountPrerequisites AfterWave5Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave11Prerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveCountPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Wave11OrGreaterPrerequisite.asset").WaitForCompletion();
        public static InfiniteTowerWaveCountPrerequisites StartWave15Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave20Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave25Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave30Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave35Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave40Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();
        public static InfiniteTowerWaveCountPrerequisites StartWave50Prerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveCountPrerequisites>();

        public static ITWave_MaxWave_Prerequisites AfterWave5EndWave30Prerequisite = ScriptableObject.CreateInstance<ITWave_MaxWave_Prerequisites>();

        public static InfiniteTowerRun Simu_Run_Run;

        public static ITWave_DLC_Prerequisites DLC1_StartWave10Prerequisite = ScriptableObject.CreateInstance<ITWave_DLC_Prerequisites>();

        public static ITWave_DLC_Prerequisites DLC2_StartWave10Prerequisite = ScriptableObject.CreateInstance<ITWave_DLC_Prerequisites>();
        public static ITWave_DLC_Prerequisites DLC2_StartWave15Prerequisite = ScriptableObject.CreateInstance<ITWave_DLC_Prerequisites>();
        public static ITWave_DLC_Prerequisites DLC2_StartWave30Prerequisite = ScriptableObject.CreateInstance<ITWave_DLC_Prerequisites>();

        //Simu Wave Reward Drop Tables
        public static BasicPickupDropTable dtITWaveTier1 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITDefaultWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier2 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier3 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITSpecialBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITVoid = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITVoid.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITLunar = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITLunar.asset").WaitForCompletion();


        public static AdvancedPickupDropTable dtITFamilyWaveDamage = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITFamilyWaveHealing = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITFamilyWaveUtility = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

        public static AdvancedPickupDropTable dtITCategoryDamage = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITCategoryHealing = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITCategoryUtility = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

        public static AdvancedPickupDropTable dtITBossCategoryDamage = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBossCategoryHealing = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBossCategoryUtility = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();


        public static AdvancedPickupDropTable dtITBasicWaveOnKill = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBasicBonusLunar = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBasicBonusVoid = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

        public static AdvancedPickupDropTable dtITBasicBonusGreen = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBossBonusRed = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITBossGreenVoid = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

        public static AdvancedPickupDropTable dtITSpecialEquipment = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITVoidInfestorWave = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static AdvancedPickupDropTable dtITSpecialBossYellow = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
        public static ExplicitPickupDropTable dtITHeresy = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();
        public static ExplicitPickupDropTable dtITWurms = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();


        public static MasterCatalog.MasterIndex IndexAffixHealingCore = MasterCatalog.MasterIndex.none;

        /*public float BasicWeightCommon = 5;
        public float BasicWeightUncommon = 4;
        public float BasicWeightRare = 3;*/
        public static float BossWeightCommon = 10;
        public static float BossWeightUncommon = 6;
        public static float BossWeightRare = 4;



        public static void MakeValues()
        {
            WolfoFixes.Shared.SetupShared();
            SimuTPEffect();
            MakePortal();
            SetupConstants();
            EnemyDropTables();
            WaveDropPools();
            WavePrerequesites();
            ContentAddition.AddEntityState<PulseWaveState>(out _);
            texITWaveDefaultWhiteS = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveBasicWhite.png");

        }

        public static void EnemyDropTables()
        {
            //Tags
            ItemTag[] bannedTags = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier1Item.asset").WaitForCompletion().bannedItemTags;

            InfiniteTowerRun InfiniteTowerRunBase = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            AdvancedPickupDropTable dtITEnemyTier1 = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
            AdvancedPickupDropTable dtITEnemyTier2 = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
            AdvancedPickupDropTable dtITEnemyTier3 = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();
            dtAISafeRandomVoid = ScriptableObject.CreateInstance<AdvancedPickupDropTable>();

            dtITEnemyTier1.name = "dtITEnemyTier1";
            dtITEnemyTier2.name = "dtITEnemyTier2";
            dtITEnemyTier3.name = "dtITEnemyTier3";

            dtITEnemyTier1.tier1Weight = 1;
            dtITEnemyTier1.tier2Weight = 0;
            dtITEnemyTier1.tier3Weight = 0;
            dtITEnemyTier1.voidTier1Weight = 0.1f;
            dtITEnemyTier1.bannedItemTags = bannedTags;

            dtITEnemyTier2.tier1Weight = 0;
            dtITEnemyTier2.tier2Weight = 1;
            dtITEnemyTier2.tier3Weight = 0;
            dtITEnemyTier2.voidTier2Weight = 0.075f;
            dtITEnemyTier2.bannedItemTags = bannedTags;

            dtITEnemyTier3.tier1Weight = 0;
            dtITEnemyTier3.tier2Weight = 0;
            dtITEnemyTier3.tier3Weight = 1;
            dtITEnemyTier3.voidTier3Weight = 0.075f;
            dtITEnemyTier3.bannedItemTags = bannedTags;

            InfiniteTowerRunBase.enemyItemPattern[0].dropTable = dtITEnemyTier1;
            InfiniteTowerRunBase.enemyItemPattern[1].dropTable = dtITEnemyTier1;
            InfiniteTowerRunBase.enemyItemPattern[2].dropTable = dtITEnemyTier2;
            InfiniteTowerRunBase.enemyItemPattern[3].dropTable = dtITEnemyTier2;
            InfiniteTowerRunBase.enemyItemPattern[4].dropTable = dtITEnemyTier3;

            //Better Blacklist since I assume in Vanilla the Blacklist still sucks ass and gives On Kill items 
            //For Scavs
            dtAISafeRandomVoid.name = "dtAISafeRandomVoid";
            dtAISafeRandomVoid.tier1Weight = 0;
            dtAISafeRandomVoid.tier2Weight = 0;
            dtAISafeRandomVoid.tier3Weight = 0;
            dtAISafeRandomVoid.voidTier1Weight = 6;
            dtAISafeRandomVoid.voidTier2Weight = 3;
            dtAISafeRandomVoid.voidTier3Weight = 1;
            dtAISafeRandomVoid.voidBossWeight = 0.1f; //Friendly Void Reaver would just suck him up and kill him
            dtAISafeRandomVoid.canDropBeReplaced = false;
            dtAISafeRandomVoid.bannedItemTags = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "81e89866bab7f7f49ad46cd10f6d9ac8").WaitForCompletion().bannedItemTags;

        }

        public static void WaveDropPools()
        {
            //The Guaranteed Red
            dtITWaveTier3.tier3Weight = 90;
            dtITWaveTier3.bossWeight = 10;

            //Vanilla Void Boss Drop Table is kinda bad
            dtITVoid.voidTier1Weight = 60;
            dtITVoid.voidTier2Weight = 60;
            dtITVoid.voidTier3Weight = 15;
            dtITVoid.voidBossWeight = 10;




            //OnKill for On Kill Artifacts
            dtITBasicWaveOnKill.tier1Weight = 80f;
            dtITBasicWaveOnKill.tier2Weight = 9f;
            dtITBasicWaveOnKill.tier3Weight = 0.75f;
            dtITBasicWaveOnKill.bossWeight = 0.25f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";
            dtITBasicWaveOnKill.requiredItemTags = new ItemTag[] { ItemTag.OnKillEffect };

            //For Basic waves intended to be difficult
            dtITBasicBonusGreen.tier1Weight = 30f;
            dtITBasicBonusGreen.tier2Weight = 80f;
            dtITBasicBonusGreen.tier3Weight = 0f;
            dtITBasicBonusGreen.bossWeight = 0f;
            dtITBasicBonusGreen.name = "dtITBasicBonusGreen";

            //For Boss waves intended to be difficult
            dtITBossBonusRed.tier1Weight = 0f;
            dtITBossBonusRed.tier2Weight = 20f; //80 default
            dtITBossBonusRed.tier3Weight = 12f; //7.5 default
            dtITBossBonusRed.bossWeight = 0f; //7.5 default
            dtITBossBonusRed.name = "dtITBossBonusRed";

            dtITBossGreenVoid.name = "dtITBossGreenVoid";
            dtITBossGreenVoid.tier1Weight = 0;
            dtITBossGreenVoid.tier2Weight = 80;
            dtITBossGreenVoid.tier3Weight = 7.5f;
            dtITBossGreenVoid.bossWeight = 7.5f;
            dtITBossGreenVoid.voidTier1Weight = 20;
            dtITBossGreenVoid.voidTier2Weight = 80;
            dtITBossGreenVoid.voidTier3Weight = 10;
            dtITBossGreenVoid.voidBossWeight = 10;

            //For Basic Void Elite Wave
            dtITBasicBonusVoid.tier1Weight = 80f;
            dtITBasicBonusVoid.tier2Weight = 10f;
            dtITBasicBonusVoid.tier3Weight = 0.25f;
            dtITBasicBonusVoid.bossWeight = 0f;
            dtITBasicBonusVoid.voidTier1Weight = 80f;
            dtITBasicBonusVoid.voidTier2Weight = 25f;
            dtITBasicBonusVoid.voidTier3Weight = 5f;
            dtITBasicBonusVoid.name = "dtITBasicBonusVoid";

            //For Basic Lunar Elite Wave
            dtITBasicBonusLunar.tier1Weight = 80f;
            dtITBasicBonusLunar.tier2Weight = 10f;
            dtITBasicBonusLunar.tier3Weight = 1f;
            dtITBasicBonusLunar.bossWeight = 0;
            dtITBasicBonusLunar.lunarItemWeight = 59f;
            dtITBasicBonusLunar.lunarEquipmentWeight = 6f;
            dtITBasicBonusLunar.name = "dtITBasicBonusLunar";

            //Void Infestor Boss Wave
            dtITVoidInfestorWave.tier1Weight = 0;
            dtITVoidInfestorWave.tier2Weight = 0;
            dtITVoidInfestorWave.tier3Weight = 0;
            dtITVoidInfestorWave.bossWeight = 30;
            dtITVoidInfestorWave.voidTier1Weight = 60;
            dtITVoidInfestorWave.voidTier2Weight = 30;
            dtITVoidInfestorWave.voidTier3Weight = 8;
            dtITVoidInfestorWave.voidBossWeight = 10;
            dtITVoidInfestorWave.name = "dtITVoidInfestorWave";

            //Vengance & Honor Boss
            dtITSpecialBossYellow.tier1Weight = 0;
            dtITSpecialBossYellow.tier2Weight = 10;
            dtITSpecialBossYellow.tier3Weight = 10;
            dtITSpecialBossYellow.bossWeight = 100; //Because how weighted selections actually work, boss items will be a lot less common
            dtITSpecialBossYellow.name = "dtITSpecialBossYellow";
            //dtITSpecialBossYellow.eliteEquipWeight = 5f;
            //dtITSpecialBossYellow.pearlWeight = 70f;
            dtITSpecialBossYellow.voidBossWeight = 20;

            //Family Waves biased 
            dtITFamilyWaveDamage.tier1Weight = 80;
            dtITFamilyWaveDamage.tier2Weight = 8; //Move 4 Points to boss
            dtITFamilyWaveDamage.tier3Weight = 0.25f;
            dtITFamilyWaveDamage.bossWeight = 4f;
            dtITFamilyWaveDamage.name = "dtITFamilyWaveDamage";
            dtITFamilyWaveDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITFamilyWaveHealing.tier1Weight = 80;
            dtITFamilyWaveHealing.tier2Weight = 8;
            dtITFamilyWaveHealing.tier3Weight = 0.25f;
            dtITFamilyWaveHealing.bossWeight = 4f;
            dtITFamilyWaveHealing.name = "dtITFamilyWaveHealing";
            dtITFamilyWaveHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITFamilyWaveUtility.tier1Weight = 80;
            dtITFamilyWaveUtility.tier2Weight = 8;
            dtITFamilyWaveUtility.tier3Weight = 0.25f;
            dtITFamilyWaveUtility.bossWeight = 4f;
            dtITFamilyWaveUtility.name = "dtITFamilyWaveUtility";
            dtITFamilyWaveUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };
            //
            //Cateogry Waves biased 
            dtITCategoryDamage.tier1Weight = 80;
            dtITCategoryDamage.tier2Weight = 12;
            dtITCategoryDamage.tier3Weight = 0.5f;
            dtITCategoryDamage.bossWeight = 0.25f;
            dtITCategoryDamage.name = "dtITCategoryDamage";
            dtITCategoryDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITCategoryHealing.tier1Weight = 80;
            dtITCategoryHealing.tier2Weight = 12;
            dtITCategoryHealing.tier3Weight = 0.5f;
            dtITCategoryHealing.bossWeight = 0.25f;
            dtITCategoryHealing.name = "dtITCategoryHealing";
            dtITCategoryHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITCategoryUtility.tier1Weight = 80;
            dtITCategoryUtility.tier2Weight = 12;
            dtITCategoryUtility.tier3Weight = 0.5f;
            dtITCategoryUtility.bossWeight = 0.25f;
            dtITCategoryUtility.name = "dtITCategoryUtility";
            dtITCategoryUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };

            //Family Waves biased 
            dtITBossCategoryDamage.tier1Weight = 0;
            dtITBossCategoryDamage.tier2Weight = 20f;
            dtITBossCategoryDamage.tier3Weight = 10f;
            dtITBossCategoryDamage.bossWeight = 5f;
            dtITBossCategoryDamage.name = "dtITBossCategoryDamage";
            dtITBossCategoryDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITBossCategoryHealing.tier1Weight = 0;
            dtITBossCategoryHealing.tier2Weight = 20f;
            dtITBossCategoryHealing.tier3Weight = 10f;
            dtITBossCategoryHealing.bossWeight = 5f;
            dtITBossCategoryHealing.name = "dtITBossCategoryHealing";
            dtITBossCategoryHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITBossCategoryUtility.tier1Weight = 0;
            dtITBossCategoryUtility.tier2Weight = 20f;
            dtITBossCategoryUtility.tier3Weight = 10f;
            dtITBossCategoryUtility.bossWeight = 5f;
            dtITBossCategoryUtility.name = "dtITBossCategoryUtility";
            dtITBossCategoryUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };
            //

            //In addition to the regular green so keep it mostly Orange
            dtITSpecialEquipment.requiredItemTags = new ItemTag[] { ItemTag.EquipmentRelated };
            dtITSpecialEquipment.tier1Weight = 40f;
            dtITSpecialEquipment.tier2Weight = 40f;
            dtITSpecialEquipment.tier3Weight = 20f;
            dtITSpecialEquipment.bossWeight = 20f;
            dtITSpecialEquipment.lunarItemWeight = 40f;
            dtITSpecialEquipment.equipmentWeight = 170f;
            dtITSpecialEquipment.lunarEquipmentWeight = 30f;
            dtITSpecialEquipment.name = "dtITSpecialEquipment";
        }

        public static void WavePrerequesites()
        {
            AfterWave5Prerequisite.minimumWaveCount = 6;
            AfterWave5Prerequisite.name = "AfterWave5Prerequisite";
            StartWave15Prerequisite.minimumWaveCount = 15;
            StartWave15Prerequisite.name = "StartWave15Prerequisite";
            StartWave20Prerequisite.minimumWaveCount = 20;
            StartWave20Prerequisite.name = "StartWave20Prerequisite";
            StartWave25Prerequisite.minimumWaveCount = 25;
            StartWave25Prerequisite.name = "StartWave25Prerequisite";
            StartWave30Prerequisite.minimumWaveCount = 30;
            StartWave30Prerequisite.name = "StartWave30Prerequisite";
            StartWave35Prerequisite.minimumWaveCount = 35;
            StartWave35Prerequisite.name = "StartWave35Prerequisite";
            StartWave40Prerequisite.minimumWaveCount = 40;
            StartWave40Prerequisite.name = "StartWave40Prerequisite";
            StartWave50Prerequisite.minimumWaveCount = 50;
            StartWave50Prerequisite.name = "StartWave50Prerequisite";

            AfterWave5EndWave30Prerequisite.minimumWaveCount = 6;
            AfterWave5EndWave30Prerequisite.maximumWaveCount = 30;
            AfterWave5EndWave30Prerequisite.name = "AfterWave5EndWave30Prerequisite";

            DLC2_StartWave10Prerequisite.minimumWaveCount = 10;
            DLC2_StartWave10Prerequisite.name = "StartWave10PrerequisiteDLC2";
            DLC2_StartWave15Prerequisite.minimumWaveCount = 15;
            DLC2_StartWave15Prerequisite.name = "StartWave15PrerequisiteDLC2";
            DLC2_StartWave30Prerequisite.minimumWaveCount = 30;
            DLC2_StartWave30Prerequisite.name = "StartWave30PrerequisiteDLC2";

        }


        public static void SetupConstants()
        {
            ITSuperBossWaves.wavePrefabs = Array.Empty<InfiniteTowerWaveCategory.WeightedWave>();
            ITSuperBossWaves.name = "SuperBossWaveCategory";
            ITSuperBossWaves.availabilityPeriod = 30;
            ITSuperBossWaves.minWaveIndex = 50;
            ITModSupportWaves.wavePrefabs = Array.Empty<InfiniteTowerWaveCategory.WeightedWave>();
            ITModSupportWaves.name = "ITModSupportWaveCategory";
            ITModSupportWaves.availabilityPeriod = 999;
            ITModSupportWaves.minWaveIndex = 998;



            //Fake Orange Tier for Orange Void Potentials
            ItemOrangeTierDef = ScriptableObject.CreateInstance<ItemTierDef>();
            ItemTierDef Tier1 = Addressables.LoadAssetAsync<ItemTierDef>(key: "RoR2/Base/Common/Tier1Def.asset").WaitForCompletion();

            ItemOrangeTierDef.tier = ItemTier.AssignedAtRuntime;
            ItemOrangeTierDef.name = "OrangeTierDef";
            ItemOrangeTierDef.bgIconTexture = Tier1.bgIconTexture;
            ItemOrangeTierDef.colorIndex = ColorCatalog.ColorIndex.Equipment;
            ItemOrangeTierDef.darkColorIndex = ColorCatalog.ColorIndex.Equipment;
            ItemOrangeTierDef.isDroppable = false;
            ItemOrangeTierDef.canScrap = false;
            ItemOrangeTierDef.canRestack = false;
            ItemOrangeTierDef.highlightPrefab = Tier1.highlightPrefab;
            ItemOrangeTierDef.dropletDisplayPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Common/EquipmentOrb.prefab").WaitForCompletion();
            ItemOrangeTierDef.pickupRules = ItemTierDef.PickupRules.Default;
            ContentAddition.AddItemTierDef(ItemOrangeTierDef);

            //Config Vals
            SimuEndingEveryXWaves = WConfig.cfgSimuEndingEveryXWaves.Value;
            SimuEndingStartAtXWaves = WConfig.cfgSimuEndingStartAtXWaves.Value;
            SimuEndingWaveRest = WConfig.cfgSimuEndingStartAtXWaves.Value % WConfig.cfgSimuEndingEveryXWaves.Value;

            SimuForcedBossEveryXWaves = WConfig.cfgSuperBossEveryXWaves.Value;
            SimuForcedBossStartAtXWaves = WConfig.cfgSuperBossStartAtXWaves.Value;
            SimuForcedBossWaveRest = WConfig.cfgSuperBossStartAtXWaves.Value % WConfig.cfgSuperBossEveryXWaves.Value;


            //Simu Game Ending
            //GameEndingDef VoidEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/DLC1/GameModes/VoidEnding.asset").WaitForCompletion();
            GameEndingDef MainEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/Base/ClassicRun/MainEnding.asset").WaitForCompletion();
            GameEndingDef VoidEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/DLC1/GameModes/VoidEnding.asset").WaitForCompletion();
            GameEndingDef ObliterationEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/Base/ClassicRun/ObliterationEnding.asset").WaitForCompletion();
            Sprite VoidTransformSprite = Addressables.LoadAssetAsync<Sprite>(key: "RoR2/DLC1/UI/texVoidTransformationBackground.png").WaitForCompletion();

            ContentAddition.AddGameEndingDef(InfiniteTowerEnding);

            VoidEnding.icon = MainEnding.icon;
            VoidEnding.lunarCoinReward += 5;

            InfiniteTowerEnding.endingTextToken = "Simulation Suspended";
            InfiniteTowerEnding.lunarCoinReward = 10;
            InfiniteTowerEnding.showCredits = false;
            InfiniteTowerEnding.isWin = true;
            InfiniteTowerEnding.gameOverControllerState = ObliterationEnding.gameOverControllerState;
            InfiniteTowerEnding.material = MainEnding.material;
            InfiniteTowerEnding.icon = VoidTransformSprite;
            InfiniteTowerEnding.backgroundColor = new Color(0.65f, 0.3f, 0.55f, 0.8f);
            //InfiniteTowerEnding.foregroundColor = new Color(0.75f, 0.4f, 0.55f, 1);
            InfiniteTowerEnding.foregroundColor = new Color(0.85f, 0.5f, 0.65f, 1);
            InfiniteTowerEnding.cachedName = "InfiniteTowerEnding";


           
            DirectorCardCategorySelection dccsITVoidMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsITVoidMonsters.asset").WaitForCompletion();
            dccsITVoidMonsters.categories[1].selectionWeight = 2;
            dccsITVoidMonsters.categories[2].cards[0].selectionWeight = 2;
        }


        internal static void MakePortal()
        {
            InteractableSpawnCard iscVoidOutroPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidOutroPortal/iscVoidOutroPortal.asset").WaitForCompletion();
            InteractableSpawnCard iscVoidPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/PortalVoid/iscVoidPortal.asset").WaitForCompletion();

            iscSimuExitPortal = GameObject.Instantiate(iscVoidOutroPortal);
            iscSimuExitPortal.name = "iscSimuExitPortal";
            GameObject EndingPortal = PrefabAPI.InstantiateClone(iscSimuExitPortal.prefab, "SimulacrumExitPortal", true);
            iscSimuExitPortal.prefab = EndingPortal;

            EndingPortal.GetComponent<GenericDisplayNameProvider>().displayToken = "PORTAL_ITEND_NAME";
            EndingPortal.GetComponent<GenericInteraction>().contextToken = "PORTAL_ITEND_CONTEXT";
            if (EndingPortal.GetComponent<GenericObjectiveProvider>())
            {
                EndingPortal.GetComponent<GenericObjectiveProvider>().objectiveToken = "OBJECTIVE_ITEND";
            }
            else
            {
                EndingPortal.AddComponent<GenericObjectiveProvider>().objectiveToken = "OBJECTIVE_ITEND";
            }

            GameObject.Instantiate(iscVoidPortal.prefab.transform.GetChild(0).gameObject, EndingPortal.transform);
            //Guh 2??
            EndingPortal.transform.localScale = new Vector3(1.26f, 1.26f, 0.85f);
            EndingPortal.transform.GetChild(2).localPosition = new Vector3(0, 4f, 0);
            EndingPortal.transform.GetChild(2).GetChild(2).GetComponent<Light>().color = new Color(-8f, -16f, -8f);
            GameObject.Destroy(EndingPortal.transform.GetChild(2).GetChild(2).GetComponent<LightIntensityCurve>());
            //0.8302 0.6461 0.8237 1
            EndingPortal.transform.GetChild(2).GetChild(4).localScale *= 0.5f;
            EndingPortal.transform.GetChild(2).GetChild(4).GetComponent<EntityLocator>().entity = EndingPortal;
            EndingPortal.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
            EndingPortal.transform.GetChild(2).GetChild(6).gameObject.SetActive(false);
            EndingPortal.transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
            GameObject.Destroy(EndingPortal.transform.GetChild(1).gameObject);
            GameObject.Destroy(EndingPortal.transform.GetChild(0).gameObject);
        }


        public static void SimuTPEffect()
        {
            //Add glows to Option Pickups

            VoidTeleportOutEffect.transform.GetChild(9).gameObject.SetActive(false);
            GameObject.Destroy(VoidTeleportOutEffect.transform.GetChild(4).gameObject);
            GameObject.Destroy(VoidTeleportOutEffect.transform.GetChild(0).gameObject);
            VoidTeleportOutEffect.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            VoidTeleportOutEffect.GetComponent<EffectComponent>().soundName = "Play_UI_charTeleport";
            ContentAddition.AddEffect(VoidTeleportOutEffect);
            if (WConfig.cfgDifferentTeleportEffect.Value)
            {
                On.RoR2.Run.GetTeleportEffectPrefab += (orig, self, objectToTeleport) =>
                {
                    if (self is InfiniteTowerRun)
                    {
                        return VoidTeleportOutEffect;
                    }
                    return orig(self, objectToTeleport);
                };
            }
        }

        public static void Late_MakeValues()
        {
            IndexAffixHealingCore = MasterCatalog.FindMasterIndex("AffixEarthHealerMaster");

            dtITHeresy.name = "dtITHeresy";
            dtITHeresy.canDropBeReplaced = false;
            dtITHeresy.pickupEntries = new ExplicitPickupDropTable.PickupDefEntry[]
            {
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.LunarPrimaryReplacement, pickupWeight = 1.25f },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.LunarSecondaryReplacement, pickupWeight = 1.25f },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.LunarUtilityReplacement, pickupWeight = 1.25f },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.LunarSpecialReplacement, pickupWeight = 1.25f },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.Pearl, pickupWeight = 0.4f },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.ShinyPearl, pickupWeight = 0.1f}
            };

            dtITWurms.name = "dtITWurms";
            dtITWurms.canDropBeReplaced = false;
            dtITWurms.pickupEntries = new ExplicitPickupDropTable.PickupDefEntry[]
            {
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.FireballsOnHit, pickupWeight = 1 },
                new ExplicitPickupDropTable.PickupDefEntry(){pickupDef = RoR2Content.Items.LightningStrikeOnHit, pickupWeight = 1 },
            };
            //

            Simu_Run_Run = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            //This is where we'd need to add Fireworks
            //Fireworks is Interactable Related and that tagged is banned
            HG.ArrayUtils.ArrayAppend(ref Simu_Run_Run.blacklistedItems, RoR2Content.Items.Squid);
            Simu_Run_Run.blacklistedItems = Simu_Run_Run.blacklistedItems.Remove(DLC1Content.Items.DroneWeapons); //But Squid Polyp wouldn't work they just die
            Simu_Run_Run.blacklistedTags = Simu_Run_Run.blacklistedTags.Remove(ItemTag.InteractableRelated); //There's only two and Fireworks works plenty

            ItemDef tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("VV_ITEM_CORNUCOPIACELL_ITEM"));
            if (tempDef != null)
            {
                HG.ArrayUtils.ArrayAppend(ref Simu_Run_Run.blacklistedItems, tempDef);
            }
            //There are no teleporters in Simu
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("FieldAccelerator"));
            if (tempDef != null)
            {
                HG.ArrayUtils.ArrayAppend(ref Simu_Run_Run.blacklistedItems, tempDef);
            }

            if (WConfig.cfgItemsEvery8.Value)
            {
                Simu_Run_Run.enemyItemPeriod = 8;
            }



        }
    }



    public class ITWave_DLC_Prerequisites : InfiniteTowerWavePrerequisites
    {
        public override bool AreMet(InfiniteTowerRun run)
        {
            if (run.IsExpansionEnabled(requiredDLC))
            {
                return run.waveIndex >= this.minimumWaveCount;
            }
            return false;
        }

        public static ExpansionDef requiredDLC = Addressables.LoadAssetAsync<ExpansionDef>(key: "RoR2/DLC2/Common/DLC2.asset").WaitForCompletion();

        public int minimumWaveCount;
    }

    public class ITWave_MaxWave_Prerequisites : InfiniteTowerWavePrerequisites
    {
        public override bool AreMet(InfiniteTowerRun run)
        {
            if (run.waveIndex >= maximumWaveCount)
            {
                return false;
            }
            return run.waveIndex >= this.minimumWaveCount;
        }

        public int maximumWaveCount;
        public int minimumWaveCount;
    }
}