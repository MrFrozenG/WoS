using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using WoS.Content.Systems.DamageClasses;
using System;
using WoS.Content.Projectiles.Weapons.Accessories;
using WoS.Content.Systems.Globals.GlobalItems;

namespace WoS.Content.ModPlayers
{
    public class MainPlayer : ModPlayer
    {
        public int SupportPointsBonus = 0;
        public float SupportPointsBonus2 = 1f;

        public float CritDamageBonus = 0f;
        //Magic accessories
        public bool SilverGoblet;
        public bool GoldenGoblet;

        //Melee accessories
        public bool HoVHairpin;

        //Ranged accessories

        //Summon accessories
        public bool PossessedSkull;

        //Support accessories

        //Universal accessories
        public bool HeartofSin;

        //Armor 

        //Weapons
        public int DualBladesMoltenDefense;

        public override void UpdateDead()
        {
            DualBladesMoltenDefense = 0;
        }
        public override void PreUpdate()
        {
            if (DualBladesMoltenDefense > 0)
            {
                DualBladesMoltenDefense--;
            }
        }

        public override void PostUpdate()
        {
            if (HeartofSin)
            {
//                Player.statLifeMax2 += 50;
            }
        }
        public override void ResetEffects()
        {
            //Magic
            SilverGoblet = false;
            GoldenGoblet = false;

            //Melee
            HoVHairpin = false;

            //Forbidden

            //Summon
            PossessedSkull = false;

            //Armor

            //Universal
            HeartofSin = false;

            //Support
            SupportPointsBonus = 0;
            SupportPointsBonus2 = 1f;
        }

        public int GetModifiedSupportPoints(int baseValue)
        {
            return (int)((baseValue + SupportPointsBonus) * SupportPointsBonus2);
        }

        public override void UpdateEquips()
        {
            int PercLife = Player.statLifeMax2 / 100;
            if (SilverGoblet)
            {
                Player.GetDamage(DamageClass.Magic).Base += 3f;
                Player.GetDamage(DamageClass.Summon).Base += 3f;
                Player.statManaMax2 += 10;
                Player.manaCost -= 0.03f;
            }

            if (GoldenGoblet)
            {
                Player.GetDamage(DamageClass.Magic).Base += 2f;
                Player.GetDamage(DamageClass.Summon).Base += 2f;
                Player.statManaMax2 += 10;
            }

            if (HeartofSin)
            {
                Player.statLifeMax2 += PercLife * 35;
                Player.statLifeMax2 += 50;
                Player.GetCritChance(ModContent.GetInstance<ForbiderDamage>()) += 4;
                Player.GetDamage(ModContent.GetInstance<ForbiderDamage>()) += 0.1f;
                if (Player.ZoneCorrupt || Player.ZoneCrimson || Player.ZoneUnderworldHeight)
                {
                    Player.lifeRegen += 1;
                }
                if (Player.ZonePurity)
                {
                    Player.moveSpeed -= 0.07f;
                }
            }

            if (PossessedSkull)
            {
                Player.GetDamage(DamageClass.Summon) += 0.05f;
                Player.aggro -= 100;
            }

            if (HoVHairpin)
            {
                Player.GetDamage(DamageClass.Melee) += 0.02f;
                Player.GetDamage(DamageClass.Ranged) += 0.02f;
            }
        }
        public override void PostUpdateEquips()
        {
        }

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (PossessedSkull && ProjectileID.Sets.IsAWhip[proj.type])
                {
                int MysteriousSkullCount = 0;
                foreach (Projectile p in Main.projectile)
                {
                    if (p.active && p.owner == Player.whoAmI && p.type == ModContent.ProjectileType<MysteriousSkull>())
                        MysteriousSkullCount++;
                }

                if (MysteriousSkullCount < 3)
                {
                    Projectile.NewProjectile(
                     Player.GetSource_FromThis(),
                     target.Center + new Vector2(0, -60f),
                     Vector2.Zero,
                     ModContent.ProjectileType<MysteriousSkull>(),
                     0, 0f, Player.whoAmI,
                     target.whoAmI
                    );
                }
            }
        }

