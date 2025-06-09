using RoR2;
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
            GameObject InfiniteTowerWavePulseLunar = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWavePulseLunar", true);
            GameObject InfiniteTowerWavePulseLunarUI = PrefabAPI.InstantiateClone(Const.LunarWaveUI, "InfiniteTowerWavePulseLunarUI", false);

            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicBonusLunar;
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
            Pulse1.baseForce = 6000;
            Pulse1.pulseInterval = 1.5f;

            SimulacrumPulseWave Pulse2;
            /*SimulacrumPulseWave Pulse2 = InfiniteTowerWavePulseLunar.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = Cripple;
            Pulse2.buffDuration = 3;
            Pulse2.pulsePrefab = PulseLunar;
            Pulse2.baseForce = 2000;
            Pulse2.pulseInterval = 1.5f;
            Pulse2.affectedTeam = TeamIndex.Monster;*/

            //
            InfiniteTowerWavePulseLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseLunarUI;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PULSE_LUNAR";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PULSE_LUNAR";
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Cripple.iconSprite;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Cripple.buffColor;
            InfiniteTowerWavePulseLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.55f, 0.85f, 0.95f);
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave ITWavePulseLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseLunar, weight = 4, prerequisites = Const.AfterWave5Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITWavePulseLunar);
            #endregion
            #region Nullifcation
            //
            //Void
            GameObject InfiniteTowerWavePulseVoid = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWavePulseVoid", true);
            GameObject InfiniteTowerWavePulseVoidUI = PrefabAPI.InstantiateClone(Const.VoidWaveUI, "InfiniteTowerWavePulseVoidUI", false);

            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITBasicBonusVoid;
            InfiniteTowerWavePulseVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseVoid = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseVoid", true);
            PulseVoid.GetComponent<PulseController>().finalRadius = 75;
            PulseVoid.GetComponent<PulseController>().duration = 0.6f;
            Material matNewVoid = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulseVoid = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/texRampPulseVoid.png");
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
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PULSE_VOID";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PULSE_VOID";
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = Nullified.iconSprite;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = Nullified.buffColor;
            InfiniteTowerWavePulseVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Nullified.buffColor;
            //InfiniteTowerCurrentWaveUIFamilyLunar.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave ITWavePulseVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseVoid, weight = 4, prerequisites = Const.AfterWave5Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITWavePulseVoid);
            #endregion
            #region Big Suck
            //
            //Reverse Force
            GameObject InfiniteTowerWavePulseSuckInward = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWavePulseSuckInward", true);
            GameObject InfiniteTowerWavePulseSuckInwardUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWavePulseSuckInwardUI", false);

            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryUtility;
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            //
            GameObject PulseSuckInward = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseSuckInward", true);
            PulseSuckInward.GetComponent<PulseController>().finalRadius = 100;
            PulseSuckInward.GetComponent<PulseController>().duration = 1f;
            PulseSuckInward.transform.localPosition = new Vector3(0, -2.5f, 0);
            Material matNewPulseSuckInward = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseSuckInward = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/texRampPulseSuck.png");
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
            Pulse1.baseForce = -7500;
            Pulse1.pulseInterval = 1.3f;

            Pulse2 = InfiniteTowerWavePulseSuckInward.AddComponent<SimulacrumPulseWave>();
            Pulse2.buffDef = null;
            Pulse2.buffDuration = 0;
            Pulse2.pulsePrefab = PulseSuckInward;
            Pulse2.baseForce = -11000;
            Pulse2.pulseInterval = 1.3f;
            Pulse2.affectedTeam = TeamIndex.Monster;
            //
            InfiniteTowerWavePulseSuckInward.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWavePulseSuckInwardUI;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PULSE_BIGSUCK";
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PULSE_BIGSUCK";
            //InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.55f, 0.55f, 0.5f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = ElementalRingVoidCooldown.iconSprite;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = ElementalRingVoidCooldown.buffColor;
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.6f, 0.6f);
            InfiniteTowerWavePulseSuckInwardUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.3f, 0.3f);

            InfiniteTowerWaveCategory.WeightedWave ITWavePulseSuckInward = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseSuckInward, weight = 4, prerequisites = Const.StartWave11Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITWavePulseSuckInward);
            #endregion
            #region Malachite Pulse
            //PulseNoHealing
            GameObject InfiniteTowerWavePulseNoHealing = PrefabAPI.InstantiateClone(Const.BasicWave, "InfiniteTowerWavePulseNoHealing", true);
            GameObject InfiniteTowerWavePulseNoHealingUI = PrefabAPI.InstantiateClone(Const.BasicWaveUI, "InfiniteTowerWavePulseNoHealingUI", false);

            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Const.dtITCategoryHealing;
            InfiniteTowerWavePulseNoHealing.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            ///
            GameObject PulseNoHealing = PrefabAPI.InstantiateClone(PulseLunar, "ITPulseNoHealing", true);
            PulseNoHealing.GetComponent<PulseController>().finalRadius = 150;
            PulseNoHealing.GetComponent<PulseController>().duration = 1.5f;
            Material matNewPulseNoHealing = GameObject.Instantiate(PulseLunar.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Texture2D texRampPulsematNewPulseNoHealing = Assets.Bundle.LoadAsset<Texture2D>("Assets/Simulacrum/Main/texRampPulseNoHeal.png");
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
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_PULSE_MALACHITE";
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_PULSE_MALACHITE";
            //InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.6f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = HealingDisabled.iconSprite;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HealingDisabled.buffColor;
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.9f, 0.3f);
            InfiniteTowerWavePulseNoHealingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.5f, 0.2f);

            InfiniteTowerWaveCategory.WeightedWave ITWavePulseNoHealing = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWavePulseNoHealing, weight = 4, prerequisites = Const.StartWave11Prerequisite };
            Const.ITBasicWaves.wavePrefabs = Const.ITBasicWaves.wavePrefabs.Add(ITWavePulseNoHealing);
            #endregion
        }

    }

}