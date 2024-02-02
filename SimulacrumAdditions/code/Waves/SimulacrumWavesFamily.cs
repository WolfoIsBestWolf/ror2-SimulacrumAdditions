using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    
    public class SimulacrumWavesFamily
    {
        private static Color FamilyEventIconColor = new Color(1f, 0.95f, 0.75f, 1);
        private static Color FamilyEventOutlineColor = new Color(0.8f, 0.78f, 0.5f, 1);

        //No more static
        public static void Start()
        {
            FamilyDirectorCardCategorySelection dccsBeetleFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsBeetleFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsGolemFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGolemFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsJellyfishFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsJellyfishFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsWispFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsWispFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsParentFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsParentFamily.asset").WaitForCompletion();
            //FamilyDirectorCardCategorySelection dccsGupFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsImpFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsImpFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLemurianFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLemurianFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsConstructFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsConstructFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsVoidFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsLarvaFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsAcidLarvaFamily.asset").WaitForCompletion();
            FamilyDirectorCardCategorySelection dccsMushroomFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsMushroomFamily.asset").WaitForCompletion();


            GameObject InfiniteTowerWaveFamilyBeetle = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyBeetle", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyBeetle = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyBeetle", false);

            GameObject InfiniteTowerWaveFamilyGolem = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyGolem", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyGolem = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyGolem", false);

            GameObject InfiniteTowerCurrentWaveUIFamilyJellyfish = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyJellyfish", false);
            GameObject InfiniteTowerWaveFamilyJellyfish = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyJellyfish", true);

            GameObject InfiniteTowerWaveFamilyWisp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyWisp", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyWisp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyWisp", false);

            GameObject InfiniteTowerWaveFamilyLemurian = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyLemurian", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLemurian = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyLemurian", false);

            GameObject InfiniteTowerWaveFamilyImp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyImp", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyImp = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyImp", false);

            GameObject InfiniteTowerWaveFamilyGup = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyGup", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyGup = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyGup", false);

            GameObject InfiniteTowerWaveFamilyConstruct = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyConstruct", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyConstruct = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyConstruct", false);
            //Vanilla Families End
            GameObject InfiniteTowerWaveFamilyParent = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyParent", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyParent = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyParent", false);

            GameObject InfiniteTowerWaveFamilyLarva = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyLarva", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLarva = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyLarva", false);

            GameObject InfiniteTowerWaveFamilyMushroom = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyMushroom", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyMushroom = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyMushroom", false);
            //
            GameObject InfiniteTowerWaveFamilyLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyLunar", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyLunar", false);

            GameObject InfiniteTowerWaveFamilyVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyVoid", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyVoid", false);



            //

            float ITFamilyWaveWeight = 0.85f;
            //Beetle
            InfiniteTowerWaveFamilyBeetle.GetComponent<CombatDirector>().monsterCards = dccsBeetleFamily;
            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyBeetle.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyBeetle;
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Beetle";
            InfiniteTowerCurrentWaveUIFamilyBeetle.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground begins to shift beneath you.";

            InfiniteTowerWaveCategory.WeightedWave ITBeetleFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyBeetle, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBeetleFamily);

            //Golem
            InfiniteTowerWaveFamilyGolem.GetComponent<CombatDirector>().monsterCards = dccsGolemFamily;
            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyGolem.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGolem;
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Earth";
            InfiniteTowerCurrentWaveUIFamilyGolem.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The earth rumbles and groans.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITGolemFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGolem, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITGolemFamily);

            //Jellyfish
            InfiniteTowerWaveFamilyJellyfish.GetComponent<CombatDirector>().monsterCards = dccsJellyfishFamily;
            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyJellyfish.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyJellyfish;
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Jellyfish";
            InfiniteTowerCurrentWaveUIFamilyJellyfish.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air crackles and arcs.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITJellyfishFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyJellyfish, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITJellyfishFamily);

            //Wisps
            InfiniteTowerWaveFamilyWisp.GetComponent<CombatDirector>().monsterCards = dccsWispFamily;
            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyWisp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyWisp;
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Embers";
            InfiniteTowerCurrentWaveUIFamilyWisp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air begins to burn.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWispFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyWisp, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWispFamily);

            //Lemurian
            InfiniteTowerWaveFamilyLemurian.GetComponent<CombatDirector>().monsterCards = dccsLemurianFamily;
            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyLemurian.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLemurian;
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Lemuria";
            InfiniteTowerCurrentWaveUIFamilyLemurian.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground's temperature begins to rise.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLemurianFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLemurian, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLemurianFamily);

            //Imps
            InfiniteTowerWaveFamilyImp.GetComponent<CombatDirector>().monsterCards = dccsImpFamily;
            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;

            InfiniteTowerWaveFamilyImp.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyImp;
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Red Plane";
            InfiniteTowerCurrentWaveUIFamilyImp.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A tear in the fabric of the universe.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITImpFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyImp, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITImpFamily);

            //Gup
            InfiniteTowerWaveFamilyGup.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsGupFamily.asset").WaitForCompletion();
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyGup;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gup";
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air smells of sweet strawberries.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITGupFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyGup, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITGupFamily);

            //Constructs
            InfiniteTowerWaveFamilyConstruct.GetComponent<CombatDirector>().monsterCards = dccsConstructFamily;
            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyConstruct.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyConstruct;
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Constructs";
            InfiniteTowerCurrentWaveUIFamilyConstruct.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "You have tripped an ancient alarm.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITConstructFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyConstruct, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITConstructFamily);
            //Vanilla Families End Here
            //
            //Parents (Pseudo Vanilla)
            InfiniteTowerWaveFamilyParent.GetComponent<CombatDirector>().monsterCards = dccsParentFamily;
            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyParent.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyParent;
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Parenthood";
            InfiniteTowerCurrentWaveUIFamilyParent.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A familial bond is being interrupted.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITParentFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyParent, weight = ITFamilyWaveWeight*2 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITParentFamily);
            //
            //Larva
            InfiniteTowerWaveFamilyLarva.GetComponent<CombatDirector>().monsterCards = dccsLarvaFamily;
            InfiniteTowerWaveFamilyLarva.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;

            InfiniteTowerWaveFamilyLarva.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLarva;
            InfiniteTowerCurrentWaveUIFamilyLarva.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Acid";
            InfiniteTowerCurrentWaveUIFamilyLarva.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The air smells foul.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLarvaFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLarva, weight = ITFamilyWaveWeight*2, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLarvaFamily);
            //
            //Mushroom
            InfiniteTowerWaveFamilyMushroom.GetComponent<CombatDirector>().monsterCards = dccsMushroomFamily;
            InfiniteTowerWaveFamilyMushroom.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyMushroom.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyMushroom;
            InfiniteTowerCurrentWaveUIFamilyMushroom.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Mushrooms";
            InfiniteTowerCurrentWaveUIFamilyMushroom.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground grows sickly.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITMushroomFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyMushroom, weight = ITFamilyWaveWeight/2};
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITMushroomFamily);
            //
            //Lunar
            InfiniteTowerWaveFamilyLunar.GetComponent<CombatDirector>().monsterCards = dccsLunarFamily;
            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLunar;
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Moon";
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The bulwark begin to falter.";
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f,0.8f,1f);
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLunarFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLunar, weight = ITFamilyWaveWeight * 2f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarFamily);
            //
            //Void
            InfiniteTowerWaveFamilyVoid.GetComponent<CombatDirector>().monsterCards = dccsVoidFamily;
            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVoid;
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Void";
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The Void has become curious.";
            //InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.9f);
            //InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.6f, 0.9f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITVoidFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVoid, weight = ITFamilyWaveWeight*2f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVoidFamily);




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
                CombatDirector combatdirector = ITFamilyWaves[i].GetComponent<CombatDirector>();
                temp.baseCredits *= 1.26f;
                temp.immediateCreditsFraction = 0.45f;
                combatdirector.skipSpawnIfTooCheap = false;
            }

            GameObject[] ITFamilyUIs = {
                InfiniteTowerCurrentWaveUIFamilyBeetle, InfiniteTowerCurrentWaveUIFamilyGolem, InfiniteTowerCurrentWaveUIFamilyJellyfish,
                InfiniteTowerCurrentWaveUIFamilyWisp, InfiniteTowerCurrentWaveUIFamilyLemurian,
                InfiniteTowerCurrentWaveUIFamilyImp, InfiniteTowerCurrentWaveUIFamilyConstruct,
                InfiniteTowerCurrentWaveUIFamilyLarva, InfiniteTowerCurrentWaveUIFamilyMushroom,
                InfiniteTowerCurrentWaveUIFamilyParent };

            Texture2D texItFamilyBasic = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texItFamilyBasic.LoadImage(Properties.Resources.texItFamilyBasic, true);
            texItFamilyBasic.filterMode = FilterMode.Bilinear;
            Sprite texItFamilyBasicS = Sprite.Create(texItFamilyBasic, WRect.rec64, WRect.half);

            for (int i = 0; i < ITFamilyUIs.Length; i++)
            {
                //ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItFamilyBasicS;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;
            }
 

            Texture2D texITWaveGupIconBasic = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveGupIconBasic.LoadImage(Properties.Resources.texITWaveGupIconBasic, true);
            texITWaveGupIconBasic.filterMode = FilterMode.Bilinear;
            Sprite texITWaveGupIconBasicS = Sprite.Create(texITWaveGupIconBasic, WRect.rec64, WRect.half);

            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveGupIconBasicS;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;

            LGTFamily();
            //BossFamilyWave();
            DroneFamilyWave();
        }

        public static void DroneFamilyWave()
        {
            //Boss Boss Family
            GameObject InfiniteTowerWaveBossFamilyDrones = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyDrones", true);
            GameObject InfiniteTowerWaveBossDronesMachinesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyDronesUI", false);
            DirectorCardCategorySelection dccsITDrones = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

            InfiniteTowerWaveBossFamilyDrones.GetComponent<CombatDirector>().monsterCards = dccsITDrones;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;

            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().baseCredits = 320f;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.3f;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 10;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 20;
            InfiniteTowerWaveBossFamilyDrones.GetComponent<CombatDirector>().eliteBias = 0.5f;
            InfiniteTowerWaveBossFamilyDrones.AddComponent<SimulacrumExtrasHelper>().newRadius = 140;
            InfiniteTowerWaveBossFamilyDrones.AddComponent<SimuBuffWaveHelper>().variant = 1;

            InfiniteTowerWaveBossFamilyDrones.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossDronesMachinesUI;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Machines";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Drones and machines fight against you.";

            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;


            InfiniteTowerWaveCategory.WeightedWave ITBossDrones = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossFamilyDrones, weight = 6f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossDrones);

            ItemDef CaptainDefenseMatrix = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CaptainDefenseMatrix");
            ItemDef DroneWeaponsBoost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsBoost");
            ItemDef DroneWeaponsDisplay1 = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsDisplay1");
            ItemDef DroneWeaponsDisplay2 = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/DroneWeaponsDisplay2");
            ItemDef CutHp = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHp");
            ItemDef HalfSpeedDoubleHealth = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/DLC1/HalfSpeedDoubleHealth/HalfSpeedDoubleHealth.asset").WaitForCompletion();
            int damageDown = 75;
            int healthScaling = 50;
            int healthBoost = 10;
            int credits = 70;

            CharacterSpawnCard cscITDrone = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscITDrone.name = "cscITDrone1";
            cscITDrone.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/Drone1Master.prefab").WaitForCompletion();
            cscITDrone.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscITDrone.directorCreditCost = 40;
            cscITDrone.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = (int)(damageDown*1.15f) },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling/2 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost/2 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 50},
                new ItemCountPair { itemDef = HalfSpeedDoubleHealth, count = 1},
                new ItemCountPair { itemDef = CutHp, count = 1},
            };

            CharacterSpawnCard cscITDroneStriker = Object.Instantiate(cscITDrone);
            cscITDroneStriker.name = "cscITDroneStriker";
            cscITDroneStriker.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/DroneBackupMaster.prefab").WaitForCompletion();
            cscITDroneStriker.directorCreditCost = credits * 2;
            cscITDroneStriker.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = damageDown },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling},//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 0},//
            };

            CharacterSpawnCard cscITDroneFlame = Object.Instantiate(cscITDrone);
            cscITDroneFlame.name = "cscITDroneFlame";
            cscITDroneFlame.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/FlameDroneMaster.prefab").WaitForCompletion();
            cscITDroneFlame.directorCreditCost = 60;
            cscITDroneFlame.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = damageDown },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling },//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
            };

            CharacterSpawnCard cscITDroneMissile = Object.Instantiate(cscITDroneFlame);
            cscITDroneMissile.name = "cscITDroneMissile";
            cscITDroneMissile.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/DroneMissileMaster.prefab").WaitForCompletion();
            cscITDroneMissile.directorCreditCost = credits;
            cscITDroneMissile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = (int)(damageDown*1.1f) },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling  },//
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost},
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 33},//
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
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = (int)(damageDown*1.25f) },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling/4 },
                new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = healthBoost/4},
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 70},
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
            cscITDroneMega.itemsToGrant[1].count = healthScaling / 2;
            cscITDroneMega.itemsToGrant[2].count = healthBoost / 2;

            CharacterSpawnCard cscITDroneColDrone = Object.Instantiate(cscITDroneFlame);
            cscITDroneColDrone.name = "cscITDroneColDrone";
            cscITDroneColDrone.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DroneCommander/DroneCommanderMaster.prefab").WaitForCompletion();
            cscITDroneColDrone.directorCreditCost = 240;
            cscITDroneColDrone.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = (int)(damageDown*1.15f) },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = healthScaling*2 },//1
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
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 70 },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 300 },//1
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
            dccsITDrones.AddCard(0, new DirectorCard
            {
                spawnCard = cscITDroneToolbot,
                selectionWeight = 1,
            });
            //1
            dccsITDrones.AddCard(1, new DirectorCard 
            {
                spawnCard = cscITDroneFlame,
                selectionWeight = 5,
            });
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneEmergency,
                selectionWeight = 2,
            });
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
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneEngiTurret,
                selectionWeight = 2,
            });
            dccsITDrones.AddCard(1, new DirectorCard
            {
                spawnCard = cscITDroneMissile,
                selectionWeight = 4,
            });
            //2
            dccsITDrones.AddCard(2, new DirectorCard
            {
                spawnCard = cscITDrone,
                selectionWeight = 10,
            });
            dccsITDrones.AddCard(2, new DirectorCard
            {
                spawnCard = cscITDroneTurret,
                selectionWeight = 1,
            });



        }

        public static void BossFamilyWave()
        {
            //Boss Boss Family
            GameObject InfiniteTowerWaveBossFamilyBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyBosses", true);
            GameObject InfiniteTowerWaveBossFamilyBossUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyBossesUI", false);
            DirectorCardCategorySelection dccsBossesOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();

            InfiniteTowerWaveBossFamilyBoss.GetComponent<CombatDirector>().monsterCards = dccsBossesOnly;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;

            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().baseCredits = 600f;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.4f;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveBossFamilyBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;



            InfiniteTowerWaveBossFamilyBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossFamilyBossUI;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Champions";
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Only bosses may spawn.";
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerWaveBossFamilyBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;


            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossFamilyBoss, weight = 4, prerequisites = SimuMain.StartWave20Prerequisite };
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
                selectionWeight = 1,
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
            GameObject InfiniteTowerWaveFamilyClay = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyClay", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyClay = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyClay", false);

            GameObject InfiniteTowerWaveFamilyRoboBall = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyRoboBall", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyRoboBall = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyRoboBall", false);

            GameObject InfiniteTowerWaveFamilyVermin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyVermin", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyVermin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyVermin", false);

            GameObject BossInfiniteTowerWaveFamilyWorms = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFamilyWorms", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyWorms = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyWorms", false);

            //Modded
            //Clay
            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

            InfiniteTowerWaveFamilyClay.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyClay;
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Aphelia";
            InfiniteTowerCurrentWaveUIFamilyClay.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "You feel parasitic influences.";

            //RoboBall
            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyRoboBall.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyRoboBall;
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Solus X";
            InfiniteTowerCurrentWaveUIFamilyRoboBall.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The whirring of wings and machinery.";
            //
            //Vermin
            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveFamilyVermin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVermin;
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Vermin";
            InfiniteTowerCurrentWaveUIFamilyVermin.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "You have invaded rampant breeding grounds.";
            //
            //Worms
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWurms;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;

            BossInfiniteTowerWaveFamilyWorms.GetComponent<CombatDirector>().eliteBias = 0f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.5f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 4; //The director doesn't seem to really care
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().baseCredits = 750f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 15;
            BossInfiniteTowerWaveFamilyWorms.AddComponent<SimulacrumExtrasHelper>().newRadius = 160;

            EliteDef FireHonor = LegacyResourcesAPI.Load<EliteDef>("EliteDefs/FireHonor");
            EliteDef Lightning = LegacyResourcesAPI.Load<EliteDef>("EliteDefs/Lightning");
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{eliteDef = FireHonor, count = 1, spawnDistance = DirectorCore.MonsterSpawnDistance.Far, spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm")},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{eliteDef = Lightning, count = 1, spawnDistance = DirectorCore.MonsterSpawnDistance.Far, spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm")},
            };

            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyWorms;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Wurms";
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "His brother loved worms.";
            //
            //Do to All families post creation
            GameObject[] ITFamilyWaves = {InfiniteTowerWaveFamilyClay, InfiniteTowerWaveFamilyRoboBall, InfiniteTowerWaveFamilyVermin };

            for (int i = 0; i < ITFamilyWaves.Length; i++)
            {
                InfiniteTowerWaveController temp = ITFamilyWaves[i].GetComponent<InfiniteTowerWaveController>();
                CombatDirector combatdirector = ITFamilyWaves[i].GetComponent<CombatDirector>();
                temp.baseCredits *= 1.26f;
                temp.immediateCreditsFraction = 0.4f;
                //combatdirector.skipSpawnIfTooCheap = false;
            }


            Texture2D texItFamilyBasic = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texItFamilyBasic.LoadImage(Properties.Resources.texItFamilyBasic, true);
            texItFamilyBasic.filterMode = FilterMode.Bilinear;
            Sprite texItFamilyBasicS = Sprite.Create(texItFamilyBasic, WRect.rec64, WRect.half);

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
            FamilyDirectorCardCategorySelection[] FamilyDCCSs = UnityEngine.Object.FindObjectsOfType(typeof(RoR2.FamilyDirectorCardCategorySelection)) as RoR2.FamilyDirectorCardCategorySelection[];
            for (var i = 0; i < FamilyDCCSs.Length; i++)
            {
                //Debug.Log(FamilyDCCSs[i].name);
                switch (FamilyDCCSs[i].name)
                {
                    case "dccsClayFamily":
                        InfiniteTowerWaveFamilyClay.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        RoR2.InfiniteTowerWaveCategory.WeightedWave ITClayFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyClay, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITClayFamily);
                        break;
                    case "dccsRoboBallFamily":
                        InfiniteTowerWaveFamilyRoboBall.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        RoR2.InfiniteTowerWaveCategory.WeightedWave ITRoboBallFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyRoboBall, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITRoboBallFamily);
                        break;
                    case "dccsVerminFamily":
                        InfiniteTowerWaveFamilyVermin.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        RoR2.InfiniteTowerWaveCategory.WeightedWave ITVerminFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVermin, weight = ITFamilyWaveWeight };
                        SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVerminFamily);
                        break;
                    case "dccsWormsFamily":
                        BossInfiniteTowerWaveFamilyWorms.GetComponent<CombatDirector>().monsterCards = FamilyDCCSs[i];
                        RoR2.InfiniteTowerWaveCategory.WeightedWave ITWormsFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = BossInfiniteTowerWaveFamilyWorms, weight = 6f, prerequisites = SimuMain.StartWave40Prerequisite };
                        SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITWormsFamily);
                        break;
                }
            }




        }

    }
}