using BepInEx;
using MonoMod.Cil;
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
    [BepInPlugin("Wolfo.SimulacrumAdditions", "SimulacrumAdditions", "1.6.1")]
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
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave11OrGreaterPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveCountPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Wave11OrGreaterPrerequisite.asset").WaitForCompletion();
        //public static RoR2.InfiniteTowerWaveCountPrerequisites Wave21OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave26OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave31OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        public static RoR2.InfiniteTowerWaveCountPrerequisites Wave45OrGreaterPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveCountPrerequisites>();
        //
        //
        //Simu Wave Reward Drop Tables
        public static BasicPickupDropTable dtITWaveTier1 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITDefaultWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier2 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITWaveTier3 = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITSpecialBossWave.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITVoid = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITVoid.asset").WaitForCompletion();
        public static BasicPickupDropTable dtITLunar = Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/dtITLunar.asset").WaitForCompletion();

        public static EliteInclusiveDropTable dtAllTier = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static EliteInclusiveDropTable dtBossMixTier = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITFamilyWaveUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITBasicWaveOnKill = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static EliteInclusiveDropTable dtITBasicBonusLunar = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static BasicPickupDropTable dtITBasicBonusVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITBasicBonusGreen = ScriptableObject.CreateInstance<BasicPickupDropTable>();

        public static BasicPickupDropTable dtITSpecialEquipment = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITVoidInfestorWave = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITSuperVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static EliteInclusiveDropTable dtITSpecialBossYellow = ScriptableObject.CreateInstance<EliteInclusiveDropTable>();
        public static ExplicitPickupDropTable dtITHeresy = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();
        public static ExplicitPickupDropTable dtITWurms = ScriptableObject.CreateInstance<ExplicitPickupDropTable>();

        public static SceneDef PreviousSceneDef = null;
        public static CombatDirector.EliteTierDef[] EliteTiersBackup;
        public static ItemDef ITDamageDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITAttackSpeedDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHealthScaling = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITKillOnCompletion = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHorrorName = ScriptableObject.CreateInstance<ItemDef>();
        public static CostTypeDef CostTypeVoidCoinBlood;
        public static CostTypeIndex CostIndexVoidCoinBlood;
        public static MasterCatalog.MasterIndex IndexAffixHealingCore = MasterCatalog.MasterIndex.none;

        public void Awake()
        {
            WConfig.InitConfig();
            if (WConfig.cfgDumpInfo.Value)
            {
                DumpAllWaveInfo(ITBasicWaves);
                DumpAllWaveInfo(ITBossWaves);
            }

            MakePortal();
            SetupConstants();

            SimuChanges();
        
            SimulacrumWavesArtifacts.Start();
            SimulacrumWavesFamily.Start();
            SimuWavesMisc.Start();
            SimulacrumDCCS.Start();

            GiantGup.Start();
            SuperMegaCrab.Start();

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
                    self.lunarCoinChanceMultiplier *= 2;
                    self.master.GiveVoidCoins(1);
                    self.gameObject.AddComponent<VoidCoinChance>();
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
            //On.RoR2.ClassicStageInfo.RebuildCards += SimulacrumDCCS.SimuInteractableDCCSAdder;
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
                if (temp && !temp.hasDone && temp.rewardDropTable != null)
                {
                    temp.hasDone = true;
                    self.rewardDisplayTier = temp.rewardDisplayTier;
                    self.rewardDropTable = temp.rewardDropTable;
                    self.rewardOptionCount = temp.rewardOptions;
                    self.DropRewards();
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
            VoidPotential.transform.GetChild(0).GetChild(1).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(2).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(3).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(4).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(5).localPosition = new Vector3(0, -0.5f, 0);
            VoidPotential.transform.GetChild(0).GetChild(6).localPosition = new Vector3(0, -0.5f, 0);
            OptionPickerPanel.GetComponent<RoR2.UI.PickupPickerPanel>().maxColumnCount = 3;

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
            //
            //
            VoidCoinStuff();
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
            //
            //LanguageAPI.Add("INFINITETOWER_SUDDEN_DEATH", "<style=cWorldEvent>[WARNING] The Focus begins to falter..</style>", "en");

        }

        private static void VoidCoinStuff()
        {
            CostTypeCatalog.modHelper.getAdditionalEntries += addVoidBloodCost;

            if (WConfig.cfgVoidCoins.Value)
            {
                GameObject VoidCoinBarrel = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel");
                ChestBehavior VoidBarrelChestBehavior = VoidCoinBarrel.AddComponent<ChestBehavior>();
                VoidBarrelChestBehavior.dropTable = LegacyResourcesAPI.Load<ExplicitPickupDropTable>("DropTables/dtVoidCoin");
                VoidBarrelChestBehavior.dropTransform = VoidCoinBarrel.transform.GetChild(0).GetComponent<ChildLocator>().FindChild("Bulb");
                VoidBarrelChestBehavior.dropUpVelocityStrength = 20;
                VoidBarrelChestBehavior.enabled = false;
            }
        }

        public static void VoidCoinRunStart()
        {
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidChest").GetComponent<PurchaseInteraction>().costType = (CostTypeIndex)CostIndexVoidCoinBlood;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidTriple").GetComponent<PurchaseInteraction>().costType = (CostTypeIndex)CostIndexVoidCoinBlood;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel").GetComponent<ChestBehavior>().enabled = true;
            GlobalEventManager.onCharacterDeathGlobal += DropVoidCoin;
        }

        public static void VoidCoinRunEnd()
        {
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidChest").GetComponent<PurchaseInteraction>().costType = CostTypeIndex.PercentHealth;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidTriple").GetComponent<PurchaseInteraction>().costType = CostTypeIndex.PercentHealth;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel").GetComponent<ChestBehavior>().enabled = false;
            GlobalEventManager.onCharacterDeathGlobal -= DropVoidCoin;
        }

        private static void DropVoidCoin(DamageReport damageReport)
        {
            CharacterMaster characterMaster = damageReport.attackerMaster;
            if (characterMaster)
            {
                VoidCoinChance component = characterMaster.GetComponent<VoidCoinChance>();
                if (component && Util.CheckRoll(component.chance, 0f, null))
                {
                    PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(DLC1Content.MiscPickups.VoidCoin.miscPickupIndex), damageReport.victim.transform.position, Vector3.up * 25f);
                    component.chance *= 0.65f;
                }
            }
        }

        internal static void addVoidBloodCost(List<CostTypeDef> obj)
        {
            //Blood <color=#CE2929>
            //Blood <color=#CE2929>
            //Void Coin <color=#F4ADFA>
            //<color=#FF70FF><sprite name=\"VoidCoin\" tint=1>1 or </color><color=#CE2929>{0}% HP</color>"
            //<color=#FF24FF><sprite name=\"VoidCoin\" tint=1>1</color> or <color=#671414>{0}% HP</color>"
            //LanguageAPI.Add("COST_VOIDCOINBLOOD_FORMAT", "<color=#FF40FF><sprite name=\"VoidCoin\" tint=1><color=#FF5BFF>1 or <color=#CE2929>{0}% HP</color></color></color>", "en");
            LanguageAPI.Add("COST_VOIDCOINBLOOD_FORMAT", "<color=#FF349F><sprite name=\"VoidCoin\" tint=1>1 or <color=#DA1D1D>{0}% HP</color></color>", "en");
            //Cost Tier Thing
            CostTypeVoidCoinBlood = new CostTypeDef();
            //CostTypeVoidCoinBlood.name = "VoidCoinOrBlood";
            CostTypeVoidCoinBlood.costStringFormatToken = "COST_VOIDCOINBLOOD_FORMAT";
            CostTypeVoidCoinBlood.colorIndex = ColorCatalog.ColorIndex.VoidItemDark;
            CostTypeVoidCoinBlood.saturateWorldStyledCostString = false;
            CostTypeVoidCoinBlood.darkenWorldStyledCostString = true;
            CostTypeVoidCoinBlood.isAffordable = delegate (CostTypeDef costTypeDef, CostTypeDef.IsAffordableContext context)
            {
                CharacterBody body = context.activator.GetComponent<CharacterBody>();
                HealthComponent component = context.activator.GetComponent<HealthComponent>();
                return body && body.master.voidCoins >= (ulong)((long)1) || component && component.combinedHealth / component.fullCombinedHealth * 100f >= (float)context.cost;
            };
            CostTypeVoidCoinBlood.payCost = delegate (CostTypeDef costTypeDef, CostTypeDef.PayCostContext context)
            {
                if (context.activatorMaster && context.activatorMaster.voidCoins > 0)
                {
                    context.activatorMaster.voidCoins -= (uint)1;
                }
                else
                {
                    HealthComponent component = context.activator.GetComponent<HealthComponent>();
                    if (component)
                    {
                        float combinedHealth = component.combinedHealth;
                        float num = component.fullCombinedHealth * (float)context.cost / 100f;
                        if (combinedHealth > num)
                        {
                            component.TakeDamage(new DamageInfo
                            {
                                damage = num,
                                attacker = context.purchasedObject,
                                position = context.purchasedObject.transform.position,
                                damageType = (DamageType.NonLethal | DamageType.BypassArmor | DamageType.BypassBlock)
                            });
                        }
                    }
                }
            };
            CostIndexVoidCoinBlood = (CostTypeIndex)CostTypeCatalog.costTypeDefs.Length + obj.Count;
            obj.Add(CostTypeVoidCoinBlood);
        }

        private void KillAllGhosts_StartTimer(On.RoR2.InfiniteTowerWaveController.orig_StartTimer orig, InfiniteTowerWaveController self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                foreach (CharacterMaster characterMaster in CharacterMaster.readOnlyInstancesList)
                {
                    int itemCount = characterMaster.inventory.GetItemCount(ITKillOnCompletion);
                    if (itemCount > 0)
                    {
                        characterMaster.inventory.GiveItem(RoR2Content.Items.HealthDecay, 1);
                    }
                }
            }
        }

        //Yknow i'd love to use recalculate stats api if it fukin worked
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
                }
            }
        }

        //GivePickupOnStarts runs after this
        //Just because we null this doesn't mean it didn't get added to the combat squad
        private void InfiniteTowerWaveController_OnCombatSquadMemberDiscovered(On.RoR2.InfiniteTowerWaveController.orig_OnCombatSquadMemberDiscovered orig, InfiniteTowerWaveController self, CharacterMaster master)
        {
            orig(self, master);
            if (NetworkServer.active)
            {
                int kill = master.inventory.GetItemCount(ITKillOnCompletion);
                if (kill > 0)
                {
                    self.combatSquad.RemoveMember(master);

                    CharacterBody body = master.GetBody();
                    body.AddBuff(RoR2Content.Buffs.Immune);

                    RoR2.CharacterAI.BaseAI tempAI = master.GetComponent<RoR2.CharacterAI.BaseAI>();
                    for (int i = 0; i < tempAI.skillDrivers.Length; i++)
                    {
                        tempAI.skillDrivers[i].maxUserHealthFraction = 1;
                    }
                }
                else if (self.hasEnabledEnemyIndicators && master.masterIndex == IndexAffixHealingCore)
                {
                    self.combatSquad.RemoveMember(master);
                }
            }
        }

        private void InfiniteTowerRunPreStartClient(On.RoR2.InfiniteTowerRun.orig_PreStartClient orig, InfiniteTowerRun self)
        {
            orig(self);

            ITBasicWaves.wavePrefabs[0].weight = 0f;
            ITBasicWaves.GenerateWeightedSelection();
            ITBossWaves.wavePrefabs[0].weight = 0f;
            ITBasicWaves.GenerateWeightedSelection();

            if (ITBossWaves.wavePrefabs[0].weight == 1)
            {
                ITBasicWaves.wavePrefabs[0].weight = 80f;
                ITBasicWaves.GenerateWeightedSelection();
                ITBossWaves.wavePrefabs[0].weight = 90f;
                ITBasicWaves.GenerateWeightedSelection();
            }
            SimulacrumDCCS.MakeITSand(true);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoinRunStart();
            }
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = false;
            Destroy(eqDrone.GetComponent<RoR2.SetDontDestroyOnLoad>());
        }

        private void InfiniteTowerRunEnd(On.RoR2.InfiniteTowerRun.orig_OnDestroy orig, InfiniteTowerRun self)
        {
            orig(self);
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<RoR2.StartEvent>().enabled = true;
            eqDrone.AddComponent<RoR2.SetDontDestroyOnLoad>();
            SimulacrumDCCS.MakeITSand(false);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoinRunEnd();
            }
        }


        private void GiveStatBoosts_ExplicitSpawnWaveController_Initialize(On.RoR2.InfiniteTowerExplicitSpawnWaveController.orig_Initialize orig, InfiniteTowerExplicitSpawnWaveController self, int waveIndex, Inventory enemyInventory, GameObject spawnTargetObject)
        {
            if (NetworkServer.active)
            {
                float bonusspecialmultiplier = 1;
                bool breachWave = false;
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
                        }
                        if (waveIndex > 24)
                        {
                            self.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].count += (int)(waveIndex / 20f);
                        }
                        break;
                    case "InfiniteTowerWaveBossVoidRaidCrab(Clone)":
                        bonusspecialmultiplier = 1.25f;
                        break;
                    case "InfiniteTowerWaveBossBrother(Clone)":
                        bonusspecialmultiplier = 2.5f;
                        break;
                    case "InfiniteTowerWaveBossGiantGup(Clone)":
                        bonusspecialmultiplier = 1.5f;
                        break;
                    case "InfiniteTowerWaveBossSuperRoboBallBoss(Clone)":
                        bonusspecialmultiplier = 0.9f;
                        break;
                    case "InfiniteTowerWaveBossTitanGold(Clone)":
                        bonusspecialmultiplier = 1.1f;
                        break;
                    case "InfiniteTowerWaveBasicGhostHaunting(Clone)":
                        bonusspecialmultiplier = 1f;
                        //It won't be 2 of the same ghost.
                        self.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].count += (waveIndex / 24);
                        break;
                    case "InfiniteTowerWaveBrotherHaunt(Clone)":
                        bonusspecialmultiplier = 0.75f;
                        if (waveIndex > 34)
                        {
                            self.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].count++;
                        }
                        break;
                    case "InfiniteTowerWaveBossGhostHaunting(Clone)":
                        //Explicit Spawn waves that shouldn't have special scaling
                        bonusspecialmultiplier = 0.2f;
                        if (waveIndex > 34)
                        {
                            self.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].count += (waveIndex / 34);
                        }
                        break;
                    case "InfiniteTowerWaveBossCharacters(Clone)":
                        bonusspecialmultiplier = 0.6f;
                        breachWave = true;
                        break;
                }
                //Debug.Log(Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.wardStateMachine.state);

                if (bonusspecialmultiplier > 0)
                {
                    float num = 1f;
                    float num2 = 1f;
                    num += Run.instance.difficultyCoefficient / 2.5f * System.Math.Max(1, (waveIndex / 10) * 0.214f + 0.214f);
                    num2 += Run.instance.difficultyCoefficient / 30f * System.Math.Max(1, (waveIndex / 10) * 0.05f);
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

                if (breachWave)
                {
                    self.enemyInventory = enemyInventory;

                    self.combatDirector.teamIndex = TeamIndex.Void;
                    for (int i = 0; i < (self.spawnList[0].spawnCard as MultiCharacterSpawnCard).masterPrefabs.Length; i++)
                    {
                        (self.spawnList[0].spawnCard as MultiCharacterSpawnCard).masterPrefabs[i].GetComponent<CharacterMaster>().bodyPrefab.transform.GetChild(0).localScale *= 1.5f;
                    }
                    CombatDirector combatDirector = self.combatDirector;
                    SpawnCard spawnCard = self.spawnList[0].spawnCard;
                    EliteDef eliteDef = self.spawnList[0].eliteDef;
                    Transform transform = spawnTargetObject.transform;
                    bool preventOverhead = self.spawnList[0].preventOverhead;
                    combatDirector.Spawn(spawnCard, eliteDef, transform, self.spawnList[0].spawnDistance, preventOverhead, 1f, DirectorPlacementRule.PlacementMode.Approximate);

                    for (int i = 0; i < (self.spawnList[0].spawnCard as MultiCharacterSpawnCard).masterPrefabs.Length; i++)
                    {
                        (self.spawnList[0].spawnCard as MultiCharacterSpawnCard).masterPrefabs[i].GetComponent<CharacterMaster>().bodyPrefab.transform.GetChild(0).localScale = new Vector3(1, 1, 1);
                    }
                    self.combatDirector.teamIndex = TeamIndex.Monster;
                }
            }
            orig(self, waveIndex, enemyInventory, spawnTargetObject);
        }

        private void MoreItems_AdvanceWave(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            if (self.waveIndex > 60)
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
            if (WConfig.cfgExtraDifficuly.Value)
            {
                if (self.waveIndex > 20)
                {
                    if (self._waveIndex % 10 == 1)
                    {
                        //self.enemyInventory.GiveItem(ITHealthScaling,10);
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
            EndingPortal.transform.GetChild(2).GetChild(4).localScale *= 0.5f;
            EndingPortal.transform.GetChild(2).GetChild(4).GetComponent<EntityLocator>().entity = EndingPortal;
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
            //SimulacrumWavesFamily.ModSupport();
            //SimulacrumWavesArtifacts.ModSupport();
            //SimuWavesMisc.ModSupport();
            //SimuWavesMisc.MakeLater();
            SimuWavesMisc.LateChanges();
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

            DLC1Content.Items.AttackSpeedAndMoveSpeed.tags = DLC1Content.Items.AttackSpeedAndMoveSpeed.tags.Remove(ItemTag.Utility);

            //RoR2Content.Items.PersonalShield.tags = RoR2Content.Items.PersonalShield.tags.Add(ItemTag.Healing);
            DLC1Content.Items.ImmuneToDebuff.tags = DLC1Content.Items.ImmuneToDebuff.tags.Add(ItemTag.Healing);


            RoR2Content.Items.BonusGoldPackOnKill.tags = RoR2Content.Items.BonusGoldPackOnKill.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.Infusion.tags = RoR2Content.Items.Infusion.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.GoldOnHit.tags = RoR2Content.Items.GoldOnHit.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.RegeneratingScrap.tags = DLC1Content.Items.RegeneratingScrap.tags.Add(ItemTag.AIBlacklist);

            RoR2Content.Items.NovaOnHeal.tags = RoR2Content.Items.NovaOnHeal.tags.Add(ItemTag.AIBlacklist);
            RoR2Content.Items.ShockNearby.tags = RoR2Content.Items.ShockNearby.tags.Add(ItemTag.Count);
            DLC1Content.Items.MoreMissile.tags = DLC1Content.Items.MoreMissile.tags.Add(ItemTag.Count);
            DLC1Content.Items.CritDamage.tags = DLC1Content.Items.CritDamage.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.DroneWeapons.tags = DLC1Content.Items.DroneWeapons.tags.Add(ItemTag.AIBlacklist);
            //RoR2Content.Items.GhostOnKill.tags = RoR2Content.Items.GhostOnKill.tags.Add(ItemTag.AIBlacklist);

            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.SprintRelated);
            DLC1Content.Items.ElementalRingVoid.tags = DLC1Content.Items.ElementalRingVoid.tags.Remove(ItemTag.Utility);

            DLC1Content.Items.TreasureCacheVoid.tags = DLC1Content.Items.TreasureCacheVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.CritGlassesVoid.tags = DLC1Content.Items.CritGlassesVoid.tags.Add(ItemTag.Count);
            DLC1Content.Items.MushroomVoid.tags = DLC1Content.Items.MushroomVoid.tags.Add(ItemTag.AIBlacklist);
            DLC1Content.Items.EquipmentMagazineVoid.tags = DLC1Content.Items.EquipmentMagazineVoid.tags.Add(ItemTag.AIBlacklist);

            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                InfiniteTowerWaveController controller = ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>();
                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                //controller.wavePeriodSeconds = 25;
                if (controller.baseCredits > 199)
                {
                    //Stupid but whatevs
                    controller.baseCredits -= 20;
                }
                if (controller is InfiniteTowerExplicitSpawnWaveController)
                {
                    controller.isBossWave = false;
                    controller.uiPrefab = ITBasicWaves.wavePrefabs[0].wavePrefab.GetComponent<InfiniteTowerWaveController>().uiPrefab;
                };
            }
            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                InfiniteTowerWaveController controller = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>();
                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }

                if (controller.wavePeriodSeconds == 60)
                {
                    controller.wavePeriodSeconds = 50;
                }
                else
                {
                    //controller.wavePeriodSeconds = 50;
                }

            }
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
            GameEndingDef VoidEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/DLC1/GameModes/VoidEnding.asset").WaitForCompletion();
            GameEndingDef ObliterationEnding = Addressables.LoadAssetAsync<GameEndingDef>(key: "RoR2/Base/ClassicRun/ObliterationEnding.asset").WaitForCompletion();

            ContentAddition.AddGameEndingDef(InfiniteTowerEnding);

            InfiniteTowerEnding.endingTextToken = "Simulation Suspended";
            InfiniteTowerEnding.lunarCoinReward = 10;
            InfiniteTowerEnding.showCredits = false;
            InfiniteTowerEnding.isWin = true;
            InfiniteTowerEnding.gameOverControllerState = ObliterationEnding.gameOverControllerState;
            InfiniteTowerEnding.material = VoidEnding.material;
            InfiniteTowerEnding.icon = VoidEnding.icon;
            InfiniteTowerEnding.backgroundColor = new Color(0.65f, 0.3f, 0.5f, 0.8f);
            InfiniteTowerEnding.foregroundColor = new Color(0.75f, 0.4f, 0.55f, 1);
            InfiniteTowerEnding.cachedName = "InfiniteTowerEnding";
            //

            //PreRequs
            AfterWave5Prerequisite.minimumWaveCount = 6;
            AfterWave5Prerequisite.name = "AfterWave5Prerequisite";
            Wave26OrGreaterPrerequisite.minimumWaveCount = 26;
            Wave26OrGreaterPrerequisite.name = "Wave26OrGreaterPrerequisite";
            Wave31OrGreaterPrerequisite.minimumWaveCount = 31;
            Wave31OrGreaterPrerequisite.name = "Wave31OrGreaterPrerequisite";
            Wave45OrGreaterPrerequisite.minimumWaveCount = 45;
            Wave45OrGreaterPrerequisite.name = "Wave45OrGreaterPrerequisite";

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
            dtAllTier.eliteEquipWeight = 1;
            dtAllTier.lunarEquipmentWeight = 1;
            dtAllTier.pearlWeight = 10;

            if (WConfig.cfgVoidTripleAllTier.Value == true)
            {
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidTriple/VoidTriple.prefab").WaitForCompletion().GetComponent<RoR2.OptionChestBehavior>().dropTable = dtAllTier;
            }

            dtBossMixTier.name = "dtBossMixTier";
            dtBossMixTier.tier1Weight = 0;
            dtBossMixTier.tier2Weight = 80;
            dtBossMixTier.tier3Weight = 8;
            dtBossMixTier.bossWeight = 5;
            dtBossMixTier.voidTier1Weight = 40;
            dtBossMixTier.voidTier2Weight = 45;
            dtBossMixTier.voidTier3Weight = 5;
            dtBossMixTier.voidBossWeight = 2;
            dtBossMixTier.pearlWeight = 10;

            //OnKill for On Kill Artifacts
            dtITBasicWaveOnKill.tier1Weight = 80;
            dtITBasicWaveOnKill.tier2Weight = 8;
            dtITBasicWaveOnKill.tier3Weight = 0.75f;
            dtITBasicWaveOnKill.bossWeight = 0.25f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";
            dtITBasicWaveOnKill.requiredItemTags = new ItemTag[] { ItemTag.OnKillEffect };
            //

            //For Basic waves intended to be difficult
            dtITBasicBonusGreen.tier1Weight = 20;
            dtITBasicBonusGreen.tier2Weight = 80;
            dtITBasicBonusGreen.tier3Weight = 0f;
            dtITBasicBonusGreen.bossWeight = 0;
            dtITBasicBonusGreen.bannedItemTags = new ItemTag[] { };
            dtITBasicBonusGreen.name = "dtITBasicBonusGreen";

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
            dtITBasicBonusLunar.lunarCombinedWeight = 65;
            dtITBasicBonusLunar.pearlWeight = 100; //It's just 2 entries they'll be rare
            dtITBasicBonusLunar.name = "dtITBasicBonusLunar";

            //Void Infestor Boss Wave
            dtITVoidInfestorWave.tier1Weight = 0;
            dtITVoidInfestorWave.tier2Weight = 0;
            dtITVoidInfestorWave.tier3Weight = 0;
            dtITVoidInfestorWave.bossWeight = 35;
            dtITVoidInfestorWave.voidTier1Weight = 60;
            dtITVoidInfestorWave.voidTier2Weight = 30;
            dtITVoidInfestorWave.voidTier3Weight = 8;
            dtITVoidInfestorWave.voidBossWeight = 10;
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
            dtITSpecialBossYellow.bossWeight = 75; //Because how weighted selections actually work, boss items will be a lot less common
            dtITSpecialBossYellow.name = "dtITSpecialBossYellow";
            dtITSpecialBossYellow.eliteEquipWeight = 5f;
            dtITSpecialBossYellow.pearlWeight = 70f;
            dtITSpecialBossYellow.voidBossWeight = 5;

            //Family Waves biased 
            dtITFamilyWaveDamage.tier1Weight = 80;
            dtITFamilyWaveDamage.tier2Weight = 10; //Move 4 Points to boss
            dtITFamilyWaveDamage.tier3Weight = 0.25f;
            dtITFamilyWaveDamage.bossWeight = 5f;
            dtITFamilyWaveDamage.name = "dtITFamilyWaveDamage";
            dtITFamilyWaveDamage.requiredItemTags = new ItemTag[] { ItemTag.Damage };

            dtITFamilyWaveHealing.tier1Weight = 80;
            dtITFamilyWaveHealing.tier2Weight = 10;
            dtITFamilyWaveHealing.tier3Weight = 0.25f;
            dtITFamilyWaveHealing.bossWeight = 5f;
            dtITFamilyWaveHealing.name = "dtITFamilyWaveHealing";
            dtITFamilyWaveHealing.requiredItemTags = new ItemTag[] { ItemTag.Healing };

            dtITFamilyWaveUtility.tier1Weight = 80;
            dtITFamilyWaveUtility.tier2Weight = 10;
            dtITFamilyWaveUtility.tier3Weight = 0.25f;
            dtITFamilyWaveUtility.bossWeight = 5f;
            dtITFamilyWaveUtility.name = "dtITFamilyWaveUtility";
            dtITFamilyWaveUtility.requiredItemTags = new ItemTag[] { ItemTag.Utility };
            //
            //In addition to the regular green so keep it mostly Orange
            dtITSpecialEquipment.requiredItemTags = new ItemTag[] { ItemTag.EquipmentRelated };
            dtITSpecialEquipment.tier1Weight = 0f;
            dtITSpecialEquipment.tier2Weight = 30f;
            dtITSpecialEquipment.tier3Weight = 20f;
            dtITSpecialEquipment.bossWeight = 0f;
            dtITSpecialEquipment.lunarItemWeight = 20f;
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
            SceneDef itfrozenwall = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
            SceneDef itdampcave = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();

            itgolemplains.mainTrack = MusicVoidFields;
            itfrozenwall.mainTrack = MusicSnowyForest;
            itdampcave.mainTrack = MusicVoidStage;


            ITSuperBossWaves.name = "SuperBossWaves";
            ITSuperBossWaves.availabilityPeriod = 30;
            ITSuperBossWaves.minWaveIndex = 50;

            ITModSupportWaves.name = "ITModSupportWaves";
            ITModSupportWaves.availabilityPeriod = 999;
            ITModSupportWaves.minWaveIndex = 999;


            float ITSpecialBossWaveWeight = 2.5f;
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                switch (ITBasicWaves.wavePrefabs[i].wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactEnigma":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardOptionCount = 2;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITSpecialEquipment;
                        //Orange tier set later
                        ITBasicWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
                        break;
                    case "InfiniteTowerWaveArtifactGlass":
                    case "InfiniteTowerWaveArtifactWeakAssKnees":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 179;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        ITBasicWaves.wavePrefabs[i].weight = 3;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 179;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 179;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().baseCredits = 179;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        ITBasicWaves.wavePrefabs[i].weight = 3;
                        break;
                };
            }


            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your equipment changes every time it's activated.";
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactSingleMonsterTypeWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters will be of only one type.";

            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();

            ITBossWaves.wavePrefabs[0].weight = 90;

            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBossVoid"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 6f;
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
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight + 0.5f;
                    ITSuperBossWaves.wavePrefabs = ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);

                    InfiniteTowerExplicitSpawnWaveController temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                    temp.rewardDisplayTier = ItemTier.Boss;
                    temp.rewardDropTable = dtITSpecialBossYellow;
                    temp.baseCredits = 100;
                    temp.linearCreditsPerWave = 4;
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
                else if (!(category.wavePrefabs[i].prerequisites is InfiniteTowerWavePrerequisites)) ;
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
                if (ITBossWaves.wavePrefabs[0].weight > 1)
                {
                    ITBasicWaves.wavePrefabs[0].weight = 40; //More Wackies past this
                    ITBasicWaves.GenerateWeightedSelection();
                    ITBossWaves.wavePrefabs[0].weight = 1; //More Wackies past this
                    ITBossWaves.GenerateWeightedSelection();
                }
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
                            costMultiplier = CombatDirector.baseEliteCostMultiplier * 4f,
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

        public static void InfiniteTowerRun_BeginNextWave(On.RoR2.InfiniteTowerRun.orig_InitializeWaveController orig, global::RoR2.InfiniteTowerRun self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                GoldTitanManager.TryStartChannelingTitansServer(self.safeWardController.gameObject, self.safeWardController.gameObject.transform.position, null, null);
            }
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
                        if (NetworkServer.active)
                        {
                            int decide = WRect.random.Next(0, 2);
                            if (decide == 0)
                            {
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarPrimaryReplacement);
                                self.enemyInventory.GiveItem(ITDamageDown, 45); //Later waves will have too much damage anyways
                                self.enemyInventory.GiveItem(ITAttackSpeedDown, 70);
                                self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, Run.instance.stageClearCount * 2);
                                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                                {
                                    baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Visions of Heresy</color> has been integrated into the Simulacrum...!</style>",
                                });
                            }
                            else
                            {
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarSecondaryReplacement);
                                self.enemyInventory.GiveItem(RoR2Content.Items.LunarUtilityReplacement);
                                self.enemyInventory.GiveItem(RoR2Content.Items.BoostHp, Run.instance.stageClearCount * 2);
                                self.enemyInventory.GiveItem(ITDamageDown, 5);
                                Chat.SendBroadcastChat(new Chat.SimpleChatMessage
                                {
                                    baseToken = "<style=cWorldEvent>[WARNING] <color=#307FFF>Hooks and Strides of Heresy</color> have been integrated into the Simulacrum...!</style>",
                                });
                            }
                        }
                        break;
                    case "InfiniteTowerWaveCoffee(Clone)":
                        if (NetworkServer.active)
                        {
                            self.enemyInventory.GiveItem(DLC1Content.Items.AttackSpeedAndMoveSpeed, 6 + Run.instance.stageClearCount * 2); //6 on stage 1, 8 stage2, 10 stage3
                        }
                        break;
                    case "InfiniteTowerWaveLepton(Clone)":
                        RoR2Content.Items.TPHealingNova.tags = RoR2Content.Items.TPHealingNova.tags.Remove(ItemTag.CannotCopy);
                        if (NetworkServer.active)
                        {
                            self.enemyInventory.GiveItem(RoR2Content.Items.TPHealingNova, 2 + Run.instance.stageClearCount - 3);
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
                    case "InfiniteTowerWaveBlindness(Clone)":
                        if (NetworkServer.active)
                        {
                            foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                            {
                                if (player.master.bodyInstanceObject)
                                {
                                    player.master.GetBody().AddBuff(DLC1Content.Buffs.Blinded);
                                }
                            }
                        }
                        break;
                    case "InfiniteTowerWaveBasicGhostHaunting(Clone)":
                    case "InfiniteTowerWaveBossGhostHaunting(Clone)":
                        break;

                }

                if (NetworkServer.active)
                {
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
                }
            }
            self.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
            InfiniteTowerWaveController waveController = self.waveInstance.GetComponent<InfiniteTowerWaveController>();
            if (WConfig.cfgExtraDifficuly.Value)
            {
                CombatDirector combatDirector = self.waveInstance.GetComponent<CombatDirector>();
                combatDirector.eliteBias = Mathf.Min(combatDirector.eliteBias * 0.5f * (1 - (self.waveIndex - 50f) / 50f), combatDirector.eliteBias); //Why is it 1.5f in Simu anyways
                combatDirector.eliteBias = Mathf.Max(combatDirector.eliteBias, 0.1f);

                float creditsMulti = 1f + ((self.waveIndex - 1) * 2f / 100f);
                if (creditsMulti > 3)
                {
                    creditsMulti = 3;
                }
                waveController.immediateCreditsFraction *= creditsMulti;
                Debug.Log("immediateCreditsFraction: " + waveController.immediateCreditsFraction + " eliteBias: " + combatDirector.eliteBias);
            }
            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                if (self.waveIndex % 5 == 0)
                {
                    //waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 20, 1);
                    //waveController.wavePeriodSeconds -= Mathf.Min(waveController.wavePeriodSeconds / 250 * self.waveIndex, 20);
                }
                else
                {
                    waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 10, 1);
                    //waveController.wavePeriodSeconds -= Mathf.Min(waveController.wavePeriodSeconds / 250 * self.waveIndex, 10);
                }
                if (self.waveIndex < 11)
                {
                    waveController.wavePeriodSeconds += 5;
                    waveController.maxSquadSize = 15;
                }
                else if (self.waveIndex > 20)
                {
                    waveController.wavePeriodSeconds = (waveController.wavePeriodSeconds / 6f) * 5f;
                }
                else if (self.waveIndex > 50)
                {
                    waveController.wavePeriodSeconds = (waveController.wavePeriodSeconds / 3f) * 2f;
                }
                //Debug.Log("secondsAfterWave: " + waveController.secondsAfterWave + " wavePeriodSeconds: " + waveController.wavePeriodSeconds);
            }

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
            if (NetworkServer.active)
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
                            self.enemyInventory.RemoveItem(ITAttackSpeedDown, self.enemyInventory.GetItemCount(ITAttackSpeedDown));
                            self.enemyInventory.RemoveItem(RoR2Content.Items.BoostHp, self.enemyInventory.GetItemCount(RoR2Content.Items.BoostHp));
                            break;
                        case "InfiniteTowerWaveCoffee(Clone)":
                            self.enemyInventory.RemoveItem(DLC1Content.Items.AttackSpeedAndMoveSpeed, 6 + Run.instance.stageClearCount * 2);
                            break;
                        case "InfiniteTowerWaveLepton(Clone)":
                            self.enemyInventory.RemoveItem(RoR2Content.Items.TPHealingNova, self.enemyInventory.GetItemCount(RoR2Content.Items.TPHealingNova));
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
                        case "InfiniteTowerWaveBlindness(Clone)":
                            foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                            {
                                if (player.master.bodyInstanceObject)
                                {
                                    player.master.GetBody().RemoveBuff(DLC1Content.Buffs.Blinded);
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

        public int rewardOptions = 3;
        public float newRadius = 0;
    }

    //This doesn't need to be networked does it
    public class VoidCoinChance : MonoBehaviour
    {
        public float chance = 1.5f;
    }

    public class SimuEquipmentWaveHelper : MonoBehaviour
    {
        protected CombatSquad combatSquad;
        public int variant;

        private void OnEnable()
        {
            InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();
            if (controller && controller.combatSquad)
            {
                combatSquad = controller.combatSquad;
                controller.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
            }
            if (variant == 1)
            {
                RoR2Content.Equipment.QuestVolatileBattery.dropOnDeathChance = 0.015f;
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius /= 2f;
            }
        }

        private void OnDisable()
        {
            if (this.combatSquad)
            {
                this.combatSquad.onMemberDiscovered -= this.OnCombatSquadMemberDiscovered;
            }
            if (variant == 1)
            {
                RoR2Content.Equipment.QuestVolatileBattery.dropOnDeathChance = 0;
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius *= 2f;
            }
        }

        protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
        {
            GameObject bodyObj = master.GetBodyObject();
            if (NetworkServer.active)
            {
                if (bodyObj)
                {
                    //Can't really add network component just like that would need a better solution ideally
                    if (!bodyObj.GetComponent<EquipmentSlot>())
                    {
                        EquipmentSlot slot = bodyObj.AddComponent<EquipmentSlot>();
                        slot.hasEffectiveAuthority = true;
                        slot._rechargeTime = Run.FixedTimeStamp.zero;
                    }
                }
                if (variant == 0)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.Jetpack.equipmentIndex);
                    master.inventory.GiveItem(RoR2Content.Items.AutoCastEquipment, 1);
                    master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 7);
                    master.inventory.GiveItem(RoR2Content.Items.SprintOutOfCombat, 1);
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, 2 + Run.instance.stageClearCount * 2);
                }
                else if (variant == 1)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex);
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, 8 + Run.instance.stageClearCount * 2);
                }
            }
        }
    }
}