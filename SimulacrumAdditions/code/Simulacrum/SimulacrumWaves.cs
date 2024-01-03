using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class SimuWavesMisc
    {

        //public static 
        //public static DirectorCardCategorySelection dccsVoidInfestorOnly = ScriptableObject.CreateInstance<DirectorCardCategorySelection>();
        public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;

        //public static FamilyDirectorCardCategorySelection dccsVoidFamily = Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion();
        public static FamilyDirectorCardCategorySelection dccsVoidFamilyNoBarnacle = UnityEngine.Object.Instantiate(Addressables.LoadAssetAsync<FamilyDirectorCardCategorySelection>(key: "RoR2/DLC1/Common/dccsVoidFamily.asset").WaitForCompletion());

        public static void Start()
        {
            
            ItemDef AdaptiveArmor = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/AdaptiveArmor/AdaptiveArmor.asset").WaitForCompletion();
            ItemDef BoostHp = Addressables.LoadAssetAsync<ItemDef>(key: "RoR2/Base/BoostHp/BoostHp.asset").WaitForCompletion();
            float ITSpecialBossWaveWeight = 2.5f;

            dccsVoidFamilyNoBarnacle.categories[2].cards = dccsVoidFamilyNoBarnacle.categories[2].cards.Remove(dccsVoidFamilyNoBarnacle.categories[2].cards[1]);
            dccsVoidFamilyNoBarnacle.name = "dccsVoidFamilyNoBarnacle";


            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_BROTHER", "Defeat the King of Nothing.", "en"); //Defeat Mithrix for a special reward.
            LanguageAPI.Add("INFINITETOWER_WAVE_DESCRIPTION_BOSS_SCAV", "A taste of your own medicine.", "en"); //Defeat the Scavenger for a special reward.

            //            
            //Scav Lunar
            GameObject InfiniteTowerWaveBossScavLunar = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossScavLunar", true);
            GameObject InfiniteTowerCurrentBossScavLunarWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossScavWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossScavLunarWaveUI", false);

            MultiCharacterSpawnCard cscScavLunarIT = Addressables.LoadAssetAsync<MultiCharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset").WaitForCompletion();
            cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 0 }, new ItemCountPair { itemDef = BoostHp, count = 0 } };
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscScavLunarIT;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 50;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 5;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath = 120;

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().secondsAfterWave += 7;
            InfiniteTowerWaveBossScavLunar.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Lunar;
            InfiniteTowerWaveBossScavLunar.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITLunar;
            InfiniteTowerWaveBossScavLunar.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;

            Texture2D texITWaveScavLunarIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveScavLunarIcon.LoadImage(Properties.Resources.texITWaveScavLunarIcon, true);
            texITWaveScavLunarIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveScavLunarIconS = Sprite.Create(texITWaveScavLunarIcon, WRect.rec64, WRect.half);

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossScavLunarWaveUI;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Twisted Scavenger";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Twisted Scavenger.";
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveScavLunarIconS;
            InfiniteTowerCurrentBossScavLunarWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 1f, 1);
            //
            //
            //Voidling
            GameObject InfiniteTowerWaveBossVoidRaidCrab = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossVoidRaidCrab", true);
            GameObject InfiniteTowerCurrentBossVoidRaidWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossVoidRaidWaveUI", false);

            CharacterSpawnCard cscMiniVoidRaidCrabPhase3IT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidRaidCrab/cscMiniVoidRaidCrabPhase3.asset").WaitForCompletion());
            cscMiniVoidRaidCrabPhase3IT.name = "cscMiniVoidRaidCrabPhase3IT";
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscMiniVoidRaidCrabPhase3IT;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 45;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.75f;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<CombatDirector>().monsterCards = dccsVoidFamilyNoBarnacle;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().secondsAfterWave += 2;
            InfiniteTowerWaveBossVoidRaidCrab.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSuperVoid;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 160;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of「Voidling」";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "D??ef?eat the 「Diviner of the Deep」";
            //
            //
            //Infestor
            CharacterSpawnCard cscVoidInfestorIT;
            cscVoidInfestorIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/EliteVoid/cscVoidInfestor.asset").WaitForCompletion());
            cscVoidInfestorIT.name = "cscVoidInfestorIT";
            cscVoidInfestorIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }, new ItemCountPair { itemDef = BoostHp, count = 10 } };
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

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 700;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.3f;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBossVoidElites.AddComponent<SimulacrumExtrasHelper>().newRadius = 65;

            CombatDirector WaveVoidEliteDirector = InfiniteTowerWaveBossVoidElites.AddComponent<CombatDirector>();
            WaveVoidEliteDirector.monsterCredit = 550;
            WaveVoidEliteDirector.monsterCards = dccsVoidInfestorOnly;
            WaveVoidEliteDirector.skipSpawnIfTooCheap = false;
            WaveVoidEliteDirector.teamIndex = TeamIndex.Void;
            RangeFloat TempRangeFloatThing = new RangeFloat { max = 1, min = 1 };
            WaveVoidEliteDirector.moneyWaveIntervals = WaveVoidEliteDirector.moneyWaveIntervals.Add(TempRangeFloatThing);
            WaveVoidEliteDirector.creditMultiplier = 4;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.VoidTier2;
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITVoidInfestorWave;

            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidEliteWaveUI;
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Infestation";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "A swarm of Void Infestors is gathering.";
            InfiniteTowerCurrentBossVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 1f, 1);
            //
            //
            //Lunar Elite Wave
            GameObject InfiniteTowerWaveLunarElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarElites", true);
            GameObject InfiniteTowerCurrentLunarEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias = 1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentLunarEliteWaveUI;

            Texture2D texITWaveLunarEliteIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarEliteIcon.LoadImage(Properties.Resources.texITWaveLunarEliteIcon, true);
            texITWaveLunarEliteIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarEliteIconS = Sprite.Create(texITWaveLunarEliteIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Perfection";
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Perfected.";

            InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 5 };
            //
            //
            //Malachite Elites
            GameObject InfiniteTowerWaveMalachitesOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElites", true);
            GameObject InfiniteTowerWaveMalachitesOnlyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElitesUI", false);

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.5f;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().baseCredits += 20;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().eliteBias = 0f; //Doesn't change the cost just the likelyhood

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMalachitesOnlyUI;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Poison";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Malachite.";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.8f, 0.35f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.8f, 0.35f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.1f, 0.3f, 0f);

            InfiniteTowerWaveCategory.WeightedWave ITEliteMalachite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMalachitesOnly, weight = 3f, prerequisites = SimuMain.Wave31OrGreaterPrerequisite};
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITEliteMalachite);
            //
            //
            //Void Elites
            GameObject InfiniteTowerWaveVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidElites", true);
            GameObject InfiniteTowerCurrentVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentVoidEliteWaveUI", false);

            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias = 1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;

            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Collapse";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Voidtouched.";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 5 };
            //
            //
            //Gold Titan
            GameObject InfiniteTowerWaveBossTitanGold = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossTitanGold", true);
            GameObject InfiniteTowerCurrentBossTitanGoldWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossTitanGoldWaveUI", false);


            CharacterSpawnCard cscTitanGoldIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscTitanGoldIT.name = "cscTitanGoldIT";
            cscTitanGoldIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 }};
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscTitanGoldIT;

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossTitanGold.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossTitanGold.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/goldshores/dccsGoldshoresMonstersDLC1.asset").WaitForCompletion();
            
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 5;
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossTitanGoldWaveUI;
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Aurelionite";
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Titanic Goldweaver.";

            Texture2D texITWaveTitanGoldIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveTitanGoldIcon.LoadImage(Properties.Resources.texITWaveTitanGoldIcon, true);
            texITWaveTitanGoldIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveTitanGoldIconS = Sprite.Create(texITWaveTitanGoldIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveTitanGoldIconS;
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossTitanGold, weight = ITSpecialBossWaveWeight, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
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
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 5; //Evens out at 400 for wave 50
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossSuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Alloy Worship Unit";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Friend of Vultures.";

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperRoboBallBoss, weight = ITSpecialBossWaveWeight, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
            //
            //
            //Mountain Boss
            GameObject InfiniteTowerWaveDoubleBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBoss", true);
            GameObject InfiniteTowerWaveDoubleBossUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBossUI", false);

            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDoubleBossUI;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2.3f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsAfterWave = 12;

            InfiniteTowerWaveDoubleBoss.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveDoubleBoss.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;

            Texture2D texITWaveBossIconMountain = new Texture2D(64, 64, TextureFormat.DXT5, false);
            texITWaveBossIconMountain.LoadImage(Properties.Resources.texITWaveBossIconMountain, true);
            texITWaveBossIconMountain.filterMode = FilterMode.Bilinear;
            Sprite texITWaveBossIconMountainS = Sprite.Create(texITWaveBossIconMountain, WRect.rec64, WRect.half);

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveBossIconMountainS;
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Mountain";
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat twice as many enemies for twice the rewards.";

            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.8f, 0.9f);
            InfiniteTowerWaveDoubleBossUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.9f); //new Color(0.240566f, 0.8644956f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITDoubleBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDoubleBoss, weight = 8f, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };

            //
            //
            InfiniteTowerWaveCategory.WeightedWave ITBossVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidElites, weight = 8f }; //7 normally

            InfiniteTowerWaveCategory.WeightedWave ITBossScavLunar = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossScavLunar, weight = ITSpecialBossWaveWeight * 2f, prerequisites = SimuMain.Wave26OrGreaterPrerequisite };
            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidRaidCrab, weight = ITSpecialBossWaveWeight, prerequisites = SimuMain.Wave45OrGreaterPrerequisite };
            
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarElites, ITVoidElites);
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITDoubleBoss, ITBossVoidElites, ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold);
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossTitanGold, ITBossSuperRoboBallBoss);

            Organized();
        }

 
        internal static void LateChanges()
        {
            CreateEquipmentDroneSpawnCards();
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
                    case "InfiniteTowerWaveSS2RainbowElites":
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
                        CharacterSpawnCard cscDireseekerIT = Object.Instantiate(cscDireseeker);
                        cscDireseekerIT.name = "cscDireseekerIT";
                        cscDireseekerIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 } };
                        wave.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscDireseekerIT;
                        SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(SimuMain.ITModSupportWaves.wavePrefabs[i]);
                        break;
                }

            }

            //This is stupid but you can't seem to just make the waves late
            Color newArticfact = new Color(1f, 0.7647f, 1.2647f, 1);
            for (int i = 0; i < SimuMain.ITBasicWaves.wavePrefabs.Length; i++)
            {
                GameObject wave = SimuMain.ITBasicWaves.wavePrefabs[i].wavePrefab;
                InfiniteTowerWaveController controller = wave.GetComponent<InfiniteTowerWaveController>();
                ArtifactEnabler artifact = wave.GetComponent<ArtifactEnabler>();
                if (artifact)
                {
                    controller.overlayEntries[1].prefab.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = newArticfact;
                }
                switch (wave.name)
                {
                    case "InfiniteTowerWaveBasicEquipmentDrone":
                    case "InfiniteTowerWaveBossEquipmentDrone":
                        controller.rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
                        break;
                }
            }

            for (int i = 0; i < SimuMain.ITSuperBossWaves.wavePrefabs.Length; i++)
            {
                SimuMain.ITSuperBossWaves.wavePrefabs[i].weight = 1;
            }
        }


        internal static void Organized()
        {
            ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
            ItemDef BoostAttackSpeed = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/BoostAttackSpeed");
            ItemDef AdaptiveArmor = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AdaptiveArmor");

            //Heresy
            On.RoR2.GenericSkill.SetSkillOverride += FixHeresyForEnemies;
            GameObject InfiniteTowerWaveHeresy = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveHeresy", true);
            GameObject InfiniteTowerWaveHeresyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveHeresyUI", false);

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveHeresyUI;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITHeresy;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Lunar;
            InfiniteTowerWaveHeresy.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveHeresy.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;

            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;
            InfiniteTowerWaveHeresy.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 12;

            Texture2D texITWaveLunarHeresy = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarHeresy.LoadImage(Properties.Resources.texITWaveLunarHeresy, true);
            texITWaveLunarHeresy.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarHeresyS = Sprite.Create(texITWaveLunarHeresy, WRect.rec64, WRect.half);

            SimuMain.ITDamageDown.pickupIconSprite = texITWaveLunarHeresyS;
            Color HeresyColor = new Color(0.4f, 0.45f, 0.79f, 1);
            //0.1882 0.498 1 1
            //0.298 0.3294 0.5647 1
            //InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarHeresyS;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Heresy";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies gain heresy items for the wave.";
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = HeresyColor;
            InfiniteTowerWaveHeresyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = HeresyColor;

            InfiniteTowerWaveCategory.WeightedWave ITHeresy = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveHeresy, weight = 6f, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
            //ITHeresy.prerequisites = null;
            //ITHeresy.weight = 20;
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITHeresy);
            //
            //
            //Equipment Drone Boss        
            GameObject InfiniteTowerWaveBossEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEquipmentDrone", true);
            GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] { new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Standard }, new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 3, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far } };

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.2f;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            InfiniteTowerWaveBossEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier2;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;

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

            InfiniteTowerWaveCategory.WeightedWave ITBossEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEquipmentDrone, weight = 10, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEquipmentDrone);
            //
            //
            //Flight
            GameObject InfiniteTowerWaveJetpack = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFlight", true);
            GameObject InfiniteTowerWaveJetpackUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveJetpackUI", false);

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 2;
            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
            InfiniteTowerWaveJetpack.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;

            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.3f;
            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 0.75f;
            InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveJetpack.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicJetpack = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveJetpack, weight = 5f };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicJetpack);
            //
            //
            //Ghost Haunting Boss
            GameObject InfiniteTowerWaveBossGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHaunting", true);
            GameObject InfiniteTowerWaveBossGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossGhostHauntingUI", false);

            MultiCharacterSpawnCard cscITGhostBoss = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITGhostBoss.masterPrefabs = new GameObject[]
            {
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscMagmaWorm").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscImpBoss").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/Titan/cscGrandparent").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscVagrant").prefab,
                //LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscRoboBallBoss").prefab,
            };
            cscITGhostBoss.name = "cscITGhostBoss";
            cscITGhostBoss.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = BoostAttackSpeed, count = 2 },
                //new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 2 },
                //new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 25 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
            };
            
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscITGhostBoss;

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBossGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 120;

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
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.8f ,0.85f, 1);
            InfiniteTowerWaveBossGhostHauntingUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.8f, 0.85f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossGhostHaunting, weight = 13f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossGhostHaunting);
            //
            //
            //Ghost Haunting Basic
            GameObject InfiniteTowerWaveBasicGhostHaunting = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHaunting", true);
            GameObject InfiniteTowerWaveBasicGhostHauntingUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicGhostHauntingUI", false);

            MultiCharacterSpawnCard cscITGhostBasic = ScriptableObject.CreateInstance<MultiCharacterSpawnCard>();
            cscITGhostBasic.masterPrefabs = new GameObject[]
            {
                Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/DLC1/VoidJailer/cscVoidJailer.asset").WaitForCompletion().prefab,
                //LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLemurianBruiser").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayBruiser").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscClayGrenadier").prefab,
                LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarGolem").prefab,
                //LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscLunarWisp").prefab,
            };
            cscITGhostBasic.name = "cscITGhostBasic";
            cscITGhostBasic.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = Ghost, count = 1 },
                new ItemCountPair { itemDef = BoostAttackSpeed, count = 3 },
                //new ItemCountPair { itemDef = RoR2Content.Items.Hoof, count = 0 },
                //new ItemCountPair { itemDef = RoR2Content.Items.LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 15 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
            };
            //Probably less so "low damage quick" but more so "immovable object to be avoided" like the Bulk Detonator

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscITGhostBasic;

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 200;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicGhostHaunting, weight = 7, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicGhostHaunting);
            //
            //
            //
            //Equipment Drone Basic
            GameObject InfiniteTowerWaveBasicEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDrone", true);
            GameObject InfiniteTowerWaveBasicEquipmentDroneUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBasicEquipmentDroneUI", false);


            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = null;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnDistance = DirectorCore.MonsterSpawnDistance.Far;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.12f;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 160;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 75;

            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = SimuMain.ItemOrangeTierDef.tier;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 2;
            InfiniteTowerWaveBasicEquipmentDrone.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBasicEquipmentDrone.GetComponent<SimulacrumExtrasHelper>().newRadius = 80;
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicEquipmentDrone = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBasicEquipmentDrone, weight = 9, prerequisites = SimuMain.AfterWave5Prerequisite };
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
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 65 },
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 1100 }, //10x hp
                new ItemCountPair { itemDef = DLC1Content.Items.CloverVoid, count = 1 },
                new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 },
                //new ItemCountPair { itemDef = RoR2Content.Items.LevelBonus, count = 5 },
            };
            CharacterSpawnCard cscNullifierIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscNullifierIT.name = "cscNullifierIT";

            //Could do like Scav Item Granter type of deal ig
            //Or well RandomItems since that's a method apparently
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscITCharacter, count = 0},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscNullifierIT, count = 2},
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 420;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossCharacters.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtBossMixTier;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossCharactersUI;
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Cell Breach";
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "This is a simulation everything is under control.";

            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.6f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.4f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossCharacters = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossCharacters, weight = 8 }; //This is a very basic wave modifier with a slight extra reward idk how common
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossCharacters);
            //
            //
            //Coffee
            GameObject InfiniteTowerWaveCoffee = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveCoffee", true);
            GameObject InfiniteTowerWaveCoffeeUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveCoffeeUI", false);

            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveCoffeeUI;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveDamage;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 3;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveCoffee.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Caffeine";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies have increased movement and attack speed.";
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 1, 0.88f);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 1, 1);
            InfiniteTowerWaveCoffeeUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.3f, 0);

            InfiniteTowerWaveCategory.WeightedWave ITCoffee = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveCoffee, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITCoffee);
            //
            //
            //Haunt
            GameObject InfiniteTowerWaveBrotherHaunt = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHaunt", true);
            GameObject InfiniteTowerWaveBrotherHauntUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBrotherHauntUI", false);

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicWaveOnKill;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBrotherHaunt.AddComponent<SimulacrumExtrasHelper>().newRadius = 65;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscBrotherHauntIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother"));
            cscBrotherHauntIT.name = "cscBrotherHauntIT";
            cscBrotherHauntIT.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/BrotherHauntMaster");
            cscBrotherHauntIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 35 }
            };
            //Multiple of these haunters would be too chaotic to dodge and having them just one shot you for this wave is probably better
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscBrotherHauntIT;
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().isBossWave = false;

            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBrotherHauntUI;
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Detonation";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The planet turns unstable.";
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 1f, 1f);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.7f, 0.9f, 1);
            InfiniteTowerWaveBrotherHauntUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.4f, 0.6f, 0.8f);

            InfiniteTowerWaveCategory.WeightedWave ITBrotherHaunt = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBrotherHaunt, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBrotherHaunt);
            //
            //
            //Many Items
            GameObject InfiniteTowerWaveManyItems = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItems", true);
            GameObject InfiniteTowerWaveManyItemsUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveManyItemsUI", false);

            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveManyItemsUI;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveManyItems.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Hoarding";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters get higher stacks of items.";
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1, 0.88f, 0.88f);
            InfiniteTowerWaveManyItemsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.58f, 0.58f);

            InfiniteTowerWaveCategory.WeightedWave ManyItems = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveManyItems, weight = 3f, prerequisites = SimuMain.Wave31OrGreaterPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ManyItems); 
            //
            //
            //Blindness Buff
            GameObject InfiniteTowerWaveBlindness = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindness", true);
            GameObject InfiniteTowerWaveBlindnessUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindnessUI", false);

            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBlindnessUI;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().baseCredits = 200;

            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Blindness";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It becomes hard to see.";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);

            InfiniteTowerWaveCategory.WeightedWave Blindness = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBlindness, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Blindness);
            //
            //
            //Battery
            GameObject InfiniteTowerWaveBattery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBattery", true);
            GameObject InfiniteTowerWaveBatteryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBatteryUI", false);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveUtility;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.5f;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBattery.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveBattery.AddComponent<SimuEquipmentWaveHelper>().variant = 1;

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBatteryUI;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Battery";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Improper storage may cause SERIOUS harm.";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);
            
            InfiniteTowerWaveCategory.WeightedWave ITBasicBattery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBattery, weight = 2f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicBattery);

            On.EntityStates.QuestVolatileBattery.CountDown.OnEnter += (orig, self) =>
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
                        gameObject.transform.GetChild(0).GetComponent<Light>().range *= 3;
                        gameObject.transform.GetChild(0).GetComponent<Light>().intensity *= 3;
                        gameObject.transform.GetChild(0).GetComponent<LightIntensityCurve>().timeMax /= 2;
                        self.vfxInstances[0] = gameObject;
                    }
                }
            };
            //
            //
            //Lepton
            GameObject InfiniteTowerWaveLepton = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLepton", true);
            GameObject InfiniteTowerWaveLeptonUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveLeptonUI", false);

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITFamilyWaveHealing;
            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;

            InfiniteTowerWaveLepton.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveLeptonUI;
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Flowers";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Monsters spawning may unleash a healing aura.";
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.6f, 0.8f, 0.6f, 1);
            InfiniteTowerWaveLeptonUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 1f, 0.8f, 1);
            
            InfiniteTowerWaveCategory.WeightedWave ITBasicLepton = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLepton, weight = 3f, prerequisites = SimuMain.Wave31OrGreaterPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicLepton);
            //
            //
            //Instant
            GameObject InfiniteTowerWaveSurprise = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSurprise", true);
            GameObject InfiniteTowerWaveSurpriseUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSurpriseUI", false);

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds = 1;
            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 1;

            InfiniteTowerWaveSurprise.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSurpriseUI;
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Surprise";
            InfiniteTowerWaveSurpriseUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All credits are spent immediately.";

            InfiniteTowerWaveCategory.WeightedWave ITBasicSurprise = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSurprise, weight = 1.5f, prerequisites = SimuMain.Wave26OrGreaterPrerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicSurprise);

            ModSupport();
        }

        internal static void ModSupport()
        {
            //Could do NemMando NemMerc but idk they'd just fucking die like in the regular game

            //Minor Mod - DireSeeker
            GameObject InfiniteTowerWaveBossDireseeker = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossDireseeker", true);
            GameObject InfiniteTowerCurrentBossDireseekerWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossDireseekerWaveUI", false);

            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 150;
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 5; //Evens out at 400 for wave 50
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

            InfiniteTowerWaveCategory.WeightedWave ITBossDireseeker = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossDireseeker, weight = 4f, prerequisites = SimuMain.Wave11OrGreaterPrerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBossDireseeker);
            //
            //
            //Will need to see what Empyrean Elites actually do
            GameObject InfiniteTowerWaveSS2RainbowElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveArtifactBomb.prefab").WaitForCompletion(), "InfiniteTowerWaveSS2RainbowElites", true);
            GameObject InfiniteTowerCurrentSS2RainbowElitesWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentArtifactBombWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentSS2RainbowElitesWaveUI", false);

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.5f;
            InfiniteTowerWaveSS2RainbowElites.GetComponent<CombatDirector>().eliteBias = 0.25f;

            InfiniteTowerWaveSS2RainbowElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentSS2RainbowElitesWaveUI;
            //InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = SS2Cognation.smallIconSelectedSprite;
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Empyrean";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Empyrean.";
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.8f, 1f);
            InfiniteTowerCurrentSS2RainbowElitesWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.5f, 1f);

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 1.5f };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
        }

        private static void FixHeresyForEnemies(On.RoR2.GenericSkill.orig_SetSkillOverride orig, GenericSkill self, object source, RoR2.Skills.SkillDef skillDef, GenericSkill.SkillOverridePriority priority)
        {
            if (priority == GenericSkill.SkillOverridePriority.Replacement && !self.characterBody.isPlayerControlled && self.stateMachine)
            {
                //Debug.Log(source);
                EntityStateMachine stateMachine = self.stateMachine;
                orig(self, source, skillDef, priority);
                if (stateMachine)
                {
                    self.stateMachine = stateMachine;
                }
                return;
            }
            else
            {
                orig(self, source, skillDef, priority);
            }
        }

        public static void CreateEquipmentDroneSpawnCards()
        {
            CharacterSpawnCard cscEquipmentDroneIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscEquipmentDroneIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EquipmentDroneMaster.prefab").WaitForCompletion();
            cscEquipmentDroneIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 15 },//0
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 0 },//1
                new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 },//2
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },//3
                new ItemCountPair { itemDef = RoR2Content.Items.CutHp, count = 0 }, //4
                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 0 }, //5
            };
            cscEquipmentDroneIT.name = "cscEquipmentDroneIT";

            /*
            EquipmentDef Blackhole = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/Blackhole/Blackhole.asset").WaitForCompletion();
            CharacterSpawnCard cscEquipmentDroneITBlackhole = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITBlackhole.name = "cscEquipmentDroneITBlackhole";
            cscEquipmentDroneITBlackhole.equipmentToGrant = new EquipmentDef[] { Blackhole };

            EquipmentDef BFG = Addressables.LoadAssetAsync<EquipmentDef>(key: "RoR2/Base/BFG/BFG.asset").WaitForCompletion();
            CharacterSpawnCard cscEquipmentDroneITBFG = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITBFG.name = "cscEquipmentDroneITBFG";
            cscEquipmentDroneITBFG.equipmentToGrant = new EquipmentDef[] { BFG };
             */

            CharacterSpawnCard cscEquipmentDroneITLightning = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITLightning.name = "cscEquipmentDroneITLightning";
            cscEquipmentDroneITLightning.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Lightning };
            cscEquipmentDroneITLightning.itemsToGrant[0].count = 3;
            cscEquipmentDroneITLightning.itemsToGrant[1].count = 80;  
            
            CharacterSpawnCard cscEquipmentDroneITMolotov = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMolotov.name = "cscEquipmentDroneITMolotov";
            cscEquipmentDroneITMolotov.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.Molotov };

            CharacterSpawnCard cscEquipmentDroneITFireBallDash = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITFireBallDash.name = "cscEquipmentDroneITFireBallDash";
            cscEquipmentDroneITFireBallDash.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.FireBallDash };
            cscEquipmentDroneITFireBallDash.itemsToGrant[0].count = 5;
            cscEquipmentDroneITFireBallDash.itemsToGrant[1].count = 50;
            cscEquipmentDroneITFireBallDash.itemsToGrant[4].count = 3;
            cscEquipmentDroneITFireBallDash.itemsToGrant[5].count = 3;

            CharacterSpawnCard cscEquipmentDroneITTeamWarCry = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITTeamWarCry.name = "cscEquipmentDroneITTeamWarCry";
            cscEquipmentDroneITTeamWarCry.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.TeamWarCry };
            cscEquipmentDroneITTeamWarCry.itemsToGrant[0].count = 25;

            CharacterSpawnCard cscEquipmentDroneITVendingMachine = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITVendingMachine.name = "cscEquipmentDroneITVendingMachine";
            cscEquipmentDroneITVendingMachine.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.VendingMachine };
            cscEquipmentDroneITVendingMachine.itemsToGrant[0].count = 25;
            cscEquipmentDroneITVendingMachine.itemsToGrant[1].count = 15;

            CharacterSpawnCard cscEquipmentDroneITMeteor = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMeteor.name = "cscEquipmentDroneITMeteor";
            cscEquipmentDroneITMeteor.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Meteor };
            cscEquipmentDroneITMeteor.itemsToGrant[0].count = 16;
            cscEquipmentDroneITMeteor.itemsToGrant[1].count = 15;
            cscEquipmentDroneITMeteor.itemsToGrant[4].count = 1;
            cscEquipmentDroneITMeteor.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITCrippleWard = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITCrippleWard.name = "cscEquipmentDroneITCrippleWard";
            cscEquipmentDroneITCrippleWard.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.CrippleWard };
            cscEquipmentDroneITCrippleWard.itemsToGrant[0].count = 12;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].count = 1;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITDeathProjectile = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITDeathProjectile.name = "cscEquipmentDroneITDeathProjectile";
            cscEquipmentDroneITDeathProjectile.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.DeathProjectile };
            cscEquipmentDroneITDeathProjectile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 15 },
                new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = RoR2Content.Items.Plant, count = 4 },
                new ItemCountPair { itemDef = RoR2Content.Items.Tooth, count = 4 },
            };


            AllCSCEquipmentDronesIT = new CharacterSpawnCard[] {                
                cscEquipmentDroneITDeathProjectile,
                //cscEquipmentDroneITBlackhole,
                cscEquipmentDroneITFireBallDash,
                cscEquipmentDroneITMolotov,
                cscEquipmentDroneITTeamWarCry,       
                cscEquipmentDroneITMeteor,
                cscEquipmentDroneITVendingMachine,
                cscEquipmentDroneITCrippleWard, 
            };
        }
    }




}