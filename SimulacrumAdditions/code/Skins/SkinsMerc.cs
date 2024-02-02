using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsMerc
    {
        internal static void MercSkin()
        {
            SkinDef skinMercDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercDefault.asset").WaitForCompletion();
            SkinDef skinMercAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Merc/skinMercAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinMercDefault.rendererInfos.Length];
            System.Array.Copy(skinMercDefault.rendererInfos, NewRenderInfos, skinMercDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRED = new CharacterModel.RendererInfo[skinMercDefault.rendererInfos.Length];
            System.Array.Copy(skinMercDefault.rendererInfos, NewRenderInfosRED, skinMercDefault.rendererInfos.Length);

            Material matMerc = Object.Instantiate(skinMercDefault.rendererInfos[0].defaultMaterial);
            Material matMercRED = Object.Instantiate(skinMercDefault.rendererInfos[0].defaultMaterial);

            Material matMercSword = Object.Instantiate(skinMercDefault.rendererInfos[1].defaultMaterial);
            Material matMercSwordRED = Object.Instantiate(skinMercDefault.rendererInfos[1].defaultMaterial);

            Texture2D texMercDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texMercDiffuse.LoadImage(Properties.Resources.texMercDiffuse, true);
            texMercDiffuse.filterMode = FilterMode.Bilinear;
            texMercDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercDiffuseRed = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texMercDiffuseRed.LoadImage(Properties.Resources.texMercDiffuseRed, true);
            texMercDiffuseRed.filterMode = FilterMode.Bilinear;
            texMercDiffuseRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercEmission = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texMercEmission.LoadImage(Properties.Resources.texMercEmission, true);
            texMercEmission.filterMode = FilterMode.Bilinear;
            texMercEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercEmissionRED = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texMercEmissionRED.LoadImage(Properties.Resources.texMercEmissionRED, true);
            texMercEmissionRED.filterMode = FilterMode.Bilinear;
            texMercEmissionRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMercSwordDiffuse = new Texture2D(256, 512, TextureFormat.DXT1, false);
            texMercSwordDiffuse.LoadImage(Properties.Resources.texMercSwordDiffuse, true);
            texMercSwordDiffuse.filterMode = FilterMode.Bilinear;
            texMercSwordDiffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D texMercSwordDiffuseRed = new Texture2D(256, 512, TextureFormat.DXT1, false);
            texMercSwordDiffuseRed.LoadImage(Properties.Resources.texMercSwordDiffuseRed, true);
            texMercSwordDiffuseRed.filterMode = FilterMode.Bilinear;
            texMercSwordDiffuseRed.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampFallbootsRed = new Texture2D(16, 256, TextureFormat.DXT1, false);
            texRampFallbootsRed.LoadImage(Properties.Resources.texRampFallbootsRed, true);
            texRampFallbootsRed.filterMode = FilterMode.Bilinear;
            texRampFallbootsRed.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressRed = new Texture2D(16, 256, TextureFormat.DXT1, false);
            texRampHuntressRed.LoadImage(Properties.Resources.texRampHuntressRed, true);
            texRampHuntressRed.filterMode = FilterMode.Point;
            texRampHuntressRed.wrapMode = TextureWrapMode.Clamp;


            matMerc.mainTexture = texMercDiffuse;
            matMerc.SetTexture("_EmTex", texMercEmission);
            matMerc.SetColor("_EmColor", new Color(0,0.8f,1));

            matMercRED.mainTexture = texMercDiffuseRed;
            matMercRED.SetTexture("_EmTex", texMercEmissionRED);
            matMercRED.SetColor("_EmColor", new Color(1, 0, 0));

            matMercSword.mainTexture = texMercSwordDiffuse;
            matMercSword.SetColor("_EmColor", new Color(0, 0.5f, 1));

            matMercSwordRED.mainTexture = texMercSwordDiffuseRed;
            matMercSwordRED.SetColor("_EmColor", new Color(1, 0, 0));
            matMercSwordRED.SetTexture("_FlowHeightRamp", texRampFallbootsRed);
            matMercSwordRED.SetTexture("_FresnelRamp", texRampHuntressRed);

            NewRenderInfos[0].defaultMaterial = matMerc;
            NewRenderInfos[1].defaultMaterial = matMercSword;

            NewRenderInfosRED[0].defaultMaterial = matMercRED;
            NewRenderInfosRED[1].defaultMaterial = matMercSwordRED;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconMerc, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);

            Texture2D SkinIcon2 = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon2.LoadImage(Properties.Resources.skinIconMercRed, true);
            SkinIcon2.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS2 = Sprite.Create(SkinIcon2, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_MERC", "Guard");
            LanguageAPI.Add("SIMU_SKIN_MERC2", "Guard Red");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MERC_NAME", "Mercenary: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MERC_DESCRIPTION", "As Mercenary, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_MERC_NAME";
            unlockableDef.cachedName = "Skins.Merc.Wolfo";
            unlockableDef.achievementIcon = SkinIconS2;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinMercWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_MERC";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinMercAlt.baseSkins;
            newSkinDef.meshReplacements = skinMercDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinMercDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;
            //newSkinDef.lightColorsChanges = null;

            SkinDefWolfo newSkinDefRED = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRED.name = "skinMercWolfoRed";
            newSkinDefRED.nameToken = "SIMU_SKIN_MERC2";
            newSkinDefRED.icon = SkinIconS2;
            newSkinDefRED.baseSkins = skinMercAlt.baseSkins;
            newSkinDefRED.meshReplacements = skinMercDefault.meshReplacements;
            newSkinDefRED.rendererInfos = NewRenderInfosRED;
            newSkinDefRED.rootObject = skinMercDefault.rootObject;
            newSkinDefRED.unlockableDef = unlockableDef;
            newSkinDefRED.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light (1)",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(1,0,0),
                    lightPath = "MercArmature/ROOT/base/stomach/chest/SwingCenter/SwordBase/Point Light",
                }
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MercBody"), newSkinDef);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MercBody"), newSkinDefRED);
        }


        [RegisterAchievement("SIMU_SKIN_MERC", "Skins.Merc.Wolfo", "CompleteUnknownEnding", null)]
        public class ClearSimulacrumMercBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MercBody");
            }
        }
    }
}