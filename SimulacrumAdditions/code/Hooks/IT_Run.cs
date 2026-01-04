using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class ITRun_Hooks
    {
        public static SceneDef PreviousSceneDef = null;

        internal static void AddHooks()
        {
            On.RoR2.InfiniteTowerRun.PreStartClient += ITRun_SetThingsOn_Start;
            On.RoR2.InfiniteTowerRun.OnDestroy += ITRun_SetThingsOn_End;

            //Prevent Repeat Stages
            On.RoR2.InfiniteTowerRun.OnWaveAllEnemiesDefeatedServer += PreventRepeatingStages;

            //Super Boss
            On.RoR2.InfiniteTowerWaveCategory.SelectWavePrefab += ForceSuperBossWave;

            //Scale Wave rarities
            On.RoR2.InfiniteTowerRun.AdvanceWave += MakeWavesMoreCommon;

            //MoreItems
            if (WConfig.cfgItemsFrequently.Value)
            {
                On.RoR2.InfiniteTowerRun.AdvanceWave += MoreItems_AdvanceWave;
            }

            On.RoR2.InfiniteTowerRun.Start += InfiniteTowerRun_Start;
        }

        private static void InfiniteTowerRun_Start(On.RoR2.InfiniteTowerRun.orig_Start orig, InfiniteTowerRun self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                self.SetEventFlag("NoArtifactWorld");
                self.SetEventFlag("NoMysterySpace");
            }
        }

        private static void MakeWavesMoreCommon(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            orig(self);
            if (self.waveIndex == 50 || self.waveIndex == 31)
            {
                if (Constant.ITBossWaves.wavePrefabs[0].weight > 1)
                {
                    float mult = 1;
                    if (self.waveIndex == 31)
                    {
                        mult = Constant.DefaultWeightMultiplier1;
                    }
                    else
                    {
                        mult = Constant.DefaultWeightMultiplier2;
                    }

                    Constant.ITBasicWaves.wavePrefabs[0].weight = Constant.BasicWaveWeight * mult; //More Wackies past this
                    Constant.ITBasicWaves.GenerateWeightedSelection();
                    Constant.ITBossWaves.wavePrefabs[0].weight = Constant.BasicBossWaveWight * mult; //More Wackies past this
                    Constant.ITBossWaves.GenerateWeightedSelection();
                }
            }
        }


        private static void MoreItems_AdvanceWave(On.RoR2.InfiniteTowerRun.orig_AdvanceWave orig, InfiniteTowerRun self)
        {
            if (self.enemyItemPatternIndex >= 5 && self.enemyItemPeriod > 5)
            {
                self.enemyItemPeriod /= 2;
            }
            /*else if (self.enemyItemPatternIndex >= 10 && self.enemyItemPeriod > 3)
            {
                self.enemyItemPeriod /= 2;
            }*/

            orig(self);

            if (WConfig.cfgDumpInfo.Value)
            {
                if (NetworkServer.active)
                {
                    if (self.waveIndex > self.enemyItemPeriod)
                    {
                        int ensure = self.waveIndex;
                        int itemCount = 0;
                        int fakeItemPeriod = 8;

                        while (ensure >= fakeItemPeriod)
                        {
                            ensure -= fakeItemPeriod;
                            itemCount++;
                            if (itemCount % 5 == 0)
                            {
                                if (fakeItemPeriod > 2)
                                {
                                    fakeItemPeriod /= 2;
                                }
                            }
                        }
                        Debug.Log("WaveIndex:" + self.waveIndex + " ExpectedItemCount " + itemCount);


                        while (self.enemyItemPatternIndex < itemCount)
                        {
                            InfiniteTowerRun.EnemyItemEntry[] array = self.enemyItemPattern;
                            int num = self.enemyItemPatternIndex;
                            self.enemyItemPatternIndex = num + 1;
                            InfiniteTowerRun.EnemyItemEntry enemyItemEntry = array[num % self.enemyItemPattern.Length];
                            if (!enemyItemEntry.dropTable)
                            {
                                return;
                            }
                            PickupIndex pickupIndex = enemyItemEntry.dropTable.GenerateDrop(self.enemyItemRng);
                            if (pickupIndex != PickupIndex.none)
                            {
                                PickupDef pickupDef = PickupCatalog.GetPickupDef(pickupIndex);
                                if (pickupDef != null)
                                {
                                    self.enemyInventory.GiveItem(pickupDef.itemIndex, enemyItemEntry.stacks);
                                    Chat.SendBroadcastChat(new Chat.PlayerPickupChatMessage
                                    {
                                        baseToken = "INFINITETOWER_ADD_ITEM",
                                        pickupToken = pickupDef.nameToken,
                                        pickupColor = pickupDef.baseColor
                                    });
                                }
                            }
                        }
                    }
                }
            }
        }

        public static GameObject ForceSuperBossWave(On.RoR2.InfiniteTowerWaveCategory.orig_SelectWavePrefab orig, InfiniteTowerWaveCategory self, InfiniteTowerRun run, Xoroshiro128Plus rng)
        {
            if (run.waveIndex >= Constant.SimuForcedBossStartAtXWaves && run.waveIndex % Constant.SimuForcedBossEveryXWaves == Constant.SimuForcedBossWaveRest)
            {
                //SimuMain.ITSuperBossWaves.GenerateWeightedSelection();
                //temp = SimuMain.ITSuperBossWaves.weightedSelection.Evaluate(rng.nextNormalizedFloat);
                //temp = SimuMain.ITSuperBossWaves.SelectWavePrefab(run, rng);
                GameObject temp = orig(Constant.ITSuperBossWaves, run, rng);
                Debug.Log("Forcing SuperBoss");
                Debug.Log(run.waveIndex + " Forced SuperBoss  " + temp);
                return temp;
            }
            else
            {
                GameObject temp = orig(self, run, rng);
                Debug.Log(run.waveIndex + " SelectWavePrefab  " + temp);
                return temp;
            }
        }

        public static void PreventRepeatingStages(On.RoR2.InfiniteTowerRun.orig_OnWaveAllEnemiesDefeatedServer orig, InfiniteTowerRun self, InfiniteTowerWaveController wc)
        {
            orig(self, wc);
            if (self.IsStageTransitionWave())
            {
                //Debug.Log("\nPreviousSceneDef " + PreviousSceneDef + "\n" + "CurrentSceneDef " + Stage.instance.sceneDef + "\n" + "NextSceneDef " + self.nextStageScene);
                if (PreviousSceneDef != null && PreviousSceneDef == self.nextStageScene)
                {
                    int preventInfiniteLoop = 0;
                    //Debug.Log("Preventing repeat scene");
                    do
                    {
                        preventInfiniteLoop++;
                        self.PickNextStageSceneFromCurrentSceneDestinations();
                        //Debug.Log("ReplacementSceneDef " + self.nextStageScene);
                    }
                    while (self.nextStageScene == PreviousSceneDef && preventInfiniteLoop < 10);
                }
                PreviousSceneDef = Stage.instance.sceneDef;
            }
        }

        private static void ITRun_SetThingsOn_Start(On.RoR2.InfiniteTowerRun.orig_PreStartClient orig, InfiniteTowerRun self)
        {
            orig(self);
            Constant.ITBossWaves.availabilityPeriod = 5;
            Constant.ITBossWaves.minWaveIndex = 0;
            if (Constant.ITBossWaves.wavePrefabs[0].weight < 1)
            {
                Constant.ITBasicWaves.wavePrefabs[0].weight = Constant.BasicWaveWeight;
                Constant.ITBasicWaves.GenerateWeightedSelection();
                Constant.ITBossWaves.wavePrefabs[0].weight = Constant.BasicBossWaveWight;
                Constant.ITBasicWaves.GenerateWeightedSelection();
            }
            SimulacrumDCCS.MakeITSand(true);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoin.VoidCoinRunStart();
            }
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<StartEvent>().enabled = false;
            eqDrone.AddComponent<EquipmentDroneInSimulacrum>();
            Object.Destroy(eqDrone.GetComponent<SetDontDestroyOnLoad>());

        }

        private static void ITRun_SetThingsOn_End(On.RoR2.InfiniteTowerRun.orig_OnDestroy orig, InfiniteTowerRun self)
        {
            orig(self);
            GameObject eqDrone = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/EquipmentDroneMaster");
            eqDrone.GetComponent<StartEvent>().enabled = true;
            eqDrone.AddComponent<SetDontDestroyOnLoad>();
            Object.Destroy(eqDrone.GetComponent<EquipmentDroneInSimulacrum>());
            SimulacrumDCCS.MakeITSand(false);
            if (WConfig.cfgVoidCoins.Value)
            {
                VoidCoin.VoidCoinRunEnd();
            }
        }
    }


}

