using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SimulacrumWavesFamily
    {
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
            //
            GameObject InfiniteTowerWaveFamilyLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyLunar", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyLunar", false);

            GameObject InfiniteTowerWaveFamilyVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFamilyVoid", true);
            GameObject InfiniteTowerCurrentWaveUIFamilyVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentWaveUIFamilyVoid", false);



            //

            float ITFamilyWaveWeight = 0.75f;
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
            InfiniteTowerWaveFamilyGup.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;

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

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITParentFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyParent, weight = ITFamilyWaveWeight };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITParentFamily);
            
            //
            //Lunar
            InfiniteTowerWaveFamilyLunar.GetComponent<CombatDirector>().monsterCards = dccsLunarFamily;
            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveFamilyLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyLunar;
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Moon";
            InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The bulwark begin to falter.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITLunarFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyLunar, weight = ITFamilyWaveWeight * 1.5f, prerequisites = SimuMain.Wave31OrGreaterPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarFamily);
            //
            //Void
            InfiniteTowerWaveFamilyVoid.GetComponent<CombatDirector>().monsterCards = dccsVoidFamily;
            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveFamilyVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentWaveUIFamilyVoid;
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Void";
            InfiniteTowerCurrentWaveUIFamilyVoid.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The Void has become curious.";

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITVoidFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveFamilyVoid, weight = ITFamilyWaveWeight*1.5f, prerequisites = SimuMain.Wave31OrGreaterPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVoidFamily);




            //Do to All families post creation
            GameObject[] ITFamilyWaves = {
                InfiniteTowerWaveFamilyBeetle, InfiniteTowerWaveFamilyGolem, InfiniteTowerWaveFamilyJellyfish,
                InfiniteTowerWaveFamilyWisp, InfiniteTowerWaveFamilyLemurian,
                InfiniteTowerWaveFamilyImp, InfiniteTowerWaveFamilyConstruct,
                InfiniteTowerWaveFamilyParent, InfiniteTowerWaveFamilyGup,
                InfiniteTowerWaveFamilyLunar,InfiniteTowerWaveFamilyVoid};

            for (int i = 0; i < ITFamilyWaves.Length; i++)
            {
                InfiniteTowerWaveController temp = ITFamilyWaves[i].GetComponent<InfiniteTowerWaveController>();
                CombatDirector combatdirector = ITFamilyWaves[i].GetComponent<CombatDirector>();
                temp.baseCredits *= 1.26f;
                temp.immediateCreditsFraction = 0.5f;
                combatdirector.skipSpawnIfTooCheap = false;
            }

            Color FamilyEventIconColor = new Color(1f, 0.8f, 0.7f, 1);
            Color FamilyEventOutlineColor = new Color(1f, 0.7f, 0.5f, 1);
            GameObject[] ITFamilyUIs = {
                InfiniteTowerCurrentWaveUIFamilyBeetle, InfiniteTowerCurrentWaveUIFamilyGolem, InfiniteTowerCurrentWaveUIFamilyJellyfish,
                InfiniteTowerCurrentWaveUIFamilyWisp, InfiniteTowerCurrentWaveUIFamilyLemurian,
                InfiniteTowerCurrentWaveUIFamilyImp, InfiniteTowerCurrentWaveUIFamilyConstruct,
                InfiniteTowerCurrentWaveUIFamilyParent };

            for (int i = 0; i < ITFamilyUIs.Length; i++)
            {
                ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;
            }
 

            Texture2D texITWaveGupIconBasic = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveGupIconBasic.LoadImage(Properties.Resources.texITWaveGupIconBasic, true);
            texITWaveGupIconBasic.filterMode = FilterMode.Bilinear;
            Sprite texITWaveGupIconBasicS = Sprite.Create(texITWaveGupIconBasic, WRect.rec64, WRect.half);

            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Color.white;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveGupIconBasicS;
            InfiniteTowerCurrentWaveUIFamilyGup.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;


        }




        public static void ModSupport()
        {
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
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0f;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 4; //The director doesn't seem to really care
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().baseCredits = 625;
            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            BossInfiniteTowerWaveFamilyWorms.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;

            BossInfiniteTowerWaveFamilyWorms.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{eliteDef = RoR2Content.Elites.FireHonor, count = 1, spawnDistance = DirectorCore.MonsterSpawnDistance.Far, spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm")},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{eliteDef = RoR2Content.Elites.LightningHonor, count = 1, spawnDistance = DirectorCore.MonsterSpawnDistance.Far, spawnCard = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm")},
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
                temp.immediateCreditsFraction = 0.5f;
                combatdirector.skipSpawnIfTooCheap = false;
            }

            Color FamilyEventIconColor = new Color(1f, 0.8f, 0.7f, 1);
            Color FamilyEventOutlineColor = new Color(1f, 0.7f, 0.5f, 1);
            GameObject[] ITFamilyUIs = {InfiniteTowerCurrentWaveUIFamilyClay, InfiniteTowerCurrentWaveUIFamilyRoboBall, InfiniteTowerCurrentWaveUIFamilyVermin };
            for (int i = 0; i < ITFamilyUIs.Length; i++)
            {
                ITFamilyUIs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = FamilyEventIconColor;
                ITFamilyUIs[i].transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = FamilyEventOutlineColor;
            }
            //
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= FamilyEventIconColor;
            InfiniteTowerCurrentWaveUIFamilyWorms.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= FamilyEventOutlineColor;
            //
            float ITFamilyWaveWeight = 0.75f;
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
                        RoR2.InfiniteTowerWaveCategory.WeightedWave ITWormsFamily = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = BossInfiniteTowerWaveFamilyWorms, weight = 3, prerequisites = SimuMain.Wave46OrGreaterPrerequisite };
                        SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITWormsFamily);
                        break;
                }
            }




        }

    }
}