using BepInEx;
using BepInEx.Configuration;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.Navigation;
//using System;
using System.Collections;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
	public class SimuSkinsMain
    {
        public static event System.Action<Run> unlockSkins;

        public static void WolfoSkins()
        {
            SkinsCommando.CommandoSkin();
            SkinsHuntress.HuntressSkin(); //Black/Pink Bunny
            SkinsBandit.BanditSkin(); //RoRR Red
            SkinsMULT.ToolbotSkin(); //Damage Chest
            SkinsEngineer.EngiSkin(); //RoRR Red
            SkinsArtificer.ArtificerSkin();
            SkinsMerc.MercSkin();
            SkinsREX.TreebotSkin(); //Lepton Daisy
            SkinsLoader.LoaderSkin();
            SkinsAcrid.AcridSkin(); //RoRR White/Blue
            SkinsCaptain.CaptainSkin(); //Pink Captain Artwork
            SkinsRailGunner.RailGunnerSkins();
            SkinsVoidFiend.VoidSkins(); //Idk maybe like Devestator or Imp or Voidling metal
            SkinsCHEF.CallDuringAwake();
            SkinsHand.CallDuringAwake();

            BodyCatalog.availability.CallWhenAvailable(ModSupport);

            On.RoR2.SkinDef.Apply += (orig, self, model) =>
            {
                orig(self, model);
                //Debug.Log("SkinApply " + self);
                if (model.GetComponent<SkinDefWolfoTracker>())
                {
                    model.GetComponent<SkinDefWolfoTracker>().UndoWolfoSkin();
                }
                if (self is SkinDefWolfo)
                {
                    (self as SkinDefWolfo).ApplyExtras(model);
                }
            };

            On.RoR2.InfiniteTowerWaveController.PlayAllEnemiesDefeatedSound += (orig, self) =>
            {
                orig(self);
                if (self.waveIndex >= 50)
                {
                    System.Action<Run> action = SimuSkinsMain.unlockSkins;
                    if (action == null)
                    {
                        return;
                    }
                    action(Run.instance);
                }        
            };
            On.EntityStates.GameOver.VoidEndingStart.OnEnter += (orig, self) =>
            {
                orig(self);
                Debug.Log("EntityStates.GameOver.VoidEndingStart.OnEnter");
                System.Action<Run> action = SimuSkinsMain.unlockSkins;
                if (action == null)
                {
                    return;
                }
                action(Run.instance);
            };
        }

        internal static void ModSupport()
        {
            GameObject ModdedBody = BodyCatalog.FindBodyPrefab("CHEF");
            if (ModdedBody != null)
            {
                SkinsCHEF.CHEFSkin(ModdedBody);
            }
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "You do not have CHEF mod installed.");
            }
            //HAND
            ModdedBody = BodyCatalog.FindBodyPrefab("HANDOverclockedBody");
            if (ModdedBody != null)
            {
                SkinsHand.HandSkin(ModdedBody);
            }
            else
            {
                LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HAND_DESCRIPTION", "You do not have Han-D mod installed.");
            }
        }


    }

    public class SkinDefWolfo : SkinDef
    {
        public new void Awake()
        {
            //Debug.LogWarning("SkinDefWolfo");
        }

        //Some sort of Undo thing
        public void ApplyExtras(GameObject modelObject)
        {
            CharacterModel model = modelObject.GetComponent<CharacterModel>();
            SkinDefWolfoTracker skinDefWolfoTracker = modelObject.AddComponent<SkinDefWolfoTracker>();

            if (!Run.instance && modelObject.transform.parent && modelObject.name.EndsWith("Engi"))
            {
                this.EngiDisplay(modelObject, skinDefWolfoTracker);
            }

            skinDefWolfoTracker.model = model;
            skinDefWolfoTracker.changedLights = new SkinDefWolfoTracker.ChangedLightColors[lightColorsChanges.Length];
            for (int i = 0; lightColorsChanges.Length > i; i++)
            {
                Transform transform = modelObject.transform.Find(lightColorsChanges[i].lightPath);
                if (transform)
                {
                    Light light = transform.GetComponent<Light>();
                    skinDefWolfoTracker.changedLights[i] = new SkinDefWolfoTracker.ChangedLightColors
                    {
                        light = light,
                        originalColor = light.color
                    };
                    light.color = lightColorsChanges[i].color;

                    for (int j = 0; model.baseLightInfos.Length > j; j++)
                    {
                        if (model.baseLightInfos[j].light == light)
                        {
                            model.baseLightInfos[j].defaultColor = lightColorsChanges[i].color;
                        }
                    }
                        
                }
            }
            skinDefWolfoTracker.addedObjects = new GameObject[addGameObjects.Length];
            for (int i = 0; addGameObjects.Length > i; i++)
            {
                Transform transform = model.childLocator.FindChild(addGameObjects[i].childName);
                if (transform)
                {
                    GameObject display = Object.Instantiate(addGameObjects[i].followerPrefab, transform);
                    display.transform.localPosition = addGameObjects[i].localPos;
                    display.transform.localEulerAngles = addGameObjects[i].localAngles;
                    display.transform.localScale = addGameObjects[i].localScale;

                    skinDefWolfoTracker.addedObjects[i] = display;

                    ItemDisplay itemDisplay = display.GetComponent<ItemDisplay>();
                    if (itemDisplay)
                    {
                        model.baseRendererInfos = model.baseRendererInfos.Add(itemDisplay.rendererInfos);
                    }  
                }
            }
            
        }

        public void EngiDisplay(GameObject modelObject, SkinDefWolfoTracker tracker)
        {
            Transform mineHolder = modelObject.transform.parent.Find("mdlEngi/EngiArmature/ROOT/base/stomach/chest/upper_arm.l/lower_arm.l/hand.l/IKBoneStart/IKBoneMid/MineHolder");
            Material newMaterial = this.minionSkinReplacements[0].minionSkin.rendererInfos[0].defaultMaterial;

            GameObject Mine1 = Instantiate(this.projectileGhostReplacements[1].projectileGhostReplacementPrefab, mineHolder.GetChild(0));
            GameObject Mine2 = Instantiate(this.projectileGhostReplacements[2].projectileGhostReplacementPrefab, mineHolder.GetChild(1));
            Mine2.transform.localEulerAngles = new Vector3(0, 0, 0);

            tracker.addedObjects = new GameObject[]
            {
                Mine1,
                Mine2
            };

            mineHolder.GetChild(0).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(0).GetChild(2).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(1).gameObject.SetActive(false);
            mineHolder.GetChild(1).GetChild(0).gameObject.SetActive(false);

            modelObject.transform.parent.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = newMaterial;
        }

        public LightColorChanges[] lightColorsChanges = System.Array.Empty<LightColorChanges>();
        public ItemDisplayRule[] addGameObjects = System.Array.Empty<ItemDisplayRule>();

        [System.Serializable]
        public struct LightColorChanges
        {
            public string lightPath;

            public Color color;
        }
    }

    public class SkinDefWolfoTracker : MonoBehaviour
    {
        public GameObject[] addedObjects;
        public ChangedLightColors[] changedLights;
        public CharacterModel model;

        [System.Serializable]
        public struct ChangedLightColors
        {
            public Light light;

            public Color originalColor;
        }

        public void UndoWolfoSkin()
        {
            if (changedLights != null)
            {
                for (int i = 0; changedLights.Length > i; i++)
                {
                    if (changedLights[i].light)
                    {
                        changedLights[i].light.color = changedLights[i].originalColor;
                        for (int j = 0; model.baseLightInfos.Length > j; j++)
                        {
                            if (model.baseLightInfos[j].light == changedLights[i].light)
                            {
                                model.baseLightInfos[j].defaultColor = changedLights[i].originalColor;
                            }
                        }
                    }
                }
            }
            if (addedObjects != null)
            {
                for (int i = 0; addedObjects.Length > i; i++)
                {
                    Destroy(addedObjects[i]);
                }
            }
            DestroyImmediate(this);
        }
    }

    public class SimuOrVoidEnding : RoR2.Achievements.BaseAchievement
    {
        public override void OnBodyRequirementMet()
        {
            base.OnBodyRequirementMet();
            SimuSkinsMain.unlockSkins += this.Unlock;
            Run.onClientGameOverGlobal += this.OnClientGameOverGlobal;
        }

        public override void OnBodyRequirementBroken()
        {
            SimuSkinsMain.unlockSkins -= this.Unlock;
            Run.onClientGameOverGlobal -= this.OnClientGameOverGlobal;
            base.OnBodyRequirementBroken();
        }

        private void OnClientGameOverGlobal(Run run, RunReport runReport)
        {
            if (!runReport.gameEnding)
            {
                return;
            }
            if (runReport.gameEnding == SimuMain.InfiniteTowerEnding)
            {
                base.Grant();
            }
        }

        private void Unlock(Run run)
        {
            base.Grant();
        }

    }
}