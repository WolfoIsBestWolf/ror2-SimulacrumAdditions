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
        public static GameObject GiantBody = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/GupBody"), "GiantGupBody", true);
        public static GameObject GiantMaster = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/GupMaster"), "GiantGupMaster", true);

        public static void Start()
        {
            GameObject InfiniteTowerWaveBossGiantGup = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGiantGup", true);
            GameObject InfiniteTowerWaveBossGiantGupUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossGiantGupWaveUI", false);
            CharacterSpawnCard cscGiantGup;

            ContentAddition.AddBody(GiantBody);
            ContentAddition.AddMaster(GiantMaster);


            GiantMaster.GetComponent<CharacterMaster>().bodyPrefab = GiantBody;
            if (!WConfig.cfgNewEnemiesVisible.Value)
            {
                GiantBody.GetComponent<DeathRewards>().logUnlockableDef = null;
            }


            GiantBody.GetComponent<SetStateOnHurt>().canBeHitStunned = false;
            GiantBody.GetComponent<SetStateOnHurt>().canBeStunned = false;
            GiantBody.GetComponent<SetStateOnHurt>().canBeFrozen = false;

            CharacterBody GiantCharacterBody = GiantBody.GetComponent<CharacterBody>();

            LanguageAPI.Add("GIANTGUP_BODY_NAME", "Guap", "en");
            GiantCharacterBody.baseNameToken = "GIANTGUP_BODY_NAME";
            GiantCharacterBody.isChampion = true;

            Texture2D GiantGupBodyIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            GiantGupBodyIcon.LoadImage(Properties.Resources.GiantGupBody, true);
            GiantGupBodyIcon.filterMode = FilterMode.Bilinear;
            GiantGupBodyIcon.wrapMode = TextureWrapMode.Clamp;
            GiantCharacterBody.portraitIcon = GiantGupBodyIcon;


            //Giant Gup gets special scaling in IT
            //Also remember he has Shiny Pearl
            //GiantCharacterBody.baseDamage *= 1f;
            GiantCharacterBody.baseMaxHealth *= 1.5f; //Base Health is 1000
            GiantCharacterBody.baseDamage *= 1.3f;

            GiantCharacterBody.baseAttackSpeed *= 0.55f;
            GiantCharacterBody.baseMoveSpeed *= 0.65f;
            GiantCharacterBody.baseJumpPower *= 2.5f;
            GiantCharacterBody.baseArmor = 20;
            GiantCharacterBody.PerformAutoCalculateLevelStats();

            //
            bool wasAdded;
            SerializableEntityStateType GiantGupDeathState = ContentAddition.AddEntityState<GiantGupSplitDeath>(out wasAdded);
            GiantBody.GetComponent<RoR2.CharacterDeathBehavior>().deathState = GiantGupDeathState;
            //Visuals
            GameObject mdlGup = GiantBody.transform.GetChild(0).GetChild(0).gameObject;
            mdlGup.transform.localScale = new Vector3(4, 4, 4);

            mdlGup.GetComponent<ModelPanelParameters>().minDistance = 5;
            mdlGup.GetComponent<ModelPanelParameters>().maxDistance = 9;

            CharacterModel gupModel = mdlGup.GetComponent<CharacterModel>();

            Material MatGiant = Object.Instantiate(gupModel.baseRendererInfos[0].defaultMaterial);
            MatGiant.color = new Color(1.5f, 1, 2.5f, 1);
            gupModel.baseRendererInfos[0].defaultMaterial = MatGiant;
            gupModel.baseRendererInfos[1].defaultMaterial = MatGiant;

            Material MatEyeGiant = Object.Instantiate(gupModel.baseRendererInfos[2].defaultMaterial);
            MatEyeGiant.color = new Color(4, 2f, 3, 1);
            gupModel.baseRendererInfos[2].defaultMaterial = MatEyeGiant;


            //Master increase range of attacks or smth
            RoR2.CharacterAI.AISkillDriver[] ai = GiantMaster.GetComponents<RoR2.CharacterAI.AISkillDriver>();
            ai[0].maxDistance = 90; //His range is giant

            //
            GiantMaster.AddComponent<GivePickupsOnStart>().equipmentString = "Cleanse";
            GiantMaster.GetComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("AutoCastEquipment"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("BoostEquipmentRecharge"), count = 5, },
                new GivePickupsOnStart.ItemInfo { itemString = ("AdaptiveArmor"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("TeleportWhenOob"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ShinyPearl"), count = 1, },
            };


            //
            CharacterSpawnCard gupCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGupBody");
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
            //

            //Spawns
            InfiniteTowerWaveBossGiantGup.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 100;
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0;
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 5;
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscGiantGup;
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;

            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossGiantGup.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;

            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;
            //
            Texture2D texITWaveGupIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveGupIcon.LoadImage(Properties.Resources.texITWaveGupIcon, true);
            texITWaveGupIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveGupIconS = Sprite.Create(texITWaveGupIcon, WRect.rec64, WRect.half);

            //Color GupColor = new Color32(255, 161, 15, 255);
            //Color GupColor = new Color32(255, 122, 104, 255); 
            Color GupColor = new Color(1f, 0.6784f, 0.6278f, 1);
            InfiniteTowerWaveBossGiantGupUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveGupIconS;
            InfiniteTowerWaveBossGiantGupUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = GupColor;
            InfiniteTowerWaveBossGiantGupUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = GupColor;

            InfiniteTowerWaveBossGiantGup.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossGiantGupUI;
            InfiniteTowerWaveBossGiantGupUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Giant";
            InfiniteTowerWaveBossGiantGupUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Goodness Gracious.";
            //
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITGiantGup = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossGiantGup, weight = 6, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITGiantGup);

            /*ITGiantGup.weight = 25;
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITGiantGup);*/

            /*ITGiantGup.weight = 5000;
			SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITGiantGup);*/
            //, prerequisites = SimuMain.Wave11OrGreaterPrerequisite
        }
    }

    public class GiantGupSplitDeath : GenericCharacterDeath
    {
        public static CharacterSpawnCard gupCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGupBody");
        public static CharacterSpawnCard geepCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGeepBody");
        public static CharacterSpawnCard gipCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody");

        public int gupCount = 4;
        public int geepCount = 6;
        public int gipCount = 10;

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
                        PickupDropletController.CreatePickupDroplet(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), characterBody.corePosition, Vector3.up * 20f);
                    }

                    DestroyBodyAsapServer();
                }
            }
        }
    }
}