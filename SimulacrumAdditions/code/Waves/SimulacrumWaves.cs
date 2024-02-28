using RoR2;
//using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using R2API;

namespace SimulacrumAdditions
{
    public class SimuWavesMain
    {
        //public static CharacterSpawnCard[] AllCSCEquipmentDronesIT;
        public static CardRandomizer CardRandomizerEquipmentDrones;
        public static MultiCSC CardRandomizerBasicGhost;
        public static MultiCSC CardRandomizerBossGhost;
        public static BuffDef bdSlippery;
        public static BuffDef bdBadLuck;

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

            /*CharacterSpawnCard cscScavLunarIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/ScavLunar/cscScavLunar.asset");
            cscScavLunarIT.name = "cscScavLunarIT";
            cscScavLunarIT.itemsToGrant = new ItemCountPair[] { new ItemCountPair { itemDef = AdaptiveArmor, count = 1 } };
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList[0].spawnCard = cscScavLunarIT;*/

            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().baseCredits = 50;
            InfiniteTowerWaveBossScavLunar.GetComponent<InfiniteTowerWaveController>().linearCreditsPerWave = 4;
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
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<CombatDirector>().monsterCards = dccsVoidFamilyNoBarnacle;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().secondsAfterWave += 2;
            InfiniteTowerWaveBossVoidRaidCrab.AddComponent<SimulacrumExtrasHelper>().rewardDisplayTier = ItemTier.VoidTier3;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSuperVoid;
            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<SimulacrumExtrasHelper>().newRadius = 160;

            InfiniteTowerWaveBossVoidRaidCrab.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossVoidRaidWaveUI;
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of「Voidling」";
            InfiniteTowerCurrentBossVoidRaidWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "D??ef?eat the「Diviner of the Deep」";
            
            InfiniteTowerWaveCategory.WeightedWave ITBossVoidRaidCrab = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossVoidRaidCrab, weight = ITSpecialBossWaveWeight+0.5f, prerequisites = SimuMain.StartWave50Prerequisite };
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
            InfiniteTowerWaveBossVoidElites.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 45;
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
            //
            //
            //Lunar Elite Wave
            GameObject InfiniteTowerWaveLunarElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveLunarElites", true);
            GameObject InfiniteTowerCurrentLunarEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossLunarWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentLunarEliteWaveUI", false);

            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusLunar;

            InfiniteTowerWaveLunarElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveLunarElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentLunarEliteWaveUI;

            Texture2D texITWaveLunarEliteIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveLunarEliteIcon.LoadImage(Properties.Resources.texITWaveLunarEliteIcon, true);
            texITWaveLunarEliteIcon.filterMode = FilterMode.Bilinear;
            Sprite texITWaveLunarEliteIconS = Sprite.Create(texITWaveLunarEliteIcon, WRect.rec64, WRect.half);

            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Perfection";
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Perfected.";
            //InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.3f,0.6f,1f);

            InfiniteTowerWaveCategory.WeightedWave ITLunarElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveLunarElites, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            //
            //
            //Malachite Elites
            GameObject InfiniteTowerWaveMalachitesOnly = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElites", true);
            GameObject InfiniteTowerWaveMalachitesOnlyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveMalachiteElitesUI", false);

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusGreen;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().baseCredits += 30;
            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 10;
            //InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds += 10;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveMalachitesOnly.GetComponent<CombatDirector>().skipSpawnIfTooCheap = false;  //Refuses to spawn anything on late waves if on
            InfiniteTowerWaveMalachitesOnly.AddComponent<SimuEquipmentWaveHelper>().variant = 2;

