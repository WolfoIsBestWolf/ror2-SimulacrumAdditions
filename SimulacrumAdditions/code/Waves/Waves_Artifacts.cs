using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;
using System.Collections;
using static SimulacrumAdditions.Constant;
using static SimulacrumAdditions.H;

namespace SimulacrumAdditions.Waves
{
    public class Waves_Artifacts
    {
        public static void MakeWaves()
        {
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
            ArtifactDef ArtifactDefMonsterTeamGainsItems = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/MonsterTeamGainsItems");
            ArtifactDef ArtifactDefRandomSurvivor = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/RandomSurvivorOnRespawn");
            ArtifactDef ArtifactDefSacrifice = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Sacrifice");
            ArtifactDef ArtifactDefSwarms = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/Swarms");

            //When Metamorphosis Toggled Respawn Player, for the Augment
            //Wonder if this actually works on Severs
            RunArtifactManager.onArtifactEnabledGlobal += RespawnMetamorphosis_onArtifactEnabledGlobal;
            RunArtifactManager.onArtifactDisabledGlobal += RespawnMetamorphosis_onArtifactDisabledGlobal;

            #region Artifact of Evolution

            InfiniteTowerWaveArtifactPrerequisites ArtifacEvolutionDisabledPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveArtifactPrerequisites>();
            ArtifacEvolutionDisabledPrerequisite.bannedArtifact = ArtifactDefMonsterTeamGainsItems;
            ArtifacEvolutionDisabledPrerequisite.name = "ArtifacEvolutionDisabledPrerequisite";

            MakeWave(new NewWaveInfo
            {
                name = "ArtifactEvolution",
                dropTable = dtITWaveTier1,
                rewardOptionCount = 2,

                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                waveCategory = ITBasicWaves,
                weight = 2,
                prereq = ArtifacEvolutionDisabledPrerequisite,

            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_EVOLUTION",
                DescToken = "ITWAVE_DESC_BASIC_EVOLUTION",
                icon = ArtifactDefMonsterTeamGainsItems.smallIconSelectedSprite,
            },
            out GameObject WaveArtifactEvolution,
            out _);
            WaveArtifactEvolution.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefMonsterTeamGainsItems;
            #endregion
            #region Artifact of Sacrifice

            InfiniteTowerWaveArtifactPrerequisites ArtifacSacrificeDisabledPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveArtifactPrerequisites>();
            ArtifacSacrificeDisabledPrerequisite.bannedArtifact = ArtifactDefSacrifice;
            ArtifacSacrificeDisabledPrerequisite.name = "ArtifacSacrificeDisabledPrerequisite";

            MakeWave(new NewWaveInfo
            {
                name = "ArtifactSacrifice",
                dropTable = dtITBasicWaveOnKill,

                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                waveCategory = ITBasicWaves,
                weight = 2f,
                prereq = ArtifacSacrificeDisabledPrerequisite,

            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_SACRIFICE",
                DescToken = "ITWAVE_DESC_BASIC_SACRIFICE",
                icon = ArtifactDefSacrifice.smallIconSelectedSprite,
            },
            out GameObject WaveArtifactSacrifice,
           out _);
            WaveArtifactSacrifice.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSacrifice;
            #endregion
            #region Artifact of Metamorphosis
            InfiniteTowerWaveArtifactPrerequisites ArtifacRandomSurvivorDisabledPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveArtifactPrerequisites>();
            ArtifacRandomSurvivorDisabledPrerequisite.bannedArtifact = ArtifactDefRandomSurvivor;
            ArtifacRandomSurvivorDisabledPrerequisite.name = "ArtifacRandomSurvivorDisabledPrerequisite";

            MakeWave(new NewWaveInfo
            {
                name = "ArtifactMetamorphosis",
                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                dropTable = Constant.dtITRainbow,
                waveCategory = ITBasicWaves,
                weight = 1,
                prereq = ArtifacRandomSurvivorDisabledPrerequisite,
            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_METAMORPH",
                DescToken = "ITWAVE_DESC_BASIC_METAMORPH",
                icon = ArtifactDefRandomSurvivor.smallIconSelectedSprite,
            },
           out GameObject WaveArtifactRandomSurvivor,
           out _);

            WaveArtifactRandomSurvivor.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefRandomSurvivor;
            #endregion
            #region Artifact of Honor
            MakeWave(new NewWaveInfo
            {
                name = "ArtifactHonor",
                dropTable = dtITBasicBonusGreen,
                itemTier = ItemTier.Tier2,
                baseCredits = 115,

                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                waveCategory = ITBasicWaves,
                weight = 2f,
                prereq = StartWave11Prerequisite,

            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_HONOR",
                DescToken = "ITWAVE_DESC_BASIC_HONOR",
                icon = ArtifactDefEliteOnly.smallIconSelectedSprite,
            },
           out GameObject WaveArtifactEliteOnly,
           out _);
            WaveArtifactEliteOnly.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;

            #endregion
            #region Artifact of Swarms
            InfiniteTowerWaveArtifactPrerequisites ArtifacSwarmDisabledPrerequisite = ScriptableObject.CreateInstance<InfiniteTowerWaveArtifactPrerequisites>();
            ArtifacSwarmDisabledPrerequisite.bannedArtifact = ArtifactDefSwarms;
            ArtifacSwarmDisabledPrerequisite.name = "ArtifacSwarmDisabledPrerequisite";
            MakeWave(new NewWaveInfo
            {
                name = "ArtifactSwarms",
                dropTable = dtITWaveTier1,
                rewardOptionCount = 6,

                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                waveCategory = ITBasicWaves,
                weight = 2f,
                prereq = ArtifacSwarmDisabledPrerequisite,

            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_SWARMS",
                DescToken = "ARTIFACT_SWARMS_DESCRIPTION",
                icon = ArtifactDefSwarms.smallIconSelectedSprite,
            },
           out GameObject WaveArtifactSwarms,
           out _);
            WaveArtifactSwarms.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDefSwarms;
            #endregion
            //
            #region Artifact of Honor + Artifact of Brigade. (Here instead of mod support cuz instantiate clone of other wave)
            //Honor+Brigade
            /*GameObject InfiniteTowerWaveArtifactHonorAndBrigade = PrefabAPI.InstantiateClone(InfiniteTowerWaveArtifactEliteOnly, "InfiniteTowerWaveArtifactHonorAndBrigade", true);
            GameObject InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI = PrefabAPI.InstantiateClone(InfiniteTowerCurrentArtifactEliteOnlyWaveUI, "InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI", false);

            InfiniteTowerWaveArtifactHonorAndBrigade.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI;
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_HONORBRIGADE";
            InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_HONORBRIGADE";
            GameObject.Instantiate(InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerCurrentArtifactHonorAndBrigadeWaveUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactHonorAndBrigade = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactHonorAndBrigade, weight = 0.75f, prerequisites = Const.StartWave11Prerequisite };
            Const.ITModSupportWaves.wavePrefabs = Const.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifactHonorAndBrigade);*/
            #endregion

            #region DLC2 Artifact
            #region Artifact of Devotion
            ArtifactDef ArtifactDevotion = Addressables.LoadAssetAsync<ArtifactDef>(key: "RoR2/CU8/Devotion.asset").WaitForCompletion();


            MakeWave(new NewWaveInfo
            {
                name = "ArtifactDevotion",
                dropTable = dtITWaveTier1,
                rewardOptionCount = 2,
                wavePrefab = ArtifactWave,
                uiPrefab = ArtifactWaveUI,
                waveCategory = ITBasicWaves,
                weight = 2f,
            }, new NewWaveUIInfo
            {
                NameToken = "ITWAVE_NAME_BASIC_DEVOTION",
                DescToken = "ITWAVE_DESC_BASIC_DEVOTION",
                icon = ArtifactDevotion.smallIconSelectedSprite,
            },
        out GameObject WaveArtifactDevotion,
        out _);

            WaveArtifactDevotion.GetComponent<ArtifactEnabler>().artifactDef = ArtifactDevotion;

            WaveArtifactDevotion.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
            WaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITWaveTier1;
            WaveArtifactDevotion.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            SimulacrumInteractablesWaveHelper spawner = WaveArtifactDevotion.AddComponent<SimulacrumInteractablesWaveHelper>();
            spawner.maxDistance = 20;
            spawner.spawnCard = Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/CU8/LemurianEgg/iscLemurianEgg.asset").WaitForCompletion(); ;
            spawner.spawnsOnStart = 4;
            spawner.spawnedTimer = 99999;
            spawner.interval = 99999;
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
            //InfiniteTowerWaveArtifactDelusion.AddComponent<RunArtifactOfDelusion>();

            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Delusion.smallIconSelectedSprite;
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DELUSION";
            InfiniteTowerCurrentArtifactDelusionWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DELUSION";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifactDelusion = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactDelusion, weight = 1f };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicArtifactDelusion);
            */
            #endregion
            #endregion
            BossWaves();
        }

