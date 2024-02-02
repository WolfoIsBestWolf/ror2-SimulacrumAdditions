using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsLoader
    {
        internal static void LoaderSkin()
        {
            SkinDef skinLoaderDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderDefault.asset").WaitForCompletion();
            SkinDef skinLoaderAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Loader/skinLoaderAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinLoaderAlt.rendererInfos.Length];
            System.Array.Copy(skinLoaderAlt.rendererInfos, NewRenderInfos, skinLoaderAlt.rendererInfos.Length);

            Material matTrimsheetConstructionLoaderAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[0].defaultMaterial);
            Material matLoaderPilotDiffuseAlt = Object.Instantiate(skinLoaderAlt.rendererInfos[1].defaultMaterial);


            Texture2D texTrimSheetConstructionLoaderAlt = new Texture2D(256, 512, TextureFormat.DXT5, false);
            texTrimSheetConstructionLoaderAlt.LoadImage(Properties.Resources.texTrimSheetConstructionLoaderAlt1, true);
            texTrimSheetConstructionLoaderAlt.filterMode = FilterMode.Bilinear;
            texTrimSheetConstructionLoaderAlt.wrapMode = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeU = TextureWrapMode.Repeat;
            texTrimSheetConstructionLoaderAlt.wrapModeV = TextureWrapMode.Clamp;
            texTrimSheetConstructionLoaderAlt.wrapModeW = TextureWrapMode.Clamp;

            Texture2D texLoaderPilotDiffuse = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texLoaderPilotDiffuse.LoadImage(Properties.Resources.texLoaderPilotDiffuse, true);
            texLoaderPilotDiffuse.filterMode = FilterMode.Bilinear;
            texLoaderPilotDiffuse.wrapMode = TextureWrapMode.Clamp;

            matTrimsheetConstructionLoaderAlt.mainTexture = texTrimSheetConstructionLoaderAlt;
            matLoaderPilotDiffuseAlt.mainTexture = texLoaderPilotDiffuse;
            matLoaderPilotDiffuseAlt.SetColor("_EmColor", new Color(1f, 0.8414f, 0f, 1));

            NewRenderInfos[0].defaultMaterial = matTrimsheetConstructionLoaderAlt;
            NewRenderInfos[1].defaultMaterial = matLoaderPilotDiffuseAlt;
            NewRenderInfos[2].defaultMaterial = matLoaderPilotDiffuseAlt;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinLoaderDefault.meshReplacements.Length];
            skinLoaderDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[0].mesh = skinLoaderAlt.meshReplacements[0].mesh;
            //MeshReplacements[2].mesh = skinLoaderAlt.meshReplacements[2].mesh;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconLoader, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_LOADER", "Eco friendly");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_LOADER_NAME", "Loader: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_LOADER_DESCRIPTION", "As Loader, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_LOADER_NAME";
            unlockableDef.cachedName = "Skins.Loader.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinLoaderWolfo",
                NameToken = "SIMU_SKIN_LOADER",
                Icon = SkinIconS,
                BaseSkins = skinLoaderAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinLoaderAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/LoaderBody"), SkinInfo);
        }

        [RegisterAchievement("SIMU_SKIN_LOADER", "Skins.Loader.Wolfo", "DefeatSuperRoboBallBoss", null)]
        public class ClearSimulacrumTreebotBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("LoaderBody");
            }
        }
    }
}