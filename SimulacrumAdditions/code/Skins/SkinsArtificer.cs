using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsArtificer
    {
        internal static void ArtificerSkin()
        {
            //RoRR Orange Artificer
            SkinDef skinMageDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageDefault.asset").WaitForCompletion();
            SkinDef skinMageAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Mage/skinMageAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[9];
            System.Array.Copy(skinMageAlt.rendererInfos, NewRenderInfos, 4);

            CharacterModel.RendererInfo[] NewRenderInfosORANGE = new CharacterModel.RendererInfo[4];
            System.Array.Copy(skinMageAlt.rendererInfos, NewRenderInfosORANGE, 4);
            //like 5 more jet renders for blue ig

            Texture2D texMageDiffuseAlt = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseAlt.LoadImage(Properties.Resources.texMageDiffuseAlt, true);
            texMageDiffuseAlt.filterMode = FilterMode.Bilinear;
            texMageDiffuseAlt.wrapMode = TextureWrapMode.Clamp;

            Texture2D texMageDiffuseORANGE = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texMageDiffuseORANGE.LoadImage(Properties.Resources.texMageDiffuseORANGE, true);
            texMageDiffuseORANGE.filterMode = FilterMode.Bilinear;
            texMageDiffuseORANGE.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampMageFire = new Texture2D(256, 16, TextureFormat.DXT1, false);
            texRampMageFire.LoadImage(Properties.Resources.texRampMageFire, true);
            texRampMageFire.filterMode = FilterMode.Point;
            texRampMageFire.wrapMode = TextureWrapMode.Clamp;


            Material mageMageFireStarburst = Object.Instantiate(skinMageAlt.rendererInfos[0].defaultMaterial);
            Material matMage = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);
            Material matMageORANGE = Object.Instantiate(skinMageAlt.rendererInfos[3].defaultMaterial);
            Material matMageFlamethrower = Object.Instantiate(NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>().material);


            matMage.mainTexture = texMageDiffuseAlt;
            matMageORANGE.mainTexture = texMageDiffuseORANGE;

            //mageMageFireStarburst.SetTexture("_RemapTex", Addressables.LoadAssetAsync<Texture2D>(key: "RoR2/Base/Common/ColorRamps/texRampLightning2.png").WaitForCompletion());
            mageMageFireStarburst.SetTexture("_RemapTex", texRampMageFire);
            matMageFlamethrower.SetTexture("_RemapTex", texRampMageFire);
            //mageMageFireStarburst.SetColor("_CutoffScroll", new Color(-30, 0, 50, 0));

            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.color = new Color(0.66f, 0.66f, 0.66f, 1);
            //matMage.SetColor("_EmColor", new Color(0.4f, 0.2f, 0));

            NewRenderInfos[0].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst //Jet
            NewRenderInfos[1].defaultMaterial = mageMageFireStarburst;     //mageMageFireStarburst
            NewRenderInfos[2].defaultMaterial = matMage;          //matMage //Cape
            NewRenderInfos[3].defaultMaterial = matMage;          //matMage //Mage

            NewRenderInfosORANGE[2].defaultMaterial = matMageORANGE;          //matMage //Cape
            NewRenderInfosORANGE[3].defaultMaterial = matMageORANGE;          //matMage //Mage

            NewRenderInfos[4] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(0).GetComponent<ParticleSystemRenderer>(), //matMageFlamethrower 
                defaultMaterial = matMageFlamethrower,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            //Light
            NewRenderInfos[5] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(2).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[6] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(3).GetComponent<MeshRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };
            NewRenderInfos[7] = new CharacterModel.RendererInfo
            {
                renderer = NewRenderInfos[0].renderer.transform.parent.GetChild(5).GetChild(4).GetComponent<ParticleSystemRenderer>(), //mageMageFireStarburst  
                defaultMaterial = mageMageFireStarburst,
                ignoreOverlays = true,
                defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On
            };

            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[2];
            skinMageAlt.meshReplacements.CopyTo(MeshReplacements, 0);
            MeshReplacements[1].mesh = skinMageDefault.meshReplacements[1].mesh;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconMage, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);

            Texture2D SkinIcon2 = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon2.LoadImage(Properties.Resources.skinIconMageOrange, true);
            SkinIcon2.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS2 = Sprite.Create(SkinIcon2, WRect.rec128, WRect.half);

            //Sprite SkinIconNew = Skins.CreateSkinIcon(new Color(0.95f,0.55f,0.55f), new Color(0.95f,0.95f,0.55f), new Color(0.55f,0.55f,0.95f), new Color(0.5f,0.9f,0.85f));
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_MAGE", "Pastel Rainbow");
            LanguageAPI.Add("SIMU_SKIN_MAGE2", "Fire Fighting");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MAGE_NAME", "Artificer : Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_MAGE_DESCRIPTION", "As Artificer, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_MAGE_NAME";
            unlockableDef.cachedName = "Skins.Mage.Wolfo";
            unlockableDef.achievementIcon = SkinIconS2;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinMageWolfoRainbow",
                NameToken = "SIMU_SKIN_MAGE",
                Icon = SkinIconS,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfos,
                RootObject = skinMageAlt.rootObject,
                UnlockableDef = unlockableDef,
            };

            R2API.SkinDefInfo SkinInfo2 = new R2API.SkinDefInfo
            {
                Name = "skinMageWolfo",
                NameToken = "SIMU_SKIN_MAGE2",
                Icon = SkinIconS2,
                BaseSkins = skinMageAlt.baseSkins,
                MeshReplacements = MeshReplacements,
                RendererInfos = NewRenderInfosORANGE,
                RootObject = skinMageAlt.rootObject,
                UnlockableDef = unlockableDef,
            };
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo2);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/MageBody"), SkinInfo);
        }

        [RegisterAchievement("SIMU_SKIN_MAGE", "Skins.Mage.Wolfo", "FreeMage", null)]
        public class ClearSimulacrumMageBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("MageBody");
            }
        }
    }
}