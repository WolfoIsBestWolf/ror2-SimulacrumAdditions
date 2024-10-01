using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_SpecialGuy
    {
        //public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;
        public static CardRandomizer CardRandomizerEquipmentDrones;

        public static MultiCSC CardRandomizerBasicGhost;
        public static MultiCSC CardRandomizerBossGhost;

        internal static void MakeWaves()
        {
            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef BoostAttackSpeed = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BoostAttackSpeed");
            ItemDef AlienHead = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AlienHead");
            ItemDef AdaptiveArmor = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AdaptiveArmor");
            ItemDef CutHP = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHP");
            ItemDef UseAmbientLevel = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel");

            ItemDef CritGlassesVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CritGlassesVoid");
            ItemDef ExtraLifeVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ExtraLifeVoid");

            //ItemDef RedWhip = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SprintOutOfCombat");
            //ItemDef Hoof = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Hoof");
            //ItemDef LunarBadLuck = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/LunarBadLuck");
            //ItemDef AutoCastEquipment = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AutoCastEquipment");
            //ItemDef SecondarySkillMagazine = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SecondarySkillMagazine");





            #region Basic Equipment Drone x1
            //Equipment Drone Basic
            GameObject InfiniteTowerWaveBasicEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDrone", true);
            GameObject InfiniteTowerWaveBasicEquipmentDroneUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDroneUI", false);

            CardRandomizerEquipmentDrones = InfiniteTowerWaveBasicEquipmentDrone.AddComponent<CardRandomizer>();

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = null;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Standard;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.12f;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 75;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;

            //InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBasicEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 75;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            SimuExplicitStats simuExplicitStats = InfiniteTowerWaveBasicEquipmentDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBasicEquipmentDroneUI;
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Equipment Drone";
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A Equipment Drone has been released from the focus.";

            Texture2D texItEquipmentBasic = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texItEquipmentBasic.LoadImage(Properties.Resources.texItEquipmentBasic, true);
            texItEquipmentBasic.filterMode = FilterMode.Bilinear;
            Sprite texItEquipmentBasicS = Sprite.Create(texItEquipmentBasic, WRect.rec64, WRect.half);

            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicEquipmentDrone, weight = 14f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicEquipmentDrone);
            #endregion

            #region (Boss) Equipment Drone x2
            //Equipment Drone Boss        
            GameObject InfiniteTowerWaveBossEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEquipmentDrone", true);
            GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 2, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far }
            };

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveBossEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;

            simuExplicitStats = InfiniteTowerWaveBossEquipmentDrone.AddComponent<SimuExplicitStats>();
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossEquipmentDroneWaveUI;

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Equipment Drones";
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Equipment Drones has been released from the focus.";

            Texture2D texITWaveBossIconEquipment = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveBossIconEquipment.LoadImage(Properties.Resources.texITEquipmentBoss, true);
            texITWaveBossIconEquipment.filterMode = FilterMode.Bilinear;
            Sprite texITWaveBossIconEquipmentS = Sprite.Create(texITWaveBossIconEquipment, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconEquipmentS;
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEquipmentDrone, weight = 10f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEquipmentDrone);
            //
            //On.EntityStates.Drone.DeathState.OnImpactServer += LeaveNoEquipmentDroneIT;

            #endregion

            #region Basic Ghost
            //Ghost Haunting Basic
            GameObject InfiniteTowerWaveBasicGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHaunting", true);
            GameObject InfiniteTowerWaveBasicGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHauntingUI", false);

            CardRandomizerBasicGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBasicGhost.name = "MulticscITGhost";
            CardRandomizerBasicGhost.sendOverNetwork = true;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = CardRandomizerBasicGhost;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBasicGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            simuExplicitStats = InfiniteTowerWaveBasicGhostHaunting.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 0;
            simuExplicitStats.damageBonusMulti = 0.8f;
            simuExplicitStats.ExtraSpawnAfterWave = 30;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBasicGhostHauntingUI;
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Haunting";
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A unknown horror follows you.";

            Texture2D texITHaunted = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITHaunted.LoadImage(Properties.Resources.texITHaunted, true);
            texITHaunted.filterMode = FilterMode.Bilinear;
            Sprite texITHauntedS = Sprite.Create(texITHaunted, WRect.rec64, WRect.half);

            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITHauntedS;
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicGhostHaunting, weight = 10f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicGhostHaunting);
            #endregion

            #region (Boss) Ghost Boss
            //Ghost Haunting Boss
            GameObject InfiniteTowerWaveBossGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHaunting", true);
            GameObject InfiniteTowerWaveBossGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHauntingUI", false);

            CardRandomizerBossGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBossGhost.name = "MulticscITGhostBoss";
            CardRandomizerBossGhost.sendOverNetwork = true;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                spawnCard = CardRandomizerBossGhost,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far,
                count = 1
            };

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBossGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 160;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;

            simuExplicitStats = InfiniteTowerWaveBossGhostHaunting.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.4f;
            simuExplicitStats.ExtraSpawnAfterWave = 54;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossGhostHauntingUI;
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Haunting";
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A strong unknown horror follows you.";

            Texture2D texITHauntedBoss = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITHauntedBoss.LoadImage(Properties.Resources.texITHauntedBoss, true);
            texITHauntedBoss.filterMode = FilterMode.Bilinear;
            Sprite texITHauntedBossS = Sprite.Create(texITHauntedBoss, WRect.rec64, WRect.half);

            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITHauntedBossS;
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossGhostHaunting, weight = 15f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossGhostHaunting);
            #endregion

            #region Brother Haunt / Moon Explode
            //BrotherHaunt
            GameObject InfiniteTowerWaveBrotherHaunt = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHaunt", true);
            GameObject InfiniteTowerWaveBrotherHauntUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHauntUI", false);

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
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
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 55 },
                new ItemCountPair { itemDef = Ghost, count = 1 }
            };
            cscBrotherHauntIT.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            //Multiple of these haunters would be too chaotic to dodge and having them just one shot you for this wave is probably better
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscBrotherHauntIT;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Close;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBrotherHauntUI;
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Detonation";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The planet turns unstable.";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 1f, 1f);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.9f, 1);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.6f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITBrotherHaunt = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBrotherHaunt, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBrotherHaunt);
            #endregion


            #region Acrid Character Fight
            //Acrid Void Boss
            GameObject InfiniteTowerWaveBossCharacters = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossCharacters", true);
            GameObject InfiniteTowerWaveBossCharactersUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossCharactersUI", false);

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
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 70 },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = 820 }, //10x hp
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CloverVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ElementalRingVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/EquipmentMagazineVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BearVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/MushroomVoid"), count = 1 },
                new ItemCountPair { itemDef = CritGlassesVoid, count = 1 },
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

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscITCharacter, count = 0, spawnDistance = DirectorCore.MonsterSpawnDistance.Far},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscNullifierIT, count = 2},
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossCharacters.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;

            InfiniteTowerWaveBossCharacters.AddComponent<SimuWaveSizeModifier>().sizeModifier = 2f;
            InfiniteTowerWaveBossCharacters.GetComponent<SimuWaveSizeModifier>().neededItem = AdaptiveArmor;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossCharactersUI;
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Cell Breach";
            //InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "This is a simulation everything is under control."; //"This is a simulation everything is under control."
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The subject is simulated, everything is under control."; //"This is a simulation everything is under control."

            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.6f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.4f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossCharacters = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossCharacters, weight = 7f, prerequisites = SimuMain.AfterWave5Prerequisite }; //This is a very basic wave modifier with a slight extra reward idk how common
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossCharacters);
            #endregion 

            #region Celestial thing
            //InvisibleDude
            GameObject InfiniteTowerWaveInvisibleDude = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveInvisibleDude", true);
            GameObject InfiniteTowerWaveInvisibleDudeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveInvisibleDudeUI", false);

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
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
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 5 }
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
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Invisibility";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are invisible inside the sphere.";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITInvisibleDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveInvisibleDude, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITInvisibleDude);
            #endregion
            #region Vagrant Nova forever
            //VagrantNovaDude
            GameObject InfiniteTowerWaveVagrantNovaDude = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveVagrantNovaDude", true);
            GameObject InfiniteTowerWaveVagrantNovaDudeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveVagrantNovaDudeUI", false);

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.10f;
            InfiniteTowerWaveVagrantNovaDude.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscVagrantNovaDudeIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscVagrantNovaDudeIT.name = "cscITSuperNovaGip";
            cscVagrantNovaDudeIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 78 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 80 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 25 },
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
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Nova";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A nova occurs periodically.";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 4f, prerequisites = SimuMain.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 44444 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVagrantNovaDude);
            //
            #endregion
            #region Just spawn in a fukin artifact ball who cares
            //ArtiifactReliquary
            GameObject InfiniteTowerWaveArtiifactReliquary = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactReliquary", true);
            GameObject InfiniteTowerWaveArtiifactReliquaryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactReliquaryUI", false);

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryDamage;
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
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 45 },
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
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Reliquary";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The reliquary rains down shots.";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 1f, prerequisites = SimuMain.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 44444 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITArtiifactReliquary);
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
            //
            //
            //
            #region
            //Super Gilded Halcyonite
            GameObject InfiniteTowerWaveHalcyonite = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveHalcyoniteBoss", true);
            GameObject InfiniteTowerWaveHalcyoniteUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveHalcyoniteBossUI", false);

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveHalcyonite.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

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

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHalcyoniteUI;
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Halcyonite";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat Halcyonite in addition to the wave.";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.3f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.9f, 0.55f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.8f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITHalcyonite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHalcyonite, weight = 5f, prerequisites = SimuMain.StartWave20PrerequisiteDLC2 };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITHalcyonite);
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