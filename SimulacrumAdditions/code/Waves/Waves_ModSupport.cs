using R2API;
using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
namespace SimulacrumAdditions.Waves
{
    public class Waves_ModSupport
    {
        public static void ModdedArtifacts()
        {
            #region StarStorm2 Artifact of Cognation
            //SS2 Cognation
            GameObject InfiniteTowerWaveArtifactSS2Cognation = PrefabAPI.InstantiateClone(Constant.ArtifactWave, "InfiniteTowerWaveArtifactSS2Cognation", true);
            GameObject InfiniteTowerWaveArtifactSS2CognationUI = PrefabAPI.InstantiateClone(Constant.ArtifactWaveUI, "InfiniteTowerWaveArtifactSS2CognationUI", false);

            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactSS2CognationUI;
            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicWaveOnKill;

            //InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "SS2_ITWAVE_NAME_BASIC_COGNATION";
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "SS2_ITWAVE_DESC_BASIC_COGNATION";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacSS2Cognation = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSS2Cognation, weight = 1.5f };
            Constant.ITModSupportWaves.wavePrefabs = Constant.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacSS2Cognation);
            #endregion

            //Risky Origin
            GameObject InfiniteTowerWaveArtifactRiskyOrigin = PrefabAPI.InstantiateClone(Constant.ArtifactWave, "InfiniteTowerWaveArtifactRiskyOrigin", true);
            GameObject InfiniteTowerWaveArtifactRiskyOriginUI = PrefabAPI.InstantiateClone(Constant.ArtifactWaveUI, "InfiniteTowerWaveArtifactRiskyOriginUI", false);

            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactRiskyOriginUI;
            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;

            //InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = RiskyOrigin.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "MOD_ITWAVE_NAME_BASIC_ORIGIN";
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "MOD_ITWAVE_DESC_BASIC_ORIGIN";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacRiskyOrigin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRiskyOrigin, weight = 1f };
            Constant.ITModSupportWaves.wavePrefabs = Constant.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacRiskyOrigin);
        }


        internal static void MakeWaves()
        {
            ModdedArtifacts();
            //Could do NemMando NemMerc but idk they'd just fucking die like in the regular game

            //Minor Mod - DireSeeker
            GameObject WaveBoss_Direseeker = PrefabAPI.InstantiateClone(Constant.ScavWave, "WaveBoss_Direseeker", true);
            GameObject InfiniteTowerCurrentBossDireseekerWaveUI = PrefabAPI.InstantiateClone(Constant.BossWaveUI, "InfiniteTowerCurrentBossDireseekerWaveUI", false);

            WaveBoss_Direseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            WaveBoss_Direseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            WaveBoss_Direseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3;
            WaveBoss_Direseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;

            WaveBoss_Direseeker.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            WaveBoss_Direseeker.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier3;

            WaveBoss_Direseeker.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_Direseeker.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();

            WaveBoss_Direseeker.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossDireseekerWaveUI;

            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "MOD_ITWAVE_NAME_BOSS_DIRESEEKER";
            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "MOD_ITWAVE_DESC_BOSS_DIRESEEKER";

            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDireseeker = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_Direseeker, weight = 4f, prerequisites = Constant.StartWave20Prerequisite };
            Constant.ITModSupportWaves.wavePrefabs = Constant.ITModSupportWaves.wavePrefabs.Add(ITBossDireseeker);
            //
            //
            //Will need to see what Empyrean Elites actually do
            GameObject InfiniteTowerWaveSS2RainbowElites = PrefabAPI.InstantiateClone(Constant.BaseExplicit_Basic, "InfiniteTowerWaveSS2RainbowElites", true);
            GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "SS2_ITWAVE_NAME_BASIC_RAINBOWELITE";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "SS2_ITWAVE_DESC_BASIC_RAINBOWELITE";
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.7f, 1f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color(0.7f, 1f, 0.7f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.85f, 0.3f, 0.3f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 4f, prerequisites = Constant.StartWave30Prerequisite };
            Constant.ITModSupportWaves.wavePrefabs = Constant.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
            //

            /*
            //Storms
            GameObject InfiniteTowerWaveSS2Storms = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveSS2Storms", true);
            GameObject InfiniteTowerCurrentSS2StormsWaveUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentSS2StormsWaveUI", false);

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2StormsWaveUI;
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Storms";
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A storm approaches.";
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2Storms = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2Storms, weight = 3f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2Storms);
            */
        }


    }


}

