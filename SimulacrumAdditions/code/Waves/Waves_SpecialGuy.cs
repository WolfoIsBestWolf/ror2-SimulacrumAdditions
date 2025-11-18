using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using static SimulacrumAdditions.H;
using static SimulacrumAdditions.Constant;

namespace SimulacrumAdditions.Waves
{
    public class Waves_SpecialGuy
    {
        //public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;
        public static CardRandomizer CardRandomizerEquipmentDrones;

        public static MultiCSC CardRandomizerBasicGhost;
        public static MultiCSC CardRandomizerBossGhost;

        internal static void MakeWaves()
        {
            InfiniteTowerExplicitSpawnWaveController wave;
            SimulacrumExtrasHelper helper;

            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef BoostAttackSpeed = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BoostAttackSpeed");
            ItemDef AlienHead = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AlienHead");
            ItemDef AdaptiveArmor = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AdaptiveArmor");
            ItemDef CutHP = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHP");
            ItemDef UseAmbientLevel = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel");

            ItemDef BossDamageBonus = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BossDamageBonus");
            ItemDef CritGlassesVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CritGlassesVoid");
            ItemDef ExtraLifeVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ExtraLifeVoid");

            //ItemDef RedWhip = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SprintOutOfCombat");
            //ItemDef Hoof = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Hoof");
            //ItemDef LunarBadLuck = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/LunarBadLuck");
            //ItemDef AutoCastEquipment = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AutoCastEquipment");
            //ItemDef SecondarySkillMagazine = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SecondarySkillMagazine");



            #region Basic Equipment Drone x1
 
            //Equipment Drone Basic
            GameObject WaveBasic_EquipmentDrone = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBasic_EquipmentDrone", true);
            GameObject WaveBasic_EquipmentDroneUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBasic_EquipmentDroneUI", false);
            WavesMain.orangeWaves.Add(WaveBasic_EquipmentDrone);

            CardRandomizerEquipmentDrones = WaveBasic_EquipmentDrone.AddComponent<CardRandomizer>();
            
            wave = WaveBasic_EquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            wave.spawnList[0].spawnCard = null;
            wave.spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Standard;
            wave.immediateCreditsFraction = 0.12f;
            wave.baseCredits = 160;
            wave.linearCreditsPerWave = 0;
            wave.secondsBeforeSuddenDeath = 75;
            wave.wavePeriodSeconds = 60;
            wave.isBossWave = false;
            wave.rewardDropTable = Constant.dtITWaveTier1;
            wave.rewardDisplayTier = ItemTier.Tier1;
       
            helper = WaveBasic_EquipmentDrone.AddComponent<SimulacrumExtrasHelper>();
            helper.rewardDropTable = Constant.dtITSpecialEquipment;
            helper.rewardOptionCount = 2;
            helper.newRadius = 75;

            WaveBasic_EquipmentDrone.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.EquipmentDroneWave;

            SimuExplicitStats simuExplicitStats = WaveBasic_EquipmentDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            WaveBasic_EquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBasic_EquipmentDroneUI;
            WaveBasic_EquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_EQUIPMENTDRONE";
            WaveBasic_EquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_EQUIPMENTDRONE";

            WaveBasic_EquipmentDroneUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveEquipment.png");
            WaveBasic_EquipmentDroneUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            WaveBasic_EquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBasic_EquipmentDrone, weight = 14f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicEquipmentDrone);
            #endregion

            #region (Boss) Equipment Drone x2
            //Equipment Drone Boss        
            GameObject WaveBoss_EquipmentDrone = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_EquipmentDrone", true);
            GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);

            WaveBoss_EquipmentDrone.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.EquipmentDroneWave;

            wave = WaveBoss_EquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>();

