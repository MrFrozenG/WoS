using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.AED.Magic
{
    public class AsteroidsExplosion : ModProjectile
    {
        private int hitsDone = 0; // Счётчик нанесённых ударов
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 7;
        }
        public override void SetDefaults()
        {
            Projectile.width = 98;
            Projectile.height = 98;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 3;
            Projectile.timeLeft = 21; 
            Projectile.aiStyle = -1; 
            Projectile.usesLocalNPCImmunity = false; 
            Projectile.localNPCHitCooldown = 40;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.scale = 0.5f;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/AED/Magic/AsteroidsExplosion_Glow").Value;
        }
        public override void AI()
        {
            UpdateHitboxWithScale();
            AnimateExplosion();
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        { 
            if (Main.rand.NextBool(3))
            {
                target.AddBuff(BuffID.OnFire3, 600);
            }
        }

        private void UpdateHitboxWithScale()
        {
            // Центрируем хитбокс при изменении размера
            int newWidth = (int)(98 * Projectile.scale);
            int newHeight = (int)(98 * Projectile.scale);

            // Сохраняем центр, чтобы снаряд не "скакал"
            Vector2 center = Projectile.Center;
            Projectile.width = newWidth;
            Projectile.height = newHeight;
            Projectile.Center = center;
        }
        private void AnimateExplosion()
        {
            Projectile.frameCounter++;

            if (Projectile.frameCounter >= 3) // скорость смены кадров (3 тика на кадр)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Projectile.type])
                {
                    Projectile.Kill(); // если последний кадр — уничтожаем снаряд
                }
            }
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            hitsDone++;
            if (target.realLife != -1)
            {
                modifiers.FinalDamage *= 0.5f;
            }
            if (hitsDone > 3)
            {
                modifiers.FinalDamage *= 0.25f;
                modifiers.Knockback *= 0f;
            }
        }
    }
}
