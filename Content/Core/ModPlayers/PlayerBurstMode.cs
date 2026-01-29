using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Burst;
using WoS.Content.Core.Changes;
using WoS.Content.Projectiles.Weapons.Vanila;
using Microsoft.Xna.Framework;
using WoS.Content.Items.Weapons.Magic.Grimoires;
using WoS.Content.Core.DamageClasses;
using System;

namespace WoS.Content.Core.ModPlayers
{
    public class PlayerBurstMode : ModPlayer
    {
        public bool isSkyBlessBurstActive => Player.HasBuff(ModContent.BuffType<BurstSkyBless>());
        public int SkyBlessBurstCD;
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player owner = Main.player[proj.owner];
            int celestialBladesCount = owner.ownedProjectileCounts[ModContent.ProjectileType<CelestialBlade>()];

            bool cfgActive = ModContent.GetInstance<MainConfig>().SkyFractureCelestialBlessing || NPC.downedGolemBoss;
            if (cfgActive && celestialBladesCount < 10 && !isSkyBlessBurstActive)
            {
                if (proj.type == ProjectileID.SkyFracture && (Main.rand.NextBool(4) || hit.Crit))
                {
                    Vector2 spawnPosition = proj.position;
                    int celestialBlades = ModContent.ProjectileType<CelestialBlade>();
                    Projectile.NewProjectileDirect(owner.GetSource_FromThis(), spawnPosition, Vector2.Zero, celestialBlades, 50, 0.1f, owner.whoAmI);
                }
            }
        }

        public override void PreUpdateBuffs()
        {
            if (isSkyBlessBurstActive)
            {
                Player.manaRegen = 0;
                Player.manaRegenBonus = 0;
                Player.manaRegenDelay = 100;
                Player.buffImmune[BuffID.ManaSickness] = true;
            }
        }
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player owner = Main.player[proj.owner];
            int celestialBladesCount = owner.ownedProjectileCounts[ModContent.ProjectileType<CelestialBlade>()];
            bool cfgActive = ModContent.GetInstance<MainConfig>().SkyFractureCelestialBlessing || NPC.downedGolemBoss;

            if (cfgActive && (proj.ModProjectile is CelestialBlade) || !isSkyBlessBurstActive)
            {
                float bonus = 0.04f * celestialBladesCount; // 4% * count
                modifiers.SourceDamage *= (1f + bonus);
            }
        }

        public override void ModifyWeaponDamage(Item item, ref StatModifier damage)
        {
            int celestialBladesCount = Player.ownedProjectileCounts[ModContent.ProjectileType<CelestialBlade>()];
            if (!isSkyBlessBurstActive)
            {
                if (item.type == ItemID.SkyFracture)
                {
                    // ТОЛЬКО бонус SkyFracture: +4% за клинок (не комбинируется с общим магическим бонусом)
                    damage += 0.04f * celestialBladesCount;
                }
                else if (item.DamageType == DamageClass.Magic)
                {
                    // Все остальные магические предметы получают общий бонус +2% за клинок
                    damage += 0.025f * celestialBladesCount;
                }
            }
            if (isSkyBlessBurstActive)
            {
                if (item.type == ItemID.SkyFracture)
                {
                    damage += 0.45f;
                }
            }
        }
        public override void ModifyManaCost(Item item, ref float reduce, ref float mult)
        {
            if (isSkyBlessBurstActive)
            {
                if (item.type == ItemID.SkyFracture)
                {
                    reduce -= 0.5f;
                }
                //else if (item.type == ModContent.ItemType<SkyBoundGrimoire>())
                //{
                //    reduce = Math.Max(reduce, 0f);
                //}
                else if (item.DamageType == DamageClass.Magic || item.DamageType == ModContent.GetInstance<MagicSupport>())
                {
                    reduce -= 0.2f;
                }
            }
        }

        public override float UseSpeedMultiplier(Item item)
        {
            if (isSkyBlessBurstActive)
            {
                if (item.type == ItemID.SkyFracture)
                {
                    return 2f;
                }
                //else if (item.type == ModContent.ItemType<SkyBoundGrimoire>())
                //{
                //    return 1.25f;
                //}
            }
            
            
            return 1f;
        }
        public override void ModifyWeaponKnockback(Item item, ref StatModifier knockback)
        {
            if (isSkyBlessBurstActive)
            {
                //if (item.type == ModContent.ItemType<SkyBoundGrimoire>())
                //{
                //    knockback *= 4f;
                //}
            }
        }
    }
}
