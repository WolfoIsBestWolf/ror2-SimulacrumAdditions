using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class VoidSafeWard_Hooks
    {
        public static float baseRadius = 65;
        public static float radiusPerPlayer = 5;

        internal static void AddHooks()
        {
            baseRadius = WConfig.cfgCrabRadius.Value;
            radiusPerPlayer = WConfig.cfgCrabRadiusPerPlayer.Value;

            //Prevents explicit wave in basic waves whatever from moving crab 
            On.RoR2.InfiniteTowerRun.MoveSafeWard += (orig, self) =>
            {
                if (!(self.waveIndex % 5 == 0))
                {
                    Debug.Log("Preventing early moving of crab");
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
                    Debug.Log("Preventing early moving of crab");
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
                    Debug.Log("Preventing early moving of crab");
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
            On.EntityStates.InfiniteTowerSafeWard.AwaitingPortalUse.OnEnter += Radius_AwaitingPortalUse_OnEnter;
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

        private static void Radius_AwaitingPortalUse_OnEnter(On.EntityStates.InfiniteTowerSafeWard.AwaitingPortalUse.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingPortalUse self)
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
            orig(self);
        }

        public static void Waiting_SetRadius(On.EntityStates.InfiniteTowerSafeWard.AwaitingActivation.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.AwaitingActivation self)
        {
            //Debug.LogWarning((Run.instance as InfiniteTowerRun).waveInstance);
            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveInstance)
            {
                //Expand radius after travelling
                float newRadius = baseRadius + Run.instance.participatingPlayerCount * radiusPerPlayer;
                self.radius = newRadius;
            }
            else
            {
                self.radius = baseRadius / 2;
            }
            orig(self);

            //Client fix??
            if (!run.safeWardController)
            {
                run.safeWardController = self.gameObject.GetComponent<InfiniteTowerSafeWardController>();
            }
        }

        public static void Travelling_RadiusAndSpeed(On.EntityStates.InfiniteTowerSafeWard.Travelling.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Travelling self)
        {
            //self.radius = baseRadius/2;
            self.zone = self.GetComponent<VerticalTubeZone>();
            self.radius = self.zone.radius;


            orig(self);

            float newRadius = baseRadius / 2;
            float speedMult = 1;
            float waves = Run.instance.GetComponent<InfiniteTowerRun>().Network_waveIndex;
            if (WConfig.cfgCrabSpeedOnLaterWaves.Value)
            {
                //Maybe travel at like two times the speed on Stage 6

                speedMult = (1f + (waves - 10f) / 50f) + 0.1f;

                //Stage 5 Onward
                if (waves > 30)
                {
                    speedMult += (waves - 25f) / 50f;
                    if (speedMult > 3) { speedMult = 3; }
                }
                newRadius = Mathf.Min(newRadius * (0.5f + 0.5f * speedMult), baseRadius);
                self.travelSpeed *= speedMult;
                self.pathMaxSpeed *= speedMult;

            }

            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(Artifact_RealStages.ArtifactUseNormalStages))
            {
                newRadius = baseRadius;
            }
            else if (self.zone && self.zone.radius > newRadius)
            {
                VoidSafeWard_Hooks.RadiusShrinker shrink = self.gameObject.AddComponent<VoidSafeWard_Hooks.RadiusShrinker>();
                shrink.originalRadius = self.zone.radius;
                shrink.remove *= 2;
                shrink.newRadius = newRadius;
            }
            Debug.Log("Wave:" + waves + " speedMult:" + speedMult + " TravellingSpeed:" + self.travelSpeed + " Radius:" + newRadius);

        }


        private static void Activate_SetCorrectRadius(On.EntityStates.InfiniteTowerSafeWard.Active.orig_OnEnter orig, EntityStates.InfiniteTowerSafeWard.Active self)
        {
            orig(self);
            if (Run.instance && Run.instance is InfiniteTowerRun && (Run.instance as InfiniteTowerRun).waveInstance)
            {
                SimulacrumExtrasHelper radiusManip = (Run.instance as InfiniteTowerRun).waveInstance.GetComponent<SimulacrumExtrasHelper>();
                if (radiusManip && radiusManip.newRadius > 0)
                {
                    float newRadius = VoidSafeWard_Hooks.baseRadius + (radiusManip.newRadius - 60) + (Run.instance.participatingPlayerCount) * VoidSafeWard_Hooks.radiusPerPlayer;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", radiusManip.newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = radiusManip.newRadius;
                }
                else
                {
                    float newRadius = baseRadius + Run.instance.participatingPlayerCount * radiusPerPlayer;
                    self.safeWardController.wardStateMachine.state.SetFieldValue("radius", newRadius);
                    self.safeWardController.holdoutZoneController.baseRadius = newRadius;
                }
            }
        }




        public class RadiusShrinker : MonoBehaviour
        {
            public float newRadius;
            public float currentRadius;
            public float originalRadius;
            public float remove = 0.13f;
            public InfiniteTowerSafeWardController crab;

            public void Start()
            {
                currentRadius = originalRadius;
                crab = Run.instance.GetComponent<InfiniteTowerRun>().safeWardController;
                if (!crab)
                {
                    Debug.Log("No gameobject");
                    Destroy(this);
                }
            }

            public void FixedUpdate()
            {
                if (crab != null)
                {
                    if (currentRadius > newRadius)
                    {
                        currentRadius -= remove;
                        crab.wardStateMachine.state.SetFieldValue("radius", currentRadius);
                        crab.holdoutZoneController.baseRadius = currentRadius;
                        ((EntityStates.InfiniteTowerSafeWard.BaseSafeWardState)crab.wardStateMachine.state).zone.Networkradius = currentRadius;
                    }
                    else
                    {
                        crab.wardStateMachine.state.SetFieldValue("radius", newRadius);
                        crab.holdoutZoneController.baseRadius = newRadius;
                        ((EntityStates.InfiniteTowerSafeWard.BaseSafeWardState)crab.wardStateMachine.state).zone.Networkradius = newRadius;
                        Destroy(this);
                    }
                }
            }
        }
    }


}

