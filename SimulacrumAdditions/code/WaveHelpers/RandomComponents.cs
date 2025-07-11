﻿using RoR2;
using System.Collections.Generic;
//using System;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{

    public class EquipmentDroneInSimulacrum : MonoBehaviour
    {
        private void Start()
        {
            CharacterMaster master = this.GetComponent<CharacterMaster>();
            this.gameObject.AddComponent<DevotedLemurianController>();

            if (master && master.teamIndex == TeamIndex.Player)
            {
                master.inventory.RemoveItem(RoR2Content.Items.BoostEquipmentRecharge, master.inventory.GetItemCount(RoR2Content.Items.BoostEquipmentRecharge));
                master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 15);
                master.inventory.GiveItem(RoR2Content.Items.BoostHp, 5);
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
        public bool adjustSpawnCount = false;
        public CharacterSpawnCard[] cscList;


        public void DoTheThing(InfiniteTowerExplicitSpawnWaveController controller)
        {
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
            if (NetworkServer.active)
            {
                if (controller.spawnList.Length == 1)
                {
                    controller.spawnList[0].spawnCard = cscList[(int)(Run.instance.bossRewardRng.nextNormalizedFloat * (float)cscList.Length)];
                    if (adjustSpawnCount)
                    {
                        controller.spawnList[0].count = controller.spawnList[0].spawnCard.directorCreditCost;
                    }
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

            if (controller.name.EndsWith("EquipmentDrone(Clone)"))
            {
                AnnounceEquipmentDrone(controller);
            }

        }

        public static void AnnounceEquipmentDrone(InfiniteTowerExplicitSpawnWaveController self)
        {
            //This would not be translated locally, probably fix that?
            string main = Language.GetString("ITWAVE_EQUIPMENTDRONE_ANNOUNCE");

            string equipment = "";
            if (self.spawnList.Length == 2)
            {
                if (self.spawnList[0].spawnCard.equipmentToGrant[0].isLunar)
                {
                    equipment = equipment + "<color=#78AFFF>";
                }
                else
                {
                    equipment = equipment + "<color=#FF8000>";
                }
                equipment = equipment + Language.GetString(self.spawnList[0].spawnCard.equipmentToGrant[0].nameToken) + "</color> & ";
                if (self.spawnList[1].spawnCard.equipmentToGrant[0].isLunar)
                {
                    equipment = equipment + "<color=#78AFFF>";
                }
                else
                {
                    equipment = equipment + "<color=#FF8000>";
                }
                equipment = equipment + Language.GetString(self.spawnList[1].spawnCard.equipmentToGrant[0].nameToken) + "</color>";
            }
            else
            {
                if (self.spawnList[0].spawnCard.equipmentToGrant[0].isLunar)
                {
                    equipment = equipment + "<color=#78AFFF>";
                }
                else
                {
                    equipment = equipment + "<color=#FF8000>";
                }
                equipment = equipment + Language.GetString(self.spawnList[0].spawnCard.equipmentToGrant[0].nameToken) + "</color>";
            }

            string token = string.Format(main, equipment);
            Chat.SendBroadcastChat(new Chat.SimpleChatMessage
            {
                baseToken = token,
            });
        }

    }

    public class EliteInclusiveDropTable : BasicPickupDropTable
    {
        public float eliteEquipWeight;
        public float pearlWeight;

        public override void Regenerate(Run run)
        {
            base.GenerateWeightedSelection(run);
            if (eliteEquipWeight > 0)
            {
                List<PickupIndex> EliteList = new List<PickupIndex>();
                for (int i = 0; i < EliteCatalog.eliteDefs.Length; i++)
                {
                    EliteDef tempDef = EliteCatalog.eliteDefs[i];

                    if (tempDef == null || !tempDef.eliteEquipmentDef || tempDef.eliteEquipmentDef && tempDef.eliteEquipmentDef.equipmentIndex != EquipmentIndex.None)
                    {
                        Debug.LogWarning("Null EliteDef");
                        break;
                    }

                    if (!(tempDef.name.StartsWith("edGold") || tempDef.name.StartsWith("edSecretSpeed")))
                    {
                        if (tempDef.IsAvailable() && tempDef.eliteEquipmentDef.dropOnDeathChance > 0)
                        {
                            PickupIndex temp = PickupCatalog.FindPickupIndex(tempDef.eliteEquipmentDef.equipmentIndex);
                            if (!EliteList.Contains(temp))
                            {
                                EliteList.Add(temp);
                            }
                        }
                    }
                }
                if (EliteList.Count > 0)
                {
                    this.Add(EliteList, eliteEquipWeight);
                }
                else
                {
                    Debug.Log("No dropable Elite Equipment");
                }
            }
            if (pearlWeight > 0)
            {
                this.selector.AddChoice(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex), pearlWeight * 0.8f);
                this.selector.AddChoice(PickupCatalog.FindPickupIndex(RoR2Content.Items.ShinyPearl.itemIndex), pearlWeight * 0.2f);
            }
        }
    }

}