        public static void BossWaves()
        {
            ArtifactDef ArtifactDefEliteOnly = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/EliteOnly");
            ArtifactDef ArtifactDefShadowClone = LegacyResourcesAPI.Load<ArtifactDef>("ArtifactDefs/ShadowClone");

            #region Artifact of Honor
            //Honor Boss
            GameObject WaveBoss_ArtifactEliteOnly = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_ArtifactEliteOnly", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactEliteOnly = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerCurrentBossWaveUIArtifactEliteOnly", false);

            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_HONOR";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_HONOR";
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ArtifactDefEliteOnly.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactEliteOnly.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.8f, 0.8f, 1);
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactEliteOnly;

            WaveBoss_ArtifactEliteOnly.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefEliteOnly;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.1f;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITSpecialBossYellow;
            WaveBoss_ArtifactEliteOnly.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            WaveBoss_ArtifactEliteOnly.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 15;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = WaveBoss_ArtifactEliteOnly.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITDamageDown";
            simulacrumGiveItemsOnStart.count = 40;
            simulacrumGiveItemsOnStart.extraPer10Wave = 0;

            InfiniteTowerWaveCategory.WeightedWave ITBossArtifactEliteOnly = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactEliteOnly, weight = 7f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossArtifactEliteOnly);
            #endregion
            #region Artifact of Vengence
            //Vengence Boss
            GameObject WaveBoss_ArtifactDoppelganger = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_ArtifactDoppelgangerVengence", true);
            GameObject InfiniteTowerCurrentBossWaveUIArtifactDoppelganger = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerCurrentBossWaveUIArtifactDoppelganger", false);

            //WaveBoss_ArtifactDoppelganger.AddComponent<ArtifactEnabler>().artifactDef = ArtifactDefShadowClone;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().baseCredits *= 0.8f;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().immediateCreditsFraction = 0.0f;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerBossWaveController>().secondsBeforeSuddenDeath *= 1.25f;

            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier2;
            WaveBoss_ArtifactDoppelganger.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_ArtifactDoppelganger.AddComponent<SimuWaveUnsortedExtras>().code = SimuWaveUnsortedExtras.Case.Vengence;

            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_VENGENCE";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_VENGENCE";
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ArtifactDefShadowClone.smallIconSelectedSprite;
            InfiniteTowerCurrentBossWaveUIArtifactDoppelganger.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.8f, 0.8f, 1);
            WaveBoss_ArtifactDoppelganger.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossWaveUIArtifactDoppelganger;

            InfiniteTowerWaveCategory.WeightedWave ITBossArtifactDoppelganger = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactDoppelganger, weight = 9f, prerequisites = Constant.StartWave11Prerequisite }; //5 normally
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossArtifactDoppelganger);
            #endregion
            #region Artifact of Kin
            //Kin Boss
            GameObject WaveBoss_ArtifactKin = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_ArtifactSingleMonsterType", true);
            GameObject WaveBoss_ArtifactKinUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_ArtifactSingleMonsterTypeUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactSingleMonsterTypeDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/ArtifactSingleMonsterTypeDisabledPrerequisite.asset").WaitForCompletion();

            WaveBoss_ArtifactKin.AddComponent<ArtifactEnabler>().artifactDef = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact;
            WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_ArtifactKinUI;
            //WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.1f;
            WaveBoss_ArtifactKin.GetComponent<CombatDirector>().eliteBias = 0.1f;
            WaveBoss_ArtifactKin.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_KIN";
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_KIN";
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ArtifactSingleMonsterTypeDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_ArtifactKinUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossKin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactKin, weight = 7f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossKin);
            #endregion     
            #region Artifact of Dissonace
            //Dissonance Boss
            GameObject WaveBoss_ArtifactDissonance = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_ArtifactDissonance", true);
            GameObject WaveBoss_ArtifactDissonanceUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_ArtifactDissonanceUI", false);
            InfiniteTowerWaveArtifactPrerequisites ArtifactMixEnemyDisabledPrerequisite = Addressables.LoadAssetAsync<InfiniteTowerWaveArtifactPrerequisites>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/ArtifactMixEnemyDisabledPrerequisite.asset").WaitForCompletion();

            WaveBoss_ArtifactDissonance.AddComponent<ArtifactEnabler>().artifactDef = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact;
            WaveBoss_ArtifactDissonance.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_ArtifactDissonanceUI;
            WaveBoss_ArtifactDissonance.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossGreenVoid;

            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DISSONANCE";
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DISSONANCE";
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_ArtifactDissonanceUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.8f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_ArtifactDissonance, weight = 10f, prerequisites = ArtifactMixEnemyDisabledPrerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossDissonance);
            #endregion
            #region Artifact of "Dissonance" (All enemy "family")
            //
            //All enemies "Family"
            GameObject WaveBoss_AllEnemiesAvailable = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_AllEnemiesAvailable", true);
            GameObject WaveBoss_AllEnemiesAvailableUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_AllEnemiesAvailableUI", false);

            WaveBoss_AllEnemiesAvailable.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/MixEnemy/dccsMixEnemy.asset").WaitForCompletion();
            WaveBoss_AllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_AllEnemiesAvailableUI;
            WaveBoss_AllEnemiesAvailable.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossGreenVoid;
            WaveBoss_AllEnemiesAvailable.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            //WaveBoss_AllEnemiesAvailable.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.Lunar

            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_DISSOFAKE";
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_DISSOFAKE";
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = ArtifactMixEnemyDisabledPrerequisite.bannedArtifact.smallIconSelectedSprite;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1, 0.8f, 0.8f, 1) * Waves_Family.FamilyEventIconColor;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color *= Waves_Family.FamilyEventIconColor;
            WaveBoss_AllEnemiesAvailableUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color *= Waves_Family.FamilyEventOutlineColor;

            InfiniteTowerWaveCategory.WeightedWave ITBossNotDissonance = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_AllEnemiesAvailable, weight = 4f };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossNotDissonance);
            #endregion
            #region 2 Artifacts
            GameObject WaveBoss_DoubleArtifact = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_DoubleArtifact", true);
            GameObject WaveBoss_DoubleArtifactUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "WaveBoss_DoubleArtifactUI", false);

            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 0;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0f;

            WaveBoss_DoubleArtifact.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            WaveBoss_DoubleArtifact.AddComponent<SimulacrumArtifactTrialWave>();
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossBonusRed;

            Color ArtifactBoss = new Color(1.2f, 0.8f, 1.2f);
            Color ArtifactBoss2 = new Color(1f, 0.7f, 0.8f, 1f);

            WaveBoss_DoubleArtifact.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_DoubleArtifactUI;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_ARTIFACTS";
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_ARTIFACTS";
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = ArtifactBoss2;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color *= ArtifactBoss;
            WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color *= ArtifactBoss;
            GameObject.Instantiate(WaveBoss_DoubleArtifactUI.transform.GetChild(0).GetChild(0).gameObject, WaveBoss_DoubleArtifactUI.transform.GetChild(0));

            InfiniteTowerWaveCategory.WeightedWave ITBossTwoArtifactsSemi = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_DoubleArtifact, weight = 6f, prerequisites = Constant.StartWave11Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(ITBossTwoArtifactsSemi);
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