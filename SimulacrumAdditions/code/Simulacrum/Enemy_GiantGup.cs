using EntityStates;
using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class GiantGup
    {
        //public static GameObject GupBody = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/GupBody");
        //public static GameObject GupMaster = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/GupMaster");
        public static GameObject GiantBody = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/GupBody"), "GupGiantBody", true);
        public static GameObject GiantMaster = PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/GupMaster"), "GupGiantMaster", true);
        public static UnlockableDef unlockable = ScriptableObject.CreateInstance<UnlockableDef>();

        public static void MakeWave()
        {
            CharacterSpawnCard gupCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGupBody");
            CharacterSpawnCard cscGiantGup;
            cscGiantGup = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscGiantGup.name = "cscITGiantGup";
            cscGiantGup._loadout = gupCard._loadout;
            cscGiantGup.forbiddenFlags = gupCard.forbiddenFlags;
            cscGiantGup.occupyPosition = false;
            cscGiantGup.prefab = GiantMaster;
            cscGiantGup.sendOverNetwork = true;
            cscGiantGup.hullSize = HullClassification.BeetleQueen;
            cscGiantGup.nodeGraphType = MapNodeGroup.GraphType.Ground;
            cscGiantGup.requiredFlags = NodeFlags.None;
            cscGiantGup.directorCreditCost = 2000;
            cscGiantGup.occupyPosition = true;

            GameObject WaveBoss_GiantGup = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_GiantGup", true);
            GameObject WaveBoss_GiantGupUI = PrefabAPI.InstantiateClone(Constant.ScavWaveUI, "InfiniteTowerCurrentBossGiantGupWaveUI", false);

            WaveBoss_GiantGup.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            InfiniteTowerExplicitSpawnWaveController wave = WaveBoss_GiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            wave.baseCredits = 5;
            wave.immediateCreditsFraction = 0;
            wave.linearCreditsPerWave = 3;
            wave.spawnList[0].spawnCard = cscGiantGup;
            wave.spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
            wave.rewardDropTable = Constant.dtITWaveTier2;
            wave.rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_GiantGup.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;

            SimuExplicitStats simuExplicitStats = WaveBoss_GiantGup.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 2f;
            simuExplicitStats.hpBonusMulti = 0.4f;

            wave.secondsBeforeSuddenDeath *= 2f;
            //
            Texture2D texITWaveGupIcon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveGupBoss.png");
            Sprite texITWaveGupIconS = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGupBoss.png");

            //Color GupColor = new Color32(255, 161, 15, 255);
            //Color GupColor = new Color32(255, 122, 104, 255); 
            Color GupColor = new Color(1f, 0.6784f, 0.6278f, 1);
            WaveBoss_GiantGupUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveGupIconS;
            WaveBoss_GiantGupUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = GupColor;
            WaveBoss_GiantGupUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = GupColor;

            wave.overlayEntries[1].prefab = WaveBoss_GiantGupUI;
            WaveBoss_GiantGupUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_GIANTGUP";
            WaveBoss_GiantGupUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_GIANTGUP"; //Gurp failed

            InfiniteTowerWaveCategory.WeightedWave ITGiantGup = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_GiantGup, weight = 7, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITGiantGup);


        }

        public static void Start()
        {
            ContentAddition.AddBody(GiantBody);
            ContentAddition.AddMaster(GiantMaster);
            GiantMaster.GetComponent<CharacterMaster>().bodyPrefab = GiantBody;


            #region Body
            CharacterBody GiantCharacterBody = GiantBody.GetComponent<CharacterBody>();
            GiantCharacterBody.baseNameToken = "GIANTGUP_BODY_NAME";
            GiantCharacterBody.isChampion = true;
            GiantCharacterBody.portraitIcon = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Bodies/GiantGupBody.png");



            GiantCharacterBody.baseMaxHealth *= 4f; //Base Health is 1000
            GiantCharacterBody.baseDamage *= 1.2f;
            GiantCharacterBody.baseAttackSpeed = 0.3f;
            GiantCharacterBody.baseMoveSpeed *= 0.6f;
            GiantCharacterBody.baseJumpPower *= 2.5f;
            GiantCharacterBody.baseArmor = 20;
            GiantCharacterBody.PerformAutoCalculateLevelStats();
            GiantCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToExecutes;
            GiantCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;
            GiantCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToLava;

            SetStateOnHurt hurt = GiantBody.GetComponent<SetStateOnHurt>();
            hurt.canBeHitStunned = false;
            hurt.canBeStunned = false;
            hurt.canBeFrozen = false;
            GiantBody.GetComponent<CharacterDeathBehavior>().deathState = ContentAddition.AddEntityState<GiantGupSplitDeath>(out _);
            #endregion
            #region Model
            GameObject mdlGup = GiantBody.transform.GetChild(0).GetChild(0).gameObject;
            mdlGup.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);

            var MainBody4 = mdlGup.GetComponent<ChildLocator>().FindChild("MainBody4");
            MainBody4.GetChild(0).localScale = new Vector3(3.5f, 3.5f, 3.5f);
            MainBody4.GetChild(1).localScale = new Vector3(3.5f, 3.5f, 3.5f);
            mdlGup.GetComponent<ModelPanelParameters>().minDistance = 5;
            mdlGup.GetComponent<ModelPanelParameters>().maxDistance = 9;


            SkinDef skinGupGiant = Object.Instantiate(Addressables.LoadAssetAsync<SkinDef>(key: "d777989411688b347927be3f356a0e9a").WaitForCompletion());
            SkinDefParams paramsGupGiant = Object.Instantiate(Addressables.LoadAssetAsync<SkinDefParams>(key: "1ad54031e06b3f748b4f3e21a2d40f19").WaitForCompletion());
            skinGupGiant.skinDefParams = paramsGupGiant;
            skinGupGiant.skinDefParamsAddress = new AssetReferenceT<SkinDefParams>("");
            skinGupGiant.name = "skinGupGiant";
            skinGupGiant.name = "skinGupGiant_params";

            Material MatGiant = Object.Instantiate(Addressables.LoadAssetAsync<Material>(paramsGupGiant.rendererInfos[0].defaultMaterialAddress.AssetGUID).WaitForCompletion());
            MatGiant.color = new Color(1.5f, 1, 2.5f, 1);
            Material MatEyeGiant = Object.Instantiate(Addressables.LoadAssetAsync<Material>(paramsGupGiant.rendererInfos[2].defaultMaterialAddress.AssetGUID).WaitForCompletion());
            MatEyeGiant.color = new Color(4, 2f, 3, 1);

            var newRender = HG.ArrayUtils.Clone(paramsGupGiant.rendererInfos);
            paramsGupGiant.rendererInfos = newRender;
            newRender[0].defaultMaterial = MatGiant;
            newRender[1].defaultMaterial = MatGiant;
            newRender[2].defaultMaterial = MatEyeGiant;
            newRender[0].defaultMaterialAddress = null;
            newRender[1].defaultMaterialAddress = null;
            newRender[2].defaultMaterialAddress = null;

            mdlGup.GetComponent<ModelSkinController>().skins[0] = skinGupGiant;
            #endregion
            #region Master

            RoR2.CharacterAI.AISkillDriver[] ai = GiantMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>();
            ai[0].maxDistance = 75; //His range is giant

            GiantMaster.AddComponent<GivePickupsOnStart>().equipmentString = "Cleanse";
            GiantMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = "AutoCastEquipment", count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = "BoostEquipmentRecharge", count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = "AdaptiveArmor", count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = "TeleportWhenOob", count = 1, },
            };
            GiantMaster.GetComponent<GivePickupsOnStart>().itemDefInfos = new GivePickupsOnStart.ItemDefInfo[]
            {
                new GivePickupsOnStart.ItemDefInfo
                {
                    count = 35,
                    itemDef = ItemHelpers.ITCooldownUp
                }
            };
            #endregion
            MakeWave();
            #region Log
            unlockable.cachedName = "Logs.GupGiantBody.0";
            unlockable.nameToken = "UNLOCKABLE_LOG_GIANTGUP";
            ContentAddition.AddUnlockableDef(unlockable);
            if (!WConfig.cfgNewEnemiesVisible.Value)
            {
                unlockable = null;
            }
            GiantBody.GetComponent<DeathRewards>().logUnlockableDef = unlockable;
            #endregion
        }
    }

    public class GiantGupSplitDeath : GenericCharacterDeath
    {
        public static CharacterSpawnCard gupCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGupBody");
        public static CharacterSpawnCard geepCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody");
        public static CharacterSpawnCard gipCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody");

        public int gupCount = 3;
        public int geepCount = 6;
        public int gipCount = 9;

        //public CharacterSpawnCard characterSpawnCard
        //public int spawnCount
        public float deathDelay = 0.5f;
        public float moneyMultiplier = 0.5f;
        public static float spawnRadiusCoefficient = 0.5f;
        public static GameObject deathEffectPrefab = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/GupExplosion");
        private bool hasDied;
        public TeamIndex teamIndexGup = TeamIndex.Monster;

        public override void OnEnter()
        {
            base.OnEnter();
            if (teamComponent.teamIndex != TeamIndex.None)
            {
                teamIndexGup = teamComponent.teamIndex;
            }
            characterBody.inventory.RemoveItem(RoR2Content.Items.BoostHp, characterBody.inventory.GetItemCount(RoR2Content.Items.BoostHp));
            characterBody.inventory.RemoveItem(RoR2Content.Items.BoostDamage, characterBody.inventory.GetItemCount(RoR2Content.Items.BoostDamage));
            characterBody.inventory.RemoveItem(RoR2Content.Items.AdaptiveArmor, characterBody.inventory.GetItemCount(RoR2Content.Items.AdaptiveArmor));
            characterBody.inventory.RemoveItem(RoR2Content.Items.TeleportWhenOob, characterBody.inventory.GetItemCount(RoR2Content.Items.TeleportWhenOob));
        }

        public override void OnExit()
        {
            base.DestroyModel();
            base.OnExit();
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (fixedAge > deathDelay && !hasDied)
            {
                hasDied = true;

                if (NetworkServer.active)
                {
                    EffectManager.SpawnEffect(deathEffectPrefab, new EffectData
                    {
                        origin = characterBody.corePosition,
                        scale = characterBody.radius * 3
                    }, true);

                    if (((healthComponent.killingDamageType & (DamageType.VoidDeath | DamageType.OutOfBounds)) == DamageType.Generic))
                    {
                        //This hopefully clears the inv
                        if (gipCard)
                        {
                            new BodySplitter
                            {
                                body = base.characterBody,
                                masterSummon =
                                {
                                    teamIndexOverride = teamIndexGup,
                                    masterPrefab = gipCard.prefab
                                },
                                count = gipCount,
                                splinterInitialVelocityLocal = new Vector3(0f, 30f, 25),
                                minSpawnCircleRadius = base.characterBody.radius * spawnRadiusCoefficient * 1.2f,
                                moneyMultiplier = this.moneyMultiplier
                            }.Perform();
                        }
                        if (geepCard)
                        {
                            new BodySplitter
                            {
                                body = base.characterBody,
                                masterSummon =
                                {
                                    teamIndexOverride = teamIndexGup,
                                    masterPrefab = geepCard.prefab
                                },
                                count = geepCount,
                                splinterInitialVelocityLocal = new Vector3(0f, 25, 20f),
                                minSpawnCircleRadius = base.characterBody.radius * spawnRadiusCoefficient * 1.1f,
                                moneyMultiplier = this.moneyMultiplier
                            }.Perform();
                        }
                        if (gupCard)
                        {
                            new BodySplitter
                            {
                                body = base.characterBody,
                                masterSummon =
                                {
                                    teamIndexOverride = teamIndexGup,
                                    masterPrefab = gupCard.prefab
                                },
                                count = gupCount,
                                splinterInitialVelocityLocal = new Vector3(0f, 20f, 15f), //Default (0f, 20f, 10f),
                                minSpawnCircleRadius = base.characterBody.radius * spawnRadiusCoefficient,
                                moneyMultiplier = this.moneyMultiplier
                            }.Perform();
                        }
                        PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), characterBody.corePosition, Vector3.up * 20f);


                        if (GiantGup.unlockable && Run.instance.CanUnlockableBeGrantedThisRun(GiantGup.unlockable))
                        {
                            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(DeathRewards.logbookPrefab, characterBody.corePosition, UnityEngine.Random.rotation);
                            gameObject.GetComponentInChildren<UnlockPickup>().unlockableDef = GiantGup.unlockable;
                            gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                            NetworkServer.Spawn(gameObject);

                        }
                    }

                    DestroyBodyAsapServer();
                }

                LocalUser user = LocalUserManager.GetFirstLocalUser();
                if (user != null)
                {
                    user.userProfile.AddAchievement("GIANT_GUP_ACHIEVEMENT", false);
                }
            }
        }
    }
}