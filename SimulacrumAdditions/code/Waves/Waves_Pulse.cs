﻿using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_Pulse
    {
        internal static void MakeWaves()
        {
            On.EntityStates.Missions.Moon.MoonBatteryComplete.OnEnter += (orig, self) =>
            {
                if (self.gameObject.GetComponent<SimulacrumPulseWave>())
                {
                    return;
                }
                orig(self);
            };

            #region Design
            //Lunar
            GameObject InfiniteTowerWavePulseLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseLunar", true);
            GameObject InfiniteTowerWavePulseLunarUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseLunarUI", false);

            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseLunar = PrefabAPI.InstantiateClone(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/MoonBatteryDesignPulse"), "ITPulseLunar", true);
            PulseLunar.GetComponent<PulseController>().finalRadius = 75;
            PulseLunar.GetComponent<PulseController>().duration = 0.6f;
            PulseLunar.transform.GetChild(1).localScale *= 2;
            PulseLunar.transform.GetChild(2).localScale *= 2;
            PulseLunar.transform.GetChild(4).localScale *= 3;

            BuffDef Cripple = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/Cripple");
            SimulacrumPulseWave Pulse1 = InfiniteTowerWavePulseLunar.AddComponent<SimulacrumPulseWave>();
            Pulse1.buffDef = Cripple;
            Pulse1.buffDuration = 3;
            Pulse1.pulsePrefab = PulseLunar;
            Pulse1.baseForce = 7000;
            Pulse1.pulseInterval = 1.5f;

            SimulacrumPulseWave Pulse2 = InfiniteTowerWavePulseLunar.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = Cripple;
            Pulse2.buffDuration = 3;
            Pulse2.pulsePrefab = PulseLunar;
            Pulse2.baseForce = 6000;
            Pulse2.pulseInterval = 1.5f;
            Pulse2.affectedTeam = TeamIndex.Monster;

            //
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseLunarUI;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Design";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A crippling pulse is being emmited.";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Cripple.iconSprite;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Cripple.buffColor;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.55f, 0.85f, 0.95f);
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseLunar, weight = 4, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseLunar);
            #endregion
            #region Nullifcation
            //
            //Void
            GameObject InfiniteTowerWavePulseVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseVoid", true);
            GameObject InfiniteTowerWavePulseVoidUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseVoidUI", false);

            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseVoid = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseVoid", true);
            PulseVoid.GetComponent<PulseController>().finalRadius = 75;
            PulseVoid.GetComponent<PulseController>().duration = 0.6f;
            Material matNewVoid = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulseVoid = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulseVoid.LoadImage(Properties.Resources.texRampPulseVoid, true);
            texRampPulseVoid.wrapMode = TextureWrapMode.Clamp;
            texRampPulseVoid.filterMode = FilterMode.Point;
            matNewVoid.SetTexture("_RemapTex", texRampPulseVoid);
            PulseVoid.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewVoid;
            PulseVoid.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(1f, 0.2f, 1f, 1f);
            PulseVoid.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.255f, 0.2f, 0.255f, 1f);
            PulseVoid.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.9137f, 0.3569f, 0.9137f, 0.5922f);

            BuffDef NullifyStack = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/NullifyStack");
            BuffDef Nullified = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/Nullified");
            Pulse1 = InfiniteTowerWavePulseVoid.AddComponent<SimulacrumPulseWave>();
            Pulse1.buffDef = NullifyStack;
            Pulse1.buffDuration = 20;
            Pulse1.pulsePrefab = PulseVoid;
            Pulse1.baseForce = 3500;
            Pulse1.pulseInterval = 1.5f;

            Pulse2 = InfiniteTowerWavePulseVoid.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = NullifyStack;
            Pulse2.buffDuration = 20;
            Pulse2.pulsePrefab = PulseVoid;
            Pulse2.baseForce = 3500;
            Pulse2.pulseInterval = 1.5f;
            Pulse2.affectedTeam = TeamIndex.Monster;
            //
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseVoidUI;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Nullifciation";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Nullifying grounded subjects.";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Nullified.iconSprite;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Nullified.buffColor;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Nullified.buffColor;
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseVoid, weight = 4, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseVoid);
            #endregion
            #region Big Suck
            //
            //Reverse Force
            GameObject InfiniteTowerWavePulseSuckInward = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseSuckInward", true);
            GameObject InfiniteTowerWavePulseSuckInwardUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseSuckInwardUI", false);

            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryUtility;
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseSuckInward = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseSuckInward", true);
            PulseSuckInward.GetComponent<PulseController>().finalRadius = 100;
            PulseSuckInward.GetComponent<PulseController>().duration = 1f;
            PulseSuckInward.transform.localPosition = new Vector3(0, -2.5f, 0);
            Material matNewPulseSuckInward = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseSuckInward = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulsematNewPulseSuckInward.LoadImage(Properties.Resources.texRampPulseSuck, true);
            texRampPulsematNewPulseSuckInward.wrapMode = TextureWrapMode.Clamp;
            texRampPulsematNewPulseSuckInward.filterMode = FilterMode.Point;
            matNewPulseSuckInward.SetTexture("_RemapTex", texRampPulsematNewPulseSuckInward);
            PulseSuckInward.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewPulseSuckInward;
            PulseSuckInward.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.2f, 0.2f, 1f);
            PulseSuckInward.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.2f, 0.2f, 1f);
            PulseSuckInward.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.3f, 0.3f, 0.3f, 0.5922f);
            //
            BuffDef ElementalRingVoidCooldown = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/ElementalRingVoidCooldown");
            //
            Pulse1 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse1.buffDef = null;
            Pulse1.buffDuration = 0;
            Pulse1.pulsePrefab = PulseSuckInward;
            Pulse1.baseForce = -11000;
            Pulse1.pulseInterval = 1.15f;

            Pulse2 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = null;
            Pulse2.buffDuration = 0;
            Pulse2.pulsePrefab = PulseSuckInward;
            Pulse2.baseForce = -11000;
            Pulse2.pulseInterval = 1.15f;
            Pulse2.affectedTeam = TeamIndex.Monster;
            //
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseSuckInwardUI;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Attraction";
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Grounded monsters get pulled inwards.";
            //InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.55f, 0.55f, 0.5f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ElementalRingVoidCooldown.iconSprite;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = ElementalRingVoidCooldown.buffColor;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.6f, 0.6f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.3f, 0.3f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseSuckInward = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseSuckInward, weight = 4, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseSuckInward);
            #endregion
            #region Malachite Pulse
            //PulseNoHealing
            GameObject InfiniteTowerWavePulseNoHealing = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWavePulseNoHealing", true);
            GameObject InfiniteTowerWavePulseNoHealingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWavePulseNoHealingUI", false);

            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITCategoryHealing;
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            ///
            GameObject PulseNoHealing = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseNoHealing", true);
            PulseNoHealing.GetComponent<PulseController>().finalRadius = 150;
            PulseNoHealing.GetComponent<PulseController>().duration = 1.5f;
            Material matNewPulseNoHealing = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseNoHealing = new Texture2D(256, 8, TextureFormat.DXT1, false);
            texRampPulsematNewPulseNoHealing.LoadImage(Properties.Resources.texRampPulseNoHeal, true);
            texRampPulsematNewPulseNoHealing.wrapMode = TextureWrapMode.Clamp;
            texRampPulsematNewPulseNoHealing.filterMode = FilterMode.Point;
            matNewPulseNoHealing.SetTexture("_RemapTex", texRampPulsematNewPulseNoHealing);
            PulseNoHealing.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matNewPulseNoHealing;
            PulseNoHealing.transform.GetChild(1).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.6f, 0.2f, 1f);
            PulseNoHealing.transform.GetChild(2).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.15f, 0.255f, 0.15f, 1f);
            PulseNoHealing.transform.GetChild(4).GetComponent<UnityEngine.ParticleSystem>().startColor = new Color(0.2f, 0.7f, 0.2f, 0.58f);


            BuffDef HealingDisabled = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/HealingDisabled");
            InfiniteTowerWavePulseNoHealing.AddComponent<SimulacrumPulseWave>().buffDef = HealingDisabled;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().buffDuration = 8;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().pulsePrefab = PulseNoHealing;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().baseForce = 0;
            InfiniteTowerWavePulseNoHealing.GetComponent<SimulacrumPulseWave>().pulseInterval = 1.5f;
            //
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseNoHealingUI;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Poison";
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Your healing will be disabled if pulsed.";
            //InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.6f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = HealingDisabled.iconSprite;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HealingDisabled.buffColor;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.9f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.5f, 0.2f);

            RoR2.InfiniteTowerWaveCategory.WeightedWave ITWavePulseNoHealing = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseNoHealing, weight = 4, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITWavePulseNoHealing);
            #endregion
        }

    }

}