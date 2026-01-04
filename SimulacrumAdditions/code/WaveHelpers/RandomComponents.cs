using R2API;
using RoR2;
using System.Collections.Generic;

//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
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
                master.inventory.RemoveItem(RoR2Content.Items.BoostEquipmentRecharge, master.inventory.GetItemCount(RoR2Content.Items.BoostEquipmentRecharge));
                master.inventory.GiveItem(RoR2Content.Items.BoostEquipmentRecharge, 15);
                master.inventory.GiveItem(RoR2Content.Items.BoostHp, 5);
                UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
            }
        }
    }

    public class MultiCSC : CharacterSpawnCard
    {
        public override void Spawn(Vector3 position, Quaternion rotation, DirectorSpawnRequest directorSpawnRequest, ref SpawnResult result)
        {
            int random = (int)(directorSpawnRequest.rng.nextNormalizedFloat * (float)cscList.Length);
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

    public class PillarSpawner : MonoBehaviour
    {
        public static GameObject pillar;
        public static void MakePrefab()
        {
            //What if crab is above other parts of ground how do we make the pillars like rise from the nether
            //Why doth not cast shadow

            //matITSafeWardAreaIndicator1
            //matUnseenHandVerticalWallRay
            Material matVoidPillar1 = Instantiate(Addressables.LoadAssetAsync<Material>(key: "1a8b98f837c44084e88c328aa2d8f4b3").WaitForCompletion());
            Material matVoidPillar2 = Instantiate(Addressables.LoadAssetAsync<Material>(key: "fd23a6bf77c1d8f46bc36636fdfb2060").WaitForCompletion());
            GameObject originalPillar = Addressables.LoadAssetAsync<GameObject>(key: "51ffdd60c86fbb34096f8c7e82b807d7").WaitForCompletion();
            pillar = PrefabAPI.InstantiateClone(originalPillar, "SimuPillarForGhostWave", true);
            pillar.transform.GetChild(0).localEulerAngles = Vector3.zero;
            pillar.transform.GetChild(1).localEulerAngles = Vector3.zero;
            pillar.transform.GetChild(1).GetChild(0).GetChild(1).gameObject.SetActive(false);
            pillar.transform.GetChild(1).GetChild(0).GetChild(3).GetComponent<Light>().color = new Color(0.9f, 0.4f, 0.9f);
            pillar.transform.GetChild(1).GetChild(0).GetChild(4).gameObject.SetActive(false);
            pillar.transform.GetChild(0).GetChild(0).localPosition = new Vector3(0, -20, 0);
            pillar.transform.GetChild(0).GetChild(0).localScale = new Vector3(1, 1, 4);
            pillar.transform.localScale = new Vector3(4f, 20f, 4f);
            //pillar.layer = 0;

            ObjectTransformCurve curve = pillar.GetComponentInChildren<ObjectTransformCurve>();
            curve.translationCurveY = curve.translationCurveZ;
            curve.translationCurveZ = curve.translationCurveX;


            matVoidPillar1.SetFloat("_RimPower", 0);
            matVoidPillar2.SetColor("_TintColor", new Color(5, 0, 4, 1));
            pillar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().materials = new Material[]
            {
                matVoidPillar1,
                matVoidPillar2,
            };
        }

        public List<GameObject> pillarInstances;
        public void MakePillar()
        {
            if (!pillar)
            {
                PillarSpawner.MakePrefab();
            }
            pillarInstances = new List<GameObject>();

            for (int i = 0; i < 4; i++)
            {
                GameObject pillarInstance = GameObject.Instantiate(pillar);
                pillarInstance.transform.position = (Run.instance as InfiniteTowerRun).safeWardController.transform.position;
                pillarInstance.transform.rotation = (Run.instance as InfiniteTowerRun).safeWardController.transform.rotation;
                pillarInstances.Add(pillarInstance);
            }
            pillarInstances[0].transform.localPosition += new Vector3(20, 0, 20);
            pillarInstances[1].transform.localPosition += new Vector3(-20, 0, 20);
            pillarInstances[2].transform.localPosition += new Vector3(-20, 0, -20);
            pillarInstances[3].transform.localPosition += new Vector3(20, 0, -20);
        }
        public void RemovePillars()
        {
            for (int i = 0; i < pillarInstances.Count; i++)
            {
                GameObject pillarInstance = pillarInstances[i];
                ObjectTransformCurve curve = pillarInstance.transform.GetChild(0).GetComponent<RoR2.ObjectTransformCurve>();
                curve.timeMax = 3;
                curve.translationCurveY.keys = new Keyframe[]
                {
                    new Keyframe
                    {
                        time = 0,
                        value = 0,
                        m_InTangent = 30,
                        outTangent = 30,
                        outWeight = 0.3333f,
                    }, new Keyframe
                    {
                         time = 1,
                        value = -30,
                        m_InTangent = -0.0134f,
                        outTangent = -0.0134f,
                        outWeight = 0.3333f,
                    },
                };
                curve.Reset();
                pillarInstance.gameObject.AddComponent<DestroyOnTimer>().duration = 6;
            }

        }
    }

}