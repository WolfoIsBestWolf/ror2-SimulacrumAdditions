using MonoMod.Cil;
using R2API;
using RoR2;
//using System;
using UnityEngine;
using WolfoLibrary;

namespace SimulacrumAdditions
{
    public class Artifact_SimuDrones
    {
        public static ArtifactDef ArtifactDef = ScriptableObject.CreateInstance<ArtifactDef>();

        public static void MakeArtifact()
        {
            ArtifactDef.cachedName = "AAADronesSimu";
            ArtifactDef.nameToken = "ARTIFACT_SIMUDRONES_NAME";
            ArtifactDef.descriptionToken = "ARTIFACT_SIMUDRONES_DESCRIPTION";
            ArtifactDef.smallIconSelectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact_Drones_On.png");
            ArtifactDef.smallIconDeselectedSprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Artifacts/Artifact_Drones_Off.png");
            ContentAddition.AddArtifactDef(ArtifactDef);


            RunArtifactManager.onArtifactEnabledGlobal += OnArtifactEnabled;
            RunArtifactManager.onArtifactDisabledGlobal += OnArtifactDisabled;

        }

        private static void ArenaMissionController_OnStartServer(On.RoR2.ArenaMissionController.orig_OnStartServer orig, ArenaMissionController self)
        {
            if (RunArtifactManager.instance && RunArtifactManager.instance.IsArtifactEnabled(ArtifactDef))
            {
                self.gameObject.SetActive(false);
                GameObject PortalArena = GameObject.Find("/PortalArena");
                if (PortalArena)
                {
                    PortalArena.gameObject.SetActive(false);
                }

                return;
            }
            orig(self);
        }

        private static void OnArtifactDisabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactDef)
            {
                return;
            }
            SceneDirector.onGenerateInteractableCardSelection -= AllowDroneCategory;
            IL.RoR2.FogDamageController.MyFixedUpdate -= NoFogDamage;
        }
        private static void OnArtifactEnabled(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef != ArtifactDef)
            {
                return;
            }
            SceneDirector.onGenerateInteractableCardSelection += AllowDroneCategory;
            IL.RoR2.FogDamageController.MyFixedUpdate += NoFogDamage;


            Run.instance.ruleBook.ApplyChoice(RuleCatalog.FindChoiceDef("Items.DronesDropDynamite.On"));
            Run.instance.ruleBook.ApplyChoice(RuleCatalog.FindChoiceDef("Items.DroneWeapons.On"));

            //On.RoR2.PickupPickerController.GenerateOptionsFromDropTable += PickupPickerController_GenerateOptionsFromDropTable;
        }

        private static PickupPickerController.Option[] PickupPickerController_GenerateOptionsFromDropTable(On.RoR2.PickupPickerController.orig_GenerateOptionsFromDropTable orig, int numOptions, PickupDropTable dropTable, Xoroshiro128Plus rng)
        {
            return orig(numOptions, dropTable, rng);
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
            SimulacrumDCCS_Drones.AddDroneRelatedToPrinter(dccs);
            int dupli = dccs.FindCategoryIndexByName("Duplicators");
            int droneRelated = dccs.FindCategoryIndexByName("SimuDroneRelated");
            int simuDrones = dccs.FindCategoryIndexByName("SimuDrones");
            int drones = dccs.FindCategoryIndexByName("SimuDrones");
            if (dupli != -1 && droneRelated != -1)
            {
                dccs.categories[droneRelated].selectionWeight = dccs.categories[dupli].selectionWeight;
            }
            if (drones != -1)
            {
                dccs.categories[drones].selectionWeight *= 1.6f;
            }


            if (simuDrones != -1)
            {
                if (SceneInfo.instance.sceneDef == SceneList.itMoon)
                {
                    dccs.categories[simuDrones].selectionWeight = 15;
                }
                else
                {
                    dccs.categories[simuDrones].selectionWeight = 30;
                }
            }
        }
    }


}
