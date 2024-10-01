using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_SizeChanging
    {
 
        internal static void MakeWaves()
        {
            Texture2D texITWaveSizeSmall = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveSizeSmall.LoadImage(Properties.Resources.texITWaveSizeSmall, true);
            texITWaveSizeSmall.filterMode = FilterMode.Bilinear;
            Sprite texITWaveSizeSmallS = Sprite.Create(texITWaveSizeSmall, WRect.rec64, WRect.half);

            Texture2D texITWaveSizeBig = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveSizeBig.LoadImage(Properties.Resources.texITWaveSizeBig, true);
            texITWaveSizeBig.filterMode = FilterMode.Bilinear;
            Sprite texITWaveSizeBigS = Sprite.Create(texITWaveSizeBig, WRect.rec64, WRect.half);


            //SizeBigEnemies Buff
            GameObject InfiniteTowerWaveSizeBigEnemies = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeBigEnemies", true);
            GameObject InfiniteTowerWaveSizeBigEnemiesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeBigEnemiesUI", false);

            InfiniteTowerWaveSizeBigEnemies.AddComponent<SimuWaveSizeModifier>().sizeModifier = 1.65f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeBigEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITHealthScaling";
            simulacrumGiveItemsOnStart.count = 65;
            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().baseCredits = 130;

            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSizeBigEnemiesUI;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gigantism";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are larger.";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeBigS;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeBigEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeBigEnemies, weight = 2f, prerequisites = SimuMain.AfterWave5Prerequisite };


            //SizeSmallEnemies
            GameObject InfiniteTowerWaveSizeSmallEnemies = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeSmallEnemies", true);
            GameObject InfiniteTowerWaveSizeSmallEnemiesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeSmallEnemiesUI", false);

            InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimuWaveSizeModifier>().sizeModifier = 0.65f;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "CutHp";
            simulacrumGiveItemsOnStart.count = 1;
            simulacrumGiveItemsOnStart.hideItem = true;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITHealthScaling";
            simulacrumGiveItemsOnStart.count = 30;

            InfiniteTowerWaveSizeSmallEnemies.GetComponent<InfiniteTowerWaveController>().baseCredits = 180;

            InfiniteTowerWaveSizeSmallEnemies.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSizeSmallEnemiesUI;
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Smallness";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are tiny.";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeSmallS;
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeSmallEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeSmallEnemies, weight = 1f, prerequisites = SimuMain.AfterWave5Prerequisite };

            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SizeBigEnemiesWave, SizeSmallEnemiesWave);
        }


    }

}