using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;
using static SimulacrumAdditions.Const;

namespace SimulacrumAdditions
{
    public class Waves_Vanilla
    {
        internal static void MakeChanges()
        {
            GameObject InfiniteTowerWaveArtifactEnigma = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactEnigma.prefab").WaitForCompletion();
            WavesMain.orangeWaves.Add(InfiniteTowerWaveArtifactEnigma);
            InfiniteTowerWaveArtifactEnigma.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITSpecialEquipment;
            InfiniteTowerWaveArtifactEnigma.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;


            float ITSpecialBossWaveWeight = 2.5f;
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                switch (ITBasicWaves.wavePrefabs[i].wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactCommand":
                        ITBasicWaves.wavePrefabs[i].weight = 1.5f;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        ITBasicWaves.wavePrefabs[i].weight = 3.5f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 184;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                        ITBasicWaves.wavePrefabs[i].weight = 2.5f;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        ITBasicWaves.wavePrefabs[i].weight = 2f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtAllTier;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        ITBasicWaves.wavePrefabs[i].weight = 4f;
                        ITBasicWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                };
            }


            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ENIGMA";
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactSingleMonsterTypeWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_KIN";

            FamilyDirectorCardCategorySelection dccsLunarFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion();

            ITBasicWaves.wavePrefabs[0].weight = BasicWaveWeight;
            ITBossWaves.wavePrefabs[0].weight = BasicBossWaveWight;

            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveVoid"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 6f;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveLunar"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 5f;
                    CombatDirector temp = ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<RoR2.CombatDirector>();
                    temp.monsterCards = dccsLunarFamily;
                    temp.skipSpawnIfTooCheap = false;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier2;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;

                    ITBossWaves.wavePrefabs[i].wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITLunar;
                    ITBossWaves.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveScav"))
                {
                    ITBossWaves.wavePrefabs[i].weight = 6f;
                    ITBossWaves.wavePrefabs[i].prerequisites = StartWave25Prerequisite;
                    ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
                else if (ITBossWaves.wavePrefabs[i].wavePrefab.name.Equals("InfiniteTowerWaveBrother"))
                {
                    ITBossWaves.wavePrefabs[i].weight = ITSpecialBossWaveWeight;
                    ITBossWaves.wavePrefabs[i].prerequisites = StartWave35Prerequisite;
                    ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
            }

        }

    }

}