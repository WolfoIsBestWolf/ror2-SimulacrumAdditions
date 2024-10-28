﻿using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_ModSupport
    {
        public static void ModdedArtifacts()
        {
            #region StarStorm2 Artifact of Cognation
            //SS2 Cognation
            GameObject InfiniteTowerWaveArtifactSS2Cognation = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactSS2Cognation", true);
            GameObject InfiniteTowerWaveArtifactSS2CognationUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerWaveArtifactSS2CognationUI", false);

            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactSS2CognationUI;
            InfiniteTowerWaveArtifactSS2Cognation.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;

            //InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "SS2_ITWAVE_NAME_BASIC_COGNATION";
            InfiniteTowerWaveArtifactSS2CognationUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "SS2_ITWAVE_DESC_BASIC_COGNATION";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacSS2Cognation = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactSS2Cognation, weight = 1.5f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacSS2Cognation);
            #endregion
            //
            //
            //ZET Tossing
            GameObject InfiniteTowerWaveArtifactZetTossing = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactZetTossing", true);
            GameObject InfiniteTowerWaveArtifactZetTossingUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerWaveArtifactZetTossingUI", false);

            InfiniteTowerWaveArtifactZetTossing.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactZetTossingUI;
            InfiniteTowerWaveArtifactZetTossing.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;

            //InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ZetTossing.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "MOD_ITWAVE_NAME_BASIC_TOSSING";
            InfiniteTowerWaveArtifactZetTossingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "MOD_ITWAVE_DESC_BASIC_TOSSING";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacZetTossing = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactZetTossing, weight = 1f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacZetTossing);
            //
            //
            //Risky Origin
            GameObject InfiniteTowerWaveArtifactRiskyOrigin = PrefabAPI.InstantiateClone(Const.ArtifactWave, "InfiniteTowerWaveArtifactRiskyOrigin", true);
            GameObject InfiniteTowerWaveArtifactRiskyOriginUI = PrefabAPI.InstantiateClone(Const.ArtifactWaveUI, "InfiniteTowerWaveArtifactRiskyOriginUI", false);

            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtifactRiskyOriginUI;
            InfiniteTowerWaveArtifactRiskyOrigin.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;

            //InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = RiskyOrigin.smallIconSelectedSprite;
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "MOD_ITWAVE_NAME_BASIC_ORIGIN";
            InfiniteTowerWaveArtifactRiskyOriginUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "MOD_ITWAVE_DESC_BASIC_ORIGIN";

            InfiniteTowerWaveCategory.WeightedWave ITBasicArtifacRiskyOrigin = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtifactRiskyOrigin, weight = 1f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicArtifacRiskyOrigin);
        }


        internal static void MakeWaves()
        {
            ModdedArtifacts();
            //Could do NemMando NemMerc but idk they'd just fucking die like in the regular game

            //Minor Mod - DireSeeker
            GameObject InfiniteTowerWaveBossDireseeker = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveBossDireseeker", true);
            GameObject InfiniteTowerCurrentBossDireseekerWaveUI = PrefabAPI.InstantiateClone(Const.BossWaveUI, "InfiniteTowerCurrentBossDireseekerWaveUI", false);

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossDireseeker.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossDireseeker.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossDireseekerWaveUI;

            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "MOD_ITWAVE_NAME_BOSS_DIRESEEKER";
            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "MOD_ITWAVE_DESC_BOSS_DIRESEEKER";

            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDireseeker = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossDireseeker, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBossDireseeker);
            //
            //
            //Will need to see what Empyrean Elites actually do
            GameObject InfiniteTowerWaveSS2RainbowElites = PrefabAPI.InstantiateClone(Const.ScavWave, "InfiniteTowerWaveSS2RainbowElites", true);
            GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "SS2_ITWAVE_NAME_BASIC_RAINBOWELITE";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "SS2_ITWAVE_DESC_BASIC_RAINBOWELITE";
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 0.7f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.85f, 0.3f, 0.3f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 4f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
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

 