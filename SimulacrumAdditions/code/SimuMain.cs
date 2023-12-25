using BepInEx;
using R2API;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
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
    [BepInPlugin("Wolfo.SimulacrumAdditions", "SimulacrumAdditions", "1.5.0")]
    //[R2APISubmoduleDependency(nameof(ContentAddition), nameof(LanguageAPI), nameof(PrefabAPI), nameof(ItemAPI), nameof(LoadoutAPI), nameof(EliteAPI))]
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
        //Would need to be the first in the Array to work normally

        public static GameEndingDef InfiniteTowerEnding = ScriptableObject.CreateInstance<GameEndingDef>();
        public static InteractableSpawnCard iscSimuExitPortal;
        public static GameObject VoidTeleportOutEffect = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/ExtraLifeVoid/VoidRezEffect.prefab").WaitForCompletion(), "VoidTeleportOutEffect", true);

        public static ItemTierDef ItemOrangeTierDef;
        //
        //Does this need to be in the Simu File 
        public static BasicPickupDropTable dtAISafeRandomVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        //
        //
        //Wave Prerequesites
        //public static RoR2.InfiniteTowerWaveArtifactPrerequisites ArtifactEliteOnlyDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave11OrGreaterPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveCountPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Wave11OrGreaterPrerequisite.asset").WaitForCompletion();
        //public static RoR2.InfiniteTowerWaveCountPrerequisites Wave21OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave26OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave31OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave46OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave61OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();

        //public static InfiniteTowerMaxWaveCountPrerequisites Wave30OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();
        //public static InfiniteTowerMaxWaveCountPrerequisites Wave50OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();
        //public static InfiniteTowerMaxWaveCountPrerequisites Wave75OrLowerPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerMaxWaveCountPrerequisites>();

        //
        //
        //Simu Wave Reward Drop Tables
        public static BasicPickupDropTable dtITWaveTier1 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITDefaultWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier2 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier3 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITSpecialBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITVoid = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITVoid.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITLunar = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITLunar.asset").WaitForCompletion();

        public static EliteInclusiveDropTable dtAllTier = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();
 
        public static BasicPickupDropTable dtITBasicWaveOnKill = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static EliteInclusiveDropTable dtITBasicBonusLunar = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static BasicPickupDropTable dtITBasicBonusVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITSpecialEquipment = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITVoidInfestorWave = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSuperVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static EliteInclusiveDropTable dtITSpecialBossYellow = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static ExplicitPickupDropTable dtITHeresy = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();
        public static ExplicitPickupDropTable dtITWurms = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();

        //
        //public static GameObject[] ITAllSpecialBossWaves = new GameObject[] { InfiniteTowerWaveBossScav, InfiniteTowerWaveBossBrother, InfiniteTowerWaveBossScavLunar, InfiniteTowerWaveBossSuperRoboBallBoss, InfiniteTowerWaveBossTitanGold, InfiniteTowerWaveBossVoidRaidCrab };
        //
        public static SceneDef PreviousSceneDef = null;
        public static CombatDirector.EliteTierDef[] EliteTiersBackup;
        public static ItemDef ITDamageDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHorrorIdentifier = ScriptableObject.CreateInstance<ItemDef>();

        public void Awake()
        {
            DumpAllWaveInfo(ITBasicWaves);
            DumpAllWaveInfo(ITBossWaves);
            MakePortal();

            WConfig.InitConfig();
            SetupConstants();

            SimuChanges();
            SimuWavesMisc.Start();
            SimulacrumWavesArtifacts.Start();
            SimulacrumWavesFamily.Start();
            SimulacrumDCCS.Start();

            GiantGup.Start();
            SuperMegaCrab.Start();

            LanguageAPI.Add("INFINITETOWER_SUDDEN_DEATH", "<style=cWorldEvent>[WARNING] The Focus begins to falter..</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_AWAITINGACTIVATION", "Activate the <style=cIsVoid>Focus</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_TRAVEL", "Follow the <style=cIsVoid>Focus</style>", "en");
            LanguageAPI.Add("INFINITETOWER_OBJECTIVE_PORTAL", "Advance through the <style=cIsVoid>Infinite Portal</style>", "en");

            LanguageAPI.Add("MAP_INFINITETOWER_SUBTITLE_ITMOON", "Latest specimen", "en");

            GameModeCatalog.availability.CallWhenAvailable(LateRunningMethod);

            //
            On.RoR2.InfiniteTowerRun.Start += InfiniteTowerRunStart;
            On.RoR2.InfiniteTowerRun.OnDestroy += InfiniteTowerRunEnd;

            //Prevent Repeat Stages
            On.RoR2.InfiniteTowerRun.OnWaveAllEnemiesDefeatedServer += InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer;
            //General Pre Wave
            On.RoR2.InfiniteTowerWaveCategory.SelectWavePrefab += InfiniteTowerWaveCategory_SelectWavePrefab;

            //Give Stats here
            On.RoR2.InfiniteTowerExplicitSpawnWaveController.Initialize += GiveStatBoosts_ExplicitSpawnWaveController_Initialize;

            //General Start of Wave
            On.RoR2.InfiniteTowerRun.BeginNextWave += InfiniteTowerRun_BeginNextWave;
            //Post Wave, End Portal, Double Rewards
            On.RoR2.InfiniteTowerWaveController.OnAllEnemiesDefeatedServer += InfiniteTowerWaveController_OnAllEnemiesDefeatedServer;
            //Mostly Elite Wave stuff
            On.RoR2.InfiniteTowerRun.CleanUpCurrentWave += InfiniteTowerRun_CleanUpCurrentWave;
            //
            if (WConfig.cfgMoreItems.Value)
            {
                On.RoR2.InfiniteTowerRun.AdvanceWave += MoreItems_AdvanceWave;
            }

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
                if (temp)
                {
                    self.duration = temp.GetComponent<InfiniteTowerWaveController>().secondsAfterWave;
                }
            };

            //Use Custom Simu Interactable DCCSs
            On.RoR2.ClassicStageInfo.RebuildCards += SimulacrumDCCS.SimuInteractableDCCSAdder;
            //More Interactables early on to get into it quicker
            if (WConfig.cfgSimuCreditsRebalance.Value)
            {
                On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += CreditsRebalance;
            }
            //
            //
            On.RoR2.InfiniteTowerWaveController.DropRewards += (orig, self) =>
            {
                orig(self);
                SimulacrumExtrasHelper temp = self.GetComponent<SimulacrumExtrasHelper>();
                if (temp && !temp.hasDone && temp.rewardDropTable != null)
                {
                    temp.hasDone = true;
                    self.rewardDisplayTier = temp.rewardDisplayTier;
                    self.rewardDropTable = temp.rewardDropTable;
                    self.DropRewards();
                }
            };


            //Fake Ass Ending Overwrite
            On.RoR2.EventFunctions.BeginEnding += SimulacrumEndingBeginEnding;

            //Give Simu Scavs Void Items
            On.RoR2.ScavengerItemGranter.Start += SimuGiveScavVoidItems;

            //Prevents explicit wave in basic waves whatever from moving crab 
            /*On.RoR2.InfiniteTowerExplicitSpawnWaveController.OnAllEnemiesDefeatedServer += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {

                }
                else
                {
                    orig(self);
                }
            };*/

            
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
            VoidPotential.transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(2).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(4).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(5).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(6).localPosition = new Vector3(0, -0.5f, 0);

            On.RoR2.PickupPickerController.SetOptionsInternal += (orig, self, newOptions) =>
            {
                orig(self, newOptions);
                if (self.name.StartsWith("Option"))
                {
                    PickupDisplay pickupDisplay = self.transform.GetChild(0).GetComponent<PickupDisplay>();
                    if (pickupDisplay.pickupIndex != PickupIndex.none)
                    {
                        switch (pickupDisplay.pickupIndex.pickupDef.itemTier)
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
                        if (pickupDisplay.pickupIndex.pickupDef.itemTier == ItemOrangeTierDef.tier)
                        {
                            pickupDisplay.equipmentParticleEffect.SetActive(true);
                        }
                    }
                    self.GetComponent<GenericDisplayNameProvider>().SetDisplayToken("OPTION_PICKUP_UNKNOWN_NAME");
                }
            };

            //
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
        }

        private void InfiniteTowerRunStart(On.RoR2.InfiniteTowerRun.orig_Start orig, InfiniteTowerRun self)
        {
            ITBasicWaves.wavePrefabs[0].weight = 80f;
            ITBossWaves.wavePrefabs[0].weight = 100f;
            SimulacrumDCCS.MakeITSand(false);

            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = false;
            Destroy(eqDrone.GetComponent<RoR2.SetDontDestroyOnLoad>());
            orig(self);
        }

        private void InfiniteTowerRunEnd(On.RoR2.InfiniteTowerRun.orig_OnDestroy orig, InfiniteTowerRun self)
        {
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = true;
            eqDrone.AddComponent<RoR2.SetDontDestroyOnLoad>();

            orig(self);
        }

        private void GiveStatBoosts_ExplicitSpawnWaveController_Initialize(On.RoR2.InfiniteTowerExplicitSpawnWaveController.orig_Initialize orig, InfiniteTowerExplicitSpawnWaveController self, int waveIndex, Inventory enemyInventory, GameObject spawnTargetObject)
        {
            float bonusspecialmultiplier = 1;
            switch (self.name)
            {
                case "InfiniteTowerWaveBossScav(Clone)":
                    break;
                case "InfiniteTowerWaveBossFamilyWorms(Clone)":
                    bonusspecialmultiplier = -1f;
                    break;
                case "InfiniteTowerWaveBossVoidElites(Clone)":
                    bonusspecialmultiplier = 3f;
                    break;
                case "InfiniteTowerWaveBossScavLunar(Clone)":
                    break;
                case "InfiniteTowerWaveBossSuperCrab(Clone)":
                    break;
                case "InfiniteTowerWaveBasicEquipmentDrone(Clone)":
                case "InfiniteTowerWaveBossEquipmentDrone(Clone)":
                    bonusspecialmultiplier = (waveIndex / 15f - 1); //
                    if (bonusspecialmultiplier > 4)
                    {
                        bonusspecialmultiplier = 4;
                    }
                    InfiniteTowerExplicitSpawnWaveController eqDrone = self.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
                    List<CharacterSpawnCard> tempCscEqList = new List<CharacterSpawnCard>(SimuWavesMisc.AllCSCEquipmentDronesIT);
                    for (int i = 0; i < eqDrone.spawnList.Length; i++)
                    {
                        eqDrone.spawnList[i].spawnCard = tempCscEqList[WRect.random.Next(tempCscEqList.Count)];
                        tempCscEqList.Remove(eqDrone.spawnList[0].spawnCard);
                        if (waveIndex > 39)
                        {
                            eqDrone.spawnList[i].count++;
                        }
                    }
                    break;
                case "InfiniteTowerWaveBossVoidRaidCrab(Clone)":
                    bonusspecialmultiplier = 1.25f;
                    break;
                case "InfiniteTowerWaveBossBrother(Clone)":
                    bonusspecialmultiplier = 2.4f;
                    break;
                case "InfiniteTowerWaveBossGiantGup(Clone)":
                    bonusspecialmultiplier = 1.5f;
                    break;
                case "InfiniteTowerWaveBossSuperRoboBallBoss(Clone)":
                    bonusspecialmultiplier = 0.8f;
                    break;
                case "InfiniteTowerWaveBossTitanGold(Clone)":
                    bonusspecialmultiplier = 1f;
                    break;
                case "InfiniteTowerWaveBasicGhostHaunting(Clone)":
                case "InfiniteTowerWaveBossGhostHaunting(Clone)":
                    //Explicit Spawn waves that shouldn't have special scaling
                    bonusspecialmultiplier = 0.1f;            
                    if (waveIndex > 35)
                    {
                        self.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].count++;
                    }
                    break;
            }
            //Debug.Log(Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state);

            if (bonusspecialmultiplier > 0)
            {
                float num = 1f;
                float num2 = 1f;
                num += Run.instance.difficultyCoefficient / 2.5f * System.Math.Max(1, (waveIndex / 10) * 0.2f + 0.2f);
                num2 += Run.instance.difficultyCoefficient / 30f * System.Math.Max(1, (waveIndex / 10) * 0.075f);
                num *= bonusspecialmultiplier;
                num /= (1 + ((Run.instance.participatingPlayerCount - 1) * 0.25f));
                int num3 = Mathf.Max(1, Run.instance.livingPlayerCount);
                num *= Mathf.Pow((float)num3, 0.5f);
                int grantHp = Mathf.RoundToInt((num - 1f) * 10f);
                int grantDamage = Mathf.RoundToInt((num2 - 1f) * 10f);
                if (grantHp > 10000) { grantHp = 10000; }
                Debug.LogFormat(self.name + " Special Scaling: currentBoostHpCoefficient={0}, currentBoostDamageCoefficient={1}", new object[]
                {
                       grantHp,
                       grantDamage
                });

                bool hasNoHP = true;
                bool hasNoDmg = true;  
                bool hasNoTP = true;
                for (int i = 0; i < self.spawnList[0].spawnCard.itemsToGrant.Length; i++)
                {
                    if (self.spawnList[0].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.BoostHp)
                    {
                        hasNoHP = false;
                        self.spawnList[0].spawnCard.itemsToGrant[i].count = grantHp;
                    }
                    else if (self.spawnList[0].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.BoostDamage)
                    {
                        hasNoDmg = false;
                        self.spawnList[0].spawnCard.itemsToGrant[i].count = grantDamage;
                    }
                    else if (self.spawnList[0].spawnCard.itemsToGrant[i].itemDef == RoR2Content.Items.TeleportWhenOob)
                    {
                        hasNoTP = false;
                    }
                }
                if (hasNoHP)
                {
                    self.spawnList[0].spawnCard.itemsToGrant = self.spawnList[0].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = grantHp });
                }
                if (hasNoDmg)
                {
                    self.spawnList[0].spawnCard.itemsToGrant = self.spawnList[0].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = grantDamage });
                }
                if (hasNoTP)
                {
                    self.spawnList[0].spawnCard.itemsToGrant = self.spawnList[0].spawnCard.itemsToGrant.Add(new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 });
                }

                foreach (ItemCountPair itemPair in self.spawnList[0].spawnCard.itemsToGrant)
                {
                    Debug.Log(itemPair.itemDef + "  " + itemPair.count);
                }

                foreach (InfiniteTowerExplicitSpawnWaveController.SpawnInfo spawnInfo in self.spawnList)
                {
                    spawnInfo.spawnCard.itemsToGrant = self.spawnList[0].spawnCard.itemsToGrant;
                }

            }

            bool addElite = false;
            if(addElite)
            {
                //CombatDirector.eliteTiers[1].GetRandomAvailableEliteDef();
                for (int i = 0; i < self.spawnList.Length; i++)
                {
                    self.spawnList[i].eliteDef = RoR2Content.Elites.Fire;
                }
            }

            orig(self, waveIndex, enemyInventory, spawnTargetObject);
        }

        private void MoreItems_AdvanceWave(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            if (self.waveIndex > 69)
            {
                //Red at 80
                self.enemyItemPeriod = 1;
            }
            else if (self.waveIndex > 60)
            {
                //Red at 70
                self.enemyItemPeriod = 2;
                self.enemyItemPattern[0].stacks = 3;
                self.enemyItemPattern[1].stacks = 3;
                self.enemyItemPattern[2].stacks = 2;
                self.enemyItemPattern[3].stacks = 2;
            }
            else if (self.waveIndex > 40)
            {
                //Red at 60
                self.enemyItemPeriod = 4;
            }
            else
            {
                //Red at 40
                self.enemyItemPeriod = 8;
            }
            orig(self);
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
            EndingPortal.transform.GetChild(2).GetChild(4).localScale *= 0.5f;
            EndingPortal.transform.GetChild(2).GetChild(4).GetComponent<EntityLocator>().entity = EndingPortal;
            EndingPortal.transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
            Destroy(EndingPortal.transform.GetChild(1).gameObject);
            Destroy(EndingPortal.transform.GetChild(0).gameObject);
            /*
            UnityEngine.Object.Destroy(EndingPortal.transform.GetChild(1).gameObject); //Music
            UnityEngine.Object.Destroy(EndingPortal.transform.GetChild(0).GetChild(1).gameObject); //PortalSpawnFX

            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial = iscVoidPortal.prefab.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().sharedMaterial;
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(0).localScale = new Vector3(4, 4, 3);
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Rewired.ComponentControls.Effects.RotateAroundAxis>().enabled = false;
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Light>().color = new Color(0.5f, 0.2f, 0.4f, 1f);
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(3).gameObject.SetActive(false); //Particles
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(4).localScale *= 0.5f;
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(5).gameObject.SetActive(false); //Particles
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(6).localScale = new Vector3(3, 3, 3);
            EndingPortal.transform.GetChild(0).GetChild(0).GetChild(7).gameObject.SetActive(false); //Particles
            */
        }



        private void CreditsRebalance(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            orig(self, sceneDirector);
            float num = 0.7f + (float)Run.instance.participatingPlayerCount * 0.3f;
            if (self.waveIndex < 39) //First 4 stages
            {
                sceneDirector.interactableCredit += System.Math.Max(0, 100 - self.waveIndex / 10 * 35);
            }
            else //Late game where you can clear most of the stage anyways
            {
                sceneDirector.interactableCredit += System.Math.Max(-250, 150 - self.waveIndex / 10 * 50);
            }

            sceneDirector.interactableCredit = (int)(num * sceneDirector.interactableCredit);
            sceneDirector.interactableCredit = System.Math.Min(sceneDirector.interactableCredit, 6000);
            Debug.Log("InfiniteTower " + sceneDirector.interactableCredit + " interactable credits. ");
        }

        private void RadiusManipActive_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Active.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Active self)
        {
            orig(self);
            if (Run.instance && Run.instance is InfiniteTowerRun && (Run.instance as InfiniteTowerRun).waveInstance)
            {
                SimulacrumExtrasHelper temp = (Run.instance as InfiniteTowerRun).waveInstance.GetComponent<SimulacrumExtrasHelper>();
                if (temp && temp.newRadius > 0)
                {
                    self.radius = temp.newRadius;
                }
            }
        }

        public static void SimuGiveScavVoidItems(On.RoR2.ScavengerItemGranter.orig_Start orig, ScavengerItemGranter self)
        {
            orig(self);

            Inventory tempinv = self.GetComponent<Inventory>();
            //Debug.LogWarning(tempbod);
            if (Run.instance is InfiniteTowerRun)
            {
                PickupIndex pickupIndex = SimuMain.dtAISafeRandomVoid.GenerateDrop(Run.instance.treasureRng);
                ItemDef itemdef = ItemCatalog.GetItemDef(pickupIndex.pickupDef.itemIndex);

                if (itemdef.tier == ItemTier.VoidTier1)
                {
                    tempinv.GiveItem(itemdef, 3);
                    Debug.Log("Giving Simu Scav 3 " + itemdef);
                }
                else if (itemdef.tier == ItemTier.VoidTier2)
                {
                    tempinv.GiveItem(itemdef, 2);
                    Debug.Log("Giving Simu Scav 2 " + itemdef);
                }
                else
                {
                    tempinv.GiveItem(itemdef, 1);
                    Debug.Log("Giving Simu Scav 1 " + itemdef);
                }
            }
        }

        public static void LateRunningMethod()
        {
            SimuWavesMisc.MakeLater();

            SimulacrumWavesFamily.ModSupport();
            SimulacrumWavesArtifacts.ModSupport();

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

          
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactEnigma.prefab").WaitForCompletion().GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;

            InfiniteTowerRun InfiniteTowerRunBase = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            //This is where we'd need to add Fireworks
            //Fireworks is Interactable Related and that tagged is banned
            InfiniteTowerRunBase.blacklistedItems = InfiniteTowerRunBase.blacklistedItems.Add(RoR2Content.Items.Squid, RoR2Content.Items.MonstersOnShrineUse); //But Squid Polyp wouldn't work they just die
            InfiniteTowerRunBase.blacklistedTags = InfiniteTowerRunBase.blacklistedTags.Remove(ItemTag.InteractableRelated); //There's only two and Fireworks works plenty


            //Blacklist VanillaVoid Cornucopia, unusable and essentially kills you
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

            RoR2Content.Items.MonstersOnShrineUse.tags = RoR2Content.Items.MonstersOnShrineUse.tags.Add(ItemTag.InteractableRelated);
            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.SprintRelated);

            DLC1Content.Items.MoveSpeedOnKill.tags = DLC1Content.Items.MoveSpeedOnKill.tags.Add(ItemTag.OnKillEffect);


            RoR2Content.Items.ParentEgg.tags[0] = ItemTag.Healing;
            RoR2Content.Items.ShieldOnly.tags[0] = ItemTag.Healing;
            RoR2Content.Items.LunarUtilityReplacement.tags[0] = ItemTag.Healing;
            RoR2Content.Items.RandomDamageZone.tags[0] = ItemTag.Damage;
            DLC1Content.Items.HalfSpeedDoubleHealth.tags[0] = ItemTag.Healing;
            DLC1Content.Items.LunarSun.tags[0] = ItemTag.Damage;

            DLC1Content.Items.MinorConstructOnKill.tags = DLC1Content.Items.MinorConstructOnKill.tags.Add(ItemTag.Utility);
            RoR2Content.Items.Knurl.tags = RoR2Content.Items.Knurl.tags.Remove(ItemTag.Utility);


            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.HeadHunter.tags = RoR2Content.Items.HeadHunter.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnKill.tags = RoR2Content.Items.BarrierOnKill.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.BarrierOnOverHeal.tags = RoR2Content.Items.BarrierOnOverHeal.tags.Remove(ItemTag.Utility);
            RoR2Content.Items.FallBoots.tags = RoR2Content.Items.FallBoots.tags.Remove(ItemTag.Damage);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Remove(ItemTag.Damage);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.Healing);

            DLC1Content.Items.AttackSpeedAndMoveSpeed.tags = DLC1Content.Items.AttackSpeedAndMoveSpeed.tags.Remove(ItemTag.Utility);
            DLC1Content.Items.ElementalRingVoid.tags = DLC1Content.Items.ElementalRingVoid.tags.Remove(ItemTag.Utility);
            
            //RoR2Content.Items.PersonalShield.tags = RoR2Content.Items.PersonalShield.tags.Add(ItemTag.Healing);
            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.Healing);
            DLC1Content.Items.ImmuneToDebuff.tags = DLC1Content.Items.ImmuneToDebuff.tags.Add(ItemTag.Healing);
            

            RoR2Content.Items.BonusGoldPackOnKill.tags = RoR2Content.Items.BonusGoldPackOnKill.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.GoldOnHit.tags = RoR2Content.Items.GoldOnHit.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.RegeneratingScrap.tags = DLC1Content.Items.RegeneratingScrap.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.ShockNearby.tags = RoR2Content.Items.ShockNearby.tags.Add(ItemTag.Count);
            DLC1Content.Items.CritDamage.tags = DLC1Content.Items.CritDamage.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.DroneWeapons.tags = DLC1Content.Items.DroneWeapons.tags.Add(ItemTag.AIBlacklist);
            //RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Add(ItemTag.AIBlacklist);

            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                if (ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().maxSquadSize > 20)
                {
                    ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().maxSquadSize -= 10;
                }
            }
            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().maxSquadSize > 20)
                {
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().maxSquadSize -= 10;
                }
            }
            DumpAllWaveInfo(ITBasicWaves);
            DumpAllWaveInfo(ITBossWaves);
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

            RecalculateStatsAPI.GetStatCoefficients += delegate (CharacterBody sender, RecalculateStatsAPI.StatHookEventArgs args)
            {
                bool flag = sender.inventory != null;
                if (flag)
                {
                    if (sender.inventory.GetItemCount(ITDamageDown) > 0)
                    {
                        args.baseDamageAdd = sender.baseDamage * -0.01f * sender.inventory.GetItemCount(ITDamageDown);
                        //args.attackSpeedMultAdd += 0.15f - 0.15f * sender.inventory.GetItemCount(ITHeresyHelper);
                    }

                }
            };

            ITHorrorIdentifier.name = "ITGhostIdentifier";
            ITHorrorIdentifier.deprecatedTier = ItemTier.NoTier;
            ITHorrorIdentifier._itemTierDef = AACannon._itemTierDef;
            ITHorrorIdentifier.nameToken = "ITGhostIdentifier";
            ITHorrorIdentifier.pickupToken = "ITGhostIdentifier";
            ITHorrorIdentifier.descriptionToken = "ITGhostIdentifier";
            ITHorrorIdentifier.hidden = true;
            ITHorrorIdentifier.canRemove = false;
            ITHorrorIdentifier.pickupIconSprite = AACannon.pickupIconSprite;
            ITHorrorIdentifier.pickupModelPrefab = AACannon.pickupModelPrefab;

            CustomItem customItem2 = new CustomItem(ITHorrorIdentifier, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem2);
            On.RoR2.Util.GetBestBodyName += (orig, bodyObject) =>
            {      
                if (bodyObject)
                {
                    CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                    if (characterBody && characterBody.inventory)
                    {
                        if (characterBody.inventory.GetItemCount(ITHorrorIdentifier) > 0)
                        {
                            return "Unknown Horror";
                        }

                    }             
                }
                return orig(bodyObject);
            };


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
            ItemTag[] TagsMonsterTeamGain = { ItemTag.AIBlacklist, ItemTag.OnKillEffect, ItemTag.EquipmentRelated, ItemTag.SprintRelated, ItemTag.PriorityScrap, ItemTag.InteractableRelated, ItemTag.OnStageBeginEffect, ItemTag.HoldoutZoneRelated, ItemTag.Count };

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

            InfiniteTowerRunBase.enemyItemPattern[0].dropTable = dtMonsterTeamTier1Item;
            InfiniteTowerRunBase.enemyItemPattern[1].dropTable = dtMonsterTeamTier1Item;
            InfiniteTowerRunBase.enemyItemPattern[2].dropTable = dtMonsterTeamTier2Item;
            InfiniteTowerRunBase.enemyItemPattern[3].dropTable = dtMonsterTeamTier2Item;
            InfiniteTowerRunBase.enemyItemPattern[4].dropTable = dtMonsterTeamTier3Item;
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
            GameEndingDef VoidEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/DLC1/GameModes/VoidEnding.asset").WaitForCompletion();
            GameEndingDef ObliterationEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/Base/ClassicRun/ObliterationEnding.asset").WaitForCompletion();

            ContentAddition.AddGameEndingDef(InfiniteTowerEnding);

            InfiniteTowerEnding.endingTextToken = "Simulation Suspended";
            InfiniteTowerEnding.lunarCoinReward = 10;
            InfiniteTowerEnding.showCredits = false;
            InfiniteTowerEnding.isWin = false;
            InfiniteTowerEnding.gameOverControllerState = ObliterationEnding.gameOverControllerState;
            InfiniteTowerEnding.material = VoidEnding.material;
            InfiniteTowerEnding.icon = VoidEnding.icon;
            InfiniteTowerEnding.backgroundColor = new Color(0.65f, 0.3f, 0.5f, 0.8f);
            InfiniteTowerEnding.foregroundColor = new Color(0.75f, 0.4f, 0.55f, 1);
            InfiniteTowerEnding.cachedName = "InfiniteTowerEnding";
            //

            //PreRequs
            Wave11OrGreaterPrerequisite.minimumWaveCount = 11;
            /*Wave21OrGreaterPrerequisite.minimumWaveCount = 21;
            Wave21OrGreaterPrerequisite.name = "Wave21OrGreaterPrerequisite";*/
            Wave26OrGreaterPrerequisite.minimumWaveCount = 26;
            Wave26OrGreaterPrerequisite.name = "Wave26OrGreaterPrerequisite";
            Wave31OrGreaterPrerequisite.minimumWaveCount = 31;
            Wave31OrGreaterPrerequisite.name = "Wave31OrGreaterPrerequisite";
            Wave46OrGreaterPrerequisite.minimumWaveCount = 46;
            Wave46OrGreaterPrerequisite.name = "Wave46OrGreaterPrerequisite";
            Wave61OrGreaterPrerequisite.minimumWaveCount = 61;
            Wave61OrGreaterPrerequisite.name = "Wave61OrGreaterPrerequisite";

            /*Wave50OrLowerPrerequisite.maximumwavecount = 50;
            Wave50OrLowerPrerequisite.name = "Wave50OrLowerPrerequisite";
            Wave75OrLowerPrerequisite.maximumwavecount = 75;
            Wave75OrLowerPrerequisite.name = "Wave75OrLowerPrerequisite";*/

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
            dtAllTier.tier1Weight = 80;
            dtAllTier.tier2Weight = 20;
            dtAllTier.tier3Weight = 2;
            dtAllTier.bossWeight = 4;
            dtAllTier.equipmentWeight = 15;
            dtAllTier.lunarItemWeight = 10;
            dtAllTier.voidTier1Weight = 50;
            dtAllTier.voidTier2Weight = 15;
            dtAllTier.voidTier3Weight = 3;
            dtAllTier.voidBossWeight = 1;
            dtAllTier.eliteEquipWeight = 1;
            dtAllTier.lunarEquipmentWeight = 1;
            dtAllTier.pearlWeight = 1;

            if (WConfig.cfgVoidTripleAllTier.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = dtAllTier;
            }

            //OnKill for On Kill Artifacts
            dtITBasicWaveOnKill.tier1Weight = 80;
            dtITBasicWaveOnKill.tier2Weight = 9;
            dtITBasicWaveOnKill.tier3Weight = 0.75f;
            dtITBasicWaveOnKill.bossWeight = 0.25f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";
            dtITBasicWaveOnKill.requiredItemTags = new ItemTag[] { ItemTag.OnKillEffect };
            //

            //For Basic Void Elite Wave
            dtITBasicBonusVoid.tier1Weight = 80;
            dtITBasicBonusVoid.tier2Weight = 10;
            dtITBasicBonusVoid.tier3Weight = 0.25f;
            dtITBasicBonusVoid.bossWeight = 0;
            dtITBasicBonusVoid.voidTier1Weight = 60;
            dtITBasicBonusVoid.voidTier2Weight = 25;
            dtITBasicBonusVoid.voidTier3Weight = 5;
            dtITBasicBonusVoid.name = "dtITBasicBonusVoid";

            //For Basic Lunar Elite Wave
            dtITBasicBonusLunar.tier1Weight = 80;
            dtITBasicBonusLunar.tier2Weight = 10;
            dtITBasicBonusLunar.tier3Weight = 0.5f;
            dtITBasicBonusLunar.bossWeight = 0;
            dtITBasicBonusLunar.lunarCombinedWeight = 70;
            dtITBasicBonusLunar.pearlWeight = 70;
            //dtITBasicBonusLunar.lunarItemWeight = 70;
            dtITBasicBonusLunar.name = "dtITBasicBonusLunar";

            //Void Infestor Boss Wave
            dtITVoidInfestorWave.tier1Weight = 0;
            dtITVoidInfestorWave.tier2Weight = 0;
            dtITVoidInfestorWave.tier3Weight = 0;
            dtITVoidInfestorWave.bossWeight = 35;
            dtITVoidInfestorWave.voidTier1Weight = 60;
            dtITVoidInfestorWave.voidTier2Weight = 30;
            dtITVoidInfestorWave.voidTier3Weight = 8;
            dtITVoidInfestorWave.voidBossWeight = 7;
            dtITVoidInfestorWave.name = "dtITVoidInfestorWave";

            //In Addition to the Red after the Voidling Wave
            dtITSuperVoid.tier1Weight = 0;
            dtITSuperVoid.tier2Weight = 0;
            dtITSuperVoid.tier3Weight = 0;
            dtITSuperVoid.bossWeight = 0;
            dtITSuperVoid.voidTier1Weight = 0.5f;
            dtITSuperVoid.voidTier2Weight = 2;
            dtITSuperVoid.voidTier3Weight = 1;
            dtITSuperVoid.voidBossWeight = 1;
            dtITSuperVoid.name = "dtITSuperVoid";

            //Vengance & Honor Boss
            dtITSpecialBossYellow.tier1Weight = 0;
            dtITSpecialBossYellow.tier2Weight = 30;
            dtITSpecialBossYellow.tier3Weight = 0;
            dtITSpecialBossYellow.bossWeight = 70; //Because how weighted selections actually work, boss items will be a lot less common
            dtITSpecialBossYellow.name = "dtITSpecialBossYellow";
            dtITSpecialBossYellow.eliteEquipWeight = 10f;
            dtITSpecialBossYellow.pearlWeight = 70f;

            //Family Waves biased 
            dtITFamilyWaveDamage.tier1Weight = 80;
            dtITFamilyWaveDamage.tier2Weight = 6; //Move 4 Points to boss
            dtITFamilyWaveDamage.tier3Weight = 0.25f;
            dtITFamilyWaveDamage.bossWeight = 4.25f;
            dtITFamilyWaveDamage.name = "dtITFamilyWaveDamage";
            dtITFamilyWaveDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITFamilyWaveHealing.tier1Weight = 80;
            dtITFamilyWaveHealing.tier2Weight = 6;
            dtITFamilyWaveHealing.tier3Weight = 0.25f;
            dtITFamilyWaveHealing.bossWeight = 4.25f;
            dtITFamilyWaveHealing.name = "dtITFamilyWaveHealing";
            dtITFamilyWaveHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITFamilyWaveUtility.tier1Weight = 80;
            dtITFamilyWaveUtility.tier2Weight = 6;
            dtITFamilyWaveUtility.tier3Weight = 0.25f;
            dtITFamilyWaveUtility.bossWeight = 4.25f;
            dtITFamilyWaveUtility.name = "dtITFamilyWaveUtility";
            dtITFamilyWaveUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };
            //
            //In addition to the regular green so keep it mostly Orange
            dtITSpecialEquipment.requiredItemTags = new ItemTag[] { ItemTag.EquipmentRelated };
            dtITSpecialEquipment.tier1Weight = 0f;
            dtITSpecialEquipment.tier2Weight = 20f;
            dtITSpecialEquipment.tier3Weight = 20f;
            dtITSpecialEquipment.bossWeight = 0f;
            dtITSpecialEquipment.lunarItemWeight = 20f;
            dtITSpecialEquipment.equipmentWeight = 170f;
            dtITSpecialEquipment.lunarEquipmentWeight = 30f;
            dtITSpecialEquipment.name = "dtITSpecialEquipment";

            //ArtifactEliteOnlyDisabledPrerequisite.bannedArtifact = ArtifactDefEliteOnly;
            //ArtifactEliteOnlyDisabledPrerequisite.name = "ArtifactEliteOnlyDisabledPrerequisite";


            DirectorCardCategorySelection dccsITVoidMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dccsITVoidMonsters.asset").WaitForCompletion();

            dccsITVoidMonsters.categories[1].selectionWeight = 2;
            dccsITVoidMonsters.categories[2].cards[0].selectionWeight = 2;
        }

        public static void SimuChanges()
        {
            SceneDef itgolemplains = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgolemplains/itgolemplains.asset").WaitForCompletion();
            SceneDef itfrozenwall = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
            SceneDef itdampcave = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();

            itgolemplains.mainTrack = MusicVoidFields;
            itfrozenwall.mainTrack = MusicSnowyForest;
            itdampcave.mainTrack = MusicVoidStage;


            ITSuperBossWaves.name = "ITSuperBossWaves";
            ITSuperBossWaves.availabilityPeriod = 30;
            ITSuperBossWaves.minWaveIndex = 50;

            float ITSpecialBossWaveWeight = 2.5f;
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                switch (ITBasicWaves.wavePrefabs[i].wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactEnigma":
                        ITBasicWaves.wavePrefabs[i].weight = 1f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITSpecialEquipment;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDisplayTier = ItemOrangeTierDef.tier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your equipment changes every time it's activated.";
                        ITBasicWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
                        break;
                    case "InfiniteTowerWaveArtifactWeakAssKnees":
                        ITBasicWaves.wavePrefabs[i].weight = 1f;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        ITBasicWaves.wavePrefabs[i].weight = 3;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits *= 1.25f;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits *= 1.25f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters will be of only one type.";
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        ITBasicWaves.wavePrefabs[i].weight = 3;
                        break;
                };
            }


            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();

            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossVoid"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 7f;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossLunar"))
                {
                    CombatDirector temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.CombatDirector>();
                    temp.monsterCards = dccsLunarFamily;
                    temp.skipSpawnIfTooCheap = false;
                    ITBossWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier2;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossScav"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight+0.5f;
                    ITSuperBossWaves.wavePrefabs = ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);

                    InfiniteTowerExplicitSpawnWaveController temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    temp.rewardDisplayTier = ItemTier.Boss;
                    temp.rewardDropTable = dtITSpecialBossYellow;
                    temp.baseCredits = 100;
                    temp.linearCreditsPerWave = 5;
                    temp.secondsBeforeSuddenDeath *= 2f;

                    CharacterSpawnCard cscITScav = Object.Instantiate(temp.spawnList[0].spawnCard);
                    cscITScav.name = "cscITScav";
                    temp.spawnList[0].spawnCard = cscITScav;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossBrother"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight;
                    ITBossWaves.wavePrefabs[i].prerequisites = Wave26OrGreaterPrerequisite;
                    ITSuperBossWaves.wavePrefabs = ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);

                    ITBossWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

                    InfiniteTowerExplicitSpawnWaveController temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    temp.baseCredits = 50;
                    temp.immediateCreditsFraction = 0.5f;
                    temp.linearCreditsPerWave = 5;
                    temp.combatDirector.monsterCards = dccsLunarFamily;
                    temp.secondsBeforeSuddenDeath *= 2;
                }
            }

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
            float boss = 0;
            float bossw5 = 0;
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                boss += category.wavePrefabs[i].weight;
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
                else
                {
                    bossw5 += category.wavePrefabs[i].weight;
                }
                token4 += "  ";
                Debug.Log(token4);

                if (category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards != null)
                {
                    Debug.Log("MonsterCards: " + category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards.name + "  ");
                }
                Debug.Log("");
            }


            Debug.Log("Total " + category.name + " Weight Until Wave 10: " + bossw5 +
             "  Extra Weight: " + (bossw5 - category.wavePrefabs[0].weight) +
             "  Percent for Default: " + category.wavePrefabs[0].weight / bossw5);

            Debug.Log("Total " + category.name + " Weight: " + boss +
             "  Extra Weight: " + (boss - category.wavePrefabs[0].weight) +
             "  Percent for Default: " + category.wavePrefabs[0].weight / boss);
        }


        public static GameObject InfiniteTowerWaveCategory_SelectWavePrefab(On.RoR2.InfiniteTowerWaveCategory.orig_SelectWavePrefab orig, InfiniteTowerWaveCategory self, InfiniteTowerRun run, Xoroshiro128Plus rng)
        {
            GameObject temp = orig(self, run, rng);
            Debug.Log("SelectWavePrefab  " + temp);

            //Debug.LogWarning(run.waveIndex % 50);
            if (run.waveIndex >= SimuForcedBossStartAtXWaves && run.waveIndex % SimuForcedBossEveryXWaves == SimuForcedBossWaveRest)
            {
                temp = ITSuperBossWaves.wavePrefabs[WRect.random.Next(ITSuperBossWaves.wavePrefabs.Length)].wavePrefab;
                Debug.Log("Forcing SuperBoss");
                ITBasicWaves.wavePrefabs[0].weight = 60; //More Wackies past this
                ITBasicWaves.GenerateWeightedSelection();
                ITBossWaves.wavePrefabs[0].weight = 0; //More Wackies past this
                ITBossWaves.GenerateWeightedSelection();
            }


            Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state.SetFieldValue("radius", 60f);
            //Radius Only, can't seem to do Radius in one place only, doing it here overwrites if it it's the first wave
            SimulacrumExtrasHelper radiusManip = temp.GetComponent<SimulacrumExtrasHelper>();
            if (radiusManip && radiusManip.newRadius > 0)
            {
                Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state.SetFieldValue("radius", radiusManip.newRadius);
            }

            switch (temp.name)
            {
                case "InfiniteTowerWaveBossArtifactDoppelganger":
                    RoR2.Artifacts.DoppelgangerInvasionManager.PerformInvasion(Run.instance.bossRewardRng);
                    break;
                case "InfiniteTowerWaveVoidElites":
                    if (CombatDirector.eliteTiers.Length > 1)
                    { 
                        CombatDirector.EliteTierDef[] arrayL = new CombatDirector.EliteTierDef[2];
                        arrayL[0] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 1,
                            eliteTypes = new EliteDef[1],
                            isAvailable = (SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive(),
                            canSelectWithoutAvailableEliteDef = true,
                        };
                        arrayL[1] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 3f,
                            eliteTypes = new EliteDef[]
                            {
                                DLC1Content.Elites.Void, //2x Health
                            },
                            isAvailable = (SpawnCard.EliteRules rules) => true,
                            canSelectWithoutAvailableEliteDef = false,
                        };
                        EliteTiersBackup = CombatDirector.eliteTiers;
                        CombatDirector.eliteTiers = arrayL;
                    }
                    break;
                case "InfiniteTowerWaveMalachiteElites":
                    if (CombatDirector.eliteTiers.Length > 1)
                    {
                        CombatDirector.EliteTierDef[] arrayL = new CombatDirector.EliteTierDef[2];
                        arrayL[0] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 1,
                            eliteTypes = new EliteDef[1],
                            isAvailable = (SpawnCard.EliteRules rules) => true,
                            canSelectWithoutAvailableEliteDef = true,
                        };
                        arrayL[1] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = CombatDirector.baseEliteCostMultiplier*4f,
                            eliteTypes = new EliteDef[]
                            {
                                RoR2Content.Elites.Poison,
                            },
                            isAvailable = (SpawnCard.EliteRules rules) => true,
                            canSelectWithoutAvailableEliteDef = false,
                        };
                        EliteTiersBackup = CombatDirector.eliteTiers;
                        CombatDirector.eliteTiers = arrayL;
                    }
                    break;
                case "InfiniteTowerWaveLunarElites":
                case "InfiniteTowerWaveBossScavLunar":
                    if (CombatDirector.eliteTiers.Length > 1)
                    {
                        //Lunar has 2.5x Health 2x Damage
                        CombatDirector.EliteTierDef[] arrayL = new CombatDirector.EliteTierDef[2];
                        arrayL[0] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 1,
                            eliteTypes = new EliteDef[1],
                            isAvailable = (SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive() && rules == SpawnCard.EliteRules.Default,
                            canSelectWithoutAvailableEliteDef = true,
                        };
                        arrayL[1] = new CombatDirector.EliteTierDef
                        {
                            costMultiplier = 2f,
                            eliteTypes = new EliteDef[]
                            {
                                RoR2Content.Elites.Lunar,
                            },
                            isAvailable = (SpawnCard.EliteRules rules) => true,
                            canSelectWithoutAvailableEliteDef = false,
                        };
                        EliteTiersBackup = CombatDirector.eliteTiers;
                        CombatDirector.eliteTiers = arrayL;
                    }
                    break;
                case "InfiniteTowerWaveBossSuperCrab":
                    break;
            }
            return temp;
        }

        public static void InfiniteTowerRun_BeginNextWave(On.RoR2.InfiniteTowerRun.orig_BeginNextWave orig, InfiniteTowerRun self)
        {
            orig(self);

            if (self.waveInstance)
            {
                switch (self.waveInstance.name)
                {
                    case "InfiniteTowerWaveBossArtifactDoppelganger(Clone)":
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
                                }
                            }
                        }
                        break;
                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        self.waveInstance.GetComponents<CombatDirector>()[1].monsterCredit *= System.Math.Max(0.9f, ((self.waveIndex / 10) + 1f) * 0.32f);
                        break;
                    case "InfiniteTowerWaveBossBrother(Clone)":
                        self.waveInstance.AddComponent<PhaseCounter>().phase = 3;
                        break;
                    case "InfiniteTowerWaveBossScav(Clone)":
                        if (!Stage.instance.scavPackDroppedServer)
                        {
                            self.waveInstance.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsAfterWave = 15;
                        }
                        break;
                    case "InfiniteTowerWaveHeresy(Clone)":
                        int decide = WRect.random.Next(0, 2);
                        RoR2Content.Items.BoostHp.hidden = true;
                        if (decide == 0)
                        {
                            self.enemyInventory.GiveItem(RoR2Content.Items.LunarPrimaryReplacement);
                            self.enemyInventory.GiveItem(ITDamageDown, 60); //Ideally we'd lower fire rate more than damage because Plate/Planula cheese it hard.
                            self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, Run.instance.stageClearCount * 3);
                            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                            {
                                baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Visions of Heresy</color> has been integrated into the Simulacrum...!</style>",
                            });
                        }
                        else
                        {
                            self.enemyInventory.GiveItem(RoR2Content.Items.LunarSecondaryReplacement);
                            self.enemyInventory.GiveItem(RoR2Content.Items.LunarUtilityReplacement);
                            self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, Run.instance.stageClearCount * 3);
                            self.enemyInventory.GiveItem(ITDamageDown, 10);
                            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                            {
                                baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Hooks and Strides of Heresy</color> have been integrated into the Simulacrum...!</style>",
                            });
                        }
                        break;
                }

                self.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= self.participatingPlayerCount;

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

                InfiniteTowerWaveController waveController = self.waveInstance.GetComponent<InfiniteTowerWaveController>();
                if (WConfig.cfgExtraDifficuly.Value)
                {
                    CombatDirector combatDirector = self.waveInstance.GetComponent<CombatDirector>();
                    combatDirector.eliteBias = Mathf.Min(combatDirector.eliteBias * 0.5f * (1 - (self.waveIndex - 60f) / 50f), combatDirector.eliteBias); //Why is it 1.5f in Simu anyways
                    combatDirector.eliteBias = Mathf.Max(combatDirector.eliteBias, 0.1f);

                    float creditsMulti = 1f + ((self.waveIndex - 1) * 2f / 100f);
                    if (creditsMulti > 3)
                    {
                        creditsMulti = 3;
                    }
                    waveController.immediateCreditsFraction *= creditsMulti;
                    //waveController.wavePeriodSeconds = Mathf.Max(waveController.wavePeriodSeconds * (1 - (self.waveIndex - 10) / 100 * 0.5f), waveController.wavePeriodSeconds * 0.6f);
                    Debug.Log("immediateCreditsFraction: " + waveController.immediateCreditsFraction + " eliteBias: " + combatDirector.eliteBias);
                }
                if (WConfig.cfgSpeedUpOnLaterWaves.Value)
                {
                    if (self.waveIndex % 5 == 0)
                    {
                        //waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 20, 1);
                        waveController.wavePeriodSeconds = waveController.wavePeriodSeconds - (self.waveIndex - 11) / 5;
                    }
                    else
                    {
                        waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 10, 1);
                        waveController.wavePeriodSeconds = waveController.wavePeriodSeconds - (self.waveIndex - 11) / 10;
                    }
                    //Debug.Log("secondsAfterWave: " + waveController.secondsAfterWave + " wavePeriodSeconds: " + waveController.wavePeriodSeconds);
                }
            }
        }


        public static void InfiniteTowerRun_OnWaveAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerRun.orig_OnWaveAllEnemiesDefeatedServer orig, InfiniteTowerRun self, InfiniteTowerWaveController wc)
        {
            orig(self, wc);
            if (self.IsStageTransitionWave())
            {
                Debug.Log("\nPreviousSceneDef " + PreviousSceneDef + "\n" + "CurrentSceneDef " + Stage.instance.sceneDef + "\n" + "NextSceneDef " + self.nextStageScene);
                if (PreviousSceneDef != null && PreviousSceneDef == self.nextStageScene)
                {
                    int preventInfiniteLoop = 0;
                    //Debug.Log("Preventing repeat scene");
                    do
                    {
                        preventInfiniteLoop++;
                        self.PickNextStageSceneFromCurrentSceneDestinations();
                        Debug.Log("ReplacementSceneDef " + self.nextStageScene);
                    }
                    while (self.nextStageScene == PreviousSceneDef && preventInfiniteLoop < 10);
                }
                PreviousSceneDef = Stage.instance.sceneDef;
            }
        }

        public static void InfiniteTowerWaveController_OnAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerWaveController.orig_OnAllEnemiesDefeatedServer orig, InfiniteTowerWaveController self)
        {
            orig(self);

            if (self.name.EndsWith("BossVoidElites(Clone)"))
            {
                self.gameObject.GetComponents<CombatDirector>()[0].enabled = false;
                self.gameObject.GetComponents<CombatDirector>()[1].enabled = false;
            }

            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveIndex >= SimuEndingStartAtXWaves && run.waveIndex % SimuEndingEveryXWaves == SimuEndingWaveRest)
            {
                GameObject EndingPortal = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(iscSimuExitPortal, new DirectorPlacementRule
                {
                    minDistance = 25f,
                    maxDistance = 35f,
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    position = run.safeWardController.transform.position,
                    spawnOnTarget = run.safeWardController.transform
                }, run.safeWardRng));
            }
        }


        public static void InfiniteTowerRun_CleanUpCurrentWave(On.RoR2.InfiniteTowerRun.orig_CleanUpCurrentWave orig, InfiniteTowerRun self)
        {
            if (self.waveInstance)
            {
                switch (self.waveInstance.name)
                {
                    case "InfiniteTowerWaveBossVoidElites(Clone)":
                        break;
                    case "InfiniteTowerWaveLunarElites(Clone)":
                    case "InfiniteTowerWaveVoidElites(Clone)":
                    case "InfiniteTowerWaveBossScavLunar(Clone)":
                    case "InfiniteTowerWaveMalachiteElites(Clone)":
                        CombatDirector.eliteTiers = EliteTiersBackup;
                        break;
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
                        self.enemyInventory.RemoveItem(RoR2Content.Items.BoostHp, self.enemyInventory.GetItemCount(RoR2Content.Items.BoostHp));
                        break;
                }
                /*ArtifactEnabler temp = self.waveInstance.GetComponent<ArtifactEnabler>();
                if (temp && temp.artifactDef == RoR2Content.Artifacts.EliteOnly && temp.artifactWasEnabled == false && Stage.instance)
                {
                    Stage.instance.singleMonsterTypeBodyIndex = BodyIndex.None;
                }*/
                if (Run.instance)
                {
                    Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                }

            }
            Debug.Log("WaveCleanUp  " + self.waveInstance);
            orig(self);
        }



        public static void AwaitingActivation_OnEnter(On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingActivation self)
        {
            //Debug.LogWarning((Run.instance as InfiniteTowerRun).waveInstance);
            if ((Run.instance as InfiniteTowerRun).waveInstance)
            {
                self.radius = 60;
            }
            else
            {
                self.radius = 25;
            }
            orig(self);
        }
        public static void Travelling_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Travelling self)
        {
            self.radius = 25;
            orig(self);
            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                //Maybe travel at like two times the speed on Stage 6
                float waves = Run.instance.GetComponent<InfiniteTowerRun>().Network_waveIndex;
                float speedMult = (1f + (waves - 10f) / 50f);

                //Stage 5 Onward
                if (waves > 30)
                {
                    speedMult += (waves - 25f) / 40f;
                    if (speedMult > 3) { speedMult = 3; }
                }
                if (speedMult > 1)
                {
                    self.zone.radius = Mathf.Min(self.radius * ((speedMult - 1f) / 1.5f + 1f), 60);
                    self.travelSpeed *= speedMult;
                    self.pathMaxSpeed *= speedMult;
                }
                self.travelSpeed += 1;
                self.pathMaxSpeed += 1;
                Debug.Log("Wave:" + waves + " speedMult:" + speedMult + " TravellingSpeed:" + self.travelSpeed + " Radius::" + self.zone.radius);
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
                    {
                        EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(characterBody.gameObject, "Body");
                        if (entityStateMachine && !(entityStateMachine.state is EntityStates.Idle))
                        {
                            entityStateMachine.SetInterruptState(new EntityStates.Idle(), EntityStates.InterruptPriority.Frozen);
                        }
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

    public class EliteInclusiveDropTable : BasicPickupDropTable
    {
        public float eliteEquipWeight;
        public float pearlWeight;

        public override void Regenerate(Run run)
        {
            base.GenerateWeightedSelection(run);
            if (eliteEquipWeight > 0)
            {
                List<PickupIndex> EliteList = new List<PickupIndex>();
                for (int i = 0; i < EliteCatalog.eliteDefs.Length; i++)
                {
                    if (!(EliteCatalog.eliteDefs[i].name.StartsWith("edGold") || EliteCatalog.eliteDefs[i].name.StartsWith("edSecretSpeed")))
                    {
                        if (EliteCatalog.eliteDefs[i].IsAvailable() && EliteCatalog.eliteDefs[i].eliteEquipmentDef.dropOnDeathChance > 0)
                        {
                            PickupIndex temp = PickupCatalog.FindPickupIndex(EliteCatalog.eliteDefs[i].eliteEquipmentDef.equipmentIndex);
                            if (!EliteList.Contains(temp))
                            {
                                EliteList.Add(temp);
                            }
                        }
                    }
                }
                this.Add(EliteList, eliteEquipWeight);
            }
            if (pearlWeight > 0)
            {
                this.selector.AddChoice(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), pearlWeight * 0.8f);
                this.selector.AddChoice(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), pearlWeight * 0.2f);
            }
        }
    }

    public class SimulacrumExtrasHelper : MonoBehaviour
    {
        public bool hasDone = false;
        [SerializeField]
        public ItemTier rewardDisplayTier;
        [SerializeField]
        public PickupDropTable rewardDropTable;

        public float newRadius = 0;
    }

    public class SimuJetpackWaveHelper : NetworkBehaviour
    {
        protected CombatSquad combatSquad;

        private void OnEnable()
        {
            InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();
            if (controller && controller.combatSquad)
            {
                combatSquad = controller.combatSquad;
                controller.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
            }
        }

        private void OnDisable()
        {
            if (this.combatSquad)
            {
                this.combatSquad.onMemberDiscovered -= this.OnCombatSquadMemberDiscovered;
            }
        }

        protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
        {
            GameObject bodyObj = master.GetBodyObject();
            if (bodyObj)
            {
                if (!bodyObj.GetComponent<EquipmentSlot>())
                {
                    EquipmentSlot slot = bodyObj.AddComponent<EquipmentSlot>();
                    slot.hasEffectiveAuthority = true;
                    slot._rechargeTime = Run.FixedTimeStamp.zero;
                }
            }


            if (master.inventory.currentEquipmentIndex == EquipmentIndex.None)
            {
                master.inventory.SetEquipmentIndex(RoR2Content.Equipment.Jetpack.equipmentIndex);
                master.inventory.GiveItem(RoR2Content.Items.AutoCastEquipment, 1);
                master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 7);
                master.inventory.GiveItem(RoR2Content.Items.SprintOutOfCombat, 1);
                master.inventory.GiveItem(RoR2Content.Items.BoostHp, 2 + Run.instance.stageClearCount * 4);
            }
            else
            {
                GameObject Jetpack = Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/BodyAttachments/JetpackController"));
                Jetpack.GetComponent<JetpackController>().duration = 1000;
                Jetpack.GetComponent<NetworkedBodyAttachment>().AttachToGameObjectAndSpawn(master.bodyInstanceObject, null);
            }
        }
    }
}