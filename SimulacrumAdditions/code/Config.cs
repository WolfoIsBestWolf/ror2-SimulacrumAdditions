using BepInEx;
using BepInEx.Configuration;
using RiskOfOptions;
using RiskOfOptions.Options;
using UnityEngine;

namespace SimulacrumAdditions
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Simulacrum_Additions.cfg", true);

        //public static ConfigEntry<bool> SimuMultiplayerChanges;

        //public static ConfigEntry<bool> SimulacrumEnemyItemChanges;
        public static ConfigEntry<bool> cfgFasterWavesLater;
        public static ConfigEntry<bool> cfgCrabSpeedOnLaterWaves;
       
        public static ConfigEntry<bool> cfgSimuCreditsRebalance;
        public static ConfigEntry<bool> cfgSimuMoreGold;
        //public static ConfigEntry<bool> cfgOnlySpecialBossesLate;
        public static ConfigEntry<bool> cfgDifferentTeleportEffect;
        public static ConfigEntry<bool> cfgVoidsEverywhere;
        public static ConfigEntry<bool> cfgExtraDifficuly;
        public static ConfigEntry<bool> cfgNewEnemiesVisible;
        public static ConfigEntry<bool> cfgDumpInfo;
        public static ConfigEntry<bool> cfgVoidCoins;
        //public static ConfigEntry<bool> cfgEnableArtifactAugments;
        //public static ConfigEntry<bool> cfgEnableArtifactStages;
        public static ConfigEntry<bool> cfgMusicChanges;
        public static ConfigEntry<bool> cfgMusicSuperBoss;
        public static ConfigEntry<bool> cfgSacrificeBalance;

        public static ConfigEntry<bool> cfgItemsEvery8;
        public static ConfigEntry<bool> cfgItemsFrequently;

        public static ConfigEntry<bool> cfgMakeSpecialWavesMoreCommon;

        public static ConfigEntry<bool> cfgAwaitTravel;
        
        public static ConfigEntry<float> ArtifactOfRealityBonusRadius;
        public static ConfigEntry<float> cfgCrabRadius;
        public static ConfigEntry<float> cfgCrabRadiusPerPlayer;
        //public static ConfigEntry<bool> cfgWarbannerOnBoss;

        public static ConfigEntry<int> cfgSimuEndingStartAtXWaves;
        public static ConfigEntry<int> cfgSimuEndingEveryXWaves;
        public static ConfigEntry<int> cfgSuperBossStartAtXWaves;
        public static ConfigEntry<int> cfgSuperBossEveryXWaves;

        public static ConfigEntry<bool> ResetStatsButton;

        public static void RiskConfig()
        {
            ModSettingsManager.SetModDescription("Simulacrum Gaming");
            ModSettingsManager.SetModIcon(Assets.Bundle.LoadAsset<Sprite>("Assets/Simulacrum/Wave/waveGupYellow.png"));

            ModSettingsManager.AddOption(new CheckBoxOption(cfgNewEnemiesVisible, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgVoidCoins));
             
            ModSettingsManager.AddOption(new CheckBoxOption(cfgSacrificeBalance));

            ModSettingsManager.AddOption(new CheckBoxOption(cfgFasterWavesLater));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgExtraDifficuly));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgSimuMoreGold, false));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgSimuCreditsRebalance, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgItemsEvery8, true));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgItemsFrequently, true));


            ModSettingsManager.AddOption(new CheckBoxOption(cfgCrabSpeedOnLaterWaves));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgMusicChanges));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgMusicSuperBoss));
            //ModSettingsManager.AddOption(new CheckBoxOption(cfgMakeSpecialWavesMoreCommon));
            ModSettingsManager.AddOption(new CheckBoxOption(cfgAwaitTravel));

            ModSettingsManager.AddOption(new FloatFieldOption(ArtifactOfRealityBonusRadius));
            ModSettingsManager.AddOption(new FloatFieldOption(cfgCrabRadius));
            ModSettingsManager.AddOption(new FloatFieldOption(cfgCrabRadiusPerPlayer));




            ModSettingsManager.AddOption(new CheckBoxOption(ResetStatsButton));


        }


        public static void InitConfig()
        {
            ResetStatsButton = ConfigFileUNSORTED.Bind(
                "Main",
                "Reset Wave Count Button",
                false,
                "Add buttons to reset wave count or set it to 50."
            );

            cfgMusicChanges = ConfigFileUNSORTED.Bind(
                "Main",
                "Simu Stage Music Changes",
                true,
                "Changes the music of some simu stages.\nPlains -> Void Fields\n"
            );

            cfgMusicSuperBoss = ConfigFileUNSORTED.Bind(
                "Main",
                "Music for Super Boss waves",
                true,
                "Like Mithrix theme for Mithrix wave."
            );

            cfgVoidCoins = ConfigFileUNSORTED.Bind(
                 "Main",
                 "Add Void Coins",
                 true,
                 "Add previously unused Void Coins with which you can purchase Void Interactables. You can still purchase them with Blood if you do not have any."
             );
            

            cfgNewEnemiesVisible = ConfigFileUNSORTED.Bind(
                "Main",
                "Logbook Entries for mod content",
                false,
                "Add reskins of enemies added by mod to Logbook."
            );
            cfgDifferentTeleportEffect = ConfigFileUNSORTED.Bind(
                "Main",
                "Simulacrum Teleport Effect",
                true,
                "Void themed teleport effect for Simulacrum runs"
            );
 
            //
            //Crab stuff
            cfgCrabSpeedOnLaterWaves = ConfigFileUNSORTED.Bind(
                "Main : Crab",
                "Speed up as waves go on",
                true,
                "The crab will travel faster the more waves are completed, mostly noticible later waves."
            );
            cfgAwaitTravel = ConfigFileUNSORTED.Bind(
                "Main : Crab",
                "Crab waits before travelling",
                true,
                "Like after wave 5, more time to do stuff before leaving or just to leave instantly."
            );
            cfgCrabRadius = ConfigFileUNSORTED.Bind(
                 "Main : Crab",
                 "Crab Radius : Base Radius",
                 65f,
                 "Vanilla radius is 60."
             );
            cfgCrabRadiusPerPlayer = ConfigFileUNSORTED.Bind(
                 "Main : Crab",
                 "Crab Radius : Per Player",
                 5f,
                 "Radius increase per player including player 1. Vanilla radius is 0."
             );
            cfgVoidsEverywhere = ConfigFileUNSORTED.Bind(
                "Main",
                "Void Enemies on all stages",
                true,
                "After wave 60 void enemies will spawn on all simu stages."
            );
            //
            //Artifacts
            /* cfgEnableArtifactAugments = ConfigFileUNSORTED.Bind(
                 "Main : Artifacts",
                 "Enable Artifact of Augments",
                 true,
                 "An Artifact that allows only special augments."
             );
             cfgEnableArtifactStages = ConfigFileUNSORTED.Bind(
                 "Main : Artifacts",
                 "Enable Artifact of Reality",
                 true,
                 "An Artifact that makes the game mode use normal stages instead of simu variants"
             );*/
            ArtifactOfRealityBonusRadius = ConfigFileUNSORTED.Bind(
                "Main : Artifacts",
                "Artifact of Reality : Larger Radius",
                10f,
                "Bonus radius since default stages are often bigger and have weird cornerns"
            );

            //
            //Balance Stuff
            cfgItemsEvery8 = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Items every 8 Waves",
                true,
                "Give items every 8 waves. Fits better with the run ending at wave 50."
            );
            cfgItemsFrequently = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Items are given more frequently later",
                true,
                "Items will be added twice as frequently after the first Red"
            );
            cfgSimuCreditsRebalance = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Simulacrum Credits Rebalance",
                true,
                "Get more credits early on, Stages have 630 to 400 credits depending on stages completed and get more credits in multiplayer. Vanilla they always have 600 regardless of stage/player count."
            );
            cfgSimuMoreGold = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Simulacrum Gold Balance",
                true,
                "Gold is multiplied per player amount. Gain 50% more gold at the start and 25% less gold starting at wave 30. Helps with early looting and makes it so you won't instantly be able to afford every chest later on."
            );
            cfgExtraDifficuly = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "More scaling",
                true,
                "Scales immediate wave spawns up and elite cost down to overall increase difficulty but also make waves shorter"
            );
            cfgSacrificeBalance = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Nerf Sacrifice in Simulacrum",
                true,
                "30% less item drops from enemies and 1 less option in potentials"
            );
            cfgFasterWavesLater = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Waves get faster later",
                true,
                "Less time between waves and waves spawn enemies faster."
            );
            //
            //
            cfgSimuEndingStartAtXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Ending Portal",
                "Ending Start Wave",
                50,
                "This is the first wave the ending Portal appears (only use steps of 10)"
            );
            cfgSimuEndingEveryXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Ending Portal",
                "Ending Portal Every X Waves",
                10,
                "The ending portal will appear every X waves. (only use steps of 10)"
            );
            cfgSuperBossStartAtXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Forced Super Boss",
                "Forced Special Boss Start Wave",
                50,
                "A forced special boss is meant to be paired with the wave where the ending portal spawns so it's less of just a random end. (only use steps of 10)"
            );
            cfgSuperBossEveryXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Forced Super Boss",
                "Forced Special Boss Every X Waves",
                30,
                "The forced special boss will appear every X waves. (only use steps of 10)"
            );

            ////////////////////////
            cfgDumpInfo = ConfigFileUNSORTED.Bind(
            "Testing",
                "Wave Info Dump",
                false,
                "Dump wave info on startup in log"
            );


        }

    }
}