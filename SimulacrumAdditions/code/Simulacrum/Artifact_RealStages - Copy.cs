using MonoMod.Cil;
using R2API;
using RoR2;
using RoR2.Hologram;
using System.Collections.Generic;
//using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class Artifact_SimuDrones
    {
        public static ArtifactDef Artifact_DronesSimu = ScriptableObject.CreateInstance<ArtifactDef>();
      
        public static void MakeArtifact()
        {
            Artifact_DronesSimu.cachedName = "AAADronesSimu";
            Artifact_DronesSimu.nameToken = "ARTIFACT_NORMALSTAGES_NAME";
            Artifact_DronesSimu.descriptionToken = "ARTIFACT_NORMALSTAGES_DESCRIPTION";
            Artifact_DronesSimu.smallIconSelectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact2_On.png");
            Artifact_DronesSimu.smallIconDeselectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact2_Off.png");
            ContentAddition.AddArtifactDef(Artifact_DronesSimu);

        
            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;
       
        }

        private static void ArenaMissionController_OnStartServer(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(Artifact_DronesSimu))
            {
                self.gameObject.SetActive(false);
                GameObject PortalArena = GameObject.Find("/PortalArena");
                if (PortalArena)
                {
                    PortalArena.transform.GetChild(0).gameObject.SetActive(false);
                }

                return;
            }
            orig(self);
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != Artifact_DronesSimu)
            {
                return;
            }
            SceneDirector.onGenerateInteractableCardSelection -= AllowDroneCategory;
            IL.RoR2.FogDamageController.MyFixedUpdate -= NoFogDamage;
        }
        private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != Artifact_DronesSimu)
            {
                return;
            }
            SceneDirector.onGenerateInteractableCardSelection += AllowDroneCategory;
            IL.RoR2.FogDamageController.MyFixedUpdate += NoFogDamage;
        }
        private static void NoFogDamage(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.Before,
             x => x.MatchLdcR4(0.5f),
             x => x.MatchMul()
            ))
            {
                c.Next.Operand = 0.00f;
            }
            else
            {
                Debug.LogWarning("IL Failed : IL.RoR2.FogDamageController.FixedUpdate");
            }

        }
        private static void AllowDroneCategory(SceneDirector arg1, DirectorCardCategorySelection dccs)
        {
            int drones = dccs.FindCategoryIndexByName("Drone");
            if ((drones == -1))
            {
                dccs.categories[drones].selectionWeight = 10;
            }
        }
    }


}
