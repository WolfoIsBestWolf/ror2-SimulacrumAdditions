﻿using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{

	public class EquipmentDroneInSimulacrum : MonoBehaviour
	{
		private void Start()
		{
			CharacterMaster master = this.GetComponent<CharacterMaster>();
			if (master && master.teamIndex == TeamIndex.Player)
            {
				master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 15);
				master.inventory.GiveItem(RoR2Content.Items.Hoof, 10);
				UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			}
		}
	}

    public class MultiCSC : CharacterSpawnCard
    {
        public override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnCard.SpawnResult result)
        {
            int random = (int)(directorSpawnRequest.rng.nextNormalizedFloat * (float)this.cscList.Length);
            cscList[random].Spawn(position, rotation, directorSpawnRequest, ref result);
        }

        public CharacterSpawnCard[] cscList;
    }

    //Card randomizer for EQ drones but not ghosts I guess???
    public class CardRandomizer : MonoBehaviour
    {
        private void OnEnable()
        {
            InfiniteTowerExplicitSpawnWaveController controller = this.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
            if (!controller)
            {
                Debug.LogWarning("CardRandomizer on invalid object");
                return;
            }
            if (!Run.instance)
            {
                Debug.LogWarning("CardRandomizer no run instance");
                return;
            }
            if (UnityEngine.Networking.NetworkServer.active)
            {
                if (controller.spawnList.Length == 1)
                {
                    controller.spawnList[0].spawnCard = cscList[(int)(Run.instance.bossRewardRng.nextNormalizedFloat * (float)cscList.Length)];
                }
                else
                {
                    List<CharacterSpawnCard> newList = new List<CharacterSpawnCard>(cscList);
                    for (int i = 0; i < controller.spawnList.Length; i++)
                    {
                        controller.spawnList[i].spawnCard = newList[(int)(Run.instance.bossRewardRng.nextNormalizedFloat * (float)newList.Count)];
                        newList.Remove(controller.spawnList[i].spawnCard);
                    }
                }
            }
        }

        public CharacterSpawnCard[] cscList;
    }
}