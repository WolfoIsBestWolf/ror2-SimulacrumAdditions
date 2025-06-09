using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class WavesMain
    {
        public static List<GameObject> orangeWaves = new List<GameObject>();

        public static void Start()
        {
            Waves_Vanilla.MakeChanges();

            Waves_Artifacts.MakeWaves();
            Waves_BuffRelated.MakeWaves();
            Waves_ConstantlySpawning.MakeWaves();
            Waves_Elite.MakeWaves();
            Waves_EquipmentGiving.MakeWaves();
            Waves_Family.MakeWaves();
            Waves_ItemGiving.MakeWaves();
            Waves_InfiniteOnDeath.MakeWaves();
            Waves_Pulse.MakeWaves();
            Waves_SizeChanging.MakeWaves();
            Waves_SpecialGuy.MakeWaves();
            Waves_SuperBoss.MakeWaves();
            Waves_Misc.MakeWaves();

            Waves_ModSupport.MakeWaves();

        }


        internal static void LateChanges()
        {
            MakeMultiCSCs.CreateEquipmentDroneSpawnCards();
            MakeMultiCSCs.CreateGhostSpawnCards();
            MakeMultiCSCs.CreateBossGhostSpawnCards();
            MakeMultiCSCs.CreateDroneSpawnCards();
            //Mod Support
            /*CharacterSpawnCard cscDireseeker = null;
            CharacterSpawnCard[] CSCList = Object.FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
                    case "cscDireseeker":
                        cscDireseeker = CSCList[i];
                        break;
                }
            }*/

            for (int i = 0; i < Const.ITModSupportWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Const.ITModSupportWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();

                switch (wave.name)
                {
                     case "InfiniteTowerWaveArtifactHonorAndBrigade":
                        ArtifactDef SingleEliteType = ArtifactCatalog.FindArtifactDef("SingleEliteType");
                        if (SingleEliteType)
                        {
                            wave.AddComponent<ArtifactEnabler>().artifactDef = SingleEliteType;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SingleEliteType.smallIconSelectedSprite;
                            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(Const.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    case "InfiniteTowerWaveSS2RainbowElites":
                        EquipmentDef rainbowEliteEquip = EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex("AffixEmpyrean"));
                        if (rainbowEliteEquip)
                        {
                            ItemDef SS2_ITEM_BOOSTMOVESPEED_NAME = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("SS2_ITEM_BOOSTMOVESPEED_NAME"));
                            ItemDef SS2_ITEM_BOOSTCOOLDOWNS_NAME = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("SS2_ITEM_BOOSTCOOLDOWNS_NAME"));

                            CharacterSpawnCard cscEmpyreanIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion());
                            cscEmpyreanIT.name = "cscEmpyreanIT";
                            cscEmpyreanIT.noElites = true;
                            cscEmpyreanIT.itemsToGrant = new ItemCountPair[] {
                            new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = 3950}, //1000
                            new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = 10}, //
                            new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = 40}, //80
                            new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1},
                            new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1},
                            new ItemCountPair { itemDef = SS2_ITEM_BOOSTMOVESPEED_NAME, count = 50}, //80
                            new ItemCountPair { itemDef = SS2_ITEM_BOOSTCOOLDOWNS_NAME, count = 100}, //80
                            };
                            cscEmpyreanIT.equipmentToGrant = new EquipmentDef[]
                            {
                            rainbowEliteEquip
                            };
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscEmpyreanIT;
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
                            controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = rainbowEliteEquip.passiveBuffDef.iconSprite;

                            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(Const.ITModSupportWaves.wavePrefabs[i]);
                        }                       
                        break;
                    case "InfiniteTowerWaveArtifactSS2Cognation":
                        ArtifactDef SS2Cognation = ArtifactCatalog.FindArtifactDef("Cognation");
                        if (SS2Cognation)
                        {
                            wave.GetComponent<ArtifactEnabler>().artifactDef = SS2Cognation;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
 
                            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(Const.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    /*case "WaveBoss_Direseeker":
                        if (cscDireseeker)
                        {
                            CharacterSpawnCard cscDireseekerIT = Object.Instantiate(cscDireseeker);
                            cscDireseekerIT.name = "cscDireseekerIT";
                            cscDireseekerIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscDireseekerIT;
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
                             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.AddTW(Const.ITModSupportWaves.wavePrefabs[i]);
                        }                
                        break;*/
                }

            }


            
            GameObject defaultUI = Const.ITBasicWaves.wavePrefabs[0].wavePrefab.GetComponent<InfiniteTowerWaveController>().uiPrefab;
            Color newArticfact = new Color(1f, 0.7647f, 1.2647f, 1);
            for (int i = 0; i < Const.ITBasicWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Const.ITBasicWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                //controller.wavePeriodSeconds = 25;
                if (controller.baseCredits > 199)
                {
                    //Stupid but whatevs
                    controller.baseCredits -= 14;
                }
                if (controller is InfiniteTowerExplicitSpawnWaveController)
                {
                    controller.isBossWave = false;
                    controller.uiPrefab = defaultUI;
                    controller.wavePeriodSeconds = 60;
                }
                //
                ArtifactEnabler artifact = wave.GetComponent<ArtifactEnabler>();
                if (artifact)
                {
                    controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = newArticfact;
                }
            }
            for (int i = 0; i< orangeWaves.Count; i++)
            {
                orangeWaves[i].GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = Const.ItemOrangeTierDef.tier;
            }
            orangeWaves.Clear();

            for (int i = 0; i < Const.ITBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Const.ITBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                switch (wave.name)
                {
                    case "WaveBoss_EquipmentDrone":
                        wave.AddComponent<CardRandomizer>().cscList = Waves_SpecialGuy.CardRandomizerEquipmentDrones.cscList;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = Const.ItemOrangeTierDef.tier;
                        break;
                }

                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                if (controller.wavePeriodSeconds == 60)
                {
                    controller.wavePeriodSeconds = 50;
                }
            }

            for (int i = 0; i < Const.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                if (Const.ITSuperBossWaves.wavePrefabs[i].weight > 1)
                {
                    Const.ITSuperBossWaves.wavePrefabs[i].weight = 1;
                }
                GameObject wave = Const.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerExplicitSpawnWaveController temp;

                switch (wave.name)
                {
                    case "InfiniteTowerWaveBrother":
                        wave.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITLunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;
                        wave.AddComponent<SimuExplicitStats>().hpBonusMulti = 2.5f;
                        wave.GetComponent<SimuExplicitStats>().damageBonusMulti = 1.2f;
                        wave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.baseCredits = 50;
                        temp.immediateCreditsFraction = 0.5f;
                        temp.linearCreditsPerWave = 4;
                        temp.combatDirector.monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
                        temp.secondsBeforeSuddenDeath *= 2;
                        break;
                    case "InfiniteTowerWaveWaveBossScav":
                        Const.ITSuperBossWaves.wavePrefabs[i].weight = 0f;
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.rewardDisplayTier = ItemTier.Boss;
                        temp.rewardDropTable = Const.dtITSpecialBossYellow;
                        temp.baseCredits = 100;
                        temp.linearCreditsPerWave = 3;
                        temp.secondsBeforeSuddenDeath *= 2f;
                        wave.AddComponent<SimuExplicitStats>().hpBonusMulti = 0.3f;
                        wave.GetComponent<SimuExplicitStats>().damageBonusMulti = 0.3f;
                        wave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
                        wave.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
                        CharacterSpawnCard cscScav = Object.Instantiate(temp.spawnList[0].spawnCard);
                        cscScav.name = "cscScavBossIT";
                        cscScav.itemsToGrant = new ItemCountPair[]
                        {
                            new ItemCountPair
                            {
                                count = 1,
                                itemDef = RoR2Content.Items.AdaptiveArmor,
                            }
                        };
                        temp.spawnList[0].spawnCard = cscScav;
                        break;
                   /*case "WaveBoss_ScavLunar":
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        CharacterSpawnCard cscScavLunarIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion());
                        cscScavLunarIT.name = "cscScavLunarIT";
                        cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                        temp.spawnList[0].spawnCard = cscScavLunarIT;
                        break;*/
                }
            }

            if (WConfig.cfgMusicSuperBoss.Value)
            {
                AddMusic();
            }
        }

        internal static void AddMusic()
        {
            //Music
            SceneDef moon2 = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/moon2");
            SceneDef rootjungle = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/rootjungle");
            SceneDef voidraid = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidraid/voidraid.asset").WaitForCompletion();
            SceneDef snowyforest = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/snowyforest");
            SceneDef goldshores = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/goldshores");
            SceneDef shipgraveyard = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/shipgraveyard");
            SceneDef skymeadow = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/skymeadow");
            SceneDef dampcavesimple = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/dampcavesimple");

            MusicTrackDef MTDSulfurBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();
            MusicTrackDef muSong_HelminthBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC2/Common/muSong_HelminthBoss.asset").WaitForCompletion();
            MusicTrackDef muSong_MeridianFSB = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC2/Common/muSong_MeridianFSB.asset").WaitForCompletion();
            MusicTrackDef muSong_LakesBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC2/Common/muSong_Lakes&HabitatBoss.asset").WaitForCompletion();

            WwiseStateReference Phase1WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            WwiseStateReference Phase2WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/08652DAD-B715-437F-AB20-0254AD418B4D.asset").WaitForCompletion();
            AK.Wwise.State StatePhase1 = new AK.Wwise.State();
            StatePhase1.WwiseObjectReference = Phase1WW;

            for (int i = 0; i < Const.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Const.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                switch (wave.name)
                {
                    case "InfiniteTowerWaveWaveBossBrother":
                        wave.AddComponent<MusicTrackOverride>().track = moon2.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "WaveBoss_VoidRaidCrab":
                        wave.AddComponent<MusicTrackOverride>().track = voidraid.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "WaveBoss_FalseSon":
                        wave.AddComponent<MusicTrackOverride>().track = muSong_MeridianFSB;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "WaveBoss_ScavLunar":
                        wave.AddComponent<MusicTrackOverride>().track = muSong_LakesBoss;
                        break;
                    case "WaveBoss_SuperVoidMegaCrab":
                        wave.AddComponent<MusicTrackOverride>().track = muSong_HelminthBoss;
                        break;
                    case "InfiniteTowerWaveWaveBossScav":
                        wave.AddComponent<MusicTrackOverride>().track = MTDSulfurBoss;
                        break;
                    case "WaveBoss_TitanGold":
                        wave.AddComponent<MusicTrackOverride>().track = dampcavesimple.bossTrack;
                        break;
                    case "WaveBoss_SuperRoboBallBoss":
                        wave.AddComponent<MusicTrackOverride>().track = skymeadow.bossTrack;
                        break;
                }
            }
        }


        
       }




}