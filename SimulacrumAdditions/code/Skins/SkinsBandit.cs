using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsBandit
    {
        internal static void BanditSkin()
        {
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);

            Texture2D texBanditRedSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texBanditRedSkinIcon.LoadImage(Properties.Resources.texBanditRedSkinIcon, true);
            texBanditRedSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texBanditRedSkinIconS = Sprite.Create(texBanditRedSkinIcon, WRect.rec128, WRect.half);

            Texture2D texBanditRedDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedDiffuse.LoadImage(Properties.Resources.texBanditRedDiffuse, true);
            texBanditRedDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedCoatDiffuse.LoadImage(Properties.Resources.texBanditRedCoatDiffuse, true);
            texBanditRedCoatDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texBanditRedEmission.LoadImage(Properties.Resources.texBanditRedEmission, true);
            texBanditRedEmission.filterMode = FilterMode.Bilinear;
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texBanditShotgunDiffuse.LoadImage(Properties.Resources.texBanditShotgunDiffuse, true);
            texBanditShotgunDiffuse.filterMode = FilterMode.Bilinear;
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1f, 0.8f, 1f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.5f, 0.15f, 0.25f)); //100 30 50
            //matBandit2Coat.color = new Color(0.85f, 0.85f, 0.82f);
            matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[5];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh, 0);

            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_BANDIT", "Autumn");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_BANDIT_NAME", "Bandit: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_BANDIT_DESCRIPTION", "As Bandit, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_BANDIT_NAME";
            unlockableDef.cachedName = "Skins.Bandit.Wolfo";
            unlockableDef.achievementIcon = texBanditRedSkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            R2API.SkinDefInfo BanditRedSkinInfos = new R2API.SkinDefInfo
            {
                Name = "skinBandit2Wolfo",
                NameToken = "SIMU_SKIN_BANDIT",
                Icon = texBanditRedSkinIconS,
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);
            BanditSkin2(unlockableDef);
        }

        internal static void BanditSkin2(UnlockableDef unlockableDef)
        {
            //RoRR Red Bandit
            SkinDef BanditDefaultSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[0];
            SkinDef BanditAltSkin = RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body").transform.GetChild(0).GetChild(0).gameObject.GetComponent<ModelSkinController>().skins[1];

            CharacterModel.RendererInfo[] BanditRedRenderInfos = new CharacterModel.RendererInfo[8];
            System.Array.Copy(BanditDefaultSkin.rendererInfos, BanditRedRenderInfos, 8);

            Material matBanditRed1 = Object.Instantiate(BanditDefaultSkin.rendererInfos[0].defaultMaterial);
            //Material matBanditRed2      = Object.Instantiate(BanditDefaultSkin.rendererInfos[1].defaultMaterial);
            //Material matBanditRed3      = Object.Instantiate(BanditDefaultSkin.rendererInfos[2].defaultMaterial);
            Material matBandit2Coat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2CoatHat = Object.Instantiate(BanditDefaultSkin.rendererInfos[3].defaultMaterial);
            Material matBandit2Shotgun = Object.Instantiate(BanditDefaultSkin.rendererInfos[4].defaultMaterial);
            Material matBandit2Knife = Object.Instantiate(BanditDefaultSkin.rendererInfos[5].defaultMaterial);
            //Material matBandit2Coat2    = Object.Instantiate(BanditDefaultSkin.rendererInfos[6].defaultMaterial);
            Material matBandit2Revolver = Object.Instantiate(BanditDefaultSkin.rendererInfos[7].defaultMaterial);

            Texture2D texBanditRedSkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            texBanditRedSkinIcon.LoadImage(Properties.Resources.texBanditRedSkinIconPURPLE, true);
            texBanditRedSkinIcon.filterMode = FilterMode.Bilinear;
            Sprite texBanditRedSkinIconS = Sprite.Create(texBanditRedSkinIcon, WRect.rec128, WRect.half);

            Texture2D texBanditRedDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedDiffuse.LoadImage(Properties.Resources.texBanditRedDiffusePURPLE, true);
            texBanditRedDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedCoatDiffuse = new Texture2D(1024, 1024, TextureFormat.DXT5, false);
            texBanditRedCoatDiffuse.LoadImage(Properties.Resources.texBanditRedCoatDiffusePURPLE, true);
            texBanditRedCoatDiffuse.filterMode = FilterMode.Bilinear;
            texBanditRedCoatDiffuse.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditRedEmission = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
            texBanditRedEmission.LoadImage(Properties.Resources.texBanditRedEmissionPURPLE, true);
            texBanditRedEmission.filterMode = FilterMode.Bilinear;
            texBanditRedEmission.wrapMode = TextureWrapMode.Clamp;

            Texture2D texBanditShotgunDiffuse = new Texture2D(256, 256, TextureFormat.DXT1, false);
            texBanditShotgunDiffuse.LoadImage(Properties.Resources.texBanditShotgunDiffusePURPLE, true);
            texBanditShotgunDiffuse.filterMode = FilterMode.Bilinear;
            texBanditShotgunDiffuse.wrapMode = TextureWrapMode.Clamp;

            //
            matBanditRed1.mainTexture = texBanditRedDiffuse;
            matBandit2Coat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2CoatHat.mainTexture = texBanditRedCoatDiffuse;
            matBandit2Shotgun.mainTexture = texBanditShotgunDiffuse;
            matBandit2Knife.mainTexture = texBanditShotgunDiffuse;
            matBandit2Revolver.mainTexture = texBanditShotgunDiffuse;

            matBanditRed1.SetTexture("_EmTex", texBanditRedEmission);
            matBanditRed1.SetColor("_EmColor", new Color(1.2f, 1.2f, 0.5f)); //0 0.3491 0.327 1
            //matBandit2Shotgun.SetColor("_EmColor", new Color(0.4f, 0.12f, 0.19f)); //100 30 50
            matBandit2Shotgun.SetColor("_EmColor", new Color(0.8f, 0.7f, 0f)); //100 30 50
            //matBandit2Coat.color = new Color(0.85f, 0.85f, 0.82f);
            //matBandit2Coat.color = new Color(0.95f, 0.95f, 0.87f);

            BanditRedRenderInfos[0].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2AccessoriesMesh //texBandit2Diffuse
            BanditRedRenderInfos[1].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2ArmsMesh
            BanditRedRenderInfos[2].defaultMaterial = matBanditRed1;     //matBandit2         //Bandit2BodyMesh
            BanditRedRenderInfos[3].defaultMaterial = matBandit2Coat;     //matBandit2Coat     //Bandit2CoatMesh        //texBandit2CoatDiffuse
            BanditRedRenderInfos[4].defaultMaterial = matBandit2Shotgun;     //matBandit2Shotgun  //BanditShotgunMesh      //texBanditShotgunDiffuse
            BanditRedRenderInfos[5].defaultMaterial = matBandit2Knife;     //matBandit2Knife    //BladeMesh              //texBanditShotgunDiffuse
            BanditRedRenderInfos[6].defaultMaterial = matBandit2CoatHat;     //matBandit2Coat     //Bandit2HatMesh
            BanditRedRenderInfos[7].defaultMaterial = matBandit2Revolver;     //matBandit2Revolver //BanditPistolMesh       //texBanditShotgunDiffuse

            //
            RoR2.SkinDef.MeshReplacement[] BanditRedMesh = new SkinDef.MeshReplacement[5];
            BanditDefaultSkin.meshReplacements.CopyTo(BanditRedMesh, 0);

            BanditRedMesh[3] = BanditAltSkin.meshReplacements[2];
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_BANDIT2", "Royal Fasion");

            R2API.SkinDefInfo BanditRedSkinInfos = new R2API.SkinDefInfo
            {
                Name = "skinBandit2WolfoPurple",
                NameToken = "SIMU_SKIN_BANDIT2",
                Icon = texBanditRedSkinIconS,
                BaseSkins = BanditAltSkin.baseSkins,
                MeshReplacements = BanditRedMesh,
                ProjectileGhostReplacements = BanditDefaultSkin.projectileGhostReplacements,
                RendererInfos = BanditRedRenderInfos,
                RootObject = BanditDefaultSkin.rootObject,
                UnlockableDef = unlockableDef,
            };
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/Bandit2Body"), BanditRedSkinInfos);
           
        }


        [RegisterAchievement("SIMU_SKIN_BANDIT", "Skins.Bandit.Wolfo", null, null)]
        public class ClearSimulacrumBandit2Body : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("Bandit2Body");
            }
        }
    }
}