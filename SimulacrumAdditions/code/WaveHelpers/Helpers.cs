using RoR2;
using RoR2.Projectile;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using System;
using static UnityEngine.ParticleSystem.PlaybackState;
using R2API.MiscHelpers;
using UnityEngine.UIElements;

namespace SimulacrumAdditions
{
    public class SimulacrumExtrasHelper : MonoBehaviour
    {
        public static bool shareSuitInstalled = false;

        //public bool hasDone = false;
        [SerializeField]
        public ItemTier rewardDisplayTier;
        [SerializeField]
        public PickupDropTable rewardDropTable;

        public int rewardOptionCount = 3;
        public float newRadius = 0;

        [Server]
        public void DropRewards()
        {
            if (!NetworkServer.active)
            {
                Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::DropRewards()' called on client");
                return;
            }
            InfiniteTowerWaveController wave = base.gameObject.GetComponent<InfiniteTowerWaveController>();
            int participatingPlayerCount = Run.instance.participatingPlayerCount;

            if (shareSuitInstalled)
            {
                participatingPlayerCount = 1;
            }

            if (participatingPlayerCount > 0 && wave.spawnTarget && this.rewardDropTable)
            {
                int num = participatingPlayerCount;
                float angle = 360f / (float)num;
                Vector3 vector = Quaternion.AngleAxis((float)UnityEngine.Random.Range(0, 360), Vector3.up) * (Vector3.up * 45f + Vector3.forward * 9f);
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.up);
                Vector3 position = wave.spawnTarget.transform.position + wave.rewardOffset;
                int i = 0;
                while (i < num)
                {
                    PickupDropletController.CreatePickupDroplet(new GenericPickupController.CreatePickupInfo
                    {
                        pickupIndex = PickupCatalog.FindPickupIndex(this.rewardDisplayTier),
                        pickerOptions = PickupPickerController.GenerateOptionsFromDropTable(this.rewardOptionCount, this.rewardDropTable, wave.rng),
                        rotation = Quaternion.identity,
                        position = position,
                        prefabOverride = wave.rewardPickupPrefab
                    }, position, vector);
                    i++;
                    vector = rotation * vector;
                }
            }
        }
    }

    public class SimuExplicitStats : MonoBehaviour
    {
        public int ExtraSpawnAfterWave = -1;
        public bool halfOnNonFinal = false;
        public bool spawnAsVoidTeam = false;
        public float hpBonusMulti = 0.5f;
        public float damageBonusMulti = 0.5f;

        public void DoTheThing()
        {

        }


    }

    public class SimuEquipmentWaveHelper : MonoBehaviour
    {
        public enum Variant
        {
            None,
            Jetpack,
            FuelArray,
            Malachite,
            GooboJr,
            Twisted
        }

        protected CombatSquad combatSquad;
        public Variant variant;
        private int bonusHP = 0;
        public bool bonusBonus = true;
        public int counter = 100;

        private void OnEnable()
        {
            InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();


            if (variant == Variant.Malachite)
            {
                bonusHP += 10;
            }
            if (controller && controller.combatSquad)
            {
                bonusHP += Run.instance.GetComponent<InfiniteTowerRun>().waveIndex / 10 * 2;
                bonusHP *= Run.instance.participatingPlayerCount;
                combatSquad = controller.combatSquad;
                controller.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
            }
            
            if (variant == Variant.FuelArray)
            {
                RoR2Content.Equipment.QuestVolatileBattery.dropOnDeathChance = 0.015f;
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius /= 1.8f;
            }
            else if (variant == Variant.GooboJr)
            {
                foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                {
                    if (player.master.bodyInstanceObject)
                    {
                        player.master.GetBody().equipmentSlot.FireGummyClone();
                    }
                }
            }
        }

        private void OnDisable()
        {
            if (this.combatSquad)
            {
                this.combatSquad.onMemberDiscovered -= this.OnCombatSquadMemberDiscovered;
            }
            if (variant == Variant.FuelArray)
            {
                RoR2Content.Equipment.QuestVolatileBattery.dropOnDeathChance = 0;
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius *= 1.8f;
            }
        }

        protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
        {
            CharacterBody body = master.GetBody();        
            if (NetworkServer.active)
            {
                if (body)
                {
                    //Can't really add network component just like that would need a better solution ideally
                    if (!body.equipmentSlot)
                    {
                        EquipmentSlot slot = body.gameObject.AddComponent<EquipmentSlot>();
                        slot.hasEffectiveAuthority = true;
                        slot._rechargeTime = Run.FixedTimeStamp.zero;
                    }
                }
                if (variant == Variant.Jetpack)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.Jetpack.equipmentIndex);
                    master.inventory.GiveItem(RoR2Content.Items.AutoCastEquipment, 1);
                    master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 7);
                    master.inventory.GiveItem(RoR2Content.Items.SprintOutOfCombat, 1);
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, 1 + (int)(bonusHP * 1.33f));
                }
                else if (variant == Variant.FuelArray)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex);
                    master.inventory.GiveItem(ItemHelpers.ITHealthScaling, 80); //Double health cuz they die at half
                    //master.inventory.GiveItem(RoR2Content.Items.BoostHp, (int)(bonusHP * 1.5f));
                }
                else if (variant == Variant.Malachite)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.AffixPoison.equipmentIndex);
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, bonusHP);
                }
                else if (variant == Variant.GooboJr)
                {
                    if (master.inventory.GetItemCount(DLC1Content.Items.GummyCloneIdentifier) == 0)
                    {
                        master.inventory.SetEquipmentIndex(DLC1Content.Equipment.GummyClone.equipmentIndex);
                        master.inventory.GiveItem(RoR2Content.Items.AutoCastEquipment, 1);
                        master.inventory.GiveItem(RoR2Content.Items.EquipmentMagazine, 1);
                        master.inventory.GiveItem(RoR2Content.Items.BoostHp, 3);
                    }
                    else
                    {
                        master.inventory.RemoveItem(RoR2Content.Items.BoostHp, 15);
                        master.inventory.RemoveItem(RoR2Content.Items.BoostDamage, 15);
                    }
                }
                else if (variant == Variant.Twisted)
                {
                    counter++;
                    if (master.inventory.GetItemCount(RoR2Content.Items.BoostDamage) < 10)
                    {
                        if (counter > 10)
                        {
                            counter = 0;
                            master.inventory.SetEquipmentIndex(DLC2Content.Equipment.EliteBeadEquipment.equipmentIndex);
                            master.inventory.GiveItem(RoR2Content.Items.BoostDamage, 5);
                            master.inventory.GiveItem(RoR2Content.Items.BoostHp, 30);
                        }
                    }
                    
                }
            }
        }
    }

    public class SimuBuffWaveHelper : MonoBehaviour
    {
        protected CombatSquad combatSquad;
        public int variant = -1;
        public int count;
        public float duration = -1;
        public bool timed = false;
        public bool addToPlayer = false;
        public bool addToEnemies = true;
        public BuffDef buffDef;

        private void OnEnable()
        {
            if (addToEnemies)
            {
                InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();
                if (controller && controller.combatSquad)
                {
                    combatSquad = controller.combatSquad;
                    controller.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
                }
            }

            if (addToPlayer && variant == -1)
            {
                if (NetworkServer.active)
                {
                    foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                    {
                        if (player.master.bodyInstanceObject)
                        {
                            if (timed)
                            {
                                player.master.GetBody().AddTimedBuff(buffDef, duration);
                            }
                            else
                            {
                                player.master.GetBody().AddBuff(buffDef);
                            }
                        }
                    }
                }
            }
            else if (variant == 0)
            {
                count = 2 + (Run.instance.GetComponent<InfiniteTowerRun>().waveIndex) / 10;
            }
        }

        private void OnDisable()
        {
            if (addToEnemies && combatSquad)
            {
                combatSquad.onMemberDiscovered -= this.OnCombatSquadMemberDiscovered;
            }
            if (addToPlayer && variant == -1)
            {
                if (NetworkServer.active)
                {
                    foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                    {
                        if (player.master.bodyInstanceObject)
                        {
                            if (player.master.GetBody().HasBuff(buffDef))
                            {
                                player.master.GetBody().RemoveBuff(buffDef);
                            }
                        }
                    }
                }
            }
        }

        protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
        {
            if (NetworkServer.active)
            {
                CharacterBody charBody = master.GetBody();
                if (variant == -1)
                {
                    if (timed)
                    {
                        charBody.AddTimedBuff(buffDef, duration);
                    }
                    else
                    {
                        charBody.AddBuff(buffDef);
                    }
                }
                else if (variant == 0)
                {
                    master.inventory.GiveItem(DLC1Content.Items.BearVoid, count);
                    charBody.AddTimedBuff(RoR2Content.Buffs.HiddenInvincibility, 0.5f);
                    for (int i = 0; i < count; i++)
                    {
                        charBody.AddBuff(DLC1Content.Buffs.BearVoidReady);
                    }
                }
            }
        }
    }

    public class SimuWaveSizeModifier : MonoBehaviour
    {
        public float sizeModifier;
        public ItemDef neededItem;

        private void OnEnable()
        {
            On.RoR2.CharacterModel.Start += ChangeSize;
        }

        private void ChangeSize(On.RoR2.CharacterModel.orig_Start orig, CharacterModel self)
        {
            orig(self);
            if (self.body.teamComponent.teamIndex != TeamIndex.Player)
            {
                if (neededItem == null || self.body.inventory.GetItemCount(neededItem) > 0)
                {
                    self.transform.localScale *= sizeModifier;

                    if (self.body.aimOriginTransform)
                    {
                        self.body.aimOriginTransform.localPosition *= sizeModifier; //Ig???
                    }
                }
            }

        }

        private void OnDisable()
        {
            On.RoR2.CharacterModel.Start -= ChangeSize;
        }
    }

    public class SimuWaveAlwaysJumping : MonoBehaviour
    {
        private void OnEnable()
        {
            On.EntityStates.GenericCharacterMain.ProcessJump += AlwaysSpamJump;
        }

        private void OnDisable()
        {
            On.EntityStates.GenericCharacterMain.ProcessJump -= AlwaysSpamJump;
        }

        private void AlwaysSpamJump(On.EntityStates.GenericCharacterMain.orig_ProcessJump orig, EntityStates.GenericCharacterMain self)
        {
            self.jumpInputReceived = true;
            orig(self);
        }
    }

    public class SimuWaveBouncyProjectiles : MonoBehaviour
    {
        private static UnityEngine.PhysicMaterial bouncyMat = Addressables.LoadAssetAsync<PhysicMaterial>(key: "RoR2/DLC1/MajorAndMinorConstruct/physmatMinorConstructProjectile.physicMaterial").WaitForCompletion();

        private void OnEnable()
        {
            On.RoR2.Projectile.ProjectileController.Start += BouncyProjectiles;
        }

        private void OnDisable()
        {
            On.RoR2.Projectile.ProjectileController.Start -= BouncyProjectiles;
        }

        private void BouncyProjectiles(On.RoR2.Projectile.ProjectileController.orig_Start orig, RoR2.Projectile.ProjectileController self)
        {
            orig(self);
            if (self.myColliders.Length > 0)
            {
                self.myColliders[0].material = bouncyMat;
                //self.myColliders[0].isTrigger = false; //Probably too invasive
            }
            var a = self.GetComponent<ProjectileImpactExplosion>();
            if (a)
            {
                a.destroyOnWorld = false;
                a.impactOnWorld = false;
            }
            var b = self.GetComponent<ProjectileSingleTargetImpact>();
            if (b)
            {
                b.destroyOnWorld = false;
            }
        }
    }

    public class SimulacrumGiveItemsOnStart : MonoBehaviour
    {
        public int count;
        public bool hadCannotyCopy = false;
        public bool hideItem = false;
        public float extraPer10Wave;
        public string itemString;
        public ItemIndex itemIndex = ItemIndex.None;

        private void OnEnable()
        {
            itemIndex = ItemCatalog.FindItemIndex(itemString);
            ItemDef def = ItemCatalog.GetItemDef(itemIndex);
            if (itemIndex == ItemIndex.None)
            {
                Debug.LogWarning("SimulacrumGiveItemsOnStart : Null Item");
                return;
            }
            if (def.ContainsTag(ItemTag.CannotCopy))
            {
                hadCannotyCopy = true;
                def.tags = def.tags.Remove(ItemTag.CannotCopy);
            }
            if (hideItem)
            {
                def.hidden = true;
            }
            if (NetworkServer.active)
            {
                InfiniteTowerRun itRun = Run.instance.GetComponent<InfiniteTowerRun>();               
                int bonusAmount = (int)(itRun.waveIndex / 10 * extraPer10Wave);
                if (bonusAmount < 0)
                {
                    bonusAmount = 0;
                }
                int amount = count + bonusAmount;
                itRun.enemyInventory.GiveItem(itemIndex, amount);
            }
        }

        private void OnDisable()
        {
            ItemDef def = ItemCatalog.GetItemDef(itemIndex);
            if (hadCannotyCopy == true)
            {
                hadCannotyCopy = false;
                def.tags = def.tags.Add(ItemTag.CannotCopy);
            }
            if (hideItem)
            {
                def.hidden = false;
            }
            if (NetworkServer.active)
            {
                InfiniteTowerRun itRun = Run.instance.GetComponent<InfiniteTowerRun>();
                int bonusAmount = (int)(itRun.waveIndex / 10 * extraPer10Wave);
                if (bonusAmount < 0)
                {
                    bonusAmount = 0;
                }
                int amount = count + bonusAmount;
                itRun.enemyInventory.RemoveItem(itemIndex, amount);
            }
        }
    }

    public class SimulacrumInteractablesWaveHelper : MonoBehaviour
    {
        public InteractableSpawnCard spawnCard;
        public float interval = 2;
        public int spawnsOnStart = 0;
        public int maxDistance = 55;

        public float spawnedTimer;
        public Xoroshiro128Plus rng;

        private void OnEnable()
        {
            if (NetworkServer.active)
            {
                InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
                rng = new Xoroshiro128Plus((ulong)((long)run.waveIndex ^ (long)Run.instance.seed));
                base.gameObject.transform.position = run.safeWardController.transform.GetChild(2).GetChild(0).position;

                for (int i = 0; i < spawnsOnStart; i++)
                {
                    DirectorPlacementRule placementRule = new DirectorPlacementRule
                    {
                        placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                        minDistance = 0,
                        maxDistance = 60,
                        position = base.gameObject.transform.position,
                        spawnOnTarget = base.gameObject.transform
                    };
                    DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, this.rng));
                }
            }
            else
            {
                Destroy(this);
            }
        }

        private void FixedUpdate()
        {
            spawnedTimer -= Time.deltaTime;
            if (spawnedTimer <= 0)
            {
                spawnedTimer += interval;
                DirectorPlacementRule placementRule = new DirectorPlacementRule
                {
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    minDistance = 0,
                    maxDistance = maxDistance,
                    position = base.gameObject.transform.position,
                    spawnOnTarget = base.gameObject.transform
                };
                DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, this.rng));
            }
        }

    }

    public class SimulacrumEliteWaves : MonoBehaviour
    {
        public CombatDirector.EliteTierDef[] backupTiers;
        public CombatDirector.EliteTierDef[] newTiers;
        public bool lunarOnly = false;
        public bool voidOnly = false;
        public bool lunarPlusVoid = false;
        public bool addLunar = false;

        private void OnEnable()
        {
            backupTiers = CombatDirector.eliteTiers;

            CombatDirector.EliteTierDef[] arrayL = new CombatDirector.EliteTierDef[2];
            arrayL[0] = new CombatDirector.EliteTierDef
            {
                costMultiplier = 1,
                eliteTypes = new EliteDef[1],
                isAvailable = (SpawnCard.EliteRules rules) => CombatDirector.NotEliteOnlyArtifactActive(),
                canSelectWithoutAvailableEliteDef = true,
            };
            if (lunarOnly)
            {
                arrayL[1] = new CombatDirector.EliteTierDef
                {
                    costMultiplier = 3f,
                    eliteTypes = new EliteDef[]
                    {
                                RoR2Content.Elites.Lunar,
                    },
                    isAvailable = (SpawnCard.EliteRules rules) => true,
                    canSelectWithoutAvailableEliteDef = false,
                };
                newTiers = arrayL;
            }
            else if (voidOnly)
            {
                arrayL[1] = new CombatDirector.EliteTierDef
                {
                    costMultiplier = 3f,
                    eliteTypes = new EliteDef[]
                    {
                                DLC1Content.Elites.Void,
                    },
                    isAvailable = (SpawnCard.EliteRules rules) => true,
                    canSelectWithoutAvailableEliteDef = false,
                };
                newTiers = arrayL;
            }
            else if (lunarPlusVoid)
            {
                arrayL[1] = new CombatDirector.EliteTierDef
                {
                    costMultiplier = 3f,
                    eliteTypes = new EliteDef[]
                    {
                                RoR2Content.Elites.Lunar,
                                DLC1Content.Elites.Void,
                    },
                    isAvailable = (SpawnCard.EliteRules rules) => true,
                    canSelectWithoutAvailableEliteDef = false,
                };
                newTiers = arrayL;
            }
            else if (addLunar)
            {
                CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Add(RoR2Content.Elites.Lunar);
                CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Add(RoR2Content.Elites.Lunar);
            }


            if (newTiers == null)
            {
                Debug.LogWarning("no elite tiers to overwrite");
                return;
            }
            CombatDirector.eliteTiers = newTiers;
        }
        private void OnDisable()
        {
            if (addLunar)
            {
                CombatDirector.eliteTiers[1].eliteTypes = CombatDirector.eliteTiers[1].eliteTypes.Remove(RoR2Content.Elites.Lunar);
                CombatDirector.eliteTiers[2].eliteTypes = CombatDirector.eliteTiers[2].eliteTypes.Remove(RoR2Content.Elites.Lunar);
            }
            if (backupTiers != null)
            {
                CombatDirector.eliteTiers = backupTiers;
            }
        }
    }

    public class SimulacrumEclipseWaveHelper : MonoBehaviour
    {
        public DifficultyIndex previous;

        private void OnEnable()
        {
            previous = Run.instance.selectedDifficulty;
            Run.instance.selectedDifficulty = DifficultyIndex.Eclipse8;
        }

        private void OnDisable()
        {
            if (Run.instance)
            {
                Run.instance.selectedDifficulty = previous;
                foreach (PlayerCharacterMasterController player in PlayerCharacterMasterController.instances)
                {
                    if (player.master.hasBody)
                    {
                        CharacterBody body = player.master.GetBody();
                        int count = body.GetBuffCount(RoR2Content.Buffs.PermanentCurse);
                        for (int i = 0; i < count; i++)
                        {
                            body.RemoveBuff(RoR2Content.Buffs.PermanentCurse);
                        }
                    }
                }
            }
        }
    }

    public class SimulacrumArtifactTrialWave : MonoBehaviour
    {
        public ArtifactDef artifactDef;
        public ArtifactDef artifactDef2;
        public bool artifactWasEnabled;
        public bool artifactWasEnabled2;
        public int count = 0;

        public void Awake()
        {
            if (NetworkServer.active)
            {
                RollArtifact();
            }
            RunArtifactManager.onArtifactEnabledGlobal += ArtifactListerner;
        }

        private void ArtifactListerner([JetBrains.Annotations.NotNull] RunArtifactManager runArtifactManager, [JetBrains.Annotations.NotNull] ArtifactDef artifactDef)
        {
            InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();
            GameObject ui = controller.overlayEntries[1].prefab;
            if (count == 0)
            {
                ui.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = artifactDef.smallIconSelectedSprite;
            }
            else if (count == 1)
            {
                ui.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = artifactDef.smallIconSelectedSprite;
            }
            count++;
        }

        public void RollArtifact()
        {
            List<ArtifactDef> artifactList = new List<ArtifactDef>()
                {
                    RoR2Content.Artifacts.bombArtifactDef,
                    RoR2Content.Artifacts.wispOnDeath,
                    RoR2Content.Artifacts.swarmsArtifactDef,
                    RoR2Content.Artifacts.sacrificeArtifactDef,
                    RoR2Content.Artifacts.mixEnemyArtifactDef,
                    RoR2Content.Artifacts.singleMonsterTypeArtifactDef,
                    RoR2Content.Artifacts.commandArtifactDef,
                    RoR2Content.Artifacts.monsterTeamGainsItemsArtifactDef,
                };

            ArtifactDef Brigade = ArtifactCatalog.FindArtifactDef("SingleEliteType");
            if (Brigade != null)
            {
                artifactList.Add(Brigade);
            }

            int random = WRect.random.Next(0, artifactList.Count);
            artifactDef = artifactList[random];
            artifactList.Remove(artifactDef);

            int random2 = WRect.random.Next(0, artifactList.Count);
            artifactDef2 = artifactList[random2];


        }

        public void OnEnable()
        {
            if (NetworkServer.active)
            {
                this.artifactWasEnabled = RunArtifactManager.instance.IsArtifactEnabled(this.artifactDef);
                RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef, true);

                this.artifactWasEnabled2 = RunArtifactManager.instance.IsArtifactEnabled(this.artifactDef2);
                RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef2, true);
            }
        }

        private void OnDisable()
        {
            RunArtifactManager.onArtifactEnabledGlobal -= ArtifactListerner;
            if (NetworkServer.active && RunArtifactManager.instance)
            {
                RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef2, this.artifactWasEnabled2);
                RunArtifactManager.instance.SetArtifactEnabledServer(this.artifactDef, this.artifactWasEnabled);
            }
        }

        protected virtual void OnCombatSquadMemberDiscovered(CharacterMaster master)
        {
            int kill = master.inventory.GetItemCount(ItemHelpers.ITKillOnCompletion);
            if (kill > 0)
            {
                master.GetBody().RemoveBuff(RoR2Content.Buffs.Immune);
                master.GetBody().healthComponent.Networkhealth *= 0.1f;

            }
        }

    }

    public class SimulacrumLightningStormWave : MonoBehaviour
    {
        public GameObject LightningStorm;
        public GameObject LightningStrike;
        public bool ShouldActivate = false;
        public int timer = 0;

        public void Awake()
        {
            if (NetworkServer.active)
            {
                LightningStorm = GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/meridian/DisableSkillsLightning/LightningStormController.prefab").WaitForCompletion());
                NetworkServer.Spawn(LightningStorm);
            }
        }

        public void OnEnable()
        {
            LightningStrike = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/meridian/DisableSkillsLightning/LightningStrikeInstance.prefab").WaitForCompletion();
            LightningStrike.GetComponent<TeamComponent>().teamIndex = TeamIndex.Neutral;
            var Lightning = LightningStrike.GetComponent<LightningStrikeInstance>();
            Lightning.blastDamageType.damageTypeExtended = DamageTypeExtended.DamagePercentOfMaxHealth;
            if (LightningStorm)
            {
                ShouldActivate = true;
            }

            //Spawn Geodes
            InteractableSpawnCard spawnCard = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC2/iscGeode.asset").WaitForCompletion();
            int spawnsOnStart = Run.instance.participatingPlayerCount+1;
            GameObject Effect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/GeodeShatterVFX");

            if (NetworkServer.active)
            {
                InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
                Xoroshiro128Plus rng = new Xoroshiro128Plus((ulong)((long)run.waveIndex ^ (long)Run.instance.seed));
                base.gameObject.transform.position = run.safeWardController.transform.GetChild(2).GetChild(0).position;

                for (int i = 0; i < spawnsOnStart; i++)
                {
                    DirectorPlacementRule placementRule = new DirectorPlacementRule
                    {
                        placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                        minDistance = 10,
                        maxDistance = 50,
                        position = base.gameObject.transform.position,
                        spawnOnTarget = base.gameObject.transform
                    };
                    GameObject Result = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, rng));
                    if (Result)
                    {
                        EffectManager.SpawnEffect(Effect, new EffectData
                        {
                            origin = Result.transform.position
                        }, false);
                    }
                }
            }
            else
            {
                Destroy(this);
            }
        }

        public void FixedUpdate()
        {
            if (ShouldActivate)
            {
                timer++;
                if (timer > 90)
                {
                    LightningStorm.GetComponent<LightningStormController>().ServerSetStormActive(true);
                    ShouldActivate = false;
                }
            }
        }

        public void OnDisable()
        {
            var Lightning = LightningStrike.GetComponent<LightningStrikeInstance>();
            Lightning.blastDamageType.damageTypeExtended = DamageTypeExtended.ApplyBuffPermanently | DamageTypeExtended.DamagePercentOfMaxHealth;
        }

        public void DisableStorm()
        {
            if (LightningStorm)
            {
                LightningStorm.GetComponent<LightningStormController>().ServerSetStormActive(false);
                GameObject.Destroy(LightningStorm, 3);
            }

            if (NetworkServer.active)
            {
                GeodeController[] geodes = GameObject.FindObjectsOfType(typeof(GeodeController)) as GeodeController[];
                if (geodes.Length > 0)
                {
                    GameObject Effect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/GeodeShatterVFX");
                    for (int i = 0; i < geodes.Length; i++)
                    {
                        Debug.Log(geodes[i]);
                        GameObject.Destroy(geodes[i].gameObject);
                        EffectManager.SpawnEffect(Effect, new EffectData
                        {
                            origin = geodes[i].transform.position
                        }, false);
                    }
                }
            }
            
        }
 

    }

    public class Simulacrum_KnockbackWave : MonoBehaviour
    {
        public void OnDisable()
        {
            On.RoR2.HealthComponent.TakeDamageForce_DamageInfo_bool_bool -= HealthComponent_TakeDamageForce_DamageInfo_bool_bool;
        }
        public void OnEnable()
        {
            On.RoR2.HealthComponent.TakeDamageForce_DamageInfo_bool_bool += HealthComponent_TakeDamageForce_DamageInfo_bool_bool;
        }

        private void HealthComponent_TakeDamageForce_DamageInfo_bool_bool(On.RoR2.HealthComponent.orig_TakeDamageForce_DamageInfo_bool_bool orig, HealthComponent self, DamageInfo damageInfo, bool alwaysApply, bool disableAirControlUntilCollision)
        {
            orig(self, damageInfo, true, disableAirControlUntilCollision);
            if (damageInfo.attacker && damageInfo.procCoefficient > 0)
            {
                CharacterMotor victimMotor = self.body.characterMotor;
                RigidbodyMotor rigid = self.GetComponent<RigidbodyMotor>();

                float scale = 1f;
                switch (self.body.hullClassification)
                {
                    case HullClassification.Human:
                        scale = 1f;
                        break;
                    case HullClassification.Golem:
                        scale = 2.5f;
                        break;
                    case HullClassification.BeetleQueen:
                        scale = 5f;
                        break;
                }
                if (rigid)
                {
                    scale *= -1f;
                }
                EffectManager.SpawnEffect(CharacterBody.CommonAssets.knockbackFinEffect, new EffectData
                {
                    origin = self.body.gameObject.transform.position,
                    scale = scale
                }, true);

                if (victimMotor )
                {
                    float mult = victimMotor.isGrounded ? 1.5f : 0.33f;
                    mult *= damageInfo.procCoefficient;
                    float num = KnockbackFinUtil.CalculateImpulseForce(victimMotor.mass, 4, self.body.isChampion, false) * mult;
                    float num2 = num;
                    float y = victimMotor.velocity.y;
                    if (y < 0f)
                    {
                        num2 -= y * victimMotor.mass;
                    }
                    Vector3 a = num2 * Vector3.up;
                    Vector3 direction = damageInfo.attacker.GetComponent<InputBankTest>().GetAimRay().direction;
                    direction.y = 0f;
                    Vector3 b = num * 0.2f * 1 * direction.normalized;
                    victimMotor.ApplyForce(a + b, false, false);

                   
                }
                else if (rigid)
                {
                    float mult = -0.5f;
                    mult *= damageInfo.procCoefficient;
                    float num = KnockbackFinUtil.CalculateImpulseForce(rigid.mass, 4, self.body.isChampion, false) * mult;
                    float num2 = num;
                    float y = rigid.moveVector.y;
                    if (y < 0f)
                    {
                        num2 -= y * rigid.mass;
                    }
                    Vector3 a = num2 * Vector3.up;
                    Vector3 direction = damageInfo.attacker.GetComponent<InputBankTest>().GetAimRay().direction;
                    direction.y = 0f;
                    Vector3 b = num * 0.2f * 1 * direction.normalized;
                    rigid.ApplyForceImpulse(new PhysForceInfo { force = a+b});
                }

            }
            
            
        }

         

        
    }

    public class DisableRegeneratingScrap : MonoBehaviour
    {
        private bool wasEnabled;

        private void OnEnable()
        {
            RuleDef ruleDef = RuleCatalog.FindRuleDef("Items." + DLC1Content.Items.RegeneratingScrap.name);
            RuleChoiceDef ruleChoiceDef = (ruleDef != null) ? ruleDef.FindChoice("Off") : null;
            if (ruleChoiceDef != null)
            {
                Run.instance.ruleBook.ApplyChoice(ruleChoiceDef);
            }
        }

        private void OnDisable()
        {
            RuleDef ruleDef = RuleCatalog.FindRuleDef("Items." + DLC1Content.Items.RegeneratingScrap.name);
            RuleChoiceDef ruleChoiceDef = (ruleDef != null) ? ruleDef.FindChoice("On") : null;
            if (ruleChoiceDef != null)
            {
                Run.instance.ruleBook.ApplyChoice(ruleChoiceDef);
            }
        }
    }

    public class DisableArtifactOfSwarms : MonoBehaviour
    {
        private bool wasEnabled;

        private void OnEnable()
        {
            if (NetworkServer.active)
            {
                this.wasEnabled = RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.Swarms);
                RunArtifactManager.instance.SetArtifactEnabledServer(RoR2Content.Artifacts.Swarms, false);
            }
        }

        private void OnDisable()
        {
            if (NetworkServer.active && RunArtifactManager.instance)
            {
                RunArtifactManager.instance.SetArtifactEnabledServer(RoR2Content.Artifacts.Swarms, this.wasEnabled);
            }
        }
    }


}