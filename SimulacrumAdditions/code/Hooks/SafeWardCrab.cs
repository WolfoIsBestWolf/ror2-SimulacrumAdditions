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
    public class VoidSafeWard_Hooks
    {

        internal static void AddHooks()
        {
            //Prevents explicit wave in basic waves whatever from moving crab 
            On.RoR2.InfiniteTowerRun.MoveSafeWard += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };
            On.RoR2.InfiniteTowerBossWaveController.OnTimerExpire += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    if (!NetworkServer.active)
                    {
                        Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnTimerExpire()' called on client");
                        return;
                    }
                    self.MarkAsFinished();
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };
            On.RoR2.InfiniteTowerExplicitSpawnWaveController.OnTimerExpire += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    if (!NetworkServer.active)
                    {
                        Debug.LogWarning("[Server] function 'System.Void RoR2.InfiniteTowerWaveController::OnTimerExpire()' called on client");
                        return;
                    }
                    self.MarkAsFinished();
                    Debug.Log("Preventing early moving of crab"); ;
                }
                else
                {
                    orig(self);
                }
            };


            //Gets spawned on every wave start ig
            On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    GoldTitanManager.EndChannelingTitansServer(self.gameObject);
                }
            };
            On.EntityStates.InfiniteTowerSafeWard.AwaitingPortalUse.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active)
                {
                    GoldTitanManager.EndChannelingTitansServer(self.gameObject);
                }
            };


            On.EntityStates.InfiniteTowerSafeWard.Active.OnEnter += Activate_SetCorrectRadius;
            //After Traveling Radius increases before the Wave Starts, helps when gets into bad spots
            On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.OnEnter += Waiting_SetRadius;
            //Bigger Travel Radius, just feels better
            On.EntityStates.InfiniteTowerSafeWard.Travelling.OnEnter += Travelling_RadiusAndSpeed;
            On.EntityStates.InfiniteTowerSafeWard.Burrow.OnEnter += (orig, self) =>
            {
                float before = self.GetComponent<VerticalTubeZone>().radius;
                self.radius = before;
                orig(self);
            };
            //He'd leave too soon on special boss waves
            On.EntityStates.InfiniteTowerSafeWard.Unburrow.OnEnter += (orig, self) =>
            {
                orig(self);
                //Debug.LogWarning(self.duration);
                GameObject temp = Run.instance.GetComponent<InfiniteTowerRun>().waveInstance;
                if (self.duration > 0 && temp)
                {
                    self.duration = temp.GetComponent<InfiniteTowerWaveController>().secondsAfterWave;
                }
            };
        }



        public static void Waiting_SetRadius(On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingActivation self)
        {
            //Debug.LogWarning((Run.instance as InfiniteTowerRun).waveInstance);
            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveInstance)
            {
                //Expand radius after travelling
                self.radius = 60 + Run.instance.participatingPlayerCount * 5;
            }
            else
            {
                self.radius = 30;
            }
            orig(self);

            if (!run.safeWardController)
            {
                run.safeWardController = self.gameObject.GetComponent<InfiniteTowerSafeWardController>();
            }
        }

        public static void Travelling_RadiusAndSpeed(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Travelling self)
        {
            self.radius = 30;
            orig(self);
            if (WConfig.cfgSpeedUpOnLaterWaves.Value)
            {
                //Maybe travel at like two times the speed on Stage 6
                float waves = Run.instance.GetComponent<InfiniteTowerRun>().Network_waveIndex;
                float speedMult = (1f + (waves - 10f) / 50f) + 0.1f;

                //Stage 5 Onward
                if (waves > 30)
                {
                    speedMult += (waves - 25f) / 50f;
                    if (speedMult > 3) { speedMult = 3; }
                }
                self.zone.radius = Mathf.Min(self.radius * (0.5f + 0.5f * speedMult), 45);
                self.travelSpeed *= speedMult;
                self.pathMaxSpeed *= speedMult;

                Debug.Log("Wave:" + waves + " speedMult:" + speedMult + " TravellingSpeed:" + self.travelSpeed + " Radius:" + self.zone.radius);
            }
        }


        private static void Activate_SetCorrectRadius(On.EntityStates.InfiniteTowerSafeWard.Active.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Active self)
        {
            orig(self);
            if (Run.instance && Run.instance is InfiniteTowerRun && (Run.instance as InfiniteTowerRun).waveInstance)
            {
                SimulacrumExtrasHelper temp = (Run.instance as InfiniteTowerRun).waveInstance.GetComponent<SimulacrumExtrasHelper>();
                if (temp && temp.newRadius > 0)
                {
                    temp.newRadius += -5 + Run.instance.participatingPlayerCount * 5;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", temp.newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = temp.newRadius;
                }
                else
                {
                    float newRadius = 60 + Run.instance.participatingPlayerCount * 5;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = newRadius;
                }
            }
        }


    }


}

