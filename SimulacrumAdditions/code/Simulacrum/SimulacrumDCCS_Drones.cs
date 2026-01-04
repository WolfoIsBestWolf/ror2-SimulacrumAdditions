using RoR2;
//using System;
using UnityEngine.AddressableAssets;
using static SimulacrumAdditions.SimulacrumDCCS;
namespace SimulacrumAdditions
{
    public class SimulacrumDCCS_Drones
    {
        public static void SimulacrumDroneArtifactCategoryGlobal(int dr, int dr2)
        {
            //Add shop every stage with (REAL)?
            DirectorCard iscDroneCombinerStation = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("2eaec01927ea16245822dcb50080cba3"),
                selectionWeight = 3,
            };
            DirectorCard iscTripleDroneShop = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("5a86990b032424e48b4b8456f7d684c9"),
                selectionWeight = 3,
            };
            DirectorCard iscdronescrapper = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("d7e78d150bd132744934165e6471f5f6"),
                selectionWeight = 2,
            };
            dccsInfiniteTowerInteractables.AddCard(dr, iscTripleDroneShop);

        }

        public static void AddDroneRelatedToPrinter(DirectorCardCategorySelection dccs)
        {
            int dict = dccs.FindCategoryIndexByName("Duplicator");
            if (dict != -1)
            {
                dccs.AddCard(dict, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("d7e78d150bd132744934165e6471f5f6"),
                    selectionWeight = 10,
                });
                dccs.AddCard(dict, new DirectorCard
                {
                    spawnCardReference = new AssetReferenceT<SpawnCard>("2eaec01927ea16245822dcb50080cba3"),
                    selectionWeight = 8,
                });
            }

            //Add Combiners and Scrappers to all stages during Simu for better Reality support
            //dccs.AddCard();
        }

        public static void SimulacrumDroneArtifactCategory(int dr)
        {
            //Plains
            //Gunner, Healing, Transport, Cleanup, Junk, Emergency
            //
            //Goolake
            //Missile, Gunner, Healing
            //
            //Aphelian
            //Flame, Equipment, Transport,
            //
            //Frozenwall,
            //TC280, Freeze, Cleanup, Junk, Barrier, Gunner
            //
            //DampCave
            //Bombardment, Flame, Equipment, Emergency, Jail
            //
            //SkyMeadow
            //Bombardment, Equipment, Junk, Missile
            //
            //Moon2
            //Jail, Freeze, TC280, Barrier
            //
            #region DirectorCards
            #region Common
            DirectorCard iscBrokenDrone1 = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("8c0ba07e91edbbd4fa573a6702b0e49f"),
                selectionWeight = 5,
            };
            DirectorCard iscBrokenDrone2 = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("47129a0298c286f4bb01754a8aaa36b0"),
                selectionWeight = 5,
            };
            DirectorCard iscBrokenTurret1 = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("81e6491f830f9c143bb5954640a383b1"),
                selectionWeight = 4,
            };
            DirectorCard iscBrokenHaulerDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("d304fff1f19d4184bb1f9444df3c0837"),
                selectionWeight = 5,
            };
            DirectorCard iscBrokenJunkDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("d8aad1d9c0616c644869900039f7e3f3"),
                selectionWeight = 5,
            };
            #endregion
            #region Uncommon
            DirectorCard iscBrokenMissileDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("749e2ff7e1839074885efb0f82197ba7"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenFlameDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("592ddd0e913440844b42eff65663abda"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenEquipmentDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("690d26f2cae4e394184aec7506e4884d"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenEmergencyDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("36f38da2d5e04e44abb4f5ed9788bad9"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenJailerDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("4e0d52fe3545f474b9076987b6ac92ec"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenRechargeDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("5b6dafbe8f6447e49a151e62961f9f77"),
                selectionWeight = 3,
            };
            DirectorCard iscBrokenCleanupDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("f92a522e57b907d4b8585b495e706636"),
                selectionWeight = 3,
            };
            #endregion
            #region Legendary
            DirectorCard iscBrokenMegaDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("1439f6d216991ee469049c5ab7aff52e"),
                selectionWeight = 1,
            };
            DirectorCard iscBrokenCopycatDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("f6970c1e4b273ea4ca332eb714b8801d"),
                selectionWeight = 1,
            };
            DirectorCard iscBrokenBombardmentDrone = new DirectorCard
            {
                spawnCardReference = new AssetReferenceT<SpawnCard>("384bcd7226702fd45a431a2795ff3d01"),
                selectionWeight = 1,
            };
            #endregion
            #endregion
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenDrone1);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenDrone2);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenTurret1);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenHaulerDrone);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenJunkDrone);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenEmergencyDrone);
            dccsITGolemPlainsInteractablesW.AddCard(dr, iscBrokenCleanupDrone);

            dccsITGooLakeInteractablesW.AddCard(dr, iscBrokenDrone1);
            dccsITGooLakeInteractablesW.AddCard(dr, iscBrokenDrone2);
            dccsITGooLakeInteractablesW.AddCard(dr, iscBrokenTurret1);
            dccsITGooLakeInteractablesW.AddCard(dr, iscBrokenMissileDrone);

            dccsITAncientLoftInteractablesW.AddCard(dr, iscBrokenHaulerDrone);
            dccsITAncientLoftInteractablesW.AddCard(dr, iscBrokenFlameDrone);
            dccsITAncientLoftInteractablesW.AddCard(dr, iscBrokenEquipmentDrone);

            dccsITFrozenWallInteractablesW.AddCard(dr, iscBrokenMegaDrone);
            dccsITFrozenWallInteractablesW.AddCard(dr, iscBrokenCopycatDrone);
            dccsITFrozenWallInteractablesW.AddCard(dr, iscBrokenCleanupDrone);
            dccsITFrozenWallInteractablesW.AddCard(dr, iscBrokenJunkDrone);
            dccsITFrozenWallInteractablesW.AddCard(dr, iscBrokenDrone1);

            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenBombardmentDrone);
            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenFlameDrone);
            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenEmergencyDrone);
            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenEquipmentDrone);
            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenRechargeDrone);
            dccsITDampCaveInteractablesW.AddCard(dr, iscBrokenMissileDrone);

            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenBombardmentDrone);
            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenCopycatDrone);
            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenEquipmentDrone);
            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenDrone2);
            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenJunkDrone);
            dccsITSkyMeadowInteractablesW.AddCard(dr, iscBrokenMissileDrone);

            dccsITMoonInteractablesW.AddCard(dr, iscBrokenMegaDrone);
            dccsITMoonInteractablesW.AddCard(dr, iscBrokenCopycatDrone);
            dccsITMoonInteractablesW.AddCard(dr, iscBrokenJailerDrone);
            dccsITMoonInteractablesW.AddCard(dr, iscBrokenRechargeDrone);
        }

    }
}