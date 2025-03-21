﻿using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SuperMegaCrab
    {
        public static GameObject SuperCrabBody = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/VoidMegaCrabBody"), "VoidSuperMegaCrabBody", true);
        public static GameObject SuperCrabMaster = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/VoidMegaCrabMaster"), "VoidSuperMegaCrabMaster", true);
        public static FamilyDirectorCardCategorySelection dccsVoidFamilyNoBarnacle = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion());
        public static UnlockableDef unlockable = ScriptableObject.CreateInstance<UnlockableDef>();

        public static void Start()
        {
            //InfiniteTowerWaveBossSuperCrab
            GameObject InfiniteTowerWaveBossSuperCrab = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveBossSuperVoidMegaCrab", true);
            GameObject InfiniteTowerWaveBossSuperCrabUI = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperVoidMegaCrabUI", false);
            CharacterSpawnCard cscSuperCrab;

            ContentAddition.AddBody(SuperCrabBody);
            ContentAddition.AddMaster(SuperCrabMaster);
            SuperCrabMaster.GetComponent<CharacterMaster>().bodyPrefab = SuperCrabBody;



            CharacterBody CrabCharacterBody = SuperCrabBody.GetComponent<CharacterBody>();
            CrabCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;


            CrabCharacterBody.baseNameToken = "SUPERMEGACRAB_BODY_NAME";

            Texture VoidSuperMegaCrabBody = Addressables.LoadAssetAsync<Texture>(key: "RoR2/DLC1/VoidSuperMegaCrabBody.png").WaitForCompletion();
            CrabCharacterBody.portraitIcon = VoidSuperMegaCrabBody;


            //Stats
            CrabCharacterBody.baseMaxHealth = 1875*2; //Base Health is 2800*1.6 cuz Trans Shrimp
            CrabCharacterBody.baseDamage *= 0.2f; //Bro gets so many damage items
            CrabCharacterBody.baseMoveSpeed *= 0.75f;
            CrabCharacterBody.baseAttackSpeed *= 0.75f;
            CrabCharacterBody.baseJumpPower *= 1.5f;

            CrabCharacterBody.PerformAutoCalculateLevelStats();



            //Entity States
            //Crazy how much more effort it'd be to do it in the official way
            On.EntityStates.VoidMegaCrab.Weapon.FireCrabCannonBase.OnEnter += (orig, self) =>
            {
                if (self.characterBody.name.StartsWith("VoidSuper"))
                {
                    self.projectileCount += (int)(self.projectileCount*1.2f) + 1;
                }
                orig(self);
            };
            On.EntityStates.VoidMegaCrab.DeathState.OnExit += DeathState_OnExit;


            //Visuals
            Material matVoidSuperMegaCrabMetal = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabMetal.mat").WaitForCompletion();
            Material matVoidSuperMegaCrabShell = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabShell.mat").WaitForCompletion();
            GameObject mdlVoidSuperMegaCrab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/mdlVoidSuperMegaCrab.fbx").WaitForCompletion();


            GameObject mdlCrab = SuperCrabBody.transform.GetChild(0).GetChild(3).gameObject;
            mdlCrab.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            mdlCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh = mdlVoidSuperMegaCrab.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
            mdlCrab.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().sharedMesh = mdlVoidSuperMegaCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh;

            mdlCrab.GetComponent<ModelPanelParameters>().minDistance = 18;
            mdlCrab.GetComponent<ModelPanelParameters>().maxDistance = 30;

            CharacterModel crabModel = mdlCrab.GetComponent<CharacterModel>();
            crabModel.baseRendererInfos[2].defaultMaterial = matVoidSuperMegaCrabShell;
            crabModel.baseRendererInfos[3].defaultMaterial = matVoidSuperMegaCrabMetal;

            crabModel.baseRendererInfos[2].renderer = mdlCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
            crabModel.baseRendererInfos[3].renderer = mdlCrab.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();


            //Fix Armature
            //The bottom metal for some reason is tied to the hands
            //It's the mesh itself that seems fucked who the hell knows why

            Transform rootOG = mdlVoidSuperMegaCrab.transform.GetChild(2).GetChild(0);
            Transform rootCopy = mdlCrab.transform.GetChild(0).GetChild(0);
            /*Transform[] transformsOG = rootOG.GetComponentsInChildren<Transform>();

            for (int i = 0; i< transformsOG.Length; i++)
            {
                Debug.Log(transformsOG[i]);
            }*/
            //
            //Maybe Scav item granter or smth idk
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
                new GivePickupsOnStart.ItemInfo { itemString = ("CutHp"), count = 1, }
            };
            SuperCrabMaster.GetComponent<GivePickupsOnStart>().itemDefInfos = new GivePickupsOnStart.ItemDefInfo[] {
                new GivePickupsOnStart.ItemDefInfo { itemDef = ItemHelpers.ITCooldownUp, count = 5, },
            };
            //SuperCrabMaster.AddComponent<GivePickupsOnStart>().equipmentString = "Tonic";

            //
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
            //

            dccsVoidFamilyNoBarnacle.categories[2].cards = dccsVoidFamilyNoBarnacle.categories[2].cards.Remove(dccsVoidFamilyNoBarnacle.categories[2].cards[1]);
            dccsVoidFamilyNoBarnacle.name = "dccsVoidFamilyNoBarnacle";

            //Spawns
            InfiniteTowerWaveBossSuperCrab.GetComponent<CombatDirector>().monsterCards = dccsVoidFamilyNoBarnacle;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 0;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscSuperCrab;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperCrab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITVoid;
            InfiniteTowerWaveBossSuperCrab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier2;
            InfiniteTowerWaveBossSuperCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

            SimuExplicitStats simuExplicitStats = InfiniteTowerWaveBossSuperCrab.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.4f;
            simuExplicitStats.hpBonusMulti = 0.4f;
            simuExplicitStats.halfOnNonFinal = true;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;
            //
            Texture2D texITWaveSuperDevestator = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveVoid.png");
            Sprite texITWaveSuperDevestatorS = Sprite.Create(texITWaveSuperDevestator, WRect.rec64, WRect.half);

            //Color Color = new Color(0.7f, 0.6278f, 1f, 1);
            Color Color = new Color(1f, 0.6278f, 0.83f, 1);
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSuperDevestatorS;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Color;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = Color;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossSuperCrabUI;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_SUPERCRAB";
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_SUPERCRAB";
            //
            InfiniteTowerWaveCategory.WeightedWave ITSuperCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperCrab, weight = 4f, prerequisites = SimuMain.StartWave35Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITSuperCrab);
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITSuperCrab);


            unlockable.cachedName = "Logs.VoidSuperMegaCrabBody.0";
            unlockable.nameToken = "UNLOCKABLE_LOG_SUPERMEGACRAB";
           
            R2API.ContentAddition.AddUnlockableDef(unlockable);
            if (!WConfig.cfgNewEnemiesVisible.Value)
            {
                SuperCrabBody.GetComponent<DeathRewards>().logUnlockableDef = null;
            }
            else
            {
                SuperCrabBody.GetComponent<DeathRewards>().logUnlockableDef = unlockable;
            }
        }

        private static void DeathState_OnExit(On.EntityStates.VoidMegaCrab.DeathState.orig_OnExit orig, EntityStates.VoidMegaCrab.DeathState self)
        {
            orig(self);
            if (self.characterBody.name.StartsWith("VoidSuper"))
            {
                if (SuperMegaCrab.unlockable && Run.instance.CanUnlockableBeGrantedThisRun(SuperMegaCrab.unlockable))
                {
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(DeathRewards.logbookPrefab, self.characterBody.corePosition, UnityEngine.Random.rotation);
                    gameObject.GetComponentInChildren<UnlockPickup>().unlockableDef = SuperMegaCrab.unlockable;
                    gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                    NetworkServer.Spawn(gameObject);
                }
            }      
        }
    }



}