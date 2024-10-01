using RoR2;
//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class Waves_Artifacts
    {
        public static void MakeWaves()
        {
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
 
            //When Metamorphosis Toggled Respawn Player, for the Augment
            //Wonder if this actually works on Severs
            RunArtifactManager.onArtifactEnabledGlobal += RespawnMetamorphosis_onArtifactEnabledGlobal;
            RunArtifactManager.onArtifactDisabledGlobal += RespawnMetamorphosis_onArtifactDisabledGlobal;

            #region Artifact of Evolution
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEvolution = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEvolution, weight = 2f, prerequisites = ArtifacEvolutionDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEvolution);
            #endregion
            #region Artifact of Sacrifice
            //Basic Sacrifice
            GameObject InfiniteTowerWaveArtifactSacrifice = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSacrifice", true);
            GameObject InfiniteTowerCurrentArtifactSacrificeWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactSacrificeWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSacrificeDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSacrifice = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Sacrifice");

            InfiniteTowerWaveArtifactSacrifice.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSacrifice;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSacrificeWaveUI;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().baseCredits = 159f; //Base Credits is 159 apparently

            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSacrifice.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Sacrifice";
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters have a chance to drop items on death.";

            ArtifacSacrificeDisabledPrerequisite.bannedArtifact = ArtifactDefSacrifice;
            ArtifacSacrificeDisabledPrerequisite.name = "ArtifacSacrificeDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSacrifice = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSacrifice, weight = 1.5f, prerequisites = ArtifacSacrificeDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSacrifice);
            #endregion
            #region Artifact of Metamorphosis
            //Basic Metamorphosis
            GameObject InfiniteTowerWaveArtifactRandomSurvivor = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactRandomSurvivor", true);
            GameObject InfiniteTowerCurrentArtifactRandomSurvivorWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactRandomSurvivorWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacRandomSurvivorDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefRandomSurvivor = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/RandomSurvivorOnRespawn");

            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefRandomSurvivor;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactRandomSurvivorWaveUI;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtAllTier;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().baseCredits = 160f;

            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefRandomSurvivor.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Metamorphosis";
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Play as a random survivor for this wave.";

            ArtifacRandomSurvivorDisabledPrerequisite.bannedArtifact = ArtifactDefRandomSurvivor;
            ArtifacRandomSurvivorDisabledPrerequisite.name = "ArtifacRandomSurvivorDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactRandomSurvivor = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRandomSurvivor, weight = 1f, prerequisites = ArtifacRandomSurvivorDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactRandomSurvivor);
            #endregion
            #region Artifact of Honor
            //Basic Honor
            GameObject InfiniteTowerWaveArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentArtifactEliteOnlyWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactEliteOnlyWaveUI", false);

            InfiniteTowerWaveArtifactEliteOnly.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEliteOnlyWaveUI;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;

            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Honor";
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are guaranteed to be Elite.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEliteOnly, weight = 1f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEliteOnly);
            #endregion
            #region Artifact of Swarms
            //
            GameObject InfiniteTowerWaveArtifactSwarm = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSwarm", true);
            GameObject InfiniteTowerCurrentArtifactSwarmWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactSwarmWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSwarmDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSwarm = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Swarms");

            InfiniteTowerWaveArtifactSwarm.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSwarm;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSwarmWaveUI;
            //InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.5f;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSwarm.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Swarms";
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Double the monsters, but monster health is halved.";

            ArtifacSwarmDisabledPrerequisite.bannedArtifact = ArtifactDefSwarm;
            ArtifacSwarmDisabledPrerequisite.name = "ArtifacSwarmDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSwarm = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSwarm, weight = 1f, prerequisites = ArtifacSwarmDisabledPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSwarm);
            #endregion
            //
            #region Artifact of Honor + Artifact of Brigade. (Here instead of mod support cuz instantiate clone of other wave)
            //Honor+Brigade
            GameObject InfiniteTowerWaveArtifactHonorAndBrigade = R2API.PrefabAPI.InstantiateClone(InfiniteTowerWaveArtifactEliteOnly, "InfiniteTowerWaveArtifactHonorAndBrigade", true);
            GameObject InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI = R2API.PrefabAPI.InstantiateClone(InfiniteTowerCurrentArtifactEliteOnlyWaveUI, "InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI", false);

            InfiniteTowerWaveArtifactHonorAndBrigade.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI;
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Brigade and Honor";
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies are elite and the same elite type.";
            GameObject.Instantiate(InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactHonorAndBrigade = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactHonorAndBrigade, weight = 0.75f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifactHonorAndBrigade);
            #endregion
            //
            #region Artifact of Devotion
            //Devotion Update
            GameObject InfiniteTowerWaveArtifactDevotion = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactDevotion", true);
            GameObject InfiniteTowerCurrentArtifactDevotionWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactDevotionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacDevotionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef LemurianArtifact = Addressables.LoadAssetAsync<ArtifactDef>(key: "RoR2/CU8/Devotion.asset").WaitForCompletion();
            InteractableSpawnCard iscLemurianEgg = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/CU8/LemurianEgg/iscLemurianEgg.asset").WaitForCompletion();

            InfiniteTowerWaveArtifactDevotion.GetComponent<ArtifactEnabler>().artifactDef = LemurianArtifact;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactDevotionWaveUI;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 2;

            InfiniteTowerWaveArtifactDevotion.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
 
            InfiniteTowerWaveArtifactDevotion.AddComponent<SimulacrumInteractablesWaveHelper>();
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnCard = iscLemurianEgg;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnsOnStart = 4;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnedTimer = 99999;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().interval = 99999;

            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = LemurianArtifact.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Devotion";
            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Lemurian Eggs appear. Hatch them with an item.";
 
            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactDevotion = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactDevotion, weight = 2f };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactDevotion);
            #endregion
            #region Artifact of Delusion
            //Delusion
            GameObject InfiniteTowerWaveArtifactDelusion = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactDelusion", true);
            GameObject InfiniteTowerCurrentArtifactDelusionWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentArtifactDelusionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacDelusionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef Delusion = Addressables.LoadAssetAsync<ArtifactDef>(key: "RoR2/CU8/Delusion.asset").WaitForCompletion();

            InfiniteTowerWaveArtifactDelusion.GetComponent<ArtifactEnabler>().artifactDef = Delusion;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactDelusionWaveUI;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveArtifactDelusion.AddComponent<RunArtifactOfDelusion>();

            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Delusion.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Delusion";
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Risk your items on previously opened chests in a test of memory.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactDelusion = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactDelusion, weight = 1f };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactDelusion);
            #endregion
            BossWaves();
            ModSupport();
        }

        public static void BossWaves()
        {
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
            ArtifactDef ArtifactDefShadowClone = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/ShadowClone");

            #region Artifact of Honor
            //Honor Boss
            GameObject InfiniteTowerWaveBossArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactEliteOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactEliteOnly", false);

            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Honor";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are stronger and guaranteed to be Elite.";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactEliteOnly;

            InfiniteTowerWaveBossArtifactEliteOnly.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;
            InfiniteTowerWaveBossArtifactEliteOnly.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            InfiniteTowerWaveBossArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 15;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveBossArtifactEliteOnly.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 40;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactEliteOnly, weight = 7f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossArtifactEliteOnly);
            #endregion
            #region Artifact of Vengence
            //Vengence Boss
            GameObject InfiniteTowerWaveBossArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDoppelganger", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactDoppelganger = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossWaveUIArtifactDoppelganger", false);

            //InfiniteTowerWaveBossArtifactDoppelganger.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefShadowClone;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().baseCredits *= 0.8f;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.0f;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveBossArtifactDoppelganger.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;

            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Vengeance";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your relentless doppelganger invades.";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefShadowClone.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            InfiniteTowerWaveBossArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactDoppelganger;

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITBossArtifactDoppelganger = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactDoppelganger, weight = 9f, prerequisites = SimuMain.StartWave11Prerequisite }; //5 normally
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossArtifactDoppelganger);
            #endregion
            #region Artifact of Kin
            //Kin Boss
            GameObject InfiniteTowerWaveBossArtifactKin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactSingleMonsterType", true);
            GameObject InfiniteTowerWaveBossArtifactKinUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactSingleMonsterTypeUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactSingleMonsterTypeDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactSingleMonsterTypeDisabledPrerequisite.asset").WaitForCompletion();

            InfiniteTowerWaveBossArtifactKin.AddComponent<ArtifactEnabler>().artifactDef = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact;
            InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossArtifactKinUI;
            //InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.1f;
            InfiniteTowerWaveBossArtifactKin.GetComponent<CombatDirector>().eliteBias = 0.1f;
            InfiniteTowerWaveBossArtifactKin.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Kin";
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat a horde of the same monster.";
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            InfiniteTowerWaveBossArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossKin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactKin, weight = 7f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossKin);
            #endregion     
            #region Artifact of Dissonace
            //Dissonance Boss
            GameObject InfiniteTowerWaveBossArtifactDissonance = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDissonance", true);
            GameObject InfiniteTowerWaveBossArtifactDissonanceUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossArtifactDissonanceUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactMixEnemyDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactMixEnemyDisabledPrerequisite.asset").WaitForCompletion();

            InfiniteTowerWaveBossArtifactDissonance.AddComponent<ArtifactEnabler>().artifactDef = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact;
            InfiniteTowerWaveBossArtifactDissonance.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossArtifactDissonanceUI;
            InfiniteTowerWaveBossArtifactDissonance.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;

            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Dissonance";
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are stronger and appear outside their usual environments.";
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            InfiniteTowerWaveBossArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossArtifactDissonance, weight = 10f, prerequisites = ArtifactMixEnemyDisabledPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossDissonance);
            #endregion
            #region Artifact of "Dissonance" (All enemy "family")
            //
            //All enemies "Family"
            GameObject InfiniteTowerWaveBossAllEnemiesAvailable = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossAllEnemiesAvailable", true);
            GameObject InfiniteTowerWaveBossAllEnemiesAvailableUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossAllEnemiesAvailableUI", false);

            InfiniteTowerWaveBossAllEnemiesAvailable.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/MixEnemy/dccsMixEnemy.asset").WaitForCompletion();
            InfiniteTowerWaveBossAllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossAllEnemiesAvailableUI;
            InfiniteTowerWaveBossAllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;
            InfiniteTowerWaveBossAllEnemiesAvailable.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossAllEnemiesAvailable.AddComponent<SimulacrumEliteWaves>().addLunar = true;

            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Dissonance?";
            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies are allowed to spawn.";
            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1) * Waves_Family.FamilyEventIconColor;
            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= Waves_Family.FamilyEventIconColor;
            InfiniteTowerWaveBossAllEnemiesAvailableUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= Waves_Family.FamilyEventOutlineColor;

            InfiniteTowerWaveCategory.WeightedWave ITBossNotDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossAllEnemiesAvailable, weight = 4f };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossNotDissonance);
            #endregion
            #region 2 Artifacts
            GameObject InfiniteTowerWaveBossDoubleArtifact = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossDoubleArtifact", true);
            GameObject InfiniteTowerWaveBossDoubleArtifactUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossDoubleArtifactUI", false);

            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0f;

            InfiniteTowerWaveBossDoubleArtifact.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveBossDoubleArtifact.AddComponent<SimulacrumArtifactTrialWave>();
            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossBonusRed;

            Color ArtifactBoss = new Color(1.2f, 0.8f, 1.2f);

            InfiniteTowerWaveBossDoubleArtifact.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossDoubleArtifactUI;
            InfiniteTowerWaveBossDoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Artifacts";
            InfiniteTowerWaveBossDoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "2 random Artifacts are enabled.";
            InfiniteTowerWaveBossDoubleArtifactUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = ArtifactBoss;
            InfiniteTowerWaveBossDoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= ArtifactBoss;
            InfiniteTowerWaveBossDoubleArtifactUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= ArtifactBoss;

            InfiniteTowerWaveCategory.WeightedWave ITBossTwoArtifactsSemi = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossDoubleArtifact, weight = 6f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossTwoArtifactsSemi);
            #endregion
        }


        public static void ModSupport()
        {
            #region StarStorm2 Artifact of Cognation
            //SS2 Cognation
            GameObject InfiniteTowerWaveArtifactSS2Cognation = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSS2Cognation", true);
            GameObject InfiniteTowerWaveArtifactSS2CognationUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactSS2CognationUI", false);

            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactSS2CognationUI;
            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;

            //InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Cognation";
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies leave behind temporary duplciates on death.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacSS2Cognation = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSS2Cognation, weight = 1.5f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacSS2Cognation);
            #endregion
            //
            //
            //ZET Tossing
            GameObject InfiniteTowerWaveArtifactZetTossing = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactZetTossing", true);
            GameObject InfiniteTowerWaveArtifactZetTossingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactZetTossingUI", false);

            InfiniteTowerWaveArtifactZetTossing.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactZetTossingUI;
            InfiniteTowerWaveArtifactZetTossing.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;

            //InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ZetTossing.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Tossing";
            InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Players can drop and scrap their items.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacZetTossing = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactZetTossing, weight = 1f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacZetTossing);
            //
            //
            //Risky Origin
            GameObject InfiniteTowerWaveArtifactRiskyOrigin = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactRiskyOrigin", true);
            GameObject InfiniteTowerWaveArtifactRiskyOriginUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactRiskyOriginUI", false);

            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactRiskyOriginUI;
            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;

            //InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = RiskyOrigin.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Origin";
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Boss monsters invade the simulation.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacRiskyOrigin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRiskyOrigin, weight = 1f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacRiskyOrigin);
        }



        public static void RespawnMetamorphosis_onArtifactEnabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.StopAllCoroutines();
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0.35f));
                };
            };
        }

        public static void RespawnMetamorphosis_onArtifactDisabledGlobal(RunArtifactManager runArtifactManager, ArtifactDef artifactDef)
        {
            if (artifactDef == RoR2Content.Artifacts.randomSurvivorOnRespawnArtifactDef)
            {
                foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
                {
                    playerCharacterMasterController.SetBodyPrefabToPreference();
                    playerCharacterMasterController.StopAllCoroutines();
                    playerCharacterMasterController.StartCoroutine(DelayedRespawn(playerCharacterMasterController, 0.1f));
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