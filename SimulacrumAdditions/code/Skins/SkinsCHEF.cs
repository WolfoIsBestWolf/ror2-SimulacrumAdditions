using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsCHEF
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinChefIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //Probably has to be added during awake

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_NAME", "CHEF: Alternated");

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_CHEF_NAME";
            unlockableDef.cachedName = "Skins.Chef.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
        }

        internal static void CHEFSkin(GameObject ChefBody)
        {
            Debug.Log("CHEF Skins");
            unlockableDef.hidden = false;

            BodyIndex ChefIndex = ChefBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = ChefBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinChef = modelSkinController.skins[0];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfos, skinChef.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRED = new CharacterModel.RendererInfo[skinChef.rendererInfos.Length];
            System.Array.Copy(skinChef.rendererInfos, NewRenderInfosRED, skinChef.rendererInfos.Length);

            Material matChef = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);
            Material matChefRED = Object.Instantiate(skinChef.rendererInfos[0].defaultMaterial);

            Texture2D texChefDefault = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefDefault.LoadImage(Properties.Resources.texChefDefault, true);
            texChefDefault.filterMode = FilterMode.Bilinear;
            texChefDefault.wrapMode = TextureWrapMode.Clamp;

            Texture2D texChefRed = new Texture2D(256, 128, TextureFormat.DXT5, false);
            texChefRed.LoadImage(Properties.Resources.texChefRed, true);
            texChefRed.filterMode = FilterMode.Bilinear;
            texChefRed.wrapMode = TextureWrapMode.Clamp;

            matChef.mainTexture = texChefDefault;
            matChefRED.mainTexture = texChefRed;

            NewRenderInfos[0].defaultMaterial = matChef;
            NewRenderInfosRED[0].defaultMaterial = matChefRED;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinChefIcon, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            Texture2D skinChefIconRed = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinChefIconRed.LoadImage(Properties.Resources.skinChefIconRed, true);
            skinChefIconRed.filterMode = FilterMode.Bilinear;
            Sprite skinChefIconRedS = Sprite.Create(skinChefIconRed, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_CHEF", "Flambe");
            LanguageAPI.Add("SIMU_SKIN_CHEF2", "Rouge");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_CHEF_DESCRIPTION", "As CHEF, complete wave 50 in Simulacrum or escape the Planetarium.");

            R2API.SkinDefInfo SkinInfo = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo",
                NameToken = "SIMU_SKIN_CHEF",
                Icon = SkinIconS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfos,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.SkinDefInfo SkinInfoRED = new R2API.SkinDefInfo
            {
                Name = "skinCHEFWolfo2",
                NameToken = "SIMU_SKIN_CHEF2",
                Icon = skinChefIconRedS,
                BaseSkins = new SkinDef[] { skinChef },
                RendererInfos = NewRenderInfosRED,
                RootObject = skinChef.rootObject,
                UnlockableDef = unlockableDef,
            };
            SkinDef ChefSkinDefNew = Skins.CreateNewSkinDef(SkinInfo);
            SkinDef ChefSkinDefRED= Skins.CreateNewSkinDef(SkinInfoRED);

            modelSkinController.skins = modelSkinController.skins.Add(ChefSkinDefRED, ChefSkinDefNew);
            BodyCatalog.skins[(int)ChefIndex] = BodyCatalog.skins[(int)ChefIndex].Add(ChefSkinDefRED, ChefSkinDefNew);
        }



        [RegisterAchievement("SIMU_SKIN_CHEF", "Skins.Chef.Wolfo", null, null)]
        public class ClearSimulacrumCHEF : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CHEF");
            }
        }
    }
}