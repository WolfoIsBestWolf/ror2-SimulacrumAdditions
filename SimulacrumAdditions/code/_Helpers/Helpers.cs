using R2API;
using RoR2;
using RoR2.UI;
using UnityEngine;
using UnityEngine.UI;

namespace SimulacrumAdditions
{
    public static class H
    {
        public static System.Random random = new System.Random();

        public struct NewWaveInfo
        {

            public GameObject wavePrefab;
            public GameObject uiPrefab;
            public string name;

            public BasicPickupDropTable dropTable;
            public ItemTier itemTier;

            public int baseCredits;
            public int rewardOptionCount;
            public int bossWave;

            public InfiniteTowerWaveCategory waveCategory;
            public float weight;
            public InfiniteTowerWavePrerequisites prereq;
        }

        public struct NewWaveUIInfo
        {
            public string NameToken;
            public string DescToken;

            public Sprite icon;
            public Color nameColor;
            public Color descColor;
        }

        public static void MakeWave(NewWaveInfo waveInfo, NewWaveUIInfo uiInfo, out GameObject WAVE, out GameObject UI)
        {
            GameObject newWave = PrefabAPI.InstantiateClone(waveInfo.wavePrefab, "SimuWave_" + waveInfo.name, true);
            GameObject newWaveUI = PrefabAPI.InstantiateClone(waveInfo.uiPrefab, "SimuWaveUI_" + waveInfo.name, false);

            var WaveController = newWave.GetComponent<InfiniteTowerWaveController>();
            if (waveInfo.dropTable)
            {
                WaveController.rewardDisplayTier = waveInfo.itemTier;
                WaveController.rewardDropTable = waveInfo.dropTable;
            }
            if (waveInfo.rewardOptionCount != 0)
            {
                WaveController.rewardOptionCount = waveInfo.rewardOptionCount;
            }
            if (waveInfo.baseCredits != 0)
            {
                WaveController.baseCredits = waveInfo.baseCredits;
            }
            if (waveInfo.bossWave > 0)
            {
                WaveController.isBossWave = waveInfo.bossWave == 2;
            }
           

            WaveController.overlayEntries[1].prefab = newWaveUI;
            Transform uiRoot = newWaveUI.transform.GetChild(0);

            uiRoot.GetChild(1).GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = uiInfo.NameToken;
            uiRoot.GetChild(1).GetChild(1).GetComponent<LanguageTextMeshController>().token = uiInfo.DescToken;
            if (uiInfo.icon)
            {
                uiRoot.GetChild(0).GetChild(0).GetComponent<Image>().sprite = uiInfo.icon;
            }
            if (uiInfo.nameColor.a == 1)
            {

            }

            //Add Wave
            HG.ArrayUtils.ArrayAppend(ref waveInfo.waveCategory.wavePrefabs, new InfiniteTowerWaveCategory.WeightedWave
            {
                wavePrefab = newWave,
                weight = waveInfo.weight,
                prerequisites = waveInfo.prereq,
            });

            UI = newWaveUI;
            WAVE = newWave;
        }

        public static void SetWaveInfo(this GameObject ui, string name, string desc, Sprite icon)
        {
            SetWaveInfo(ui, name, desc, icon, Color.white, Color.white, false);
        }
        public static void SetWaveInfo(this GameObject ui, string name, string desc, Sprite icon, Color color)
        {
            SetWaveInfo(ui, name, desc, icon, color, color, true);
        }

        public static void SetWaveInfo(this GameObject ui, string name, string desc, Sprite icon, Color color1, Color color2)
        {
            SetWaveInfo(ui, name, desc, icon, color1, color2, true);
        }

        public static void SetWaveInfo(GameObject ui, string name, string desc, Sprite icon, Color color1, Color color2, bool setColor)
        {
            Transform root = ui.transform.GetChild(0);
            Transform root1 = root.GetChild(1);
            root1.GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = name;
            root1.GetChild(1).GetComponent<LanguageTextMeshController>().token = desc;
            if (icon != null)
            {
                root.GetChild(0).GetChild(0).GetComponent<Image>().sprite = icon;
            }
            if (setColor)
            {
                root1.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = color1;
                root.GetChild(2).GetComponent<Image>().color = color2;
            }

        }

        /*public static void SetWaveInfo(GameObject ui, string name, string desc, Color color, Sprite icon)
        {
            Transform root = ui.transform.GetChild(0);
            Transform root1 = root.GetChild(1);
            if (icon != null)
            {
                root.GetChild(0).GetChild(0).GetComponent<Image>().sprite = icon;
            }
            if (color != null)
            {
                root1.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().color = color;
                root.GetChild(2).GetComponent<Image>().color = color;
            }
            root1.GetChild(0).GetComponent<InfiniteTowerWaveCounter>().token = name;
            root1.GetChild(1).GetComponent<LanguageTextMeshController>().token = desc;
        }*/
    }


}
