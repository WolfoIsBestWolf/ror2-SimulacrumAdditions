using R2API;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace SimulacrumAdditions
{
    public class SkinsCommando
    {
        public static GameObject CommandoDashJetsBlue = R2API.PrefabAPI.InstantiateClone(Addressables.LoadAssetAsync<GameObject>(key: "RoR2/Base/Commando/CommandoDashJets.prefab").WaitForCompletion(), "CommandoDashJetsBlue", false);

        internal static void CommandoSkin()
        {
            SkinDef UnusedCommandoSkin = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/DLC1/skinCommandoMarine.asset").WaitForCompletion();
            R2API.Skins.AddSkinToCharacter(RoR2.LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), UnusedCommandoSkin);

            //This removes Limb removal, not good but better than bugged ones I guess
            //I have no idea how to actually fix his Right Calf being both Calfs
            Material matCommandoDualiesMarine = Addressables.LoadAssetAsync<Material>(key: "RoR2/DLC1/matCommandoDualiesMarine.mat").WaitForCompletion();
            matCommandoDualiesMarine.DisableKeyword("LIMBREMOVAL");

            Texture2D skinIconCommando = new Texture2D(128, 128, TextureFormat.DXT5, false);
            skinIconCommando.LoadImage(Properties.Resources.skinIconCommandoSOTV, true);
            skinIconCommando.filterMode = FilterMode.Bilinear;
            Sprite skinIconCommandoS = Sprite.Create(skinIconCommando, WRect.rec128, WRect.half);

            LanguageAPI.Add("SIMU_SKIN_COMMANDO", "Providence Trial");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_COMMANDO_NAME", "Commando: Alternated");
            LanguageAPI.Add("ACHIEVEMENT_SIMU_SKIN_COMMANDO_DESCRIPTION", "As Commando, complete wave 50 in Simulacrum or escape the Planetarium.");

            UnlockableDef unlockableDef = ScriptableObject.CreateInstance<UnlockableDef>();
            unlockableDef.nameToken = "ACHIEVEMENT_SIMU_SKIN_COMMANDO_NAME";
            unlockableDef.cachedName = "Skins.Commando.Wolfo";
            unlockableDef.achievementIcon = skinIconCommandoS;
            UnusedCommandoSkin.unlockableDef = unlockableDef;
            R2API.ContentAddition.AddUnlockableDef(unlockableDef);
            //
            //
            //
            SkinDef skinCommandoAlt = Addressables.LoadAssetAsync<SkinDef>(key: "RoR2/Base/Commando/skinCommandoAlt.asset").WaitForCompletion();

            CharacterModel.RendererInfo[] NewRenderInfos = new CharacterModel.RendererInfo[skinCommandoAlt.rendererInfos.Length];
            System.Array.Copy(skinCommandoAlt.rendererInfos, NewRenderInfos, skinCommandoAlt.rendererInfos.Length);

            Texture2D texCommandoPaletteDiffuse = new Texture2D(64, 8, TextureFormat.DXT5, false);
            texCommandoPaletteDiffuse.LoadImage(Properties.Resources.texCommandoPaletteDiffuse, true);
            texCommandoPaletteDiffuse.filterMode = FilterMode.Bilinear;
            texCommandoPaletteDiffuse.wrapMode = TextureWrapMode.Clamp;

            Material matCommandoDualiesAlt = Object.Instantiate(skinCommandoAlt.rendererInfos[2].defaultMaterial);

            matCommandoDualiesAlt.mainTexture = texCommandoPaletteDiffuse;
            //matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.15f, 0.775f, 0.96f)); //0.8491 0.5267 0.1402 1
            matCommandoDualiesAlt.color = new Color(1.1f, 1f, 0.8f);
            matCommandoDualiesAlt.SetColor("_EmColor", new Color(0.2f, 0.9f, 0.9f)); //0.8491 0.5267 0.1402 1

            NewRenderInfos[2].defaultMaterial = matCommandoDualiesAlt;
            //
            //SkinIcon
            Texture2D SkinIcon = new Texture2D(128, 128, TextureFormat.DXT5, false);
            SkinIcon.LoadImage(Properties.Resources.skinIconCommando, true);
            SkinIcon.filterMode = FilterMode.Bilinear;
            Sprite SkinIconS = Sprite.Create(SkinIcon, WRect.rec128, WRect.half);
            //
            SkinDefWolfo newSkinDef = ScriptableObject.CreateInstance<SkinDefWolfo>();
            newSkinDef.name = "skinCommandoWolfoProvi";
            newSkinDef.nameToken = "SIMU_SKIN_COMMANDO";
            newSkinDef.icon = SkinIconS;
            newSkinDef.baseSkins = skinCommandoAlt.baseSkins;
            newSkinDef.meshReplacements = skinCommandoAlt.meshReplacements;
            newSkinDef.rendererInfos = NewRenderInfos;
            newSkinDef.rootObject = skinCommandoAlt.rootObject;
            newSkinDef.unlockableDef = unlockableDef;

            Skins.AddSkinToCharacter(LegacyResourcesAPI.Load<GameObject>("Prefabs/CharacterBodies/CommandoBody"), newSkinDef);

            CommandoDashJetsBlue.GetComponent<Light>().color = new Color(0, 0.7f, 1f); //1 0.7045 0.051 1
            CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = new Color(0, 0.425f, 1f);

            var colorOverLifetime = CommandoDashJetsBlue.transform.GetChild(0).GetComponent<ParticleSystem>().colorOverLifetime;
            colorOverLifetime.enabled = false;

            CommandoDashJetsBlue.transform.GetChild(3).GetComponent<ParticleSystem>().startColor = new Color(0, 0.3f, 0.3f); //0.2925 0.2922 0 1
            CommandoDashJetsBlue.transform.GetChild(4).GetComponent<ParticleSystem>().startColor = new Color(0.3f, 0.8f, 1f, 1); //1 0.4941 0.3098 1


            On.EntityStates.Commando.DodgeState.OnEnter += DodgeState_OnEnter;
            On.EntityStates.Commando.SlideState.OnEnter += SlideState_OnEnter;

        }

        private static void SlideState_OnEnter(On.EntityStates.Commando.SlideState.orig_OnEnter orig, EntityStates.Commando.SlideState self)
        {
            if (self.modelLocator.modelTransform.GetComponent<SkinDefWolfoTracker>())
            {
                GameObject jetOg = EntityStates.Commando.SlideState.jetEffectPrefab;
                EntityStates.Commando.SlideState.jetEffectPrefab = CommandoDashJetsBlue;
                orig(self);
                EntityStates.Commando.SlideState.jetEffectPrefab = jetOg;
                return;
            }
            else
            {
                orig(self);
            }
        }

        private static void DodgeState_OnEnter(On.EntityStates.Commando.DodgeState.orig_OnEnter orig, EntityStates.Commando.DodgeState self)
        {
            if(self.modelLocator.modelTransform.GetComponent<SkinDefWolfoTracker>())
            {
                GameObject jetOg = EntityStates.Commando.DodgeState.jetEffect;
                EntityStates.Commando.DodgeState.jetEffect = CommandoDashJetsBlue;
                orig(self);
                EntityStates.Commando.DodgeState.jetEffect = jetOg;
                return;
            }
            else
            {
                orig(self);
            }
        }

        [RegisterAchievement("SIMU_SKIN_COMMANDO", "Skins.Commando.Wolfo", null, null)]
        public class CommandoClearSimulacrum : SimuOrVoidEnding
        {
            public override BodyIndex LookUpRequiredBodyIndex()
            {
                return BodyCatalog.FindBodyIndex("CommandoBody");
            }
        }
    }
}