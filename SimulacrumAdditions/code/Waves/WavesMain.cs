using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections.Generic;
 

namespace SimulacrumAdditions.Waves
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
            MakeMultiCSCs.CreateGhostSpawnCardsAC();
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

            for (int i = 0; i < Constant.ITModSupportWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Constant.ITModSupportWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();

                switch (wave.name)
                {
                    case "InfiniteTowerWaveArtifactHonorAndBrigade":
                        ArtifactDef SingleEliteType = ArtifactCatalog.FindArtifactDef("SingleEliteType");
                        if (SingleEliteType)
                        {
                            wave.AddComponent<ArtifactEnabler>().artifactDef = SingleEliteType;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = SingleEliteType.smallIconSelectedSprite;
                            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(Constant.ITModSupportWaves.wavePrefabs[i]);
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
                            new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 3950}, //1000
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
                            controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = rainbowEliteEquip.passiveBuffDef.iconSprite;

                            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(Constant.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    case "InfiniteTowerWaveArtifactSS2Cognation":
                        ArtifactDef SS2Cognation = ArtifactCatalog.FindArtifactDef("Cognation");
                        if (SS2Cognation)
                        {
                            wave.GetComponent<ArtifactEnabler>().artifactDef = SS2Cognation;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = SS2Cognation.smallIconSelectedSprite;

                            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(Constant.ITModSupportWaves.wavePrefabs[i]);
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



            GameObject defaultUI = Constant.ITBasicWaves.wavePrefabs[0].wavePrefab.GetComponent<InfiniteTowerWaveController>().uiPrefab;
            Color newArticfact = new Color(1f, 0.7647f, 1.2647f, 1);
            for (int i = 0; i < Constant.ITBasicWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Constant.ITBasicWaves.wavePrefabs[i].wavePrefab;
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
                    controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = newArticfact;
                }
            }
            for (int i = 0; i < orangeWaves.Count; i++)
            {
                orangeWaves[i].GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = Constant.ItemOrangeTierDef.tier;
            }
            orangeWaves.Clear();

            Waves_SpecialGuy.WaveBoss_EquipmentDrone.AddComponent<CardRandomizer>().cscList = Waves_SpecialGuy.CardRandomizerEquipmentDrones.cscList;
            Waves_SpecialGuy.WaveBoss_EquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = Constant.ItemOrangeTierDef.tier;

            for (int i = 0; i < Constant.ITBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Constant.ITBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
        
                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                if (controller.wavePeriodSeconds == 60)
                {
                    controller.wavePeriodSeconds = 50;
                }
            }

            for (int i = 0; i < Constant.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                if (Constant.ITSuperBossWaves.wavePrefabs[i].weight > 1)
                {
                    Constant.ITSuperBossWaves.wavePrefabs[i].weight = 1;
                }
                GameObject wave = Constant.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerExplicitSpawnWaveController WaveTemp;

                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossBrother":
                        wave.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITLunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;
                        wave.AddComponent<SimuExplicitStats>().hpBonusMulti = 3f;
                        wave.GetComponent<SimuExplicitStats>().damageBonusMulti = 1.2f;
                        wave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
                        WaveTemp = wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
                        WaveTemp.baseCredits = 50;
                        WaveTemp.immediateCreditsFraction = 0.5f;
                        WaveTemp.linearCreditsPerWave = 4;
                        WaveTemp.combatDirector.monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
                        WaveTemp.secondsBeforeSuddenDeath *= 2;
                        break;
 
                }
            }

             
            var temp = Constant.ScavWave.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            temp.rewardDisplayTier = ItemTier.Boss;
            temp.rewardDropTable = Constant.dtITSpecialBossYellow;
            temp.baseCredits = 100;
            temp.linearCreditsPerWave = 3;
            temp.secondsBeforeSuddenDeath *= 2f;
            Constant.ScavWave.AddComponent<SimuExplicitStats>().hpBonusMulti = 0.3f;
            Constant.ScavWave.GetComponent<SimuExplicitStats>().damageBonusMulti = 0.3f;
            Constant.ScavWave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
            Constant.ScavWave.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
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
            MusicTrackDef muGameplayDLC3_07_RC_Boss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "bdab9790c981dac439bf3b540b2b171d").WaitForCompletion();

            WwiseStateReference Phase1WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            WwiseStateReference Phase2WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/08652DAD-B715-437F-AB20-0254AD418B4D.asset").WaitForCompletion();
            AK.Wwise.State StatePhase1 = new AK.Wwise.State();
            StatePhase1.WwiseObjectReference = Phase1WW;

            for (int i = 0; i < Constant.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = Constant.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
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
                    case "WaveBoss_SuperVoidMegaCrab":
                        wave.AddComponent<MusicTrackOverride>().track = muSong_HelminthBoss;
                        break;
                    case "WaveBoss_TitanGold":
                        wave.AddComponent<MusicTrackOverride>().track = dampcavesimple.bossTrack;
                        break;
                    case "WaveBoss_SuperRoboBallBoss":
                        wave.AddComponent<MusicTrackOverride>().track = skymeadow.bossTrack;
                        break;
                }
            }


            Constant.ScavWave.AddComponent<MusicTrackOverride>().track = MTDSulfurBoss;
            Waves_SuperBoss.WaveBoss_ScavLunar.AddComponent<MusicTrackOverride>().track = muSong_LakesBoss;
            Waves_SuperBoss.WaveBoss_VultureHunter.AddComponent<MusicTrackOverride>().track = muGameplayDLC3_07_RC_Boss;
        }



    }




}