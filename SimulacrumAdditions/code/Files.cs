using BepInEx;
using System.IO;
using System.Linq;
using UnityEngine;
using System;

namespace SimulacrumAdditions
{
    //Taken from EnFucker, Thank you EnFucker
    internal static class Assets
    {

        public static AssetBundle Bundle;
        public static PluginInfo PluginInfo;
        public static string Folder = "SimulacrumMod\\";
   

        internal static string assemblyDir
        {
            get
            {
                return System.IO.Path.GetDirectoryName(PluginInfo.Location);
            }
        }

        internal static void Init(PluginInfo info)
        {
            PluginInfo = info;

            if (!Directory.Exists(GetPathToFile("SimulacrumMod")))
            {
                Folder = "";
            }


            if (Directory.Exists(GetPathToFile(Folder + "Languages")))
            {
                On.RoR2.Language.SetFolders += SetFolders;
            }
            else
            {
                Debug.LogWarning("COULD NOT FIND LANGUAGES FOLDER");
            }
            if (Directory.Exists(GetPathToFile(Folder + "AssetBundles")))
            {
                Bundle = AssetBundle.LoadFromFile(GetPathToFile(Folder + "AssetBundles", "simulacrumadditions"));
            }
            else
            {
                Debug.LogWarning("COULD NOT FIND ASSETBUNDLES FOLDER");
            }
        }

        private static void SetFolders(On.RoR2.Language.orig_SetFolders orig, RoR2.Language self, System.Collections.Generic.IEnumerable<string> newFolders)
        {
           var dirs = System.IO.Directory.EnumerateDirectories(Path.Combine(GetPathToFile(Folder + "Languages")), self.name);
           orig(self, newFolders.Union(dirs));
        }


        internal static string GetPathToFile(string folderName)
        {
            return Path.Combine(assemblyDir, folderName);
        }
        internal static string GetPathToFile(string folderName, string fileName)
        {
            return Path.Combine(assemblyDir, folderName, fileName);
        }
    }
}