            InfiniteTowerWaveMalachitesOnly.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveMalachitesOnlyUI;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Malachite";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "All enemies spawn as weakened Malachites.";
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            //InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5f, 0.75f, 0.25f, 1);
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.5f, 0.75f, 0.25f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITEliteMalachite = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveMalachitesOnly, weight = 4.5f, prerequisites = SimuMain.StartWave30Prerequisite};
            //
            //
            //Void Elites
            GameObject InfiniteTowerWaveVoidElites = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidElites", true);
            GameObject InfiniteTowerCurrentVoidEliteWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentVoidEliteWaveUI", false);

            InfiniteTowerWaveVoidElites.GetComponent<CombatDirector>().eliteBias = 1f;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;

            BuffDef bdEliteVoid = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/EliteVoid/bdEliteVoid.asset").WaitForCompletion();
            InfiniteTowerWaveVoidElites.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentVoidEliteWaveUI;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Collapse";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Elites spawn as Voidtouched.";
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = bdEliteVoid.buffColor;

            InfiniteTowerWaveCategory.WeightedWave ITVoidElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidElites, weight = 6.5f, prerequisites = SimuMain.AfterWave5Prerequisite };

            //
            /*
            BuffDef AffixLunar = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/AffixLunar");
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = AffixLunar.iconSprite;
            InfiniteTowerCurrentLunarEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = AffixLunar.buffColor;

            BuffDef AffixPoison = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/AffixPoison");
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = AffixPoison.iconSprite;
            InfiniteTowerWaveMalachitesOnlyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = AffixPoison.buffColor;

            BuffDef bdEliteVoid = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/EliteVoid/bdEliteVoid.asset").WaitForCompletion();
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = bdEliteVoid.iconSprite;
            InfiniteTowerCurrentVoidEliteWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = bdEliteVoid.buffColor;*/

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
            InfiniteTowerWaveBossTitanGold.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 6;
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
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 1f, 0.6f, 1);
            InfiniteTowerCurrentBossTitanGoldWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 1f, 0.4f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossTitanGold = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossTitanGold, weight = ITSpecialBossWaveWeight + 0.3f, prerequisites = SimuMain.StartWave25Prerequisite };
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
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 6; //Evens out at 400 for wave 50
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120f;

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier3;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier3;

            InfiniteTowerWaveBossSuperRoboBallBoss.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;
            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<CombatDirector>().monsterCards = Addressables.LoadAssetAsync<DirectorCardCategorySelection>(key: "RoR2/Base/shipgraveyard/dccsShipgraveyardMonstersDLC1.asset").WaitForCompletion();

            InfiniteTowerWaveBossSuperRoboBallBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerCurrentBossSuperRoboBallBossWaveUI;

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of the Alloy Worship Unit";
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Defeat the Friend of Vultures.";

            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveLunarEliteIconS;
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 1f, 0.76f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0f, 0.9f, 0.6f, 1);
            InfiniteTowerCurrentBossSuperRoboBallBossWaveUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0f, 0.6f, 0.6f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossSuperRoboBallBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossSuperRoboBallBoss, weight = ITSpecialBossWaveWeight+0.3f, prerequisites = SimuMain.StartWave25Prerequisite };
            //
            //
            //Mountain Boss
            GameObject InfiniteTowerWaveDoubleBoss = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBoss", true);
            GameObject InfiniteTowerWaveDoubleBossUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveDoubleBossUI", false);

            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveDoubleBossUI;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().baseCredits *= 2.4f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsBeforeSuddenDeath *= 1.5f;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().secondsAfterWave = 14;
            InfiniteTowerWaveDoubleBoss.GetComponent<InfiniteTowerWaveController>().wavePeriodSeconds -= 10;
            InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().eliteBias = 0f;
            InfiniteTowerWaveDoubleBoss.GetComponent<CombatDirector>().skipSpawnIfTooCheap = true;
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

            InfiniteTowerWaveCategory.WeightedWave ITDoubleBoss = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveDoubleBoss, weight = 10f, prerequisites = SimuMain.StartWave11Prerequisite };
            //
            //
            
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITLunarElites, ITVoidElites, ITEliteMalachite);
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossSuperRoboBallBoss, ITBossTitanGold, ITDoubleBoss, ITBossVoidElites);

            ITBossSuperRoboBallBoss.weight = 0.5f;
            ITBossTitanGold.weight = 0.5f;
            SimuMain.ITSuperBossWaves.wavePrefabs = SimuMain.ITSuperBossWaves.wavePrefabs.Add(ITBossScavLunar, ITBossVoidRaidCrab, ITBossTitanGold, ITBossSuperRoboBallBoss);

            Organized();
        }

 
        internal static void LateChanges()
        {
            CreateEquipmentDroneSpawnCards();
            SimuWavesGhosts.CreateGhostSpawnCards();
            SimuWavesGhosts.CreateBossGhostSpawnCards();
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
            ItemDef CloverVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CloverVoid");
            ItemDef ExtraLifeVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/ExtraLifeVoid");
            ItemDef UseAmbientLevel = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/UseAmbientLevel");
            ItemDef SecondarySkillMagazine = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/SecondarySkillMagazine");
            ItemDef CritGlassesVoid = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/CritGlassesVoid");
            ItemDef AlienHead = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AlienHead");

            Texture2D texITWaveDefaultWhite = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texITWaveDefaultWhite.LoadImage(Properties.Resources.texITWaveDefaultWhite, true);
            texITWaveDefaultWhite.filterMode = FilterMode.Bilinear;
            Sprite texITWaveDefaultWhiteS = Sprite.Create(texITWaveDefaultWhite, WRect.rec64, WRect.half);

            //
            //
            //Equipment Drone Boss        
            GameObject InfiniteTowerWaveBossEquipmentDrone = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBossScav.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEquipmentDrone", true);
            GameObject InfiniteTowerCurrentBossEquipmentDroneWaveUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossBrotherUI.prefab").WaitForCompletion(), "InfiniteTowerCurrentBossEquipmentDroneWaveUI", false);

            InfiniteTowerWaveBossEquipmentDrone.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[] {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 1, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far },
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo { count = 3, spawnCard = null, spawnDistance = DirectorCore.MonsterSpawnDistance.Far }
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
            //Flight
            GameObject InfiniteTowerWaveJetpack = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveFlight", true);
            GameObject InfiniteTowerWaveJetpackUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveJetpackUI", false);


            InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            //
            InfiniteTowerWaveJetpack.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveJetpack.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            //InfiniteTowerWaveJetpack.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.3f;
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicJetpack = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveJetpack, weight = 6f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicJetpack);
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

            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 500;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 90;
            InfiniteTowerWaveBossGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 60;
            InfiniteTowerWaveBossGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 140;
            InfiniteTowerWaveBossGhostHaunting.AddComponent<SimulacrumExtrasHelper>().newRadius = 140;

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

            InfiniteTowerWaveCategory.WeightedWave ITBossGhostHaunting = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossGhostHaunting, weight = 15f, prerequisites = SimuMain.AfterWave5Prerequisite };
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

            InfiniteTowerWaveBasicGhostHaunting.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
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
                new ItemCountPair { itemDef = SimuMain.ITHealthScaling, count = 800 }, //10x hp
                new ItemCountPair { itemDef = CloverVoid, count = 1 },
                new ItemCountPair { itemDef = AutoCastEquipment, count = 2 },
                new ItemCountPair { itemDef = LunarBadLuck, count = 1 },
                new ItemCountPair { itemDef = AdaptiveArmor, count = 1 },
                new ItemCountPair { itemDef = UseAmbientLevel, count = 1 },
            };

            CharacterSpawnCard cscNullifierIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscNullifier"));
            cscNullifierIT.name = "cscNullifierIT";
            cscNullifierIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = BoostAttackSpeed, count = 3 },
                new ItemCountPair { itemDef = CutHP, count = 3 },
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().spawnList = new InfiniteTowerExplicitSpawnWaveController.SpawnInfo[]
            {
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscITCharacter, count = 0, spawnDistance = DirectorCore.MonsterSpawnDistance.Far},
                new InfiniteTowerExplicitSpawnWaveController.SpawnInfo{spawnCard = cscNullifierIT, count = 2},
            };

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().immediateCreditsFraction /= 2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().baseCredits = 400;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().secondsBeforeSuddenDeath = 120;
            InfiniteTowerWaveBossCharacters.AddComponent<SimulacrumExtrasHelper>().newRadius = 90;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerExplicitSpawnWaveController>().wavePeriodSeconds = 40;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier2;
            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBossGreenVoid;

            InfiniteTowerWaveBossCharacters.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossCharactersUI;
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Boss Augment of Cell Breach";
            //InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "This is a simulation everything is under control."; //"This is a simulation everything is under control."
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The subject is simulated, everything is under control."; //"This is a simulation everything is under control."

            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.8f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.8f, 0.6f, 1f, 1);
            InfiniteTowerWaveBossCharactersUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.6f, 0.4f, 0.8f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBossCharacters = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossCharacters, weight = 7f }; //This is a very basic wave modifier with a slight extra reward idk how common
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossCharacters);
            //
            //
            //Battery
            GameObject InfiniteTowerWaveBattery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBattery", true);
            GameObject InfiniteTowerWaveBatteryUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBatteryUI", false);

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            //
            InfiniteTowerWaveBattery.AddComponent<SimulacrumExtrasHelper>().rewardDropTable = SimuMain.dtITSpecialEquipment;
            //InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardDisplayTier = //Orange;
            InfiniteTowerWaveBattery.GetComponent<SimulacrumExtrasHelper>().rewardOptionCount = 2;

            //InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().baseCredits *= 1.6f;
            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().immediateCreditsFraction = 0.15f;
            InfiniteTowerWaveBattery.GetComponent<CombatDirector>().eliteBias = 80000;
            InfiniteTowerWaveBattery.AddComponent<SimuEquipmentWaveHelper>().variant = 1;

            InfiniteTowerWaveBattery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBatteryUI;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Battery";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Improper storage may cause serious harm.";
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texItEquipmentBasicS;
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(1f, 0.65f, 0.25f, 1);
            InfiniteTowerWaveBatteryUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(1f, 0.55f, 0.1f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicBattery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBattery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
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
            };

            On.EntityStates.QuestVolatileBattery.CountDown.Detonate += (orig, self) =>
            {
                if (!self.networkedBodyAttachment.attachedBody)
                {
                    return;
                }
                bool suicide = false;
                if (self.networkedBodyAttachment.attachedBody.teamComponent.teamIndex == TeamIndex.Monster)
                {
                    suicide = true;
                    float newHealth = (77 + 33 * TeamManager.instance.GetTeamLevel(TeamIndex.Player))/6; //*3
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
            };
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
            InfiniteTowerWaveBrotherHaunt.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 0;
            InfiniteTowerWaveBrotherHaunt.AddComponent<SimulacrumExtrasHelper>().newRadius = 80;

            //If we made him Void Team the enemies would probably go after him when he's not actually real
            CharacterSpawnCard cscBrotherHauntIT = Object.Instantiate(LegacyResourcesAPI.Load<CharacterSpawnCard>("SpawnCards/CharacterSpawnCards/cscBrother"));
            cscBrotherHauntIT.name = "cscBrotherHauntIT";
            cscBrotherHauntIT.prefab = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterMasters/BrotherHauntMaster");
            cscBrotherHauntIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = SimuMain.ITHorrorName, count = 1 },
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 45 }
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

            InfiniteTowerWaveCategory.WeightedWave ITBrotherHaunt = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBrotherHaunt, weight = 5f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBrotherHaunt);
            //
            //
            //VoidBear
            GameObject InfiniteTowerWaveVoidBear = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidBear", true);
            GameObject InfiniteTowerWaveVoidBearUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossVoidWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveVoidBearUI", false);

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITBasicBonusVoid;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().maxSquadSize = 15;
            InfiniteTowerWaveVoidBear.AddComponent<SimuBuffWaveHelper>().variant = 0;
            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;

            InfiniteTowerWaveVoidBear.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveVoidBearUI;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Safety";
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Enemies block the first instances of damage.";

            BuffDef BearVoidReady = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/BearVoidReady");
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = BearVoidReady.iconSprite;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BearVoidReady.buffColor;
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BearVoidReady.buffColor;
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.7f, 1f, 1);
            //InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.85f, 0.55f, 1f, 1);
            InfiniteTowerWaveVoidBearUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.675f, 0.425f, 0.825f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITBasicVoidBear = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveVoidBear, weight = 5f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITBasicVoidBear);
            //
            //
            //ITBossEclipse8
            GameObject InfiniteTowerWaveBossEclipse8 = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveBoss.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEclipse8", true);
            GameObject InfiniteTowerWaveBossEclipse8UI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentBossWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBossEclipse8", false);

            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBossEclipse8UI;
            InfiniteTowerWaveBossEclipse8.GetComponent<InfiniteTowerWaveController>().baseCredits = 500;
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

            InfiniteTowerWaveCategory.WeightedWave ITBossEclipse8 = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBossEclipse8, weight = 10f, prerequisites = SimuMain.StartWave20Prerequisite };
            SimuMain.ITBossWaves.wavePrefabs = SimuMain.ITBossWaves.wavePrefabs.Add(ITBossEclipse8);
            //
            //
            //Blindness Buff
            GameObject InfiniteTowerWaveBlindness = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindness", true);
            GameObject InfiniteTowerWaveBlindnessUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBlindnessUI", false);

            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBlindnessUI;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBlindness.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBlindness.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().addToEnemies = false;
            InfiniteTowerWaveBlindness.GetComponent<SimuBuffWaveHelper>().buffDef = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/Common/Buffs/bdBlinded.asset").WaitForCompletion();

            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Blindness";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It becomes hard to see.";
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.2f, 0.2f);
            InfiniteTowerWaveBlindnessUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0);

            InfiniteTowerWaveCategory.WeightedWave Blindness = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBlindness, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Blindness);
            //
            //
            //Slippery Buff
            GameObject InfiniteTowerWaveSlippery = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveSlippery", true);
            GameObject InfiniteTowerWaveSlipperyUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveSlipperyUI", false);

            BuffDef Weak = RoR2.LegacyResourcesAPI.Load<BuffDef>("buffdefs/Weak");
            BuffDef bdOutOfCombatArmorBuff = Addressables.LoadAssetAsync<BuffDef>(key: "RoR2/DLC1/OutOfCombatArmor/bdOutOfCombatArmorBuff.asset").WaitForCompletion();
            bdSlippery = ScriptableObject.CreateInstance<BuffDef>();
            bdSlippery.iconSprite = bdOutOfCombatArmorBuff.iconSprite;
            bdSlippery.buffColor = new Color(0.9f, 0.8f, 1f);
            bdSlippery.name = "bdITSlippery";
            bdSlippery.isDebuff = false;
            bdSlippery.canStack = false;
            bdSlippery.isCooldown = false;
            ContentAddition.AddBuffDef(bdSlippery);

            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveSlipperyUI;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveSlippery.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveSlippery.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveSlippery.GetComponent<SimuBuffWaveHelper>().buffDef = bdSlippery;

            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Friction";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "The ground turns slippery.";
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.9f, 0.8f, 1f);
            InfiniteTowerWaveSlipperyUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.8f, 0.7f, 0.9f);

            InfiniteTowerWaveCategory.WeightedWave Slippery = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSlippery, weight = 4f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(Slippery);
            //
            //
            //BadLuck Buff
            GameObject InfiniteTowerWaveBadLuck = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerWaveDefault.prefab").WaitForCompletion(), "InfiniteTowerWaveBadLuck", true);
            GameObject InfiniteTowerWaveBadLuckUI = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/InfiniteTowerCurrentWaveUI.prefab").WaitForCompletion(), "InfiniteTowerWaveBadLuckUI", false);
            Color BadLuckColor = new Color(0.7f, 0.8f, 0.3f);

            bdBadLuck = ScriptableObject.CreateInstance<BuffDef>();
            bdBadLuck.iconSprite = Weak.iconSprite;
            bdBadLuck.buffColor = BadLuckColor;
            bdBadLuck.name = "bdITBadLuck";
            bdBadLuck.isDebuff = false;
            bdBadLuck.canStack = false;
            bdBadLuck.isCooldown = false;
            ContentAddition.AddBuffDef(bdBadLuck);

            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveBadLuckUI;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardDropTable = SimuMain.dtITWaveTier1;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardDisplayTier = ItemTier.Tier1;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().rewardOptionCount = 6;
            InfiniteTowerWaveBadLuck.GetComponent<InfiniteTowerWaveController>().baseCredits = 160;
            InfiniteTowerWaveBadLuck.AddComponent<SimuBuffWaveHelper>().variant = -1;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().addToPlayer = true;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().addToEnemies = false;
            InfiniteTowerWaveBadLuck.GetComponent<SimuBuffWaveHelper>().buffDef = bdBadLuck;

            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Misfortune";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "Luck down.";
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().sprite = texITWaveDefaultWhiteS;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = BadLuckColor;
            InfiniteTowerWaveBadLuckUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = BadLuckColor;

            InfiniteTowerWaveCategory.WeightedWave BadLuck = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveBadLuck, weight = 4f, prerequisites = SimuMain.StartWave11Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(BadLuck);
            //
            //
            //
            ItemDef HealthDecay = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/HealthDecay");
            BuffDef BanditSkull = LegacyResourcesAPI.Load<BuffDef>("BuffDefs/DeathMark");
            //
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
            secondDirector.maxRerollSpawnInterval = 0f;
            secondDirector.minRerollSpawnInterval = 0f;
            //RerollSpawnInterval like closer to how long between each wave
            //minSeriesSpawnInterval


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
            waveHelper.spawnsOnStart = 0;

            InfiniteTowerWaveTarPots.GetComponent<InfiniteTowerWaveController>().overlayEntries[1].prefab = InfiniteTowerWaveTarPotsUI;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RoR2.UI.InfiniteTowerWaveCounter>().token = "Wave {0} - Augment of Tar";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<RoR2.UI.LanguageTextMeshController>().token = "It's raining pots of tar.";
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1)*2; //0.2 0.0902 0.0902 1
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.2f, 0.0902f, 0.0902f, 1) * 2;
            InfiniteTowerWaveTarPotsUI.transform.GetChild(0).GetChild(2).GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.0902f, 0.0902f, 1);

            InfiniteTowerWaveCategory.WeightedWave ITTarPots = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveTarPots, weight = 3f, prerequisites = SimuMain.AfterWave5Prerequisite };
            SimuMain.ITBasicWaves.wavePrefabs = SimuMain.ITBasicWaves.wavePrefabs.Add(ITTarPots);
            //
            ModSupport();
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
            InfiniteTowerWaveBossDireseeker.GetComponent<InfiniteTowerExplicitSpawnWaveController>().linearCreditsPerWave = 6; //Evens out at 400 for wave 50
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

            InfiniteTowerWaveCategory.WeightedWave ITBasicSS2RainbowElites = new InfiniteTowerWaveCategory.WeightedWave { wavePrefab = InfiniteTowerWaveSS2RainbowElites, weight = 2f, prerequisites = SimuMain.StartWave30Prerequisite };
            SimuMain.ITModSupportWaves.wavePrefabs = SimuMain.ITModSupportWaves.wavePrefabs.Add(ITBasicSS2RainbowElites);
        }

        public static void CreateEquipmentDroneSpawnCards()
        {
            CharacterSpawnCard cscEquipmentDroneIT = Object.Instantiate(Addressables.LoadAssetAsync<CharacterSpawnCard>(key: "RoR2/Base/Titan/cscTitanGold.asset").WaitForCompletion());
            cscEquipmentDroneIT.name = "cscEquipmentDroneIT";
            cscEquipmentDroneIT.nodeGraphType = RoR2.Navigation.MapNodeGroup.GraphType.Air;
            cscEquipmentDroneIT.noElites = true;
            cscEquipmentDroneIT.prefab = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Drones/EquipmentDroneMaster.prefab").WaitForCompletion();
            cscEquipmentDroneIT.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 15 },//0
                new ItemCountPair { itemDef = SimuMain.ITDamageDown, count = 0 },//1
                new ItemCountPair { itemDef = RoR2Content.Items.AdaptiveArmor, count = 1 },//2
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },//3
                new ItemCountPair { itemDef = RoR2Content.Items.CutHp, count = 0 }, //4
                new ItemCountPair { itemDef = DLC1Content.Items.HalfSpeedDoubleHealth, count = 0 }, //5
            };
           
            CharacterSpawnCard cscEquipmentDroneITLightning = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITLightning.name = "cscEquipmentDroneITLightning";
            cscEquipmentDroneITLightning.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Lightning };
            cscEquipmentDroneITLightning.itemsToGrant[0].count = 3;
            cscEquipmentDroneITLightning.itemsToGrant[1].count = 80;  
            
            CharacterSpawnCard cscEquipmentDroneITMolotov = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMolotov.name = "cscEquipmentDroneITMolotov";
            cscEquipmentDroneITMolotov.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.Molotov };
            cscEquipmentDroneITMolotov.itemsToGrant[1].count = 20;

            CharacterSpawnCard cscEquipmentDroneITFireBallDash = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITFireBallDash.name = "cscEquipmentDroneITFireBallDash";
            cscEquipmentDroneITFireBallDash.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.FireBallDash };
            cscEquipmentDroneITFireBallDash.itemsToGrant[0].count = 3;
            cscEquipmentDroneITFireBallDash.itemsToGrant[1].count = 70;
            cscEquipmentDroneITFireBallDash.itemsToGrant[5].count = 2;

            CharacterSpawnCard cscEquipmentDroneITTeamWarCry = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITTeamWarCry.name = "cscEquipmentDroneITTeamWarCry";
            cscEquipmentDroneITTeamWarCry.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.TeamWarCry };
            cscEquipmentDroneITTeamWarCry.itemsToGrant[0].count = 25;

            CharacterSpawnCard cscEquipmentDroneITVendingMachine = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITVendingMachine.name = "cscEquipmentDroneITVendingMachine";
            cscEquipmentDroneITVendingMachine.equipmentToGrant = new EquipmentDef[] { DLC1Content.Equipment.VendingMachine };
            cscEquipmentDroneITVendingMachine.itemsToGrant[0].count = 24;
            cscEquipmentDroneITVendingMachine.itemsToGrant[1].count = 25;

            CharacterSpawnCard cscEquipmentDroneITMeteor = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITMeteor.name = "cscEquipmentDroneITMeteor";
            cscEquipmentDroneITMeteor.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.Meteor };
            cscEquipmentDroneITMeteor.itemsToGrant[0].count = 15;
            cscEquipmentDroneITMeteor.itemsToGrant[1].count = 20;
            cscEquipmentDroneITMeteor.itemsToGrant[4].count = 1;
            cscEquipmentDroneITMeteor.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITCrippleWard = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITCrippleWard.name = "cscEquipmentDroneITCrippleWard";
            cscEquipmentDroneITCrippleWard.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.CrippleWard };
            cscEquipmentDroneITCrippleWard.itemsToGrant[0].count = 10;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].count = 1;
            cscEquipmentDroneITCrippleWard.itemsToGrant[4].itemDef = RoR2Content.Items.ShieldOnly;

            CharacterSpawnCard cscEquipmentDroneITDeathProjectile = Object.Instantiate(cscEquipmentDroneIT);
            cscEquipmentDroneITDeathProjectile.name = "cscEquipmentDroneITDeathProjectile";
            cscEquipmentDroneITDeathProjectile.equipmentToGrant = new EquipmentDef[] { RoR2Content.Equipment.DeathProjectile }; //45s
            cscEquipmentDroneITDeathProjectile.itemsToGrant = new ItemCountPair[] {
                new ItemCountPair { itemDef = RoR2Content.Items.BoostEquipmentRecharge, count = 3 },
                new ItemCountPair { itemDef = SimuMain.ITKillOnCompletion, count = 2 },
                new ItemCountPair { itemDef = RoR2Content.Items.Plant, count = 4 },
                new ItemCountPair { itemDef = RoR2Content.Items.Tooth, count = 3 },
            };

            CardRandomizerEquipmentDrones.cscList = new CharacterSpawnCard[] {
                cscEquipmentDroneITDeathProjectile,
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