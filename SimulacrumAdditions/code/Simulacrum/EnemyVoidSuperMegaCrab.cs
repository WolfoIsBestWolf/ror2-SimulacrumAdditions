using R2API;
using RoR2;
using RoR2.Navigation;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SuperMegaCrab
    {
        //public static GameObject GupBody = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/GupBody");
        //public static GameObject GupMaster = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/GupMaster");
        public static GameObject SuperCrabBody = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/VoidMegaCrabBody"), "SuperVoidMegaCrabBody", true);
        public static GameObject SuperCrabMaster = R2API.PrefabAPI.InstantiateClone(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/VoidMegaCrabMaster"), "SuperVoidMegaCrabMaster", true);

        public static void Start()
        {
            //InfiniteTowerWaveBossSuperCrab
            GameObject InfiniteTowerWaveBossSuperCrab = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperVoidMegaCrab", true);
            GameObject InfiniteTowerWaveBossSuperCrabUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperVoidMegaCrabUI", false);
            CharacterSpawnCard cscSuperCrab;

            ContentAddition.AddBody(SuperCrabBody);
            ContentAddition.AddMaster(SuperCrabMaster);


            SuperCrabMaster.GetComponent<CharacterMaster>().bodyPrefab = SuperCrabBody;
            if (!WConfig.cfgNewEnemiesVisible.Value)
            {
                SuperCrabBody.GetComponent<DeathRewards>().logUnlockableDef = null;
            }


            CharacterBody CrabCharacterBody = SuperCrabBody.GetComponent<CharacterBody>();
            CrabCharacterBody.bodyFlags |= CharacterBody.BodyFlags.ImmuneToVoidDeath;

            LanguageAPI.Add("SUPERMEGACRAB_BODY_NAME", "Deepend Void Devastator", "en");
            CrabCharacterBody.baseNameToken = "SUPERMEGACRAB_BODY_NAME";

            Texture VoidSuperMegaCrabBody = Addressables.LoadAssetAsync<Texture>(key: "RoR2/DLC1/VoidSuperMegaCrabBody.png").WaitForCompletion();
            CrabCharacterBody.portraitIcon = VoidSuperMegaCrabBody;


            //Stats
            CrabCharacterBody.baseMaxHealth *= 0.9f; //Base Health is 2800*1.6 cuz Trans Shrimp
            CrabCharacterBody.baseDamage *= 0.25f; //Bro gets so many damage items
            CrabCharacterBody.attackSpeed *= 0.75f;
            CrabCharacterBody.baseJumpPower *= 1.5f;

            CrabCharacterBody.PerformAutoCalculateLevelStats();



            //Entity States
            //Crazy how much more effort it'd be to do it in the official way
            On.EntityStates.VoidMegaCrab.Weapon.FireCrabCannonBase.OnEnter += (orig, self) =>
            {
                if (self.characterBody.name.StartsWith("Super"))
                {
                    self.projectileCount += (int)(self.projectileCount*1.2f) + 1;
                }
                orig(self);
            };


            //Visuals
            Material matVoidSuperMegaCrabMetal = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabMetal.mat").WaitForCompletion();
            Material matVoidSuperMegaCrabShell = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matVoidSuperMegaCrabShell.mat").WaitForCompletion();
            GameObject mdlVoidSuperMegaCrab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/mdlVoidSuperMegaCrab.fbx").WaitForCompletion();


            GameObject mdlCrab = SuperCrabBody.transform.GetChild(0).GetChild(3).gameObject;
            mdlCrab.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
            mdlCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh = mdlVoidSuperMegaCrab.transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().sharedMesh;
            mdlCrab.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>().sharedMesh = mdlVoidSuperMegaCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>().sharedMesh;

            mdlCrab.GetComponent<ModelPanelParameters>().minDistance = 18;
            mdlCrab.GetComponent<ModelPanelParameters>().maxDistance = 30;

            CharacterModel crabModel = mdlCrab.GetComponent<CharacterModel>();
            crabModel.baseRendererInfos[2].defaultMaterial = matVoidSuperMegaCrabShell;
            crabModel.baseRendererInfos[3].defaultMaterial = matVoidSuperMegaCrabMetal;

            crabModel.baseRendererInfos[2].renderer = mdlCrab.transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
            crabModel.baseRendererInfos[3].renderer = mdlCrab.transform.GetChild(2).GetComponent<SkinnedMeshRenderer>();


            //Fix Armature
            //The bottom metal for some reason is tied to the hands
            //It's the mesh itself that seems fucked who the hell knows why

            //
            //Maybe Scav item granter or smth idk
            SuperCrabMaster.AddComponent<GivePickupsOnStart>().itemInfos = new GivePickupsOnStart.ItemInfo[] {
                new GivePickupsOnStart.ItemInfo { itemString = ("AdaptiveArmor"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("TeleportWhenOob"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ShieldOnly"), count = 1, },
                //new GivePickupsOnStart.ItemInfo { itemString = ("RandomDamageZone"), count = 6, },
                new GivePickupsOnStart.ItemInfo { itemString = ("CritGlassesVoid"), count = 100, },
                new GivePickupsOnStart.ItemInfo { itemString = ("BearVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("MissileVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("ElementalRingVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("EquipmentMagazineVoid"), count = 1, },
                new GivePickupsOnStart.ItemInfo { itemString = ("SlowOnHitVoid"), count = 1, }, //Tentabaubel duration stacks so keep it low
                new GivePickupsOnStart.ItemInfo { itemString = ("VoidMegaCrabItem"), count = 3, },

            };
            //SuperCrabMaster.AddComponent<GivePickupsOnStart>().equipmentString = "Tonic";

            //
            CharacterSpawnCard cscVoidMegaCrab = RoR2.LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVoidMegaCrab");
            cscSuperCrab = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscSuperCrab.name = "cscITVoidMegaCrabSuper";
            cscSuperCrab._loadout = cscVoidMegaCrab._loadout;
            cscSuperCrab.forbiddenFlags = cscVoidMegaCrab.forbiddenFlags;
            cscSuperCrab.occupyPosition = false;
            cscSuperCrab.prefab = SuperCrabMaster;
            cscSuperCrab.sendOverNetwork = true;
            cscSuperCrab.hullSize = HullClassification.BeetleQueen;
            cscSuperCrab.nodeGraphType = MapNodeGroup.GraphType.Ground;
            cscSuperCrab.requiredFlags = NodeFlags.None;
            cscSuperCrab.directorCreditCost = 6000;
            cscSuperCrab.occupyPosition = true;
            //

            //Spawns
            InfiniteTowerWaveBossSuperCrab.GetComponent<CombatDirector>().monsterCards = SimuWavesMisc.dccsVoidFamilyNoBarnacle;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 50;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 5;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscSuperCrab;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperCrab.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSuperVoid;
            InfiniteTowerWaveBossSuperCrab.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier3;
            InfiniteTowerWaveBossSuperCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath *= 2f;
            //
            Texture2D texITWaveSuperDevestator = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveSuperDevestator.LoadImage(Properties.Resources.texITWaveSuperDevestator, true);
            texITWaveSuperDevestator.filterMode = FilterMode.Bilinear;
            Sprite texITWaveSuperDevestatorS = Sprite.Create(texITWaveSuperDevestator, WRect.rec64, WRect.half);

            //Color GupColor = new Color32(255, 161, 15, 255);
            //Color GupColor = new Color32(255, 122, 104, 255); 
            Color Color = new Color(0.7f, 0.6278f, 1f, 1);
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSuperDevestatorS;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = Color;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = Color;

            InfiniteTowerWaveBossSuperCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossSuperCrabUI;
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Devastator";
            InfiniteTowerWaveBossSuperCrabUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Face the deepened Void Devastator.";
            //
            RoR2.InfiniteTowerWaveCategory.WeightedWave ITSuperCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperCrab, weight = 2.5f, prerequisites = SimuMain.Wave26OrGreaterPrerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITSuperCrab);
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITSuperCrab);

            //SimuMain.ITSuperBossWaves.wavePrefabs = new InfiniteTowerWaveCategory.WeightedWave[] { ITSuperCrab };

            /*ITSuperCrab.weight = 5000;
            ITSuperCrab.prerequisites = null;
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITSuperCrab); */
        }

    }



}