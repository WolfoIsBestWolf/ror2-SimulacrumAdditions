using BepInEx;
using R2API.Utils;
using RoR2;
using SimulacrumAdditions.Waves;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;

#pragma warning disable CS0618 // Type or member is obsolete
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
#pragma warning restore CS0618 // Type or member is obsolete
[module: UnverifiableCode]

namespace SimulacrumAdditions
{
    [BepInDependency("com.bepis.r2api")]
    [BepInPlugin("Wolfo.SimulacrumAdditions", "SimulacrumAdditions", "2.6.0")]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]

    public class SimuMain : BaseUnityPlugin
    {

        public void Awake()
        {
            Assets.Init(Info);
            WConfig.InitConfig();
            if (WConfig.cfgDumpInfo.Value)
            {
                DumpAllWaveInfo(Constant.ITBasicWaves);
                DumpAllWaveInfo(Constant.ITBossWaves);
            }
            ItemHelpers.MakeItems();
            Constant.MakeValues();

            SimuChanges();

            WavesMain.Start();
            GiantGup.Start();
            SuperMegaCrab.Start();

           
            ITRun_Hooks.AddHooks();
            Wave_Hooks.AddHooks();
            VoidSafeWard_Hooks.AddHooks();
            Hooks_Other.AddHooks();

            GameModeCatalog.availability.CallWhenAvailable(LateRunningMethod);

            ArtifactTweaks.Main();

            Artifact_OnlyAugments.MakeArtifact();
            Artifact_RealStages.MakeArtifact();
            Artifact_SimuDrones.MakeArtifact();

            VoidCoin.MakeVoidCoin();

            //Use Custom Simu Interactable DCCSs
            On.RoR2.SceneDirector.Start += SimulacrumDCCS.Stage_ExtraObjects;
            On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.SimuInteractableDCCSAdder;
            //More Interactables early on to get into it quicker
            if (WConfig.cfgSimuCreditsRebalance.Value)
            {
                On.RoR2.InfiniteTowerRun.OnPrePopulateSceneServer += SimulacrumDCCS.CreditsRebalance;
            }
            //

            //Doesn't need to ban world unique anymore anyways
            Addressables.LoadAssetAsync<BasicPickupDropTable>(key: "RoR2/Base/DuplicatorWild/dtDuplicatorWild.asset").WaitForCompletion().bannedItemTags = new ItemTag[0];
            On.RoR2.Run.BuildDropTable += SimuDropTableEdits;

            //What is this?
            On.RoR2.InfiniteTowerWaveController.HasFullProgress += (orig, self) =>
            {
                if (self._totalCreditsSpent == 0)
                {
                    return false;
                }
                return orig(self);
            };


            On.RoR2.UI.MainMenu.MainMenuController.Start += OneTimeLateRunner;

            Material matAncientLoft_Water = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/ancientloft/matAncientLoft_Water.mat").WaitForCompletion();
            matAncientLoft_Water.SetTextureScale("_FoamTex", new Vector2(20, 20));

        }

        private void SimuDropTableEdits(On.RoR2.Run.orig_BuildDropTable orig, Run self)
        {
            orig(self);
            if (self is InfiniteTowerRun)
            {
                if (RunArtifactManager.instance.IsArtifactEnabled(Artifact_SimuDrones.ArtifactDef))
                {
                    if (self.IsExpansionEnabled(WolfoLibrary.DLCS.DLC1))
                    {
                        self.availableTier3DropList.Add(PickupCatalog.FindPickupIndex(DLC1Content.Items.DroneWeapons.itemIndex));
                    }
                    if (self.IsExpansionEnabled(WolfoLibrary.DLCS.DLC3))
                    {
                        self.availableTier2DropList.Add(PickupCatalog.FindPickupIndex(DLC3Content.Items.DroneDynamiteDisplay.itemIndex));
                    }
                }
                self.availableBossDropList.Add(PickupCatalog.FindPickupIndex(RoR2Content.Items.Pearl.itemIndex));
            }
        }

        public void Start()
        {
            SimulacrumDCCS.Start();
            CrabActivateToTravel.Make();
            WConfig.RiskConfig();
        }



        private void OneTimeLateRunner(On.RoR2.UI.MainMenu.MainMenuController.orig_Start orig, RoR2.UI.MainMenu.MainMenuController self)
        {
            orig(self);
            On.RoR2.UI.MainMenu.MainMenuController.Start -= OneTimeLateRunner;

            CharacterSpawnCard cscScavLunarIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion());
            cscScavLunarIT.name = "cscScavLunarIT";
            cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
            Waves_SuperBoss.WaveBoss_ScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscScavLunarIT;

            LocalUser user = LocalUserManager.GetFirstLocalUser();
            if (user != null && user.userProfile != null)
            {
                if (user.userProfile.HasAchievement("GIANT_GUP_ACHIEVEMENT"))
                {
                    user.userProfile.AddUnlockToken("Logs.GupGiantBody.0");
                }
                if (user.userProfile.HasAchievement("VOIDSUPERMEGACRAB_ACHIEVEMENT"))
                {
                    user.userProfile.AddUnlockToken("Logs.VoidSuperMegaCrabBody.0");
                }

            }
        }



        public static void LateRunningMethod()
        {
            WavesMain.LateChanges();
            Constant.Late_MakeValues();
            AutoConfig.InitConfig();
            AutoConfig.ApplyConfig();
            RoR2Content.Items.BoostHp.hidden = true;
            if (WConfig.cfgDumpInfo.Value)
            {
                SimuMain.DumpAllWaveInfo(Constant.ITBasicWaves);
                SimuMain.DumpAllWaveInfo(Constant.ITBossWaves);
                SimuMain.DumpAllWaveInfo(Constant.ITSuperBossWaves);
                SimuMain.DumpAllWaveInfo(Constant.ITModSupportWaves);
            }
        }

        public static void SimuChanges()
        {
            if (!WConfig.cfgMusicChanges.Value)
                { return; }
            SceneDef itgolemplains = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgolemplains/itgolemplains.asset").WaitForCompletion();
            SceneDef itgoolake = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itgoolake/itgoolake.asset").WaitForCompletion();
            SceneDef itfrozenwall = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itfrozenwall/itfrozenwall.asset").WaitForCompletion();
            SceneDef itdampcave = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itdampcave/itdampcave.asset").WaitForCompletion();
            SceneDef itmoon = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/itmoon/itmoon.asset").WaitForCompletion();

            MusicTrackDef MusicVoidFields = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/MusicTrackDefs/muSong08.asset").WaitForCompletion();
            MusicTrackDef MusicVoidStage = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_08.asset").WaitForCompletion();
            MusicTrackDef MusicSnowyForest = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muGameplayDLC1_03.asset").WaitForCompletion();

            MusicTrackDef MTDSulfurBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();
            MusicTrackDef MTDShipgraveyardBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/Base/Common/MusicTrackDefs/muSong22.asset").WaitForCompletion();
            MusicTrackDef MTDPrelude = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muMenuDLC1.asset").WaitForCompletion();

            itgolemplains.mainTrack = MusicVoidFields;
            itfrozenwall.mainTrack = MusicSnowyForest;
            //itdampcave.mainTrack = MusicVoidStage;

            //GP : Thermodynamic Equilibrium 
            itgoolake.bossTrack = MTDSulfurBoss; //A Boat Made from a Sheet of Newspaper
            //AL //Having Fallen, It Was Blood 
            itfrozenwall.bossTrack = MTDShipgraveyardBoss; //Köppen As Fuck 
            //Damp //Hydrophobia 
            //Sky //Antarctic Oscillation
            itmoon.bossTrack = MTDPrelude;

 
            //Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/InfiniteTowerCurrentArtifactEnigmaWaveUI.prefab").WaitForCompletion()
        }


        public static void DumpAllWaveInfo(InfiniteTowerWaveCategory category)
        {
            Debug.Log("");
            Debug.Log("All Simulacrum Waves : " + category.name);
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                string text = "\n" +
                Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "\n" +
                Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token);
                Debug.Log(text);
            }
            Debug.Log("");
            Debug.Log("");
            //Debug.Log("All Simulacrum Waves : " + category.name);
            float totalWeight = 0;
            float totalWeightPre11 = 0;
            float totalWeightPre30 = 0;
            for (int i = 0; i < category.wavePrefabs.Length; i++)
            {
                totalWeight += category.wavePrefabs[i].weight;


                string tokenCredit = "Credits: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().baseCredits;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave > 0)
                {
                    tokenCredit += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave + " * Wave";
                }
                string tokenDropTable = "DropTable: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().rewardDropTable.name;
                if (category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>() && category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable)
                {
                    tokenDropTable += " + " + category.wavePrefabs[i].wavePrefab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable.name;
                }

                string token4 = "Prerequesites: ";
                if (category.wavePrefabs[i].prerequisites)
                {
                    token4 += category.wavePrefabs[i].prerequisites.name;
                }

                string text = "\n" +
                    " [" + i + "] " + category.wavePrefabs[i].wavePrefab.name + "\n" +
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token) + "\n" +
                    Language.GetString(category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token) + "\n" +
                    "Weight: " + category.wavePrefabs[i].weight + "\n" +
                    tokenCredit + "\n" +
                    "Immediate: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction + "\n" +
                    "WaveSeconds: " + category.wavePrefabs[i].wavePrefab.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds + "\n" +
                    tokenDropTable + "\n" +
                    tokenCredit + "\n" +
                    token4;

                if (category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards != null)
                {
                    text += "\nMonsterCards: " + category.wavePrefabs[i].wavePrefab.GetComponent<CombatDirector>().monsterCards.name;
                }
                Debug.Log(text);


                if (category.wavePrefabs[i].prerequisites is InfiniteTowerWaveCountPrerequisites && (category.wavePrefabs[i].prerequisites as InfiniteTowerWaveCountPrerequisites).minimumWaveCount < 11)
                {
                    totalWeightPre11 += category.wavePrefabs[i].weight;

                }
                if (category.wavePrefabs[i].prerequisites is InfiniteTowerWaveCountPrerequisites && (category.wavePrefabs[i].prerequisites as InfiniteTowerWaveCountPrerequisites).minimumWaveCount < 30)
                {
                    totalWeightPre30 += category.wavePrefabs[i].weight;
                }
                if (category.wavePrefabs[i].prerequisites == null || category.wavePrefabs[i].prerequisites is InfiniteTowerWaveArtifactPrerequisites)
                {
                    totalWeightPre11 += category.wavePrefabs[i].weight;
                    totalWeightPre30 += category.wavePrefabs[i].weight;
                }
            }


            float defaultWeightOG = category.wavePrefabs[0].weight;
            float defaultWeight = category.wavePrefabs[0].weight;

            Debug.Log("Total " + category.name + " Weight  pre 10: " + totalWeightPre11 +
             "  Extra Weight: " + (totalWeightPre11 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre11);

            Debug.Log("Total " + category.name + " Weight  pre 30: " + totalWeightPre30 +
             "  Extra Weight: " + (totalWeightPre30 - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeightPre30);

            defaultWeight = defaultWeightOG * Constant.DefaultWeightMultiplier1;
            totalWeight -= defaultWeight;

            Debug.Log("Total " + category.name + " Weight post 30: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);

            defaultWeight = defaultWeightOG * Constant.DefaultWeightMultiplier2;
            totalWeight -= defaultWeightOG * (Constant.DefaultWeightMultiplier2 - Constant.DefaultWeightMultiplier1);



            Debug.Log("Total " + category.name + " Weight post 50: " + totalWeight +
             "  Extra Weight: " + (totalWeight - defaultWeight) +
             "  Percent for Default: " + defaultWeight / totalWeight);
        }



    }


}