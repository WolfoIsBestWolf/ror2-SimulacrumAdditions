using BepInEx;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
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
    [BepInPlugin("Wolfo.SimulacrumAdditions", "SimulacrumAdditions", "2.3.3")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class SimuMain : BaseUnityPlugin
    {
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
        public static BasicPickupDropTable dtAISafeRandomVoid = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        //
        //
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

        public static BasicPickupDropTable dtITCategoryDamage = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITCategoryHealing = ScriptableObject.CreateInstance<BasicPickupDropTable>();
        public static BasicPickupDropTable dtITCategoryUtility = ScriptableObject.CreateInstance<BasicPickupDropTable>();

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


        public static MasterCatalog.MasterIndex IndexAffixHealingCore = MasterCatalog.MasterIndex.none;

        /*public float BasicWeightCommon = 5;
        public float BasicWeightUncommon = 4;
        public float BasicWeightRare = 3;*/
        public float BossWeightCommon = 10;
        public float BossWeightUncommon = 6;
        public float BossWeightRare = 4;


        public void Awake()
        {
            Assets.Init(Info);
            WConfig.InitConfig();          
            if (WConfig.cfgDumpInfo.Value)
            {
                DumpAllWaveInfo(ITBasicWaves);
                DumpAllWaveInfo(ITBossWaves);
            }

            MakePortal();
            SetupConstants();
            SimuChanges();

            WavesMain.Start();
            GiantGup.Start();
            SuperMegaCrab.Start();

            SimulacrumDCCS.Start();
            CrabActivateToTravel.Make();

            ItemHelpers.MakeItems();

            ITRun_Hooks.AddHooks();
            Wave_Hooks.AddHooks();
            VoidSafeWard_Hooks.AddHooks();
            Hooks_Other.AddHooks();

            GameModeCatalog.availability.CallWhenAvailable(LateRunningMethod);

            ArtifactTweaks.Main();
            if (WConfig.cfgEnableArtifactAugments.Value)
            {
                ArtifactAugments.MakeArtifact();      
            }
            if (WConfig.cfgEnableArtifactStages.Value)
            {
                ArtifactReal.MakeArtifact();
            }
            VoidCoin.MakeVoidCoin();
            Visual_Upgrades();
           
            //Use Custom Simu Interactable DCCSs
            On.RoR2.SceneDirector.Start += SimulacrumDCCS.Stage_ExtraObjects;
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.SimuInteractableDCCSAdder;
            //More Interactables early on to get into it quicker
            if (WConfig.cfgSimuCreditsRebalance.Value)
            {
                On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.CreditsRebalance;
            }
            //

            //Doesn't need to ban world unique anymore anyways
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/DuplicatorWild/dtDuplicatorWild.asset").WaitForCompletion().bannedItemTags = new ItemTag[0];
            On.RoR2.Run.BuildDropTable += (orig, self) =>
            {
                orig(self);
                if (self is InfiniteTowerRun)
                {
                    self.availableBossDropList.Add(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex));
                    //self.availableBossDropList.Add(PickupCatalog.FindPickupIndex(RoR2Content.Items.TitanGoldDuringTP.itemIndex));
                }
            };

            //What is this?
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

            Material matAncientLoft_Water = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion();
            matAncientLoft_Water.SetTextureScale("_FoamTex", new Vector2(20, 20));

        }


        public static void Visual_Upgrades()
        {
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
        }

        private void OneTimeLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);
            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                //LTG more twisted scavs compatibility ig ig
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

        private static void OptionChestColorsAndName(On.RoR2.PickupPickerController.orig_SetOptionsInternal orig, PickupPickerController self, PickupPickerController.Option[] newOptions)
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

        internal static void MakePortal()
        {
            InteractableSpawnCard iscVoidOutroPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidOutroPortal/iscVoidOutroPortal.asset").WaitForCompletion();
            InteractableSpawnCard iscVoidPortal = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/PortalVoid/iscVoidPortal.asset").WaitForCompletion();

            iscSimuExitPortal = Instantiate(iscVoidOutroPortal);
            iscSimuExitPortal.name = "iscSimuExitPortal";
            GameObject EndingPortal = PrefabAPI.InstantiateClone(iscSimuExitPortal.prefab, "SimulacrumExitPortal", true);
            iscSimuExitPortal.prefab = EndingPortal;

            EndingPortal.GetComponent<GenericDisplayNameProvider>().displayToken = "PORTAL_ITEND_NAME";
            EndingPortal.GetComponent<GenericInteraction>().contextToken = "PORTAL_ITEND_CONTEXT";
            if (EndingPortal.GetComponent<GenericObjectiveProvider>())
            {
                EndingPortal.GetComponent<GenericObjectiveProvider>().objectiveToken = "OBJECTIVE_ITEND"; ;
            }
            else
            {
                EndingPortal.AddComponent<GenericObjectiveProvider>().objectiveToken = "OBJECTIVE_ITEND"; ;
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
            EndingPortal.transform.GetChild(2).GetChild(6).gameObject.SetActive(false);
            EndingPortal.transform.GetChild(2).GetChild(7).gameObject.SetActive(false);
            Destroy(EndingPortal.transform.GetChild(1).gameObject);
            Destroy(EndingPortal.transform.GetChild(0).gameObject);
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

            Simu_Run_Run = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerRun.prefab").WaitForCompletion().GetComponent<InfiniteTowerRun>();

            //This is where we'd need to add Fireworks
            //Fireworks is Interactable Related and that tagged is banned
            Simu_Run_Run.blacklistedItems = Simu_Run_Run.blacklistedItems.Add(RoR2Content.Items.Squid); //But Squid Polyp wouldn't work they just die
            Simu_Run_Run.blacklistedItems = Simu_Run_Run.blacklistedItems.Remove(DLC1Content.Items.DroneWeapons); //But Squid Polyp wouldn't work they just die
            Simu_Run_Run.blacklistedTags = Simu_Run_Run.blacklistedTags.Remove(ItemTag.InteractableRelated); //There's only two and Fireworks works plenty

            ItemDef tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("VV_ITEM_CORNUCOPIACELL_ITEM"));
            if (tempDef != null)
            {
                Simu_Run_Run.blacklistedItems = Simu_Run_Run.blacklistedItems.Add(tempDef);
            }
            //There are no teleporters in Simu
            tempDef = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("FieldAccelerator"));
            if (tempDef != null)
            {
                Simu_Run_Run.blacklistedItems = Simu_Run_Run.blacklistedItems.Add(tempDef);
            }

            if (WConfig.cfgItemsEvery8.Value)
            {
                Simu_Run_Run.enemyItemPeriod = 8;
            }

            AutoConfig.InitConfig();
            AutoConfig.ApplyConfig();
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

            AfterWave5EndWave30Prerequisite.minimumWaveCount = 6;
            AfterWave5EndWave30Prerequisite.maximumWaveCount = 30;
            AfterWave5EndWave30Prerequisite.name = "AfterWave5EndWave30Prerequisite";

            DLC2_StartWave10Prerequisite.minimumWaveCount = 10;
            DLC2_StartWave10Prerequisite.name = "StartWave10PrerequisiteDLC2";
            DLC2_StartWave15Prerequisite.minimumWaveCount = 15;
            DLC2_StartWave15Prerequisite.name = "StartWave15PrerequisiteDLC2";
            DLC2_StartWave30Prerequisite.minimumWaveCount = 30;
            DLC2_StartWave30Prerequisite.name = "StartWave30PrerequisiteDLC2";


            //
            //Drop Pools
            //The Guaranteed Red
            dtITWaveTier3.tier3Weight = 90;
            dtITWaveTier3.bossWeight = 10;

            //Vanilla Void Boss Drop Table is kinda bad
            dtITVoid.voidTier1Weight = 60;
            dtITVoid.voidTier2Weight = 60;
            dtITVoid.voidTier3Weight = 15;
            dtITVoid.voidBossWeight = 10;

            //Wacky Tier for Wacky Artifacts
            dtAllTier.name = "dtAllTier";
            dtAllTier.tier1Weight = 120;
            dtAllTier.tier2Weight = 20;
            dtAllTier.tier3Weight = 2;
            dtAllTier.bossWeight = 2;
            dtAllTier.equipmentWeight = 15;
            dtAllTier.lunarItemWeight = 8;
            dtAllTier.voidTier1Weight = 50;
            dtAllTier.voidTier2Weight = 20;
            dtAllTier.voidTier3Weight = 2;
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
            dtITBasicWaveOnKill.tier2Weight = 9f;
            dtITBasicWaveOnKill.tier3Weight = 0.75f;
            dtITBasicWaveOnKill.bossWeight = 0.25f;
            dtITBasicWaveOnKill.name = "dtITBasicWaveOnKill";
            dtITBasicWaveOnKill.requiredItemTags = new ItemTag[] { ItemTag.OnKillEffect };
            //
            //For Basic waves intended to be difficult
            dtITBasicBonusGreen.tier1Weight = 30f;
            dtITBasicBonusGreen.tier2Weight = 80f;
            dtITBasicBonusGreen.tier3Weight = 0f;
            dtITBasicBonusGreen.bossWeight = 0f;
            dtITBasicBonusGreen.bannedItemTags = new ItemTag[] { };
            dtITBasicBonusGreen.name = "dtITBasicBonusGreen";

            //For Boss waves intended to be difficult
            dtITBossBonusRed.tier1Weight = 0f;
            dtITBossBonusRed.tier2Weight = 20f; //80 default
            dtITBossBonusRed.tier3Weight = 12f; //7.5 default
            dtITBossBonusRed.bossWeight = 0f; //7.5 default
            dtITBossBonusRed.bannedItemTags = new ItemTag[] { };
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

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/MusicTrackDefs/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();

            MusicTrackDef MTDSulfurBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();
            MusicTrackDef MTDShipgraveyardBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/MusicTrackDefs/muSong22.asset").WaitForCompletion();
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
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 184;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        ITBasicWaves.wavePrefabs[i].weight = 4f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                };
            }


            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ENIGMA";
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactSingleMonsterTypeWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_KIN";

            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();

            ITBasicWaves.wavePrefabs[0].weight = BasicWaveWeight;
            ITBossWaves.wavePrefabs[0].weight = BasicBossWaveWight;


            #region JUDGEMENT FIX IGNORE
            if (SimuMain.ITBossWaves.wavePrefabs.Length < 2)
            {
                GameObject InfiniteTowerWaveBossLunar = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossLunar.prefab").WaitForCompletion();
                GameObject InfiniteTowerWaveBossVoid = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossVoid.prefab").WaitForCompletion();
                GameObject InfiniteTowerWaveBossBrother = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossBrother.prefab").WaitForCompletion();

                InfiniteTowerWaveCategory.WeightedWave WaveBossLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossLunar, weight = 10 };
                InfiniteTowerWaveCategory.WeightedWave WaveBossVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoid, weight = 6 };
                InfiniteTowerWaveCategory.WeightedWave WaveBossBrother = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossBrother, weight = 2 };
                SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(WaveBossLunar, WaveBossVoid, WaveBossBrother);
            }
            #endregion

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

                string text = "\n"+
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "\n" +
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token);

                Debug.Log(text);

            }
            Debug.Log("");
            Debug.Log("");
            //Debug.Log("All Simulacrum Waves : " + category.name);
            float totalWeight = 0;
            float totalWeightPre11 = 0;
            float totalWeightPre30 = 0;
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                totalWeight += category.wavePrefabs[i].weight;


                string tokenCredit = "Credits: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave > 0)
                {
                    tokenCredit += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave + " * Wave";
                }
                string tokenDropTable = "DropTable: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable.name;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>() && category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable)
                {
                    tokenDropTable += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable.name;
                }

                string token4 = "Prerequesites: ";
                if (category.wavePrefabs[i].prerequisites)
                {
                    token4 += category.wavePrefabs[i].prerequisites.name;
                }




                string text = "\n" +
                    " [" + i + "] " + category.wavePrefabs[i].wavePrefab.name + "\n" +
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "\n" +
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token) + "\n" +
                    "Weight: " + category.wavePrefabs[i].weight + "\n" +
                    tokenCredit + "\n" +
                    "Immediate: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction + "\n" +
                    "WaveSeconds: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds + "\n" +
                    tokenDropTable + "\n" +
                    tokenCredit + "\n" +
                    token4;

                if (category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards != null)
                {
                    text += "\nMonsterCards: " + category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards.name;
                }
                Debug.Log(text);


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
            }


            float defaultWeightOG = category.wavePrefabs[0].weight;
            float defaultWeight = category.wavePrefabs[0].weight;

            Debug.Log("Total " + category.name + " Weight  pre 10: " + totalWeightPre11 +
             "  Extra Weight: " + (totalWeightPre11 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre11);

            Debug.Log("Total " + category.name + " Weight  pre 30: " + totalWeightPre30 +
             "  Extra Weight: " + (totalWeightPre30 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre30);

            defaultWeight = defaultWeightOG * DefaultWeightMultiplier1;
            totalWeight -= defaultWeight;

            Debug.Log("Total " + category.name + " Weight post 30: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);

            defaultWeight = defaultWeightOG * DefaultWeightMultiplier2;
            totalWeight -= defaultWeightOG * (DefaultWeightMultiplier2 - DefaultWeightMultiplier1);



            Debug.Log("Total " + category.name + " Weight post 50: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);
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


}