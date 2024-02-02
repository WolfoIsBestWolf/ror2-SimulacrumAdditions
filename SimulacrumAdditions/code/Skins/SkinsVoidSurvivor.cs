using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsVoidFiend
    {
        internal static void VoidSkins()
        {
            SkinDef skinVoidSurvivorDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorDefault.asset").WaitForCompletion();
            SkinDef skinVoidSurvivorAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/VoidSurvivor/skinVoidSurvivorAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinVoidSurvivorDefault.rendererInfos.Length+1];
            System.Array.Copy(skinVoidSurvivorDefault.rendererInfos, NewRenderInfos, skinVoidSurvivorDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosIMP = new CharacterModel.RendererInfo[skinVoidSurvivorDefault.rendererInfos.Length+1];
            System.Array.Copy(skinVoidSurvivorDefault.rendererInfos, NewRenderInfosIMP, skinVoidSurvivorDefault.rendererInfos.Length);

            Material matVoidSurvivorFlesh = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[0].defaultMaterial);
            Material matVoidSurvivorHead = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[1].defaultMaterial);
            Material matVoidSurvivorMetal = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[2].defaultMaterial);

            Material matVoidSurvivorFleshIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[0].defaultMaterial);
            Material matVoidSurvivorHeadIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[1].defaultMaterial);
            Material matVoidSurvivorMetalIMP = Object.Instantiate(skinVoidSurvivorDefault.rendererInfos[2].defaultMaterial);
            
            Texture2D texVoidSurvivorFleshDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texVoidSurvivorFleshDiffuse.LoadImage(Properties.Resources.texVoidSurvivorFleshDiffuse, true);
            texVoidSurvivorFleshDiffuse.filterMode = FilterMode.Bilinear;
            texVoidSurvivorFleshDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshEmission = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texVoidSurvivorFleshEmission.LoadImage(Properties.Resources.texVoidSurvivorFleshEmission, true);
            texVoidSurvivorFleshEmission.filterMode = FilterMode.Bilinear;
            texVoidSurvivorFleshEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampNullifierOffset = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampNullifierOffset.LoadImage(Properties.Resources.texRampNullifierOffset, true);
            texRampNullifierOffset.filterMode = FilterMode.Bilinear;
            texRampNullifierOffset.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshDiffuseIMP = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texVoidSurvivorFleshDiffuseIMP.LoadImage(Properties.Resources.texVoidSurvivorFleshDiffuseIMP, true);
            texVoidSurvivorFleshDiffuseIMP.filterMode = FilterMode.Bilinear;
            texVoidSurvivorFleshDiffuseIMP.wrapMode = TextureWrapMode.Clamp;

            Texture2D texVoidSurvivorFleshEmissionIMP = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texVoidSurvivorFleshEmissionIMP.LoadImage(Properties.Resources.texVoidSurvivorFleshEmissionIMP, true);
            texVoidSurvivorFleshEmissionIMP.filterMode = FilterMode.Bilinear;
            texVoidSurvivorFleshEmissionIMP.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampNullifierOffsetIMP = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampNullifierOffsetIMP.LoadImage(Properties.Resources.texRampNullifierOffsetIMP, true);
            texRampNullifierOffsetIMP.filterMode = FilterMode.Bilinear;
            texRampNullifierOffsetIMP.wrapMode = TextureWrapMode.Clamp;


            matVoidSurvivorFlesh.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorFlesh.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorFlesh.SetColor("_EmColor", new Color(1,2,2));
            //matVoidSurvivorFlesh.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorHead.mainTexture = texVoidSurvivorFleshDiffuse;
            matVoidSurvivorHead.SetTexture("_EmTex", texVoidSurvivorFleshEmission);
            matVoidSurvivorHead.SetTexture("_FresnelRamp", texRampNullifierOffset);

            matVoidSurvivorMetal.color = new Color(0.65f, 0.7f, 0.7f);
            matVoidSurvivorMetal.SetTexture("_FresnelRamp", texRampNullifierOffset);
            //matVoidSurvivorMetal.SetTexture("_PrintRamp", texRampNullifierOffset);

            matVoidSurvivorFleshIMP.mainTexture = texVoidSurvivorFleshDiffuseIMP;
            matVoidSurvivorFleshIMP.SetTexture("_EmTex", texVoidSurvivorFleshEmissionIMP);
            matVoidSurvivorFleshIMP.SetColor("_EmColor", new Color(2, 1, 1));
            matVoidSurvivorFleshIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);

            matVoidSurvivorHeadIMP.mainTexture = texVoidSurvivorFleshDiffuseIMP;
            matVoidSurvivorHeadIMP.SetTexture("_EmTex", texVoidSurvivorFleshEmissionIMP);
            matVoidSurvivorHeadIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);

            matVoidSurvivorMetalIMP.color = new Color(0.4f, 0.0f, 0.0f);
            matVoidSurvivorMetalIMP.SetTexture("_FresnelRamp", texRampNullifierOffsetIMP);
            matVoidSurvivorMetalIMP.SetTexture("_PrintRamp", texRampNullifierOffsetIMP);

            NewRenderInfos[0].defaultMaterial = matVoidSurvivorFlesh;
            NewRenderInfos[1].defaultMaterial = matVoidSurvivorHead;
            NewRenderInfos[2].defaultMaterial = matVoidSurvivorMetal;
            NewRenderInfos[3] = new CharacterModel.RendererInfo
            {
                renderer = skinVoidSurvivorDefault.rendererInfos[0].renderer.transform.parent.GetChild(3).GetComponent<SkinnedMeshRenderer>(),
                defaultMaterial = matVoidSurvivorFlesh,
            };

            NewRenderInfosIMP[0].defaultMaterial = matVoidSurvivorFleshIMP;
            NewRenderInfosIMP[1].defaultMaterial = matVoidSurvivorHeadIMP;
            NewRenderInfosIMP[2].defaultMaterial = matVoidSurvivorMetalIMP;
            NewRenderInfosIMP[3] = new CharacterModel.RendererInfo
            {
                renderer = skinVoidSurvivorDefault.rendererInfos[0].renderer.transform.parent.GetChild(3).GetComponent<SkinnedMeshRenderer>(),
                defaultMaterial = matVoidSurvivorFleshIMP,
            };
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconVoidSurvivor, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);

            Texture2D SkinIconIMP = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIconIMP.LoadImage(Properties.Resources.skinIconVoidSurvivorIMP, true);
            SkinIconIMP.filterMode = FilterMode.Bilinear;
            Sprite SkinIconSIMP = Sprite.Create(SkinIconIMP, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_VOIDSURVIVOR", "Friend");
            LanguageAPI.Add("SIMU_SKIN_VOIDSURVIVOR2", "Foe"); //Impish
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_VOIDSURVIVOR_NAME", "V??oid Fiend: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_VOIDSURVIVOR_DESCRIPTION", "As V??oid Fiend, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_VOIDSURVIVOR_NAME";
            unlockableDef.cachedName = "Skins.VoidSurvivor.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinVoidSurvivorWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_VOIDSURVIVOR";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinVoidSurvivorAlt.baseSkins;
            newSkinDef.meshReplacements = skinVoidSurvivorDefault.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinVoidSurvivorDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            SkinDefWolfo newSkinDefIMP = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefIMP.name = "skinVoidSurvivorWolfo2";
            newSkinDefIMP.nameToken = "SIMU_SKIN_VOIDSURVIVOR2";
            newSkinDefIMP.icon = SkinIconSIMP;
            newSkinDefIMP.baseSkins = skinVoidSurvivorAlt.baseSkins;
            newSkinDefIMP.meshReplacements = skinVoidSurvivorDefault.meshReplacements;
            newSkinDefIMP.rendererInfos = NewRenderInfosIMP;
            newSkinDefIMP.rootObject = skinVoidSurvivorDefault.rootObject;
            newSkinDefIMP.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion(), newSkinDef);
            Skins.AddSkinToCharacter(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/VoidSurvivor/VoidSurvivorBody.prefab").WaitForCompletion(), newSkinDefIMP);
        }


        [RegisterAchievement("SIMU_SKIN_VOIDSURVIVOR", "Skins.VoidSurvivor.Wolfo", "CompleteVoidEnding", null)]
        public class ClearSimulacrumCrocoBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("VoidSurvivorBody");
            }
        }
    }
}