﻿using RoR2;
//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SimulacrumWavesArtifacts
    {
        public static void Start()
        {
            GameObject InfiniteTowerWaveBossArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactEliteOnly", true);
            GameObject InfiniteTowerWaveBossArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDoppelganger", true);

            GameObject InfiniteTowerCurrentBossWaveUIArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactEliteOnly", false);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactDoppelganger", false);


            ArtifactDef ArtifactDefSingleMonster = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/SingleMonsterType");
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
            ArtifactDef ArtifactDefShadowClone = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/ShadowClone");

            //Honor Boss
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Honor";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are stronger, more numerous, and guaranteed to be Elite.";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactEliteOnly;

            InfiniteTowerWaveBossArtifactEliteOnly.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            //InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactEliteOnly, weight = 6, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossArtifactEliteOnly);

            //Vengence Boss
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Vengence";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your relentless doppelganger invades.";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefShadowClone.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactDoppelganger;

            InfiniteTowerWaveBossArtifactDoppelganger.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefShadowClone;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().baseCredits *= 0.8f;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.10f;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;
            InfiniteTowerWaveBossArtifactDoppelganger.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;


            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactDoppelganger = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactDoppelganger, weight = 8f }; //5 normally
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossArtifactDoppelganger);

            //When Metamorphosis Toggled Respawn Player, for the Augment
            //Wonder if this actually works on Severs
            RunArtifactManager.onArtifactEnabledGlobal += RunArtifactManager_onArtifactEnabledGlobal;
            RunArtifactManager.onArtifactDisabledGlobal += RunArtifactManager_onArtifactDisabledGlobal;

            //
            //Basic Evo
            GameObject InfiniteTowerWaveArtifactEvolution = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactEvolution", true);
            GameObject InfiniteTowerCurrentArtifactEvolutionWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactEvolutionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacEvolutionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefMonsterTeamGainsItems = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/MonsterTeamGainsItems");

            InfiniteTowerWaveArtifactEvolution.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefMonsterTeamGainsItems;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEvolutionWaveUI;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefMonsterTeamGainsItems.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Evolution";
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have additional items during this wave.";

            ArtifacEvolutionDisabledPrerequisite.bannedArtifact = ArtifactDefMonsterTeamGainsItems;
            ArtifacEvolutionDisabledPrerequisite.name = "ArtifacEvolutionDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEvolution = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEvolution, weight = 1, prerequisites = ArtifacEvolutionDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEvolution);
            //

            //Basic Sacrifice
            GameObject InfiniteTowerWaveArtifactSacrifice = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSacrifice", true);
            GameObject InfiniteTowerCurrentArtifactSacrificeWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactSacrificeWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSacrificeDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSacrifice = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Sacrifice");

            InfiniteTowerWaveArtifactSacrifice.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSacrifice;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSacrificeWaveUI;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().baseCredits = 200f; //Base Credits is 159 apparently

            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSacrifice.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Sacrifice";
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have a chance to drop items on death.";

            ArtifacSacrificeDisabledPrerequisite.bannedArtifact = ArtifactDefSacrifice;
            ArtifacSacrificeDisabledPrerequisite.name = "ArtifacSacrificeDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSacrifice = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSacrifice, weight = 1 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSacrifice);

            //
            //Basic Metamorphosis
            GameObject InfiniteTowerWaveArtifactRandomSurvivor = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactRandomSurvivor", true);
            GameObject InfiniteTowerCurrentArtifactRandomSurvivorWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactRandomSurvivorWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacRandomSurvivorDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefRandomSurvivor = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/RandomSurvivorOnRespawn");

            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefRandomSurvivor;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactRandomSurvivorWaveUI;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtAllTier;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefRandomSurvivor.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Metamorphosis";
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Play as a random survivor for this wave.";

            ArtifacRandomSurvivorDisabledPrerequisite.bannedArtifact = ArtifactDefRandomSurvivor;
            ArtifacRandomSurvivorDisabledPrerequisite.name = "ArtifacRandomSurvivorDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactRandomSurvivor = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRandomSurvivor, weight = 1f, prerequisites = ArtifacRandomSurvivorDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactRandomSurvivor);
            //
            //Basic Honor
            GameObject InfiniteTowerWaveArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentArtifactEliteOnlyWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactEliteOnlyWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacEliteOnlyDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            //ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnlyOnRespawn");

            InfiniteTowerWaveArtifactEliteOnly.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEliteOnlyWaveUI;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Honor";
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are guaranteed to be Elite.";

            ArtifacEliteOnlyDisabledPrerequisite.bannedArtifact = ArtifactDefEliteOnly;
            ArtifacEliteOnlyDisabledPrerequisite.name = "ArtifacEliteOnlyDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEliteOnly, weight = 1f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEliteOnly);
            //
            //
            GameObject InfiniteTowerWaveArtifactSwarm = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSwarm", true);
            GameObject InfiniteTowerCurrentArtifactSwarmWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactSwarmWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSwarmDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSwarm = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Swarms");

            InfiniteTowerWaveArtifactSwarm.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSwarm;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSwarmWaveUI;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2f;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSwarm.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Swarms";
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Double the monsters, but monster health is halved.";

            ArtifacSwarmDisabledPrerequisite.bannedArtifact = ArtifactDefSwarm;
            ArtifacSwarmDisabledPrerequisite.name = "ArtifacSwarmDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSwarm = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSwarm, weight = 1f, prerequisites = ArtifacSwarmDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSwarm);
            //
            //
            //Kin Boss
            GameObject InfiniteTowerWaveBossArtifactKin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactSingleMonsterType", true);
            GameObject InfiniteTowerWaveBossArtifactKinUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactSingleMonsterTypeUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactSingleMonsterTypeDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactSingleMonsterTypeDisabledPrerequisite.asset").WaitForCompletion();
  
            InfiniteTowerWaveBossArtifactKin.AddComponent<ArtifactEnabler>().artifactDef = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact;
            InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossArtifactKinUI;
            InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.2f;
            InfiniteTowerWaveBossArtifactKin.GetComponent<CombatDirector>().eliteBias = 0.1f;
            InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Kin";
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat a horde of the same monster.";
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossKin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactKin, weight = 6f, prerequisites = ArtifactSingleMonsterTypeDisabledPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossKin);
            //
            //
            //Dissonance Boss
            GameObject InfiniteTowerWaveBossArtifactDissonance = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDissonance", true);
            GameObject InfiniteTowerWaveBossArtifactDissonanceUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDissonanceUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactMixEnemyDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactMixEnemyDisabledPrerequisite.asset").WaitForCompletion();

            InfiniteTowerWaveBossArtifactDissonance.AddComponent<ArtifactEnabler>().artifactDef = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact;
            InfiniteTowerWaveBossArtifactDissonance.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossArtifactDissonanceUI;
            //InfiniteTowerWaveBossArtifactDissonance.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.1f;
            InfiniteTowerWaveBossArtifactDissonance.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtBossMixTier;

            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Dissonance";
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are stronger and appear outside their usual environments.";
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactDissonance, weight = 10f, prerequisites = ArtifactMixEnemyDisabledPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossDissonance);

        }


        public static void ModSupport()
        {
            ArtifactDef SS2Cognation = ArtifactCatalog.FindArtifactDef("Cognation");
            if (SS2Cognation)
            {
                GameObject InfiniteTowerWaveArtifacSS2Cognation = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifacSS2Cognation", true);
                GameObject InfiniteTowerCurrentArtifacSS2CognationWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifacSS2CognationWaveUI", false);

                InfiniteTowerWaveArtifacSS2Cognation.GetComponent<ArtifactEnabler>().artifactDef = SS2Cognation;
                InfiniteTowerWaveArtifacSS2Cognation.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifacSS2CognationWaveUI;
                InfiniteTowerWaveArtifacSS2Cognation.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;

                InfiniteTowerCurrentArtifacSS2CognationWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
                InfiniteTowerCurrentArtifacSS2CognationWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Cognation";
                InfiniteTowerCurrentArtifacSS2CognationWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies leave behind temporary duplciates on death.";

                InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacSS2Cognation = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifacSS2Cognation, weight = 1 };
                SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifacSS2Cognation);
            }

            //Will need to see what Empyrean Elites actually do
            EliteDef RainbowElite = null;
            if (RainbowElite)
            {
                GameObject InfiniteTowerWaveSS2RainbowElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2RainbowElites", true);
                GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

                InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
                InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
                InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.5f;
                InfiniteTowerWaveSS2RainbowElites.GetComponent<CombatDirector>().eliteBias = 0.25f;

                InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Empyrean";
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Empyrean.";
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f);
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.8f, 1f);
                InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.5f, 1f);

                InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 1 };
                //SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
            }
        }



        public static void RunArtifactManager_onArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0.25f));
                };
            };
        }

        public static void RunArtifactManager_onArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.SetBodyPrefabToPreference();
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0f));
                };
            };
        }

        public static IEnumerator DelayedRespawn(PlayerCharacterMasterController playerCharacterMasterController, float delay)
        {
            yield return new WaitForSeconds(delay);
            CharacterBody temp = playerCharacterMasterController.master.GetBody();
            if (temp)
            {
                playerCharacterMasterController.master.Respawn(temp.footPosition, temp.transform.rotation);
            }
            yield break;
        }

    }
}