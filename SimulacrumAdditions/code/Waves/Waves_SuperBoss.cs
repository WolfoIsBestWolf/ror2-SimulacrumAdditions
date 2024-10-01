using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class Waves_SuperBoss
    {
        internal static void MakeWaves()
        {
            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_BROTHER", "Defeat the King of Nothing.", "en"); //Defeat Mithrix for a special reward.
            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_SCAV", "A taste of your own medicine.", "en"); //Defeat the Scavenger for a special reward.

            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            float ITSpecialBossWaveWeight = 2.5f;


            #region Twisted Scavengers
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

            SimuExplicitStats simuExplicitStats = InfiniteTowerWaveBossScavLunar.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.4f;
            simuExplicitStats.hpBonusMulti = 0.4f;
            simuExplicitStats.halfOnNonFinal = false;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Twisted Scavenger";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Twisted Scavenger.";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveScavLunarIconS;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossScavLunar, weight = ITSpecialBossWaveWeight * 3f, prerequisites = SimuMain.StartWave35Prerequisite };
            //
#endregion
            #region Voidling
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

            simuExplicitStats = InfiniteTowerWaveBossVoidRaidCrab.AddComponent<SimuExplicitStats>();
            simuExplicitStats.damageBonusMulti = 0.3f;
            simuExplicitStats.hpBonusMulti = 1.15f;
            simuExplicitStats.halfOnNonFinal = true;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of「Voidling」";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "D??ef?eat the「Diviner of the Deep」";

            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidRaidCrab, weight = ITSpecialBossWaveWeight + 0.5f, prerequisites = SimuMain.StartWave50Prerequisite };
            //
#endregion
            #region Aurelionite
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
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 100;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 2;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            simuExplicitStats = InfiniteTowerWaveBossTitanGold.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 1.2f;
            simuExplicitStats.damageBonusMulti = 0.7f;
            simuExplicitStats.halfOnNonFinal = true;

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
            #endregion
            #region Sots_FalseSon
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

            simuExplicitStats = InfiniteTowerWaveBossFalseSon.AddComponent<SimuExplicitStats>();
            simuExplicitStats.halfOnNonFinal = true;

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
#endregion

            #region SuperRoboBall
            GameObject InfiniteTowerWaveBossSuperRoboBallBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossSuperRoboBallBoss", true);
            GameObject InfiniteTowerCurrentBossSuperRoboBallBossWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossSuperRoboBallBossWaveUI", false);

            CharacterSpawnCard cscSuperRoboBallBossIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/RoboBallBoss/cscSuperRoboBallBoss.asset").WaitForCompletion());
            cscSuperRoboBallBossIT.name = "cscSuperRoboBallBossIT";
            cscSuperRoboBallBossIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscSuperRoboBallBossIT;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 100;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 2; //Evens out at 400 for wave 50
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossSuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            simuExplicitStats = InfiniteTowerWaveBossSuperRoboBallBoss.AddComponent<SimuExplicitStats>();
            simuExplicitStats.hpBonusMulti = 0.9f;
            simuExplicitStats.damageBonusMulti = 0.7f;
            simuExplicitStats.halfOnNonFinal = true;

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
            #endregion

            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITBossFalseSon);
            ITBossTitanGold.weight = 0.3f;
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossTitanGold, ITBossFalseSon); //ITBossSuperRoboBallBoss

        }

    }

}