using R2API;
using RoR2;
using RoR2.UI;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace SimulacrumAdditions.Waves
{
    public class Waves_Elite
    {

        internal static void MakeWaves()
        {
            #region Collective Elites
            //Collective Elite Wave
            GameObject WaveBasic_EliteCollective = PrefabAPI.InstantiateClone(Constant.BasicWave, "WaveBasic_EliteCollective", true);
            GameObject WaveBasic_EliteCollectiveUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "WaveBasic_EliteCollectiveUI", false);

            WaveBasic_EliteCollective.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            WaveBasic_EliteCollective.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITTechnology;

            WaveBasic_EliteCollective.GetComponent<CombatDirector>().eliteBias = 1f;
            WaveBasic_EliteCollective.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBasic_EliteCollectiveUI;
            WaveBasic_EliteCollective.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.Collective;

            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveAC.png");
            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_DLC3_COLLECTIVE";
            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_DLC3_COLLECTIVE";
            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.65f, 0.854f, 0.895f, 1);
            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.65f, 0.854f, 0.895f, 1);
            WaveBasic_EliteCollectiveUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.65f, 0.854f, 0.895f, 2) * 0.8f;
            InfiniteTowerWaveCategory.WeightedWave WaveBasic_EliteCollectiveIT = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBasic_EliteCollective, weight = 5f, prerequisites = Constant.DLC3_Prerequisite };
            #endregion

            #region Perfected Elietes
            //Lunar Elite Wave
            GameObject InfiniteTowerWaveLunarElites = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveLunarElites", true);
            GameObject InfiniteTowerWaveLunarElitesUI = PrefabAPI.InstantiateClone(Constant.LunarWaveUI, "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusLunar;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLunarElitesUI;
            InfiniteTowerWaveLunarElites.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.Lunar;

            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_LUNARELITE";
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_LUNARELITE";
            //InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.3f,0.6f,1f);

            InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 6.5f, prerequisites = Constant.AfterWave5Prerequisite };
            #endregion
            #region Voidtouched Elites
            //
            //Void Elites
            GameObject InfiniteTowerWaveVoidElites = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveVoidElites", true);
            GameObject InfiniteTowerCurrentVoidEliteWaveUI = PrefabAPI.InstantiateClone(Constant.VoidWaveUI, "InfiniteTowerCurrentVoidEliteWaveUI", false);

            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidElites.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.Void;

            BuffDef bdEliteVoid = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/EliteVoid/bdEliteVoid.asset").WaitForCompletion();
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_VOIDELITE";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_VOIDELITE";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = bdEliteVoid.buffColor;

            InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 6.5f, prerequisites = Constant.AfterWave5Prerequisite };
            #endregion
            #region (Boss) Lunar + Void
            //LunarVoidBoss
            GameObject WaveBoss_LunarAndVoidElites = PrefabAPI.InstantiateClone(Constant.BossWave, "WaveBoss_LunarAndVoidElites", true);
            GameObject WaveBoss_LunarAndVoidElitesUI = PrefabAPI.InstantiateClone(Constant.VoidWaveUI, "WaveBoss_LunarAndVoidElites", false);

            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBossGreenVoid;
            WaveBoss_LunarAndVoidElites.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            WaveBoss_LunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITHeresy;
            WaveBoss_LunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
            WaveBoss_LunarAndVoidElites.AddComponent<SimulacrumEliteWaves>().eliteCase = SimulacrumEliteWaves.EliteCase.LunarAndVoid;

            WaveBoss_LunarAndVoidElites.GetComponent<CombatDirector>().eliteBias = 0.75f;
            WaveBoss_LunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = WaveBoss_LunarAndVoidElitesUI;

            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Duality";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BOSS_LUNARVOIDELITE";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BOSS_LUNARVOIDELITE";
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1);
            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 1f, 1);
            //WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.9f, 1);

            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(1f, 0.8f, 1f, 1);
            GameObject.Instantiate(WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).gameObject, WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0));
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.6f, 0.8f, 1f, 1);
            WaveBoss_LunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveLunarWhite.png");

            InfiniteTowerWaveCategory.WeightedWave BossLunarVoids = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = WaveBoss_LunarAndVoidElites, weight = 6f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBossWaves.wavePrefabs = Constant.ITBossWaves.wavePrefabs.Add(BossLunarVoids);
            #endregion

            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(WaveBasic_EliteCollectiveIT, ITLunarElites, ITVoidElites);
        }


    }

}