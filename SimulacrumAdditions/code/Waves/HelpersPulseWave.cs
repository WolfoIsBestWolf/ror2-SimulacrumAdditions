using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.Networking;
using EntityStates;

namespace SimulacrumAdditions
{

    public class SimulacrumPulseWave : MonoBehaviour
    {
        private TeamFilter teamFilter;
        private EntityStateMachine stateMachine;

        public TeamIndex affectedTeam = TeamIndex.Player;
        public GameObject pulsePrefab;
        public BuffDef buffDef;
        public float buffDuration = 1.5f;
        public float baseForce = 7000;  
        public float pulseInterval = 1;

        private void OnEnable()
        {
            if (teamFilter == null)
            {
                teamFilter = base.gameObject.AddComponent<TeamFilter>();   
            }
            teamFilter.teamIndex = affectedTeam;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.buffDef = buffDef;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.buffDuration = buffDuration;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.baseForce = baseForce;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.pulseInterval = pulseInterval;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.pulsePrefab = pulsePrefab;
           
            GameObject crab = (Run.instance as InfiniteTowerRun).safeWardController.gameObject;
            base.gameObject.transform.position = crab.transform.GetChild(2).GetChild(0).position;
            if (!stateMachine)
            {
                stateMachine = base.gameObject.AddComponent<EntityStateMachine>();
                var PulseState = new PulseWaveState();
                stateMachine.initialStateType = new EntityStates.SerializableEntityStateType();
                stateMachine.mainStateType = new EntityStates.SerializableEntityStateType();

                PulseState.teamFilter = teamFilter;
                stateMachine.SetState(PulseState);
            }
        }

        private void OnDisable()
        {
            EntityStates.Missions.Moon.MoonBatteryDesignActive.pulsePrefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MoonBatteryDesignPulse");
            EntityStates.Missions.Moon.MoonBatteryDesignActive.buffDef = RoR2Content.Buffs.Cripple;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.buffDuration = 1.5f;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.baseForce = 7000;
            EntityStates.Missions.Moon.MoonBatteryDesignActive.pulseInterval = 1;
        }
    }

    public class PulseWaveState : EntityStates.Missions.Moon.MoonBatteryDesignActive
    {
        public override void OnEnter()
        {
            this.holdoutZoneController = (Run.instance as InfiniteTowerRun).safeWardController.gameObject.GetComponent<HoldoutZoneController>();
            if (teamFilter == null)
            {
                this.teamFilter = base.GetComponent<TeamFilter>();
            }
            this.pulseTimer = 3f;
        }

        public override void Update()
        {
            this.age += Time.deltaTime;
        }

        public override void OnExit()
        {
 
        }
    }
}