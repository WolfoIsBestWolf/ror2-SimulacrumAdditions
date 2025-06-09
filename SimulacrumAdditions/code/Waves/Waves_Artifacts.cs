using RoR2;
//using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

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
            GameObject InfiniteTowerWaveArtifactEvolution = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactEvolution", true);
            GameObject InfiniteTowerCurrentArtifactEvolutionWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactEvolutionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacEvolutionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefMonsterTeamGainsItems = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/MonsterTeamGainsItems");

            InfiniteTowerWaveArtifactEvolution.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefMonsterTeamGainsItems;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEvolutionWaveUI;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier1;
            InfiniteTowerWaveArtifactEvolution.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefMonsterTeamGainsItems.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_EVOLUTION";
            InfiniteTowerCurrentArtifactEvolutionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_EVOLUTION";

            ArtifacEvolutionDisabledPrerequisite.bannedArtifact = ArtifactDefMonsterTeamGainsItems;
            ArtifacEvolutionDisabledPrerequisite.name = "ArtifacEvolutionDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEvolution = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEvolution, weight = 2f, prerequisites = ArtifacEvolutionDisabledPrerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEvolution);
            #endregion
            #region Artifact of Sacrifice
            //Basic Sacrifice
            GameObject InfiniteTowerWaveArtifactSacrifice = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactSacrifice", true);
            GameObject InfiniteTowerCurrentArtifactSacrificeWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactSacrificeWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSacrificeDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSacrifice = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Sacrifice");

            InfiniteTowerWaveArtifactSacrifice.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSacrifice;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSacrificeWaveUI;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicWaveOnKill;
            InfiniteTowerWaveArtifactSacrifice.GetComponent<InfiniteTowerWaveController>().baseCredits = 159f; //Base Credits is 159 apparently

            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSacrifice.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SACRIFICE";
            InfiniteTowerCurrentArtifactSacrificeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_SACRIFICE";

            ArtifacSacrificeDisabledPrerequisite.bannedArtifact = ArtifactDefSacrifice;
            ArtifacSacrificeDisabledPrerequisite.name = "ArtifacSacrificeDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSacrifice = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSacrifice, weight = 1.5f, prerequisites = ArtifacSacrificeDisabledPrerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSacrifice);
            #endregion
            #region Artifact of Metamorphosis
            //Basic Metamorphosis
            GameObject InfiniteTowerWaveArtifactRandomSurvivor = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactRandomSurvivor", true);
            GameObject InfiniteTowerCurrentArtifactRandomSurvivorWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactRandomSurvivorWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacRandomSurvivorDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefRandomSurvivor = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/RandomSurvivorOnRespawn");

            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefRandomSurvivor;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactRandomSurvivorWaveUI;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtAllTier;
            InfiniteTowerWaveArtifactRandomSurvivor.GetComponent<InfiniteTowerWaveController>().baseCredits = 160f;

            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefRandomSurvivor.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_METAMORPH";
            InfiniteTowerCurrentArtifactRandomSurvivorWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_METAMORPH";

            ArtifacRandomSurvivorDisabledPrerequisite.bannedArtifact = ArtifactDefRandomSurvivor;
            ArtifacRandomSurvivorDisabledPrerequisite.name = "ArtifacRandomSurvivorDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactRandomSurvivor = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRandomSurvivor, weight = 1f, prerequisites = ArtifacRandomSurvivorDisabledPrerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactRandomSurvivor);
            #endregion
            #region Artifact of Honor
            //Basic Honor
            GameObject InfiniteTowerWaveArtifactEliteOnly = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentArtifactEliteOnlyWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactEliteOnlyWaveUI", false);

            InfiniteTowerWaveArtifactEliteOnly.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactEliteOnlyWaveUI;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicBonusGreen;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;

            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_HONOR";
            InfiniteTowerCurrentArtifactEliteOnlyWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_HONOR";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactEliteOnly, weight = 1f, prerequisites = Const.StartWave11Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactEliteOnly);
            #endregion
            #region Artifact of Swarms
            //
            GameObject InfiniteTowerWaveArtifactSwarm = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactSwarm", true);
            GameObject InfiniteTowerCurrentArtifactSwarmWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactSwarmWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacSwarmDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef ArtifactDefSwarm = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Swarms");

            InfiniteTowerWaveArtifactSwarm.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSwarm;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactSwarmWaveUI;
            //InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.5f;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier1;
            InfiniteTowerWaveArtifactSwarm.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;

            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefSwarm.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_SWARMS";
            InfiniteTowerCurrentArtifactSwarmWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ARTIFACT_SWARMS_DESCRIPTION";

            ArtifacSwarmDisabledPrerequisite.bannedArtifact = ArtifactDefSwarm;
            ArtifacSwarmDisabledPrerequisite.name = "ArtifacSwarmDisabledPrerequisite";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactSwarm = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSwarm, weight = 1f, prerequisites = ArtifacSwarmDisabledPrerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactSwarm);
            #endregion
            //
            #region Artifact of Honor + Artifact of Brigade. (Here instead of mod support cuz instantiate clone of other wave)
            //Honor+Brigade
            GameObject InfiniteTowerWaveArtifactHonorAndBrigade = PrefabAPI.InstantiateClone(InfiniteTowerWaveArtifactEliteOnly, "InfiniteTowerWaveArtifactHonorAndBrigade", true);
            GameObject InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI = PrefabAPI.InstantiateClone(InfiniteTowerCurrentArtifactEliteOnlyWaveUI, "InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI", false);

            InfiniteTowerWaveArtifactHonorAndBrigade.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI;
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_HONORBRIGADE";
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_HONORBRIGADE";
            GameObject.Instantiate(InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactHonorAndBrigade = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactHonorAndBrigade, weight = 0.75f, prerequisites = Const.StartWave11Prerequisite };
            Const.ITModSupportWaves.wavePrefabs = Const.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifactHonorAndBrigade);
            #endregion
            //
            #region Artifact of Devotion
            //Devotion Update
            GameObject InfiniteTowerWaveArtifactDevotion = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactDevotion", true);
            GameObject InfiniteTowerCurrentArtifactDevotionWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactDevotionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacDevotionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef LemurianArtifact = Addressables.LoadAssetAsync<ArtifactDef>(key: "RoR2/CU8/Devotion.asset").WaitForCompletion();
            InteractableSpawnCard iscLemurianEgg = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/CU8/LemurianEgg/iscLemurianEgg.asset").WaitForCompletion();

            InfiniteTowerWaveArtifactDevotion.GetComponent<ArtifactEnabler>().artifactDef = LemurianArtifact;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactDevotionWaveUI;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 2;

            InfiniteTowerWaveArtifactDevotion.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITWaveTier1;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveArtifactDevotion.AddComponent<SimulacrumInteractablesWaveHelper>().maxDistance = 20;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnCard = iscLemurianEgg;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnsOnStart = 4;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().spawnedTimer = 99999;
            InfiniteTowerWaveArtifactDevotion.GetComponent<SimulacrumInteractablesWaveHelper>().interval = 99999;

            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = LemurianArtifact.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DEVOTION";
            InfiniteTowerCurrentArtifactDevotionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DEVOTION";
 
            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactDevotion = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactDevotion, weight = 2f };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactDevotion);
            #endregion
            #region Artifact of Delusion
            //Artifact does not care to remember if it is not active.
            //Not really any easy way around this so removing it.
            /*
            //Delusion
            GameObject InfiniteTowerWaveArtifactDelusion = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactDelusion", true);
            GameObject InfiniteTowerCurrentArtifactDelusionWaveUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerCurrentArtifactDelusionWaveUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifacDelusionDisabledPrerequisite = ScriptableObject.CreateInstance<RoR2.InfiniteTowerWaveArtifactPrerequisites>();
            ArtifactDef Delusion = Addressables.LoadAssetAsync<ArtifactDef>(key: "RoR2/CU8/Delusion.asset").WaitForCompletion();

            InfiniteTowerWaveArtifactDelusion.GetComponent<ArtifactEnabler>().artifactDef = Delusion;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactDelusionWaveUI;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveArtifactDelusion.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            //InfiniteTowerWaveArtifactDelusion.AddComponent<RunArtifactOfDelusion>();

            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Delusion.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DELUSION";
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DELUSION";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactDelusion = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactDelusion, weight = 1f };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactDelusion);
            */
            #endregion

         BossWaves();
        }

        public static void BossWaves()
        {
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
            ArtifactDef ArtifactDefShadowClone = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/ShadowClone");

            #region Artifact of Honor
            //Honor Boss
            GameObject WaveBoss_ArtifactEliteOnly = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_ArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactEliteOnly = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerCurrentBossWaveUIArtifactEliteOnly", false);

            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_HONOR";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_HONOR";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactEliteOnly;

            WaveBoss_ArtifactEliteOnly.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.1f;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITSpecialBossYellow;
            WaveBoss_ArtifactEliteOnly.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 15;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = WaveBoss_ArtifactEliteOnly.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 40;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveCategory.WeightedWave ITBossArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactEliteOnly, weight = 7f, prerequisites = Const.StartWave20Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossArtifactEliteOnly);
            #endregion
            #region Artifact of Vengence
            //Vengence Boss
            GameObject WaveBoss_ArtifactDoppelganger = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_ArtifactDoppelganger", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactDoppelganger = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerCurrentBossWaveUIArtifactDoppelganger", false);

            //WaveBoss_ArtifactDoppelganger.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefShadowClone;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().baseCredits *= 0.8f;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.0f;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITWaveTier2;
            WaveBoss_ArtifactDoppelganger.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;

            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_VENGENCE";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_VENGENCE";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactDefShadowClone.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactDoppelganger;

            InfiniteTowerWaveCategory.WeightedWave ITBossArtifactDoppelganger = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactDoppelganger, weight = 9f, prerequisites = Const.StartWave11Prerequisite }; //5 normally
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossArtifactDoppelganger);
            #endregion
            #region Artifact of Kin
            //Kin Boss
            GameObject WaveBoss_ArtifactKin = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_ArtifactSingleMonsterType", true);
            GameObject WaveBoss_ArtifactKinUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "WaveBoss_ArtifactSingleMonsterTypeUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactSingleMonsterTypeDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactSingleMonsterTypeDisabledPrerequisite.asset").WaitForCompletion();

            WaveBoss_ArtifactKin.AddComponent<ArtifactEnabler>().artifactDef = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact;
            WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_ArtifactKinUI;
            //WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.1f;
            WaveBoss_ArtifactKin.GetComponent<CombatDirector>().eliteBias = 0.1f;
            WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_KIN";
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_KIN";
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossKin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactKin, weight = 7f, prerequisites = Const.StartWave11Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossKin);
            #endregion     
            #region Artifact of Dissonace
            //Dissonance Boss
            GameObject WaveBoss_ArtifactDissonance = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_ArtifactDissonance", true);
            GameObject WaveBoss_ArtifactDissonanceUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "WaveBoss_ArtifactDissonanceUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactMixEnemyDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/ArtifactMixEnemyDisabledPrerequisite.asset").WaitForCompletion();

            WaveBoss_ArtifactDissonance.AddComponent<ArtifactEnabler>().artifactDef = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact;
            WaveBoss_ArtifactDissonance.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_ArtifactDissonanceUI;
            WaveBoss_ArtifactDissonance.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossGreenVoid;

            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DISSONANCE";
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DISSONANCE";
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactDissonance, weight = 10f, prerequisites = ArtifactMixEnemyDisabledPrerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossDissonance);
            #endregion
            #region Artifact of "Dissonance" (All enemy "family")
            //
            //All enemies "Family"
            GameObject WaveBoss_AllEnemiesAvailable = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_AllEnemiesAvailable", true);
            GameObject WaveBoss_AllEnemiesAvailableUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "WaveBoss_AllEnemiesAvailableUI", false);

            WaveBoss_AllEnemiesAvailable.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/MixEnemy/dccsMixEnemy.asset").WaitForCompletion();
            WaveBoss_AllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_AllEnemiesAvailableUI;
            WaveBoss_AllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossGreenVoid;
            WaveBoss_AllEnemiesAvailable.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_AllEnemiesAvailable.AddComponent<SimulacrumEliteWaves>().addLunar = true;

            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DISSOFAKE";
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DISSOFAKE";
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.8f, 0.8f, 1) * Waves_Family.FamilyEventIconColor;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= Waves_Family.FamilyEventIconColor;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= Waves_Family.FamilyEventOutlineColor;

            InfiniteTowerWaveCategory.WeightedWave ITBossNotDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_AllEnemiesAvailable, weight = 4f };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossNotDissonance);
            #endregion
            #region 2 Artifacts
            GameObject WaveBoss_DoubleArtifact = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_DoubleArtifact", true);
            GameObject WaveBoss_DoubleArtifactUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "WaveBoss_DoubleArtifactUI", false);

            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0f;

            WaveBoss_DoubleArtifact.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            WaveBoss_DoubleArtifact.AddComponent<SimulacrumArtifactTrialWave>();
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossBonusRed;

            Color ArtifactBoss = new Color(1.2f, 0.8f, 1.2f);
            Color ArtifactBoss2 = new Color(1f, 0.7f, 0.8f, 1f);

            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_DoubleArtifactUI;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_ARTIFACTS";
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_ARTIFACTS";
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = ArtifactBoss2;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= ArtifactBoss;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= ArtifactBoss;
            GameObject.Instantiate(WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(0).gameObject, WaveBoss_DoubleArtifactUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITBossTwoArtifactsSemi = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_DoubleArtifact, weight = 6f, prerequisites = Const.StartWave11Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(ITBossTwoArtifactsSemi);
            #endregion
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