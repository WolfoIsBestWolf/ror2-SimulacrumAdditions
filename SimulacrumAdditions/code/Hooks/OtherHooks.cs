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

namespace SimulacrumAdditions
{
    public class Hooks_Other
    {

        internal static void AddHooks()
        {
            //Fix ORANGE tier
            On.RoR2.PickupCatalog.FindPickupIndex_ItemTier += PickupCatalog_FindPickupIndex_ItemTier;
            
            IL.RoR2.CharacterBody.RpcTeleportCharacterToSafety += FixUnstableTransmitterBeingStupid;

            //Give Simu Scavs Void Items
            On.RoR2.ScavengerItemGranter.Start += SimuGiveScavVoidItems;

            //Fake Ass Ending Overwrite
            On.RoR2.EventFunctions.BeginEnding += SimulacrumEndingBeginEnding;


        }

        public static void SimulacrumEndingBeginEnding(On.RoR2.EventFunctions.orig_BeginEnding orig, EventFunctions self, GameEndingDef gameEndingDef)
        {
            if (gameEndingDef == DLC1Content.GameEndings.VoidEnding && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                orig(self, SimuMain.InfiniteTowerEnding);
                foreach (CharacterBody characterBody in CharacterBody.readOnlyInstancesList)
                {
                    if (characterBody.hasEffectiveAuthority)
                    { }
                    EntityStateMachine entityStateMachine = EntityStateMachine.FindByCustomName(characterBody.gameObject, "Body");
                    if (entityStateMachine)
                    {
                        entityStateMachine.SetInterruptState(new EntityStates.Idle(), EntityStates.InterruptPriority.Frozen);
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


        public static void SimuGiveScavVoidItems(On.RoR2.ScavengerItemGranter.orig_Start orig, ScavengerItemGranter self)
        {
            orig(self);
            if (Run.instance is InfiniteTowerRun)
            {
                Inventory tempinv = self.GetComponent<Inventory>();

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


        private static PickupIndex PickupCatalog_FindPickupIndex_ItemTier(On.RoR2.PickupCatalog.orig_FindPickupIndex_ItemTier orig, ItemTier tier)
        {
            if (tier == SimuMain.ItemOrangeTierDef.tier)
            {
                if (RunArtifactManager.instance)
                {
                    if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.commandArtifactDef))
                    {
                        return PickupCatalog.FindPickupIndex(RoR2Content.Equipment.Fruit.equipmentIndex);
                    }
                }
            }
            return orig(tier);
        }

        private static void PickupPickerController_SetOptionsFromPickupForCommandArtifact(On.RoR2.PickupPickerController.orig_SetOptionsFromPickupForCommandArtifact orig, PickupPickerController self, PickupIndex pickupIndex)
        {
            //IF ORANGE THEN JUST USE FRUIT IDK

            orig(self, pickupIndex);
        }

        private static void FixUnstableTransmitterBeingStupid(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(MoveType.Before,
             x => x.MatchCall("RoR2.TeleportHelper", "TeleportBody")))
            {
                c.EmitDelegate<System.Func<Vector3, Vector3>>((teleportPosition) =>
                {
                    if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
                    {
                        return Run.instance.GetComponent<InfiniteTowerRun>().safeWardController.transform.position;
                    }
                    return teleportPosition;
                });
                Debug.Log("IL Found : Unstable Transmitter to Void Crab");
            }
            else
            {
                Debug.LogWarning("IL Failed : Unstable Transmitter to Void Crab");
            }
        }

    }


}

