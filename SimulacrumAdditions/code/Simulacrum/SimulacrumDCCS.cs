using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SimulacrumDCCS
    {

        //Simu Interactable DCCS
        public static DirectorCardCategorySelection dccsInfiniteTowerInteractables = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/ITAssets/dccsInfiniteTowerInteractables.asset").WaitForCompletion();
        public static DirectorCardCategorySelection dccsITGolemPlainsInteractablesW = null;
        public static DirectorCardCategorySelection dccsITGooLakeInteractablesW = null;
        public static DirectorCardCategorySelection dccsITAncientLoftInteractablesW = null;
        public static DirectorCardCategorySelection dccsITFrozenWallInteractablesW = null;
        public static DirectorCardCategorySelection dccsITDampCaveInteractablesW = null;
        public static DirectorCardCategorySelection dccsITSkyMeadowInteractablesW = null;
        public static DirectorCardCategorySelection dccsITMoonInteractablesW = null;

        public static InteractableSpawnCard iscVoidCoinBarrelITSacrifice;
        public static InteractableSpawnCard iscVoidSuppressorIT;

        public static void Start()
        {
            SimuDCCS();
            Enemies();
        }

        public static void CreditsRebalance(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            orig(self, sceneDirector);

            if (!SimulacrumExtrasHelper.shareSuitInstalled)
            {
                int players = Run.instance.participatingPlayerCount - 1;
                sceneDirector.interactableCredit += players * 150;
            }

            if (self.waveIndex < 40) //First 4 stages
            {
                sceneDirector.interactableCredit += 30; //630
            }
            else if (self.waveIndex >= 60)//Stage 7+
            {
                sceneDirector.interactableCredit = (int)(sceneDirector.interactableCredit * 0.55f); //450
            }
            else if (self.waveIndex >= 40) //Stage 5/6
            {
                sceneDirector.interactableCredit = (int)(sceneDirector.interactableCredit * 0.75f); //450
            }
            if (RunArtifactManager.instance.IsArtifactEnabled(Artifact_RealStages.ArtifactUseNormalStages))
            {
                sceneDirector.interactableCredit += 100;
            }
            Debug.Log("InfiniteTower " + sceneDirector.interactableCredit + " interactable credits. ");

            if (SceneInfo.instance)
            {
                var stageInfo = SceneInfo.instance.GetComponent<ClassicStageInfo>();
                if (stageInfo)
                {
                    //Share Suit fix
                    stageInfo.sceneDirectorInteractibleCredits = sceneDirector.interactableCredit;
                }
            }
        }


        public static void MakeITSand(bool makeITSand)
        {
            Material matShrineChanceSandy = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/ShrineChance/matShrineChanceSandy.mat").WaitForCompletion();
            Material matShrineBloodSandy = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/ShrineBlood/matShrineBloodSandy.mat").WaitForCompletion();
            Material matShrineCleanseSandy = Addressables.LoadAssetAsync<Material>(key: "RoR2/Base/ShrineCleanse/matShrineCleanseSandy.mat").WaitForCompletion();
            GameObject ShrineHealing = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ShrineHealing/ShrineHealing.prefab").WaitForCompletion();

            if (makeITSand)
            {
                Texture2D ITSand = Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/DLC1/itgoolake/texSand1SimpleInfiniteTower.png").WaitForCompletion();
                matShrineChanceSandy.SetTexture("_GreenChannelTex", ITSand);
                matShrineBloodSandy.SetTexture("_GreenChannelTex", ITSand);
                matShrineCleanseSandy.SetTexture("_GreenChannelTex", ITSand);

                ShrineHealing.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                ShrineHealing.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);

            }
            else
            {
                Texture2D Sand = Addressables.LoadAssetAsync<Texture2D>(key: "b33b64ab57a530f4592b379c3224ba4d").WaitForCompletion();
                matShrineChanceSandy.SetTexture("_GreenChannelTex", Sand);
                matShrineBloodSandy.SetTexture("_GreenChannelTex", Sand);
                matShrineCleanseSandy.SetTexture("_GreenChannelTex", Sand);

                ShrineHealing.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);
                ShrineHealing.transform.GetChild(0).GetChild(0).GetChild(2).gameObject.SetActive(false);
            }
        }


        public static void SimuDCCS()
        {
            //Make Soup
            InteractableSpawnCard SoupWhiteGreenISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupWhiteGreenISC.name = "iscSoupWhiteGreen";
            SoupWhiteGreenISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, WhiteToGreen");
            SoupWhiteGreenISC.sendOverNetwork = true;
            SoupWhiteGreenISC.hullSize = HullClassification.Golem;
            SoupWhiteGreenISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupWhiteGreenISC.requiredFlags = NodeFlags.None;
            SoupWhiteGreenISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupWhiteGreenISC.directorCreditCost = 1;
            SoupWhiteGreenISC.occupyPosition = true;
            SoupWhiteGreenISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupWhiteGreenISC.orientToFloor = true;
            SoupWhiteGreenISC.slightlyRandomizeOrientation = false;
            SoupWhiteGreenISC.skipSpawnWhenSacrificeArtifactEnabled = false;

            InteractableSpawnCard SoupGreenRedISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupGreenRedISC.name = "iscSoupGreenRed";
            SoupGreenRedISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, GreenToRed Variant");
            SoupGreenRedISC.sendOverNetwork = true;
            SoupGreenRedISC.hullSize = HullClassification.Golem;
            SoupGreenRedISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupGreenRedISC.requiredFlags = NodeFlags.None;
            SoupGreenRedISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupGreenRedISC.directorCreditCost = 1;
            SoupGreenRedISC.occupyPosition = true;
            SoupGreenRedISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupGreenRedISC.orientToFloor = true;
            SoupGreenRedISC.slightlyRandomizeOrientation = false;
            SoupGreenRedISC.skipSpawnWhenSacrificeArtifactEnabled = false;

            InteractableSpawnCard SoupRedWhiteISC = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            SoupRedWhiteISC.name = "iscSoupRedWhite";
            SoupRedWhiteISC.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/networkedobjects/LunarCauldron, RedToWhite Variant");
            SoupRedWhiteISC.sendOverNetwork = true;
            SoupRedWhiteISC.hullSize = HullClassification.Golem;
            SoupRedWhiteISC.nodeGraphType = MapNodeGroup.GraphType.Ground;
            SoupRedWhiteISC.requiredFlags = NodeFlags.None;
            SoupRedWhiteISC.forbiddenFlags = NodeFlags.NoChestSpawn;
            SoupRedWhiteISC.directorCreditCost = 1;
            SoupRedWhiteISC.occupyPosition = true;
            SoupRedWhiteISC.eliteRules = SpawnCard.EliteRules.Default;
            SoupRedWhiteISC.orientToFloor = true;
            SoupRedWhiteISC.slightlyRandomizeOrientation = false;
            SoupRedWhiteISC.skipSpawnWhenSacrificeArtifactEnabled = false;


            //To prevent infinite loops with Sacrifice
            InteractableSpawnCard VoidCoinBarrel = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidCoinBarrel/iscVoidCoinBarrel.asset").WaitForCompletion());
            VoidCoinBarrel.directorCreditCost = 1;
            VoidCoinBarrel.name = "iscVoidCoinBarrelIT";
            VoidCoinBarrel.skipSpawnWhenSacrificeArtifactEnabled = true;

            iscVoidCoinBarrelITSacrifice = Object.Instantiate(VoidCoinBarrel);
            iscVoidCoinBarrelITSacrifice.directorCreditCost = 1;
            iscVoidCoinBarrelITSacrifice.name = "iscVoidCoinBarrelITSacrifice";
            iscVoidCoinBarrelITSacrifice.skipSpawnWhenSacrificeArtifactEnabled = false;
            iscVoidCoinBarrelITSacrifice.weightScalarWhenSacrificeArtifactEnabled = 0.5f;

            InteractableSpawnCard iscGoldChestIT = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/Base/GoldChest/iscGoldChest.asset").WaitForCompletion());
            iscGoldChestIT.maxSpawnsPerStage = 1;
            iscGoldChestIT.name = "iscGoldChestIT";

            iscVoidSuppressorIT = Object.Instantiate(Addressables.LoadAssetAsync<InteractableSpawnCard>(key: "RoR2/DLC1/VoidSuppressor/iscVoidSuppressor.asset").WaitForCompletion());
            iscVoidSuppressorIT.directorCreditCost = 20;
            iscVoidSuppressorIT.name = "iscVoidSuppressorIT";


            /*
            --[0]--Chests--  wt:45
            [0] iscChest1  wt:240  minStage:0
            [1] iscChest2  wt:40  minStage:0
            [2] iscEquipmentBarrel  wt:20  minStage:0
            [3] iscTripleShop  wt:80  minStage:0
            [4] iscLunarChest  wt:10  minStage:0
            [5] iscCategoryChestDamage  wt:20  minStage:0
            [6] iscCategoryChestHealing  wt:20  minStage:0
            [7] iscCategoryChestUtility  wt:20  minStage:0
            [8] iscCasinoChest  wt:10  minStage:0
            [9] iscTripleShopEquipment  wt:20  minStage:0

            --[1]--Shrines--  wt:1
            [0] iscShrineBlood  wt:1  minStage:0

            --[2]--Rare--  wt:0.4
            [0] iscChest1Stealthed  wt:6  minStage:0
            [1] iscGoldChest  wt:2  minStage:0

            --[3]--Duplicator--  wt:8
            [0] iscDuplicator  wt:30  minStage:0
            [1] iscDuplicatorLarge  wt:6  minStage:0
            [2] iscDuplicatorMilitary  wt:1  minStage:0
            [3] iscDuplicatorWild  wt:2  minStage:0
            [4] iscScrapper  wt:12  minStage:0

            --[4]--Void Stuff--  wt:3
            [0] iscVoidChest  wt:1  minStage:0*/
 

           
            //To prevent softlocks with sacrfice or other credits manipulators
         
            //dccsInfiniteTowerInteractables.categories[0].cards[2].selectionWeight = 15; //Eq Barrel
            dccsInfiniteTowerInteractables.categories[0].cards[3].selectionWeight = 60; //Triple
            dccsInfiniteTowerInteractables.categories[0].cards[4].selectionWeight = 15; //Lunar Chest
            dccsInfiniteTowerInteractables.categories[0].cards[9].selectionWeight = 5; //Eq triple
            dccsInfiniteTowerInteractables.categories[0].cards = dccsInfiniteTowerInteractables.categories[0].cards.Remove(dccsInfiniteTowerInteractables.categories[0].cards[8]);
           //Removing Casino Chest -> Annoying to Simu
           //NOT ANYMORE Removing Category x3 -> Specific ones per stage (?)
            dccsInfiniteTowerInteractables.AddCard(0, new DirectorCard
			{
				spawnCard = VoidCoinBarrel,
				selectionWeight = 10,
			});
			dccsInfiniteTowerInteractables.AddCard(0, new DirectorCard
			{
                //Temporary Vendor
				spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "6df786822d3105e4e820c69e1ef94d16").WaitForCompletion(),
				selectionWeight = 15222,
			});
			dccsInfiniteTowerInteractables.categories[1].selectionWeight = 2f;

            dccsInfiniteTowerInteractables.categories[2].selectionWeight = 0.8f;
            dccsInfiniteTowerInteractables.categories[2].cards[0].minimumStageCompletions = 1;
            dccsInfiniteTowerInteractables.categories[2].cards[1].minimumStageCompletions = 1; //No red chest stage 1 ig
            dccsInfiniteTowerInteractables.AddCard(2, new DirectorCard
			{
				spawnCard = iscVoidCoinBarrelITSacrifice,
				selectionWeight = 1,
			});
            dccsInfiniteTowerInteractables.AddCard(2, new DirectorCard
			{
				spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidChest/iscVoidChestSacrificeOn.asset").WaitForCompletion(),
				selectionWeight = 1,
			});
            /*dccsInfiniteTowerInteractables.AddCard(2, new DirectorCard
            {
                spawnCard = iscVoidSuppressorIT,
                selectionWeight = 6
            });*/

            dccsInfiniteTowerInteractables.categories[3].cards[1].selectionWeight = 8;
            dccsInfiniteTowerInteractables.categories[3].cards[2].selectionWeight = 2;
            dccsInfiniteTowerInteractables.categories[3].cards[3].selectionWeight = 3;
            dccsInfiniteTowerInteractables.categories[3].cards[2].minimumStageCompletions = 1;
            dccsInfiniteTowerInteractables.categories[3].cards[3].minimumStageCompletions = 1;
            dccsInfiniteTowerInteractables.categories[3].cards[4].selectionWeight = 16;

            dccsInfiniteTowerInteractables.categories[4].selectionWeight = 7f;
            dccsInfiniteTowerInteractables.categories[4].cards[0].selectionWeight = 3;
            dccsInfiniteTowerInteractables.AddCard(4, new DirectorCard
			{
				spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/VoidTriple/iscVoidTriple.asset").WaitForCompletion(),
				selectionWeight = 3,
			});

            int drone = dccsInfiniteTowerInteractables.AddCategory("Drones", 0);

            dccsITGolemPlainsInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITGooLakeInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITAncientLoftInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITFrozenWallInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITDampCaveInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITSkyMeadowInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);
            dccsITMoonInteractablesW = UnityEngine.Object.Instantiate(dccsInfiniteTowerInteractables);

            dccsITGolemPlainsInteractablesW.name = "dccsITGolemPlainsInteractablesW";
            dccsITGooLakeInteractablesW.name = "dccsITGooLakeInteractablesW";
            dccsITAncientLoftInteractablesW.name = "dccsITAncientLoftInteractablesW";
            dccsITFrozenWallInteractablesW.name = "dccsITFrozenWallInteractablesW";
            dccsITDampCaveInteractablesW.name = "dccsITDampCaveInteractablesW";
            dccsITSkyMeadowInteractablesW.name = "dccsITSkyMeadowInteractablesW";
            dccsITMoonInteractablesW.name = "dccsITMoonInteractablesW";

            DirectorCard ADCategoryChest1Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestDamage.asset").WaitForCompletion(),
                selectionWeight = 60,
            };
            DirectorCard ADCategoryChest1Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestHealing.asset").WaitForCompletion(),
                selectionWeight = 60,
            };
            DirectorCard ADCategoryChest1Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CategoryChest/iscCategoryChestUtility.asset").WaitForCompletion(),
                selectionWeight = 60,
            };


            DirectorCard ADCategoryChest2Damage = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Damage.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ADCategoryChest2Healing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Healing.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ADCategoryChest2Utility = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/DLC1/CategoryChest2/iscCategoryChest2Utility.asset").WaitForCompletion(),
                selectionWeight = 10,
            };
            DirectorCard ADGreenMultiShop = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/TripleShopLarge/iscTripleShopLarge.asset").WaitForCompletion(),
                selectionWeight = 25,
            };

            DirectorCard ADAdaptiveChest = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/CasinoChest/iscCasinoChest.asset").WaitForCompletion(),
                selectionWeight = 20,
            };

            DirectorCard ADShrineChance = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChance.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineChanceSand = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChanceSandy.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineChanceSnowy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineChance/iscShrineChanceSnowy.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineCleanse = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanse.asset").WaitForCompletion(),
                selectionWeight = 4,
            };

            DirectorCard ADShrineHealing = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineHealing/iscShrineHealing.asset").WaitForCompletion(),
                selectionWeight = 3,
            };
            DirectorCard ADShrineOrder = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestack.asset").WaitForCompletion(),
                selectionWeight = 2,
            };
            DirectorCard ADSoupWhiteGreen = new DirectorCard
            {
                spawnCard = SoupWhiteGreenISC,
                selectionWeight = 20,
            };
            DirectorCard ADSoupGreenRed = new DirectorCard
            {
                spawnCard = SoupGreenRedISC,
                selectionWeight = 15,
            };
            DirectorCard ADSoupRedWhite = new DirectorCard
            {
                spawnCard = SoupRedWhiteISC,
                selectionWeight = 10,
            };

            DirectorCard ADShrineCleanseSand = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineCleanse/iscShrineCleanseSandy.asset").WaitForCompletion(),
                selectionWeight = 3,
            };

            DirectorCard ADShrineOrderSnow = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineRestack/iscShrineRestackSnowy.asset").WaitForCompletion(),
                selectionWeight = 2,
            };

            //GolemPlains (Healing)
            //GooLake (Damage)
            //AncientLoft (Utility)
            //FrozenWall (Utility)
            //DampCave (Damage)
            //SkyMeadow (Healing)
            //
            dccsITGolemPlainsInteractablesW.AddCard(0, ADCategoryChest1Healing);
            dccsITGolemPlainsInteractablesW.AddCard(0, ADCategoryChest2Healing);
            dccsITGolemPlainsInteractablesW.categories[1].selectionWeight = 5f;
            dccsITGolemPlainsInteractablesW.categories[1].cards[0] = ADShrineChance; //Healing Chance
            dccsITGolemPlainsInteractablesW.AddCard(1, ADShrineHealing);
            //
            dccsITGooLakeInteractablesW.AddCard(0, ADCategoryChest1Damage);
            dccsITGooLakeInteractablesW.AddCard(0, ADCategoryChest2Damage);
            dccsITGooLakeInteractablesW.AddCard(0, ADGreenMultiShop);
            dccsITGooLakeInteractablesW.categories[1].selectionWeight = 5f; //Blood Chance Cleanse
            dccsITGooLakeInteractablesW.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSandy.asset").WaitForCompletion();
            dccsITGooLakeInteractablesW.AddCard(1, ADShrineChanceSand);
            dccsITGooLakeInteractablesW.AddCard(1, ADShrineCleanseSand);
            //
            dccsITAncientLoftInteractablesW.AddCard(0, ADCategoryChest1Utility);
            dccsITAncientLoftInteractablesW.AddCard(0, ADCategoryChest2Utility);
            dccsITAncientLoftInteractablesW.categories[0].cards[8].selectionWeight = 30;
            dccsITAncientLoftInteractablesW.categories[1].selectionWeight = 7f; //Healing Cleanse
            dccsITAncientLoftInteractablesW.categories[1].cards[0] = ADShrineCleanse;
            dccsITAncientLoftInteractablesW.AddCard(1, ADShrineHealing);
            dccsITAncientLoftInteractablesW.AddCard(1, ADShrineChance);
            //
            dccsITFrozenWallInteractablesW.AddCard(0, ADCategoryChest1Utility);
            dccsITFrozenWallInteractablesW.AddCard(0, ADCategoryChest2Utility);
            dccsITFrozenWallInteractablesW.AddCard(0, ADAdaptiveChest);
            dccsITFrozenWallInteractablesW.categories[1].selectionWeight = 5f; //Blood Chance Order
            dccsITFrozenWallInteractablesW.categories[1].cards[0].spawnCard = Addressables.LoadAssetAsync<SpawnCard>(key: "RoR2/Base/ShrineBlood/iscShrineBloodSnowy.asset").WaitForCompletion();
            dccsITFrozenWallInteractablesW.AddCard(1, ADShrineChanceSnowy);
            dccsITFrozenWallInteractablesW.AddCard(1, ADShrineOrderSnow);
            //
            dccsITDampCaveInteractablesW.AddCard(0, ADCategoryChest1Damage);
            dccsITDampCaveInteractablesW.AddCard(0, ADCategoryChest2Damage);
            dccsITDampCaveInteractablesW.AddCard(0, ADGreenMultiShop);
            dccsITDampCaveInteractablesW.AddCard(0, ADAdaptiveChest);
            dccsITDampCaveInteractablesW.categories[1].selectionWeight = 5f; //Blood Healing
            dccsITDampCaveInteractablesW.AddCard(1, ADShrineHealing);
            dccsITDampCaveInteractablesW.AddCard(1, ADShrineChance);
            //
            dccsITSkyMeadowInteractablesW.AddCard(0, ADCategoryChest1Healing);
            dccsITSkyMeadowInteractablesW.AddCard(0, ADCategoryChest2Healing);
            dccsITSkyMeadowInteractablesW.AddCard(0, ADGreenMultiShop);
            dccsITSkyMeadowInteractablesW.categories[1].selectionWeight = 5f; //Blood Chance
            dccsITSkyMeadowInteractablesW.AddCard(1, ADShrineChance);
            //
            //More Rares Lunars and Voids
            dccsITMoonInteractablesW.AddCard(0, ADGreenMultiShop);
            dccsITMoonInteractablesW.categories[0].cards[4].selectionWeight = 60; //Lunar Chest
            dccsITMoonInteractablesW.categories[0].cards = dccsITMoonInteractablesW.categories[0].cards.Remove(dccsITMoonInteractablesW.categories[0].cards[5]);
            dccsITMoonInteractablesW.categories[1].selectionWeight = 3f; // Order
            dccsITMoonInteractablesW.categories[1].cards[0] = ADShrineOrder;
            dccsITMoonInteractablesW.categories[2].selectionWeight *= 4;
            dccsITMoonInteractablesW.categories[2].cards = dccsITMoonInteractablesW.categories[2].cards.Remove(dccsITMoonInteractablesW.categories[2].cards[0]); //No Cloaked
            dccsITMoonInteractablesW.categories[2].cards[0].spawnCard = iscGoldChestIT;
            dccsITMoonInteractablesW.categories[3].selectionWeight += 4; //Soups
            dccsITMoonInteractablesW.categories[3].cards = dccsITMoonInteractablesW.categories[3].cards.Remove(dccsITMoonInteractablesW.categories[3].cards[3], dccsITMoonInteractablesW.categories[3].cards[2], dccsITMoonInteractablesW.categories[3].cards[1], dccsITMoonInteractablesW.categories[3].cards[0]);
            dccsITMoonInteractablesW.AddCard(3, ADSoupRedWhite);
            dccsITMoonInteractablesW.AddCard(3, ADSoupGreenRed);
            dccsITMoonInteractablesW.AddCard(3, ADSoupWhiteGreen);
            dccsITMoonInteractablesW.categories[4].selectionWeight += 4; //More Void StuffS
            if (dccsITMoonInteractablesW.categories[2].cards.Length >= 3)
            {
                dccsITMoonInteractablesW.categories[2].cards[1].selectionWeight = 4;
            }
            SimulacrumDroneArtifactCategory(drone);

		}

        public static void SimulacrumDroneArtifactCategory(int dr)
        {
            //dccsITMoonInteractablesW.AddCard(dr, null);

		}


        public static void SimuInteractableDCCSAdder(On.RoR2.InfiniteTowerRun.orig_OnPrePopulateSceneServer orig, InfiniteTowerRun self, SceneDirector sceneDirector)
        {
            orig(self, sceneDirector);
            //Debug.LogWarning(self.sceneDirectorInteractibleCredits);

            if (Run.instance && SceneInfo.instance)
            {
                //Debug.Log("Running");
                switch (SceneInfo.instance.sceneDef.baseSceneName)
                {
                    case "itgolemplains":
                        ClassicStageInfo.instance.interactableCategories = dccsITGolemPlainsInteractablesW;
                        break;
                    case "itgoolake":
                        ClassicStageInfo.instance.interactableCategories = dccsITGooLakeInteractablesW;
                        break;
                    case "itancientloft":
                        ClassicStageInfo.instance.interactableCategories = dccsITAncientLoftInteractablesW;
                        break;
                    case "itfrozenwall":
                        ClassicStageInfo.instance.interactableCategories = dccsITFrozenWallInteractablesW;
                        break;
                    case "itdampcave":
                        ClassicStageInfo.instance.interactableCategories = dccsITDampCaveInteractablesW;
                        break;
                    case "itskymeadow":
                        ClassicStageInfo.instance.interactableCategories = dccsITSkyMeadowInteractablesW;
                        break;
                    case "itmoon":
                        ClassicStageInfo.instance.interactableCategories = dccsITMoonInteractablesW;
                        sceneDirector.interactableCredit += 50;
                        break;
                }
            }
        }

        public static void Stage_ExtraObjects(On.RoR2.SceneDirector.orig_Start orig, global::RoR2.SceneDirector self)
        {
            orig(self);
            if (!SceneInfo.instance)
            {
                Debug.LogWarning("How no SceneInfo.instance??");
                return;
            }

            if (SceneInfo.instance.sceneDef.baseSceneName.StartsWith("itmoon"))
            {
                SceneInfo.instance.gameObject.AddComponent<SetGravity>().newGravity = -20f;
                GameObject MoonArenaDynamicPillar = GameObject.Find("/HOLDER: Stage");
                if (MoonArenaDynamicPillar)
                {
                    Vector3 mooncolumnlocalpos = new Vector3(7.2f, -1.08f, 0f);
                    Vector3 mooncolumnrotation = new Vector3(270.0198f, 0f, 0f);
                    Vector3 mooncolumnlocalscale = new Vector3(1f, 1f, 1f);

                    Vector3 borderScale = new Vector3(1.774f, 0.32f, 1.774f);

                    MoonArenaDynamicPillar.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                    for (int i = 0; i < MoonArenaDynamicPillar.transform.GetChild(1).GetChild(0).childCount; i++)
                    {
                        MoonArenaDynamicPillar.transform.GetChild(1).GetChild(0).GetChild(i).localScale = borderScale;
                    }

                    MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localPosition = mooncolumnlocalpos;
                    MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localEulerAngles = mooncolumnrotation;
                    MoonArenaDynamicPillar.transform.GetChild(2).GetChild(0).localScale = mooncolumnlocalscale;

                    MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
                    MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localPosition = mooncolumnlocalpos;
                    MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localEulerAngles = mooncolumnrotation;
                    MoonArenaDynamicPillar.transform.GetChild(3).GetChild(0).localScale = mooncolumnlocalscale;

                    MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                    MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localPosition = mooncolumnlocalpos;
                    MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localEulerAngles = mooncolumnrotation;
                    MoonArenaDynamicPillar.transform.GetChild(4).GetChild(0).localScale = mooncolumnlocalscale;

                    MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
                    MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localPosition = mooncolumnlocalpos;
                    MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localEulerAngles = mooncolumnrotation;
                    MoonArenaDynamicPillar.transform.GetChild(5).GetChild(0).localScale = mooncolumnlocalscale;
                }
            }
            else if (SceneInfo.instance.sceneDef.baseSceneName.StartsWith("itdampcave"))
            {
                GameObject GeyserHolder = GameObject.Find("/HOLDER: Geyser");
                if (GeyserHolder)
                {
                    //Up to side path when fall of chain : Random Ass unused Geyser
                    GeyserHolder.transform.GetChild(4).gameObject.SetActive(true);
                    GeyserHolder.transform.GetChild(4).position = new Vector3(-75.6025f, -182.2917f, -247.026f); //-75.6025 -178.9915 -247.026;

                    //Up to platform 2 incase you fall off : Using preexisting unused Geyser
                    GeyserHolder.transform.GetChild(5).gameObject.SetActive(true);
                    GeyserHolder.transform.GetChild(5).position = new Vector3(46f, -179.5f, -59f); //-75.6025 -178.9915 -247.026;
                    GeyserHolder.transform.GetChild(5).GetChild(2).GetComponent<JumpVolume>().jumpVelocity = new Vector3(10f, 80f, -10);
                }
            }
            else if (SceneInfo.instance.sceneDef.baseSceneName.StartsWith("itancientloft"))
            {
                GameObject Terrain = GameObject.Find("/HOLDER: Terrain");
                MeshRenderer render = Terrain.transform.GetChild(6).GetChild(4).GetComponent<MeshRenderer>();
                render.materials = new Material[] { render.material };
            }
        }



        public static void Enemies()
        {
            DirectorCardCategorySelection dccsITGolemplainsMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgolemplains/dccsITGolemplainsMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITGooLakeMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgoolake/dccsITGooLakeMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITAncientLoftMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itancientloft/dccsITAncientLoftMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITFrozenWallMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itfrozenwall/dccsITFrozenWallMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITDampCaveMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itdampcave/dccsITDampCaveMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITSkyMeadowMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itskymeadow/dccsITSkyMeadowMonsters.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITMoonMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itmoon/dccsITMoonMonsters.asset").WaitForCompletion();

            DirectorCardCategorySelection dccsITGolemplainsMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgolemplains/dccsITGolemplainsMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsITGooLakeMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itgoolake/dccsITGooLakeMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsITAncientLoftMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itancientloft/dccsITAncientLoftMonsters.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsITFrozenWallMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itfrozenwall/dccsITFrozenWallMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITDampCaveMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itdampcave/dccsITDampCaveMonstersDLC2.asset").WaitForCompletion();
            DirectorCardCategorySelection dccsITSkyMeadowMonstersDLC2 = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itskymeadow/dccsITSkyMeadowMonstersDLC2.asset").WaitForCompletion();
            //DirectorCardCategorySelection dccsITMoonMonsters = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC1/itmoon/dccsITMoonMonsters.asset").WaitForCompletion();


            //PreLoop
            DirectorCard SimuBrass = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard SimuElectricWorm = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscElectricWorm"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };


            //Some of these are meant to be with LittleGameplayTweaks but kinda don't care
            DirectorCard SimuLoopVulture = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVulture"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopGrovetender = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Gravekeeper/cscGravekeeper.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard SimuLoopGreaterWisp = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/GreaterWisp/cscGreaterWisp.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopGolemSandy = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Golem/cscGolemSandy.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopAcidLarva = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/AcidLarva/cscAcidLarva.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            //Void Stage 4 rest stage 3
            DirectorCard SimuLoopVoidBarnacleOnWalls = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacle.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard SimuLoopVoidBarnacle = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidBarnacle/cscVoidBarnacleNoCast.asset").WaitForCompletion(),
                selectionWeight = 2,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far
            };
            DirectorCard SimuLoopVoidReaver = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                selectionWeight = 4,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 3,
                preventOverhead = true,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopMiniMushroom = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMiniMushroom"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            /*DirectorCard SimuLoopClayGrenadier = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };*/
            DirectorCard SimuLoopHermitCrab = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscHermitCrab"),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            DirectorCard SimuLoopMinorConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMinorConstruct.asset").WaitForCompletion(),
                selectionWeight = 2,
                preventOverhead = true,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopMegaConstruct = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/MajorAndMinorConstruct/cscMegaConstruct.asset").WaitForCompletion(),
                selectionWeight = 2,
                preventOverhead = true,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopElderLemurian = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser"),
                preventOverhead = false,
                selectionWeight = 2,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopJellyfish = new DirectorCard
            {
                spawnCard = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish"),
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopRoboBallBoss = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscRoboBallBoss.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard SimuLoopcscRoboBallMini = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscRoboBallMini.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscChild = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Child/cscChild.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscParent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Parent/cscParent.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscGrandparent = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Grandparent/cscGrandparent.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCard cscBison = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bison/cscBison.asset").WaitForCompletion(),
                selectionWeight = 1,
                preventOverhead = false,
                minimumStageCompletions = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            //Minimum Stage completion 3 is wave 31

            //0  0 - 10
            //1 10 - 20
            //2 20 - 30
            //3 30 - 40
            //4 40 - 50

            #region Plains
            dccsITGolemplainsMonsters.AddCard(0, SimuLoopMegaConstruct);
            dccsITGolemplainsMonsters.AddCard(2, SimuLoopMinorConstruct);
            dccsITGolemplainsMonsters.AddCard(2, SimuLoopHermitCrab);

            dccsITGolemplainsMonsters.AddCard(0, cscGrandparent);
            dccsITGolemplainsMonsters.AddCard(1, cscParent);
            dccsITGolemplainsMonsters.AddCard(1, cscBison);

            dccsITGolemplainsMonstersDLC2.categories[0].cards[0].minimumStageCompletions = 2; //Halc
            dccsITGolemplainsMonstersDLC2.AddCard(0, cscChild);//Pre-loop

            #endregion
            #region Aquaduct
            //dccsITGooLakeMonsters.AddCard(0, SimuLoopVagrant);  //Simu thing where it imitates other stages
            //dccsITGooLakeMonsters.AddCard(1, SimuLoopGup);  //Simu thing where it imitates other stages
            dccsITGooLakeMonsters.AddCard(1, SimuLoopGolemSandy);
            dccsITGooLakeMonsters.AddCard(1, SimuLoopAcidLarva);
            //Has Loop Templar by default
            #endregion
            #region Ancient Loft
            dccsITAncientLoftMonsters.AddCard(1, SimuLoopElderLemurian); //Match my changes
            dccsITAncientLoftMonsters.AddCard(2, SimuLoopJellyfish); //More Jellyfish Ig?
            #endregion
            #region Rallypoint 
            dccsITFrozenWallMonsters.AddCard(0, SimuLoopRoboBallBoss); //Match my changes
            dccsITFrozenWallMonsters.AddCard(1, SimuLoopcscRoboBallMini);
            dccsITFrozenWallMonsters.AddCard(2, SimuLoopVulture);  //Match my changes
            dccsITFrozenWallMonsters.categories[1].cards[2].selectionWeight = 2; //More Bison
            dccsITFrozenWallMonsters.AddCard(0, SimuElectricWorm); //Pre-loop
            #endregion
            #region Abyss
            dccsITDampCaveMonsters.AddCard(0, SimuLoopGrovetender); //Simu thing where it imitates other stages
            dccsITDampCaveMonsters.AddCard(1, SimuLoopMiniMushroom); //Simu thing where it imitates other stages
            dccsITDampCaveMonsters.AddCard(1, SimuLoopAcidLarva); //Simu thing where it imitates other stages
            dccsITDampCaveMonsters.categories[2].cards[3].spawnDistance = DirectorCore.MonsterSpawnDistance.Close; //Hermit Crab
            dccsITDampCaveMonsters.AddCard(2, SimuLoopHermitCrab);
            dccsITDampCaveMonsters.AddCard(0, SimuElectricWorm);

            dccsITDampCaveMonstersDLC2.categories[0].cards[0].minimumStageCompletions = 2; //Earlier Halcy      
            #endregion
            #region Sky Meadow
            dccsITSkyMeadowMonsters.AddCard(2, SimuBrass);  //Pre loop
            dccsITSkyMeadowMonsters.AddCard(1, SimuLoopGreaterWisp);
            dccsITSkyMeadowMonsters.categories[0].cards[1].selectionWeight = 2; //More GrandParents
            dccsITSkyMeadowMonsters.categories[1].cards[0].selectionWeight = 2; //More Parents

            dccsITSkyMeadowMonstersDLC2.categories[0].cards[0].selectionWeight = 2; //More Children
            #endregion
            #region Moon
            dccsITMoonMonsters.categories[0].selectionWeight = 3;
            dccsITMoonMonsters.AddCategory("Minibosses", 1);
            dccsITMoonMonsters.AddCard(1, SimuLoopVoidDevestator);
            dccsITMoonMonsters.AddCard(1, SimuLoopVoidReaver);
            dccsITMoonMonsters.AddCard(1, SimuLoopVoidJailer);
            dccsITMoonMonsters.AddCard(1, SimuLoopVoidBarnacle);
            dccsITMoonMonsters.AddCard(1, SimuLoopVoidBarnacleOnWalls);
            dccsITMoonMonsters.categories[0].cards[0].selectionWeight = 4;
            dccsITMoonMonsters.categories[0].cards[1].selectionWeight = 4;
            dccsITMoonMonsters.categories[0].cards[1].selectionWeight = 3;
            #endregion
            #region Void Late Stuff
            /*
            DirectorCard LateVoidReaver = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Nullifier/cscNullifier.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard LateVoidJailer = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 7,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            };
            DirectorCard LateVoidDevestator = new DirectorCard
            {
                spawnCard = Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidMegaCrab/cscVoidMegaCrab.asset").WaitForCompletion(),
                selectionWeight = 1,
                minimumStageCompletions = 8,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };

            if (WConfig.cfgVoidsEverywhere.Value)
            {
                dccsITGolemplainsMonsters.AddCategory("LateVoid", 1);
                dccsITGooLakeMonsters.AddCategory("LateVoid", 1);
                dccsITAncientLoftMonsters.AddCategory("LateVoid", 1);
                dccsITFrozenWallMonsters.AddCategory("LateVoid", 1);
                dccsITDampCaveMonsters.AddCategory("LateVoid", 1);
                dccsITSkyMeadowMonsters.AddCategory("LateVoid", 1);

                dccsITGolemplainsMonsters.AddCard(4, LateVoidReaver);
                dccsITGooLakeMonsters.AddCard(4, LateVoidReaver);
                dccsITAncientLoftMonsters.AddCard(4, LateVoidReaver);
                dccsITFrozenWallMonsters.AddCard(4, LateVoidReaver);
                dccsITDampCaveMonsters.AddCard(4, LateVoidReaver);
                dccsITSkyMeadowMonsters.AddCard(4, LateVoidReaver);

                dccsITGolemplainsMonsters.AddCard(4, LateVoidJailer);
                dccsITGooLakeMonsters.AddCard(4, LateVoidJailer);
                dccsITAncientLoftMonsters.AddCard(4, LateVoidJailer);
                dccsITFrozenWallMonsters.AddCard(4, LateVoidJailer);
                dccsITDampCaveMonsters.AddCard(4, LateVoidJailer);
                dccsITSkyMeadowMonsters.AddCard(4, LateVoidJailer);

                dccsITGolemplainsMonsters.AddCard(4, LateVoidDevestator);
                dccsITGooLakeMonsters.AddCard(4, LateVoidDevestator);
                dccsITAncientLoftMonsters.AddCard(4, LateVoidDevestator);
                dccsITFrozenWallMonsters.AddCard(4, LateVoidDevestator);
                dccsITDampCaveMonsters.AddCard(4, LateVoidDevestator);
                dccsITSkyMeadowMonsters.AddCard(4, LateVoidDevestator);
            }
            */
            #endregion
        }

    }
}