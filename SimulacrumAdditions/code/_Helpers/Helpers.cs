using System.Linq;
using UnityEngine;
using RoR2;
using RoR2.UI;
using UnityEngine.UI;
using JetBrains.Annotations;
 
namespace SimulacrumAdditions
{
    public static class H
    {
        public static System.Random random = new System.Random();


        public static void SetWaveInfo(this GameObject ui, string name, string desc, Sprite icon)
        {
            SetWaveInfo(ui, name, desc, icon, Color.white, Color.white, false);
        }
        public static void SetWaveInfo(this GameObject ui, string name, string desc, Sprite icon,  Color color)
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
