/*
using R2API;
using RoR2;
//using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class JungleScene
    {
        static SceneDef ITRootJungle;

        public static void Test()
        {
            SceneDef rootjungle = LegacyResourcesAPI.Load<SceneDef>("SceneDefs/rootjungle");
            ITRootJungle = GameObject.Instantiate(rootjungle);
            ITRootJungle.cachedName = "itrootjungleW";
            ITRootJungle.loreToken = "MAP_ARENA_LORE";
            ITRootJungle.nameToken = "MAP_INFINITETOWER_TITLE";
            ITRootJungle.subtitleToken = "MAP_INFINITETOWER_SUBTITLE";
            ITRootJungle.stageOrder = 97;
            ITRootJungle.validForRandomSelection = false;
            ITRootJungle.shouldIncludeInLogbook = false;

            ContentAddition.AddSceneDef(ITRootJungle);

            On.RoR2.ClassicStageInfo.Awake += ClassicStageInfo_Awake;
            SceneDirector.onPrePopulateSceneServer += SceneDirector_onPrePopulateSceneServer;
            SceneDirector.onPostPopulateSceneServer += SceneDirector_onPostPopulateSceneServer;

        }

        private static void SceneDirector_onPostPopulateSceneServer(SceneDirector self)
        {
            if (SceneInfo.instance.sceneDef.cachedName.StartsWith("rootjungle"))
            {
                GameObject.Destroy(self.gameObject.GetComponents<CombatDirector>()[1]);
                GameObject.Destroy(self.gameObject.GetComponents<CombatDirector>()[0]);
            }
        }

        private static void SceneDirector_onPrePopulateSceneServer(SceneDirector self)
        {
            if (SceneInfo.instance.sceneDef.cachedName.StartsWith("rootjungle"))
            {
                GameObject.Instantiate(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/GameModes/InfiniteTowerRun/InfiniteTowerAssets/Weather, InfiniteTower.prefab").WaitForCompletion());

                GameObject Randomization = GameObject.Find("/HOLDER: Randomization");
                Randomization.transform.GetChild(0).gameObject.SetActive(false);
                Randomization.transform.GetChild(1).gameObject.SetActive(false);
                Randomization.transform.GetChild(2).gameObject.SetActive(false);
                Randomization.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                Randomization.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
                Randomization.transform.GetChild(4).gameObject.SetActive(false);

                GameObject Distant = GameObject.Find("/HOLDER: Distant Terrain");
                Distant.SetActive(false);

                GameObject Meteors = GameObject.Find("/HOLDER: Meteors");
                Meteors.SetActive(false);

                GameObject Weather = GameObject.Find("/HOLDER: Weather Set 1");
                Weather.SetActive(false);

                GameObject Terrain = GameObject.Find("/HOLDER: Terrain");
                Terrain.transform.GetChild(0).GetChild(18).gameObject.SetActive(false);
                Terrain.transform.GetChild(0).GetChild(20).gameObject.SetActive(false);
                Terrain.transform.GetChild(0).GetChild(21).gameObject.SetActive(false);

                ClassicStageInfo.instance.sceneDirectorInteractibleCredits = 0;
                ClassicStageInfo.instance.sceneDirectorMonsterCredits = 0;
                self.interactableCredit = 0;
                self.monsterCredit = 0;
                self.teleporterSpawnCard = null;
            }
        }


        private static void ClassicStageInfo_Awake(On.RoR2.ClassicStageInfo.orig_Awake orig, ClassicStageInfo self)
        {
            orig(self);
        }
    }


}
*/