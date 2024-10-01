using BepInEx;
using BepInEx.Configuration;

namespace SimulacrumAdditions
{
    public class WConfig
    {
        public static ConfigFile ConfigFileUNSORTED = new ConfigFile(Paths.ConfigPath + "\\Wolfo.Simulacrum_Additions.cfg", true);

        //public static ConfigEntry<bool> SimuMultiplayerChanges;

        //public static ConfigEntry<bool> SimulacrumEnemyItemChanges;
        public static ConfigEntry<bool> cfgSpeedUpOnLaterWaves;
        public static ConfigEntry<bool> cfgVoidTripleAllTier;
        public static ConfigEntry<bool> cfgVoidTripleContentsInPing;

        public static ConfigEntry<bool> cfgSimuCreditsRebalance;
        public static ConfigEntry<bool> cfgOnlySpecialBossesLate;
        public static ConfigEntry<bool> cfgDifferentTeleportEffect;
        public static ConfigEntry<bool> cfgVoidsEverywhere;
        public static ConfigEntry<bool> cfgExtraDifficuly;
        public static ConfigEntry<bool> cfgNewEnemiesVisible;
        public static ConfigEntry<bool> cfgDumpInfo;
        public static ConfigEntry<bool> cfgVoidCoins;
        public static ConfigEntry<bool> cfgEnableArtifact;
        public static ConfigEntry<bool> cfgMusicSuperBoss;
        public static ConfigEntry<bool> cfgSacrificeBalance;

        public static ConfigEntry<bool> cfgItemsEvery8;
        public static ConfigEntry<bool> cfgItemsFrequently;

        public static ConfigEntry<bool> cfgMakeSpecialWavesMoreCommon;

        public static ConfigEntry<bool> cfgAwaitTravel;


        public static ConfigEntry<bool> cfgCrabRadius;
        public static ConfigEntry<bool> cfgCrabRadiusPerPlayer;
        public static ConfigEntry<bool> cfgWarbannerOnBoss;

        public static ConfigEntry<int> cfgSimuEndingStartAtXWaves;
        public static ConfigEntry<int> cfgSimuEndingEveryXWaves;
        public static ConfigEntry<int> cfgSuperBossStartAtXWaves;
        public static ConfigEntry<int> cfgSuperBossEveryXWaves;


        public static void InitConfig()
        {
            cfgMusicSuperBoss = ConfigFileUNSORTED.Bind(
                "Main",
                "Music for Super Boss waves",
                true,
                "Like Mithrix theme for Mithrix wave."
            );
            cfgSpeedUpOnLaterWaves = ConfigFileUNSORTED.Bind(
                "Main",
                "Speed up as waves go on",
                true,
                "The crab will travel faster the more waves are completed, mostly noticible later waves."
            );
            cfgAwaitTravel = ConfigFileUNSORTED.Bind(
                "Main",
                "Crab waits before travelling",
                true,
                "Like after wave 5, more time to do stuff before leaving or just to leave instantly."
            );
            cfgEnableArtifact = ConfigFileUNSORTED.Bind(
                "Main",
                "Enable Artifact of Augments",
                true,
                "An Artifact that allows only special augments."
            );
            cfgVoidCoins = ConfigFileUNSORTED.Bind(
                 "Main",
                 "Add Void Coins",
                 true,
                 "Add previously unused Void Coins with which you can purchase Void Interactables. You can still purchase them with Blood if you do not have any."
             );
            cfgVoidTripleAllTier = ConfigFileUNSORTED.Bind(
                "Main",
                "Void Potential Chests can drop more tiers",
                true,
                "With this they can give items from any tier, normally they use a normal chest pool."
            );
            cfgDumpInfo = ConfigFileUNSORTED.Bind(
                "Main",
                "Wave Info Dump",
                false,
                "Dump wave info on startup in log"
            );
            cfgVoidTripleContentsInPing = ConfigFileUNSORTED.Bind(
                "Main",
                "Void Potential contents in ping message",
                true,
                "When pinging a Void Potential the items inside will be in the ping message. Requested for more easily sharing items inside Void Potentials."
            );

            cfgDifferentTeleportEffect = ConfigFileUNSORTED.Bind(
                "Main",
                "Simulacrum Teleport Effect",
                true,
                "Void themed teleport effect for Simulacrum runs"
            );
            cfgItemsEvery8 = ConfigFileUNSORTED.Bind(
                "Main : Balance",
                "Items every 8 Waves",
                true,
                "Default item period is every 8 waves. Fits better with the run ending at wave 50."
            );
            cfgItemsFrequently = ConfigFileUNSORTED.Bind(
                "Main : Balance",
                "Items are given more frequently later",
                true,
                "Default item period is every 8 waves. Fits better with the run ending at wave 50."
            );
            cfgVoidsEverywhere = ConfigFileUNSORTED.Bind(
                "Main",
                "Void Enemies on all stages",
                true,
                "After wave 60 void enemies will spawn on all simu stages."
            );
            cfgSimuCreditsRebalance = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Simulacrum Credits Rebalance",
                true,
                "Get more hold early on, Stages have 630 to 400 credits depending on stages completed and get more credits in multiplayer. Vanilla they always have 600."
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
            cfgNewEnemiesVisible = ConfigFileUNSORTED.Bind(
                "Main",
                "Logbook Entries",
                false,
                "Add reskins of enemies added by mod to Logbook"
            );


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
        }

    }
}