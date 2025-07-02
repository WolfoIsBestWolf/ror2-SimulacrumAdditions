using MonoMod.Cil;
using R2API;
using RoR2;
using SimulacrumAdditions.Waves;
using UnityEngine;

namespace SimulacrumAdditions
{
    public static class ItemHelpers
    {
        public static ItemDef Ghost = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/Ghost");
        public static ItemDef HealthDecay = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/HealthDecay");
        public static ItemDef AACannon = LegacyResourcesAPI.Load<ItemDef>("ItemDefs/AACannon");
        public static ItemDef ITDamageDownMult;
        public static ItemDef ITAttackSpeedDownMult;
        public static ItemDef ITHealthUpMult;
        public static ItemDef ITKillOnCompletion;
        public static ItemDef ITHorrorName;
        public static ItemDef ITCooldownUp;

        public static ItemDef ITMakeDudeInvisible;
        public static ItemDef ITDisableAllSkills;
        public static ItemDef ITDisableMovement;
        public static ItemDef ITMakeImmune;

        public static ItemDef Make(string name)
        {
            ItemDef def = ScriptableObject.CreateInstance<ItemDef>();
            def.name = name;
            def.nameToken = name;
            def.pickupToken = name;
            def.descriptionToken = name;
            def.deprecatedTier = ItemTier.NoTier;
            def.hidden = true;
            def.canRemove = false;
            def.pickupIconSprite = AACannon.pickupIconSprite;
            def.pickupModelPrefab = AACannon.pickupModelPrefab;
            ItemAPI.Add(new CustomItem(def, System.Array.Empty<ItemDisplayRule>()));
            return def;
        }


        public static void MakeItems()
        {
            ITDamageDownMult = Make("ITDamageDownMult");
            ITAttackSpeedDownMult = Make("ITAttackSpeedDownMult");
            ITHealthUpMult = Make("ITHealthUpMult");
            ITKillOnCompletion = Make("ITKillsThisGuy");
            ITCooldownUp = Make("ITCooldownUp");
            ITMakeDudeInvisible = Make("ITMakeDudeInvisible");
            ITDisableAllSkills = Make("ITDisableAllSkills");
            ITDisableMovement = Make("ITDisableMovement");
            ITMakeImmune = Make("ITMakeImmune");


            On.RoR2.Util.GetBestBodyName += HorrorName;

            On.RoR2.CharacterBody.RecalculateStats += CharacterBody_RecalculateStats;
            IL.RoR2.CharacterBody.RecalculateStats += IL_CharacterBody_RecalculateStats;

        }

        private static string HorrorName(On.RoR2.Util.orig_GetBestBodyName orig, GameObject bodyObject)
        {
            if (bodyObject)
            {
                CharacterBody characterBody = bodyObject.GetComponent<CharacterBody>();
                if (characterBody && characterBody.inventory)
                {
                    if (characterBody.inventory.GetItemCount(ITHorrorName) > 0)
                    {
                        return Language.GetString("HORROR_BODY_NAME");
                    }
                }
            }
            return orig(bodyObject);
        }

        private static void CharacterBody_RecalculateStats(On.RoR2.CharacterBody.orig_RecalculateStats orig, CharacterBody self)
        {
            orig(self);
            if (self.inventory)
            {
                int numDmg = self.inventory.GetItemCount(ITDamageDownMult);
                if (numDmg > 0)
                {
                    self.damage *= 1 - 0.01f * numDmg;
                }
                int numAspd = self.inventory.GetItemCount(ITAttackSpeedDownMult);
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
                float numCooldown = self.inventory.GetItemCount(ItemHelpers.ITCooldownUp);
                if (numCooldown > 0)
                {
                    if (self.skillLocator.primary)
                    {
                        self.skillLocator.primary.cooldownScale *= (1 + (numCooldown / 10));
                    }
                    if (self.skillLocator.secondaryBonusStockSkill)
                    {
                        self.skillLocator.secondaryBonusStockSkill.cooldownScale *= 1 + numCooldown / 10;
                    }
                    if (self.skillLocator.utilityBonusStockSkill)
                    {
                        self.skillLocator.utilityBonusStockSkill.cooldownScale *= 1 + numCooldown / 10;
                    }
                    if (self.skillLocator.specialBonusStockSkill)
                    {
                        self.skillLocator.specialBonusStockSkill.cooldownScale *= 1 + numCooldown / 10;
                    }
                }

                int noMove = self.inventory.GetItemCount(ITDisableMovement);
                if (noMove > 0)
                {
                    self.SetBuffCount(RoR2Content.Buffs.Nullified.buffIndex, 1);
                }
                int makeImmune = self.inventory.GetItemCount(ITMakeImmune);
                if (makeImmune > 0)
                {
                    self.SetBuffCount(RoR2Content.Buffs.Immune.buffIndex, 1);
                }
                int DisableAllSkills = self.inventory.GetItemCount(ITDisableAllSkills);
                if (DisableAllSkills > 0)
                {
                    self.SetBuffCount(DLC2Content.Buffs.DisableAllSkills.buffIndex, 1);
                }

                if (self.HasBuff(Waves_BuffRelated.bdSlippery))
                {
                    self.acceleration /= 7.5f;
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
            if (c.TryGotoNext(MoveType.Before,
             x => x.MatchCall("RoR2.CharacterBody", "get_maxBarrier")
            ))
            {
                c.EmitDelegate<System.Func<CharacterBody, CharacterBody>>((body) =>
                {
                    //Might not have inventory ig
                    if (body.inventory)
                    {
                        int numHealth = body.inventory.GetItemCount(ITHealthUpMult);
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