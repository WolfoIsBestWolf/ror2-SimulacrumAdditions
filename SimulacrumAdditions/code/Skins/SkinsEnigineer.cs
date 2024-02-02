using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsEngineer
    {
        internal static void EngiSkin()
        {
            //RoRR Red/Yellow Engineer
            SkinDef skinEngiDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiDefault.asset").WaitForCompletion();
            SkinDef skinEngiAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiAlt.asset").WaitForCompletion();
            SkinDef skinEngiTurretDefault = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretDefault.asset").WaitForCompletion();
            SkinDef skinEngiTurretAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Engi/skinEngiTurretAlt.asset").WaitForCompletion();

            //Engi
            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[3];
            System.Array.Copy(skinEngiDefault.rendererInfos, NewRenderInfos, 3);

            CharacterModel.RendererInfo[] TurretNewRenderInfos = new CharacterModel.RendererInfo[1];
            System.Array.Copy(skinEngiTurretDefault.rendererInfos, TurretNewRenderInfos, 1);

            Material matEngi = Object.Instantiate(skinEngiDefault.rendererInfos[2].defaultMaterial);
            Material matEngiTrail = Object.Instantiate(skinEngiDefault.rendererInfos[0].defaultMaterial);
            Material matEngiTurret = Object.Instantiate(skinEngiTurretDefault.rendererInfos[0].defaultMaterial);

            Texture2D texEngiDiffuseAlt2 = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texEngiDiffuseAlt2.LoadImage(Properties.Resources.texEngiDiffuseAlt2, true);
            texEngiDiffuseAlt2.filterMode = FilterMode.Bilinear;
            texEngiDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texEngiEmissionAlt2 = new Texture2D(512, 512, TextureFormat.DXT5, false);
            texEngiEmissionAlt2.LoadImage(Properties.Resources.texEngiEmissionAlt2, true);
            texEngiEmissionAlt2.filterMode = FilterMode.Bilinear;
            texEngiEmissionAlt2.wrapMode = TextureWrapMode.Repeat;

            Texture2D texRampEngiAlt2 = new Texture2D(256, 16, TextureFormat.DXT1, false);
            texRampEngiAlt2.LoadImage(Properties.Resources.texRampEngiAlt2, true);
            texRampEngiAlt2.filterMode = FilterMode.Bilinear;
            texRampEngiAlt2.wrapMode = TextureWrapMode.Clamp;

            Texture2D texEngiTurretDiffuseAlt2 = new Texture2D(256, 256, TextureFormat.DXT5, false);
            texEngiTurretDiffuseAlt2.LoadImage(Properties.Resources.texEngiTurretDiffuseAlt2, true);
            texEngiTurretDiffuseAlt2.filterMode = FilterMode.Bilinear;
            texEngiTurretDiffuseAlt2.wrapMode = TextureWrapMode.Repeat;

            matEngiTurret.mainTexture = texEngiTurretDiffuseAlt2;
            matEngiTurret.SetTexture("_PrintRamp", texRampEngiAlt2);
            matEngiTurret.SetColor("_EmColor", new Color(0.845f, 0.8f, 0.365f));

            matEngi.mainTexture = texEngiDiffuseAlt2;
            matEngi.SetTexture("_EmTex", texEngiEmissionAlt2);
            matEngiTrail.SetTexture("_RemapTex", texRampEngiAlt2);

            NewRenderInfos[0].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[1].defaultMaterial = matEngiTrail;     //matEngiTrail
            NewRenderInfos[2].defaultMaterial = matEngi;          //matEngi

            TurretNewRenderInfos[0].defaultMaterial = matEngiTurret;
            //
            //MeshReplacements
            RoR2.SkinDef.MeshReplacement[] MeshReplacements = new SkinDef.MeshReplacement[1];
            skinEngiDefault.meshReplacements.CopyTo(MeshReplacements, 0);
            //
            //ProjectileReplacements
            GameObject EngiGrenadeGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiGrenadeGhostSkin2.prefab").WaitForCompletion(), "EngiGrenadeGhostSkinW", false);
            GameObject EngiMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiMineGhost2.prefab").WaitForCompletion(), "EngiMineGhostW", false);
            GameObject SpiderMineGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/SpiderMineGhost2.prefab").WaitForCompletion(), "SpiderMineGhostW", false);
            GameObject EngiBubbleShieldGhostW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiBubbleShieldGhost2.prefab").WaitForCompletion(), "EngiBubbleShieldGhostW", false);
            GameObject EngiHarpoonProjectile = Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoon.prefab").WaitForCompletion();
            GameObject EngiHarpoonGhostSkinW = PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Engi/EngiHarpoonGhost.prefab").WaitForCompletion(), "EngiHarpoonGhostSkinW", false);

            RoR2.SkinDef.ProjectileGhostReplacement[] ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[5];
            skinEngiAlt.projectileGhostReplacements.CopyTo(ProjectileGhostReplacements, 0);

            EngiGrenadeGhostSkinW.transform.GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            SpiderMineGhostW.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(1).GetComponent<MeshRenderer>().material = matEngiTurret;
            EngiBubbleShieldGhostW.transform.GetChild(0).GetChild(2).GetComponent<MeshRenderer>().material = matEngiTurret;

            Transform SpiderMineLights = SpiderMineGhostW.transform.Find("mdlEngiSpiderMine/EngiSpiderMineArmature/Base/Screw");

            Color Yellow1 = new Color(0.75f, 0.75f, 0.25f);
            Color Yellow2 = new Color(1, 1, 0.25f);

            SpiderMineLights.GetChild(1).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(1).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1
            SpiderMineLights.GetChild(2).GetComponent<LineRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(2).GetChild(0).GetComponent<Light>().color = Yellow1; //0.251 0.7373 0.5451 1

            SpiderMineLights.GetChild(2).GetChild(1).GetComponent<UnityEngine.TrailRenderer>().startColor = Yellow2;

            SpiderMineLights.GetChild(3).GetComponent<Light>().color = new Color(0.75f, 0.75f, 0.25f);
            SpiderMineLights.GetChild(3).GetComponent<LineRenderer>().startColor = new Color(1, 1, 0.25f);

            SpiderMineLights.GetChild(3).GetChild(0).GetComponent<Light>().color = Yellow1;
            //
            ParticleSystemRenderer particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(0).GetComponent<ParticleSystemRenderer>(); //matEngiHarpoonRing 
            Material newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAlt2);
            particleSystem.material = newShards;

            EngiHarpoonGhostSkinW.transform.GetChild(1).GetComponent<MeshRenderer>().material = matEngi;
            TrailRenderer trailRender = EngiHarpoonGhostSkinW.transform.GetChild(1).GetChild(0).GetComponent<TrailRenderer>(); //matEngiHarpoonTrail 
            Material newTrail = Object.Instantiate(trailRender.material);
            newTrail.SetTexture("_RemapTex", texRampEngiAlt2);
            trailRender.material = newTrail;

            //EngiHarpoonGhostSkinW.transform.GetChild(2).GetComponent<ParticleSystemRenderer>(); //GenericFlash
            particleSystem = EngiHarpoonGhostSkinW.transform.GetChild(3).GetComponent<ParticleSystemRenderer>(); //matEngiShieldSHards 
            newShards = Object.Instantiate(particleSystem.material);
            newShards.SetTexture("_RemapTex", texRampEngiAlt2);
            newShards.SetColor("_TintColor", new Color(1,1,0,1));
            particleSystem.material = newShards;

            //Would ideally also need a muzzle flash replacement guh

            ProjectileGhostReplacements[0].projectileGhostReplacementPrefab = EngiGrenadeGhostSkinW;
            ProjectileGhostReplacements[1].projectileGhostReplacementPrefab = EngiMineGhostW;
            ProjectileGhostReplacements[2].projectileGhostReplacementPrefab = SpiderMineGhostW;
            ProjectileGhostReplacements[3].projectileGhostReplacementPrefab = EngiBubbleShieldGhostW;
            ProjectileGhostReplacements[4] = new SkinDef.ProjectileGhostReplacement
            {
                projectileGhostReplacementPrefab = EngiHarpoonGhostSkinW,
                projectilePrefab = EngiHarpoonProjectile
            };
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.texSkinEngi3, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            //MinionReplacements
            RoR2.SkinDef.MinionSkinReplacement[] MinionSkinReplacements = new SkinDef.MinionSkinReplacement[2];
            skinEngiAlt.minionSkinReplacements.CopyTo(MinionSkinReplacements, 0);

            R2API.SkinDefInfo SkinInfoTurret = new R2API.SkinDefInfo
            {
                Name = "skinEngiTurretWolfo",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = skinEngiTurretDefault.rootObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew = Skins.CreateNewSkinDef(SkinInfoTurret);
            TurretSkinDefNew.Bake();
            MinionSkinReplacements[0].minionSkin = TurretSkinDefNew;

            TurretNewRenderInfos[0].renderer = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>();
            R2API.SkinDefInfo SkinInfoTurret2 = new R2API.SkinDefInfo
            {
                Name = "skinEngiTurretWalkerWolfo",
                NameToken = "",
                Icon = null,
                BaseSkins = skinEngiTurretAlt.baseSkins,
                RendererInfos = TurretNewRenderInfos,
                RootObject = LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody").transform.GetChild(0).GetChild(0).gameObject,
                UnlockableDef = null,
            };
            SkinDef TurretSkinDefNew2 = Skins.CreateNewSkinDef(SkinInfoTurret2);
            TurretSkinDefNew2.Bake();
            MinionSkinReplacements[1].minionSkin = TurretSkinDefNew2;
            //
            //
            //
            //Unlockable
            LanguageAPI.Add("SIMU_SKIN_ENGINEER", "EOD Fashion");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ENGINEER_NAME", "Engineer : Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_ENGINEER_DESCRIPTION", "As Engineer, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_ENGINEER_NAME";
            unlockableDef.cachedName = "Skins.Engineer.Wolfo";
            unlockableDef.achievementIcon = SkinIconS;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinEngiWolfo";
            newSkinDef.nameToken = "SIMU_SKIN_ENGINEER";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinEngiAlt.baseSkins;
            newSkinDef.meshReplacements = MeshReplacements;
            newSkinDef.projectileGhostReplacements = ProjectileGhostReplacements;
            newSkinDef.minionSkinReplacements = MinionSkinReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinEngiDefault.rootObject;
            newSkinDef.unlockableDef = unlockableDef;
            newSkinDef.lightColorsChanges = new SkinDefWolfo.LightColorChanges[]
            {
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0), //0 1 0.5508 1
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.l/cannonJoint2.l/cannonHead.l/EngiJet/Point Light",
                },
                new SkinDefWolfo.LightColorChanges
                {
                    color = new Color(0.9f,0.9f,0),
                    lightPath = "EngiArmature/ROOT/base/stomach/chest/cannonJoint1.r/cannonJoint2.r/cannonHead.r/EngiJet/Point Light (1)",
                }
            };

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiBody"), newSkinDef);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiTurretBody"), TurretSkinDefNew);
            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/EngiWalkerTurretBody"), TurretSkinDefNew2);
        }

        [RegisterAchievement("SIMU_SKIN_ENGINEER", "Skins.Engineer.Wolfo", "Complete30StagesCareer", null)]
        public class ClearSimulacrumTreebotBody : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("EngiBody");
            }
        }
    }
}