        /*
        ### OLD

        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (CryotiumSetMagicSummon && proj.IsMinionOrSentryRelated)
            {
                if (Main.rand.NextBool(4))
                    target.AddBuff(BuffID.Frostburn2, 360);
                else
                    target.AddBuff(BuffID.Frostburn, 120);
            }
            if (FrostStone && proj.CountsAsClass(DamageClass.Melee))
            {
                if (Main.rand.NextBool(4))
                    target.AddBuff(BuffID.Frostburn2, 360);
                else if (Main.rand.NextBool(2))
                    target.AddBuff(BuffID.Frostburn2, 240);
                else
                    target.AddBuff(BuffID.Frostburn2, 120);
            }
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
           if (DeepIceCrystal && proj.type != ModContent.ProjectileType<DeepIceCrystal_Shards>() && !proj.IsMinionOrSentryRelated)
            {
                IceCrystalsSummonProj(target, damageDone, proj);
            }

        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (DeepIceCrystal)
            {
                IceCrystalsSummonMelee(target, damageDone);
            }
        }
        private void IceCrystalsSummonProj(NPC target, int damageDone, Projectile proj)
        {
            Player owner = Main.player[proj.owner];
            int numberOfBlades = Main.rand.Next(1, 5);
            if (IceCrystalsCooldown == 0)
            {
                for (int i = 0; i < numberOfBlades; i++)
                {
                    Vector2 spawnPosition = Player.Center + new Vector2(Main.rand.NextFloat(-30, 30), Main.rand.NextFloat(-58, 58));
                    Vector2 direction = (target.Center - spawnPosition).SafeNormalize(Vector2.UnitX) * 7f;
                    int IceCrystalsShards = ModContent.ProjectileType<DeepIceCrystal_Shards>();
                    Projectile.NewProjectileDirect(owner.GetSource_FromThis(), spawnPosition, direction * 1.2f, IceCrystalsShards, 30 + (damageDone / 3), 0.1f, Player.whoAmI);
                    IceCrystalsCooldown = 120;
                }
            }
        }
        private void IceCrystalsSummonMelee(NPC target, int damageDone)
        {
            Player owner = Main.player[Player.whoAmI];
            int numberOfBlades = Main.rand.Next(1, 5);
            if (IceCrystalsCooldown == 0)
            {
                for (int i = 0; i < numberOfBlades; i++)
                {
                    Vector2 spawnPosition = Player.Center + new Vector2(Main.rand.NextFloat(-30, 30), Main.rand.NextFloat(-58, 58));
                    Vector2 direction = (target.Center - spawnPosition).SafeNormalize(Vector2.UnitX) * 7f;
                    int IceCrystalsShards = ModContent.ProjectileType<DeepIceCrystal_Shards>();
                    Projectile projectile = Projectile.NewProjectileDirect(owner.GetSource_FromThis(), spawnPosition, direction * 1.1f, IceCrystalsShards, 30 + (damageDone / 3), 0.1f, Player.whoAmI);
                    projectile.CritChance += 15;
                    IceCrystalsCooldown = 100;
                }
            }
        }

                public override void MeleeEffects(Item item, Rectangle hitbox)
        {
            base.MeleeEffects(item, hitbox);
            if (FrostStone && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic && Main.rand.NextBool(3))
            {
                int index = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, item.velocity.X * 0.2f + (float)(item.direction * 3), item.velocity.Y * 0.2f, 100, Scale: 2.5f);
                Main.dust[index].noGravity = true;
                Main.dust[index].velocity.X *= 2f;
                Main.dust[index].velocity.Y *= 2f;
            }
        }
        public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (FrostStone && item.CountsAsClass(DamageClass.Melee))
            {
                if (Main.rand.NextBool(4))
                    target.AddBuff(BuffID.Frostburn2, 360);
                else if (Main.rand.NextBool(2))
                    target.AddBuff(BuffID.Frostburn2, 240);
                else
                    target.AddBuff(BuffID.Frostburn, 120);
            }
        }
    */
    }
}
