using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class SimulacrumExtrasHelper : MonoBehaviour
    {
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
                        prefabOverride = wave.rewardPickupPrefab
                    }, position, vector);
                    i++;
                    vector = rotation * vector;
                }
            }
        }
    }


    public class SimuEquipmentWaveHelper : MonoBehaviour
    {
        protected CombatSquad combatSquad;
        public int variant;
        private int bonusHP = 0;

        private void OnEnable()
        {
            InfiniteTowerWaveController controller = this.GetComponent<InfiniteTowerWaveController>();
            if (controller && controller.combatSquad)
            {
                //bonusHP = (Run.instance as InfiniteTowerRun).waveIndex * 2;
                bonusHP = Run.instance.GetComponent<InfiniteTowerRun>().waveIndex / 10 * 2;
                if (variant == 2)
                {
                    bonusHP += 20;
                }
                bonusHP *= Run.instance.participatingPlayerCount;
                combatSquad = controller.combatSquad;
                controller.combatSquad.onMemberDiscovered += this.OnCombatSquadMemberDiscovered;
            }
            if (variant == 1)
            {
                RoR2Content.Equipment.QuestVolatileBattery.dropOnDeathChance = 0.015f;
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius /= 1.8f;
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
                EntityStates.QuestVolatileBattery.CountDown.explosionRadius *= 1.8f;
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
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, 1 + (int)(bonusHP * 1.33f));
                }
                else if (variant == 1)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.QuestVolatileBattery.equipmentIndex);
                    master.inventory.GiveItem(SimuMain.ITHealthScaling, 100); //Double health cuz they die at half
                    //master.inventory.GiveItem(RoR2Content.Items.BoostHp, (int)(bonusHP * 1.5f));
                }
                else if (variant == 2)
                {
                    master.inventory.SetEquipmentIndex(RoR2Content.Equipment.AffixPoison.equipmentIndex);
                    master.inventory.GiveItem(RoR2Content.Items.BoostHp, bonusHP);
                }
            }
        }
    }


    public class SimuBuffWaveHelper : MonoBehaviour
    {
        protected CombatSquad combatSquad;
        public int variant = -1;
        public int count;
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
                            player.master.GetBody().AddBuff(buffDef);
                        }
                    }
                }
            }
            else if (variant == 0)
            {
                count = 3 + (Run.instance.GetComponent<InfiniteTowerRun>().waveIndex) / 5;
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
                            player.master.GetBody().RemoveBuff(buffDef);
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
                    charBody.AddBuff(buffDef);
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
                else if (variant == 1)
                {
                    charBody.GetComponent<ModelLocator>().modelTransform.localScale *= 1.5f;
                }
                
            }
        }
    }

    public class SimulacrumGiveItemsOnStart : MonoBehaviour
    {
        public int count;
        public bool hadCannotyCopy = false;
        public float extraPer10Wave;
        public string itemString;
        public ItemIndex itemIndex = ItemIndex.None;

        private void OnEnable()
        {
            itemIndex = ItemCatalog.FindItemIndex(itemString);
            ItemDef def = ItemCatalog.GetItemDef(itemIndex);
            if (def.ContainsTag(ItemTag.CannotCopy))
            {
                hadCannotyCopy = true;
                def.tags = def.tags.Remove(ItemTag.CannotCopy);
            }
            if (itemIndex == ItemIndex.None)
            {
                Debug.LogWarning("SimulacrumGiveItemsOnStart : Null Item");
            }

            if (NetworkServer.active)
            {
                InfiniteTowerRun itRun = Run.instance.GetComponent<InfiniteTowerRun>();
                int amount = (int)(count + itRun.waveIndex / 10 * extraPer10Wave);
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

            if (NetworkServer.active)
            {
                InfiniteTowerRun itRun = Run.instance.GetComponent<InfiniteTowerRun>();
                int amount = (int)(count + itRun.waveIndex / 10 * extraPer10Wave);
                itRun.enemyInventory.RemoveItem(itemIndex, amount);
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


    public class SimulacrumInteractablesWaveHelper : MonoBehaviour
    {
        public InteractableSpawnCard spawnCard;
        public float interval = 2;
        public int spawnsOnStart = 0;

        public float spawnedTimer;
        public Xoroshiro128Plus rng;

        private void OnEnable()
        {
            if (!NetworkServer.active)
            {
                Destroy(this);
            }
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
                    maxDistance = 60,
                    position = base.gameObject.transform.position,
                    spawnOnTarget = base.gameObject.transform
                };
                DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(spawnCard, placementRule, this.rng));
            }
        }
      
    }

    //Nullify stack everyone wave
    //Cripple + Knockback wave
    //Maybe more

}