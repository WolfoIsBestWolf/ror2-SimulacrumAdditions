using BepInEx;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System.Collections.Generic;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using RoR2.UI;
using RoR2.Stats;
using RoR2.UI.LogBook;

namespace SimulacrumAdditions
{
    public class Hooks_Other
    {

        internal static void AddHooks()
        {
            //Fix ORANGE tier
            On.RoR2.PickupCatalog.FindPickupIndex_ItemTier += PickupCatalog_FindPickupIndex_ItemTier;
            
            //Give Simu Scavs Void Items
            On.RoR2.ScavengerItemGranter.Start += SimuGiveScavVoidItems;

            //Fake Ass Ending Overwrite
            On.RoR2.EventFunctions.BeginEnding += SimulacrumEndingBeginEnding;

            //Wave on End screen
            if (WConfig.cfgWaveOnEndScreen.Value)
            {
                On.RoR2.UI.GameEndReportPanelController.SetDisplayData += GameEndReportPanelController_SetDisplayData;
            }

            //Reset button
            On.RoR2.UI.InfiniteTowerMenuController.OnEventSystemDiscovered += InfiniteTowerMenuController_OnEventSystemDiscovered;

        }

        private static void InfiniteTowerMenuController_OnEventSystemDiscovered(On.RoR2.UI.InfiniteTowerMenuController.orig_OnEventSystemDiscovered orig, InfiniteTowerMenuController self, MPEventSystem eventSystem)
        {
            orig(self, eventSystem);
            if (WConfig.ResetStatsButton.Value)
            {
                Transform JuicePannel = self.transform.GetChild(0).GetChild(1).GetChild(0);
                Transform ResetButton = JuicePannel.Find("Reset");
                Transform ResetAllButton = JuicePannel.Find("ResetAll");
                Transform Set50ButtonD = JuicePannel.Find("Set50D");
                Transform Set50ButtonR = JuicePannel.Find("Set50R");
                Transform Set50ButtonM = JuicePannel.Find("Set50M");
                if (JuicePannel.childCount == 2)
                {
                    GameObject originalButton = JuicePannel.GetChild(1).gameObject;


                    GameObject newButton = GameObject.Instantiate(originalButton, JuicePannel);
                    newButton.name = "ResetAll";
                    newButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                    newButton.transform.GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Reset all Survivors";
                    ResetAllButton = newButton.transform;

                    newButton = GameObject.Instantiate(originalButton, JuicePannel);
                    newButton.name = "Reset";
                    newButton.GetComponent<UnityEngine.UI.Image>().color = Color.red;
                    newButton.transform.GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Reset selected Survivor";
                    ResetButton = newButton.transform;

                    newButton = GameObject.Instantiate(originalButton, JuicePannel);
                    newButton.name = "Set50D";
                    newButton.GetComponent<UnityEngine.UI.Image>().color = Color.magenta;
                    newButton.transform.GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Set to 50 for selected Driz";
                    Set50ButtonD = newButton.transform;

                    newButton = GameObject.Instantiate(originalButton, JuicePannel);
                    newButton.name = "Set50R";
                    newButton.GetComponent<UnityEngine.UI.Image>().color = Color.magenta;
                    newButton.transform.GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Set to 50 for selected Rain";
                    Set50ButtonR = newButton.transform;

                    newButton = GameObject.Instantiate(originalButton, JuicePannel);
                    newButton.name = "Set50M";
                    newButton.GetComponent<UnityEngine.UI.Image>().color = Color.magenta;
                    newButton.transform.GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Set to 50 for selected Mons";
                    Set50ButtonM = newButton.transform;


                    JuicePannel.GetChild(1).SetAsLastSibling();
                }
                if (ResetButton)
                {
                    ResetButton.GetChild(2).gameObject.SetActive(false);
                    HGButton button = ResetButton.GetComponent<HGButton>();

                    ResetWavesCounter travelComamnd = self.gameObject.AddComponent<ResetWavesCounter>();
                    button.onClick.m_PersistentCalls = new UnityEngine.Events.PersistentCallGroup
                    {
                        m_Calls = new List<UnityEngine.Events.PersistentCall>()
                        {
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = travelComamnd,
                                m_MethodName = "Reset",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            },
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = self,
                                m_MethodName = "UpdateDisplayedSurvivor",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            }
                        }
                    };
                }
                if (ResetAllButton)
                {
                    ResetAllButton.GetChild(2).gameObject.SetActive(false);
                    HGButton button = ResetAllButton.GetComponent<HGButton>();

                    ResetWavesCounter travelComamnd = self.gameObject.AddComponent<ResetWavesCounter>();
                    button.onClick.m_PersistentCalls = new UnityEngine.Events.PersistentCallGroup
                    {
                        m_Calls = new List<UnityEngine.Events.PersistentCall>()
                        {
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = travelComamnd,
                                m_MethodName = "ResetAll",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            },
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = self,
                                m_MethodName = "UpdateDisplayedSurvivor",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            }
                        }
                    };
                }
                if (Set50ButtonD)
                {
                    Set50ButtonD.GetChild(2).gameObject.SetActive(false);
                    HGButton button = Set50ButtonD.GetComponent<HGButton>();

                    ResetWavesCounter travelComamnd = self.gameObject.AddComponent<ResetWavesCounter>();
                    button.onClick.m_PersistentCalls = new UnityEngine.Events.PersistentCallGroup
                    {
                        m_Calls = new List<UnityEngine.Events.PersistentCall>()
                        {
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = travelComamnd,
                                m_MethodName = "Set50D",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            },
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = self,
                                m_MethodName = "UpdateDisplayedSurvivor",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            }
                        }
                    };


                }
                if (Set50ButtonR)
                {
                    Set50ButtonR.GetChild(2).gameObject.SetActive(false);
                    HGButton button = Set50ButtonR.GetComponent<HGButton>();

                    ResetWavesCounter travelComamnd = self.gameObject.AddComponent<ResetWavesCounter>();
                    button.onClick.m_PersistentCalls = new UnityEngine.Events.PersistentCallGroup
                    {
                        m_Calls = new List<UnityEngine.Events.PersistentCall>()
                        {
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = travelComamnd,
                                m_MethodName = "Set50R",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            },
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = self,
                                m_MethodName = "UpdateDisplayedSurvivor",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            }
                        }
                    };


                }
                if (Set50ButtonM)
                {
                    Set50ButtonM.GetChild(2).gameObject.SetActive(false);
                    HGButton button = Set50ButtonM.GetComponent<HGButton>();

                    ResetWavesCounter travelComamnd = self.gameObject.AddComponent<ResetWavesCounter>();
                    button.onClick.m_PersistentCalls = new UnityEngine.Events.PersistentCallGroup
                    {
                        m_Calls = new List<UnityEngine.Events.PersistentCall>()
                        {
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = travelComamnd,
                                m_MethodName = "Set50M",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            },
                            new UnityEngine.Events.PersistentCall
                            {
                                m_Target = self,
                                m_MethodName = "UpdateDisplayedSurvivor",
                                m_Mode = UnityEngine.Events.PersistentListenerMode.Void,
                                m_Arguments = new UnityEngine.Events.ArgumentCache
                                {
                                }
                            }
                        }
                    };


                }



            }
        }


        public class ResetWavesCounter : MonoBehaviour
        {
            public void Reset()
            {
                Debug.LogWarning("Attempting to reset Simulacrum Wave High-scores");

                MPEventSystem eventSystem = MPEventSystem.instancesList[0];
                UserProfile userProfile;
                if (eventSystem == null)
                {
                    userProfile = null;
                }
                else
                {
                    LocalUser localUser = eventSystem.localUser;
                    userProfile = ((localUser != null) ? localUser.userProfile : null);
                }
                UserProfile userProfile2 = userProfile;
                if (userProfile2 != null)
                {
                    StatSheet statSheet = userProfile2.statSheet;
                    SurvivorDef survivorPreference = userProfile2.GetSurvivorPreference();
                    if (survivorPreference)
                    {
                        string bodyName = BodyCatalog.GetBodyName(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorPreference.survivorIndex));
                        Debug.LogWarning("Resetting for " + bodyName);
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy.FindStatDef(bodyName ?? ""), "0");
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal.FindStatDef(bodyName ?? ""), "0");
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedHard.FindStatDef(bodyName ?? ""), "0");

                    } 
                }
            }

            public void ResetAll()
            {
                Debug.LogWarning("Attempting to reset ALL Simulacrum Wave High-scores");

                MPEventSystem eventSystem = MPEventSystem.instancesList[0];
                UserProfile userProfile;
                if (eventSystem == null)
                {
                    userProfile = null;
                }
                else
                {
                    LocalUser localUser = eventSystem.localUser;
                    userProfile = ((localUser != null) ? localUser.userProfile : null);
                }
                UserProfile userProfile2 = userProfile;
                if (userProfile2 != null)
                {
                    StatSheet statSheet = userProfile2.statSheet;
                    foreach (SurvivorDef survivorDef in SurvivorCatalog.survivorDefs)
                    {
                        if (survivorDef.bodyPrefab)
                        {
                            CharacterBody body = survivorDef.bodyPrefab.GetComponent<CharacterBody>();
                            if (body)
                            {
                                string bodyName = BodyCatalog.GetBodyName(body.bodyIndex);
                                Debug.LogWarning("Resetting for " + bodyName);
                                statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy.FindStatDef(bodyName ?? ""), "0");
                                statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal.FindStatDef(bodyName ?? ""), "0");
                                statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedHard.FindStatDef(bodyName ?? ""), "0");

                            }
                        }

                    }
                }
            }

            public void Set50D()
            {
                Set50(0);
            }
            public void Set50R()
            {
                Set50(1);
            }
            public void Set50M()
            {
                Set50(2);
            }

            public void Set50(int difficulty)
            {
                Debug.LogWarning("Attempting to set 50 Simulacrum Wave High-scores");




                MPEventSystem eventSystem = MPEventSystem.instancesList[0];
                UserProfile userProfile;
                if (eventSystem == null)
                {
                    userProfile = null;
                }
                else
                {
                    LocalUser localUser = eventSystem.localUser;
                    userProfile = ((localUser != null) ? localUser.userProfile : null);
                }
                UserProfile userProfile2 = userProfile;
                if (userProfile2 != null)
                {
                    StatSheet statSheet = userProfile2.statSheet;
                    SurvivorDef survivorPreference = userProfile2.GetSurvivorPreference();
                    if (survivorPreference)
                    {
                        string bodyName = BodyCatalog.GetBodyName(SurvivorCatalog.GetBodyIndexFromSurvivorIndex(survivorPreference.survivorIndex));
                        Debug.LogWarning("Setting 50 for " + bodyName);
                        /*
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy.FindStatDef(bodyName ?? ""), "0");
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal.FindStatDef(bodyName ?? ""), "0");
                        statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedHard.FindStatDef(bodyName ?? ""), "0");
                        */


                        if (difficulty == 0)
                        {
                            statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedEasy.FindStatDef(bodyName ?? ""), "50");
                        }
                        else if (difficulty == 1)
                        {
                            statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedNormal.FindStatDef(bodyName ?? ""), "50");
                        }
                        else if (difficulty == 2)
                        {
                            statSheet.SetStatValueFromString(PerBodyStatDef.highestInfiniteTowerWaveReachedHard.FindStatDef(bodyName ?? ""), "50");
                        }
                    }
                }
            }


        }


        private static void GameEndReportPanelController_SetDisplayData(On.RoR2.UI.GameEndReportPanelController.orig_SetDisplayData orig, GameEndReportPanelController self, GameEndReportPanelController.DisplayData newDisplayData)
        {
            orig(self, newDisplayData);



            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                LastWaveHolder lastWave = Run.instance.GetComponent<LastWaveHolder>();
                if (!lastWave)
                {
                    return;
                }
                GameObject UI = lastWave.LatestWave;
                if (UI)
                {
                    if (Run.instance.GetComponent<InfiniteTowerRun>().waveInstance)
                    {
                        UI = Run.instance.GetComponent<InfiniteTowerRun>().waveInstance.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab;
                    }
                  
                }
                if (UI)
                {
                    if (self.selectedDifficultyLabel)
                    {
                        if (!lastWave.UIThing)
                        {

                            GameObject newWaveSlot = Object.Instantiate(self.selectedDifficultyLabel.transform.parent.gameObject, self.selectedDifficultyLabel.transform.parent.parent);
                            newWaveSlot.transform.SetSiblingIndex(1);
                            lastWave.UIThing = newWaveSlot;


                            newWaveSlot.transform.GetChild(0).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Last Wave:";

                            string waveToken = UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token;
                            waveToken = Language.GetString(waveToken);
                            waveToken = string.Format(waveToken, (Run.instance as InfiniteTowerRun).waveIndex);
                            //waveToken = "<color=#FFFFFF>" + waveToken + "</color>";
                            newWaveSlot.transform.GetChild(2).GetComponent<RoR2.UI.LanguageTextMeshController>().token = waveToken;

                            UnityEngine.UI.Image newSprite = newWaveSlot.transform.GetChild(3).GetComponent<UnityEngine.UI.Image>();
                            UnityEngine.UI.Image waveSprite = UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>();

                            newSprite.sprite = waveSprite.sprite;
                            newSprite.color = waveSprite.color;
                           
                            //Color underlin e colro
                            newWaveSlot.transform.GetChild(2).GetComponent<RoR2.UI.HGTextMeshProUGUI>().color = UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color;
                            //newWaveSlot.transform.GetChild(2).GetComponent<RoR2.UI.HGTextMeshProUGUI>().m_underlineColor = UI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color;
                            //newWaveSlot.transform.GetChild(2).GetComponent<RoR2.UI.HGTextMeshProUGUI>().fontStyle |= TMPro.FontStyles.Underline;
                            newWaveSlot.transform.GetChild(2).SetAsLastSibling();

                            try
                            {
                                if (UI.transform.GetChild(0).childCount == 4)
                                {
                                    GameObject Sprite2 = GameObject.Instantiate(newSprite.gameObject, newWaveSlot.transform);
                                    waveSprite = UI.transform.GetChild(0).GetChild(3).GetChild(0).GetComponent<UnityEngine.UI.Image>();
                                    newSprite = Sprite2.GetComponent<Image>();
                                    newSprite.sprite = waveSprite.sprite;
                                    newSprite.color = waveSprite.color;
                                }
                            }
                            catch (System.Exception e)
                            {

                            }


                            TooltipProvider toolTip = newWaveSlot.AddComponent<TooltipProvider>();
                            toolTip.titleToken = waveToken;
                            toolTip.titleColor = UI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color;
                            toolTip.bodyToken = UI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token;
                        }
                    }
                }
            }

        }


        public class LastWaveHolder : MonoBehaviour
        {
            public GameObject UIThing;
            public GameObject LatestWave;
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

        
    }


}