            wave.spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 2, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far }
            };

            wave.baseCredits = 500;
            wave.linearCreditsPerWave = 0;
            wave.immediateCreditsFraction = 0.2f;
            wave.secondsBeforeSuddenDeath = 120f;
            wave.wavePeriodSeconds = 60;
            wave.rewardDisplayTier = ItemTier.Tier2;
            wave.rewardDropTable = Constant.dtITWaveTier2;

            WaveBoss_EquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITSpecialEquipment;
            //WaveBoss_EquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange
            WaveBoss_EquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;
 
            simuExplicitStats = WaveBoss_EquipmentDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            wave.overlayEntries[1].prefab = InfiniteTowerCurrentBossEquipmentDroneWaveUI;

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_EQUIPMENTDRONE";
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_EQUIPMENTDRONE";

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveEquipmentBoss.png");
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_EquipmentDrone, weight = 10f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossEquipmentDrone);

            #endregion

            #region Basic Ghost
            //Ghost Haunting Basic
            GameObject WaveBasic_GhostHaunting = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBasic_GhostHaunting", true);
            GameObject WaveBasic_GhostHauntingUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBasic_GhostHauntingUI", false);

            CardRandomizerBasicGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBasicGhost.name = "MulticscITGhost";
            CardRandomizerBasicGhost.sendOverNetwork = true;

            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = CardRandomizerBasicGhost;

            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            WaveBasic_GhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusGreen;
            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            simuExplicitStats = WaveBasic_GhostHaunting.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 0;
            simuExplicitStats.damageBonusMulti = 0.8f;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            WaveBasic_GhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBasic_GhostHauntingUI;
            WaveBasic_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_GHOST";
            WaveBasic_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_GHOST";

            WaveBasic_GhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGhost.png");
            WaveBasic_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            WaveBasic_GhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBasic_GhostHaunting, weight = 10f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicGhostHaunting);
            #endregion

            #region (Boss) Ghost Boss
            //Ghost Haunting Boss
            GameObject WaveBoss_GhostHaunting = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_GhostHaunting", true);
            GameObject WaveBoss_GhostHauntingUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_GhostHauntingUI", false);

            CardRandomizerBossGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBossGhost.name = "MulticscITGhostBoss";
            CardRandomizerBossGhost.sendOverNetwork = true;

            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                spawnCard = CardRandomizerBossGhost,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far,
                count = 1
            };

            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0;
            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            WaveBoss_GhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 160;

            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITSpecialBossYellow;

            simuExplicitStats = WaveBoss_GhostHaunting.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.4f;
            simuExplicitStats.ExtraSpawnAfterWave = 54;

            WaveBoss_GhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_GhostHauntingUI;
            WaveBoss_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_GHOST";
            WaveBoss_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_GHOST";

            WaveBoss_GhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGhostBoss.png");
            WaveBoss_GhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            WaveBoss_GhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_GhostHaunting, weight = 15f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossGhostHaunting);
            #endregion

            #region Brother Haunt / Moon Explode
            //BrotherHaunt
            GameObject InfiniteTowerWaveBrotherHaunt = PrefabAPI.InstantiateClone(Constant.ScavWave, "InfiniteTowerWaveBrotherHaunt", true);
            GameObject InfiniteTowerWaveBrotherHauntUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "InfiniteTowerWaveBrotherHauntUI", false);

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusLunar;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveBrotherHaunt.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            simuExplicitStats = InfiniteTowerWaveBrotherHaunt.AddComponent<SimuExplicitStats>();
            simuExplicitStats.spawnAsVoidTeam = true;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscBrotherHauntIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother"));
            cscBrotherHauntIT.name = "cscBrotherHauntIT";
            cscBrotherHauntIT.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/BrotherHauntMaster");
            cscBrotherHauntIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 55 },
                new ItemCountPair { itemDef = Ghost, count = 1 }
            };
            cscBrotherHauntIT.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            //Multiple of these haunters would be too chaotic to dodge and having them just one shot you for this wave is probably better
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscBrotherHauntIT;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Close;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBrotherHauntUI;
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BROTHERHAUNT";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BROTHERHAUNT";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 1f, 1f);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.9f, 1);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.6f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITBrotherHaunt = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBrotherHaunt, weight = 5f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBrotherHaunt);
            #endregion


            #region Acrid Character Fight
            //Acrid Void Boss
            GameObject WaveBoss_Characters = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_Characters", true);
            GameObject WaveBoss_CharactersUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "WaveBoss_CharactersUI", false);

            WaveBoss_Characters.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.AcridWave;

            MultiCharacterSpawnCard cscITCharacter = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITCharacter.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/MageMonsterMaster"),
                LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/CrocoMonsterMaster"),
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorMonsterMaster.prefab").WaitForCompletion(),
            };
            cscITCharacter.name = "cscITCharacter";
            cscITCharacter.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ExtraLifeVoid, count = 0 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 70 },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 820 }, //10x hp
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CloverVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ElementalRingVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/EquipmentMagazineVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BearVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/MushroomVoid"), count = 1 },
                new ItemCountPair { itemDef = CritGlassesVoid, count = 1 },
                new ItemCountPair { itemDef = BossDamageBonus, count = 15 },
                //new ItemCountPair { itemDef = AlienHead, count = 1 },
                new ItemCountPair { itemDef = AdaptiveArmor, count = 1 },
                new ItemCountPair { itemDef = UseAmbientLevel, count = 1 },
            };

            CharacterSpawnCard cscNullifierIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscNullifierIT.name = "cscNullifierIT";
            cscNullifierIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = BoostAttackSpeed, count = 2 },
                new ItemCountPair { itemDef = CutHP, count = 2 },
            };

            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscITCharacter, count = 0, spawnDistance = DirectorCore.MonsterSpawnDistance.Far},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscNullifierIT, count = 2},
            };

            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 450;
            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            WaveBoss_Characters.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;
            WaveBoss_Characters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;

            WaveBoss_Characters.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_Characters.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossGreenVoid;

            WaveBoss_Characters.AddComponent<SimuWaveSizeModifier>().sizeModifier = 2f;
            WaveBoss_Characters.GetComponent<SimuWaveSizeModifier>().neededItem = AdaptiveArmor;

            WaveBoss_Characters.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_CharactersUI;
            WaveBoss_CharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_ACRID";
            //WaveBoss_CharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "This is a simulation everything is under control."; //"This is a simulation everything is under control."
            WaveBoss_CharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_ACRID"; //"This is a simulation everything is under control."

            WaveBoss_CharactersUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            WaveBoss_CharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.6f, 1f, 1);
            WaveBoss_CharactersUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.4f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossCharacters = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_Characters, weight = 4f, prerequisites = Constant.AfterWave5Prerequisite }; //This is a very basic wave modifier with a slight extra reward idk how common
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossCharacters);
            #endregion 

            #region Celestial thing
            //InvisibleDude
            GameObject InfiniteTowerWaveInvisibleDude = PrefabAPI.InstantiateClone(Constant.ScavWave, "InfiniteTowerWaveInvisibleDude", true);
            GameObject InfiniteTowerWaveInvisibleDudeUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveInvisibleDudeUI", false);

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveInvisibleDude.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscInvisibleDudeIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscInvisibleDudeIT.name = "cscITInvisibleGip";
            cscInvisibleDudeIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 56 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 5 }
                //new ItemCountPair { itemDef = ItemHelpers.ITDisableAllSkills, count = 1},
            };
            cscInvisibleDudeIT.equipmentToGrant = new EquipmentDef[]
            {
                LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/AffixHaunted")
            };

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscInvisibleDudeIT;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            simuExplicitStats = InfiniteTowerWaveInvisibleDude.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = -1;
            simuExplicitStats.ExtraSpawnAfterWave = 30;


            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveInvisibleDudeUI;
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_CELESTIAL";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_CELESTIAL";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = LegacyResourcesAPI.Load<Sprite>("Textures/BuffIcons/texBuffAffixHaunted");
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITInvisibleDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveInvisibleDude, weight = 4f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITInvisibleDude);
            #endregion
            #region Vagrant Nova forever
            //VagrantNovaDude
            GameObject InfiniteTowerWaveVagrantNovaDude = PrefabAPI.InstantiateClone(Constant.ScavWave, "InfiniteTowerWaveVagrantNovaDude", true);
            GameObject InfiniteTowerWaveVagrantNovaDudeUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveVagrantNovaDudeUI", false);

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusGreen;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.10f;
            InfiniteTowerWaveVagrantNovaDude.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscVagrantNovaDudeIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscVagrantNovaDudeIT.name = "cscITSuperNovaGip";
            cscVagrantNovaDudeIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 1},
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 80 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 25 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/NovaOnLowHealth"), count = 2 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/PersonalShield"), count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDisableAllSkills, count = 1},
            };

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                count = 1,
                spawnCard = cscVagrantNovaDudeIT,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close,
            };
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            simuExplicitStats = InfiniteTowerWaveVagrantNovaDude.AddComponent<SimuExplicitStats>();
            simuExplicitStats.spawnAsVoidTeam = true;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveVagrantNovaDudeUI;
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_VAGRANTNOVA";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_VAGRANTNOVA";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 4f, prerequisites = Constant.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 44444 };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITVagrantNovaDude);
            //
            #endregion
            #region Just spawn in a fukin artifact ball who cares
            //ArtiifactReliquary
            GameObject InfiniteTowerWaveArtiifactReliquary = PrefabAPI.InstantiateClone(Constant.ScavWave, "InfiniteTowerWaveArtifactReliquary", true);
            GameObject InfiniteTowerWaveArtiifactReliquaryUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveArtifactReliquaryUI", false);

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryDamage;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<CombatDirector>().teamIndex = TeamIndex.Void;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            CharacterSpawnCard cscITArtifactBall = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITArtifactBall.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ArtifactShell/ArtifactShellMaster.prefab").WaitForCompletion();
            cscITArtifactBall.name = "cscITArtifactBall";
            cscITArtifactBall.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 54 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 45 },
            };
            cscITArtifactBall.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                count = 1,
                spawnCard = cscITArtifactBall,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close,
            };
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            simuExplicitStats = InfiniteTowerWaveArtiifactReliquary.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = -1;

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtiifactReliquaryUI;
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_ARTIFACTBALL";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ARTIFACTBALL";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 1f, prerequisites = Constant.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 44444 };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITArtiifactReliquary);
            #endregion
            On.EntityStates.VagrantNovaItem.ReadyState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.attachedBody.inventory.GetItemCount(ItemHelpers.ITKillOnCompletion) == 78)
                {
                    self.attachedHealthComponent.Networkhealth = 10;
                }
            };
            //
            #region Gilded Halcyonite
            //Super Gilded Halcyonite
            GameObject InfiniteTowerWaveHalcyonite = PrefabAPI.InstantiateClone(Constant.ScavWave, "InfiniteTowerWaveHalcyoniteBoss", true);
            GameObject InfiniteTowerWaveHalcyoniteUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerWaveHalcyoniteBossUI", false);

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 4;
            InfiniteTowerWaveHalcyonite.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveHalcyonite.AddComponent<SimuExplicitStats>().halfOnNonFinal = false;
            InfiniteTowerWaveHalcyonite.GetComponent<SimuExplicitStats>().hpBonusMulti = 2f;
            InfiniteTowerWaveHalcyonite.GetComponent<SimuExplicitStats>().damageBonusMulti = 1f;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscHalcyoniteIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion());
            cscHalcyoniteIT.name = "cscHalcyoniteIT";
            cscHalcyoniteIT.itemsToGrant = new ItemCountPair[] {
            };
            cscHalcyoniteIT.equipmentToGrant = new EquipmentDef[]
            {
                Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC2/Elites/EliteAurelionite/EliteAurelioniteEquipment.asset").WaitForCompletion(),
            };

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscHalcyoniteIT;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].eliteDef = Addressables.LoadAssetAsync<EliteDef>(key: "RoR2/DLC2/Elites/EliteAurelionite/edAurelionite.asset").WaitForCompletion();
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = true;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardPickupPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FragmentPotentialPickup.prefab").WaitForCompletion();

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHalcyoniteUI;
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_HALCYONITE";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_HALCYONITE";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.3f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.9f, 0.55f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.8f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITHalcyonite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHalcyonite, weight = 5f, prerequisites = Constant.DLC2_StartWave15Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITHalcyonite);
            #endregion
        }

        private static void LeaveNoEquipmentDroneIT(On.EntityStates.Drone.DeathState.orig_OnImpactServer orig, EntityStates.Drone.DeathState self, Vector3 contactPoint)
        {
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                if (self.characterBody.name.StartsWith("Equipment"))
                {
                    orig(self, contactPoint);
                }
                return;
            }
            orig(self, contactPoint);
        }
    }

}