using R2API.Utils;
using RoR2;
using UnityEngine;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class Wave_Hooks
    {

        internal static void AddHooks()
        {
            //General + Manual Stuff that just needs to be done
            On.RoR2.InfiniteTowerRun.InitializeWaveController += InfiniteTowerRun_BeginNextWave;

            //Give Stats here
            On.RoR2.InfiniteTowerExplicitSpawnWaveController.Initialize += GiveStatBoosts_ExplicitSpawnWaveController_Initialize;

            //Do Ghost things
            On.RoR2.InfiniteTowerWaveController.OnCombatSquadMemberDiscovered += InfiniteTowerWaveController_OnCombatSquadMemberDiscovered;

            //Countdown
            On.RoR2.InfiniteTowerWaveController.StartTimer += KillAllGhosts_StartTimer;

            //Post Wave, End Portal, Double Rewards
            On.RoR2.InfiniteTowerWaveController.OnAllEnemiesDefeatedServer += InfiniteTowerWaveController_OnAllEnemiesDefeatedServer;

            //Remove current wave
            On.RoR2.InfiniteTowerRun.CleanUpCurrentWave += InfiniteTowerRun_CleanUpCurrentWave;

            //Double Reward

            SimulacrumExtrasHelper.shareSuitInstalled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.funkfrog_sipondo.sharesuite");

            On.RoR2.InfiniteTowerWaveController.DropRewards += (orig, self) =>
            {
                orig(self);
                SimulacrumExtrasHelper temp = self.GetComponent<SimulacrumExtrasHelper>();
                if (temp && temp.rewardDropTable != null)
                {
                    temp.DropRewards();
                }
            };
        }

        private static void KillAllGhosts_StartTimer(On.RoR2.InfiniteTowerWaveController.orig_StartTimer orig, InfiniteTowerWaveController self)
        {
            orig(self);

            if (WConfig.cfgFasterWavesLater.Value)
            {
                if (self.waveIndex % 5 != 0)
                {
                    int players = Mathf.Min(5, Run.instance.participatingPlayerCount);
                    self.Network_timerStart += self.waveIndex / 10 * players * 0.2f;

                    Debug.Log("secondsAfterWave: " + self.Network_timerStart);
                }
            }
            MusicTrackOverride music = self.gameObject.GetComponent<MusicTrackOverride>();
            if (music)
            {
                music.enabled = false;
            }
            if (NetworkServer.active)
            {
                for (int i = 0; i < CharacterMaster.instancesList.Count; i++)
                {
                    CharacterMaster characterMaster = CharacterMaster.instancesList[i];

                    if (characterMaster && characterMaster.inventory)
                    {
                        int itemCount = characterMaster.inventory.GetItemCount(ItemHelpers.ITKillOnCompletion);
                        if (itemCount > 0)
                        {
                            characterMaster.inventory.GiveItem(RoR2Content.Items.HealthDecay, 1);
                        }
                        if (itemCount > 6)
                        {
                            characterMaster.TrueKill();
                            Object.Destroy(characterMaster.gameObject, 2f);
                        }
                    }
                }
            }
        }

        public static void InfiniteTowerWaveController_OnAllEnemiesDefeatedServer(On.RoR2.InfiniteTowerWaveController.orig_OnAllEnemiesDefeatedServer orig, InfiniteTowerWaveController self)
        {
            orig(self);

            CombatDirector[] combatDirectors = self.gameObject.GetComponents<CombatDirector>();
            if (combatDirectors.Length > 1)
            {
                self.gameObject.GetComponents<CombatDirector>()[0].enabled = false;
                self.gameObject.GetComponents<CombatDirector>()[1].enabled = false;
            }
            SimulacrumInteractablesWaveHelper simulacrumInteractablesWaveHelper = self.gameObject.GetComponent<SimulacrumInteractablesWaveHelper>();
            if (simulacrumInteractablesWaveHelper)
            {
                simulacrumInteractablesWaveHelper.enabled = false;
            }

            InfiniteTowerRun run = Run.instance.GetComponent<InfiniteTowerRun>();
            if (run.waveIndex >= Constant.SimuEndingStartAtXWaves && run.waveIndex % Constant.SimuEndingEveryXWaves == Constant.SimuEndingWaveRest)
            {
                GameObject EndingPortal = DirectorCore.instance.TrySpawnObject(new DirectorSpawnRequest(Constant.iscSimuExitPortal, new DirectorPlacementRule
                {
                    minDistance = 30f,
                    maxDistance = 35f,
                    placementMode = DirectorPlacementRule.PlacementMode.Approximate,
                    position = run.safeWardController.transform.position,
                    spawnOnTarget = run.safeWardController.transform
                }, run.safeWardRng));
            }
            if (self.TryGetComponent<SimuWaveUnsortedExtras>(out var helper))
            {
                helper.OnAllEnemiesDefeatedServer();
            }
            if (self.GetComponent<SimulacrumLightningStormWave>())
            {
                self.GetComponent<SimulacrumLightningStormWave>().DisableStorm();
            }

            if (Run.instance)
            {
                Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
            }
            Debug.Log("OnAllEnemiesDefeatedServer  " + self);
        }


        private static void GiveStatBoosts_ExplicitSpawnWaveController_Initialize(On.RoR2.InfiniteTowerExplicitSpawnWaveController.orig_Initialize orig, InfiniteTowerExplicitSpawnWaveController self, int waveIndex, Inventory enemyInventory, GameObject spawnTargetObject)
        {
            try
            {
                if (NetworkServer.active)
                {
                    if (self.linearCreditsPerWave * waveIndex > 400)
                    {
                        self.linearCreditsPerWave = (self.linearCreditsPerWave * waveIndex / 400);
                    }

                    float bonusBonusHPMulti = 0.5f;
                    float bonusBonusDmgMulti = 0.5f;
                    bool forcedSuperboss = false;
                    if (waveIndex >= Constant.SimuForcedBossStartAtXWaves && waveIndex % Constant.SimuForcedBossEveryXWaves == Constant.SimuForcedBossWaveRest)
                    {
                        forcedSuperboss = true;
                        bonusBonusHPMulti = 1f;
                    }

                    if (self.TryGetComponent<CardRandomizer>(out var eee))
                    {
                        eee.DoTheThing(self);
                    }
                    bool IsCharacterWave = false;
                    if (self.TryGetComponent<SimuWaveUnsortedExtras>(out var help))
                    {
                        bonusBonusHPMulti = help.ExplicitInitialize(self, waveIndex);
                        IsCharacterWave = help.code == SimuWaveUnsortedExtras.Case.AcridWave;
                    }
 
                    SimuExplicitStats stats = self.GetComponent<SimuExplicitStats>();
                    if (stats)
                    {
                        if (stats.halfOnNonFinal && !forcedSuperboss)
                        {
                            bonusBonusHPMulti = stats.hpBonusMulti / 2;
                            bonusBonusDmgMulti = stats.damageBonusMulti / 2;
                        }
                        else
                        {
                            bonusBonusDmgMulti = stats.damageBonusMulti;
                            bonusBonusHPMulti = stats.hpBonusMulti;
                        }
                        if (stats.spawnAsVoidTeam)
                        {
                            self.combatDirector.teamIndex = TeamIndex.Void;
                        }
                        if (stats.ExtraSpawnAfterWave > 0 && waveIndex > stats.ExtraSpawnAfterWave)
                        {
                            self.spawnList[0].count++;
                        }
                    }

                    if (bonusBonusHPMulti > 0)
                    {
                        float num = 1f;
                        float num2 = 1f;
                        num += Run.instance.difficultyCoefficient / 2.5f * System.Math.Max(1, (waveIndex / 10) * 0.225f + 0.225f);
                        num2 += Run.instance.difficultyCoefficient / 30f * System.Math.Max(1, (waveIndex / 10) * 0.04f);
                        num *= bonusBonusHPMulti;
                        num2 *= bonusBonusDmgMulti;

                        if (forcedSuperboss)
                        {
                            num /= (1 + ((Run.instance.participatingPlayerCount - 1) * 0.2f));
                            int num3 = Run.instance.participatingPlayerCount;
                            num *= Mathf.Pow((float)num3, 0.4f);
                        }
                        //num /= (1 + ((Run.instance.participatingPlayerCount - 1) * 0.25f));
                        //int num3 = Mathf.Max(1, Run.instance.livingPlayerCount);
                        //num *= Mathf.Pow((float)num3, 0.5f);
                        int grantHp = Mathf.RoundToInt((num - 1f) * 10f);
                        int grantDamage = Mathf.RoundToInt((num2 - 1f) * 10f);
                        if (grantHp > 100000) { grantHp = 100000; }
                        Debug.LogFormat(self.name + " Special Scaling: currentBoostHpCoefficient={0}, currentBoostDamageCoefficient={1}", new object[]
                        {
                           grantHp,
                           grantDamage
                        });


                        for (int list = 0; list < self.spawnList.Length; list++)
                        {
                            bool hasNoHP = true;
                            bool hasNoDmg = true;
                            bool hasNoTP = true;
                            var grant = self.spawnList[list].spawnCard.itemsToGrant;
                            for (int i = 0; i < grant.Length; i++)
                            {
                                if (grant[i].itemDef == RoR2Content.Items.BoostHp)
                                {
                                    hasNoHP = false;
                                    grant[i].count = grantHp;
                                }
                                else if (grant[i].itemDef == RoR2Content.Items.BoostDamage)
                                {
                                    hasNoDmg = false;
                                    grant[i].count = grantDamage;
                                }
                                else if (grant[i].itemDef == RoR2Content.Items.TeleportWhenOob)
                                {
                                    hasNoTP = false;
                                }
                            }
                            if (hasNoHP)
                            {
                                grant = grant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = grantHp });
                            }
                            if (hasNoDmg)
                            {
                                grant = grant.Add(new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = grantDamage });
                            }
                            if (hasNoTP)
                            {
                                grant = grant.Add(new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 });
                            }

                            /*foreach (ItemCountPair itemPair in self.spawnList[0].spawnCard.itemsToGrant)
                            {
                                Debug.Log(itemPair.itemDef + "  " + itemPair.count);
                            }*/
                        }
                    }

                    if (IsCharacterWave)
                    {
                        self.combatDirector.teamIndex = TeamIndex.Void;

                        self.enemyInventory = enemyInventory;
                        CombatDirector combatDirector = self.combatDirector;
                        SpawnCard spawnCard = self.spawnList[0].spawnCard;
                        EliteDef eliteDef = self.spawnList[0].eliteDef;
                        Transform transform = spawnTargetObject.transform;
                        bool preventOverhead = self.spawnList[0].preventOverhead;
                        combatDirector.Spawn(spawnCard, eliteDef, transform, self.spawnList[0].spawnDistance, preventOverhead, 1f, DirectorPlacementRule.PlacementMode.Approximate);

                        self.combatDirector.teamIndex = TeamIndex.Monster;
                    }
                }
            }
            catch
            {
                Debug.LogWarning("ERROR IN GiveStatBoosts_ExplicitSpawnWaveController_Initialize");
                orig(self, waveIndex, enemyInventory, spawnTargetObject);
                return;
            }
            orig(self, waveIndex, enemyInventory, spawnTargetObject);
            self.combatDirector.teamIndex = TeamIndex.Monster;
        }


        public static void InfiniteTowerRun_BeginNextWave(On.RoR2.InfiniteTowerRun.orig_InitializeWaveController orig, global::RoR2.InfiniteTowerRun self)
        {
            orig(self);

            if (NetworkServer.active)
            {
                //GoldTitanManager.TryStartChannelingTitansServer(self.safeWardController.gameObject, self.safeWardController.gameObject.transform.position, null, null);
            }

            CombatDirector combatDirector = self.waveInstance.GetComponent<CombatDirector>();
            InfiniteTowerWaveController waveController = self.waveInstance.GetComponent<InfiniteTowerWaveController>();
            //Radius
            SimulacrumExtrasHelper radiusManip = self.waveInstance.GetComponent<SimulacrumExtrasHelper>();

            float newRadius = VoidSafeWard_Hooks.baseRadius;
            float originalRadius = self.safeWardController.wardStateMachine.GetComponent<VerticalTubeZone>().radius;

            if (radiusManip && radiusManip.newRadius > 0)
            {
                newRadius = VoidSafeWard_Hooks.baseRadius + (radiusManip.newRadius - 60) + (Run.instance.participatingPlayerCount) * VoidSafeWard_Hooks.radiusPerPlayer;
            }
            else
            {
                newRadius += Run.instance.participatingPlayerCount * VoidSafeWard_Hooks.radiusPerPlayer;
                if (waveController.isBossWave)
                {
                    newRadius += 5;
                }
            }
            if (originalRadius > newRadius)
            {
                VoidSafeWard_Hooks.RadiusShrinker shrink = self.gameObject.AddComponent<VoidSafeWard_Hooks.RadiusShrinker>();
                shrink.originalRadius = originalRadius;
                shrink.newRadius = newRadius;
            }
            else
            {
                self.safeWardController.wardStateMachine.state.SetFieldValue("radius", newRadius);
                self.safeWardController.holdoutZoneController.baseRadius = newRadius;
            }


            combatDirector.minSpawnRange = 15;
            combatDirector.maxSpawnDistance = newRadius - 5;

            if (self._waveController)
            {
                if (self.waveInstance.TryGetComponent<SimuWaveUnsortedExtras>(out var a))
                {
                    a.InitializeWaveController(self, self.waveInstance);
                }
 
                if (NetworkServer.active)
                {
                    if (WConfig.cfgSimuMoreGold.Value)
                    {
                        self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= self.participatingPlayerCount; //Keep in mind cost doesn't scale money just gets divided by player count
                        self.waveInstance.GetComponent<CombatDirector>().goldRewardCoefficient *= Mathf.Max(0.75f, 1.5f - 0.5f * (self.waveIndex / 20));
                    }
                    if (self.waveIndex % 5 == 0)
                    {
                        //Warbanner on Boss Wave  
                        for (int j = 0; j < PlayerCharacterMasterController.instances.Count; j++)
                        {
                            CharacterBody body = PlayerCharacterMasterController.instances[j].body;
                            if (body)
                            {
                                CharacterMaster master = PlayerCharacterMasterController.instances[j].master;
                                if (master)
                                {
                                    int itemCount = master.inventory.GetItemCount(RoR2Content.Items.WardOnLevel);
                                    if (itemCount > 0)
                                    {
                                        GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/WarbannerWard"), body.transform.position, Quaternion.identity);
                                        gameObject.GetComponent<TeamFilter>().teamIndex = TeamIndex.Player;
                                        gameObject.GetComponent<BuffWard>().Networkradius = 8f + 8f * (float)itemCount;
                                        NetworkServer.Spawn(gameObject);
                                    }
                                }
                            }
                        }

                    }
                }
            }
     
            self.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
            if (self.waveIndex < 11)
            {
                if (self.waveIndex == 5)
                {
                    waveController.baseCredits -= 100;
                    waveController.immediateCreditsFraction -= 0.1f;
                    waveController.wavePeriodSeconds += 5;
                }
                if (self.waveIndex == 10)
                {
                    waveController.baseCredits -= 50;
                }
            }


            if (WConfig.cfgExtraDifficuly.Value)
            {
                /*combatDirector.eliteBias = Mathf.Min(combatDirector.eliteBias * 0.5f * (1 - (self.waveIndex - 1 - 70f) / 70f), combatDirector.eliteBias); //Why is it 1.5f in Simu anyways
                combatDirector.eliteBias = Mathf.Max(combatDirector.eliteBias, 0.5f);*/
                combatDirector.eliteBias = Mathf.Min(combatDirector.eliteBias - (float)self.waveIndex / 100f, combatDirector.eliteBias); //Why is it 1.5f in Simu anyways
                combatDirector.eliteBias = Mathf.Max(combatDirector.eliteBias, 0.5f);

                float creditsMulti = 1f + ((self.waveIndex - 1) * 2f / 100f);
                if (creditsMulti > 3)
                {
                    creditsMulti = 3;
                }
                waveController.immediateCreditsFraction *= creditsMulti;

            }
            if (WConfig.cfgFasterWavesLater.Value)
            {
                if (self.waveIndex % 5 == 0)
                {
                    if (WConfig.cfgAwaitTravel.Value)
                    {
                        waveController.secondsAfterWave = 0;
                    }
                    else
                    {
                        int players = Run.instance.participatingPlayerCount;
                        if (players > 1)
                        {
                            waveController.secondsAfterWave += 1 + players;
                        }
                        else
                        {
                            waveController.secondsAfterWave += 1;
                        }
                    }

                }
                else
                {
                    //10,9,8,7
                    waveController.secondsAfterWave = Mathf.Max(waveController.secondsAfterWave - self.waveIndex / 10, 5);
                }
                if (self.waveIndex < 11)
                {
                    waveController.maxSquadSize = 15;
                }
                else if (self.waveIndex > 20)
                {
                    waveController.wavePeriodSeconds *= 0.9f;
                }
                else if (self.waveIndex > 40)
                {
                    waveController.wavePeriodSeconds *= 0.8f;
                }
                else if (self.waveIndex > 60)
                {
                    waveController.wavePeriodSeconds *= 0.6f;
                }
            }
            if (RunArtifactManager.instance.IsArtifactEnabled(RoR2Content.Artifacts.swarmsArtifactDef))
            {
                waveController.maxSquadSize += 10;
            }
            if (waveController.immediateCreditsFraction < 0)
            {
                waveController.immediateCreditsFraction = 0f;
            }

            Debug.Log(self.waveIndex + " immediateCreditsFraction: " + waveController.immediateCreditsFraction + " eliteBias: " + combatDirector.eliteBias + " wavePeriodSeconds: " + waveController.wavePeriodSeconds);
 
        }


        public static void InfiniteTowerRun_CleanUpCurrentWave(On.RoR2.InfiniteTowerRun.orig_CleanUpCurrentWave orig, InfiniteTowerRun self)
        {
            try
            {
                if (NetworkServer.active)
                {
                    if (self.waveInstance)
                    {
                        if (self.waveInstance.TryGetComponent<SimuWaveUnsortedExtras>(out var a))
                        {
                            a.CleanUpCurrentWave(self, self.waveInstance);
                        }
                        if (Run.instance)
                        {
                            Run.instance.GetComponent<RoR2.EnemyInfoPanelInventoryProvider>().MarkAsDirty();
                        }
                    }
                }
            }
            catch
            {
                Debug.LogWarning("ERROR IN InfiniteTowerRun_CleanUpCurrentWave");
                orig(self);
                return;
            }
            orig(self);
            Debug.Log("WaveCleanUp  " + self.waveInstance);
        }

        private static void InfiniteTowerWaveController_OnCombatSquadMemberDiscovered(On.RoR2.InfiniteTowerWaveController.orig_OnCombatSquadMemberDiscovered orig, InfiniteTowerWaveController self, CharacterMaster master)
        {
            orig(self, master);
            if (master.inventory)
            {
                int kill = master.inventory.GetItemCount(ItemHelpers.ITKillOnCompletion);
                if (kill > 0)
                {
                    self.combatSquad.RemoveMember(master);
                    CharacterBody body = master.GetBody();

                    if (master.inventory.GetItemCount(ItemHelpers.ITDisableAllSkills) > 0)
                    {
                        body.AddBuff(DLC2Content.Buffs.DisableAllSkills);
                        body.AddBuff(RoR2Content.Buffs.Nullified);
                    }

                    int horror = master.inventory.GetItemCount(ItemHelpers.ITHorrorName);
                    if (horror > 0)
                    {

                    }
                    if (NetworkServer.active)
                    {
                        RoR2.CharacterAI.BaseAI tempAI = master.GetComponent<RoR2.CharacterAI.BaseAI>();
                        if (tempAI)
                        {
                            tempAI.fullVision = true;
                        }
                        for (int i = 0; i < tempAI.skillDrivers.Length; i++)
                        {
                            tempAI.skillDrivers[i].maxUserHealthFraction = 1;
                            if (kill == 4)
                            {
                                if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Special)
                                {
                                    tempAI.skillDrivers[i].maxTimesSelected = 1;
                                }
                            }
                            else if (kill == 5)
                            {
                                if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Utility)
                                {
                                    tempAI.skillDrivers[i].maxTimesSelected = 0;
                                }
                            }
                            else if (kill == 6)
                            {
                                if (tempAI.skillDrivers[i].skillSlot == SkillSlot.Utility)
                                {
                                    tempAI.skillDrivers[i].maxTimesSelected = 2;
                                }
                            }
                        }
                        if (kill == 10)
                        {
                            body.AddBuff(DLC2Content.Buffs.DisableAllSkills);
                        }
                        if (kill == 15)
                        {
                        }
                        else
                        {
                            body.AddBuff(RoR2Content.Buffs.Immune);
                        }

                    }
                }
                else if (self.hasEnabledEnemyIndicators && master.masterIndex == Constant.IndexAffixHealingCore)
                {
                    self.combatSquad.RemoveMember(master);
                }

            }
        }

    }


}

