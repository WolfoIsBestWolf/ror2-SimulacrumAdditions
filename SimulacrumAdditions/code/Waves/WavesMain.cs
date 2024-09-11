using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class WavesMain
    {
        //public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;
        public static CardRandomizer CardRandomizerEquipmentDrones;
        public static InfiniteTowerExplicitSpawnWaveController DroneWave;
        public static MultiCSC CardRandomizerBasicGhost;
        public static MultiCSC CardRandomizerBossGhost;

        internal static void SuperBossWaves()
        {
            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_BROTHER", "Defeat the King of Nothing.", "en"); //Defeat Mithrix for a special reward.
            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_SCAV", "A taste of your own medicine.", "en"); //Defeat the Scavenger for a special reward.

            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            float ITSpecialBossWaveWeight = 2.5f;

            //            
            //Scav Lunar
            GameObject InfiniteTowerWaveBossScavLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossScavLunar", true);
            GameObject InfiniteTowerCurrentBossScavLunarWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossScavLunarWaveUI", false);

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 50;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 4;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath = 120;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().secondsAfterWave += 7;
            InfiniteTowerWaveBossScavLunar.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            InfiniteTowerWaveBossScavLunar.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
            InfiniteTowerWaveBossScavLunar.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossScavLunar.AddComponent<SimulacrumEliteWaves>().lunarOnly = true;

            Texture2D texITWaveScavLunarIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveScavLunarIcon.LoadImage(Properties.Resources.texITWaveScavLunarIcon, true);
            texITWaveScavLunarIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveScavLunarIconS = Sprite.Create(texITWaveScavLunarIcon, WRect.rec64, WRect.half);

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Twisted Scavenger";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Twisted Scavenger.";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveScavLunarIconS;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossScavLunar, weight = ITSpecialBossWaveWeight * 3f, prerequisites = SimuMain.StartWave35Prerequisite };
            //
            //
            //Voidling
            GameObject InfiniteTowerWaveBossVoidRaidCrab = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossVoidRaidCrab", true);
            GameObject InfiniteTowerCurrentBossVoidRaidWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossVoidRaidWaveUI", false);

            CharacterSpawnCard cscMiniVoidRaidCrabPhase3IT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase3.asset").WaitForCompletion());
            cscMiniVoidRaidCrabPhase3IT.name = "cscMiniVoidRaidCrabPhase3IT";
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscMiniVoidRaidCrabPhase3IT;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 0;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.75f;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<CombatDirector>().monsterCards = SuperMegaCrab.dccsVoidFamilyNoBarnacle;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().secondsAfterWave += 2;
            InfiniteTowerWaveBossVoidRaidCrab.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier2;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITVoid;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 160;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of「Voidling」";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "D??ef?eat the「Diviner of the Deep」";

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidRaidCrab, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = SimuMain.StartWave50Prerequisite };
            //
            //
            //Gold Titan
            GameObject InfiniteTowerWaveBossTitanGold = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossTitanGold", true);
            GameObject InfiniteTowerCurrentBossTitanGoldWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossTitanGoldWaveUI", false);


            CharacterSpawnCard cscTitanGoldIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscTitanGoldIT.name = "cscTitanGoldIT";
            cscTitanGoldIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscTitanGoldIT;

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossTitanGold.AddComponent<SimulacrumExtrasHelper>().newRadius = 110;
            InfiniteTowerWaveBossTitanGold.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossTitanGoldWaveUI;
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Aurelionite";
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Titanic Goldweaver.";

            Texture2D texITWaveTitanGoldIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveTitanGoldIcon.LoadImage(Properties.Resources.texITWaveTitanGoldIcon, true);
            texITWaveTitanGoldIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveTitanGoldIconS = Sprite.Create(texITWaveTitanGoldIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveTitanGoldIconS;
            //InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossTitanGold, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = SimuMain.StartWave25Prerequisite };
            //
            //
            //SEEKERS FALSE SON
            GameObject InfiniteTowerWaveBossFalseSon = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossFalseSon", true);
            GameObject InfiniteTowerCurrentBossFalseSonWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossFalseSonWaveUI", false);

            CharacterSpawnCard cscFalseSonIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscFalseSonIT.name = "cscFalseSonIT";
            cscFalseSonIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC2/FalseSonBoss/FalseSonBossMaster.prefab").WaitForCompletion();
            cscFalseSonIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 0 } };
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscFalseSonIT;

            //Could make it a gold fragment but idk
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossFalseSon.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossFalseSon.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/DLC2/meridian/dccsFalseSonBossPhase2.asset").WaitForCompletion();

            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0f;
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 0;
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossFalseSon.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossFalseSonWaveUI;
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the False Son";
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the mockery of a greater being.";

            Texture2D texITWaveFalseSonIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveFalseSonIcon.LoadImage(Properties.Resources.texITFalseSon, true);
            texITWaveFalseSonIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveFalseSonIconS = Sprite.Create(texITWaveFalseSonIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveFalseSonIconS;
            //InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.95f, 0.55f, 1);
            InfiniteTowerCurrentBossFalseSonWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossFalseSon = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossFalseSon, weight = ITSpecialBossWaveWeight, prerequisites = SimuMain.StartWave40PrerequisiteDLC2 };


            //
            //
            //Super Robo Baller
            GameObject InfiniteTowerWaveBossSuperRoboBallBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperRoboBallBoss", true);
            GameObject InfiniteTowerCurrentBossSuperRoboBallBossWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossSuperRoboBallBossWaveUI", false);

            CharacterSpawnCard cscSuperRoboBallBossIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscSuperRoboBallBoss.asset").WaitForCompletion());
            cscSuperRoboBallBossIT.name = "cscSuperRoboBallBossIT";
            cscSuperRoboBallBossIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscSuperRoboBallBossIT;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3; //Evens out at 400 for wave 50
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossSuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Alloy Worship Unit";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Friend of Vultures.";

            Texture2D texITWaveLunarEliteIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarEliteIcon.LoadImage(Properties.Resources.texITWaveLunarEliteIcon, true);
            texITWaveLunarEliteIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarEliteIconS = Sprite.Create(texITWaveLunarEliteIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperRoboBallBoss, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = SimuMain.StartWave25Prerequisite };


            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITBossFalseSon);
            ITBossTitanGold.weight = 0.3f;
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossTitanGold, ITBossFalseSon); //ITBossSuperRoboBallBoss

        }

        internal static void EliteWaves()
        {
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
            //
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
            //
            //
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

            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarElites, ITVoidElites);
        }

        internal static void SizeChangingWaves()
        {
            Texture2D texITWaveSizeSmall = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveSizeSmall.LoadImage(Properties.Resources.texITWaveSizeSmall, true);
            texITWaveSizeSmall.filterMode = FilterMode.Bilinear;
            Sprite texITWaveSizeSmallS = Sprite.Create(texITWaveSizeSmall, WRect.rec64, WRect.half);

            Texture2D texITWaveSizeBig = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveSizeBig.LoadImage(Properties.Resources.texITWaveSizeBig, true);
            texITWaveSizeBig.filterMode = FilterMode.Bilinear;
            Sprite texITWaveSizeBigS = Sprite.Create(texITWaveSizeBig, WRect.rec64, WRect.half);


            //SizeBigEnemies Buff
            GameObject InfiniteTowerWaveSizeBigEnemies = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeBigEnemies", true);
            GameObject InfiniteTowerWaveSizeBigEnemiesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeBigEnemiesUI", false);

            InfiniteTowerWaveSizeBigEnemies.AddComponent<SimuWaveSizeModifier>().sizeModifier = 1.65f;

            SimulacrumGiveItemsOnStart simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeBigEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITHealthScaling";
            simulacrumGiveItemsOnStart.count = 65;
            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().baseCredits = 130;

            InfiniteTowerWaveSizeBigEnemies.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSizeBigEnemiesUI;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Gigantism";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are larger.";
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeBigS;
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeBigEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeBigEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeBigEnemies, weight = 2f, prerequisites = SimuMain.AfterWave5Prerequisite };
           

            //SizeSmallEnemies
            GameObject InfiniteTowerWaveSizeSmallEnemies = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeSmallEnemies", true);
            GameObject InfiniteTowerWaveSizeSmallEnemiesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSizeSmallEnemiesUI", false);

            InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimuWaveSizeModifier>().sizeModifier = 0.65f;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "CutHp";
            simulacrumGiveItemsOnStart.count = 1;
            simulacrumGiveItemsOnStart.hideItem = true;

            simulacrumGiveItemsOnStart = InfiniteTowerWaveSizeSmallEnemies.AddComponent<SimulacrumGiveItemsOnStart>();
            simulacrumGiveItemsOnStart.itemString = "ITHealthScaling";
            simulacrumGiveItemsOnStart.count = 30;

            InfiniteTowerWaveSizeSmallEnemies.GetComponent<InfiniteTowerWaveController>().baseCredits = 180;

            InfiniteTowerWaveSizeSmallEnemies.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSizeSmallEnemiesUI;
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Smallness";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are tiny.";
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveSizeSmallS;
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.75f, 0.9f, 0.95f);
            InfiniteTowerWaveSizeSmallEnemiesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.75f, 0.9f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave SizeSmallEnemiesWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSizeSmallEnemies, weight = 1f, prerequisites = SimuMain.AfterWave5Prerequisite };
            //
            //
            //AlwaysJumping Buff
            GameObject InfiniteTowerWaveAlwaysJumping = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveAlwaysJumping", true);
            GameObject InfiniteTowerWaveAlwaysJumpingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveAlwaysJumpingUI", false);

            InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveAlwaysJumping>();
            //InfiniteTowerWaveAlwaysJumping.AddComponent<SimuWaveBouncyProjectiles>();
            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;

            InfiniteTowerWaveAlwaysJumping.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveAlwaysJumpingUI;
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Bounciness";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Everything is always jumping.";
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 1, 0.6f);
            InfiniteTowerWaveAlwaysJumpingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1, 0.6f);

            InfiniteTowerWaveCategory.WeightedWave AlwaysJumpingWave = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveAlwaysJumping, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SizeBigEnemiesWave, SizeSmallEnemiesWave, AlwaysJumpingWave);
        }


        public static void Start()
        {
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            //
            //
            //Infestor
            CharacterSpawnCard cscVoidInfestorIT;
            cscVoidInfestorIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/EliteVoid/cscVoidInfestor.asset").WaitForCompletion());
            cscVoidInfestorIT.name = "cscVoidInfestorIT";
            //cscVoidInfestorIT.directorCreditCost = 1;
            cscVoidInfestorIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 30 } };
            DirectorCard SimuWaveVoidInfestor = new DirectorCard
            {
                spawnCard = cscVoidInfestorIT,
                selectionWeight = 1,
                preventOverhead = true,
                minimumStageCompletions = 0,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            };
            DirectorCardCategorySelection dccsVoidInfestorOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsVoidInfestorOnly.AddCategory("Basic Monsters", 6);
            dccsVoidInfestorOnly.AddCard(0, SimuWaveVoidInfestor);
            dccsVoidInfestorOnly.name = "dccsVoidInfestorOnly";
            dccsVoidInfestorOnly.categories[0].cards[0].spawnCard = cscVoidInfestorIT;


            GameObject InfiniteTowerWaveBossVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossVoidElites", true);
            GameObject InfiniteTowerCurrentBossVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossVoidEliteWaveUI", false);
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscVoidInfestorIT;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 580;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.3f;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;
            InfiniteTowerWaveBossVoidElites.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            CombatDirector WaveVoidEliteDirector = InfiniteTowerWaveBossVoidElites.AddComponent<CombatDirector>();
            WaveVoidEliteDirector.monsterCredit = 550;
            WaveVoidEliteDirector.monsterCards = dccsVoidInfestorOnly;
            WaveVoidEliteDirector.skipSpawnIfTooCheap = false;
            WaveVoidEliteDirector.teamIndex = TeamIndex.Void;
            RangeFloat TempRangeFloatThing = new RangeFloat { max = 1, min = 0.5f };
            WaveVoidEliteDirector.moneyWaveIntervals = WaveVoidEliteDirector.moneyWaveIntervals.Add(TempRangeFloatThing);
            WaveVoidEliteDirector.creditMultiplier = 4;
            WaveVoidEliteDirector.maxSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minSeriesSpawnInterval += 0.5f;
            WaveVoidEliteDirector.maxRerollSpawnInterval += 0.5f;
            WaveVoidEliteDirector.minRerollSpawnInterval += 0.5f;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.VoidTier2;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITVoidInfestorWave;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidEliteWaveUI;
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Infestation";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Void Infestors is gathering.";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 1f, 1);
            
            InfiniteTowerWaveCategory.WeightedWave ITBossVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidElites, weight = 10f, prerequisites = SimuMain.AfterWave5Prerequisite}; //7 normally
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossVoidElites);
            //
            //
            //Mountain Boss
            GameObject InfiniteTowerWaveDoubleBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBoss", true);
            GameObject InfiniteTowerWaveDoubleBossUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBossUI", false);

            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDoubleBossUI;
            //InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsAfterWave = 14;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 40;
            InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().eliteBias = 0f;
            //InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = true;
            InfiniteTowerWaveDoubleBoss.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;

            Texture2D texITWaveBossIconMountain = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveBossIconMountain.LoadImage(Properties.Resources.texITWaveBossIconMountain, true);
            texITWaveBossIconMountain.filterMode = FilterMode.Bilinear;
            Sprite texITWaveBossIconMountainS = Sprite.Create(texITWaveBossIconMountain, WRect.rec64, WRect.half);

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconMountainS;
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Mountain";
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat twice as many enemies for twice the rewards.";

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.8f, 0.9f);
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.9f); //new Color(0.240566f, 0.8644956f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITDoubleBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDoubleBoss, weight = 10f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITDoubleBoss);
            //
            //
            //Wave with the drone in it
            GameObject InfiniteTowerWaveBossWithDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossWithDrone", true);
            GameObject InfiniteTowerWaveBossDronesMachinesUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossWithDroneUI", false);
 
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().baseCredits = 450f;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossCategoryUtility;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 50;
            InfiniteTowerWaveBossWithDrone.AddComponent<SimulacrumExtrasHelper>().newRadius = 95;
            DroneWave = InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>();
 
            InfiniteTowerWaveBossWithDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossDronesMachinesUI;

            //InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Machines";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Flames";
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Incineration Drones fight alongside monsters.";

            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 0.5f);
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.4f);
            InfiniteTowerWaveBossDronesMachinesUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.4f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITInfiniteTowerWaveBossWithDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossWithDrone, weight = 6f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITInfiniteTowerWaveBossWithDrone);

            Organized();
        }

 
        internal static void LateChanges()
        {
            MakeMultiCSCs.CreateEquipmentDroneSpawnCards();
            MakeMultiCSCs.CreateGhostSpawnCards();
            MakeMultiCSCs.CreateBossGhostSpawnCards();
            MakeMultiCSCs.CreateDroneSpawnCards();
            //Mod Support
            CharacterSpawnCard cscDireseeker = null;
            CharacterSpawnCard[] CSCList = Object.FindObjectsOfType(typeof(CharacterSpawnCard)) as CharacterSpawnCard[];
            for (var i = 0; i < CSCList.Length; i++)
            {
                //Debug.LogWarning(CSCList[i]);
                switch (CSCList[i].name)
                {
                    case "cscDireseeker":
                        cscDireseeker = CSCList[i];
                        break;
                }
            }

            for (int i = 0; i < SimuMain.ITModSupportWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITModSupportWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();

                switch (wave.name)
                {
                     case "InfiniteTowerWaveArtifactHonorAndBrigade":
                        ArtifactDef SingleEliteType = ArtifactCatalog.FindArtifactDef("SingleEliteType");
                        if (SingleEliteType)
                        {
                            wave.AddComponent<ArtifactEnabler>().artifactDef = SingleEliteType;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SingleEliteType.smallIconSelectedSprite;
                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    case "InfiniteTowerWaveSS2RainbowElites":
                        EquipmentDef rainbowEliteEquip = EquipmentCatalog.GetEquipmentDef(EquipmentCatalog.FindEquipmentIndex("AffixEmpyrean"));
                        if (rainbowEliteEquip)
                        {
                            ItemDef SS2_ITEM_BOOSTMOVESPEED_NAME = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("SS2_ITEM_BOOSTMOVESPEED_NAME"));
                            ItemDef SS2_ITEM_BOOSTCOOLDOWNS_NAME = ItemCatalog.GetItemDef(ItemCatalog.FindItemIndex("SS2_ITEM_BOOSTCOOLDOWNS_NAME"));

                            CharacterSpawnCard cscEmpyreanIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Bell/cscBell.asset").WaitForCompletion());
                            cscEmpyreanIT.name = "cscEmpyreanIT";
                            cscEmpyreanIT.noElites = true;
                            cscEmpyreanIT.itemsToGrant = new ItemCountPair[] {
                            new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 3950}, //1000
                            new ItemCountPair { itemDef = RoR2Content.Items.BoostHp, count = 10}, //
                            new ItemCountPair { itemDef = RoR2Content.Items.BoostDamage, count = 40}, //80
                            new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1},
                            new ItemCountPair { itemDef = RoR2Content.Items.TeleportWhenOob, count = 1},
                            new ItemCountPair { itemDef = SS2_ITEM_BOOSTMOVESPEED_NAME, count = 50}, //80
                            new ItemCountPair { itemDef = SS2_ITEM_BOOSTCOOLDOWNS_NAME, count = 100}, //80
                            };
                            cscEmpyreanIT.equipmentToGrant = new EquipmentDef[]
                            {
                            rainbowEliteEquip
                            };
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscEmpyreanIT;
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
                            controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = rainbowEliteEquip.passiveBuffDef.iconSprite;

                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }                       
                        break;
                    case "InfiniteTowerWaveArtifactSS2Cognation":
                        ArtifactDef SS2Cognation = ArtifactCatalog.FindArtifactDef("Cognation");
                        if (SS2Cognation)
                        {
                            wave.GetComponent<ArtifactEnabler>().artifactDef = SS2Cognation;
                            wave.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
 
                            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }
                        break;
                    case "InfiniteTowerWaveBossDireseeker":
                        if (cscDireseeker)
                        {
                            CharacterSpawnCard cscDireseekerIT = Object.Instantiate(cscDireseeker);
                            cscDireseekerIT.name = "cscDireseekerIT";
                            cscDireseekerIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscDireseekerIT;
                            wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;
                            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        }                
                        break;
                }

            }

            //
            Color newArticfact = new Color(1f, 0.7647f, 1.2647f, 1);
            for (int i = 0; i < SimuMain.ITBasicWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITBasicWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                //controller.wavePeriodSeconds = 25;
                if (controller.baseCredits > 199)
                {
                    //Stupid but whatevs
                    controller.baseCredits -= 14;
                }
                if (controller is InfiniteTowerExplicitSpawnWaveController)
                {
                    controller.isBossWave = false;
                    controller.uiPrefab = SimuMain.ITBasicWaves.wavePrefabs[0].wavePrefab.GetComponent<InfiniteTowerWaveController>().uiPrefab;
                    controller.wavePeriodSeconds = 60;
                }
                //
                ArtifactEnabler artifact = wave.GetComponent<ArtifactEnabler>();
                if (artifact)
                {
                    controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = newArticfact;
                }
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBasicEquipmentDrone":
                    case "InfiniteTowerWaveFlight":
                    case "InfiniteTowerWaveBattery":
                    case "InfiniteTowerWaveGoobo":
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
                        break;
                }
            }
            for (int i = 0; i < SimuMain.ITBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossEquipmentDrone":
                        wave.AddComponent<CardRandomizer>().cscList = CardRandomizerEquipmentDrones.cscList;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
                        break;
                }

                if (controller.maxSquadSize > 20)
                {
                    controller.maxSquadSize = 20;
                }
                if (controller.wavePeriodSeconds == 60)
                {
                    controller.wavePeriodSeconds = 50;
                }
            }

            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                if (SimuMain.ITSuperBossWaves.wavePrefabs[i].weight > 1)
                {
                    SimuMain.ITSuperBossWaves.wavePrefabs[i].weight = 1;
                }
                GameObject wave = SimuMain.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerExplicitSpawnWaveController temp;

                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossBrother":
                        wave.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
                        wave.GetComponent<SimulacrumExtrasHelper>().newRadius = 110;

                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.baseCredits = 50;
                        temp.immediateCreditsFraction = 0.5f;
                        temp.linearCreditsPerWave = 4;
                        temp.combatDirector.monsterCards = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/Base/Common/dccsLunarFamily.asset").WaitForCompletion(); ;
                        temp.secondsBeforeSuddenDeath *= 2;
                        break;
                    case "InfiniteTowerWaveBossScav":
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        temp.rewardDisplayTier = ItemTier.Boss;
                        temp.rewardDropTable = SimuMain.dtITSpecialBossYellow;
                        temp.baseCredits = 100;
                        temp.linearCreditsPerWave = 3;
                        temp.secondsBeforeSuddenDeath *= 2f;
                        CharacterSpawnCard cscScav = Object.Instantiate(temp.spawnList[0].spawnCard);
                        cscScav.name = "cscScavBossIT";
                        cscScav.itemsToGrant = new ItemCountPair[]
                        {
                            new ItemCountPair
                            {
                                count = 1,
                                itemDef = RoR2Content.Items.AdaptiveArmor,
                            }
                        };
                        temp.spawnList[0].spawnCard = cscScav;
                        break;
                   /*case "InfiniteTowerWaveBossScavLunar":
                        temp = wave.GetComponent<RoR2.InfiniteTowerExplicitSpawnWaveController>();
                        CharacterSpawnCard cscScavLunarIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion());
                        cscScavLunarIT.name = "cscScavLunarIT";
                        cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                        temp.spawnList[0].spawnCard = cscScavLunarIT;
                        break;*/
                }
            }
            SimuMain.ITSuperBossWaves.wavePrefabs[0].weight = 0.5f;

            if (WConfig.cfgMusicSuperBoss.Value)
            {
                AddMusic();
            }
        }

        internal static void AddMusic()
        {
            //Music
            SceneDef moon2 = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/moon2");
            SceneDef rootjungle = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/rootjungle");
            SceneDef voidraid = Addressables.LoadAssetAsync<SceneDef>(key: "RoR2/DLC1/voidraid/voidraid.asset").WaitForCompletion();
            SceneDef snowyforest = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/snowyforest");
            SceneDef goldshores = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/goldshores");
            SceneDef shipgraveyard = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/shipgraveyard");
            SceneDef skymeadow = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/skymeadow");
            SceneDef dampcavesimple = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/dampcavesimple");

            MusicTrackDef MTDSulfurBoss = Addressables.LoadAssetAsync<MusicTrackDef>(key: "RoR2/DLC1/Common/muBossfightDLC1_12.asset").WaitForCompletion();

            WwiseStateReference Phase1WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            //WwiseStateReference Phase2WW = Addressables.LoadAssetAsync<WwiseStateReference>(key: "Wwise/08652DAD-B715-437F-AB20-0254AD418B4D.asset").WaitForCompletion();
            //AK.Wwise.State VoidlingPhase1 = Addressables.LoadAssetAsync<AK.Wwise.State>(key: "Wwise/0DE64677-408F-42E3-8F5A-7246170C2CE9.asset").WaitForCompletion();
            AK.Wwise.State StatePhase1 = new AK.Wwise.State();
            StatePhase1.WwiseObjectReference = Phase1WW;

            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITSuperBossWaves.wavePrefabs[i].wavePrefab;
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBossBrother":
                        wave.AddComponent<MusicTrackOverride>().track = moon2.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "InfiniteTowerWaveBossVoidRaidCrab":
                        wave.AddComponent<MusicTrackOverride>().track = voidraid.bossTrack;
                        wave.AddComponent<AkState>().data = StatePhase1;
                        break;
                    case "InfiniteTowerWaveBossScavLunar":
                        wave.AddComponent<MusicTrackOverride>().track = snowyforest.bossTrack;
                        break;
                    case "InfiniteTowerWaveBossSuperVoidMegaCrab":
                        wave.AddComponent<MusicTrackOverride>().track = MTDSulfurBoss;
                        break;
                    case "InfiniteTowerWaveBossScav":
                        wave.AddComponent<MusicTrackOverride>().track = rootjungle.mainTrack;
                        break;
                    case "InfiniteTowerWaveBossTitanGold":
                        wave.AddComponent<MusicTrackOverride>().track = dampcavesimple.bossTrack;
                        break;
                    case "InfiniteTowerWaveBossSuperRoboBallBoss":
                        wave.AddComponent<MusicTrackOverride>().track = skymeadow.bossTrack;
                        break;
                }
            }
        }

        internal static void Organized()
        {
            ItemDef RedWhip = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SprintOutOfCombat");
            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef BoostAttackSpeed = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BoostAttackSpeed");
            ItemDef Hoof = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Hoof");
            ItemDef AdaptiveArmor = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AdaptiveArmor");
            ItemDef CutHP = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CutHP");
            ItemDef LunarBadLuck = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/LunarBadLuck");
            ItemDef AutoCastEquipment = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AutoCastEquipment");
            ItemDef ExtraLifeVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ExtraLifeVoid");
            ItemDef UseAmbientLevel = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel");
            ItemDef SecondarySkillMagazine = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SecondarySkillMagazine");
            ItemDef CritGlassesVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CritGlassesVoid");
            ItemDef AlienHead = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AlienHead");

            //
            //
            //Equipment Drone Boss        
            GameObject InfiniteTowerWaveBossEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEquipmentDrone", true);
            GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 2, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far }
            };

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveBossEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 90;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossEquipmentDroneWaveUI;

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Equipment Drones";
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Equipment Drones has been released from the focus.";

            Texture2D texITWaveBossIconEquipment = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveBossIconEquipment.LoadImage(Properties.Resources.texITEquipmentBoss, true);
            texITWaveBossIconEquipment.filterMode = FilterMode.Bilinear;
            Sprite texITWaveBossIconEquipmentS = Sprite.Create(texITWaveBossIconEquipment, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconEquipmentS;
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerCurrentBossEquipmentDroneWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEquipmentDrone, weight = 10f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEquipmentDrone);
            //
            On.EntityStates.Drone.DeathState.OnImpactServer += LeaveNoEquipmentDroneIT;
            //
            //
            
            //
            //
            //Ghost Haunting Boss
            GameObject InfiniteTowerWaveBossGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHaunting", true);
            GameObject InfiniteTowerWaveBossGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHauntingUI", false);

            CardRandomizerBossGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBossGhost.name = "MulticscITGhostBoss";
            CardRandomizerBossGhost.sendOverNetwork = true;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                spawnCard = CardRandomizerBossGhost,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Far,
                count = 1
            };

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBossGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 160;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Boss;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialBossYellow;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossGhostHauntingUI;
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Haunting";
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A strong unknown horror follows you.";

            Texture2D texITHauntedBoss = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITHauntedBoss.LoadImage(Properties.Resources.texITHauntedBoss, true);
            texITHauntedBoss.filterMode = FilterMode.Bilinear;
            Sprite texITHauntedBossS = Sprite.Create(texITHauntedBoss, WRect.rec64, WRect.half);

            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITHauntedBossS;
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossGhostHaunting, weight = 15f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossGhostHaunting);
            //
            //
            //Ghost Haunting Basic
            GameObject InfiniteTowerWaveBasicGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHaunting", true);
            GameObject InfiniteTowerWaveBasicGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHauntingUI", false);

            CardRandomizerBasicGhost = ScriptableObject.CreateInstance<MultiCSC>();
            CardRandomizerBasicGhost.name = "MulticscITGhost";
            CardRandomizerBasicGhost.sendOverNetwork = true;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = CardRandomizerBasicGhost;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBasicGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBasicGhostHauntingUI;
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Haunting";
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A unknown horror follows you.";

            Texture2D texITHaunted = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITHaunted.LoadImage(Properties.Resources.texITHaunted, true);
            texITHaunted.filterMode = FilterMode.Bilinear;
            Sprite texITHauntedS = Sprite.Create(texITHaunted, WRect.rec64, WRect.half);

            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITHauntedS;
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f, 0.85f, 1);
            InfiniteTowerWaveBasicGhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicGhostHaunting, weight = 10f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicGhostHaunting);
            //
            //
            //
            //Equipment Drone Basic
            GameObject InfiniteTowerWaveBasicEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDrone", true);
            GameObject InfiniteTowerWaveBasicEquipmentDroneUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDroneUI", false);

            CardRandomizerEquipmentDrones = InfiniteTowerWaveBasicEquipmentDrone.AddComponent<CardRandomizer>();

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = null;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Standard;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.12f;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 75;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;

            //InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBasicEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 75;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBasicEquipmentDroneUI;
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of the Equipment Drone";
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A Equipment Drone has been released from the focus.";

            Texture2D texItEquipmentBasic = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texItEquipmentBasic.LoadImage(Properties.Resources.texItEquipmentBasic, true);
            texItEquipmentBasic.filterMode = FilterMode.Bilinear;
            Sprite texItEquipmentBasicS = Sprite.Create(texItEquipmentBasic, WRect.rec64, WRect.half);

            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            InfiniteTowerWaveBasicEquipmentDroneUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicEquipmentDrone, weight = 14f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicEquipmentDrone);
            //
            //
            //Acrid Void Boss
            GameObject InfiniteTowerWaveBossCharacters = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossCharacters", true);
            GameObject InfiniteTowerWaveBossCharactersUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossCharactersUI", false);

            MultiCharacterSpawnCard cscITCharacter = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITCharacter.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/MageMonsterMaster"),
                LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/CrocoMonsterMaster"),
                Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorMonsterMaster.prefab").WaitForCompletion(),
            };
            cscITCharacter.name = "cscITCharacter";
            cscITCharacter.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = ExtraLifeVoid, count = 0 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 70 },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 820 }, //10x hp
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CloverVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ElementalRingVoid"), count = 1 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/EquipmentMagazineVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BearVoid"), count = 1 },
                //new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/MushroomVoid"), count = 1 },
                new ItemCountPair { itemDef = CritGlassesVoid, count = 1 },
                //new ItemCountPair { itemDef = AlienHead, count = 1 },
                new ItemCountPair { itemDef = AdaptiveArmor, count = 1 },
                new ItemCountPair { itemDef = UseAmbientLevel, count = 1 },
            };

            CharacterSpawnCard cscNullifierIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscNullifierIT.name = "cscNullifierIT";
            cscNullifierIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = BoostAttackSpeed, count = 2 },
                new ItemCountPair { itemDef = CutHP, count = 2 },
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscITCharacter, count = 0, spawnDistance = DirectorCore.MonsterSpawnDistance.Far},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscNullifierIT, count = 2},
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossCharacters.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 50;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;

            InfiniteTowerWaveBossCharacters.AddComponent<SimuWaveSizeModifier>().sizeModifier = 2f;
            InfiniteTowerWaveBossCharacters.GetComponent<SimuWaveSizeModifier>().neededItem = AdaptiveArmor;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossCharactersUI;
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Cell Breach";
            //InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "This is a simulation everything is under control."; //"This is a simulation everything is under control."
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The subject is simulated, everything is under control."; //"This is a simulation everything is under control."

            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.6f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.4f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossCharacters = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossCharacters, weight = 7f, prerequisites = SimuMain.AfterWave5Prerequisite }; //This is a very basic wave modifier with a slight extra reward idk how common
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossCharacters);

            //
            //
            //Instant
            GameObject InfiniteTowerWaveSurprise = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSurprise", true);
            GameObject InfiniteTowerWaveSurpriseUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSurpriseUI", false);

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 2;

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSurpriseUI;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Surprise";
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies spawn immediately.";
            //InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color *= 1.1f;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color *= 1.1f;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color *= 1.1f;

            InfiniteTowerWaveCategory.WeightedWave ITBasicSurprise = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSurprise, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicSurprise);
            //
            //
            //BrotherHaunt
            GameObject InfiniteTowerWaveBrotherHaunt = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHaunt", true);
            GameObject InfiniteTowerWaveBrotherHauntUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHauntUI", false);

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveBrotherHaunt.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscBrotherHauntIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother"));
            cscBrotherHauntIT.name = "cscBrotherHauntIT";
            cscBrotherHauntIT.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/BrotherHauntMaster");
            cscBrotherHauntIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 55 },
                new ItemCountPair { itemDef = Ghost, count = 1 }
            };
            cscBrotherHauntIT.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            //Multiple of these haunters would be too chaotic to dodge and having them just one shot you for this wave is probably better
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscBrotherHauntIT;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Close;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBrotherHauntUI;
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Detonation";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The planet turns unstable.";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 1f, 1f);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.9f, 1);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.6f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITBrotherHaunt = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBrotherHaunt, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBrotherHaunt);
            //
            //
            //ITBossEclipse8
            GameObject InfiniteTowerWaveBossEclipse8 = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEclipse8", true);
            GameObject InfiniteTowerWaveBossEclipse8UI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEclipse8", false);

            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossEclipse8UI;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossEclipse8.AddComponent<SimulacrumExtrasHelper>().newRadius = 50;
            InfiniteTowerWaveBossEclipse8.AddComponent<SimulacrumEclipseWaveHelper>();

            Texture2D texDifficultyEclipse8Icon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texDifficultyEclipse8Icon.LoadImage(Properties.Resources.texDifficultyEclipse8Icon, true);
            texDifficultyEclipse8Icon.filterMode = FilterMode.Bilinear;
            Sprite texDifficultyEclipse8IconS = Sprite.Create(texDifficultyEclipse8Icon, WRect.rec64, WRect.half);

            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Eclipse";
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All eclipse 8 modifiers are enabled.";
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texDifficultyEclipse8IconS;
            //InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.5f, 0.8f);
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 0.95f);
            InfiniteTowerWaveBossEclipse8UI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.1f, 0.15f, 0.4f);

            InfiniteTowerWaveCategory.WeightedWave ITBossEclipse8 = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEclipse8, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEclipse8);
            //
            //
            ItemDef HealthDecay = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/HealthDecay");
            BuffDef BanditSkull = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/DeathMark");
            //
            GameObject InfiniteTowerWaveDeathVoid = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathVoid", true);
            GameObject InfiniteTowerWaveDeathVoidUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathVoidUI", false);

            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathVoid.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            CharacterSpawnCard cscITSuicideVoid = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscITSuicideVoid.name = "cscITSuicideVoid";
            cscITSuicideVoid.noElites = true;
            cscITSuicideVoid.directorCreditCost = 1;
            cscITSuicideVoid.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideVoid.hullSize = HullClassification.BeetleQueen;
            cscITSuicideVoid.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 79 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = HealthDecay, count = 1 }
            };
            DirectorCardCategorySelection dccsITSuicideWaveVoid = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveVoid.name = "dccsITSuicideWaveVoid";
            dccsITSuicideWaveVoid.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveVoid.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideVoid,
                selectionWeight = 5,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveVoid.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideVoid,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            CombatDirector secondDirector = InfiniteTowerWaveDeathVoid.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveVoid;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 1.5f;
            secondDirector.minSeriesSpawnInterval = 1.5f;
            secondDirector.maxRerollSpawnInterval = 0.1f;
            secondDirector.minRerollSpawnInterval = 0.1f;
            //RerollSpawnInterval like closer to how long between each wave

            InfiniteTowerWaveDeathVoid.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathVoidUI;
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Implosion";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless void implosions occur.";
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.4f, 0.8f);
            InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.4f, 0.8f);
            //InfiniteTowerWaveDeathVoidUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathVoid = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathVoid, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathVoid);
            //
            //
            GameObject InfiniteTowerWaveDeathLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathLunar", true);
            GameObject InfiniteTowerWaveDeathLunarUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathLunarUI", false);

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathLunar.AddComponent<SimulacrumExtrasHelper>().newRadius = 85;

            CharacterSpawnCard cscITSuicideLunar = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarExploder"));
            cscITSuicideLunar.name = "cscITSuicideLunar";
            cscITSuicideLunar.noElites = true;
            cscITSuicideLunar.directorCreditCost = 1;
            cscITSuicideLunar.hullSize = HullClassification.BeetleQueen;
            cscITSuicideLunar.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITSuicideLunar.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 78 },
                new ItemCountPair { itemDef = HealthDecay, count = 2 },
                new ItemCountPair { itemDef = Ghost, count = 1 }
            };
            DirectorCardCategorySelection dccsITSuicideWaveLunar = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveLunar.name = "dccsITSuicideWaveLunar";
            dccsITSuicideWaveLunar.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveLunar.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideLunar,
                selectionWeight = 6,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveLunar.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideLunar,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathLunar.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveLunar;
            secondDirector.monsterCredit = 10000;
            secondDirector.teamIndex = TeamIndex.Void;
            secondDirector.maxSeriesSpawnInterval = 0.75f;
            secondDirector.minSeriesSpawnInterval = 0.75f;
            secondDirector.maxRerollSpawnInterval = 2.25f;
            secondDirector.minRerollSpawnInterval = 2.25f;

            InfiniteTowerWaveDeathLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathLunarUI;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Blaze";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground gets covered in lunar flames.";
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.8f, 0.8f);
            InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.4f, 0.8f, 0.8f);
            //InfiniteTowerWaveDeathLunarUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.3f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITDeathLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathLunar, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathLunar);
            //
            //
            GameObject InfiniteTowerWaveDeathMendingCore = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathMendingCore", true);
            GameObject InfiniteTowerWaveDeathMendingCoreUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathMendingCoreUI", false);

            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;
            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            CharacterSpawnCard cscITSuicideHealing = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscITSuicideHealing.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/EliteEarth/AffixEarthHealerMaster.prefab").WaitForCompletion();
            cscITSuicideHealing.name = "cscITAffixEarthHealerMaster";
            cscITSuicideHealing.noElites = true;
            cscITSuicideHealing.directorCreditCost = 1;
            cscITSuicideHealing.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideHealing.hullSize = HullClassification.Golem;
            cscITSuicideHealing.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel"), count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 10 },
            };
            CharacterSpawnCard cscITSuicideHealingAir = Object.Instantiate(cscITSuicideHealing);
            cscITSuicideHealingAir.name = "cscITAffixEarthHealerMasterAIR";
            cscITSuicideHealingAir.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;

            DirectorCardCategorySelection dccsITSuicideWaveHealing = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveHealing.name = "dccsITSuicideWaveHealing";
            dccsITSuicideWaveHealing.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveHealing.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideHealing,
                selectionWeight = 2,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveHealing.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideHealingAir,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathMendingCore.AddComponent<CombatDirector>();
            secondDirector.monsterCards = dccsITSuicideWaveHealing;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 1f;
            secondDirector.minSeriesSpawnInterval = 1f;
            secondDirector.maxRerollSpawnInterval = 0.1f;
            secondDirector.minRerollSpawnInterval = 0.1f;

            InfiniteTowerWaveDeathMendingCore.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathMendingCoreUI;
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Mending";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless healing cores spawn.";
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(161, 231, 79, 255);
            InfiniteTowerWaveDeathMendingCoreUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(161, 231, 79, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathMendingCore = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathMendingCore, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathMendingCore);
            //
            //
            GameObject InfiniteTowerWaveDeathIceElite = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathIceElite", true);
            GameObject InfiniteTowerWaveDeathIceEliteUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDeathIceEliteUI", false);

            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;
            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().secondsAfterWave++;
            InfiniteTowerWaveDeathIceElite.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;

            CharacterSpawnCard cscITSuicideIce = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscITSuicideIce.name = "cscITSuicideIce";
            cscITSuicideIce.noElites = true;
            cscITSuicideIce.directorCreditCost = 1;
            cscITSuicideIce.forbiddenFlags = RoR2.Navigation.NodeFlags.None;
            cscITSuicideIce.hullSize = HullClassification.Golem;
            cscITSuicideIce.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 99 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 81 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
            };
            cscITSuicideIce.equipmentToGrant = new EquipmentDef[]
            {
                LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/AffixWhite")
            };

            CharacterSpawnCard cscITSuicideIceAIR = Object.Instantiate(cscITSuicideIce);
            cscITSuicideIceAIR.name = "cscITSuicideIceAIR";
            cscITSuicideIceAIR.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            //cscITSuicideIceAIR.prefab = LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscJellyfish").prefab;

            DirectorCardCategorySelection dccsITSuicideWaveIce = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
            dccsITSuicideWaveIce.name = "dccsITSuicideWaveIce";
            dccsITSuicideWaveIce.AddCategory("Basic Monsters", 1);
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIce,
                selectionWeight = 3,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Standard
            });
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIce,
                selectionWeight = 1,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            dccsITSuicideWaveIce.AddCard(0, new DirectorCard
            {
                spawnCard = cscITSuicideIceAIR,
                selectionWeight = 1,
                preventOverhead = false,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close
            });
            secondDirector = InfiniteTowerWaveDeathIceElite.AddComponent<CombatDirector>();
            secondDirector.teamIndex = TeamIndex.Void;
            secondDirector.monsterCards = dccsITSuicideWaveIce;
            secondDirector.monsterCredit = 10000;
            secondDirector.maxSeriesSpawnInterval = 1.5f;
            secondDirector.minSeriesSpawnInterval = 1.5f;
            secondDirector.maxRerollSpawnInterval = 0.1f;
            secondDirector.minRerollSpawnInterval = 0.1f;
            //RerollSpawnInterval like closer to how long between each wave

            InfiniteTowerWaveDeathIceElite.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDeathIceEliteUI;
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Ice";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Endless freezing shocks occur.";
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color32(214, 247, 247, 255);
            InfiniteTowerWaveDeathIceEliteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color32(214, 247, 247, 255);

            InfiniteTowerWaveCategory.WeightedWave ITDeathIce = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDeathIceElite, weight = 3f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITDeathIce);

            On.RoR2.HealthComponent.Suicide += FixIceOnGhosts;
            //
            //
            //
            GameObject InfiniteTowerWaveSulfurPods = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSulfurPods", true);
            GameObject InfiniteTowerWaveSulfurPodsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSulfurPodsUI", false);

            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            InteractableSpawnCard cscITSulfurPod = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            cscITSulfurPod.name = "cscITSulfurPod";
            cscITSulfurPod.slightlyRandomizeOrientation = false;
            cscITSulfurPod.orientToFloor = false;
            cscITSulfurPod.directorCreditCost = 1;
            cscITSulfurPod.hullSize = HullClassification.Human;
            cscITSulfurPod.sendOverNetwork = true;
            cscITSulfurPod.skipSpawnWhenSacrificeArtifactEnabled = false;
            cscITSulfurPod.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Ground;
            cscITSulfurPod.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/SulfurPodBody");
 
            SimulacrumInteractablesWaveHelper waveHelper = InfiniteTowerWaveSulfurPods.AddComponent<SimulacrumInteractablesWaveHelper>();
            waveHelper.spawnCard = cscITSulfurPod;
            waveHelper.interval = 1f;
            waveHelper.spawnsOnStart = 7;

            InfiniteTowerWaveSulfurPods.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSulfurPodsUI;
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Sulfur";
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Sulfur pods keep growing.";
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BanditSkull.iconSprite;
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7882f, 0.949f, 0.302f, 1);
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7882f, 0.949f, 0.302f, 1);
            InfiniteTowerWaveSulfurPodsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7882f, 0.949f, 0.302f, 1) * 0.8f;

            InfiniteTowerWaveCategory.WeightedWave ITSulfurPods = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSulfurPods, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite};
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITSulfurPods);
            //
            GameObject InfiniteTowerWaveTarPots = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveTarPots", true);
            GameObject InfiniteTowerWaveTarPotsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveTarPotsUI", false);

            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 5;

            InteractableSpawnCard cscITTarPot = ScriptableObject.CreateInstance<InteractableSpawnCard>();
            cscITTarPot.name = "cscITTarPot";
            cscITTarPot.directorCreditCost = 1;
            cscITTarPot.hullSize = HullClassification.Human;
            cscITTarPot.sendOverNetwork = true;
            cscITTarPot.skipSpawnWhenSacrificeArtifactEnabled = false;
            cscITTarPot.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscITTarPot.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ExplosivePotDestructibleBody");

            waveHelper = InfiniteTowerWaveTarPots.AddComponent<SimulacrumInteractablesWaveHelper>();
            waveHelper.spawnCard = cscITTarPot;
            waveHelper.interval = 0.8f;
            waveHelper.spawnsOnStart = 2;

            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveTarPotsUI;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Tar";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It's raining pots of tar.";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1)*2; //0.2 0.0902 0.0902 1
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.0902f, 0.0902f, 1) * 2;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITTarPots = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveTarPots, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITTarPots);
            //
            //
            //InvisibleDude
            GameObject InfiniteTowerWaveInvisibleDude = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveInvisibleDude", true);
            GameObject InfiniteTowerWaveInvisibleDudeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveInvisibleDudeUI", false);

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            InfiniteTowerWaveInvisibleDude.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscInvisibleDudeIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscInvisibleDudeIT.name = "cscITInvisibleGip";
            cscInvisibleDudeIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 56 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 99 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 5 },
            };
            cscInvisibleDudeIT.equipmentToGrant = new EquipmentDef[]
            {
                LegacyResourcesAPI.Load<EquipmentDef>("EquipmentDefs/AffixHaunted")
            };

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscInvisibleDudeIT;
            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveInvisibleDude.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveInvisibleDudeUI;
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Invisibility";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies are invisible inside the sphere.";
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.95f, 0.7f);
            InfiniteTowerWaveInvisibleDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.95f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITInvisibleDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveInvisibleDude, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITInvisibleDude);
            //
            //
            //VagrantNovaDude
            GameObject InfiniteTowerWaveVagrantNovaDude = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveVagrantNovaDude", true);
            GameObject InfiniteTowerWaveVagrantNovaDudeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveVagrantNovaDudeUI", false);

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.10f;
            InfiniteTowerWaveVagrantNovaDude.AddComponent<SimulacrumExtrasHelper>().newRadius = 100;
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;
            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscVagrantNovaDudeIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscGipBody"));
            cscVagrantNovaDudeIT.name = "cscITSuperNovaGip";
            cscVagrantNovaDudeIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 78 },
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 80 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 25 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/NovaOnLowHealth"), count = 2 },
                new ItemCountPair { itemDef = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/PersonalShield"), count = 1 },
            };

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                count = 1,
                spawnCard = cscVagrantNovaDudeIT,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close,
            };
            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveVagrantNovaDude.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveVagrantNovaDudeUI;
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Nova";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A nova occurs periodically.";
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.8f, 0.95f);
            InfiniteTowerWaveVagrantNovaDudeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.95f);

            InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 4f, prerequisites = SimuMain.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITVagrantNovaDude = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVagrantNovaDude, weight = 44444 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITVagrantNovaDude);
            //
            //
            //
            //ArtiifactReliquary
            GameObject InfiniteTowerWaveArtiifactReliquary = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactReliquary", true);
            GameObject InfiniteTowerWaveArtiifactReliquaryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveArtifactReliquaryUI", false);

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.1f;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<CombatDirector>().teamIndex = TeamIndex.Void;
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            CharacterSpawnCard cscITArtifactBall = ScriptableObject.CreateInstance<CharacterSpawnCard>();
            cscITArtifactBall.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/ArtifactShell/ArtifactShellMaster.prefab").WaitForCompletion();
            cscITArtifactBall.name = "cscITArtifactBall";
            cscITArtifactBall.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 54 },
                new ItemCountPair { itemDef = SimuMain.ITAttackSpeedDown, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 45 },
            };
            cscITArtifactBall.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0] = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo
            {
                count = 1,
                spawnCard = cscITArtifactBall,
                spawnDistance = DirectorCore.MonsterSpawnDistance.Close,
            };
            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveArtiifactReliquary.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveArtiifactReliquaryUI;
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Reliquary";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The reliquary rains down shots.";
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.7f, 0.8f);
            InfiniteTowerWaveArtiifactReliquaryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 1f, prerequisites = SimuMain.StartWave15Prerequisite };
            //InfiniteTowerWaveCategory.WeightedWave ITArtiifactReliquary = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveArtiifactReliquary, weight = 44444 };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITArtiifactReliquary);

            On.EntityStates.VagrantNovaItem.ReadyState.OnEnter += (orig, self) =>
            {
                orig(self);
                if (self.attachedBody.inventory.GetItemCount(SimuMain.ITKillOnCompletion) == 78)
                {
                    self.attachedHealthComponent.Networkhealth = 10;
                }
            };
            //
            //
            //
            //
            //
            //Halcyonite
            GameObject InfiniteTowerWaveHalcyonite = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveHalcyoniteBoss", true);
            GameObject InfiniteTowerWaveHalcyoniteUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveHalcyoniteBossUI", false);

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().baseCredits = 450;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveHalcyonite.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscHalcyoniteIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC2/Halcyonite/cscHalcyonite.asset").WaitForCompletion());
            cscHalcyoniteIT.name = "cscHalcyoniteIT";
            cscHalcyoniteIT.itemsToGrant = new ItemCountPair[] {
            };
            cscHalcyoniteIT.equipmentToGrant = new EquipmentDef[]
            {
                Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/DLC2/Elites/EliteAurelionite/EliteAurelioniteEquipment.asset").WaitForCompletion(),
            };

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscHalcyoniteIT;
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].eliteDef = Addressables.LoadAssetAsync<EliteDef>(key: "RoR2/DLC2/Elites/EliteAurelionite/edAurelionite.asset").WaitForCompletion();
            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = true;

            InfiniteTowerWaveHalcyonite.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHalcyoniteUI;
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Halcyonite";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat Halcyonite in addition to the wave.";
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.7f, 0.3f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.9f, 0.55f, 1);
            InfiniteTowerWaveHalcyoniteUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.8f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITHalcyonite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHalcyonite, weight = 5f, prerequisites = SimuMain.StartWave20PrerequisiteDLC2 };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITHalcyonite);


            //
            SizeChangingWaves();

            ModSupport();
        }

        private static void FixIceOnGhosts(On.RoR2.HealthComponent.orig_Suicide orig, HealthComponent self, GameObject killerOverride, GameObject inflictorOverride, DamageTypeCombo damageType)
        {
            orig(self, killerOverride,inflictorOverride, damageType);
            if (NetworkServer.active)
            {
                if (self.body.HasBuff(RoR2Content.Buffs.AffixWhite))
                {
                    Vector3 position = self.body.corePosition;
                    GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(LegacyResourcesAPI.Load<GameObject>("Prefabs/NetworkedObjects/GenericDelayBlast"), position, Quaternion.identity);
                    float num = 12f + self.body.radius;
                    gameObject2.transform.localScale = new Vector3(num, num, num);
                    DelayBlast component = gameObject2.GetComponent<DelayBlast>();
                    if (component)
                    {
                        component.position = position;
                        component.baseDamage = self.body.damage * 1.5f;
                        component.baseForce = 2300f;
                        component.attacker = self.gameObject;
                        component.radius = num;
                        component.crit = Util.CheckRoll(self.body.crit, 0);
                        component.procCoefficient = 0.75f;
                        component.maxTimer = 2f;
                        component.falloffModel = BlastAttack.FalloffModel.None;
                        component.explosionEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/ImpactEffects/AffixWhiteExplosion");
                        component.delayEffect = LegacyResourcesAPI.Load<GameObject>("Prefabs/Effects/AffixWhiteDelayEffect");
                        component.damageType = DamageType.Freeze2s;
                        TeamFilter component2 = gameObject2.GetComponent<TeamFilter>();
                        if (component2)
                        {
                            component2.teamIndex = TeamComponent.GetObjectTeam(component.attacker);
                        }
                    }
                }
            }
            
        }

        private static void LeaveNoEquipmentDroneIT(On.EntityStates.Drone.DeathState.orig_OnImpactServer orig, EntityStates.Drone.DeathState self, Vector3 contactPoint)
        {
            if (Run.instance && Run.instance.GetComponent<InfiniteTowerRun>())
            {
                if (self.characterBody.name.StartsWith("Equipment"))
                {
                    orig(self, contactPoint);
                }
                return;
            }
            orig(self, contactPoint);
        }

        internal static void ModSupport()
        {
            //Could do NemMando NemMerc but idk they'd just fucking die like in the regular game

            //Minor Mod - DireSeeker
            GameObject InfiniteTowerWaveBossDireseeker = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossDireseeker", true);
            GameObject InfiniteTowerCurrentBossDireseekerWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossDireseekerWaveUI", false);

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossDireseeker.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossDireseeker.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/dampcave/dccsDampCaveMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossDireseekerWaveUI;

            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Direseeker";
            InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Track and Kill.";

            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            //InfiniteTowerCurrentBossDireseekerWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossDireseeker = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossDireseeker, weight = 4f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBossDireseeker);
            //
            //
            //Will need to see what Empyrean Elites actually do
            GameObject InfiniteTowerWaveSS2RainbowElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2RainbowElites", true);
            GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 30;

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Empyrean";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Powerful energies have harmonized.";
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 1f, 0.7f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.85f, 0.3f, 0.3f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 4f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
            //
            //
            //Storms
            GameObject InfiniteTowerWaveSS2Storms = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2Storms", true);
            GameObject InfiniteTowerCurrentSS2StormsWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2StormsWaveUI", false);

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().baseCredits = 100;

            InfiniteTowerWaveSS2Storms.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2StormsWaveUI;
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Storms";
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A storm approaches.";
            //InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.3f, 0.85f, 0.85f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.7f, 0.7f);
            InfiniteTowerCurrentSS2StormsWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.7f, 0.7f, 0.7f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2Storms = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2Storms, weight = 3f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2Storms);
        }

       }




}