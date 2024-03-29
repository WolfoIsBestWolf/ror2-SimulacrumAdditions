﻿using BepInEx;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace SimulacrumAdditions
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.SimulacrumAdditions", "SimulacrumAdditions", "1.9.3")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class SimuMain : BaseUnityPlugin
    {
        public static int SimuEndingStartAtXWaves;
        public static int SimuEndingEveryXWaves;
        public static int SimuEndingWaveRest;
        public static int SimuForcedBossStartAtXWaves;
        public static int SimuForcedBossEveryXWaves;
        public static int SimuForcedBossWaveRest;

        public static RoR2.InfiniteTowerWaveCategory ITBasicWaves = Addressables.LoadAssetAsync<RoR2.InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/CommonWaveCategory.asset").WaitForCompletion();
        public static RoR2.InfiniteTowerWaveCategory ITBossWaves = Addressables.LoadAssetAsync<RoR2.InfiniteTowerWaveCategory>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveCategories/BossWaveCategory.asset").WaitForCompletion();
        public static RoR2.InfiniteTowerWaveCategory ITSuperBossWaves = ScriptableObject.CreateInstance<InfiniteTowerWaveCategory>();
        public static RoR2.InfiniteTowerWaveCategory ITModSupportWaves = ScriptableObject.CreateInstance<InfiniteTowerWaveCategory>();
        //Would need to be the first in the Array to work normally

        public static GameEndingDef InfiniteTowerEnding = ScriptableObject.CreateInstance<GameEndingDef>();
        public static InteractableSpawnCard iscSimuExitPortal;
        public static GameObject VoidTeleportOutEffect = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/ExtraLifeVoid/VoidRezEffect.prefab").WaitForCompletion(), "VoidTeleportOutEffect", false);

        public static ItemTierDef ItemOrangeTierDef;
        //
        //Does this need to be in the Simu File 
        public static BasicPickupDropTable dtAISafeRandomVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        //
        //
        //Wave Prerequesites
        public static RoR2.InfiniteTowerWaveCountPrerequisites AfterWave5Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave11Prerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveCountPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Wave11OrGreaterPrerequisite.asset").WaitForCompletion();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave15Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave20Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave25Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave30Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave35Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave40Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites StartWave50Prerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        //
        //
        //Simu Wave Reward Drop Tables
        public static BasicPickupDropTable dtITWaveTier1 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITDefaultWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier2 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier3 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITSpecialBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITVoid = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITVoid.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITLunar = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITLunar.asset").WaitForCompletion();

        public static BasicPickupDropTable dtAllTier = ScriptableObject.CreateInstance<BasicPickupDropTable>();
       
        public static BasicPickupDropTable dtITFamilyWaveDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITBossCategoryDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBossCategoryHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBossCategoryUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();


        public static BasicPickupDropTable dtITBasicWaveOnKill = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBasicBonusLunar = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBasicBonusVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITBasicBonusGreen = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBossBonusRed = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBossGreenVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITSpecialEquipment = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITVoidInfestorWave = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSpecialBossYellow = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static ExplicitPickupDropTable dtITHeresy = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();
        public static ExplicitPickupDropTable dtITWurms = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();

        public static SceneDef PreviousSceneDef = null;
        //public static CombatDirector.EliteTierDef[] EliteTiersBackup;
        public static ItemDef ITDamageDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITAttackSpeedDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHealthScaling = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITKillOnCompletion = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHorrorName = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITCooldownUp = ScriptableObject.CreateInstance<ItemDef>();

        public static MasterCatalog.MasterIndex IndexAffixHealingCore = MasterCatalog.MasterIndex.none;

        /*public float BasicWeightCommon = 5;
        public float BasicWeightUncommon = 4;
        public float BasicWeightRare = 3;*/
        public float BossWeightCommon = 10;
        public float BossWeightUncommon = 6;
        public float BossWeightRare = 4;

        public void Awake()
        {
            //AkSoundEngine.SetState(this.id, this.valueId);

            WConfig.InitConfig();
            if (WConfig.cfgDumpInfo.Value)
            {
                DumpAllWaveInfo(ITBasicWaves);
                DumpAllWaveInfo(ITBossWaves);
            }

            MakePortal();
            SetupConstants();
            SimuChanges();

            WavesMain.SuperBossWaves();         
            GiantGup.Start();
            SuperMegaCrab.Start();
            WavesMain.EliteWaves();
            WavesArtifacts.Start();
            WavesFamily.Start();
            WavesMiscSorted.MakeItemGivingWaves();
            WavesMiscSorted.MakeEquipmentWaves();
            WavesBuff.MakeBuffWaves();
            WavesMiscSorted.MakePulseWaves();

            WavesMain.Start();

            SimulacrumDCCS.Start();
            CrabActivateToTravel.Make();

            if (WConfig.cfgEnableArtifact.Value)
            {
                Artifact.MakeArtifact();
                ArtifactReal.MakeArtifact();
            }

            LanguageAPI.Add("INFINITETOWER_SUDDEN_DEATH", "<style=cWorldEvent>[WARNING] The Focus begins to falter..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_AWAITINGACTIVATION", "Activate the <style=cIsVoid>Focus</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_TRAVEL", "Follow the <style=cIsVoid>Focus</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_PORTAL", "Advance through the <style=cIsVoid>Infinite Portal</style>", "en");

            LanguageAPI.Add("MAP_INFINITETOWER_SUBTITLE_ITMOON", "Latest specimen", "en");

            //EquipmentCatalog.availability.CallWhenAvailable(MakeLateWaves);
            GameModeCatalog.availability.CallWhenAvailable(LateRunningMethod);

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            IL.RoR2.CharacterBody.RecalculateStats += (ILContext il) =>
            {
                ILCursor c = new ILCursor(il);
                if (c.TryGotoNext(MoveType.After,
                 x => x.MatchCall("RoR2.CharacterBody", "set_barrierDecayRate")
                ))
                {
                    c.Index -= 4;
                    c.EmitDelegate<System.Func<CharacterBody, CharacterBody>>((body) =>
                    {
                        //Might not have inventory ig
                        if (body.inventory)
                        {
                            int numHealth = body.inventory.GetItemCount(ITHealthScaling);
                            if (numHealth > 0)
                            {
                                body.maxHealth *= 1 + 0.01f * numHealth;
                                body.maxShield *= 1 + 0.01f * numHealth;
                            }
                        }
                        return body;
                    });
                    //Debug.Log("IL Found : IL.RoR2.CharacterBody.RecalculateStats");
                }
                else
                {
                    Debug.LogWarning("IL Failed : IL.RoR2.CharacterBody.RecalculateStats");
                }
            };


            //
            On.RoR2.InfiniteTowerRun.PreStartClient += InfiniteTowerRunPreStartClient;
            On.RoR2.InfiniteTowerRun.OnDestroy += InfiniteTowerRunEnd;

            On.RoR2.PlayerCharacterMasterController.Start += (orig, self) =>
            {
                orig(self);
                Debug.Log(Run.instance);
                if (NetworkServer.active && Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
                {
                    self.master.GiveVoidCoins(1);
                    VoidCoinChance chance = self.gameObject.AddComponent<VoidCoinChance>();
                    float players = 1 + (Run.instance.participatingPlayerCount - 1) * 0.2f; //This probably don't really work
                    chance.chance /= players;
                };
            };

            //Prevent Repeat Stages
            On.RoR2.InfiniteTowerRun.OnWaveAllEnemiesDefeatedServer += InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer;
            //General Pre Wave
            On.RoR2.InfiniteTowerWaveCategory.SelectWavePrefab += InfiniteTowerWaveCategory_SelectWavePrefab;

            //Give Stats here
            On.RoR2.InfiniteTowerExplicitSpawnWaveController.Initialize += GiveStatBoosts_ExplicitSpawnWaveController_Initialize;

            //General Start of Wave
            On.RoR2.InfiniteTowerRun.InitializeWaveController += InfiniteTowerRun_BeginNextWave;
            On.RoR2.InfiniteTowerWaveController.OnCombatSquadMemberDiscovered += InfiniteTowerWaveController_OnCombatSquadMemberDiscovered;

            On.RoR2.InfiniteTowerWaveController.StartTimer += KillAllGhosts_StartTimer;
            //Post Wave, End Portal, Double Rewards
            On.RoR2.InfiniteTowerWaveController.OnAllEnemiesDefeatedServer += InfiniteTowerWaveController_OnAllEnemiesDefeatedServer;
            //Mostly Elite Wave stuff
            On.RoR2.InfiniteTowerRun.CleanUpCurrentWave += InfiniteTowerRun_CleanUpCurrentWave;
            //
            if (WConfig.cfgItemsFrequently.Value)
            {
                On.RoR2.InfiniteTowerRun.AdvanceWave += MoreItems_AdvanceWave;
            }
            On.RoR2.InfiniteTowerRun.AdvanceWave += MakeWavesMoreCommon;

            On.EntityStates.InfiniteTowerSafeWard.Active.OnEnter += RadiusManipActive_OnEnter;
            //After Traveling Radius increases before the Wave Starts, helps when gets into bad spots
            On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.OnEnter += AwaitingActivation_OnEnter;
            //Bigger Travel Radius, just feels better
            On.EntityStates.InfiniteTowerSafeWard.Travelling.OnEnter += Travelling_OnEnter;
            On.EntityStates.InfiniteTowerSafeWard.Burrow.OnEnter += (orig, self) =>
            {
                float before = self.GetComponent<VerticalTubeZone>().radius;
                self.radius = before;
                orig(self);
            };
            //He'd leave too soon on special boss waves
            On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += (orig, self) =>
            {
                orig(self);
                //Debug.LogWarning(self.duration);
                GameObject temp = Run.instance.GetComponent<InfiniteTowerRun>().waveInstance;
                if (self.duration > 0 && temp)
                {
                    self.duration = temp.GetComponent<InfiniteTowerWaveController>().secondsAfterWave;
                }
            };

            //Use Custom Simu Interactable DCCSs
            On.RoR2.SceneDirector.Start += SimulacrumDCCS.ITMoonExtras;
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.SimuInteractableDCCSAdder;
            //More Interactables early on to get into it quicker
            if (WConfig.cfgSimuCreditsRebalance.Value)
            {
                On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.CreditsRebalance;
            }
            //
            //
            On.RoR2.InfiniteTowerWaveController.DropRewards += (orig, self) =>
            {
                orig(self);
                SimulacrumExtrasHelper temp = self.GetComponent<SimulacrumExtrasHelper>();
                if (temp && temp.rewardDropTable != null)
                {
                    temp.DropRewards();
                }
            };


            //Fake Ass Ending Overwrite
            On.RoR2.EventFunctions.BeginEnding += SimulacrumEndingBeginEnding;

            //Give Simu Scavs Void Items
            On.RoR2.ScavengerItemGranter.Start += SimuGiveScavVoidItems;

            //Prevents explicit wave in basic waves whatever from moving crab 
            On.RoR2.InfiniteTowerRun.MoveSafeWard += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };
            On.RoR2.InfiniteTowerBossWaveController.OnTimerExpire += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    if (!NetworkServer.active)
                    {
                        Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnTimerExpire()' called on client");
                        return;
                    }
                    self.MarkAsFinished();
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };
            On.RoR2.InfiniteTowerExplicitSpawnWaveController.OnTimerExpire += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    if (!NetworkServer.active)
                    {
                        Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnTimerExpire()' called on client");
                        return;
                    }
                    self.MarkAsFinished();
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };

            //Add glows to Option Pickups
            GameObject VoidPotential = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/OptionPickup/OptionPickup.prefab").WaitForCompletion();
            GameObject OptionPickerPanel = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/OptionPickup/OptionPickerPanel.prefab").WaitForCompletion();
            VoidPotential.transform.GetChild(0).GetChild(0).GetComponent<SphereCollider>().radius = 1.5f;
            VoidPotential.transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(2).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(4).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(5).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(6).localPosition = new Vector3(0, -0.5f, 0);

            OptionPickerPanel.GetComponent<RoR2.UI.PickupPickerPanel>().maxColumnCount = 3;

            On.RoR2.PickupPickerController.SetOptionsInternal += OptionChestColorsAndName;
            //
            VoidTeleportOutEffect.transform.GetChild(9).gameObject.SetActive(false);
            Destroy(VoidTeleportOutEffect.transform.GetChild(4).gameObject);
            Destroy(VoidTeleportOutEffect.transform.GetChild(0).gameObject);
            VoidTeleportOutEffect.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            VoidTeleportOutEffect.GetComponent<EffectComponent>().soundName = "Play_UI_charTeleport";
            R2API.ContentAddition.AddEffect(VoidTeleportOutEffect);
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
            //
            //
            VoidCoin.MakeVoidCoin();
            //
            //Doesn't need to ban world unique anymore anyways
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/DuplicatorWild/dtDuplicatorWild.asset").WaitForCompletion().bannedItemTags = new ItemTag[0];
            On.RoR2.Run.BuildDropTable += (orig, self) =>
            {
                orig(self);
                if (self is InfiniteTowerRun)
                {
                    self.availableBossDropList.Add(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex));
                    self.availableBossDropList.Add(PickupCatalog.FindPickupIndex(RoR2Content.Items.TitanGoldDuringTP.itemIndex));
                }
            };
            //Gets spawned on every wave start ig
            On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    GoldTitanManager.EndChannelingTitansServer(self.gameObject);
                }
            };
            On.EntityStates.InfiniteTowerSafeWard.AwaitingPortalUse.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    GoldTitanManager.EndChannelingTitansServer(self.gameObject);
                }
            };
            //

            On.RoR2.InfiniteTowerWaveController.HasFullProgress += (orig, self) =>
            {
                if (self._totalCreditsSpent == 0)
                {
                    return false;
                }
                return orig(self);
            };

            IL.RoR2.InfiniteTowerWaveController.FixedUpdate += FixRequestIndicatorsClient;

            /*On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += (orig, self, sceneDirector) =>
            {
                orig(self, sceneDirector);
                if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.Sacrifice))
                {
                    sceneDirector.interactableCredit = 0;
                }
            };*/

            On.RoR2.UI.MainMenu.MainMenuController.Start += OneTimeLateRunner;
         
        }


 

        private void OneTimeLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);
            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                if (ITSuperBossWaves.wavePrefabs[i].wavePrefab.name.EndsWith("ScavLunar"))
                {
                    InfiniteTowerExplicitSpawnWaveController temp = ITSuperBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    CharacterSpawnCard cscScavLunarIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion());
                    cscScavLunarIT.name = "cscScavLunarIT";
                    cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                    temp.spawnList[0].spawnCard = cscScavLunarIT;

                }
            }
            On.RoR2.UI.MainMenu.MainMenuController.Start -= OneTimeLateRunner;
        }

        private void OptionChestColorsAndName(On.RoR2.PickupPickerController.orig_SetOptionsInternal orig, PickupPickerController self, PickupPickerController.Option[] newOptions)
        {
            orig(self, newOptions);
            if (self.name.StartsWith("Option"))
            {
                PickupIndexNetworker index = self.GetComponent<PickupIndexNetworker>();
                PickupDisplay pickupDisplay = self.transform.GetChild(0).GetComponent<PickupDisplay>();
                pickupDisplay.pickupIndex = index.pickupIndex;
                self.GetComponent<Highlight>().pickupIndex = index.pickupIndex;
                self.GetComponent<Highlight>().isOn = true;

                if (index.pickupIndex != PickupIndex.none)
                {
                    switch (index.pickupIndex.pickupDef.itemTier)
                    {
                        case ItemTier.Tier1:
                            pickupDisplay.tier1ParticleEffect.SetActive(true);
                            break;
                        case ItemTier.Tier2:
                            pickupDisplay.tier2ParticleEffect.SetActive(true);
                            break;
                        case ItemTier.Tier3:
                            pickupDisplay.tier3ParticleEffect.SetActive(true);
                            break;
                        case ItemTier.Boss:
                            pickupDisplay.bossParticleEffect.SetActive(true);
                            break;
                        case ItemTier.Lunar:
                            pickupDisplay.lunarParticleEffect.SetActive(true);
                            break;
                        case ItemTier.VoidTier1:
                        case ItemTier.VoidTier2:
                        case ItemTier.VoidTier3:
                        case ItemTier.VoidBoss:
                            if (!pickupDisplay.voidParticleEffect)
                            {
                                pickupDisplay.voidParticleEffect = Object.Instantiate(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GenericPickup").GetComponent<GenericPickupController>().pickupDisplay.voidParticleEffect, pickupDisplay.transform);
                            }
                            pickupDisplay.voidParticleEffect.SetActive(true);
                            break;
                    }
                    if (index.pickupIndex.pickupDef.itemTier == ItemOrangeTierDef.tier)
                    {
                        pickupDisplay.equipmentParticleEffect.SetActive(true);
                    }
                }


                if (WConfig.cfgVoidTripleContentsInPing.Value)
                {
                    string NameWithOptions = Language.GetString("OPTION_PICKUP_UNKNOWN_NAME");
                    NameWithOptions += "\n(";

                    for (int i = 0; i < newOptions.Length; i++)
                    {
                        PickupDef defTemp = newOptions[i].pickupIndex.pickupDef;
                        string ItemName = "";
                        if (defTemp.itemIndex != ItemIndex.None)
                        {
                            ItemName = Language.GetString(ItemCatalog.GetItemDef(newOptions[i].pickupIndex.pickupDef.itemIndex).nameToken);
                        }
                        else if (defTemp.equipmentIndex != EquipmentIndex.None)
                        {
                            ItemName = Language.GetString(EquipmentCatalog.GetEquipmentDef(newOptions[i].pickupIndex.pickupDef.equipmentIndex).nameToken);
                        }


                        string Hex = ColorUtility.ToHtmlStringRGB(newOptions[i].pickupIndex.pickupDef.baseColor);
                        if (i != 0)
                        {
                            NameWithOptions += " | <color=#" + Hex + ">" + ItemName + "</color>";
                        }
                        else
                        {
                            NameWithOptions += "<color=#" + Hex + ">" + ItemName + "</color>";
                        }
                    }
                    NameWithOptions += ")";
                    self.GetComponent<GenericDisplayNameProvider>().SetDisplayToken(NameWithOptions);
                }
                else
                {
                    self.GetComponent<GenericDisplayNameProvider>().SetDisplayToken("OPTION_PICKUP_UNKNOWN_NAME");
                }
            }
        }

        private void FixRequestIndicatorsClient(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            c.TryGotoNext(MoveType.After,
             x => x.MatchCallvirt("RoR2.CombatSquad", "get_readOnlyMembersList"));

            if (c.TryGotoPrev(MoveType.Before,
             x => x.MatchLdfld("RoR2.InfiniteTowerWaveController", "combatSquad")
            ))
            {
                c.EmitDelegate<System.Func<InfiniteTowerWaveController, InfiniteTowerWaveController>>((wave) =>
               {
                   if (wave.combatSquad.readOnlyMembersList.Count == 0)
                   {
                       Debug.LogWarning("Couln't do indicators the normal way");
                       for (int i = 0; wave.combatSquad.membersList.Count > i; i++)
                       {
                           wave.RequestIndicatorForMaster(wave.combatSquad.membersList[i]);
                       }
                   }
                   return wave;
               });
                Debug.Log("IL Found : IL.RoR2.InfiniteTowerWaveController.FixedUpdate");
            }
            else
            {
                Debug.LogWarning("IL Failed : IL.RoR2.InfiniteTowerWaveController.FixedUpdate");
            }
        }

        private void MakeWavesMoreCommon(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            orig(self);
            if (self.waveIndex == 50)
            {
                if (ITBossWaves.wavePrefabs[0].weight > 1)
                {
                    ITBasicWaves.wavePrefabs[0].weight = 25; //More Wackies past this
                    ITBasicWaves.GenerateWeightedSelection();
                    ITBossWaves.wavePrefabs[0].weight = 18; //More Wackies past this
                    ITBossWaves.GenerateWeightedSelection();
                }
            }
            else if (self.waveIndex == 31)
            {
                if (ITBasicWaves.wavePrefabs[0].weight > 1)
                {
                    ITBasicWaves.wavePrefabs[0].weight = 50; //More Wackies past this
                    ITBasicWaves.GenerateWeightedSelection();
                    ITBossWaves.wavePrefabs[0].weight = 45; //More Wackies past this
                    ITBossWaves.GenerateWeightedSelection();
                }
            }
        }

        private void KillAllGhosts_StartTimer(On.RoR2.InfiniteTowerWaveController.orig_StartTimer orig, InfiniteTowerWaveController self)
        {
            orig(self);


            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                if (self.waveIndex % 5 != 0)
                {
                    int players = Mathf.Min(5, Run.instance.participatingPlayerCount);
                    self.Network_timerStart += self.waveIndex / 10 * players * 0.2f;

                    Debug.Log("secondsAfterWave: " + self.Network_timerStart);
                }
            }
            MusicTrackOverride music = self.gameObject.GetComponent<MusicTrackOverride>();
            if (music)
            {
                music.enabled = false;
            }
            if (NetworkServer.active)
            {
                foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
                {
                    if (characterMaster && characterMaster.inventory)
                    {
                        int itemCount = characterMaster.inventory.GetItemCount(ITKillOnCompletion);
                        if (itemCount > 0)
                        {
                            characterMaster.inventory.GiveItem(RoR2Content.Items.HealthDecay, 1);
                        }
                        if (itemCount > 6)
                        {
                            CharacterBody body = characterMaster.GetBody();
                            if (body)
                            {
                                body.healthComponent.Suicide(null, null, DamageType.Generic);
                            }
                            characterMaster.TrueKill();
                            if (characterMaster.playerCharacterMasterController == null)
                            {
                                Destroy(characterMaster.gameObject);
                            }
                        }
                    }
                }
            }
        }

        private void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            if (self.inventory)
            {
                int numDmg = self.inventory.GetItemCount(ITDamageDown);
                if (numDmg > 0)
                {
                    self.damage *= 1 - 0.01f * numDmg;
                }
                int numAspd = self.inventory.GetItemCount(ITAttackSpeedDown);
                if (numAspd > 0)
                {
                    self.attackSpeed *= 1 - 0.01f * numAspd;
                    int numITKillOnCompletion = self.inventory.GetItemCount(ITKillOnCompletion);
                    if (numITKillOnCompletion == 54)
                    {
                        self.healthComponent.Networkhealth = self.maxHealth * 0.45f;
                    }
                    if (numITKillOnCompletion == 56)
                    {
                        self.skillLocator.primary = null;
                        self.AddBuff(RoR2Content.Buffs.Cloak);
                    }
                    if (numITKillOnCompletion > 77)
                    {
                        self.baseMoveSpeed = 0;
                        self.moveSpeed = 0;
                        self.skillLocator.primary = null;
                        if (numITKillOnCompletion > 78)
                        {
                            self.AddBuff(RoR2Content.Buffs.Cloak);

                            if (numITKillOnCompletion == 81)
                            {
                                if (!self.master.gameObject.GetComponent<MasterSuicideOnTimer>())
                                {
                                    //This is like really unhealthy inIt?
                                    self.master.gameObject.AddComponent<MasterSuicideOnTimer>().lifeTimer = 0.1f;
                                }                              
                            }
                        }

                    }
                }
                int numCooldown = self.inventory.GetItemCount(ITCooldownUp);
                if (numCooldown > 0)
                {
                    if (self.skillLocator.primary)
                    {
                        self.skillLocator.primary.cooldownScale = 1 + numCooldown / 10;
                    }
                }

                if (self.HasBuff(WavesBuff.bdSlippery))
                {
                    if (self.teamComponent.teamIndex == TeamIndex.Monster)
                    {
                        self.acceleration /= 5;
                        self.moveSpeed *= 1.5f;
                    }
                    else
                    {
                        self.acceleration /= 10;
                        self.moveSpeed *= 2f;
                    }
                }
                if (self.HasBuff(WavesBuff.bdBadLuck))
                {
                    self.master.luck = -3;
                }
            }
        }

        //GivePickupOnStarts runs after this
        //Just because we null this doesn't mean it didn't get added to the combat squad
        private void InfiniteTowerWaveController_OnCombatSquadMemberDiscovered(On.RoR2.InfiniteTowerWaveController.orig_OnCombatSquadMemberDiscovered orig, InfiniteTowerWaveController self, CharacterMaster master)
        {
            orig(self, master);

            int kill = master.inventory.GetItemCount(ITKillOnCompletion);
            if (kill > 0)
            {
                self.combatSquad.RemoveMember(master);
                CharacterBody body = master.GetBody();

                int horror = master.inventory.GetItemCount(ITHorrorName);
                if (horror > 0)
                {
                    if (body)
                    {
                        Transform modelTransform = body.GetComponent<ModelLocator>().modelTransform;
                        if (modelTransform)
                        {
                            /*
                            Highlight highlight = modelTransform.gameObject.AddComponent<Highlight>();
                            CharacterModel component2 = modelTransform.GetComponent<CharacterModel>();
                            if (component2)
                            {
                                bool flag = false;
                                foreach (CharacterModel.RendererInfo rendererInfo in component2.baseRendererInfos)
                                {
                                    if (!rendererInfo.ignoreOverlays && !flag)
                                    {
                                        highlight.highlightColor = Highlight.HighlightColor.interactive;
                                        highlight.targetRenderer = rendererInfo.renderer;
                                        highlight.strength = 1f;
                                        highlight.isOn = true;
                                        flag = true;
                                    }
                                }
                            }
                            */
                        }
                    }
                }
                if (NetworkServer.active)
                {
                    RoR2.CharacterAI.BaseAI tempAI = master.GetComponent<RoR2.CharacterAI.BaseAI>();
                    if (tempAI)
                    {
                        tempAI.fullVision = true;
                    }
                    for (int i = 0; i < tempAI.skillDrivers.Length; i++)
                    {
                        tempAI.skillDrivers[i].maxUserHealthFraction = 1;
                        if (kill == 4)
                        {
                            if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Special)
                            {
                                tempAI.skillDrivers[i].maxTimesSelected = 1;
                            }
                        }
                        else if (kill == 5)
                        {
                            if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Utility)
                            {
                                tempAI.skillDrivers[i].maxTimesSelected = 0;
                            }
                        }
                        else if (kill == 6)
                        {
                            if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Utility)
                            {
                                tempAI.skillDrivers[i].maxTimesSelected = 2;
                            }
                        }
                    }
                    if (kill == 10)
                    {
                        Destroy(body.gameObject.GetComponent<GenericSkill>());
                    }
                    if (kill == 78 || kill == 15)
                    {
                    }
                    else
                    {
                        body.AddBuff(RoR2Content.Buffs.Immune);
                    }

                }
            }
            else if (self.hasEnabledEnemyIndicators && master.masterIndex == IndexAffixHealingCore)
            {
                self.combatSquad.RemoveMember(master);
            }

        }

        private void InfiniteTowerRunPreStartClient(On.RoR2.InfiniteTowerRun.orig_PreStartClient orig, InfiniteTowerRun self)
        {
            orig(self);
            ITBossWaves.availabilityPeriod = 5;
            ITBossWaves.minWaveIndex = 0;
            if (ITBossWaves.wavePrefabs[0].weight == 1)
            {
                ITBasicWaves.wavePrefabs[0].weight = 100f;
                ITBasicWaves.GenerateWeightedSelection();
                ITBossWaves.wavePrefabs[0].weight = 90f;
                ITBasicWaves.GenerateWeightedSelection();
            }
            SimulacrumDCCS.MakeITSand(true);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoin.VoidCoinRunStart();
            }
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = false;
            eqDrone.AddComponent<EquipmentDroneInSimulacrum>();
            Destroy(eqDrone.GetComponent<RoR2.SetDontDestroyOnLoad>());
        }



        private void InfiniteTowerRunEnd(On.RoR2.InfiniteTowerRun.orig_OnDestroy orig, InfiniteTowerRun self)
        {
            orig(self);
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = true;
            eqDrone.AddComponent<RoR2.SetDontDestroyOnLoad>();
            eqDrone.GetComponent<EquipmentDroneInSimulacrum>();
            SimulacrumDCCS.MakeITSand(false);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoin.VoidCoinRunEnd();
            }
        }


        private void GiveStatBoosts_ExplicitSpawnWaveController_Initialize(On.RoR2.InfiniteTowerExplicitSpawnWaveController.orig_Initialize orig, InfiniteTowerExplicitSpawnWaveController self, int waveIndex, Inventory enemyInventory, GameObject spawnTargetObject)
        {
            if (NetworkServer.active)
            {
                if (self.linearCreditsPerWave * waveIndex > 400)
                {
                    self.linearCreditsPerWave = (self.linearCreditsPerWave * waveIndex / 400);
                }

                float bonusBonusHPMulti = 0.5f;
                float bonusBonusDmgMulti = 1f;
                bool forcedSuperboss = false;
                if (waveIndex >= SimuForcedBossStartAtXWaves && waveIndex % SimuForcedBossEveryXWaves == SimuForcedBossWaveRest)
                {
                    forcedSuperboss = true;
                    bonusBonusHPMulti = 1f;
                }

                if (self.GetComponent<CardRandomizer>())
                {
                    self.GetComponent<CardRandomizer>().DoTheThing(self);
                }


                int bonusSpawns = 0;
                bool spawnAsVoidTeam = false;
                switch (self.name)
                {
                    case "InfiniteTowerWaveBossScav(Clone)":
                        bonusBonusHPMulti *= 0.3f;
                        bonusBonusDmgMulti = 0.3f;
                        break;
                    case "InfiniteTowerWaveBossBrother(Clone)":
                        bonusBonusHPMulti *= 2.5f;
                        break;
                    case "InfiniteTowerWaveBossVoidRaidCrab(Clone)":
                        bonusBonusHPMulti *= 1.25f;
                        bonusBonusDmgMulti = 0.3f;
                        break;
                    case "InfiniteTowerWaveBossScavLunar(Clone)":
                        bonusBonusHPMulti *= 0.4f;
                        bonusBonusDmgMulti = 0.4f;
                        break;
                    case "InfiniteTowerWaveBossSuperCrab(Clone)":
                        bonusBonusHPMulti *= 0.4f;
                        bonusBonusDmgMulti = 0.4f;
                        break;
                    case "InfiniteTowerWaveBossSuperRoboBallBoss(Clone)":
                        bonusBonusHPMulti *= 0.9f;
                        bonusBonusDmgMulti = 0.7f;
                        break;
                    case "InfiniteTowerWaveBossTitanGold(Clone)":
                        bonusBonusHPMulti *= 1.2f;
                        bonusBonusDmgMulti = 0.7f;
                        break;
                    case "InfiniteTowerWaveBossGiantGup(Clone)":
                        bonusBonusHPMulti = 2f;
                        bonusBonusDmgMulti = 0.4f;
                        break;
                    case "InfiniteTowerWaveBossFamilyWorms(Clone)":
                    case "InfiniteTowerWaveArtifactReliquary(Clone)":
                        bonusBonusHPMulti = -1f;
                        break;
                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        bonusBonusHPMulti = 3f;
                        break;
                    case "InfiniteTowerWaveBasicEquipmentDrone(Clone)":
                    case "InfiniteTowerWaveBossEquipmentDrone(Clone)":
                        if (waveIndex > 30)
                        {
                            self.spawnList[0].count++;
                        }

                        string eqToken = "<style=cWorldEvent>[WARNING] Running test with ";
                        if (self.spawnList.Length == 2)
                        {
                            if (self.spawnList[0].spawnCard.equipmentToGrant[0].isLunar)
                            {
                                eqToken = eqToken + "<color=#78AFFF>";
                            }
                            else
                            {
                                eqToken = eqToken + "<color=#FF8000>";
                            }
                            eqToken = eqToken + Language.GetString(self.spawnList[0].spawnCard.equipmentToGrant[0].nameToken, "en") + "</color> and ";
                            if (self.spawnList[1].spawnCard.equipmentToGrant[0].isLunar)
                            {
                                eqToken = eqToken + "<color=#78AFFF>";
                            }
                            else
                            {
                                eqToken = eqToken + "<color=#FF8000>";
                            }
                            eqToken = eqToken + Language.GetString(self.spawnList[1].spawnCard.equipmentToGrant[0].nameToken, "en") + "</color></style>";
                        }
                        else
                        {
                            if (self.spawnList[0].spawnCard.equipmentToGrant[0].isLunar)
                            {
                                eqToken = eqToken + "<color=#78AFFF>";
                            }
                            else
                            {
                                eqToken = eqToken + "<color=#FF8000>";
                            }
                            eqToken = eqToken + Language.GetString(self.spawnList[0].spawnCard.equipmentToGrant[0].nameToken, "en") + "</color></style>";
                        }
                        Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                        {
                            baseToken = eqToken,
                        });
                        break;
                    case "InfiniteTowerWaveBasicGhostHaunting(Clone)":
                    case "InfiniteTowerWaveInvisibleDude(Clone)":
                    case "InfiniteTowerWaveBossWithDrone(Clone)":
                        bonusBonusDmgMulti = 0.8f;
                        if (waveIndex > 30)
                        {
                            self.spawnList[0].count++;
                        }
                        break;
                    case "InfiniteTowerWaveBossGhostHaunting(Clone)":
                        //Explicit Spawn waves that shouldn't have special scaling
                        bonusBonusDmgMulti = 0.5f;
                        if (waveIndex > 44)
                        {
                            self.spawnList[0].count++;
                        }
                        break;
                    case "InfiniteTowerWaveBrotherHaunt(Clone)":
                    case "InfiniteTowerWaveVagrantNovaDude(Clone)":
                        bonusBonusDmgMulti = 0.5f;
                        self.combatDirector.teamIndex = TeamIndex.Void;
                        if (waveIndex > 30)
                        {
                            self.spawnList[0].count++;
                        }
                        break;
                    case "InfiniteTowerWaveBossCharacters(Clone)":
                        bonusBonusHPMulti = 1.2f;
                        spawnAsVoidTeam = true;
                        if (waveIndex > 29)
                        {
                            self.spawnList[0].spawnCard.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Blackhole };
                            self.spawnList[0].spawnCard.itemsToGrant[0].count = 1;
                        }
                        else
                        {
                            self.spawnList[0].spawnCard.equipmentToGrant = new EquipmentDef[] { };
                            self.spawnList[0].spawnCard.itemsToGrant[0].count = 0;
                        }
                        break;
                    case "InfiniteTowerWaveSS2RainbowElites(Clone)":
                        bonusBonusHPMulti = -1f;
                        //20f * Run.instance.loopClearCount)
                        //4 * StageClear
                        //This method kinda fucking blows??
                        DirectorCard chosenMonsterCard = self.GetComponent<CombatDirector>().SelectMonsterCardForCombatShrine(waveIndex * 2 + 20);
                        if (chosenMonsterCard == null)
                        {
                            Debug.LogWarning("Could not find Emyprean candidate normally"); //This would only happen if too cheap right?
                            chosenMonsterCard = ClassicStageInfo.instance.monsterCategories.categories[2].cards[0];
                        }
                        self.spawnList[0].spawnCard.prefab = chosenMonsterCard.spawnCard.prefab;
                        self.spawnList[0].spawnCard.nodeGraphType = chosenMonsterCard.spawnCard.nodeGraphType;
                        break;
                }
                //Debug.Log(Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state);

                if (bonusBonusHPMulti > 0)
                {
                    float num = 1f;
                    float num2 = 1f;
                    num += Run.instance.difficultyCoefficient / 2.5f * System.Math.Max(1, (waveIndex / 10) * 0.225f + 0.225f);
                    num2 += Run.instance.difficultyCoefficient / 30f * System.Math.Max(1, (waveIndex / 10) * 0.04f);
                    num *= bonusBonusHPMulti;
                    num2 *= bonusBonusDmgMulti;

                    if (forcedSuperboss)
                    {
                        num /= (1 + ((Run.instance.participatingPlayerCount - 1) * 0.2f));
                        int num3 = Run.instance.participatingPlayerCount;
                        num *= Mathf.Pow((float)num3, 0.4f);
                    }
                    //num /= (1 + ((Run.instance.participatingPlayerCount - 1) * 0.25f));
                    //int num3 = Mathf.Max(1, Run.instance.livingPlayerCount);
                    //num *= Mathf.Pow((float)num3, 0.5f);
                    int grantHp = Mathf.RoundToInt((num - 1f) * 10f);
                    int grantDamage = Mathf.RoundToInt((num2 - 1f) * 10f);
                    if (grantHp > 100000) { grantHp = 100000; }
                    Debug.LogFormat(self.name + " Special Scaling: currentBoostHpCoefficient={0}, currentBoostDamageCoefficient={1}", new object[]
                    {
                           grantHp,
                           grantDamage
                    });


                    for (int list = 0; list < self.spawnList.Length; list++)
                    {
                        bool hasNoHP = true;
                        bool hasNoDmg = true;
                        bool hasNoTP = true;
                        for (int i = 0; i < self.spawnList[list].spawnCard.itemsToGrant.Length; i++)
                        {
                            if (self.spawnList[list].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.BoostHp)
                            {
                                hasNoHP = false;
                                self.spawnList[list].spawnCard.itemsToGrant[i].count = grantHp;
                            }
                            else if (self.spawnList[list].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.BoostDamage)
                            {
                                hasNoDmg = false;
                                self.spawnList[list].spawnCard.itemsToGrant[i].count = grantDamage;
                            }
                            else if (self.spawnList[list].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.TeleportWhenOob)
                            {
                                hasNoTP = false;
                            }
                        }
                        if (hasNoHP)
                        {
                            self.spawnList[list].spawnCard.itemsToGrant = self.spawnList[list].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = grantHp });
                        }
                        if (hasNoDmg)
                        {
                            self.spawnList[list].spawnCard.itemsToGrant = self.spawnList[list].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = grantDamage });
                        }
                        if (hasNoTP)
                        {
                            self.spawnList[list].spawnCard.itemsToGrant = self.spawnList[list].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 });
                        }

                        /*foreach (ItemCountPair itemPair in self.spawnList[0].spawnCard.itemsToGrant)
                        {
                            Debug.Log(itemPair.itemDef + "  " + itemPair.count);
                        }*/
                    }
                }

                bool addElite = false;
                if (addElite)
                {
                    //CombatDirector.eliteTiers[1].GetRandomAvailableEliteDef();
                    for (int i = 0; i < self.spawnList.Length; i++)
                    {
                        self.spawnList[i].eliteDef = RoR2Content.Elites.Fire;
                    }
                }

                if (spawnAsVoidTeam)
                {
                    self.combatDirector.teamIndex = TeamIndex.Void;

                    self.enemyInventory = enemyInventory;
                    CombatDirector combatDirector = self.combatDirector;
                    SpawnCard spawnCard = self.spawnList[0].spawnCard;
                    EliteDef eliteDef = self.spawnList[0].eliteDef;
                    Transform transform = spawnTargetObject.transform;
                    bool preventOverhead = self.spawnList[0].preventOverhead;
                    combatDirector.Spawn(spawnCard, eliteDef, transform, self.spawnList[0].spawnDistance, preventOverhead, 1f, DirectorPlacementRule.PlacementMode.Approximate);

                    self.combatDirector.teamIndex = TeamIndex.Monster;
                }
            }
            orig(self, waveIndex, enemyInventory, spawnTargetObject);
            self.combatDirector.teamIndex = TeamIndex.Monster;
        }

        private void MoreItems_AdvanceWave(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            if (self.enemyItemPatternIndex >= 5 && self.enemyItemPeriod > 5)
            {
                self.enemyItemPeriod /= 2;
            }
            else if (self.enemyItemPatternIndex >= 10 && self.enemyItemPeriod > 3)
            {
                self.enemyItemPeriod /= 2;
            }

            orig(self);

            if (WConfig.cfgDumpInfo.Value)
            {
                if (NetworkServer.active)
                {
                    if (self.waveIndex > self.enemyItemPeriod)
                    {
                        int ensure = self.waveIndex;
                        int itemCount = 0;
                        int fakeItemPeriod = 8;

                        while (ensure >= fakeItemPeriod)
                        {
                            ensure -= fakeItemPeriod;
                            itemCount++;
                            if (itemCount % 5 == 0)
                            {
                                if (fakeItemPeriod > 2)
                                {
                                    fakeItemPeriod /= 2;
                                }
                            }
                        }
                        Debug.Log("WaveIndex:" + self.waveIndex + " ExpectedItemCount " + itemCount);


                        while (self.enemyItemPatternIndex < itemCount)
                        {
                            InfiniteTowerRun.EnemyItemEntry[] array = self.enemyItemPattern;
                            int num = self.enemyItemPatternIndex;
                            self.enemyItemPatternIndex = num + 1;
                            InfiniteTowerRun.EnemyItemEntry enemyItemEntry = array[num % self.enemyItemPattern.Length];
                            if (!enemyItemEntry.dropTable)
                            {
                                return;
                            }
                            PickupIndex pickupIndex = enemyItemEntry.dropTable.GenerateDrop(self.enemyItemRng);
                            if (pickupIndex != PickupIndex.none)
                            {
                                PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
                                if (pickupDef != null)
                                {
                                    self.enemyInventory.GiveItem(pickupDef.itemIndex, enemyItemEntry.stacks);
                                    Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
                                    {
                                        baseToken = "INFINITETOWER_ADD_ITEM",
                                        pickupToken = pickupDef.nameToken,
                                        pickupColor = pickupDef.baseColor
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        internal static void MakePortal()
        {
            InteractableSpawnCard iscVoidOutroPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidOutroPortal/iscVoidOutroPortal.asset").WaitForCompletion();
            InteractableSpawnCard iscVoidPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/PortalVoid/iscVoidPortal.asset").WaitForCompletion();

            iscSimuExitPortal = Instantiate(iscVoidOutroPortal);
            iscSimuExitPortal.name = "iscSimuExitPortal";
            GameObject EndingPortal = R2API.PrefabAPI.InstantiateClone(iscSimuExitPortal.prefab, "SimulacrumExitPortal", true);
            iscSimuExitPortal.prefab = EndingPortal;

            EndingPortal.GetComponent<GenericDisplayNameProvider>().displayToken = "Simulated Exit Rift";
            EndingPortal.GetComponent<GenericInteraction>().contextToken = "End Simulation?";
            if (EndingPortal.GetComponent<GenericObjectiveProvider>())
            {
                EndingPortal.GetComponent<GenericObjectiveProvider>().objectiveToken = "Continue or end the <style=cIsVoid>Simulation</style>"; ;
            }
            else
            {
                EndingPortal.AddComponent<GenericObjectiveProvider>().objectiveToken = "Continue or end the <style=cIsVoid>Simulation</style>"; ;
            }

            Instantiate(iscVoidPortal.prefab.transform.GetChild(0).gameObject, EndingPortal.transform);
            //Guh 2??
            EndingPortal.transform.localScale = new Vector3(1.26f, 1.26f, 0.85f);
            EndingPortal.transform.GetChild(2).localPosition = new Vector3(0, 4f, 0);
            EndingPortal.transform.GetChild(2).GetChild(2).GetComponent<Light>().color = new Color(-8f, -16f, -8f);
            Destroy(EndingPortal.transform.GetChild(2).GetChild(2).GetComponent<LightIntensityCurve>());
            //0.8302 0.6461 0.8237 1
            EndingPortal.transform.GetChild(2).GetChild(4).localScale *= 0.5f;
            EndingPortal.transform.GetChild(2).GetChild(4).GetComponent<EntityLocator>().entity = EndingPortal;
            EndingPortal.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
            EndingPortal.transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
            Destroy(EndingPortal.transform.GetChild(1).gameObject);
            Destroy(EndingPortal.transform.GetChild(0).gameObject);
        }

        private void RadiusManipActive_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Active.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Active self)
        {
            orig(self);
            if (Run.instance && Run.instance is InfiniteTowerRun && (Run.instance as InfiniteTowerRun).waveInstance)
            {
                SimulacrumExtrasHelper temp = (Run.instance as InfiniteTowerRun).waveInstance.GetComponent<SimulacrumExtrasHelper>();
                if (temp && temp.newRadius > 0)
                {
                    temp.newRadius += -5 + Run.instance.participatingPlayerCount * 5;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", temp.newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = temp.newRadius;
                }
                else
                {
                    float newRadius = 60 + Run.instance.participatingPlayerCount * 5;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = newRadius;
                }
            }
        }

        public static void SimuGiveScavVoidItems(On.RoR2.ScavengerItemGranter.orig_Start orig, ScavengerItemGranter self)
        {
            orig(self);
            if (Run.instance is InfiniteTowerRun)
            {
                Inventory tempinv = self.GetComponent<Inventory>();

                PickupIndex pickupIndex = SimuMain.dtAISafeRandomVoid.GenerateDrop(Run.instance.treasureRng);
                ItemDef itemdef = ItemCatalog.GetItemDef(pickupIndex.pickupDef.itemIndex);

                if (itemdef.tier == ItemTier.VoidTier1)
                {
                    tempinv.GiveItem(itemdef, 3);
                    Debug.Log("Giving Simu Scav 3x " + itemdef);
                }
                else if (itemdef.tier == ItemTier.VoidTier2)
                {
                    tempinv.GiveItem(itemdef, 2);
                    Debug.Log("Giving Simu Scav 2x " + itemdef);
                }
                else
                {
                    tempinv.GiveItem(itemdef, 1);
                    Debug.Log("Giving Simu Scav 1x " + itemdef);
                }
            }
        }


        public static void LateRunningMethod()
        {
            WavesMain.LateChanges();
            RoR2Content.Items.BoostHp.hidden = true;
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
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactEnigma.prefab").WaitForCompletion().GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;

            InfiniteTowerRun InfiniteTowerRunBase = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            //This is where we'd need to add Fireworks
            //Fireworks is Interactable Related and that tagged is banned
            //InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Add(RoR2Content.Items.Squid); //But Squid Polyp wouldn't work they just die
            InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Remove(RoR2Content.Items.TitanGoldDuringTP, DLC1Content.Items.DroneWeapons); //But Squid Polyp wouldn't work they just die
            InfiniteTowerRunBase.blacklistedTags = InfiniteTowerRunBase.blacklistedTags.Remove(ItemTag.InteractableRelated); //There's only two and Fireworks works plenty

            if (WConfig.cfgItemsEvery8.Value)
            {
                InfiniteTowerRunBase.enemyItemPeriod = 8;
            }

            //Blacklist VanillaVoid Cornucopia, not realistically usable and essentially kills you
            ItemDef tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("VV_ITEM_CORNUCOPIACELL_ITEM"));
            if (tempDef != null)
            {
                InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Add(tempDef);
            }
            //There are no teleporters in Simu
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("FieldAccelerator"));
            if (tempDef != null)
            {
                InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Add(tempDef);
            }
            //SS2 Missing some tags
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("WatchMetronome"));
            if (tempDef != null)
            {
                tempDef.tags = tempDef.tags.Add(ItemTag.SprintRelated);
            }
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("PortableReactor"));
            if (tempDef != null)
            {
                tempDef.tags = tempDef.tags.Add(ItemTag.AIBlacklist);
            }
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("HuntersSigil"));
            if (tempDef != null)
            {
                tempDef.tags = tempDef.tags.Add(ItemTag.AIBlacklist);
            }
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("Fork"));
            if (tempDef != null)
            {
                tempDef.tags = tempDef.tags.Add(ItemTag.AIBlacklist);
            }
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("VV_ITEM_EHANCE_VIALS_ITEM"));
            if (tempDef != null)
            {
                tempDef.tags = tempDef.tags.Add(ItemTag.AIBlacklist);
            }

            RoR2Content.Items.MonstersOnShrineUse.tags = RoR2Content.Items.MonstersOnShrineUse.tags.Add(ItemTag.InteractableRelated);
            DLC1Content.Items.MoveSpeedOnKill.tags = DLC1Content.Items.MoveSpeedOnKill.tags.Add(ItemTag.OnKillEffect);

            RoR2Content.Items.ParentEgg.tags[0] = ItemTag.Healing;
            RoR2Content.Items.ShieldOnly.tags[0] = ItemTag.Healing;
            RoR2Content.Items.LunarUtilityReplacement.tags[0] = ItemTag.Healing;
            RoR2Content.Items.RandomDamageZone.tags[0] = ItemTag.Damage;
            DLC1Content.Items.HalfSpeedDoubleHealth.tags[0] = ItemTag.Healing;
            DLC1Content.Items.LunarSun.tags[0] = ItemTag.Damage;

            DLC1Content.Items.MinorConstructOnKill.tags = DLC1Content.Items.MinorConstructOnKill.tags.Add(ItemTag.Utility);
            RoR2Content.Items.Knurl.tags = RoR2Content.Items.Knurl.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.Pearl.tags = RoR2Content.Items.Pearl.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.Pearl.tags = RoR2Content.Items.Pearl.tags.Add(ItemTag.Healing);

            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.HeadHunter.tags = RoR2Content.Items.HeadHunter.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnKill.tags = RoR2Content.Items.BarrierOnKill.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnOverHeal.tags = RoR2Content.Items.BarrierOnOverHeal.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.FallBoots.tags = RoR2Content.Items.FallBoots.tags.Remove(ItemTag.Damage);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.Healing);

            //RoR2Content.Items.PersonalShield.tags = RoR2Content.Items.PersonalShield.tags.Add(ItemTag.Healing);
            DLC1Content.Items.ImmuneToDebuff.tags = DLC1Content.Items.ImmuneToDebuff.tags.Add(ItemTag.Healing);

            RoR2Content.Items.BonusGoldPackOnKill.tags = RoR2Content.Items.BonusGoldPackOnKill.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.GoldOnHit.tags = RoR2Content.Items.GoldOnHit.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.RegeneratingScrap.tags = DLC1Content.Items.RegeneratingScrap.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.ShockNearby.tags = RoR2Content.Items.ShockNearby.tags.Add(ItemTag.Count);
            DLC1Content.Items.MoreMissile.tags = DLC1Content.Items.MoreMissile.tags.Add(ItemTag.Count);
            DLC1Content.Items.CritDamage.tags = DLC1Content.Items.CritDamage.tags.Add(ItemTag.Count);
            DLC1Content.Items.DroneWeapons.tags = DLC1Content.Items.DroneWeapons.tags.Add(ItemTag.AIBlacklist);
            //RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Add(ItemTag.AIBlacklist);

            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.SprintRelated);
            DLC1Content.Items.ElementalRingVoid.tags = DLC1Content.Items.ElementalRingVoid.tags.Remove(ItemTag.Utility);

            DLC1Content.Items.TreasureCacheVoid.tags = DLC1Content.Items.TreasureCacheVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.CritGlassesVoid.tags = DLC1Content.Items.CritGlassesVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.EquipmentMagazineVoid.tags = DLC1Content.Items.EquipmentMagazineVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.ExplodeOnDeathVoid.tags = DLC1Content.Items.ExplodeOnDeathVoid.tags.Add(ItemTag.AIBlacklist);

            if (WConfig.cfgDumpInfo.Value)
            {
                DumpAllWaveInfo(ITBasicWaves);
                DumpAllWaveInfo(ITBossWaves);
                DumpAllWaveInfo(ITSuperBossWaves);
                DumpAllWaveInfo(ITModSupportWaves);
            }
        }

        public static void SetupConstants()
        {
            ItemDef AACannon = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AACannon");
            ITDamageDown.name = "ITDamageDown";
            ITDamageDown.deprecatedTier = ItemTier.NoTier;
            ITDamageDown._itemTierDef = AACannon._itemTierDef;
            ITDamageDown.nameToken = "ITDamageDown";
            ITDamageDown.pickupToken = "ITDamageDown";
            ITDamageDown.descriptionToken = "ITDamageDown";
            ITDamageDown.hidden = true;
            ITDamageDown.canRemove = false;
            ITDamageDown.pickupIconSprite = AACannon.pickupIconSprite;
            ITDamageDown.pickupModelPrefab = AACannon.pickupModelPrefab;

            CustomItem customItem = new CustomItem(ITDamageDown, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);

            ITAttackSpeedDown.name = "ITAttackSpeedDown";
            ITAttackSpeedDown.deprecatedTier = ItemTier.NoTier;
            ITAttackSpeedDown._itemTierDef = AACannon._itemTierDef;
            ITAttackSpeedDown.nameToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.pickupToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.descriptionToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.hidden = true;
            ITAttackSpeedDown.canRemove = false;
            ITAttackSpeedDown.pickupIconSprite = AACannon.pickupIconSprite;
            ITAttackSpeedDown.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITAttackSpeedDown, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITHealthScaling.name = "ITHealthScaling";
            ITHealthScaling.deprecatedTier = ItemTier.NoTier;
            ITHealthScaling._itemTierDef = AACannon._itemTierDef;
            ITHealthScaling.nameToken = "ITHealthScaling";
            ITHealthScaling.pickupToken = "ITHealthScaling";
            ITHealthScaling.descriptionToken = "ITHealthScaling";
            ITHealthScaling.hidden = true;
            ITHealthScaling.canRemove = false;
            ITHealthScaling.pickupIconSprite = AACannon.pickupIconSprite;
            ITHealthScaling.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITHealthScaling, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITKillOnCompletion.name = "ITKillOnCompletion";
            ITKillOnCompletion.deprecatedTier = ItemTier.NoTier;
            ITKillOnCompletion._itemTierDef = AACannon._itemTierDef;
            ITKillOnCompletion.nameToken = "ITKillOnCompletion";
            ITKillOnCompletion.pickupToken = "ITKillOnCompletion";
            ITKillOnCompletion.descriptionToken = "ITKillOnCompletion";
            ITKillOnCompletion.hidden = true;
            ITKillOnCompletion.canRemove = false;
            ITKillOnCompletion.pickupIconSprite = AACannon.pickupIconSprite;
            ITKillOnCompletion.pickupModelPrefab = AACannon.pickupModelPrefab;

            customItem = new CustomItem(ITKillOnCompletion, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITHorrorName.name = "ITHorrorName";
            ITHorrorName.deprecatedTier = ItemTier.NoTier;
            ITHorrorName._itemTierDef = AACannon._itemTierDef;
            ITHorrorName.nameToken = "ITHorrorName";
            ITHorrorName.pickupToken = "ITHorrorName";
            ITHorrorName.descriptionToken = "ITHorrorName";
            ITHorrorName.hidden = true;
            ITHorrorName.canRemove = false;
            ITHorrorName.pickupIconSprite = AACannon.pickupIconSprite;
            ITHorrorName.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITHorrorName, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            On.RoR2.Util.GetBestBodyName += (orig, bodyObject) =>
            {
                if (bodyObject)
                {
                    CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                    if (characterBody && characterBody.inventory)
                    {
                        if (characterBody.inventory.GetItemCount(ITHorrorName) > 0)
                        {
                            return "Unknown Horror";
                        }
                    }
                }
                return orig(bodyObject);
            };

            ITCooldownUp.name = "ITCooldownUp";
            ITCooldownUp.deprecatedTier = ItemTier.NoTier;
            ITCooldownUp._itemTierDef = AACannon._itemTierDef;
            ITCooldownUp.nameToken = "ITCooldownUp";
            ITCooldownUp.pickupToken = "ITCooldownUp";
            ITCooldownUp.descriptionToken = "10% longer cooldown";
            ITCooldownUp.hidden = true;
            ITCooldownUp.canRemove = false;
            ITCooldownUp.pickupIconSprite = AACannon.pickupIconSprite;
            ITCooldownUp.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITCooldownUp, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);

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

            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/OptionPickup/OptionPickerPanel.prefab").WaitForCompletion().GetComponent<RoR2.UI.PickupPickerPanel>().maxColumnCount = 3;

            //Idk if this is hella overkill or not
            R2API.ContentAddition.AddItemTierDef(ItemOrangeTierDef);

            //Config Vals
            SimuEndingEveryXWaves = WConfig.cfgSimuEndingEveryXWaves.Value;
            SimuEndingStartAtXWaves = WConfig.cfgSimuEndingStartAtXWaves.Value;
            SimuEndingWaveRest = WConfig.cfgSimuEndingStartAtXWaves.Value % WConfig.cfgSimuEndingEveryXWaves.Value;

            SimuForcedBossEveryXWaves = WConfig.cfgSuperBossEveryXWaves.Value;
            SimuForcedBossStartAtXWaves = WConfig.cfgSuperBossStartAtXWaves.Value;
            SimuForcedBossWaveRest = WConfig.cfgSuperBossStartAtXWaves.Value % WConfig.cfgSuperBossEveryXWaves.Value;

            //Tags
            BasicPickupDropTable dtMonsterTeamTier1Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier1Item.asset").WaitForCompletion();
            BasicPickupDropTable dtMonsterTeamTier2Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier2Item.asset").WaitForCompletion();
            BasicPickupDropTable dtMonsterTeamTier3Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/MonsterTeamGainsItems/dtMonsterTeamTier3Item.asset").WaitForCompletion();

            ArenaMonsterItemDropTable dtArenaMonsterTier1 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier1.asset").WaitForCompletion();
            ArenaMonsterItemDropTable dtArenaMonsterTier2 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier2.asset").WaitForCompletion();
            ArenaMonsterItemDropTable dtArenaMonsterTier3 = Addressables.LoadAssetAsync<ArenaMonsterItemDropTable>(key: "RoR2/Base/arena/dtArenaMonsterTier3.asset").WaitForCompletion();

            BasicPickupDropTable dtAISafeTier1Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier1Item.asset").WaitForCompletion();
            BasicPickupDropTable dtAISafeTier2Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier2Item.asset").WaitForCompletion();
            BasicPickupDropTable dtAISafeTier3Item = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/Common/dtAISafeTier3Item.asset").WaitForCompletion();


            ItemTag[] TagsAISafe = { ItemTag.AIBlacklist, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.HoldoutZoneRelated, ItemTag.OnStageBeginEffect };
            ItemTag[] TagsMonsterTeamGain = { ItemTag.AIBlacklist, ItemTag.CannotCopy, ItemTag.OnKillEffect, ItemTag.EquipmentRelated, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.OnStageBeginEffect, ItemTag.HoldoutZoneRelated, ItemTag.Count };

            dtMonsterTeamTier1Item.bannedItemTags = TagsMonsterTeamGain;
            dtMonsterTeamTier2Item.bannedItemTags = TagsMonsterTeamGain;
            dtMonsterTeamTier3Item.bannedItemTags = TagsMonsterTeamGain;

            dtArenaMonsterTier1.bannedItemTags = TagsMonsterTeamGain;
            dtArenaMonsterTier2.bannedItemTags = TagsMonsterTeamGain;
            dtArenaMonsterTier3.bannedItemTags = TagsMonsterTeamGain;

            dtAISafeTier1Item.bannedItemTags = TagsAISafe;
            dtAISafeTier2Item.bannedItemTags = TagsAISafe;
            dtAISafeTier3Item.bannedItemTags = TagsAISafe;

            InfiniteTowerRun InfiniteTowerRunBase = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            BasicPickupDropTable dtITEnemyTier1 = Instantiate(dtMonsterTeamTier1Item);
            BasicPickupDropTable dtITEnemyTier2 = Instantiate(dtMonsterTeamTier2Item);
            BasicPickupDropTable dtITEnemyTier3 = Instantiate(dtMonsterTeamTier3Item);

            dtITEnemyTier1.name = "dtITEnemyTier1";
            dtITEnemyTier2.name = "dtITEnemyTier2";
            dtITEnemyTier3.name = "dtITEnemyTier3";

            dtITEnemyTier1.voidTier1Weight = 0.1f;
            dtITEnemyTier2.voidTier2Weight = 0.075f;
            dtITEnemyTier3.voidTier3Weight = 0.075f;

            InfiniteTowerRunBase.enemyItemPattern[0].dropTable = dtITEnemyTier1;
            InfiniteTowerRunBase.enemyItemPattern[1].dropTable = dtITEnemyTier1;
            InfiniteTowerRunBase.enemyItemPattern[2].dropTable = dtITEnemyTier2;
            InfiniteTowerRunBase.enemyItemPattern[3].dropTable = dtITEnemyTier2;
            InfiniteTowerRunBase.enemyItemPattern[4].dropTable = dtITEnemyTier3;

            //Better Blacklist since I assume in Vanilla the Blacklist still sucks ass and gives On Kill items 
            //For Scavs
            dtAISafeRandomVoid.tier1Weight = 0;
            dtAISafeRandomVoid.tier2Weight = 0;
            dtAISafeRandomVoid.tier3Weight = 0;
            dtAISafeRandomVoid.voidTier1Weight = 6;
            dtAISafeRandomVoid.voidTier2Weight = 3;
            dtAISafeRandomVoid.voidTier3Weight = 1;
            dtAISafeRandomVoid.voidBossWeight = 0.5f; //Friendly Void Reaver would just suck him up and kill him
            dtAISafeRandomVoid.canDropBeReplaced = false;
            dtAISafeRandomVoid.name = "dtAISafeRandomVoid";
            dtAISafeRandomVoid.bannedItemTags = TagsAISafe;


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
            //

            //PreRequs
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

            //
            //Drop Pools
            //The Guaranteed Red
            dtITWaveTier3.tier3Weight = 90;
            dtITWaveTier3.bossWeight = 10;

            //Vanilla Void Boss Drop Table is kinda bad
            dtITVoid.voidTier1Weight = 60;
            dtITVoid.voidTier2Weight = 30;
            dtITVoid.voidTier3Weight = 10;
            dtITVoid.voidBossWeight = 5;

            //Wacky Tier for Wacky Artifacts
            dtAllTier.name = "dtAllTier";
            dtAllTier.tier1Weight = 120;
            dtAllTier.tier2Weight = 20;
            dtAllTier.tier3Weight = 2;
            dtAllTier.bossWeight = 5;
            dtAllTier.equipmentWeight = 15;
            dtAllTier.lunarItemWeight = 8;
            dtAllTier.voidTier1Weight = 60;
            dtAllTier.voidTier2Weight = 30;
            dtAllTier.voidTier3Weight = 3;
            dtAllTier.voidBossWeight = 1;
            dtAllTier.lunarEquipmentWeight = 1;
            //dtAllTier.eliteEquipWeight = 1;
            //dtAllTier.pearlWeight = 10;

            if (WConfig.cfgVoidTripleAllTier.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = dtAllTier;
            }



            //OnKill for On Kill Artifacts
            dtITBasicWaveOnKill.tier1Weight = 80f;
            dtITBasicWaveOnKill.tier2Weight = 8f;
            dtITBasicWaveOnKill.tier3Weight = 0.75f;
            dtITBasicWaveOnKill.bossWeight = 0f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";
            dtITBasicWaveOnKill.requiredItemTags = new ItemTag[] { ItemTag.OnKillEffect };
            //
            //For Basic waves intended to be difficult
            dtITBasicBonusGreen.tier1Weight = 20f;
            dtITBasicBonusGreen.tier2Weight = 80f;
            dtITBasicBonusGreen.tier3Weight = 1f;
            dtITBasicBonusGreen.bossWeight = 0f;
            dtITBasicBonusGreen.bannedItemTags = new ItemTag[] { };
            dtITBasicBonusGreen.name = "dtITBasicBonusGreen";

            //For Boss waves intended to be difficult
            dtITBossBonusRed.tier1Weight = 0f;
            dtITBossBonusRed.tier2Weight = 20f; //80 default
            dtITBossBonusRed.tier3Weight = 10f; //7.5 default
            dtITBossBonusRed.bossWeight = 0f; //7.5 default
            dtITBossBonusRed.bannedItemTags = new ItemTag[] { };
            dtITBossBonusRed.name = "dtITBossBonusRed";

            dtITBossGreenVoid.name = "dtITBossGreenVoid";
            dtITBossGreenVoid.tier1Weight = 0;
            dtITBossGreenVoid.tier2Weight = 80;
            dtITBossGreenVoid.tier3Weight = 7.5f;
            dtITBossGreenVoid.bossWeight = 7.5f;
            dtITBossGreenVoid.voidTier1Weight = 50;
            dtITBossGreenVoid.voidTier2Weight = 50;
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
            dtITFamilyWaveDamage.tier2Weight = 10; //Move 4 Points to boss
            dtITFamilyWaveDamage.tier3Weight = 0.25f;
            dtITFamilyWaveDamage.bossWeight = 5f;
            dtITFamilyWaveDamage.name = "dtITFamilyWaveDamage";
            dtITFamilyWaveDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITFamilyWaveHealing.tier1Weight = 80;
            dtITFamilyWaveHealing.tier2Weight = 11;
            dtITFamilyWaveHealing.tier3Weight = 0.3f;
            dtITFamilyWaveHealing.bossWeight = 5f;
            dtITFamilyWaveHealing.name = "dtITFamilyWaveHealing";
            dtITFamilyWaveHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITFamilyWaveUtility.tier1Weight = 80;
            dtITFamilyWaveUtility.tier2Weight = 11;
            dtITFamilyWaveUtility.tier3Weight = 0.3f;
            dtITFamilyWaveUtility.bossWeight = 5f;
            dtITFamilyWaveUtility.name = "dtITFamilyWaveUtility";
            dtITFamilyWaveUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };
            //
            //Family Waves biased 
            dtITBossCategoryDamage.tier1Weight = 0;
            dtITBossCategoryDamage.tier2Weight = 80f;
            dtITBossCategoryDamage.tier3Weight = 20f;
            dtITBossCategoryDamage.bossWeight = 20f;
            dtITBossCategoryDamage.name = "dtITBossCategoryDamage";
            dtITBossCategoryDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITBossCategoryHealing.tier1Weight = 0;
            dtITBossCategoryHealing.tier2Weight = 80f;
            dtITBossCategoryHealing.tier3Weight = 25f;
            dtITBossCategoryHealing.bossWeight = 25f;
            dtITBossCategoryHealing.name = "dtITBossCategoryHealing";
            dtITBossCategoryHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITBossCategoryUtility.tier1Weight = 0;
            dtITBossCategoryUtility.tier2Weight = 80f;
            dtITBossCategoryUtility.tier3Weight = 20f;
            dtITBossCategoryUtility.bossWeight = 20f;
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

            DirectorCardCategorySelection dccsITVoidMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsITVoidMonsters.asset").WaitForCompletion();
            dccsITVoidMonsters.categories[1].selectionWeight = 2;
            dccsITVoidMonsters.categories[2].cards[0].selectionWeight = 2;
        }

        public static void SimuChanges()
        {
            SceneDef itgolemplains = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgolemplains/itgolemplains.asset").WaitForCompletion();
            SceneDef itgoolake = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgoolake/itgoolake.asset").WaitForCompletion();
            SceneDef itfrozenwall = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
            SceneDef itdampcave = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();
            SceneDef itmoon = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itmoon/itmoon.asset").WaitForCompletion();

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();

            MusicTrackDef MTDSulfurBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();
            MusicTrackDef MTDShipgraveyardBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/muSong22.asset").WaitForCompletion();
            MusicTrackDef MTDPrelude = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muMenuDLC1.asset").WaitForCompletion();

            itgolemplains.mainTrack = MusicVoidFields;
            itfrozenwall.mainTrack = MusicSnowyForest;
            itdampcave.mainTrack = MusicVoidStage;

            //GP : Thermodynamic Equilibrium 
            itgoolake.bossTrack = MTDSulfurBoss; //A Boat Made from a Sheet of Newspaper
            //AL //Having Fallen, It Was Blood 
            itfrozenwall.bossTrack = MTDShipgraveyardBoss; //Köppen As Fuck 
            //Damp //Hydrophobia 
            //Sky //Antarctic Oscillation
            itmoon.bossTrack = MTDPrelude;


            ITSuperBossWaves.name = "SuperBossWaves";
            ITSuperBossWaves.availabilityPeriod = 30;
            ITSuperBossWaves.minWaveIndex = 50;

            ITModSupportWaves.name = "ITModSupportWaves";
            ITModSupportWaves.availabilityPeriod = 999;
            ITModSupportWaves.minWaveIndex = 998;


            float ITSpecialBossWaveWeight = 2.5f;
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                switch (ITBasicWaves.wavePrefabs[i].wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactEnigma":
                        //Orange tier set later
                        ITBasicWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
                        break;
                    case "InfiniteTowerWaveArtifactCommand":
                        ITBasicWaves.wavePrefabs[i].weight = 1.5f;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        ITBasicWaves.wavePrefabs[i].weight = 3.5f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 184;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        ITBasicWaves.wavePrefabs[i].weight = 4f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                };
            }


            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your equipment changes every time it's activated.";
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactSingleMonsterTypeWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters will be of only one type.";

            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();

            ITBasicWaves.wavePrefabs[0].weight = 100;
            ITBossWaves.wavePrefabs[0].weight = 90;

            if (SimuMain.ITBossWaves.wavePrefabs.Length < 3)
            {
                GameObject InfiniteTowerWaveBossLunar = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossLunar.prefab").WaitForCompletion();
                GameObject InfiniteTowerWaveBossVoid = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossVoid.prefab").WaitForCompletion();
                GameObject InfiniteTowerWaveBossBrother = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossBrother.prefab").WaitForCompletion();

                InfiniteTowerWaveCategory.WeightedWave WaveBossLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossLunar, weight = 10 };
                InfiniteTowerWaveCategory.WeightedWave WaveBossVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoid, weight = 6 };
                InfiniteTowerWaveCategory.WeightedWave WaveBossBrother = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossBrother, weight = 2 };
                SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(WaveBossLunar, WaveBossVoid, WaveBossBrother);
            }

            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossVoid"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 6f;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossLunar"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 5f;
                    CombatDirector temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.CombatDirector>();
                    temp.monsterCards = dccsLunarFamily;
                    temp.skipSpawnIfTooCheap = false;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;

                    ITBossWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossScav"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 6f;
                    ITBossWaves.wavePrefabs[i].prerequisites = StartWave25Prerequisite;
                    ITSuperBossWaves.wavePrefabs = ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossBrother"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight;
                    ITBossWaves.wavePrefabs[i].prerequisites = StartWave35Prerequisite;
                    ITSuperBossWaves.wavePrefabs = ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
            }


            //Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion()
        }


        public static void DumpAllWaveInfo(InfiniteTowerWaveCategory category)
        {
            Debug.Log("");
            Debug.Log("All Simulacrum Waves : " + category.name);
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                Debug.Log(Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "  ");
                Debug.Log(Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token) + "  ");
            }
            Debug.Log("");
            Debug.Log("");
            Debug.Log("All Simulacrum Waves : " + category.name);
            float totalWeight = 0;
            float totalWeightPre11 = 0;
            float totalWeightPre30 = 0;
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                totalWeight += category.wavePrefabs[i].weight;
                Debug.Log("[" + i + "] " + category.wavePrefabs[i].wavePrefab.name + "  ");
                Debug.Log(Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "  ");
                Debug.Log(Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token) + "  ");
                Debug.Log("Weight: " + category.wavePrefabs[i].weight + "  ");
                string tokenCredit = "Credits: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave > 0)
                {
                    tokenCredit += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave + " * Wave";
                }
                tokenCredit += "  ";
                Debug.Log(tokenCredit);

                Debug.Log("Immediate: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction);
                Debug.Log("WaveSeconds: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds);

                
                string tokenDropTable = "DropTable: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable.name;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>() && category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable)
                {
                    tokenDropTable += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable.name;
                }
                tokenDropTable += "  ";
                Debug.Log(tokenDropTable);

                string token4 = "Prerequesites: ";
                if (category.wavePrefabs[i].prerequisites)
                {
                    token4 += category.wavePrefabs[i].prerequisites.name;
                }

                if (category.wavePrefabs[i].prerequisites is InfiniteTowerWaveCountPrerequisites && (category.wavePrefabs[i].prerequisites as InfiniteTowerWaveCountPrerequisites).minimumWaveCount < 11)
                {
                    totalWeightPre11 += category.wavePrefabs[i].weight;

                }
                if (category.wavePrefabs[i].prerequisites is InfiniteTowerWaveCountPrerequisites && (category.wavePrefabs[i].prerequisites as InfiniteTowerWaveCountPrerequisites).minimumWaveCount < 30)
                {
                    totalWeightPre30 += category.wavePrefabs[i].weight;
                }
                if (category.wavePrefabs[i].prerequisites == null || category.wavePrefabs[i].prerequisites is InfiniteTowerWaveArtifactPrerequisites)
                {
                    totalWeightPre11 += category.wavePrefabs[i].weight;
                    totalWeightPre30 += category.wavePrefabs[i].weight;
                }
                token4 += "  ";
                Debug.Log(token4);

                if (category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards != null)
                {
                    Debug.Log("MonsterCards: " + category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards.name + "  ");
                }
                Debug.Log("");
            }


            float defaultWeight = category.wavePrefabs[0].weight;

            Debug.Log("Total " + category.name + " Weight  pre 10: " + totalWeightPre11 +
             "  Extra Weight: " + (totalWeightPre11 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre11);

            Debug.Log("Total " + category.name + " Weight  pre 30: " + totalWeightPre30 +
             "  Extra Weight: " + (totalWeightPre30 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre30);

            defaultWeight *= 0.5f;
            totalWeight -= defaultWeight;

            Debug.Log("Total " + category.name + " Weight post 30: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);

            totalWeight -= defaultWeight * 0.8f;
            defaultWeight *= 0.2f;

            Debug.Log("Total " + category.name + " Weight post 50: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);
        }


        public static GameObject InfiniteTowerWaveCategory_SelectWavePrefab(On.RoR2.InfiniteTowerWaveCategory.orig_SelectWavePrefab orig, InfiniteTowerWaveCategory self, InfiniteTowerRun run, Xoroshiro128Plus rng)
        {
            GameObject temp = orig(self, run, rng);
            Debug.Log(run.waveIndex + " SelectWavePrefab  " + temp);

            //Debug.LogWarning(run.waveIndex % 50);
            if (run.waveIndex >= SimuForcedBossStartAtXWaves && run.waveIndex % SimuForcedBossEveryXWaves == SimuForcedBossWaveRest)
            {
                ITSuperBossWaves.GenerateWeightedSelection();
                temp = ITSuperBossWaves.weightedSelection.Evaluate(rng.nextNormalizedFloat);
                Debug.Log("Forcing SuperBoss");
            }  
            return temp;
        }

        public static void InfiniteTowerRun_BeginNextWave(On.RoR2.InfiniteTowerRun.orig_InitializeWaveController orig, global::RoR2.InfiniteTowerRun self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                GoldTitanManager.TryStartChannelingTitansServer(self.safeWardController.gameObject, self.safeWardController.gameObject.transform.position, null, null);
            }

            CombatDirector combatDirector = self.waveInstance.GetComponent<CombatDirector>();
            InfiniteTowerWaveController waveController = self.waveInstance.GetComponent<InfiniteTowerWaveController>();
            //Radius
            SimulacrumExtrasHelper radiusManip = self.waveInstance.GetComponent<SimulacrumExtrasHelper>();

            float newRadius = 60;

            if (radiusManip && radiusManip.newRadius > 0)
            {
                newRadius = radiusManip.newRadius -5 + Run.instance.participatingPlayerCount * 5;

            }
            else
            {
                newRadius += Run.instance.participatingPlayerCount * 5;
                if (waveController.isBossWave)
                {
                    newRadius += 5;
                }
            }

            self.safeWardController.wardStateMachine.state.SetFieldValue("radius", newRadius);
            self.safeWardController.holdoutZoneController.baseRadius = newRadius;
            combatDirector.minSpawnRange = 20;
            combatDirector.maxSpawnDistance = newRadius;


            //

            if (self._waveController)
            {
                switch (self.waveInstance.name)
                {
                    case "InfiniteTowerWaveBossArtifactDoppelganger(Clone)":
                        if (NetworkServer.active)
                        {
                            RoR2.Artifacts.DoppelgangerInvasionManager.PerformInvasion(self.bossRewardRng);

                            CombatSquad WaveSquad = self.waveInstance.GetComponent<CombatSquad>();
                            CombatSquad[] bossgrouplist2 = UnityEngine.Object.FindObjectsOfType(typeof(CombatSquad)) as CombatSquad[];
                            for (var i = 0; i < bossgrouplist2.Length; i++)
                            {
                                //Debug.LogWarning(bossgrouplist2[i]);
                                if (bossgrouplist2[i].name.Equals("ShadowCloneEncounter(Clone)") || bossgrouplist2[i].name.Equals("ShadowCloneEncounterAltered"))
                                {
                                    foreach (CharacterMaster charactermaster in bossgrouplist2[i].membersList)
                                    {
                                        WaveSquad.AddMember(charactermaster);
                                        //Seems to cause an error about adding items tho idk what
                                    }
                                }
                            }
                        }
                        break;
                    case "InfiniteTowerWaveBossBrother(Clone)":
                        self.waveInstance.AddComponent<PhaseCounter>().phase = 3;
                        break;
                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        self.waveInstance.GetComponents<CombatDirector>()[1].monsterCredit *= System.Math.Max(0.9f, ((self.waveIndex / 10) + 1f) * 0.24f);
                        break;
                    case "InfiniteTowerWaveBossScav(Clone)":
                        break;
                    case "InfiniteTowerWaveHeresy(Clone)":
                        if (NetworkServer.active)
                        {
                            int decide = WRect.random.Next(0, 2);
                            if (decide == 0)
                            {
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarPrimaryReplacement);
                                self.enemyInventory.GiveItem(ITDamageDown, 50); //Later waves will have too much damage anyways
                                self.enemyInventory.GiveItem(ITAttackSpeedDown, 70);
                                self.enemyInventory.GiveItem(ITCooldownUp, 10);
                                //self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, Run.instance.stageClearCount * 2);
                                self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, self.waveIndex / 10 * 2);
                                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                                {
                                    //baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Visions of Heresy</color> has been integrated into the Simulacrum...!</style>",
                                    baseToken = "<style=cWorldEvent>[WARNING] Running test with <color=#307FFF>Visions of Heresy</color></style>",
                                });
                            }
                            else
                            {
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarSecondaryReplacement);
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarUtilityReplacement);
                                self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, self.waveIndex / 10 * 2);
                                self.enemyInventory.GiveItem(ITDamageDown, 5);
                                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                                {
                                    //baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Hooks and Strides of Heresy</color> have been integrated into the Simulacrum...!</style>",
                                    baseToken = "<style=cWorldEvent>[WARNING] Running test with <color=#307FFF>Hooks and Strides of Heresy</color></style>",
                                });
                            }
                        }
                        break;
                    case "InfiniteTowerWaveManyItems(Clone)":
                        if (NetworkServer.active)
                        {
                            for (int i = 0; i < self.enemyInventory.itemAcquisitionOrder.Count; i++)
                            {
                                ItemDef tempDef = ItemCatalog.GetItemDef(self.enemyInventory.itemAcquisitionOrder[i]);
                                if (tempDef == ITHealthScaling || tempDef == RoR2Content.Items.Bear || tempDef == RoR2Content.Items.ExtraLife || tempDef == DLC1Content.Items.ExtraLifeVoid)
                                {
                                    self.enemyInventory.GiveItem(tempDef, 1);
                                }
                                else
                                {
                                    self.enemyInventory.GiveItem(tempDef, self.enemyInventory.GetItemCount(self.enemyInventory.itemAcquisitionOrder[i]) * 4);
                                }
                            }
                        }
                        break;
                }

                if (NetworkServer.active)
                {
                    if (WConfig.cfgSimuCreditsRebalance.Value)
                    {
                        self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= self.participatingPlayerCount; //Keep in mind cost doesn't scale money just gets divided by player count
                        self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= Mathf.Max(0.75f, 1.5f - 0.5f * (self.waveIndex / 20));
                    }
                    if (self.waveIndex % 5 == 0)
                    {
                        //Warbanner on Boss Wave  
                        for (int j = 0; j < PlayerCharacterMasterController.instances.Count; j++)
                        {
                            CharacterBody body = PlayerCharacterMasterController.instances[j].body;
                            if (body)
                            {
                                CharacterMaster master = PlayerCharacterMasterController.instances[j].master;
                                if (master)
                                {
                                    int itemCount = master.inventory.GetItemCount(RoR2Content.Items.WardOnLevel);
                                    if (itemCount > 0)
                                    {
                                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard"), body.transform.position, Quaternion.identity);
                                        gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                                        gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * (float)itemCount;
                                        NetworkServer.Spawn(gameObject);
                                    }
                                }
                            }
                        }

                    }
                }
            }
            //
            self.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
            if (self.waveIndex < 11)
            {
                if (self.waveIndex == 5)
                {
                    waveController.baseCredits -= 100;
                    waveController.immediateCreditsFraction -= 0.1f;
                    waveController.wavePeriodSeconds += 5;
                }
                if (self.waveIndex == 10)
                {
                    waveController.baseCredits -= 50;
                }
            }
            

            if (WConfig.cfgExtraDifficuly.Value)
            {
                combatDirector.eliteBias = Mathf.Min(combatDirector.eliteBias * 0.5f * (1 - (self.waveIndex - 1 - 60f) / 60f), combatDirector.eliteBias); //Why is it 1.5f in Simu anyways
                combatDirector.eliteBias = Mathf.Max(combatDirector.eliteBias, 0.1f);

                float creditsMulti = 1f + ((self.waveIndex - 1) * 2f / 100f);
                if (creditsMulti > 3)
                {
                    creditsMulti = 3;
                }
                waveController.immediateCreditsFraction *= creditsMulti;

            }
            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                if (self.waveIndex % 5 == 0)
                {
                    if (WConfig.cfgAwaitTravel.Value)
                    {
                        waveController.secondsAfterWave = 0;
                    }
                    else
                    {
                        int players = Run.instance.participatingPlayerCount;
                        if (players > 1)
                        {
                            waveController.secondsAfterWave += 1 + players;
                        }
                        else
                        {
                            waveController.secondsAfterWave += 1;
                        }
                    }

                }
                else
                {
                    //10,9,8,7
                    waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 10, 5);
                }
                if (self.waveIndex < 11)
                {
                    waveController.maxSquadSize = 15;
                }
                else if (self.waveIndex > 20)
                {
                    waveController.wavePeriodSeconds *= 0.85f;
                }
                else if (self.waveIndex > 40)
                {
                    waveController.wavePeriodSeconds *= 0.75f;
                }
                else if (self.waveIndex > 60)
                {
                    waveController.wavePeriodSeconds *= 0.6f;
                }
            }
            Debug.Log(self.waveIndex + " immediateCreditsFraction: " + waveController.immediateCreditsFraction + " eliteBias: " + combatDirector.eliteBias + " wavePeriodSeconds: " + waveController.wavePeriodSeconds);

        }


        public static void InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerRun.orig_OnWaveAllEnemiesDefeatedServer orig, InfiniteTowerRun self, InfiniteTowerWaveController wc)
        {
            orig(self, wc);
            if (self.IsStageTransitionWave())
            {
                //Debug.Log("\nPreviousSceneDef " + PreviousSceneDef + "\n" + "CurrentSceneDef " + Stage.instance.sceneDef + "\n" + "NextSceneDef " + self.nextStageScene);
                if (PreviousSceneDef != null && PreviousSceneDef == self.nextStageScene)
                {
                    int preventInfiniteLoop = 0;
                    //Debug.Log("Preventing repeat scene");
                    do
                    {
                        preventInfiniteLoop++;
                        self.PickNextStageSceneFromCurrentSceneDestinations();
                        //Debug.Log("ReplacementSceneDef " + self.nextStageScene);
                    }
                    while (self.nextStageScene == PreviousSceneDef && preventInfiniteLoop < 10);
                }
                PreviousSceneDef = Stage.instance.sceneDef;
            }
        }

        public static void InfiniteTowerWaveController_OnAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerWaveController.orig_OnAllEnemiesDefeatedServer orig, InfiniteTowerWaveController self)
        {
            orig(self);

            CombatDirector[] combatDirectors = self.gameObject.GetComponents<CombatDirector>();
            if (combatDirectors.Length > 1)
            {
                self.gameObject.GetComponents<CombatDirector>()[0].enabled = false;
                self.gameObject.GetComponents<CombatDirector>()[1].enabled = false;
            }
            SimulacrumInteractablesWaveHelper simulacrumInteractablesWaveHelper = self.gameObject.GetComponent<SimulacrumInteractablesWaveHelper>();
            if (simulacrumInteractablesWaveHelper)
            {
                simulacrumInteractablesWaveHelper.enabled = false;
            }

            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveIndex >= SimuEndingStartAtXWaves && run.waveIndex % SimuEndingEveryXWaves == SimuEndingWaveRest)
            {
                GameObject EndingPortal = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(iscSimuExitPortal, new DirectorPlacementRule
                {
                    minDistance = 30f,
                    maxDistance = 35f,
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    position = run.safeWardController.transform.position,
                    spawnOnTarget = run.safeWardController.transform
                }, run.safeWardRng));
            }

            if (self.name.EndsWith("EquipmentDrone(Clone)"))
            {
                MeteorStormController[] meteorList = Object.FindObjectsOfType(typeof(MeteorStormController)) as MeteorStormController[];
                if (meteorList.Length > 0)
                {
                    for (int i = 0; i < meteorList.Length; i++)
                    {
                        Destroy(meteorList[i].gameObject);
                    }
                }
                BuffWard[] buffWard = Object.FindObjectsOfType(typeof(BuffWard)) as BuffWard[];
                if (buffWard.Length > 0)
                {
                    GameObject onDestroyEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/BrittleDeath");
                    //onDestroyEffect.GetComponent<EffectComponent>().soundName = "";
                    //onDestroyEffect.GetComponent<EffectComponent>().soundName = "Play_char_glass_death";
                    for (int i = 0; i < buffWard.Length; i++)
                    {
                        if (buffWard[i].buffDef = RoR2Content.Buffs.Cripple)
                        {
                            Destroy(buffWard[i].gameObject);
                            EffectManager.SpawnEffect(onDestroyEffect, new EffectData
                            {
                                origin = buffWard[i].transform.position,
                                rotation = buffWard[i].transform.rotation
                            }, true);
                        }
                    }
                }
            }
            else if (self.name.EndsWith("Enigma(Clone)"))
            {
                self.GetComponent<ArtifactEnabler>().OnDisable();
            }

            if (Run.instance)
            {
                Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
            }
            Debug.Log("OnAllEnemiesDefeatedServer  " + self);
        }

        public static void InfiniteTowerRun_CleanUpCurrentWave(On.RoR2.InfiniteTowerRun.orig_CleanUpCurrentWave orig, InfiniteTowerRun self)
        {
            if (NetworkServer.active)
            {
                if (self.waveInstance)
                {
                    switch (self.waveInstance.name)
                    {
                        case "InfiniteTowerWaveHeresy(Clone)":
                            if (self.enemyInventory.GetItemCount(ITDamageDown) > 10)
                            {
                                self.enemyInventory.RemoveItem(RoR2Content.Items.LunarPrimaryReplacement);
                            }
                            else
                            {
                                self.enemyInventory.RemoveItem(RoR2Content.Items.LunarSecondaryReplacement);
                                self.enemyInventory.RemoveItem(RoR2Content.Items.LunarUtilityReplacement);
                            }
                            self.enemyInventory.RemoveItem(ITDamageDown, self.enemyInventory.GetItemCount(ITDamageDown));
                            self.enemyInventory.RemoveItem(ITAttackSpeedDown, self.enemyInventory.GetItemCount(ITAttackSpeedDown));
                            self.enemyInventory.RemoveItem(ITCooldownUp, self.enemyInventory.GetItemCount(ITCooldownUp));
                            self.enemyInventory.RemoveItem(RoR2Content.Items.BoostHp, self.enemyInventory.GetItemCount(RoR2Content.Items.BoostHp));
                            break;
                        case "InfiniteTowerWaveManyItems(Clone)":
                            for (int i = 0; i < self.enemyInventory.itemAcquisitionOrder.Count; i++)
                            {
                                ItemDef tempDef = ItemCatalog.GetItemDef(self.enemyInventory.itemAcquisitionOrder[i]);
                                if (tempDef == ITHealthScaling || tempDef == RoR2Content.Items.Bear || tempDef == RoR2Content.Items.ExtraLife || tempDef == DLC1Content.Items.ExtraLifeVoid)
                                {
                                    self.enemyInventory.RemoveItem(tempDef, 1);
                                }
                                else
                                {
                                    self.enemyInventory.RemoveItem(tempDef, (int)(self.enemyInventory.GetItemCount(self.enemyInventory.itemAcquisitionOrder[i]) / 5f * 4f));
                                }
                            }
                            break;
                    }
                    if (Run.instance)
                    {
                        Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                    }
                }
            }
            orig(self);
            Debug.Log("WaveCleanUp  " + self.waveInstance);
        }

        public static void AwaitingActivation_OnEnter(On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingActivation self)
        {
            //Debug.LogWarning((Run.instance as InfiniteTowerRun).waveInstance);
            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveInstance)
            {
                self.radius = 60 + Run.instance.participatingPlayerCount * 5;
            }
            else
            {
                self.radius = 30;
            }
            orig(self);

            if (!run.safeWardController)
            {
                run.safeWardController = self.gameObject.GetComponent<InfiniteTowerSafeWardController>();
            }
        }

        public static void Travelling_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Travelling self)
        {
            self.radius = 30;
            orig(self);
            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                //Maybe travel at like two times the speed on Stage 6
                float waves = Run.instance.GetComponent<InfiniteTowerRun>().Network_waveIndex;
                float speedMult = (1f + (waves - 10f) / 50f) + 0.1f;

                //Stage 5 Onward
                if (waves > 30)
                {
                    speedMult += (waves - 25f) / 50f;
                    if (speedMult > 3) { speedMult = 3; }
                }
                self.zone.radius = Mathf.Min(self.radius * (0.5f + 0.5f * speedMult), 45);
                self.travelSpeed *= speedMult;
                self.pathMaxSpeed *= speedMult;

                Debug.Log("Wave:" + waves + " speedMult:" + speedMult + " TravellingSpeed:" + self.travelSpeed + " Radius:" + self.zone.radius);
            }
        }

        public static void SimulacrumEndingBeginEnding(On.RoR2.EventFunctions.orig_BeginEnding orig, EventFunctions self, GameEndingDef gameEndingDef)
        {
            if (gameEndingDef == DLC1Content.GameEndings.VoidEnding && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                orig(self, InfiniteTowerEnding);
                foreach (CharacterBody characterBody in CharacterBody.readOnlyInstancesList)
                {
                    if (characterBody.hasEffectiveAuthority)
                    { }
                    EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(characterBody.gameObject, "Body");
                    if (entityStateMachine)
                    {
                        entityStateMachine.SetInterruptState(new EntityStates.Idle(), EntityStates.InterruptPriority.Frozen);
                    }

                }
                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                {
                    baseToken = "<color=#F4ADFA><sprite name=\"VoidCoin\" tint=1> The Simulation has been suspended. Printing results.. <sprite name=\"VoidCoin\" tint=1></color>"
                });
                return;
            }
            orig(self, gameEndingDef);
        }

    }


}