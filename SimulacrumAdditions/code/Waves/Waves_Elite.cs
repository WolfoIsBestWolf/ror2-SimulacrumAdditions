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
            GameObject InfiniteTowerWaveLunarElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarElites", true);
            GameObject InfiniteTowerWaveLunarElitesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLunarElitesUI;
            InfiniteTowerWaveLunarElites.AddComponent<SimulacrumEliteWaves>().lunarOnly = true;

            Texture2D texITWaveLunarEliteIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarEliteIcon.LoadImage(Properties.Resources.texITWaveLunarEliteIcon, true);
            texITWaveLunarEliteIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarEliteIconS = Sprite.Create(texITWaveLunarEliteIcon, WRect.rec64, WRect.half);

            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Perfection";
            InfiniteTowerWaveLunarElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Perfected.";
            //InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.3f,0.6f,1f);

            InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            #endregion
            #region Voidtouched Elites
            //
            //Void Elites
            GameObject InfiniteTowerWaveVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidElites", true);
            GameObject InfiniteTowerCurrentVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentVoidEliteWaveUI", false);

            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidElites.AddComponent<SimulacrumEliteWaves>().voidOnly = true;

            BuffDef bdEliteVoid = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/EliteVoid/bdEliteVoid.asset").WaitForCompletion();
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Collapse";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Voidtouched.";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = bdEliteVoid.buffColor;

            InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            #endregion
            #region (Boss) Lunar + Void
            //LunarVoidBoss
            GameObject InfiniteTowerWaveBossLunarAndVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossLunarAndVoidElites", true);
            GameObject InfiniteTowerWaveBossLunarAndVoidElitesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;
            InfiniteTowerWaveBossLunarAndVoidElites.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITHeresy;
            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossLunarAndVoidElites.AddComponent<SimulacrumEliteWaves>().lunarPlusVoid = true;

            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<CombatDirector>().eliteBias = 0.75f;
            InfiniteTowerWaveBossLunarAndVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossLunarAndVoidElitesUI;

            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Duality";
            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Perfected or Voidtouched.";
            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.6f, 1f, 1);
            //InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.85f, 0.95f, 1);
            //InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.9f, 1);

            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            GameObject.Instantiate(InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).gameObject, InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0));
            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.8f, 1f, 1);
            InfiniteTowerWaveBossLunarAndVoidElitesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;

            InfiniteTowerWaveCategory.WeightedWave BossLunarVoids = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossLunarAndVoidElites, weight = 6f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(BossLunarVoids);
#endregion

            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarElites, ITVoidElites);
        }


    }

}