using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsCaptain
    {
        internal static void CaptainSkin()
        {
            //Pink stuff test
            SkinDef CaptainSkinDefault = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef CaptainSkinWhite = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            Texture2D PinktexCaptainJacketDiffuseW = new Texture2D(512, 512, TextureFormat.DXT1, false);
            PinktexCaptainJacketDiffuseW.LoadImage(Properties.Resources.PinktexCaptainJacketDiffuseW, true);
            PinktexCaptainJacketDiffuseW.filterMode = FilterMode.Bilinear;
            PinktexCaptainJacketDiffuseW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW.LoadImage(Properties.Resources.PinktexCaptainPaletteW, true);
            PinktexCaptainPaletteW.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW.wrapMode = TextureWrapMode.Clamp;

            Texture2D PinktexCaptainPaletteW2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW2.LoadImage(Properties.Resources.PinktexCaptainPaletteW2, true);
            PinktexCaptainPaletteW2.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW2.wrapMode = TextureWrapMode.Clamp;

            //Pallete for HAT
            Texture2D PinktexCaptainPaletteW3 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            PinktexCaptainPaletteW3.LoadImage(Properties.Resources.PinktexCaptainPaletteW3, true);
            PinktexCaptainPaletteW3.filterMode = FilterMode.Bilinear;
            PinktexCaptainPaletteW3.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainJacketDiffuseRED = new Texture2D(512, 512, TextureFormat.DXT1, false);
            texCaptainJacketDiffuseRED.LoadImage(Properties.Resources.texCaptainJacketDiffuseRED, true);
            texCaptainJacketDiffuseRED.filterMode = FilterMode.Bilinear;
            texCaptainJacketDiffuseRED.wrapMode = TextureWrapMode.Clamp;

            Texture2D texCaptainPaletteRED2 = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texCaptainPaletteRED2.LoadImage(Properties.Resources.texCaptainPaletteRED2, true);
            texCaptainPaletteRED2.filterMode = FilterMode.Bilinear;
            texCaptainPaletteRED2.wrapMode = TextureWrapMode.Clamp;

            CharacterModel.RendererInfo[] CaptainPinkRenderInfos = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainPinkRenderInfos, 7);

            CharacterModel.RendererInfo[] CaptainRenderInfosRED = new CharacterModel.RendererInfo[7];
            System.Array.Copy(CaptainSkinWhite.rendererInfos, CaptainRenderInfosRED, 7);

            Material PinkmatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt2 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material PinkmatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material PinkmatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material PinkmatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);

            Material RedMatCaptainAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainAlt2 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            //Material RedMatCaptainAlt3 = Object.Instantiate(CaptainSkinWhite.rendererInfos[0].defaultMaterial);
            Material RedMatCaptainArmorAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[2].defaultMaterial);
            Material RedMatCaptainJacketAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[3].defaultMaterial);
            Material RedMatCaptainRobotBitsAlt = Object.Instantiate(CaptainSkinWhite.rendererInfos[4].defaultMaterial);


            PinkmatCaptainAlt.mainTexture = PinktexCaptainPaletteW;
            PinkmatCaptainAlt2.mainTexture = PinktexCaptainPaletteW2;
            PinkmatCaptainAlt3.mainTexture = PinktexCaptainPaletteW3;
            //PinkmatCaptainArmorAlt.color = new Color32(255, 223, 188, 255);
            PinkmatCaptainArmorAlt.color = new Color32(255, 190, 135, 255);//(255, 195, 150, 255);
            //_EmColor is juts fucking weird
            PinkmatCaptainJacketAlt.mainTexture = PinktexCaptainJacketDiffuseW;
            PinkmatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.2f, 0.6f, 1.2f, 1f));
            PinkmatCaptainRobotBitsAlt.color = new Color32(255, 190, 135, 255);

            RedMatCaptainAlt.mainTexture = texCaptainPaletteRED2;
            RedMatCaptainAlt2.mainTexture = texCaptainPaletteRED2;
            //RedMatCaptainAlt3.mainTexture = texCaptainPaletteRED3;
            RedMatCaptainArmorAlt.color = new Color(0.5f, 0.5f, 0.5f);
            RedMatCaptainJacketAlt.mainTexture = texCaptainJacketDiffuseRED;

            RedMatCaptainAlt.color = new Color(0.8f, 0.8f, 0.8f);
            RedMatCaptainJacketAlt.color = new Color(0.8f, 0.7f, 0.7f);

            RedMatCaptainRobotBitsAlt.color = new Color(0.65f, 0.65f, 0.65f);
            RedMatCaptainRobotBitsAlt.SetColor("_EmColor", new Color(1.3f, 0.6f, 0.4f, 1f));    

            CaptainPinkRenderInfos[0].defaultMaterial = PinkmatCaptainAlt; //matCaptainAlt
            CaptainPinkRenderInfos[1].defaultMaterial = PinkmatCaptainAlt3; //matCaptainAlt //Hat
            CaptainPinkRenderInfos[2].defaultMaterial = PinkmatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainPinkRenderInfos[3].defaultMaterial = PinkmatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainPinkRenderInfos[4].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[5].defaultMaterial = PinkmatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainPinkRenderInfos[6].defaultMaterial = PinkmatCaptainAlt2; //matCaptainAlt //Skirt
            //
            //
            CaptainRenderInfosRED[0].defaultMaterial = RedMatCaptainAlt; //matCaptainAlt
            CaptainRenderInfosRED[1].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt
            CaptainRenderInfosRED[2].defaultMaterial = RedMatCaptainArmorAlt; //matCaptainArmorAlt
            CaptainRenderInfosRED[3].defaultMaterial = RedMatCaptainJacketAlt; //matCaptainJacketAlt
            CaptainRenderInfosRED[4].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfosRED[5].defaultMaterial = RedMatCaptainRobotBitsAlt; //matCaptainRobotBitsAlt
            CaptainRenderInfosRED[6].defaultMaterial = RedMatCaptainAlt2; //matCaptainAlt //Skirt
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CAPTAIN", "Honeymoon");
            LanguageAPI.Add("SIMU_SKIN_CAPTAIN2", "Occasion");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CAPTAIN_NAME", "Captain : Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CAPTAIN_DESCRIPTION", "As Captain, complete wave 50 in Simulacrum or escape the Planetarium.");
            //
            Texture2D texCaptainPinkSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texCaptainPinkSkinIcon.LoadImage(Properties.Resources.texCaptainPinkSkinIcon, true);
            texCaptainPinkSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texCaptainPinkSkinIconS = Sprite.Create(texCaptainPinkSkinIcon, WRect.rec128, WRect.half);

            Texture2D skinIconCaptainRED = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCaptainRED.LoadImage(Properties.Resources.skinIconCaptainRED, true);
            skinIconCaptainRED.filterMode = FilterMode.Bilinear;
            Sprite skinIconCaptainREDS = Sprite.Create(skinIconCaptainRED, WRect.rec128, WRect.half);

            //
            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CAPTAIN_NAME";
            unlockableDef.cachedName = "Skins.Captain.Wolfo";
            unlockableDef.achievementIcon = texCaptainPinkSkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            R2API.SkinDefInfo CaptainPinkSkinInfos = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN",
                UnlockableDef = unlockableDef,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainPinkRenderInfos,
                Name = "skinCaptainWolfo",
                Icon = texCaptainPinkSkinIconS,
            };
            //
            R2API.SkinDefInfo SkinInfo2 = new R2API.SkinDefInfo
            {
                BaseSkins = CaptainSkinWhite.baseSkins,
                NameToken = "SIMU_SKIN_CAPTAIN2",
                UnlockableDef = unlockableDef,
                RootObject = CaptainSkinWhite.rootObject,
                RendererInfos = CaptainRenderInfosRED,
                Name = "skinCaptainWolfo",
                Icon = skinIconCaptainREDS,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), CaptainPinkSkinInfos);
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CaptainBody"), SkinInfo2);
        }

        [RegisterAchievement("SIMU_SKIN_Captain", "Skins.Captain.Wolfo", "CompleteMainEnding", null)]
        public class Bandit2ClearGameMonsoonAchievement : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CaptainBody");
            }
        }
    }
}