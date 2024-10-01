using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_InfiniteOnDeath
    {
 
        internal static void MakeWaves()
        {

            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef HealthDecay = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/HealthDecay");
            BuffDef BanditSkull = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/DeathMark");
            //
            #region Void Implosion Death
            GameObject InfiniteTowerWaveDeathVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathVoid", true);
            GameObject InfiniteTowerWaveDeathVoidUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathVoidUI", false);

            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathVoid.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            CharacterSpawnCard cscITSuicideVoid = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscITSuicideVoid.name = "cscITSuicideVoid";
            cscITSuicideVoid.noElites = true;
            cscITSuicideVoid.directorCreditCost = 1;
            cscITSuicideVoid.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideVoid.hullSize = HullClassification.BeetleQueen;
            cscITSuicideVoid.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 79 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = HealthDecay, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDisableAllSkills, count = 1},
            };
            DirectorCardCategorySelection dccsITSuicideWaveVoid = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveVoid.name = "dccsITSuicideWaveVoid";
            dccsITSuicideWaveVoid.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveVoid.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideVoid,
                selectionWeight = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveVoid.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideVoid,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            CombatDirector secondDirector = InfiniteTowerWaveDeathVoid.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveVoid;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 2.5f;
            secondDirector.minSeriesSpawnInterval = 1.5f;
            secondDirector.maxRerollSpawnInterval = 0.2f;
            secondDirector.minRerollSpawnInterval = 0.1f;
            //RerollSpawnInterval like closer to how long between each wave

            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathVoidUI;
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Implosion";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless void implosions occur.";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.4f, 0.8f);
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.4f, 0.8f);
            //InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathVoid, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathVoid);
            #endregion
            #region Lunar Exploder Deaths
            //
            GameObject InfiniteTowerWaveDeathLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathLunar", true);
            GameObject InfiniteTowerWaveDeathLunarUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathLunarUI", false);

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathLunar.AddComponent<SimulacrumExtrasHelper>().newRadius = 85;

            CharacterSpawnCard cscITSuicideLunar = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"));
            cscITSuicideLunar.name = "cscITSuicideLunar";
            cscITSuicideLunar.noElites = true;
            cscITSuicideLunar.directorCreditCost = 1;
            cscITSuicideLunar.hullSize = HullClassification.BeetleQueen;
            cscITSuicideLunar.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITSuicideLunar.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 78 },
                new ItemCountPair { itemDef = HealthDecay, count = 2 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITDisableAllSkills, count = 1},
            };
            DirectorCardCategorySelection dccsITSuicideWaveLunar = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveLunar.name = "dccsITSuicideWaveLunar";
            dccsITSuicideWaveLunar.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveLunar.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideLunar,
                selectionWeight = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveLunar.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideLunar,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathLunar.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveLunar;
            secondDirector.monsterCredit = 10000;
            secondDirector.teamIndex = TeamIndex.Void;
            secondDirector.maxSeriesSpawnInterval = 0.8f;
            secondDirector.minSeriesSpawnInterval = 0.7f;
            secondDirector.maxRerollSpawnInterval = 2.3f;
            secondDirector.minRerollSpawnInterval = 2f;

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathLunarUI;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Blaze";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground gets covered in lunar flames.";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.8f, 0.8f);
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.8f, 0.8f);
            //InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathLunar, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathLunar);
            #endregion
            #region Mending Core Spam
            //
            GameObject InfiniteTowerWaveDeathMendingCore = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathMendingCore", true);
            GameObject InfiniteTowerWaveDeathMendingCoreUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathMendingCoreUI", false);

            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryHealing;
            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            CharacterSpawnCard cscITSuicideHealing = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscITSuicideHealing.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerMaster.prefab").WaitForCompletion();
            cscITSuicideHealing.name = "cscITAffixEarthHealerMaster";
            cscITSuicideHealing.noElites = true;
            cscITSuicideHealing.directorCreditCost = 1;
            cscITSuicideHealing.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideHealing.hullSize = HullClassification.Golem;
            cscITSuicideHealing.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel"), count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = 10 },
            };
            CharacterSpawnCard cscITSuicideHealingAir = Object.Instantiate(cscITSuicideHealing);
            cscITSuicideHealingAir.name = "cscITAffixEarthHealerMasterAIR";
            cscITSuicideHealingAir.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;

            DirectorCardCategorySelection dccsITSuicideWaveHealing = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveHealing.name = "dccsITSuicideWaveHealing";
            dccsITSuicideWaveHealing.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveHealing.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideHealing,
                selectionWeight = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveHealing.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideHealingAir,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathMendingCore.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveHealing;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 1.5f;
            secondDirector.minSeriesSpawnInterval = 1f;
            secondDirector.maxRerollSpawnInterval = 0.15f;
            secondDirector.minRerollSpawnInterval = 0.1f;

            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathMendingCoreUI;
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Mending";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless healing cores spawn.";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathMendingCore = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathMendingCore, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathMendingCore);
            #endregion
            #region Freeze Spam
            //
            GameObject InfiniteTowerWaveDeathIceElite = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathIceElite", true);
            GameObject InfiniteTowerWaveDeathIceEliteUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathIceEliteUI", false);

            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathIceElite.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            CharacterSpawnCard cscITSuicideIce = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscITSuicideIce.name = "cscITSuicideIce";
            cscITSuicideIce.noElites = true;
            cscITSuicideIce.directorCreditCost = 1;
            cscITSuicideIce.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideIce.hullSize = HullClassification.Golem;
            cscITSuicideIce.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 81 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = HealthDecay, count = 1},
                new ItemCountPair { itemDef = ItemHelpers.ITDisableAllSkills, count = 1},
            };
            cscITSuicideIce.equipmentToGrant = new EquipmentDef[]
            {
                LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/AffixWhite")
            };

            CharacterSpawnCard cscITSuicideIceAIR = Object.Instantiate(cscITSuicideIce);
            cscITSuicideIceAIR.name = "cscITSuicideIceAIR";
            cscITSuicideIceAIR.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            //cscITSuicideIceAIR.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish").prefab;

            DirectorCardCategorySelection dccsITSuicideWaveIce = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveIce.name = "dccsITSuicideWaveIce";
            dccsITSuicideWaveIce.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIce,
                selectionWeight = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIce,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIceAIR,
                selectionWeight = 1,
                preventOverhead = false,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathIceElite.AddComponent<CombatDirector>();
            secondDirector.teamIndex = TeamIndex.Void;
            secondDirector.monsterCards = dccsITSuicideWaveIce;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 2f;
            secondDirector.minSeriesSpawnInterval = 1f;
            secondDirector.maxRerollSpawnInterval = 0.2f;
            secondDirector.minRerollSpawnInterval = 0.1f;
            //RerollSpawnInterval like closer to how long between each wave

            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathIceEliteUI;
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Ice";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless freezing shocks occur.";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathIce = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathIceElite, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathIce);

            On.RoR2.HealthComponent.Suicide += FixIceOnGhosts;
            #endregion
        }

        private static void FixIceOnGhosts(On.RoR2.HealthComponent.orig_Suicide orig, HealthComponent self, GameObject killerOverride, GameObject inflictorOverride, DamageTypeCombo damageType)
        {
            orig(self, killerOverride, inflictorOverride, damageType);
            if (NetworkServer.active)
            {
                if (self.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                {
                    Vector3 position = self.body.corePosition;
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
                    float num = 12f + self.body.radius;
                    gameObject2.transform.localScale = new Vector3(num, num, num);
                    DelayBlast component = gameObject2.GetComponent<DelayBlast>();
                    if (component)
                    {
                        component.position = position;
                        component.baseDamage = self.body.damage * 1.5f;
                        component.baseForce = 2300f;
                        component.attacker = self.gameObject;
                        component.radius = num;
                        component.crit = Util.CheckRoll(self.body.crit, 0);
                        component.procCoefficient = 0.75f;
                        component.maxTimer = 2f;
                        component.falloffModel = BlastAttack.FalloffModel.None;
                        component.explosionEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/AffixWhiteExplosion");
                        component.delayEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/AffixWhiteDelayEffect");
                        component.damageType = DamageType.Freeze2s;
                        TeamFilter component2 = gameObject2.GetComponent<TeamFilter>();
                        if (component2)
                        {
                            component2.teamIndex = TeamComponent.GetObjectTeam(component.attacker);
                        }
                    }
                }
            }

        }

    }

}