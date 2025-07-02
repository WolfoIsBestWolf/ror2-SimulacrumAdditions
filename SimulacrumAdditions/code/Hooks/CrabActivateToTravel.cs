using EntityStates.InfiniteTowerSafeWard;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.Events;

namespace SimulacrumAdditions
{
    public class CrabActivateToTravel
    {
        public static void Make()
        {
            On.EntityStates.InfiniteTowerSafeWard.Travelling.OnEnter += Travelling_OnEnter;
            if (WConfig.cfgAwaitTravel.Value)
            {
                On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += Unburrow_OnEnter;
                On.EntityStates.InfiniteTowerSafeWard.Unburrow.FixedUpdate += Unburrow_FixedUpdate;
            }
        }

        private static void Travelling_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, Travelling self)
        {
            self.animationStateName = "Swim";
            self.animationLayerName = "Base";
            orig(self);
        }

        private static void Unburrow_OnEnter(On.EntityStates.InfiniteTowerSafeWard.Unburrow.orig_OnEnter orig, Unburrow self)
        {
            self.zone = self.GetComponent<VerticalTubeZone>();
            float newRadius = VoidSafeWard_Hooks.baseRadius + Run.instance.participatingPlayerCount * VoidSafeWard_Hooks.radiusPerPlayer;
            if (self.zone && self.zone.radius > newRadius)
            {
                VoidSafeWard_Hooks.RadiusShrinker shrink = self.gameObject.AddComponent<VoidSafeWard_Hooks.RadiusShrinker>();
                shrink.originalRadius = self.zone.radius;
                shrink.newRadius = newRadius;
                self.radius = self.zone.radius;
            }
            else
            {
                self.radius = newRadius;
            }


            self.objectiveToken = "INFINITETOWER_OBJECTIVE_AWAITING_TRAVEL";
            orig(self);
            self.duration = 0;
            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
            {
                baseToken = "INFINITETOWER_TRAVEL_NOTICE"
            });

            Animator modelAnimator = self.GetModelAnimator();
            modelAnimator.Play("BurrowedToIdle", 0, -0.2f);

            CrabTravelOnCommand travelComamnd = self.gameObject.AddComponent<CrabTravelOnCommand>();
            PurchaseInteraction purchaseInteraction = self.gameObject.GetComponent<PurchaseInteraction>();
            purchaseInteraction.SetAvailable(true);
            purchaseInteraction.contextToken = "INFINITETOWER_TRAVEL_CONTEXT";

            PersistentCall newCall = new UnityEngine.Events.PersistentCall
            {
                m_Target = travelComamnd,
                m_MethodName = "Activate",
                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                m_Arguments = new UnityEngine.Events.ArgumentCache
                {
                }
            };
            purchaseInteraction.onPurchase.m_PersistentCalls.m_Calls.Add(newCall);
            purchaseInteraction.onPurchase.m_CallsDirty = true;
        }

        private static void Unburrow_FixedUpdate(On.EntityStates.InfiniteTowerSafeWard.Unburrow.orig_FixedUpdate orig, Unburrow self)
        {
            self.fixedAge += Time.fixedDeltaTime;
            if (self.fixedAge > 5 && self.fixedAge < 6)
            {
                Animator modelAnimator = self.GetModelAnimator();
                modelAnimator.Play("BurrowedToIdle", 0, 12);
            }
        }

        public class CrabTravelOnCommand : MonoBehaviour
        {
            public void Activate()
            {
                var Rng = (Run.instance as InfiniteTowerRun).safeWardRng;
                EntityStateMachine stateMachine = this.GetComponent<EntityStateMachine>();
                stateMachine.SetNextState(new Travelling(Rng));

                PurchaseInteraction purchaseInteraction = this.gameObject.GetComponent<PurchaseInteraction>();
                purchaseInteraction.SetAvailable(false);
                purchaseInteraction.contextToken = "INFINITE_TOWER_SAFE_WARD_CONTEXT";
                Destroy(this);
            }
        }

    }


}