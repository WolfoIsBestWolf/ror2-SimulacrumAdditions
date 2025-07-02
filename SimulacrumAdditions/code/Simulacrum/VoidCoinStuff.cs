using RoR2;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class VoidCoin
    {
        public static CostTypeDef CostTypeVoidCoinBlood;
        public static CostTypeIndex CostIndexVoidCoinBlood;

        public static void MakeVoidCoin()
        {
            CostTypeCatalog.modHelper.getAdditionalEntries += addVoidBloodCost;
            On.RoR2.PlayerCharacterMasterController.Start += StartWithOneVoidCoin;

            GameObject VoidCoinBarrel = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel");
            ChestBehavior VoidBarrelChestBehavior = VoidCoinBarrel.AddComponent<ChestBehavior>();
            VoidBarrelChestBehavior.dropTable = LegacyResourcesAPI.Load<ExplicitPickupDropTable>("DropTables/dtVoidCoin");
            VoidBarrelChestBehavior.dropTransform = VoidCoinBarrel.transform.GetChild(0).GetComponent<ChildLocator>().FindChild("Bulb");
            VoidBarrelChestBehavior.dropUpVelocityStrength = 20;
            VoidBarrelChestBehavior.enabled = false;

            PurchaseInteraction purch = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSuppressor/VoidSuppressor.prefab").WaitForCompletion().GetComponent<PurchaseInteraction>();
            purch.costType = CostTypeIndex.None;
            purch.cost = 1;

        }


        private static void StartWithOneVoidCoin(On.RoR2.PlayerCharacterMasterController.orig_Start orig, PlayerCharacterMasterController self)
        {
            orig(self);
            if (WConfig.cfgVoidCoins.Value)
            {
                if (NetworkServer.active && Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
                {
                    self.master.GiveVoidCoins(1);
                    VoidCoinChance chance = self.gameObject.AddComponent<VoidCoinChance>();
                    float players = 1 + (Run.instance.participatingPlayerCount - 1) * 0.2f; //This probably don't really work
                    chance.chance /= players;
                };
            }
        }

        public static void VoidCoinRunStart()
        {
            PurchaseInteraction VoidChest = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidChest").GetComponent<PurchaseInteraction>();
            if (VoidChest.cost > 0)
            {
                VoidChest.costType = (CostTypeIndex)CostIndexVoidCoinBlood;
            }
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidTriple").GetComponent<PurchaseInteraction>().costType = (CostTypeIndex)CostIndexVoidCoinBlood;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel").GetComponent<ChestBehavior>().enabled = true;
            GameObject voidSupp = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSuppressor/VoidSuppressor.prefab").WaitForCompletion();
            voidSupp.GetComponent<PurchaseInteraction>().costType = CostTypeIndex.VoidCoin;
            voidSupp.GetComponent<VoidSuppressorBehavior>().costMultiplierPerPurchase = 0;
            GlobalEventManager.onCharacterDeathGlobal += DropVoidCoin;
        }

        public static void VoidCoinRunEnd()
        {
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidChest").GetComponent<PurchaseInteraction>().costType = CostTypeIndex.PercentHealth;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidTriple").GetComponent<PurchaseInteraction>().costType = CostTypeIndex.PercentHealth;
            LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/Chest/VoidCoinBarrel").GetComponent<ChestBehavior>().enabled = false;
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSuppressor/VoidSuppressor.prefab").WaitForCompletion().GetComponent<PurchaseInteraction>().costType = CostTypeIndex.None;
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
                    component.chance *= component.mult;
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
                                damageType = (DamageType.NonLethal | DamageType.BypassArmor)
                            });
                        }
                    }
                }
            };
            CostIndexVoidCoinBlood = (CostTypeIndex)CostTypeCatalog.costTypeDefs.Length + obj.Count;
            obj.Add(CostTypeVoidCoinBlood);
        }

    }

    //This doesn't need to be networked does it
    public class VoidCoinChance : MonoBehaviour
    {
        public float chance = 1f;
        public float mult = 0.6f;
        public int coinsRemaining = 5;
    }

}