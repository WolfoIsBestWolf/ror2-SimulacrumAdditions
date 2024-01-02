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
        public static ConfigEntry<bool> cfgMoreItems;
        public static ConfigEntry<bool> cfgSimuCreditsRebalance;
        public static ConfigEntry<bool> cfgOnlySpecialBossesLate;
        public static ConfigEntry<bool> cfgDifferentTeleportEffect;
        public static ConfigEntry<bool> cfgVoidsEverywhere;
        public static ConfigEntry<bool> cfgExtraDifficuly;
        public static ConfigEntry<bool> cfgNewEnemiesVisible;
        public static ConfigEntry<bool> cfgDumpInfo;
        public static ConfigEntry<bool> cfgVoidCoins;

        public static ConfigEntry<int> cfgSimuEndingStartAtXWaves;
        public static ConfigEntry<int> cfgSimuEndingEveryXWaves;
        public static ConfigEntry<int> cfgSuperBossStartAtXWaves;
        public static ConfigEntry<int> cfgSuperBossEveryXWaves;


        public static void InitConfig()
        {
            cfgSpeedUpOnLaterWaves = ConfigFileUNSORTED.Bind(
                "Main",
                "Speed up as waves go on",
                true,
                "The crab will travel faster the more waves are completed, mostly noticible later waves."
            );
            cfgVoidCoins = ConfigFileUNSORTED.Bind(
                 "Main",
                 "Add Void Coins",
                 true,
                 "Add preveiously unused Void Coins with which you can purchase Void Interactables. You can still purchase them with Blood if you do not have any."
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

            cfgDifferentTeleportEffect = ConfigFileUNSORTED.Bind(
                "Main",
                "Simulacrum Teleport Effect",
                true,
                "Void themed teleport effect for Simulacrum runs"
            );
            cfgMoreItems = ConfigFileUNSORTED.Bind(
                "Main",
                "Enemies gain items quicker",
                true,
                "Every 8 waves until wave 40, 4 waves until 60, 2 waves until 70, 1 every wave after that."
            );
            cfgVoidsEverywhere = ConfigFileUNSORTED.Bind(
                "Main",
                "Void Enemies on all stages",
                true,
                "After wave 70 void enemies will spawn on all simu stages."
            );
            cfgSimuCreditsRebalance = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "Simulacrum Credits Rebalance",
                true,
                "Stages have 700 to 400 credits depending on stages completed and get more credits in multiplayer. Vanilla they always have 600."
            );
            cfgExtraDifficuly = ConfigFileUNSORTED.Bind(
                "Simulacrum : Balance",
                "More scaling",
                true,
                "Scales immediate wave spawns up and elite cost down to overall increase difficulty but also make waves shorter"
            );
            cfgNewEnemiesVisible = ConfigFileUNSORTED.Bind(
                "Main",
                "Logbook Entries",
                false,
                "Add reskins of enemies added by mod to Logbook"
            );


            cfgSimuEndingStartAtXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Ending",
                "Ending Start Wave",
                60,
                "This is the first wave the ending Portal appears (only use steps of 10)"
            );
            cfgSimuEndingEveryXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Ending",
                "Ending Portal Every X Waves",
                10,
                "The ending portal will appear every X waves. (only use steps of 10)"
            );
            cfgSuperBossStartAtXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Forced Boss",
                "Forced Special Boss Start Wave",
                60,
                "A forced special boss is meant to be paired with the wave where the ending portal spawns so it's less of just a random end. (only use steps of 10)"
            );
            cfgSuperBossEveryXWaves = ConfigFileUNSORTED.Bind(
                "Simulacrum : Forced Boss",
                "Forced Special Boss Every X Waves",
                30,
                "The forced special boss will appear every X waves. (only use steps of 10)"
            );

            ////////////////////////
        }

    }
}