using BepInEx;
using MonoMod.Cil;
using R2API;
using R2API.Utils;
using RoR2;
using RoR2.ExpansionManagement;
using System.Security;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace SimulacrumAdditions
{
    public class ItemHelpers
    {

        public static ItemDef ITDamageDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITAttackSpeedDown = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHealthScaling = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITKillOnCompletion = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITHorrorName = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITCooldownUp = ScriptableObject.CreateInstance<ItemDef>();

        public static ItemDef ITMakeDudeInvisible = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITDisableAllSkills = ScriptableObject.CreateInstance<ItemDef>();
        public static ItemDef ITDisableMovement = ScriptableObject.CreateInstance<ItemDef>();


        public static ItemDef ImmuneToVoidFog = ScriptableObject.CreateInstance<ItemDef>();



        public static void MakeItems()
        {
            ItemDef AACannon = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AACannon");
            ITDamageDown.name = "ITDamageDown";
            ITDamageDown.nameToken = "ITDamageDown";
            ITDamageDown.pickupToken = "ITDamageDown";
            ITDamageDown.descriptionToken = "ITDamageDown";
            ITDamageDown.deprecatedTier = ItemTier.NoTier;
            ITDamageDown._itemTierDef = AACannon._itemTierDef;
            ITDamageDown.hidden = true;
            ITDamageDown.canRemove = false;
            ITDamageDown.pickupIconSprite = AACannon.pickupIconSprite;
            ITDamageDown.pickupModelPrefab = AACannon.pickupModelPrefab;

            CustomItem customItem = new CustomItem(ITDamageDown, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            
            ITAttackSpeedDown.name = "ITAttackSpeedDown";
            ITAttackSpeedDown.nameToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.pickupToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.descriptionToken = "ITAttackSpeedDown";
            ITAttackSpeedDown.deprecatedTier = ItemTier.NoTier;
            ITAttackSpeedDown._itemTierDef = AACannon._itemTierDef;
            ITAttackSpeedDown.hidden = true;
            ITAttackSpeedDown.canRemove = false;
            ITAttackSpeedDown.pickupIconSprite = AACannon.pickupIconSprite;
            ITAttackSpeedDown.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITAttackSpeedDown, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITHealthScaling.name = "ITHealthScaling";
            ITHealthScaling.nameToken = "ITHealthScaling";
            ITHealthScaling.pickupToken = "ITHealthScaling";
            ITHealthScaling.descriptionToken = "ITHealthScaling";
            ITHealthScaling.deprecatedTier = ItemTier.NoTier;
            ITHealthScaling._itemTierDef = AACannon._itemTierDef;
            ITHealthScaling.hidden = true;
            ITHealthScaling.canRemove = false;
            ITHealthScaling.pickupIconSprite = AACannon.pickupIconSprite;
            ITHealthScaling.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITHealthScaling, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITKillOnCompletion.name = "ITKillOnCompletion";
            ITKillOnCompletion.nameToken = "ITKillOnCompletion";
            ITKillOnCompletion.pickupToken = "ITKillOnCompletion";
            ITKillOnCompletion.descriptionToken = "ITKillOnCompletion";
            ITKillOnCompletion.deprecatedTier = ItemTier.NoTier;
            ITKillOnCompletion._itemTierDef = AACannon._itemTierDef;
            ITKillOnCompletion.hidden = true;
            ITKillOnCompletion.canRemove = false;
            ITKillOnCompletion.pickupIconSprite = AACannon.pickupIconSprite;
            ITKillOnCompletion.pickupModelPrefab = AACannon.pickupModelPrefab;

            customItem = new CustomItem(ITKillOnCompletion, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            //
            ITHorrorName.name = "ITHorrorName";
            ITHorrorName.nameToken = "ITHorrorName";
            ITHorrorName.pickupToken = "ITHorrorName";
            ITHorrorName.descriptionToken = "ITHorrorName";
            ITHorrorName.deprecatedTier = ItemTier.NoTier;
            ITHorrorName._itemTierDef = AACannon._itemTierDef;
            ITHorrorName.hidden = true;
            ITHorrorName.canRemove = false;
            ITHorrorName.pickupIconSprite = AACannon.pickupIconSprite;
            ITHorrorName.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITHorrorName, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);
            On.RoR2.Util.GetBestBodyName += (orig, bodyObject) =>
            {
                if (bodyObject)
                {
                    CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                    if (characterBody && characterBody.inventory)
                    {
                        if (characterBody.inventory.GetItemCount(ITHorrorName) > 0)
                        {
                            return "Unknown Horror";
                        }
                    }
                }
                return orig(bodyObject);
            };

            ITCooldownUp.name = "ITCooldownUp";
            ITCooldownUp.nameToken = "ITCooldownUp";
            ITCooldownUp.pickupToken = "ITCooldownUp";
            ITCooldownUp.descriptionToken = "10% longer cooldown";
            ITCooldownUp.deprecatedTier = ItemTier.NoTier;
            ITCooldownUp._itemTierDef = AACannon._itemTierDef;
            ITCooldownUp.hidden = true;
            ITCooldownUp.canRemove = false;
            ITCooldownUp.pickupIconSprite = AACannon.pickupIconSprite;
            ITCooldownUp.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ITCooldownUp, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);

            ImmuneToVoidFog.name = "ImmuneToVoidFog";         
            ImmuneToVoidFog.nameToken = "ImmuneToVoidFog";
            ImmuneToVoidFog.pickupToken = "ImmuneToVoidFog";
            ImmuneToVoidFog.descriptionToken = "No damage from void fog";
            ImmuneToVoidFog.deprecatedTier = ItemTier.NoTier;
            ImmuneToVoidFog._itemTierDef = AACannon._itemTierDef;
            ImmuneToVoidFog.hidden = true;
            ImmuneToVoidFog.canRemove = false;
            ImmuneToVoidFog.pickupIconSprite = AACannon.pickupIconSprite;
            ImmuneToVoidFog.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ImmuneToVoidFog, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);

            ITMakeDudeInvisible.name = "ITMakeDudeInvisible";
            ITMakeDudeInvisible.nameToken = "ITMakeDudeInvisible";
            ITMakeDudeInvisible.pickupToken = "ITMakeDudeInvisible";
            ITMakeDudeInvisible.descriptionToken = "ITMakeDudeInvisible";
            ITMakeDudeInvisible.deprecatedTier = ItemTier.NoTier;
            ITMakeDudeInvisible._itemTierDef = AACannon._itemTierDef;
            ITMakeDudeInvisible.hidden = true;
            ITMakeDudeInvisible.canRemove = false;
            ITMakeDudeInvisible.pickupIconSprite = AACannon.pickupIconSprite;
            ITMakeDudeInvisible.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ImmuneToVoidFog, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);

            ITDisableAllSkills.name = "ITDisableAllSkills";
            ITDisableAllSkills.nameToken = "ITDisableAllSkills";
            ITDisableAllSkills.pickupToken = "ITDisableAllSkills";
            ITDisableAllSkills.descriptionToken = "ITDisableAllSkills";
            ITDisableAllSkills.deprecatedTier = ItemTier.NoTier;
            ITDisableAllSkills._itemTierDef = AACannon._itemTierDef;
            ITDisableAllSkills.hidden = true;
            ITDisableAllSkills.canRemove = false;
            ITDisableAllSkills.pickupIconSprite = AACannon.pickupIconSprite;
            ITDisableAllSkills.pickupModelPrefab = AACannon.pickupModelPrefab;
            customItem = new CustomItem(ImmuneToVoidFog, new ItemDisplayRule[0]);
            ItemAPI.Add(customItem);


            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            IL.RoR2.CharacterBody.RecalculateStats += IL_CharacterBody_RecalculateStats;
 
        }
        private static void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            if (self.inventory)
            {
                int numDmg = self.inventory.GetItemCount(ITDamageDown);
                if (numDmg > 0)
                {
                    self.damage *= 1 - 0.01f * numDmg;
                }
                int numAspd = self.inventory.GetItemCount(ITAttackSpeedDown);
                if (numAspd > 0)
                {
                    self.attackSpeed *= 1 - 0.01f * numAspd;
                    int numITKillOnCompletion = self.inventory.GetItemCount(ITKillOnCompletion);
                    if (numITKillOnCompletion == 54)
                    {
                        self.healthComponent.Networkhealth = self.maxHealth * 0.45f;
                    }
                    if (numITKillOnCompletion == 56)
                    {
                        self.skillLocator.primary = null;
                        self.AddBuff(RoR2Content.Buffs.Cloak);
                    }
                    if (numITKillOnCompletion > 77)
                    {
                        self.baseMoveSpeed = 0;
                        self.moveSpeed = 0;
                        self.skillLocator.primary = null;
                        if (numITKillOnCompletion > 78)
                        {
                            self.AddBuff(RoR2Content.Buffs.Cloak);

                            if (numITKillOnCompletion == 81)
                            {
                                if (!self.master.gameObject.GetComponent<MasterSuicideOnTimer>())
                                {
                                    //This is like really unhealthy inIt?
                                    self.master.gameObject.AddComponent<MasterSuicideOnTimer>().lifeTimer = 0.1f;
                                }
                            }
                        }

                    }
                }
                int numCooldown = self.inventory.GetItemCount(ItemHelpers.ITCooldownUp);
                if (numCooldown > 0)
                {
                    if (self.skillLocator.primary)
                    {
                        self.skillLocator.primary.cooldownScale = 1 + numCooldown / 10;
                    }
                }

                if (self.HasBuff(Waves_BuffRelated.bdSlippery))
                {
                    self.acceleration /= 5;
                    self.moveSpeed *= 1.5f;
                }
                if (self.HasBuff(Waves_BuffRelated.bdBadLuck))
                {
                    self.master.luck = -3;
                }
            }
        }


        private static void IL_CharacterBody_RecalculateStats(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
             x => x.MatchCall("RoR2.CharacterBody", "set_barrierDecayRate")
            ))
            {
                c.Index -= 4;
                c.EmitDelegate<System.Func<CharacterBody, CharacterBody>>((body) =>
                {
                    //Might not have inventory ig
                    if (body.inventory)
                    {
                        int numHealth = body.inventory.GetItemCount(ITHealthScaling);
                        if (numHealth > 0)
                        {
                            body.maxHealth *= 1 + 0.01f * numHealth;
                            body.maxShield *= 1 + 0.01f * numHealth;
                        }
                    }
                    return body;
                });
                //Debug.Log("IL Found : IL.RoR2.CharacterBody.RecalculateStats");
            }
            else
            {
                Debug.LogWarning("IL Failed : IL.RoR2.CharacterBody.RecalculateStats");
            }
        }
    }


}