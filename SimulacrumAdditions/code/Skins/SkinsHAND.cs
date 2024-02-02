using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsHand
    {
        private static UnlockableDef unlockableDef;

        internal static void CallDuringAwake()
        {
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.texHANDSkinIconRorr, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);

            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HAND_NAME", "HAN-D: Alternated");

            unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_HandBody_NAME";
            unlockableDef.cachedName = "Skins.Hand.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            unlockableDef.hidden = true;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
        }

        internal static void HandSkin(GameObject HandBody)
        {
            Debug.Log("HandBody Skins");
            unlockableDef.hidden = false;
            BodyIndex HandBodyIndex = HandBody.GetComponent<CharacterBody>().bodyIndex;
            ModelSkinController modelSkinController = HandBody.transform.GetChild(0).GetChild(0).GetComponent<ModelSkinController>();
            SkinDef skinHandDefault = modelSkinController.skins[0];
            SkinDef skinHandReturns = modelSkinController.skins[2];

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHandDefault.rendererInfos.Length];
            System.Array.Copy(skinHandDefault.rendererInfos, NewRenderInfos, skinHandDefault.rendererInfos.Length);

            CharacterModel.RendererInfo[] NewRenderInfosRETURNS = new CharacterModel.RendererInfo[skinHandReturns.rendererInfos.Length];
            System.Array.Copy(skinHandReturns.rendererInfos, NewRenderInfosRETURNS, skinHandReturns.rendererInfos.Length);


            //0 matHANDRorr
            //1 matHANDRorr
            //2 matHANDRorr
            //3 matDroneSawTemp

            Material matHANDDefault = Object.Instantiate(skinHandDefault.rendererInfos[0].defaultMaterial);
            Material matHANDWeaponDefault = Object.Instantiate(skinHandDefault.rendererInfos[1].defaultMaterial);
            Material matDroneBody = Object.Instantiate(skinHandDefault.rendererInfos[2].defaultMaterial);

            Material matHandBodyRETURNS = Object.Instantiate(skinHandReturns.rendererInfos[0].defaultMaterial);

            Texture2D HanD_Diffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            HanD_Diffuse.LoadImage(Properties.Resources.HanD_Diffuse, true);
            HanD_Diffuse.filterMode = FilterMode.Bilinear;
            HanD_Diffuse.wrapMode = TextureWrapMode.Repeat;

            Texture2D HanD_Emission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            HanD_Emission.LoadImage(Properties.Resources.HanD_Emission, true);
            HanD_Emission.filterMode = FilterMode.Bilinear;
            HanD_Emission.wrapMode = TextureWrapMode.Clamp;

            Texture2D HanDWeapon_Diffuse = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            HanDWeapon_Diffuse.LoadImage(Properties.Resources.HanDWeapon_Diffuse, true);
            HanDWeapon_Diffuse.filterMode = FilterMode.Bilinear;
            HanDWeapon_Diffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texDroneBody = new Texture2D(4, 4, TextureFormat.RGB24, false);
            texDroneBody.LoadImage(Properties.Resources.texDroneBody, true);
            texDroneBody.filterMode = FilterMode.Point;
            texDroneBody.wrapMode = TextureWrapMode.Repeat;

            Texture2D texDroneBodyEmission = new Texture2D(4, 4, TextureFormat.RGB24, false);
            texDroneBodyEmission.LoadImage(Properties.Resources.texDroneBodyEmission, true);
            texDroneBodyEmission.filterMode = FilterMode.Point;
            texDroneBodyEmission.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTex = new Texture2D(64, 64, TextureFormat.DXT5, false);
            RoRRHANDTex.LoadImage(Properties.Resources.RoRRHANDTex, true);
            RoRRHANDTex.filterMode = FilterMode.Bilinear;
            RoRRHANDTex.wrapMode = TextureWrapMode.Repeat;

            Texture2D RoRRHANDTexEmission = new Texture2D(64, 64, TextureFormat.DXT5, false);
            RoRRHANDTexEmission.LoadImage(Properties.Resources.RoRRHANDTexEmission, true);
            RoRRHANDTexEmission.filterMode = FilterMode.Bilinear;
            RoRRHANDTexEmission.wrapMode = TextureWrapMode.Repeat;


            matHANDDefault.mainTexture = HanD_Diffuse;
            matHANDDefault.SetTexture("_EmTex", HanD_Emission);
            matHANDDefault.SetColor("_EmColor", new Color(1,1,1.6f));
            matHANDWeaponDefault.mainTexture = HanDWeapon_Diffuse;
            matDroneBody.mainTexture = texDroneBody;
            matDroneBody.SetTexture("_EmTex", texDroneBodyEmission);
            matDroneBody.SetColor("_EmColor", new Color(1, 1, 1.6f));

            matHandBodyRETURNS.mainTexture = RoRRHANDTex;
            matHandBodyRETURNS.SetTexture("_EmTex", RoRRHANDTexEmission);
            matHandBodyRETURNS.SetColor("_EmColor", new Color(1, 1, 1));

            NewRenderInfos[0].defaultMaterial = matHANDDefault;
            NewRenderInfos[1].defaultMaterial = matHANDWeaponDefault;
            NewRenderInfos[2].defaultMaterial = matDroneBody;
            NewRenderInfosRETURNS[0].defaultMaterial = matHandBodyRETURNS;
            NewRenderInfosRETURNS[1].defaultMaterial = matHandBodyRETURNS;
            NewRenderInfosRETURNS[2].defaultMaterial = matHandBodyRETURNS;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(256, 256, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.texHANDSkinIconDefault, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec256, WRect.half);
            //
            Texture2D skinHandBodyIconRETURNS = new Texture2D(256, 256, TextureFormat.DXT5, false);
            skinHandBodyIconRETURNS.LoadImage(Properties.Resources.texHANDSkinIconRorr, true);
            skinHandBodyIconRETURNS.filterMode = FilterMode.Bilinear;
            Sprite skinHandBodyIconRETURNSS = Sprite.Create(skinHandBodyIconRETURNS, WRect.rec256, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_HAND", "Golden");
            LanguageAPI.Add("SIMU_SKIN_HAND2", "RE-TRN Golden");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HAND_DESCRIPTION", "As HAN-D, complete wave 50 in Simulacrum or escape the Planetarium.");

            //ADD SHINY SPARKLES
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHandBodyWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_HAND";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinHandReturns.baseSkins;
            newSkinDef.meshReplacements = skinHandDefault.meshReplacements;
            newSkinDef.projectileGhostReplacements = skinHandDefault.projectileGhostReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHandDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            GameObject GoldAffixEffect = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Junk/EliteGold/GoldAffixEffect.prefab").WaitForCompletion();
            GameObject GoldAffixEffectNew = R2API.PrefabAPI.InstantiateClone(GoldAffixEffect, "HANDGoldSparkles", false);

            GoldAffixEffectNew.transform.GetChild(0).gameObject.SetActive(false);
            GoldAffixEffectNew.transform.GetChild(0).localPosition = new Vector3(0, 4, 0);
            GoldAffixEffectNew.transform.GetChild(0).GetComponent<Light>().intensity = 1;

            ParticleSystem particleSystem = GoldAffixEffectNew.GetComponent<ParticleSystem>();
            particleSystem.playbackSpeed = 0.5f;
            particleSystem.emissionRate = 6;
            particleSystem.simulationSpace = ParticleSystemSimulationSpace.World;
            particleSystem.startColor = new Color(0f, 0.6f, 1);

            var Main = particleSystem.main;
            Main.cullingMode = ParticleSystemCullingMode.AlwaysSimulate;

            var Shape = particleSystem.shape;
            Shape.shapeType = ParticleSystemShapeType.Sphere;
            Shape.scale = new Vector3(1.8f, 2.4f, 1.5f);

            newSkinDef.addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = GoldAffixEffectNew,
                            childName = "Base",
                            localPos = new Vector3(0f, 4.6f, 0f),
                            localAngles = new Vector3(0f,0f,0f),
                            localScale = new Vector3(0.6f,0.6f,0.6f),
                            limbMask = LimbFlags.None
                        },
            };



            SkinDefWolfo newSkinDefRETURNS = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDefRETURNS.name = "skinHandBodyWolfo2";
            newSkinDefRETURNS.nameToken = "SIMU_SKIN_HAND2";
            newSkinDefRETURNS.icon = skinHandBodyIconRETURNSS;
            newSkinDefRETURNS.baseSkins = skinHandReturns.baseSkins;
            newSkinDefRETURNS.meshReplacements = skinHandReturns.meshReplacements;
            newSkinDefRETURNS.projectileGhostReplacements = skinHandReturns.projectileGhostReplacements;
            newSkinDefRETURNS.rendererInfos = NewRenderInfosRETURNS;
            newSkinDefRETURNS.rootObject = skinHandDefault.rootObject;
            newSkinDefRETURNS.unlockableDef = unlockableDef;
            newSkinDefRETURNS.addGameObjects = newSkinDef.addGameObjects;

            modelSkinController.skins = modelSkinController.skins.Add(newSkinDef, newSkinDefRETURNS);
            BodyCatalog.skins[(int)HandBodyIndex] = BodyCatalog.skins[(int)HandBodyIndex].Add(newSkinDef, newSkinDefRETURNS);
        }


        [RegisterAchievement("SIMU_SKIN_HAND", "Skins.Hand.Wolfo", null, null)]
        public class ClearSimulacrumHANDOverclockedBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HANDOverclockedBody");
            }
        }
    }
}