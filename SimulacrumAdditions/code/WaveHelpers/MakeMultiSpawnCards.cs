using RoR2;
using SimulacrumAdditions.Waves;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class MakeMultiCSCs
    {
        public static void CreateGhostSpawnCards()
        {
            CharacterSpawnCard cscITGhostVoidJailer = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITGhostVoidJailer.prefab = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion().prefab;
            cscITGhostVoidJailer.name = "cscITGhostVoidJailer";
            cscITGhostVoidJailer.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },

                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 40 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 30 },
            };

            MultiCharacterSpawnCard cscITGhostBasicSlow = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITGhostBasicSlow.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser").prefab,
            };
            cscITGhostBasicSlow.name = "cscITGhostBasicSlow";
            cscITGhostBasicSlow.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },

                //new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 0 },
                //new ItemCountPair { itemDef = RoR2Content.Items.SprintOutOfCombat, count = 0 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 1 },
            };

            MultiCharacterSpawnCard cscITGhostBasicSpeedy = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITGhostBasicSpeedy.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem").prefab,
            };
            cscITGhostBasicSpeedy.name = "cscITGhostBasicSpeedy";
            cscITGhostBasicSpeedy.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },

                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 0 },
                new ItemCountPair { itemDef = RoR2Content.Items.SprintOutOfCombat, count = 3 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 5 },
                new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },
            };

            Waves_SpecialGuy.CardRandomizerBasicGhost.cscList = new CharacterSpawnCard[] {
                cscITGhostVoidJailer,
                cscITGhostBasicSlow,
                cscITGhostBasicSpeedy,
                cscITGhostBasicSpeedy,
                cscITGhostBasicSpeedy,
            };
        }

        public static void CreateBossGhostSpawnCards()
        {
            MultiCharacterSpawnCard cscITBossGhostNormal = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITBossGhostNormal.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss").prefab,
            };
            cscITBossGhostNormal.name = "cscITBossGhostNormal";
            cscITBossGhostNormal.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },

                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 1 },
                //new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 0 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 3 },
                //new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 0 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 20 },
            };

            MultiCharacterSpawnCard cscITBossGhostLimitedSpecial = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITBossGhostLimitedSpecial.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss").prefab,
            };
            cscITBossGhostLimitedSpecial.name = "cscITBossGhostLimitedSpecial";
            cscITBossGhostLimitedSpecial.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 6 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },

                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 50 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 6 },
                new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 0 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 20 },
            };


            CharacterSpawnCard cscITBossGhostRoboBallBoss = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostRoboBallBoss.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscSuperRoboBallBoss").prefab;
            cscITBossGhostRoboBallBoss.name = "cscITBossGhostRoboBallBoss";
            cscITBossGhostRoboBallBoss.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 5 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITCooldownUp, count = 5 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },

                //new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 0 },
                //new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 0 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 30 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 50 },
            };

            CharacterSpawnCard cscITBossGhostGrandparent = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostGrandparent.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscGrandparent").prefab;
            cscITBossGhostGrandparent.name = "cscITBossGhostGrandparent";
            cscITBossGhostGrandparent.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                //new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 4 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },

                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 10 },
                new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 40 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 5 },
            };


            On.EntityStates.GrandParent.ChannelSunStart.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.characterBody && self.characterBody.inventory && self.characterBody.inventory.GetItemCount(ItemHelpers.ITHorrorName) > 0)
                {
                    self.duration *= 2f;
                    self.PlayAnimation(EntityStates.GrandParent.ChannelSunStart.animLayerName, EntityStates.GrandParent.ChannelSunStart.animStateName, EntityStates.GrandParent.ChannelSunStart.animPlaybackRateParam, self.duration);
                }
            };

            CharacterSpawnCard cscITBossGhostVagrant = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostVagrant.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant").prefab;
            cscITBossGhostVagrant.name = "cscITBossGhostVagrant";
            cscITBossGhostVagrant.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 4 },
                new ItemCountPair { itemDef = ItemHelpers.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 20 },

                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 2 },
                //new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 30 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 40 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 3 },
            };

            On.EntityStates.VagrantMonster.ChargeMegaNova.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.characterBody && self.characterBody.inventory && self.characterBody.inventory.GetItemCount(ItemHelpers.ITHorrorName) > 0)
                {
                    self.duration *= 1.25f;
                    self.PlayCrossfade("Gesture, Override", "ChargeMegaNova", "ChargeMegaNova.playbackRate", self.duration, 0.3f);
                }
            };

            Waves_SpecialGuy.CardRandomizerBossGhost.cscList = new CharacterSpawnCard[] {
                cscITBossGhostNormal,
                cscITBossGhostLimitedSpecial,
                cscITBossGhostLimitedSpecial,
                cscITBossGhostGrandparent,
                cscITBossGhostRoboBallBoss,
                cscITBossGhostVagrant
            };
        }

        public static void CreateEquipmentDroneSpawnCards()
        {
            CharacterSpawnCard cscEquipmentDroneIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscEquipmentDroneIT.name = "cscEquipmentDroneIT";
            cscEquipmentDroneIT.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscEquipmentDroneIT.noElites = true;
            cscEquipmentDroneIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EquipmentDroneMaster.prefab").WaitForCompletion();
            cscEquipmentDroneIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 15 },//0
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 0 },//1
                new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 },//2
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },//3
                new ItemCountPair { itemDef = RoR2Content.Items.CutHp, count = 0 }, //4
                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 0 }, //5
            };

            /*CharacterSpawnCard cscEquipmentDroneITLightning = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITLightning.name = "cscEquipmentDroneITLightning";
            cscEquipmentDroneITLightning.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Lightning };
            cscEquipmentDroneITLightning.itemsToGrant[0].count = 3;
            cscEquipmentDroneITLightning.itemsToGrant[1].count = 99;*/

            CharacterSpawnCard cscEquipmentDroneITMolotov = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMolotov.name = "cscEquipmentDroneITMolotov";
            cscEquipmentDroneITMolotov.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.Molotov };
            cscEquipmentDroneITMolotov.itemsToGrant[1].count = 60;

            CharacterSpawnCard cscEquipmentDroneITFireBallDash = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITFireBallDash.name = "cscEquipmentDroneITFireBallDash";
            cscEquipmentDroneITFireBallDash.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.FireBallDash };
            cscEquipmentDroneITFireBallDash.itemsToGrant[0].count = 25;
            cscEquipmentDroneITFireBallDash.itemsToGrant[1].count = 97;
            cscEquipmentDroneITFireBallDash.itemsToGrant[5].count = 2;

            CharacterSpawnCard cscEquipmentDroneITTeamWarCry = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITTeamWarCry.name = "cscEquipmentDroneITTeamWarCry";
            cscEquipmentDroneITTeamWarCry.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.TeamWarCry };
            cscEquipmentDroneITTeamWarCry.itemsToGrant[0].count = 15;

            CharacterSpawnCard cscEquipmentDroneITVendingMachine = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITVendingMachine.name = "cscEquipmentDroneITVendingMachine";
            cscEquipmentDroneITVendingMachine.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.VendingMachine };
            cscEquipmentDroneITVendingMachine.itemsToGrant[0].count = 24;
            cscEquipmentDroneITVendingMachine.itemsToGrant[1].count = 35;

            CharacterSpawnCard cscEquipmentDroneITMeteor = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMeteor.name = "cscEquipmentDroneITMeteor";
            cscEquipmentDroneITMeteor.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Meteor };
            cscEquipmentDroneITMeteor.itemsToGrant[0].count = 15;
            cscEquipmentDroneITMeteor.itemsToGrant[1].count = 35;
            cscEquipmentDroneITMeteor.itemsToGrant[4].count = 1;
            cscEquipmentDroneITMeteor.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITCrippleWard = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITCrippleWard.name = "cscEquipmentDroneITCrippleWard";
            cscEquipmentDroneITCrippleWard.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.CrippleWard };
            cscEquipmentDroneITCrippleWard.itemsToGrant[0].count = 15;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].count = 1;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITDeathProjectile = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITDeathProjectile.name = "cscEquipmentDroneITDeathProjectile";
            cscEquipmentDroneITDeathProjectile.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.DeathProjectile }; //45s
            cscEquipmentDroneITDeathProjectile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 3 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = RoR2Content.Items.Plant, count = 5 },
                new ItemCountPair { itemDef = RoR2Content.Items.Tooth, count = 3 },
            };

            Waves_SpecialGuy.CardRandomizerEquipmentDrones.cscList = new CharacterSpawnCard[] {
                cscEquipmentDroneITDeathProjectile,
                cscEquipmentDroneITFireBallDash,
                cscEquipmentDroneITMolotov,
                cscEquipmentDroneITTeamWarCry,
                cscEquipmentDroneITMeteor,
                cscEquipmentDroneITVendingMachine,
                cscEquipmentDroneITCrippleWard,
            };
        }

        public static void CreateDroneSpawnCards()
        {
            CharacterSpawnCard cscITDroneFlame = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscITDroneFlame.name = "cscITDroneFlame";
            cscITDroneFlame.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscITDroneFlame.hullSize = HullClassification.Human;
            cscITDroneFlame.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/FlameDroneMaster.prefab").WaitForCompletion();
            cscITDroneFlame.directorCreditCost = 3;
            cscITDroneFlame.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 10},//
                new ItemCountPair { itemDef = RoR2Content.Items.ExtraLife, count = 10 },//   
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 15 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 80 },
                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 1 },
            };

            CharacterSpawnCard cscITDroneEmergency = Object.Instantiate(cscITDroneFlame);
            cscITDroneEmergency.name = "cscITDroneEmergency";
            cscITDroneEmergency.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EmergencyDroneMaster.prefab").WaitForCompletion();
            cscITDroneEmergency.directorCreditCost = 2;
            cscITDroneEmergency.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 10 },//   
                new ItemCountPair { itemDef = RoR2Content.Items.ExtraLife, count = 10 },//   
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 15 },
                new ItemCountPair { itemDef = RoR2Content.Items.CaptainDefenseMatrix, count = 1},
                new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 1 },
            };

            CharacterSpawnCard cscITDroneTurret = Object.Instantiate(cscITDroneFlame);
            cscITDroneTurret.name = "cscITDroneTurret";
            cscITDroneTurret.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/Turret1Master.prefab").WaitForCompletion();
            cscITDroneTurret.directorCreditCost = 3;
            cscITDroneTurret.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITDroneTurret.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 15 },//
                new ItemCountPair { itemDef = RoR2Content.Items.ExtraLife, count = 10 },//              
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 15 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 60 },
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 98 },
            };


            CharacterSpawnCard cscITDroneMissile = Object.Instantiate(cscITDroneFlame);
            cscITDroneMissile.name = "cscITDroneMissile";
            cscITDroneMissile.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/DroneMissileMaster.prefab").WaitForCompletion();
            cscITDroneMissile.directorCreditCost = 2;
            cscITDroneMissile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 10 },//   
                new ItemCountPair { itemDef = RoR2Content.Items.ExtraLife, count = 10 },//   
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 15 },
                new ItemCountPair { itemDef = RoR2Content.Items.CaptainDefenseMatrix, count = 1},
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDownMult, count = 85 },
            };


            Waves_Misc.DroneWave.spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
                {
                    count = 3,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                    spawnCard = cscITDroneFlame,
                },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
                {
                    count = 1,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Standard,
                    spawnCard = cscITDroneEmergency
                },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
                {
                    count = 0,
                    spawnDistance = DirectorCore.MonsterSpawnDistance.Far,
                    spawnCard = cscITDroneFlame,
                },
            };

        }

    }
}