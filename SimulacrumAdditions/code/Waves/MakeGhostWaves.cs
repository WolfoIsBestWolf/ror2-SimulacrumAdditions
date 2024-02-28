using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class SimuWavesGhosts
    {
        public static void CreateGhostSpawnCards()
        {
            CharacterSpawnCard cscITGhostVoidJailer = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITGhostVoidJailer.prefab = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion().prefab;
            cscITGhostVoidJailer.name = "cscITGhostVoidJailer";
            cscITGhostVoidJailer.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 20 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 20 },
            };

            MultiCharacterSpawnCard cscITGhostBasicSlow = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITGhostBasicSlow.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser").prefab,
            };
            cscITGhostBasicSlow.name = "cscITGhostBasicSlow";
            cscITGhostBasicSlow.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 3 },
                new ItemCountPair { itemDef = RoR2Content.Items.SprintOutOfCombat, count = 2 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },
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
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 3 },
                new ItemCountPair { itemDef = RoR2Content.Items.SprintOutOfCombat, count = 3 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 6 },
                new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },
            };

            SimuWavesMain.CardRandomizerBasicGhost.cscList = new CharacterSpawnCard[] {
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
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },

                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 3 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 4 },
                new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 0 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 10 },
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
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 4 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },

                new ItemCountPair { itemDef = RoR2Content.Items.BoostAttackSpeed, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.SecondarySkillMagazine, count = 4 },
                new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 0 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 10 },
            };


            CharacterSpawnCard cscITBossGhostRoboBallBoss = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostRoboBallBoss.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscSuperRoboBallBoss").prefab;
            cscITBossGhostRoboBallBoss.name = "cscITBossGhostRoboBallBoss";
            cscITBossGhostRoboBallBoss.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 5 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },

                new ItemCountPair { itemDef = RoR2Content.Items.AlienHead, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 3 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 20 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 10 },
            };

            CharacterSpawnCard cscITBossGhostGrandparent = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostGrandparent.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscGrandparent").prefab;
            cscITBossGhostGrandparent.name = "cscITBossGhostGrandparent";
            cscITBossGhostGrandparent.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 4 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },

                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 20 },
                new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 30 },
            };

            CharacterSpawnCard cscITBossGhostVagrant = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITBossGhostVagrant.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant").prefab;
            cscITBossGhostVagrant.name = "cscITBossGhostVagrant";
            cscITBossGhostVagrant.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = DLC1Content.Items.CritGlassesVoid, count = 200 },

                new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 2 },
                new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 10 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 20 },
            };

            SimuWavesMain.CardRandomizerBossGhost.cscList = new CharacterSpawnCard[] {
                cscITBossGhostNormal,
                cscITBossGhostLimitedSpecial,
                cscITBossGhostLimitedSpecial,
                cscITBossGhostGrandparent,
                cscITBossGhostRoboBallBoss,
                cscITBossGhostVagrant
            };
        }

    }
}