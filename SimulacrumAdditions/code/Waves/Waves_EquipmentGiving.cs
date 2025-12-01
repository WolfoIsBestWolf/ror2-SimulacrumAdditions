using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine;
namespace SimulacrumAdditions.Waves
{
    public class Waves_EquipmentGiving
    {
        internal static void MakeWaves()
        {
            #region Flying enemies (Milky Chris)
            //Flight
            GameObject InfiniteTowerWaveJetpack = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveFlight", true);
            GameObject InfiniteTowerWaveJetpackUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveJetpackUI", false);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveJetpack.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 0.75f;
            InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;
            InfiniteTowerWaveJetpack.AddComponent<SimuEquipmentWaveHelper>().variant = SimuEquipmentWaveHelper.Variant.Jetpack;
            InfiniteTowerWaveJetpack.AddComponent<SetGravity>().newGravity = -20;
            WavesMain.orangeWaves.Add(InfiniteTowerWaveJetpack);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveJetpackUI;
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_FLYING";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_FLYING";
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveJetpack.png");
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color32(190, 158, 202, 255);
            InfiniteTowerWaveJetpackUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(190, 158, 202, 255);

            InfiniteTowerWaveCategory.WeightedWave ITBasicJetpack = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveJetpack, weight = 6.5f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicJetpack);
            //
            #endregion
            #region Malachite Elite Aspect
            //Malachite Elites
            GameObject InfiniteTowerWaveMalachitesOnly = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveMalachiteElites", true);
            GameObject InfiniteTowerWaveMalachitesOnlyUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveMalachiteElitesUI", false);

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITBasicBonusGreen;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().baseCredits *= 0.5f;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().eliteBias = 80000;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;  //Refuses to spawn anything on late waves if on
            InfiniteTowerWaveMalachitesOnly.AddComponent<SimuEquipmentWaveHelper>().variant = SimuEquipmentWaveHelper.Variant.Malachite;

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMalachitesOnlyUI;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_MALACHITE";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_MALACHITE";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Addressables.LoadAssetAsync<Sprite>(key: "2a3ba62b56822304f89831856876a651").WaitForCompletion();
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITEliteMalachite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMalachitesOnly, weight = 4f, prerequisites = Constant.StartWave30Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITEliteMalachite);
            #endregion
            #region Fuel Array
            //Battery
            GameObject InfiniteTowerWaveBattery = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveBattery", true);
            GameObject InfiniteTowerWaveBatteryUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveBatteryUI", false);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBattery.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITSpecialEquipment;
            //InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBattery.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveBattery.AddComponent<SimuEquipmentWaveHelper>().variant = SimuEquipmentWaveHelper.Variant.FuelArray;
            WavesMain.orangeWaves.Add(InfiniteTowerWaveBattery);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBatteryUI;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_BATTERY";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_BATTERY";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveEquipment.png");
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(1f, 0.55f, 0.1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicBattery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBattery, weight = 4f, prerequisites = Constant.AfterWave5Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicBattery);
 
            On.EntityStates.QuestVolatileBattery.CountDown.Detonate += NerfDamageFromEnemies;
            #endregion
            #region Goobo
            //Goobo
            GameObject InfiniteTowerWaveGoobo = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveGoobo", true);
            GameObject InfiniteTowerWaveGooboUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveGooboUI", false);

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            //
            InfiniteTowerWaveGoobo.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = Constant.dtITSpecialEquipment;
            //InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveGoobo.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;
            InfiniteTowerWaveGoobo.AddComponent<DisableArtifactOfSwarms>();

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().baseCredits = 70;
            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;
            //InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 45;
            InfiniteTowerWaveGoobo.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveGoobo.AddComponent<SimuEquipmentWaveHelper>().variant = SimuEquipmentWaveHelper.Variant.GooboJr;
            WavesMain.orangeWaves.Add(InfiniteTowerWaveGoobo);

            InfiniteTowerWaveGoobo.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveGooboUI;
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_GOOBO";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_GOOBO";
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveEquipment.png");
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color(0.7f, 0.4f, 0.5f);
            InfiniteTowerWaveGooboUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color(0.7f, 0.4f, 0.5f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGoobo = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveGoobo, weight = 3f, prerequisites = Constant.StartWave15Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicGoobo);
 
            #endregion

            #region Twisted
            //Twisted
            GameObject InfiniteTowerWaveTwisted = PrefabAPI.InstantiateClone(Constant.BasicWave, "InfiniteTowerWaveTwisted", true);
            GameObject InfiniteTowerWaveTwistedUI = PrefabAPI.InstantiateClone(Constant.BasicWaveUI, "InfiniteTowerWaveTwistedUI", false);

            InfiniteTowerWaveTwisted.GetComponent<InfiniteTowerWaveController>().rewardDropTable = Constant.dtITWaveTier1;
            InfiniteTowerWaveTwisted.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveTwisted.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 4;
            InfiniteTowerWaveTwisted.GetComponent<InfiniteTowerWaveController>().rewardPickupPrefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FragmentPotentialPickup.prefab").WaitForCompletion();

            InfiniteTowerWaveTwisted.AddComponent<SimuEquipmentWaveHelper>().variant = SimuEquipmentWaveHelper.Variant.Twisted;

            InfiniteTowerWaveTwisted.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveTwistedUI;
            InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = "ITWAVE_NAME_BASIC_TWISTED";
            InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = "ITWAVE_DESC_BASIC_TWISTED";
            InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = Addressables.LoadAssetAsync<Sprite>(key: "RoR2/DLC2/Elites/EliteBead/texBuffEliteBeadIcon.png").WaitForCompletion();
            //InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(132,201,255,255);
            InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>().color = new Color32(132, 201, 255, 255);
            InfiniteTowerWaveTwistedUI.transform.GetChild(0).GetChild(2).GetComponent<Image>().color = new Color32(192, 228, 255, 255);

            InfiniteTowerWaveCategory.WeightedWave ITBasicTwisted = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveTwisted, weight = 3f, prerequisites = Constant.DLC2_StartWave15Prerequisite };
            Constant.ITBasicWaves.wavePrefabs = Constant.ITBasicWaves.wavePrefabs.Add(ITBasicTwisted);
 
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

        
    }

}