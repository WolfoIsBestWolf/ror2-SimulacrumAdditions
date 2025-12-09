using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class SuperMegaCrab
    {
        public static GameObject SuperCrabBody = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/VoidMegaCrabBody"), "VoidSuperMegaCrabBody", true);
        public static GameObject SuperCrabMaster = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/VoidMegaCrabMaster"), "VoidSuperMegaCrabMaster", true);
        public static FamilyDirectorCardCategorySelection dccsVoidFamilyNoBarnacle = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion());
        public static UnlockableDef unlockable = ScriptableObject.CreateInstance<UnlockableDef>();

        public static void MakeWave()
        {
            GameObject WaveBoss_SuperCrab = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_SuperVoidMegaCrab", true);
            GameObject WaveBoss_SuperCrabUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "WaveBoss_SuperVoidMegaCrabUI", false);
            CharacterSpawnCard cscSuperCrab;


            CharacterSpawnCard cscVoidMegaCrab = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVoidMegaCrab");
            cscSuperCrab = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscSuperCrab.name = "cscITVoidMegaCrabSuper";
            cscSuperCrab._loadout = cscVoidMegaCrab._loadout;
            cscSuperCrab.forbiddenFlags = cscVoidMegaCrab.forbiddenFlags;
            cscSuperCrab.occupyPosition = false;
            cscSuperCrab.prefab = SuperCrabMaster;
            cscSuperCrab.sendOverNetwork = true;
            cscSuperCrab.hullSize = HullClassification.BeetleQueen;
            cscSuperCrab.nodeGraphType = MapNodeGroup.GraphType.Ground;
            cscSuperCrab.requiredFlags = NodeFlags.None;
            cscSuperCrab.directorCreditCost = 6000;
            cscSuperCrab.occupyPosition = true;


            dccsVoidFamilyNoBarnacle.categories[2].cards = dccsVoidFamilyNoBarnacle.categories[2].cards.Remove(dccsVoidFamilyNoBarnacle.categories[2].cards[1]);
            dccsVoidFamilyNoBarnacle.name = "dccsVoidFamilyNoBarnacle";

            //Spawns
            WaveBoss_SuperCrab.GetComponent<CombatDirector>().monsterCards = dccsVoidFamilyNoBarnacle;
            var wave = WaveBoss_SuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            wave.baseCredits = 0;
            wave.linearCreditsPerWave = 0;
            wave.spawnList[0].spawnCard = cscSuperCrab;
            wave.spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
            wave.secondsBeforeSuddenDeath *= 2f;
            wave.rewardDropTable = Constant.dtITWaveTier3;
            wave.rewardDisplayTier = ItemTier.Tier3;
            WaveBoss_SuperCrab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITVoid;
            WaveBoss_SuperCrab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier2;
            WaveBoss_SuperCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

            SimuExplicitStats simuExplicitStats = WaveBoss_SuperCrab.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.5f;
            simuExplicitStats.hpBonusMulti = 1f;
            simuExplicitStats.halfOnNonFinal = true;


            wave.overlayEntries[1].prefab = WaveBoss_SuperCrabUI;
            WaveBoss_SuperCrabUI.SetWaveInfo("ITWAVE_NAME_BOSS_SUPERCRAB", "ITWAVE_DESC_BOSS_SUPERCRAB", Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveVoid.png"), new Color(1f, 0.6278f, 0.83f, 1));
            InfiniteTowerWaveCategory.WeightedWave ITSuperCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SuperCrab, weight = 4f, prerequisites = Constant.StartWave35Prerequisite };
            InfiniteTowerWaveCategory.WeightedWave ITSuperCrabS = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_SuperCrab, weight = 4f,  };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITSuperCrab);
            Constant.ITSuperBossWaves.wavePrefabs = Constant.ITSuperBossWaves.wavePrefabs.Add(ITSuperCrabS);

        }

        public static void Start()
        {
            ContentAddition.AddBody(SuperCrabBody);
            ContentAddition.AddMaster(SuperCrabMaster);
            SuperCrabMaster.GetComponent<CharacterMaster>().bodyPrefab = SuperCrabBody;

            #region Body

            CharacterBody CrabCharacterBody = SuperCrabBody.GetComponent<CharacterBody>();
            CrabCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;


            CrabCharacterBody.baseNameToken = "SUPERMEGACRAB_BODY_NAME";
            CrabCharacterBody.portraitIcon = Addressables.LoadAssetAsync<Texture>(key: "RoR2/DLC1/VoidSuperMegaCrabBody.png").WaitForCompletion();

            CrabCharacterBody.baseMaxHealth = 2020; //Base Health is 2800*1.6 cuz Trans Shrimp
            CrabCharacterBody.baseDamage *= 0.2f; //Bro gets so many damage items
            CrabCharacterBody.baseMoveSpeed *= 0.75f;
            CrabCharacterBody.baseAttackSpeed *= 0.75f;
            CrabCharacterBody.baseJumpPower *= 1.5f;
            CrabCharacterBody.PerformAutoCalculateLevelStats();

            //Crazy how much more effort it'd be to do it in the official way
            On.EntityStates.VoidMegaCrab.Weapon.FireCrabCannonBase.OnEnter += FireCrabCannonBase_OnEnter;
            On.EntityStates.VoidMegaCrab.DeathState.OnExit += DeathState_OnExit;
            #endregion

            #region Master
            SuperCrabMaster.AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("AdaptiveArmor"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("TeleportWhenOob"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ShieldOnly"), count = 1, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("RandomDamageZone"), count = 6, },
                new GivePickupsOnStart.ItemInfo { itemString = ("CritGlassesVoid"), count = 10, },
                new GivePickupsOnStart.ItemInfo { itemString = ("BearVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("MissileVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ElementalRingVoid"), count = 1, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("EquipmentMagazineVoid"), count = 1, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("SlowOnHitVoid"), count = 1, }, //Tentabaubel duration stacks so keep it low
                new GivePickupsOnStart.ItemInfo { itemString = ("VoidMegaCrabItem"), count = 3, },
            };
            SuperCrabMaster.GetComponent<GivePickupsOnStart>().itemDefInfos = new GivePickupsOnStart.ItemDefInfo[] {
                new GivePickupsOnStart.ItemDefInfo { itemDef = ItemHelpers.ITCooldownUp, count = 5, },
            };
            #endregion

            #region Model
            //Fix Armature
            //The bottom metal for some reason is tied to the hands
            //It's the mesh itself that seems fucked who the hell knows why

            /*ransform rootOG = mdlVoidSuperMegaCrab.transform.GetChild(2).GetChild(0);
           Transform rootCopy = mdlCrab.transform.GetChild(0).GetChild(0);
          Transform[] transformsOG = rootOG.GetComponentsInChildren<Transform>();

           for (int i = 0; i< transformsOG.Length; i++)
           {
               Debug.Log(transformsOG[i]);
           }*/


            //Visuals
            Material matVoidSuperMegaCrabMetal = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabMetal.mat").WaitForCompletion();
            Material matVoidSuperMegaCrabShell = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabShell.mat").WaitForCompletion();
            GameObject mdlVoidSuperMegaCrab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/mdlVoidSuperMegaCrab.fbx").WaitForCompletion();
            Debug.Log(mdlVoidSuperMegaCrab);

            GameObject mdlCrab = SuperCrabBody.transform.GetChild(0).GetChild(3).gameObject;
            mdlCrab.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            mdlCrab.GetComponent<ModelPanelParameters>().minDistance = 20;
            mdlCrab.GetComponent<ModelPanelParameters>().maxDistance = 50;

            //mdlCrab.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().sharedMesh = mdlVoidSuperMegaCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh;

            SkinDef skinSuperVoidCrab = Object.Instantiate(Addressables.LoadAssetAsync<SkinDef>(key: "00bdccf5542881c42ab3eb4de8adbaf0").WaitForCompletion());
            SkinDefParams paramsSuperVoidCrab = Object.Instantiate(Addressables.LoadAssetAsync<SkinDefParams>(key: "872a5d01600f3d04fbe96095db442636").WaitForCompletion());
            skinSuperVoidCrab.skinDefParamsAddress = new AssetReferenceT<SkinDefParams>("");
            skinSuperVoidCrab.name = "skinVoidSuperMegaCrab";
            paramsSuperVoidCrab.name = "skinVoidSuperMegaCrab_params";
            skinSuperVoidCrab.skinDefParams = paramsSuperVoidCrab;
            var newRender = HG.ArrayUtils.Clone(paramsSuperVoidCrab.rendererInfos);
            paramsSuperVoidCrab.rendererInfos = newRender;
            var newMesh = HG.ArrayUtils.Clone(paramsSuperVoidCrab.meshReplacements);
            paramsSuperVoidCrab.meshReplacements = newMesh;

            newMesh[2].mesh = mdlVoidSuperMegaCrab.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
            newMesh[3].mesh = mdlVoidSuperMegaCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh;
            newMesh[2].meshAddress = null;
            newMesh[3].meshAddress = null;
            //newMesh[3].meshAddress = new AssetReferenceT<Mesh>("fb901b69236f51b4ab16a5a7dd58af34");
            //newMesh[3].meshAddress = mdlVoidSuperMegaCrab.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;


            newRender[2].defaultMaterialAddress = new AssetReferenceT<Material>("0eb7df469ba68fb408a30cb0b20a83d8");
            newRender[3].defaultMaterialAddress = new AssetReferenceT<Material>("e1cd8b55b8fa1004294c9280ec34d3f2");

            mdlCrab.GetComponent<ModelSkinController>().skins[0] = skinSuperVoidCrab;
            #endregion

            MakeWave();
            #region Log
            unlockable.cachedName = "Logs.VoidSuperMegaCrabBody.0";
            unlockable.nameToken = "UNLOCKABLE_LOG_SUPERMEGACRAB";
            ContentAddition.AddUnlockableDef(unlockable);
            if (!WConfig.cfgNewEnemiesVisible.Value)
            {
                unlockable = null;
            }
            SuperCrabBody.GetComponent<DeathRewards>().logUnlockableDef = unlockable;
            #endregion
        }

        private static void FireCrabCannonBase_OnEnter(On.EntityStates.VoidMegaCrab.Weapon.FireCrabCannonBase.orig_OnEnter orig, EntityStates.VoidMegaCrab.Weapon.FireCrabCannonBase self)
        {
            if (self.characterBody.name.StartsWith("VoidSuper"))
            {
                self.projectileCount += (int)(self.projectileCount * 1.2f) + 1;
            }
            orig(self);
        }

        private static void DeathState_OnExit(On.EntityStates.VoidMegaCrab.DeathState.orig_OnExit orig, EntityStates.VoidMegaCrab.DeathState self)
        {
            orig(self);
            if (self.characterBody.name.StartsWith("VoidSuper"))
            {
                if (SuperMegaCrab.unlockable && Run.instance.CanUnlockableBeGrantedThisRun(SuperMegaCrab.unlockable))
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate(DeathRewards.logbookPrefab, self.characterBody.corePosition, UnityEngine.Random.rotation);
                    gameObject.GetComponentInChildren<UnlockPickup>().unlockableDef = SuperMegaCrab.unlockable;
                    gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                    NetworkServer.Spawn(gameObject);
                }
                LocalUser user = LocalUserManager.GetFirstLocalUser();
                if (user != null)
                {
                    user.userProfile.AddAchievement("VOIDSUPERMEGACRAB_ACHIEVEMENT", false);
                }
            }
        }
    }



}