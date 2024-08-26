﻿using BepInEx;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class ArtifactTweaks
    {
        public static SpawnCard iscLemEggIT;


        public static void Main()
        {
            //Make Devotion actually work
            //Probably just add the spawn card into some category like chests but also they probably need some sort of immunity to void fog or smth
            //Probably upgrade every 5 waves

            IL.RoR2.FogDamageController.FixedUpdate += (ILContext il) =>
            {
                ILCursor c = new ILCursor(il);
                if (c.TryGotoNext(MoveType.Before,
                 x => x.MatchLdcR4(0.5f),
                 x => x.MatchMul()
                ))
                {
                    c.Next.Operand = 0f;
                    Debug.Log("IL Found : IL.RoR2.FogDamageController.FixedUpdate");
                }
                else
                {
                    Debug.LogWarning("IL Failed : IL.RoR2.FogDamageController.FixedUpdate");
                }
            };
            On.RoR2.InfiniteTowerWaveController.OnAllEnemiesDefeatedServer += ActivateNewArtifacts_OnAllEnemiesDefeatedServer;


            SpawnCard iscLemEgg = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/CU8/LemurianEgg/iscLemurianEgg.asset").WaitForCompletion();
            iscLemEggIT = Object.Instantiate(iscLemEgg);
            iscLemEggIT.name = "iscLemurianEggIT";
            iscLemEggIT.directorCreditCost = 1;
            //Make Delusion work
            //Idk I guess every 5 waves

            SceneDirector.onGenerateInteractableCardSelection += SimulacrumDevotionAddEgg;

            if (WConfig.cfgSacrificeBalance.Value)
            {
                On.RoR2.InfiniteTowerWaveController.DropRewards += (orig, self) =>
                {
                    bool sacrifice = RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.sacrificeArtifactDef);
                    SimulacrumExtrasHelper temp = self.GetComponent<SimulacrumExtrasHelper>();
                    if (sacrifice && self.rewardOptionCount > 1)
                    {
                        self.rewardOptionCount--;
                        if (temp && temp.rewardOptionCount > 1)
                        {
                            temp.rewardOptionCount--;
                        }
                    }
                    orig(self);
                };
                On.RoR2.Artifacts.SacrificeArtifactManager.OnArtifactEnabled += SacrificeArtifactManager_OnArtifactEnabled;
                On.RoR2.Artifacts.SacrificeArtifactManager.OnArtifactDisabled += SacrificeArtifactManager_OnArtifactDisabled;
            }
        }

        private static void ActivateNewArtifacts_OnAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerWaveController.orig_OnAllEnemiesDefeatedServer orig, InfiniteTowerWaveController self)
        {
            orig(self);
            if(NetworkServer.active)
            {
                if(!RunArtifactManager.instance)
                {
                    return;
                }
                if (self.isBossWave)
                {
                    if (RunArtifactManager.instance.IsArtifactEnabled(CU8Content.Artifacts.Devotion))
                    {
                        DevotionInventoryController.ActivateDevotedEvolution();
                    }
                    if (RunArtifactManager.instance.IsArtifactEnabled(CU8Content.Artifacts.Delusion))
                    {
                        /*foreach (ChestBehavior chestBehavior in InstanceTracker.GetInstancesList<ChestBehavior>())
                        {
                            chestBehavior.CallRpcResetChests();
                        }*/
                    }
                }
            }
        }

        private static void SimulacrumDevotionAddEgg(SceneDirector scene, DirectorCardCategorySelection dccs)
        {
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>() && RunArtifactManager.instance.IsArtifactEnabled(CU8Content.Artifacts.Devotion))
            {
                DirectorCard simuLemEgg = new DirectorCard
                {
                    spawnCard = iscLemEggIT,
                    selectionWeight = 10,
                    preventOverhead = false,
                    minimumStageCompletions = 0,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
                };
                int egg = dccs.AddCategory("Egg", 40);
                dccs.AddCard(egg, simuLemEgg);
            }
        }

        private static void SacrificeArtifactManager_OnArtifactEnabled(On.RoR2.Artifacts.SacrificeArtifactManager.orig_OnArtifactEnabled orig, RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            orig(runArtifactManager, artifactDef);
            if (!NetworkServer.active)
            {
                return;
            }
            if (artifactDef != RoR2Content.Artifacts.sacrificeArtifactDef)
            {
                return;
            }
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                Debug.Log("Simulacrum : Added Sacrifice");
                On.RoR2.Util.GetExpAdjustedDropChancePercent += SimulacrumNerfSacrifice;
            }
        }

        private static float SimulacrumNerfSacrifice(On.RoR2.Util.orig_GetExpAdjustedDropChancePercent orig, float baseChancePercent, GameObject characterBodyObject)
        {
            return orig(baseChancePercent, characterBodyObject) * 0.7f;
        }

        private static void SacrificeArtifactManager_OnArtifactDisabled(On.RoR2.Artifacts.SacrificeArtifactManager.orig_OnArtifactDisabled orig, RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            orig(runArtifactManager, artifactDef);
            if (!NetworkServer.active)
            {
                return;
            }
            if (artifactDef != RoR2Content.Artifacts.sacrificeArtifactDef)
            {
                return;
            }
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                Debug.Log("Simulacrum : Removed Sacrifice");
                On.RoR2.Util.GetExpAdjustedDropChancePercent -= SimulacrumNerfSacrifice;
            }
        }
    }


}