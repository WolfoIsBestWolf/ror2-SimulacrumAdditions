using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsHuntress
    {
        internal static void HuntressSkin()
        {
            SkinDef skinHuntressDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressDefault.asset").WaitForCompletion();
            SkinDef skinHuntressAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Huntress/skinHuntressAlt.asset").WaitForCompletion();

            //
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinHuntressDefault.rendererInfos.Length];
            System.Array.Copy(skinHuntressDefault.rendererInfos, NewRenderInfos, skinHuntressDefault.rendererInfos.Length);

            //0 matBowString BowString
            //1 matHuntress Bow
            //2 matHuntressArrow 
            //3 matHuntressArrow 
            //4 matHuntressCharged Particle 
            //5 matHuntressArrow 
            //6 matHuntressArrow 
            //7 matHuntressArrow 
            //8 matHuntressArrow 
            //8 matHuntressArrow 
            //9 matHuntressArrow 
            //10 matHuntress 
            //11 matHuntressCape 

            Material matBowString = Object.Instantiate(skinHuntressDefault.rendererInfos[0].defaultMaterial);
            Material matHuntress = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressBow = Object.Instantiate(skinHuntressDefault.rendererInfos[1].defaultMaterial);
            Material matHuntressArrow = Object.Instantiate(skinHuntressDefault.rendererInfos[2].defaultMaterial);
            Material matHuntressCharged = Object.Instantiate(skinHuntressDefault.rendererInfos[4].defaultMaterial);
            Material matHuntressCape = Object.Instantiate(skinHuntressDefault.rendererInfos[11].defaultMaterial);

            Texture2D texHuntressDiffuse = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuse.LoadImage(Properties.Resources.texHuntressDiffuse, true);
            texHuntressDiffuse.filterMode = FilterMode.Bilinear;
            texHuntressDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressDiffuseBow = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texHuntressDiffuseBow.LoadImage(Properties.Resources.texHuntressDiffuseBow, true);
            texHuntressDiffuseBow.filterMode = FilterMode.Bilinear;
            texHuntressDiffuseBow.wrapMode = TextureWrapMode.Clamp;

            Texture2D texHuntressEmission = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texHuntressEmission.LoadImage(Properties.Resources.texHuntressEmission, true);
            texHuntressEmission.filterMode = FilterMode.Bilinear;
            texHuntressEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntress = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntress.LoadImage(Properties.Resources.texRampHuntress, true);
            texRampHuntress.filterMode = FilterMode.Point;
            texRampHuntress.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft.LoadImage(Properties.Resources.texRampHuntressSoft, true);
            texRampHuntressSoft.filterMode = FilterMode.Point;
            texRampHuntressSoft.wrapMode = TextureWrapMode.Clamp;

            Texture2D texRampHuntressSoft2 = new Texture2D(256, 16, TextureFormat.DXT5, false);
            texRampHuntressSoft2.LoadImage(Properties.Resources.texRampHuntressSoft2, true);
            texRampHuntressSoft2.filterMode = FilterMode.Point;
            texRampHuntressSoft2.wrapMode = TextureWrapMode.Clamp;

            matHuntress.mainTexture = texHuntressDiffuse;
            matHuntress.SetTexture("_EmTex", texHuntressEmission);
            matHuntress.SetColor("_EmColor", new Color(2f,1f,2f));

            matHuntressBow.mainTexture = texHuntressDiffuseBow;
            matHuntressBow.SetTexture("_EmTex", texHuntressEmission);
            matHuntressBow.SetColor("_EmColor", new Color(1f, 0.5f, 1f));
            //matHuntressBow.color = new Color(0.5f, 0.5f, 0.5f);

            //matHuntressCape.color = new Color(0.25f, 0.2f, 0.3f);
            //matHuntressCape.color = new Color(1f, 0.5f, 0.8f);
            matHuntressCape.color = new Color(0.8f, 0.4f, 0.6f);

            matBowString.SetTexture("_RemapTex", texRampHuntressSoft);
            matHuntressArrow.SetTexture("_RemapTex", texRampHuntressSoft2);
            matHuntressCharged.SetTexture("_RemapTex", texRampHuntress);

            NewRenderInfos[0].defaultMaterial = matBowString;     // 
            NewRenderInfos[1].defaultMaterial = matHuntressBow;     //
            NewRenderInfos[2].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[3].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[4].defaultMaterial = matHuntressCharged;     //
            NewRenderInfos[5].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[6].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[7].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[8].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[9].defaultMaterial = matHuntressArrow;     //
            NewRenderInfos[10].defaultMaterial = matHuntress;
            NewRenderInfos[11].defaultMaterial = matHuntressCape;
            //


            GameObject HuntressArrowRain = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Huntress/HuntressArrowRain.prefab").WaitForCompletion();
            GameObject HuntressArrowRainWolfo1 = PrefabAPI.InstantiateClone(HuntressArrowRain, "HuntressArrowRainWolfo1", false);

            Material matHuntressAreaIndicatorActive = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
            Material matHuntressFlash = Object.Instantiate(HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material);

            matHuntressAreaIndicatorActive.SetTexture("_RemapTex", texRampHuntress);
            matHuntressFlash.SetTexture("_RemapTex", texRampHuntress);

            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matHuntressAreaIndicatorActive ;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(1).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow; 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(2).GetComponent<ParticleSystemRenderer>().material = matHuntressCharged;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);//HITBOX 
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(4).gameObject.SetActive(false); //HITBOX
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(5).GetComponent<ParticleSystemRenderer>().material = matHuntressArrow;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetComponent<ParticleSystemRenderer>().material = matHuntressFlash;
            HuntressArrowRainWolfo1.transform.GetChild(0).GetChild(6).GetChild(0).GetComponent<Light>().color = new Color(0.8f,0.2f,0.8f);
             
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconHuntress, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_HUNTRESS", "Amor");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HUNTRESS_NAME", "Huntress: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_HUNTRESS_DESCRIPTION", "As Huntress, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_HUNTRESS_NAME";
            unlockableDef.cachedName = "Skins.Huntress.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinHuntressWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_HUNTRESS";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinHuntressAlt.baseSkins;
            newSkinDef.meshReplacements = skinHuntressDefault.meshReplacements;
            //newSkinDef.projectileGhostReplacements = Projectiles;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinHuntressDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            GameObject DisplayEliteRabbitEars = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/DLC1/DisplayEliteRabbitEars.prefab").WaitForCompletion();
            GameObject DisplayEliteRabbitEarsNew = R2API.PrefabAPI.InstantiateClone(DisplayEliteRabbitEars, "HuntressRabbitEars", false);
            SkinnedMeshRenderer RabbitMesh = DisplayEliteRabbitEarsNew.transform.GetChild(0).GetChild(0).GetComponent<SkinnedMeshRenderer>();

            Material matRabbitEars = Object.Instantiate(RabbitMesh.material);

            Texture2D texRabbitEarsDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texRabbitEarsDiffuse.LoadImage(Properties.Resources.texRabbitEarsDiffuse, true);
            texRabbitEarsDiffuse.filterMode = FilterMode.Bilinear;
            texRabbitEarsDiffuse.wrapMode = TextureWrapMode.Clamp;

            matRabbitEars.mainTexture = texRabbitEarsDiffuse;
            matRabbitEars.color = new Color(0.6f,0.3f,0.6f);
            matRabbitEars.SetTexture("_FresnelRamp", null);
            RabbitMesh.material = matRabbitEars;
            DisplayEliteRabbitEarsNew.GetComponent<ItemDisplay>().rendererInfos[0].defaultMaterial = matRabbitEars;

            newSkinDef.addGameObjects = new ItemDisplayRule[]
            {
                        new ItemDisplayRule
                        {
                            ruleType = ItemDisplayRuleType.ParentedPrefab,
                            followerPrefab = DisplayEliteRabbitEarsNew,
                            childName = "Head",
                            localPos = new Vector3(0f, 0.3f, -0.05f),
                            localAngles = new Vector3(340f,0f,0f),
                            localScale = new Vector3(0.7f,0.7f,0.7f),
                            limbMask = LimbFlags.None
                        },
            };

            ReplaceArrowRainVFX replaceArrowRainVFX = HuntressArrowRain.AddComponent<ReplaceArrowRainVFX>();
            replaceArrowRainVFX.skinDef = newSkinDef;
            replaceArrowRainVFX.newVFX = HuntressArrowRainWolfo1.transform.GetChild(0).gameObject;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/HuntressBody"), newSkinDef);
        }

        public class ReplaceArrowRainVFX : MonoBehaviour
        {
            public GameObject newVFX;
            public SkinDef skinDef;

            public void FixedUpdate()
            {   
                GameObject owner = this.gameObject.GetComponent<RoR2.Projectile.ProjectileController>().owner;
                Debug.LogWarning(owner);
                if (owner)
                {
                    CharacterBody body = owner.GetComponent<CharacterBody>();
                    Debug.Log(SkinCatalog.GetBodySkinDef(body.bodyIndex, (int)body.skinIndex));
                    if (SkinCatalog.GetBodySkinDef(body.bodyIndex, (int)body.skinIndex) == skinDef)
                    {
                        this.gameObject.transform.GetChild(0).GetChild(3).parent = this.transform;
                        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        Instantiate(newVFX, this.transform);
                    }
                    Destroy(this);
                }
            }
        }


        [RegisterAchievement("SIMU_SKIN_HUNTRESS", "Skins.Huntress.Wolfo", null, null)]
        public class ClearSimulacrumHuntressBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("HuntressBody");
            }
        }
    }
}