using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;
using static SimulacrumAdditions.Constant;

namespace SimulacrumAdditions.Waves
{
    public class Waves_Vanilla
    {
        internal static void MakeChanges()
        {
            GameObject InfiniteTowerWaveArtifactEnigma = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerWaveArtifactEnigma.prefab").WaitForCompletion();
            WavesMain.orangeWaves.Add(InfiniteTowerWaveArtifactEnigma);
            InfiniteTowerWaveArtifactEnigma.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITSpecialEquipment;
            InfiniteTowerWaveArtifactEnigma.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
            InfiniteTowerWaveArtifactEnigma.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.Enigma;


            float ITSpecialBossWaveWeight = 2.5f;
            for (int i = 0; i < ITBasicWaves.wavePrefabs.Length; i++)
            {
                var wav = ITBasicWaves.wavePrefabs[i];
                switch (wav.wavePrefab.name)
                {
                    case "InfiniteTowerWaveArtifactCommand":
                        wav.weight = 1.5f;
                        break;
                    case "InfiniteTowerWaveArtifactMixEnemy":
                        wav.weight = 3.5f;
                            wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITRainbow;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 184;
                        break;
                    case "InfiniteTowerWaveArtifactBomb":
                    case "InfiniteTowerWaveArtifactWispOnDeath":
                        wav.weight = 2.5f;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = dtITBasicWaveOnKill;
                        break;
                    case "InfiniteTowerWaveArtifactStatsOnLowHealth":
                    case "InfiniteTowerWaveArtifactSingleMonsterType":
                            wav.weight = 2.5f;
                        break;
                    case "InfiniteTowerWaveArtifactRandomLoadout":
                        wav.weight = 2f;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITRainbow;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                    case "InfiniteTowerWaveArtifactSingleEliteType":
                        wav.weight = 4f;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits = 159;
                        break;
                };
            }


            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_ENIGMA";
            Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentArtifactSingleMonsterTypeWaveUI.prefab").WaitForCompletion().transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_KIN";
 
            ITBasicWaves.wavePrefabs[0].weight = BasicWaveWeight;
            ITBossWaves.wavePrefabs[0].weight = BasicBossWaveWight;

            for (int i = 0; i < ITBossWaves.wavePrefabs.Length; i++)
            {
                var wav = ITBossWaves.wavePrefabs[i];
                if (wav.wavePrefab.name.Equals("InfiniteTowerWaveBossVoid"))
                {
                    wav.weight = 6f;
                }
                else if (wav.wavePrefab.name.Equals("InfiniteTowerWaveBossLunar"))
                {
                    wav.weight = 5f;
                    CombatDirector temp = wav.wavePrefab.GetComponent<CombatDirector>();
                    temp.monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/DirectorCardCategorySelections/dccsLunarFamily.asset").WaitForCompletion(); ;
                    temp.skipSpawnIfTooCheap = false;
                        wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier2;
                    wav.wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;

                    wav.wavePrefab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITLunar;
                    wav.wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                }
                else if (wav.wavePrefab.name.Equals("InfiniteTowerWaveBossScav"))
                {
                    wav.weight = 6f;
                    wav.prerequisites = StartWave25Prerequisite;
                    ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
                else if (wav.wavePrefab.name.Equals("InfiniteTowerWaveBossBrother"))
                {
                    wav.wavePrefab.AddComponent<PhaseCounter>().phase = 3;
                    wav.weight = ITSpecialBossWaveWeight;
                    wav.prerequisites = StartWave35Prerequisite;
                    ITSuperBossWaves.wavePrefabs.Add(ITBossWaves.wavePrefabs[i]);
                }
            }

        }

    }

}