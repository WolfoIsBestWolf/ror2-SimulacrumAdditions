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
            Sprite texITWaveSizeSmallS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveSMALL.png"), WRect.rec64, WRect.half);

            Sprite texITWaveSizeBigS = Sprite.Create(Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Wave/waveBIG.png"), WRect.rec64, WRect.half);


            //SizeBigEnemies Buff
            GameObject InfiniteTowerWaveSizeBigEnemies = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveSizeBigEnemies", true);
            GameObject InfiniteTowerWaveSizeBigEnemiesUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveSizeBigEnemiesUI", false);

            InfiniteTowerWaveSizeBigEnemies.AddComponent<SimuWaveSizeModifier>().sizeModifier = 1.65f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeBigEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITHealthScaling";
            simulacrumGiveItemsOnStart.count = 65;
            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().baseCredits = 130;

            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSizeBigEnemiesUI;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BIG";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BIG";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeBigS;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeBigEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeBigEnemies, weight = 2f, prerequisites = SimuMain.AfterWave5Prerequisite };


            //SizeSmallEnemies
            GameObject InfiniteTowerWaveSizeSmallEnemies = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveSizeSmallEnemies", true);
            GameObject InfiniteTowerWaveSizeSmallEnemiesUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWaveSizeSmallEnemiesUI", false);

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
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SMALL";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_SMALL";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeSmallS;
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeSmallEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeSmallEnemies, weight = 1f, prerequisites = SimuMain.AfterWave5Prerequisite };

            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SizeBigEnemiesWave, SizeSmallEnemiesWave);
        }


    }

}