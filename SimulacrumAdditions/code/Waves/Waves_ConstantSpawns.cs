﻿using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_ConstantlySpawning
    {
 
        internal static void MakeWaves()
        {
            BuffDef BanditSkull = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/DeathMark");
            //
            GameObject InfiniteTowerWaveSulfurPods = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSulfurPods", true);
            GameObject InfiniteTowerWaveSulfurPodsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSulfurPodsUI", false);

            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            InteractableSpawnCard cscITSulfurPod = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            cscITSulfurPod.name = "cscITSulfurPod";
            cscITSulfurPod.slightlyRandomizeOrientation = false;
            cscITSulfurPod.orientToFloor = false;
            cscITSulfurPod.directorCreditCost = 1;
            cscITSulfurPod.hullSize = HullClassification.Human;
            cscITSulfurPod.sendOverNetwork = true;
            cscITSulfurPod.skipSpawnWhenSacrificeArtifactEnabled = false;
            cscITSulfurPod.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITSulfurPod.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/SulfurPodBody");

            SimulacrumInteractablesWaveHelper waveHelper = InfiniteTowerWaveSulfurPods.AddComponent<SimulacrumInteractablesWaveHelper>();
            waveHelper.spawnCard = cscITSulfurPod;
            waveHelper.interval = 1f;
            waveHelper.spawnsOnStart = 7;

            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSulfurPodsUI;
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Sulfur";
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Sulfur pods keep growing.";
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7882f, 0.949f, 0.302f, 1);
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7882f, 0.949f, 0.302f, 1);
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7882f, 0.949f, 0.302f, 1) * 0.8f;

            InfiniteTowerWaveCategory.WeightedWave ITSulfurPods = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSulfurPods, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITSulfurPods);
            //
            GameObject InfiniteTowerWaveTarPots = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveTarPots", true);
            GameObject InfiniteTowerWaveTarPotsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveTarPotsUI", false);

            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            InteractableSpawnCard cscITTarPot = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            cscITTarPot.name = "cscITTarPot";
            cscITTarPot.directorCreditCost = 1;
            cscITTarPot.hullSize = HullClassification.Human;
            cscITTarPot.sendOverNetwork = true;
            cscITTarPot.skipSpawnWhenSacrificeArtifactEnabled = false;
            cscITTarPot.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscITTarPot.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ExplosivePotDestructibleBody");

            waveHelper = InfiniteTowerWaveTarPots.AddComponent<SimulacrumInteractablesWaveHelper>();
            waveHelper.spawnCard = cscITTarPot;
            waveHelper.interval = 0.8f;
            waveHelper.spawnsOnStart = 2;

            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveTarPotsUI;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Tar";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It's raining pots of tar.";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1) * 2; //0.2 0.0902 0.0902 1
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.0902f, 0.0902f, 1) * 2;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITTarPots = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveTarPots, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITTarPots);
        }


    }

}