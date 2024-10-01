using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_EquipmentGiving
    {
        internal static void MakeWaves()
        {
            #region Flying enemies (Milky Chris)
            //Flight
            GameObject InfiniteTowerWaveJetpack = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFlight", true);
            GameObject InfiniteTowerWaveJetpackUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveJetpackUI", false);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveJetpack.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 0.75f;
            InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;
            InfiniteTowerWaveJetpack.AddComponent<SimuEquipmentWaveHelper>();
            InfiniteTowerWaveJetpack.GetComponent<SimuEquipmentWaveHelper>().variant = 0;
            InfiniteTowerWaveJetpack.AddComponent<SetGravity>().newGravity = -20;

            Texture2D texITWaveJetpack = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveJetpack.LoadImage(Properties.Resources.texITWaveJetpack, true);
            texITWaveJetpack.filterMode = FilterMode.Bilinear;
            Sprite texITWaveJetpackS = Sprite.Create(texITWaveJetpack, WRect.rec64, WRect.half);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveJetpackUI;
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Flight";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies gain flight.";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveJetpackS;
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(190, 158, 202, 255);
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(190, 158, 202, 255);

            InfiniteTowerWaveCategory.WeightedWave ITBasicJetpack = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveJetpack, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicJetpack);
            //
            #endregion
            #region Malachite Elite Aspect
            //Malachite Elites
            GameObject InfiniteTowerWaveMalachitesOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElites", true);
            GameObject InfiniteTowerWaveMalachitesOnlyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElitesUI", false);

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.7f;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;  //Refuses to spawn anything on late waves if on
            InfiniteTowerWaveMalachitesOnly.AddComponent<SimuEquipmentWaveHelper>().variant = 2;

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMalachitesOnlyUI;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Malachite";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies spawn as weakened Malachites.";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            //InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITEliteMalachite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMalachitesOnly, weight = 4.5f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITEliteMalachite);
            #endregion
            #region Fuel Array
            //Battery
            GameObject InfiniteTowerWaveBattery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBattery", true);
            GameObject InfiniteTowerWaveBatteryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBatteryUI", false);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBattery.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBattery.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveBattery.AddComponent<SimuEquipmentWaveHelper>().variant = 1;

            Texture2D texItEquipmentBasic = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texItEquipmentBasic.LoadImage(Properties.Resources.texItEquipmentBasic, true);
            texItEquipmentBasic.filterMode = FilterMode.Bilinear;
            Sprite texItEquipmentBasicS = Sprite.Create(texItEquipmentBasic, WRect.rec64, WRect.half);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBatteryUI;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Battery";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Improper storage may cause serious harm.";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicBattery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBattery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicBattery);

            On.EntityStates.QuestVolatileBattery.CountDown.OnEnter += FixSizeOnEnemies;
            On.EntityStates.QuestVolatileBattery.CountDown.Detonate += NerfDamageFromEnemies;
            #endregion
            #region
            //Goobo
            GameObject InfiniteTowerWaveGoobo = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveGoobo", true);
            GameObject InfiniteTowerWaveGooboUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveGooboUI", false);

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            //
            InfiniteTowerWaveGoobo.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
            InfiniteTowerWaveGoobo.AddComponent<DisableArtifactOfSwarms>();

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().baseCredits = 70;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            //InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 45;
            InfiniteTowerWaveGoobo.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveGoobo.AddComponent<SimuEquipmentWaveHelper>().variant = 3;

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveGooboUI;
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gummy";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters spawn gummy clones.";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0.5f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGoobo = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveGoobo, weight = 3f, prerequisites = SimuMain.StartWave15Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicGoobo);

            //FixGooboSwarms.Fix();
            #endregion
        }

        private static void NerfDamageFromEnemies(On.EntityStates.QuestVolatileBattery.CountDown.orig_Detonate orig, EntityStates.QuestVolatileBattery.CountDown self)
        {
            if (!self.networkedBodyAttachment.attachedBody)
            {
                return;
            }
            bool suicide = false;
            if (self.networkedBodyAttachment.attachedBody.teamComponent.teamIndex == TeamIndex.Monster)
            {
                suicide = true;
                float newHealth = (77 + 33 * TeamManager.instance.GetTeamLevel(TeamIndex.Player)) / 6; //*3
                self.networkedBodyAttachment.attachedBody.baseMaxHealth = newHealth;
                self.networkedBodyAttachment.attachedBody.levelMaxHealth = 0;
                self.networkedBodyAttachment.attachedBody.maxHealth = newHealth;
                self.networkedBodyAttachment.attachedBody.maxShield = 0;
            }
            orig(self);
            if (suicide && self.attachedHealthComponent)
            {
                self.attachedHealthComponent.Suicide();
            }
        }

        private static void FixSizeOnEnemies(On.EntityStates.QuestVolatileBattery.CountDown.orig_OnEnter orig, EntityStates.QuestVolatileBattery.CountDown self)
        {
            orig(self);
            if (self.vfxInstances.Length == 0)
            {
                if (!self.networkedBodyAttachment.attachedBody)
                {
                    return;
                }
                if (EntityStates.QuestVolatileBattery.CountDown.vfxPrefab && self.attachedCharacterModel)
                {
                    self.vfxInstances = new GameObject[1];
                    GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(EntityStates.QuestVolatileBattery.CountDown.vfxPrefab, self.networkedBodyAttachment.attachedBody.coreTransform);
                    gameObject.transform.localPosition = Vector3.zero;
                    gameObject.transform.localRotation = Quaternion.identity;
                    gameObject.transform.localScale = new Vector3(2.5f / gameObject.transform.lossyScale.x, 2.5f / gameObject.transform.lossyScale.y, 2.5f / gameObject.transform.lossyScale.z);
                    if (gameObject.transform.localScale.z < 1)
                    {
                        gameObject.transform.localScale = new Vector3(1, 1, 1); //Idk dude
                    }
                    gameObject.transform.GetChild(0).GetComponent<Light>().range *= 3;
                    gameObject.transform.GetChild(0).GetComponent<Light>().intensity *= 3;
                    gameObject.transform.GetChild(0).GetComponent<LightIntensityCurve>().timeMax /= 2;
                    self.vfxInstances[0] = gameObject;
                }
            }
        }
    }

}