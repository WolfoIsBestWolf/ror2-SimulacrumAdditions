using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    
    public class Waves_Family
    {
        public static Color FamilyEventIconColor = new Color(1f, 0.95f, 0.75f, 1);
        public static Color FamilyEventOutlineColor = new Color(0.8f, 0.78f, 0.5f, 1);

        public static void MakeWaves()
        {
            LGTFamily();
            float ITFamilyWaveWeight = 0.85f;

            #region Base Game
            #region Beetle : Family
            GameObject InfiniteTowerWaveFamilyBeetle = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyBeetle", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyBeetle = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyBeetle", false);
            
            InfiniteTowerWaveFamilyBeetle.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyBeetle;
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_BEETLE";
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_BEETLE";

            InfiniteTowerWaveCategory.WeightedWave ITBeetleFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyBeetle, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBeetleFamily);
            #endregion

            #region Wisp : Family
            GameObject InfiniteTowerWaveFamilyWisp = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyWisp", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyWisp = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyWisp", false);

            InfiniteTowerWaveFamilyWisp.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyWisp;
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_WISP";
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_WISP";

            InfiniteTowerWaveCategory.WeightedWave ITWispFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyWisp, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWispFamily);
            #endregion

            #region Golem : Family
            GameObject InfiniteTowerWaveFamilyGolem = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyGolem", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyGolem = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyGolem", false);

            InfiniteTowerWaveFamilyGolem.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGolem;
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_GOLEM";
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_GOLEM";

            InfiniteTowerWaveCategory.WeightedWave ITGolemFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGolem, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITGolemFamily);
            #endregion

            #region Jellyfish : Family
            GameObject InfiniteTowerWaveFamilyJellyfish = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyJellyfish", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyJellyfish = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyJellyfish", false);

            InfiniteTowerWaveFamilyJellyfish.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsJellyfishFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyJellyfish;
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_JELLYFISH";
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_JELLYFISH";

            InfiniteTowerWaveCategory.WeightedWave ITJellyfishFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyJellyfish, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITJellyfishFamily);
            #endregion

            #region Lemurian : Family
            GameObject InfiniteTowerWaveFamilyLemurian = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyLemurian", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLemurian = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyLemurian", false);

            InfiniteTowerWaveFamilyLemurian.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLemurian;
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_LEMURIAN";
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_LEMURIAN";

            InfiniteTowerWaveCategory.WeightedWave ITLemurianFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLemurian, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLemurianFamily);
            #endregion

            #region Imp : Family
            GameObject InfiniteTowerWaveFamilyImp = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyImp", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyImp = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyImp", false);

            InfiniteTowerWaveFamilyImp.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsImpFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyImp;
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_IMP";
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_IMP";

            InfiniteTowerWaveCategory.WeightedWave ITImpFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyImp, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITImpFamily);
            #endregion

            #region Parent : Family
            GameObject InfiniteTowerWaveFamilyParent = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyParent", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyParent = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyParent", false);

            InfiniteTowerWaveFamilyParent.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsParentFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyParent;
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_PARENT";
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_PARENT";

            InfiniteTowerWaveCategory.WeightedWave ITParentFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyParent, weight = ITFamilyWaveWeight * 2 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITParentFamily);
            #endregion

            #region Mushroom : Family
            GameObject InfiniteTowerWaveFamilyMushroom = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyMushroom", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyMushroom = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyMushroom", false);

            InfiniteTowerWaveFamilyMushroom.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyMushroom.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyMushroom.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyMushroom;
            InfiniteTowerCurrentWaveUIFamilyMushroom.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_MUSHROOM";
            InfiniteTowerCurrentWaveUIFamilyMushroom.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_MUSHROOM";

            InfiniteTowerWaveCategory.WeightedWave ITMushroomFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyMushroom, weight = ITFamilyWaveWeight / 2 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITMushroomFamily);
            #endregion

            #region Lunar : Family
            GameObject InfiniteTowerWaveFamilyLunar = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyLunar", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLunar = PrefabAPI.InstantiateClone(Const.LunarWaveUI, "InfiniteTowerCurrentWaveUIFamilyLunar", false);

            InfiniteTowerWaveFamilyLunar.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;

            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLunar;
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_LUNAR";
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_LUNAR";

            InfiniteTowerWaveCategory.WeightedWave ITLunarFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLunar, weight = ITFamilyWaveWeight * 2f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarFamily);
            #endregion

            #endregion

            #region DLC1 Families
            #region Gup : Family
            GameObject InfiniteTowerWaveFamilyGup = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyGup", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyGup = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyGup", false);

            InfiniteTowerWaveFamilyGup.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGup;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_GUP";
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_GUP";

            InfiniteTowerWaveCategory.WeightedWave ITGupFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGup, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITGupFamily);
            #endregion

            #region Acid Larva : Family
            GameObject InfiniteTowerWaveFamilyLarva = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyLarva", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLarva = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyLarva", false);

            InfiniteTowerWaveFamilyLarva.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyLarva.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;

            InfiniteTowerWaveFamilyLarva.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLarva;
            InfiniteTowerCurrentWaveUIFamilyLarva.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_LARVA";
            InfiniteTowerCurrentWaveUIFamilyLarva.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_LARVA";

            InfiniteTowerWaveCategory.WeightedWave ITLarvaFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLarva, weight = ITFamilyWaveWeight * 2, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLarvaFamily);
            #endregion

            #region Construct : Family
            GameObject InfiniteTowerWaveFamilyConstruct = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyConstruct", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyConstruct = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyConstruct", false);

            InfiniteTowerWaveFamilyConstruct.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyConstruct;
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_CONSTRUCT";
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_CONSTRUCT";

            InfiniteTowerWaveCategory.WeightedWave ITConstructFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyConstruct, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITConstructFamily);
            #endregion

            #region Void : Family
            GameObject InfiniteTowerWaveFamilyVoid = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyVoid", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyVoid = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerCurrentWaveUIFamilyVoid", false);

            InfiniteTowerWaveFamilyVoid.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion(); ;
            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;

            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVoid;
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_VOID";
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_VOID";

            InfiniteTowerWaveCategory.WeightedWave ITVoidFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVoid, weight = ITFamilyWaveWeight * 2f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVoidFamily);
            #endregion
            #endregion

            //Do to All families post creation
            GameObject[] ITFamilyWaves = {
                InfiniteTowerWaveFamilyBeetle, InfiniteTowerWaveFamilyGolem, InfiniteTowerWaveFamilyJellyfish,
                InfiniteTowerWaveFamilyWisp, InfiniteTowerWaveFamilyLemurian,
                InfiniteTowerWaveFamilyImp, InfiniteTowerWaveFamilyConstruct, 
                InfiniteTowerWaveFamilyLarva,InfiniteTowerWaveFamilyMushroom,
                InfiniteTowerWaveFamilyParent, InfiniteTowerWaveFamilyGup,
                InfiniteTowerWaveFamilyLunar,InfiniteTowerWaveFamilyVoid};

            for (int i = 0; i < ITFamilyWaves.Length; i++)
            {
                InfiniteTowerWaveController temp = ITFamilyWaves[i].GetComponent<InfiniteTowerWaveController>();
                ITFamilyWaves[i].GetComponent<CombatDirector>().skipSpawnIfTooCheap = false; ;
                temp.baseCredits = 170f;
                temp.immediateCreditsFraction = 0.35f;
            }

            GameObject[] ITFamilyUIs = {
                InfiniteTowerCurrentWaveUIFamilyBeetle, InfiniteTowerCurrentWaveUIFamilyGolem, InfiniteTowerCurrentWaveUIFamilyJellyfish,
                InfiniteTowerCurrentWaveUIFamilyWisp, InfiniteTowerCurrentWaveUIFamilyLemurian,
                InfiniteTowerCurrentWaveUIFamilyImp, InfiniteTowerCurrentWaveUIFamilyConstruct,
                InfiniteTowerCurrentWaveUIFamilyLarva, InfiniteTowerCurrentWaveUIFamilyMushroom,
                InfiniteTowerCurrentWaveUIFamilyParent };

            Sprite texItFamilyBasicS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveFamily.png"), WRect.rec64, WRect.half);

            for (int i = 0; i < ITFamilyUIs.Length; i++)
            {
                //ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItFamilyBasicS;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;
            }
 

            Sprite texITWaveGupIconBasicS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveGupYellow.png"), WRect.rec64, WRect.half);

            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveGupIconBasicS;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

           
            BossFamilyWave();
            //DroneFamilyWave();

        }

        public static void DroneFamilyWave()
        {
            //Boss Boss Family
            GameObject InfiniteTowerWaveBossFamilyDrones = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyDrones", true);
            GameObject InfiniteTowerWaveBossDronesMachinesUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveBossFamilyDronesUI", false);
            DirectorCardCategorySelection dccsITDrones = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

            InfiniteTowerWaveBossFamilyDrones.GetComponent<CombatDirector>().monsterCards = dccsITDrones;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;

            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().baseCredits = 300f;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0f;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<CombatDirector>().eliteBias = 0.5f;
            InfiniteTowerWaveBossFamilyDrones.AddComponent<SimulacrumExtrasHelper>().newRadius = 140;

            InfiniteTowerWaveBossFamilyDrones.AddComponent<SimuWaveSizeModifier>().sizeModifier = 2f;


            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossDronesMachinesUI;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FAMILY_DRONES";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FAMILY_DRONES";

            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;



            InfiniteTowerWaveCategory.WeightedWave ITBossDrones = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossFamilyDrones, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossDrones);

            ItemDef CaptainDefenseMatrix = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CaptainDefenseMatrix");
            ItemDef DroneWeaponsBoost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsBoost");
            ItemDef DroneWeaponsDisplay1 = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsDisplay1");
            ItemDef DroneWeaponsDisplay2 = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsDisplay2");
            ItemDef CutHp = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHp");
            ItemDef HalfSpeedDoubleHealth = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/HalfSpeedDoubleHealth/HalfSpeedDoubleHealth.asset").WaitForCompletion();
            int damageDown = 80;
            int healthScaling = 40;
            int healthBoost = 10;
            int credits = 70;

            CharacterSpawnCard cscITDrone = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscITDrone.name = "cscITDrone1";
            cscITDrone.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/Drone1Master.prefab").WaitForCompletion();
            cscITDrone.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscITDrone.directorCreditCost = 40;
            cscITDrone.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = (int)(damageDown*1.15f) },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling/2 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost/2 },
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 50},
                new ItemCountPair { itemDef = HalfSpeedDoubleHealth, count = 1},
                new ItemCountPair { itemDef = CutHp, count = 1},
            };

            CharacterSpawnCard cscITDroneStriker = Object.Instantiate(cscITDrone);
            cscITDroneStriker.name = "cscITDroneStriker";
            cscITDroneStriker.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/DroneBackupMaster.prefab").WaitForCompletion();
            cscITDroneStriker.directorCreditCost = credits * 2;
            cscITDroneStriker.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = damageDown },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling},//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 0},//
            };

            CharacterSpawnCard cscITDroneFlame = Object.Instantiate(cscITDrone);
            cscITDroneFlame.name = "cscITDroneFlame";
            cscITDroneFlame.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/FlameDroneMaster.prefab").WaitForCompletion();
            cscITDroneFlame.directorCreditCost = 60;
            cscITDroneFlame.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = damageDown },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling },//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
            };

            CharacterSpawnCard cscITDroneMissile = Object.Instantiate(cscITDroneFlame);
            cscITDroneMissile.name = "cscITDroneMissile";
            cscITDroneMissile.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/DroneMissileMaster.prefab").WaitForCompletion();
            cscITDroneMissile.directorCreditCost = credits;
            cscITDroneMissile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = (int)(damageDown*1.1f) },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling  },//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 33},//
            };

            CharacterSpawnCard cscITDroneEngiWalker = Object.Instantiate(cscITDroneFlame);
            cscITDroneEngiWalker.name = "cscITDroneEngiWalker";
            cscITDroneEngiWalker.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiWalkerTurretMaster.prefab").WaitForCompletion();
            cscITDroneEngiWalker.directorCreditCost = credits;
            cscITDroneEngiWalker.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITDroneEngiWalker.itemsToGrant[1].count = healthScaling *= 2;
            cscITDroneEngiWalker.itemsToGrant[2].count = healthBoost *= 2;

            CharacterSpawnCard cscITDroneEngiTurret = Object.Instantiate(cscITDroneEngiWalker);
            cscITDroneEngiTurret.name = "cscITDroneEngiTurret";
            cscITDroneEngiTurret.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiTurretMaster.prefab").WaitForCompletion();
            cscITDroneEngiTurret.directorCreditCost = credits;
            cscITDroneEngiTurret.itemsToGrant[1].count = healthScaling *= 2;
            cscITDroneEngiTurret.itemsToGrant[2].count = healthBoost *= 2;

            CharacterSpawnCard cscITDroneTurret = Object.Instantiate(cscITDroneEngiWalker);
            cscITDroneTurret.name = "cscITDroneTurret";
            cscITDroneTurret.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/Turret1Master.prefab").WaitForCompletion();
            cscITDroneTurret.directorCreditCost = credits/3;
            cscITDroneTurret.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = (int)(damageDown*1.25f) },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling/4 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost/4},
                new ItemCountPair { itemDef = ItemHelpers.ITAttackSpeedDown, count = 70},
            };

            CharacterSpawnCard cscITDroneEmergency = Object.Instantiate(cscITDroneFlame);
            cscITDroneEmergency.name = "cscITDroneEmergency";
            cscITDroneEmergency.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EmergencyDroneMaster.prefab").WaitForCompletion();
            cscITDroneEmergency.directorCreditCost = credits;
            cscITDroneEmergency.itemsToGrant[1].count = healthScaling / 2;

            CharacterSpawnCard cscITDroneMega = Object.Instantiate(cscITDroneFlame);
            cscITDroneMega.name = "cscITDroneMega";
            cscITDroneMega.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/MegaDroneMaster.prefab").WaitForCompletion();
            cscITDroneMega.directorCreditCost = 350;
            cscITDroneMega.itemsToGrant[0].count = damageDown+18;
            cscITDroneMega.itemsToGrant[1].count = healthScaling / 4;
            cscITDroneMega.itemsToGrant[2].count = healthBoost / 3;

            CharacterSpawnCard cscITDroneColDrone = Object.Instantiate(cscITDroneFlame);
            cscITDroneColDrone.name = "cscITDroneColDrone";
            cscITDroneColDrone.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DroneCommander/DroneCommanderMaster.prefab").WaitForCompletion();
            cscITDroneColDrone.directorCreditCost = 240;
            cscITDroneColDrone.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = (int)(damageDown*1.15f) },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = healthScaling*2 },//1
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost*2 },
                new ItemCountPair { itemDef = DroneWeaponsBoost, count = 1 },
                new ItemCountPair { itemDef = DroneWeaponsDisplay1, count = 1 },
                new ItemCountPair { itemDef = DroneWeaponsDisplay2, count = 1 },
            };

            CharacterSpawnCard cscITDroneToolbot = Object.Instantiate(cscITDroneStriker);
            cscITDroneToolbot.name = "cscITDroneToolbot";
            cscITDroneToolbot.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Toolbot/ToolbotMonsterMaster.prefab").WaitForCompletion();
            cscITDroneToolbot.directorCreditCost = 240;
            cscITDroneToolbot.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ItemHelpers.ITDamageDown, count = 70 },
                new ItemCountPair { itemDef = ItemHelpers.ITHealthScaling, count = 300 },//1
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = 10 },//1
                new ItemCountPair { itemDef = CaptainDefenseMatrix, count = 1 },
            };

            dccsITDrones.name = "dccsITDrones";
            dccsITDrones.AddCategory("Champions", 3);
            dccsITDrones.AddCategory("Minibosses", 4);
            dccsITDrones.AddCategory("Monsters", 2);
            //
            dccsITDrones.AddCard(0, new DirectorCard
            {
                spawnCard = cscITDroneMega,
                selectionWeight = 1,
            });
            dccsITDrones.AddCard(0, new DirectorCard
            {
                spawnCard = cscITDroneColDrone,
                selectionWeight = 1,
            });
            /*dccsITDrones.AddCard(0, new DirectorCard
            {
                spawnCard = cscITDroneToolbot,
                selectionWeight = 1,
            });*/
            //1
            dccsITDrones.AddCard(1, new DirectorCard 
            {
                spawnCard = cscITDroneFlame,
                selectionWeight = 5,
            });
            /*dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneEmergency,
                selectionWeight = 2,
            });*/
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneStriker,
                selectionWeight = 2,
            });
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneEngiWalker,
                selectionWeight = 8,
            });
            /*dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneEngiTurret,
                selectionWeight = 2,
            });*/
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneMissile,
                selectionWeight = 4,
            });
            //2
            dccsITDrones.AddCard(2, new DirectorCard
            {
                spawnCard = cscITDrone,
                selectionWeight = 5,
            });
            /*dccsITDrones.AddCard(2, new DirectorCard
            {
                spawnCard = cscITDroneTurret,
                selectionWeight = 1,
            });*/



        }

        public static void BossFamilyWave()
        {
            //Boss Boss Family
            GameObject InfiniteTowerWaveBossFamilyBoss = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyBosses", true);
            GameObject InfiniteTowerWaveBossFamilyBossUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerWaveBossFamilyBossesUI", false);
            DirectorCardCategorySelection dccsBossesOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

            InfiniteTowerWaveBossFamilyBoss.GetComponent<CombatDirector>().monsterCards = dccsBossesOnly;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;

            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().baseCredits = 500f;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.4f;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 40;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;

            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossFamilyBossUI;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FAMILY_BOSS";
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FAMILY_BOSS";
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;


            InfiniteTowerWaveCategory.WeightedWave ITBossFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossFamilyBoss, weight = 2f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossFamily);

            dccsBossesOnly.AddCategory("Champions", 10);
            dccsBossesOnly.name = "dccsBossesOnly";
            dccsBossesOnly.AddCard(0, new DirectorCard //cscBeetleQueen
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBeetleQueen"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscClayBoss
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBoss"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscElectricWorm
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscGravekeeper
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGravekeeper"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscImpBoss
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscMagmaWorm
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscRoboBallBoss
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscScav
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscScav"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscVagrant
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscVoidMegaCrab
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVoidMegaCrab"),
                selectionWeight = 0,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscGrandparent
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscGrandparent"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscTitanBlackBeach
            {
                spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscTitanBlackBeach"),
                selectionWeight = 1,
            });
            dccsBossesOnly.AddCard(0, new DirectorCard //cscMegaConstruct
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                selectionWeight = 1,
            });


        }


        public static void LGTFamily()
        {
            //Are loaded by the time I add the mod
            //Modded Families
            #region Clay : Family
            GameObject InfiniteTowerWaveFamilyClay = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyClay", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyClay = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyClay", false);
            
            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyClay;
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_CLAY";
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_CLAY";

            #endregion
            #region Solus : Family
            GameObject InfiniteTowerWaveFamilyRoboBall = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyRoboBall", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyRoboBall = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyRoboBall", false);
            
            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyRoboBall;
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_SOLUS";
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_SOLUS";

            #endregion
            #region Vermin : Family
            GameObject InfiniteTowerWaveFamilyVermin = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveFamilyVermin", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyVermin = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentWaveUIFamilyVermin", false);

            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVermin;
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FAMILY_VERMIN";
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FAMILY_VERMIN";

            #endregion
            #region Worm Boss : Family
            GameObject BossInfiniteTowerWaveFamilyWorms = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveBossFamilyWorms", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyWorms = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerCurrentWaveUIFamilyWorms", false);

            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;

            BossInfiniteTowerWaveFamilyWorms.GetComponent<CombatDirector>().eliteBias = 0f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.3f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12; //The director doesn't seem to really care
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().baseCredits = 500f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 20;
            BossInfiniteTowerWaveFamilyWorms.AddComponent<SimulacrumExtrasHelper>().newRadius = 140;

            BossInfiniteTowerWaveFamilyWorms.AddComponent<SimuExplicitStats>().hpBonusMulti = -1;
            
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyWorms;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_FAMILY_WORM";
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_FAMILY_WORM";
            #endregion

            //Do to All families post creation
            GameObject[] ITFamilyWaves = {InfiniteTowerWaveFamilyClay, InfiniteTowerWaveFamilyRoboBall, InfiniteTowerWaveFamilyVermin };

            for (int i = 0; i < ITFamilyWaves.Length; i++)
            {
                InfiniteTowerWaveController temp = ITFamilyWaves[i].GetComponent<InfiniteTowerWaveController>();
                ITFamilyWaves[i].GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;
                temp.baseCredits = 170f;
                temp.immediateCreditsFraction = 0.35f;
            }


            Sprite texItFamilyBasicS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveFamily.png"), WRect.rec64, WRect.half);

            GameObject[] ITFamilyUIs = {InfiniteTowerCurrentWaveUIFamilyClay, InfiniteTowerCurrentWaveUIFamilyRoboBall, InfiniteTowerCurrentWaveUIFamilyVermin };
            for (int i = 0; i < ITFamilyUIs.Length; i++)
            {
                //ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItFamilyBasicS;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;
            }
            //
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;
    
            float ITFamilyWaveWeight = 0.85f;
            FamilyDirectorCardCategorySelection[] FamilyDCCSs = Object.FindObjectsOfType(typeof(RoR2.FamilyDirectorCardCategorySelection)) as RoR2.FamilyDirectorCardCategorySelection[];
            for (var i = 0; i < FamilyDCCSs.Length; i++)
            {
                //Debug.Log(FamilyDCCSs[i].name);
                switch (FamilyDCCSs[i].name)
                {
                    case "dccsClayFamily":
                        InfiniteTowerWaveFamilyClay.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        InfiniteTowerWaveCategory.WeightedWave ITClayFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyClay, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITClayFamily);
                        break;
                    case "dccsRoboBallFamily":
                        InfiniteTowerWaveFamilyRoboBall.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        InfiniteTowerWaveCategory.WeightedWave ITRoboBallFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyRoboBall, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITRoboBallFamily);
                        break;
                    case "dccsVerminFamily":
                        InfiniteTowerWaveFamilyVermin.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        InfiniteTowerWaveCategory.WeightedWave ITVerminFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVermin, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVerminFamily);
                        break;
                    case "dccsWormsFamily":
                        BossInfiniteTowerWaveFamilyWorms.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        InfiniteTowerWaveCategory.WeightedWave ITWormsFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = BossInfiniteTowerWaveFamilyWorms, weight = 4f, prerequisites = SimuMain.StartWave40Prerequisite };
                        SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITWormsFamily);
                        break;
                }
            }


        }

    }
}