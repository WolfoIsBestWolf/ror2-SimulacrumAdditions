using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsMULT
    {
        internal static void ToolbotSkin()
        {
            SkinDef skinToolbotDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotDefault.asset").WaitForCompletion();
            SkinDef skinToolbotAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Toolbot/skinToolbotAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinToolbotDefault.rendererInfos.Length];
            System.Array.Copy(skinToolbotDefault.rendererInfos, NewRenderInfos, skinToolbotDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfos2 = new CharacterModel.RendererInfo[skinToolbotDefault.rendererInfos.Length];
            System.Array.Copy(skinToolbotDefault.rendererInfos, NewRenderInfos2, skinToolbotDefault.rendererInfos.Length);

            Material matToolbot = Object.Instantiate(skinToolbotDefault.rendererInfos[1].defaultMaterial);
            Material matToolbot2 = Object.Instantiate(skinToolbotDefault.rendererInfos[1].defaultMaterial);

            Texture2D texToolbotNew = new Texture2D(256, 512, TextureFormat.DXT1, false);
            texToolbotNew.LoadImage(Properties.Resources.texToolbotNew, true);
            texToolbotNew.filterMode = FilterMode.Bilinear;
            texToolbotNew.wrapMode = TextureWrapMode.Clamp;

            Texture2D texToolbotNew2 = new Texture2D(256, 512, TextureFormat.DXT1, false);
            texToolbotNew2.LoadImage(Properties.Resources.texToolbotNew2, true);
            texToolbotNew2.filterMode = FilterMode.Bilinear;
            texToolbotNew2.wrapMode = TextureWrapMode.Clamp;

            matToolbot.mainTexture = texToolbotNew;
            matToolbot2.mainTexture = texToolbotNew2;

            //NewRenderInfos[0].defaultMaterial = ;     //MatRebar 
            NewRenderInfos[1].defaultMaterial = matToolbot;     //MatToolbot
            NewRenderInfos2[1].defaultMaterial = matToolbot2;     //MatToolbot

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[skinToolbotDefault.meshReplacements.Length];
            skinToolbotDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconToolbot, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            Texture2D SkinIcon2 = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon2.LoadImage(Properties.Resources.skinIconToolbot2, true);
            SkinIcon2.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS2 = Sprite.Create(SkinIcon2, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_TOOLBOT", "Damage");
            LanguageAPI.Add("SIMU_SKIN_TOOLBOT2", "Healing");
            LanguageAPI.Add("SIMU_SKIN_TOOLBOT3", "Utility");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TOOLBOT_NAME", "MUL-T: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_TOOLBOT_DESCRIPTION", "As MUL-T, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_TOOLBOT_NAME";
            unlockableDef.cachedName = "Skins.Toolbot.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinToolbotWolfo",
                NameToken = "SIMU_SKIN_TOOLBOT",
                Icon = SkinIconS,
                BaseSkins = skinToolbotAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinToolbotAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ToolbotBody"), SkinInfo);
            //
            R2API.SkinDefInfo SkinInfo2 = new R2API.SkinDefInfo
            {
                Name = "skinToolbotWolfo2",
                NameToken = "SIMU_SKIN_TOOLBOT2",
                Icon = SkinIconS2,
                BaseSkins = skinToolbotAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos2,
                RootObject = skinToolbotAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/ToolbotBody"), SkinInfo2);
        }


        [RegisterAchievement("SIMU_SKIN_TOOLBOT", "Skins.Toolbot.Wolfo", "RepeatFirstTeleporter", null)]
        public class ClearSimulacrumToolbotBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("ToolbotBody");
            }
        }
    }
}