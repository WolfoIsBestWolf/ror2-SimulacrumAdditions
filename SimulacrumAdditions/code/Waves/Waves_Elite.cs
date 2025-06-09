using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_Elite
    {
 
        internal static void MakeWaves()
        {
            #region Perfected Elietes
            //Lunar Elite Wave
            GameObject InfiniteTowerWaveLunarElites = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveLunarElites", true);
            GameObject InfiniteTowerWaveLunarElitesUI = PrefabAPI.InstantiateClone(Const.LunarWaveUI, "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicBonusLunar;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLunarElitesUI;
            InfiniteTowerWaveLunarElites.AddComponent<SimulacrumEliteWaves>().lunarOnly = true;
  
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_LUNARELITE";
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_LUNARELITE";
            //InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.3f,0.6f,1f);

            InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 6.5f, prerequisites = Const.AfterWave5Prerequisite };
            #endregion
            #region Voidtouched Elites
            //
            //Void Elites
            GameObject InfiniteTowerWaveVoidElites = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWaveVoidElites", true);
            GameObject InfiniteTowerCurrentVoidEliteWaveUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerCurrentVoidEliteWaveUI", false);

            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidElites.AddComponent<SimulacrumEliteWaves>().voidOnly = true;

            BuffDef bdEliteVoid = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/EliteVoid/bdEliteVoid.asset").WaitForCompletion();
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_VOIDELITE";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_VOIDELITE";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = bdEliteVoid.buffColor;

            InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 6.5f, prerequisites = Const.AfterWave5Prerequisite };
            #endregion
            #region (Boss) Lunar + Void
            //LunarVoidBoss
            GameObject WaveBoss_LunarAndVoidElites = PrefabAPI.InstantiateClone(Const.BossWave, "WaveBoss_LunarAndVoidElites", true);
            GameObject WaveBoss_LunarAndVoidElitesUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "WaveBoss_LunarAndVoidElites", false);

            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBossGreenVoid;
            WaveBoss_LunarAndVoidElites.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            WaveBoss_LunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Const.dtITHeresy;
            WaveBoss_LunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_LunarAndVoidElites.AddComponent<SimulacrumEliteWaves>().lunarPlusVoid = true;

            WaveBoss_LunarAndVoidElites.GetComponent<CombatDirector>().eliteBias = 0.75f;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_LunarAndVoidElitesUI;

            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Duality";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_LUNARVOIDELITE";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_LUNARVOIDELITE";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1);
            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1);
            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.9f, 1);

            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            GameObject.Instantiate(WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).gameObject, WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0));
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 1f, 1);
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");

            InfiniteTowerWaveCategory.WeightedWave BossLunarVoids = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_LunarAndVoidElites, weight = 6f, prerequisites = Const.AfterWave5Prerequisite };
             Const.ITBossWaves.wavePrefabs = Const.ITBossWaves.wavePrefabs.Add(BossLunarVoids);
#endregion

            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITLunarElites, ITVoidElites);
        }


    }

}