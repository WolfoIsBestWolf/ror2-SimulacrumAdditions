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

        public static void Start()
        {
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

            Organized();
            ModSupport();
        }

 
        internal static void LateChanges()
        {
            MakeMultiCSCs.CreateEquipmentDroneSpawnCards();
            MakeMultiCSCs.CreateGhostSpawnCards();
            MakeMultiCSCs.CreateBossGhostSpawnCards();
            MakeMultiCSCs.CreateDroneSpawnCards();
            //Mod Support
            CharacterSpawnCard cscDireseeker = null;
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
            }

            for (int i = 0; i < SimuMain.ITModSupportWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITModSupportWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();

                switch (wave.name)
                {
                     case "InfiniteTowerWaveArtifactHonorAndBrigade":
                        ArtifactDef SingleEliteType = ArtifactCatalog.FindArtifactDef("SingleEliteType");
                        if (SingleEliteType)
                        {
                            wave.AddComponent<ArtifactEnabler>().artifactDef = SingleEliteType;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SingleEliteType.smallIconSelectedSprite;
                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
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

                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }                       
                        break;
                    case "InfiniteTowerWaveArtifactSS2Cognation":
                        ArtifactDef SS2Cognation = ArtifactCatalog.FindArtifactDef("Cognation");
                        if (SS2Cognation)
                        {
                            wave.GetComponent<ArtifactEnabler>().artifactDef = SS2Cognation;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
 
                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    case "InfiniteTowerWaveBossDireseeker":
                        if (cscDireseeker)
                        {
                            CharacterSpawnCard cscDireseekerIT = Object.Instantiate(cscDireseeker);
                            cscDireseekerIT.name = "cscDireseekerIT";
                            cscDireseekerIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscDireseekerIT;
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
                            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }                
                        break;
                }

            }

            //
            Color newArticfact = new Color(1f, 0.7647f, 1.2647f, 1);
            for (int i = 0; i < SimuMain.ITBasicWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITBasicWaves.wavePrefabs[i].wavePrefab;
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
                    controller.uiPrefab = SimuMain.ITBasicWaves.wavePrefabs[0].wavePrefab.GetComponent<InfiniteTowerWaveController>().uiPrefab;
                    controller.wavePeriodSeconds = 60;
                }
                //
                ArtifactEnabler artifact = wave.GetComponent<ArtifactEnabler>();
                if (artifact)
                {
                    controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = newArticfact;
                }
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBasicEquipmentDrone":
                    case "InfiniteTowerWaveFlight":
                    case "InfiniteTowerWaveBattery":
                    case "InfiniteTowerWaveGoobo":
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
                        break;
                }
            }
            for (int i = 0; i < SimuMain.ITBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossEquipmentDrone":
                        wave.AddComponent<CardRandomizer>().cscList = Waves_SpecialGuy.CardRandomizerEquipmentDrones.cscList;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
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

            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                if (SimuMain.ITSuperBossWaves.wavePrefabs[i].weight > 1)
                {
                    SimuMain.ITSuperBossWaves.wavePrefabs[i].weight = 1;
                }
                GameObject wave = SimuMain.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerExplicitSpawnWaveController temp;

                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossBrother":
                        wave.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;
                        wave.AddComponent<SimuExplicitStats>().hpBonusMulti = 2.5f;
                        wave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.baseCredits = 50;
                        temp.immediateCreditsFraction = 0.5f;
                        temp.linearCreditsPerWave = 4;
                        temp.combatDirector.monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion(); ;
                        temp.secondsBeforeSuddenDeath *= 2;
                        break;
                    case "InfiniteTowerWaveBossScav":
                        SimuMain.ITSuperBossWaves.wavePrefabs[i].weight = 0f;
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.rewardDisplayTier = ItemTier.Boss;
                        temp.rewardDropTable = SimuMain.dtITSpecialBossYellow;
                        temp.baseCredits = 100;
                        temp.linearCreditsPerWave = 3;
                        temp.secondsBeforeSuddenDeath *= 2f;
                        wave.AddComponent<SimuExplicitStats>().hpBonusMulti = 0.3f;
                        wave.GetComponent<SimuExplicitStats>().damageBonusMulti = 0.3f;
                        wave.GetComponent<SimuExplicitStats>().halfOnNonFinal = false;
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
                   /*case "InfiniteTowerWaveBossScavLunar":
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

            WwiseStateReference Phase1WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            //WwiseStateReference Phase2WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/08652DAD-B715-437F-AB20-0254AD418B4D.asset").WaitForCompletion();
            //AK.Wwise.State VoidlingPhase1 = Addressables.LoadAssetAsync<AK.Wwise.State>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            AK.Wwise.State StatePhase1 = new AK.Wwise.State();
            StatePhase1.WwiseObjectReference = Phase1WW;

            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossBrother":
                        wave.AddComponent<MusicTrackOverride>().track = moon2.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "InfiniteTowerWaveBossVoidRaidCrab":
                        wave.AddComponent<MusicTrackOverride>().track = voidraid.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "InfiniteTowerWaveBossScavLunar":
                        wave.AddComponent<MusicTrackOverride>().track = snowyforest.bossTrack;
                        break;
                    case "InfiniteTowerWaveBossSuperVoidMegaCrab":
                        wave.AddComponent<MusicTrackOverride>().track = MTDSulfurBoss;
                        break;
                    case "InfiniteTowerWaveBossScav":
                        wave.AddComponent<MusicTrackOverride>().track = rootjungle.mainTrack;
                        break;
                    case "InfiniteTowerWaveBossTitanGold":
                        wave.AddComponent<MusicTrackOverride>().track = dampcavesimple.bossTrack;
                        break;
                    case "InfiniteTowerWaveBossSuperRoboBallBoss":
                        wave.AddComponent<MusicTrackOverride>().track = skymeadow.bossTrack;
                        break;
                }
            }
        }

        internal static void Organized()
        {
            ItemDef RedWhip = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SprintOutOfCombat");
            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef BoostAttackSpeed = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BoostAttackSpeed");
            ItemDef Hoof = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Hoof");
            ItemDef AdaptiveArmor = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AdaptiveArmor");
            ItemDef CutHP = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHP");
            ItemDef LunarBadLuck = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/LunarBadLuck");
            ItemDef AutoCastEquipment = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AutoCastEquipment");
        
            ItemDef UseAmbientLevel = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel");
            ItemDef SecondarySkillMagazine = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SecondarySkillMagazine");
          
            ItemDef AlienHead = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AlienHead");

            ItemDef CritGlassesVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CritGlassesVoid");
            ItemDef ExtraLifeVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ExtraLifeVoid");
            //


        }



        internal static void ModSupport()
        {
            //Could do NemMando NemMerc but idk they'd just fucking die like in the regular game

            //Minor Mod - DireSeeker
            GameObject InfiniteTowerWaveBossDireseeker = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossDireseeker", true);
            GameObject InfiniteTowerCurrentBossDireseekerWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossDireseekerWaveUI", false);

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossDireseeker.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossDireseeker.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossDireseekerWaveUI;

            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Direseeker";
            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Track and Kill.";

            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDireseeker = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossDireseeker, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBossDireseeker);
            //
            //
            //Will need to see what Empyrean Elites actually do
            GameObject InfiniteTowerWaveSS2RainbowElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2RainbowElites", true);
            GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Empyrean";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Powerful energies have harmonized.";
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 0.7f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.85f, 0.3f, 0.3f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 4f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
            //
            //
            //Storms
            GameObject InfiniteTowerWaveSS2Storms = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2Storms", true);
            GameObject InfiniteTowerCurrentSS2StormsWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2StormsWaveUI", false);

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2StormsWaveUI;
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Storms";
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A storm approaches.";
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2Storms = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2Storms, weight = 3f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2Storms);
        }

       }




}