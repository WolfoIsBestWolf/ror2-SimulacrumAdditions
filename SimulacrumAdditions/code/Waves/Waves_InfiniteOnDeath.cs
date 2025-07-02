using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;
using static SimulacrumAdditions.ItemHelpers;

namespace SimulacrumAdditions.Waves
{
    public class Waves_InfiniteOnDeath
    {

        internal static void MakeWaves()
        {

            BuffDef BanditSkull = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/DeathMark");
            //
            #region Void Implosion Death


            //GameObject voidDeathWave = MakeWave(BasicWave, VoidWaveUI, )

            GameObject InfiniteTowerWaveDeathVoid = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveDeathVoid", true);
            GameObject InfiniteTowerWaveDeathVoidUI = PrefabAPI.InstantiateClone(Constant.VoidWaveUI, "InfiniteTowerWaveDeathVoidUI", false);

            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusVoid;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;


            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathVoid.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            CharacterSpawnCard cscITSuicideVoid = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscITSuicideVoid.name = "cscITSuicideVoid";
            cscITSuicideVoid.noElites = true;
            cscITSuicideVoid.directorCreditCost = 1;
            cscITSuicideVoid.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideVoid.hullSize = HullClassification.BeetleQueen;
            cscITSuicideVoid.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ITAttackSpeedDownMult, count = 99 },
                new ItemCountPair { itemDef = ITKillOnCompletion, count = 79 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = HealthDecay, count = 1 },
                new ItemCountPair { itemDef = ITDisableAllSkills, count = 1},
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
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DEATHVOID";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DEATHVOID";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.4f, 0.8f);
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.4f, 0.8f);
            //InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathVoid, weight = 5f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITDeathVoid);
            #endregion
            #region Lunar Exploder Deaths
            //
            GameObject InfiniteTowerWaveDeathLunar = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveDeathLunar", true);
            GameObject InfiniteTowerWaveDeathLunarUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "InfiniteTowerWaveDeathLunarUI", false);

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusLunar;
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
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 1 },
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
            secondDirector.maxSeriesSpawnInterval = 1.5f;
            secondDirector.minSeriesSpawnInterval = 1f;
            secondDirector.maxRerollSpawnInterval = 2.5f;
            secondDirector.minRerollSpawnInterval = 2f;

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathLunarUI;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DEATHLUNAR";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DEATHLUNAR";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.8f, 0.8f);
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.8f, 0.8f);
            //InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathLunar, weight = 5f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITDeathLunar);
            #endregion
            #region Mending Core Spam
            //
            GameObject InfiniteTowerWaveDeathMendingCore = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveDeathMendingCore", true);
            GameObject InfiniteTowerWaveDeathMendingCoreUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveDeathMendingCoreUI", false);

            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITCategoryHealing;
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
                new ItemCountPair { itemDef = ItemHelpers.ITHealthUpMult, count = 10 },
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
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DEATHHEALING";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DEATHHEALING";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathMendingCore = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathMendingCore, weight = 3f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITDeathMendingCore);
            #endregion
            #region Freeze Spam
            //
            GameObject InfiniteTowerWaveDeathIceElite = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveDeathIceElite", true);
            GameObject InfiniteTowerWaveDeathIceEliteUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "InfiniteTowerWaveDeathIceEliteUI", false);

            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicWaveOnKill;
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
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDownMult, count = 99 },
                new ItemCountPair { itemDef = ItemHelpers.ITKillOnCompletion, count = 81 },
                new ItemCountPair { itemDef = ItemHelpers.ITMakeImmune, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.Ghost, count = 1 },
                new ItemCountPair { itemDef = ItemHelpers.HealthDecay, count = 1},
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
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DEATHICE";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DEATHICE";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathIce = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathIceElite, weight = 3f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITDeathIce);

            
            #endregion
        }
 
    }

}