using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.AED.Magic
{
    public class Asteroids : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = false;
            Projectile.tileCollide = true;
            Projectile.light = 0.3f;
            Projectile.penetrate = 2;
            Projectile.timeLeft = 360;
            Projectile.aiStyle = -1;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/AED/Magic/Asteroids_Glow").Value;

            // Угол падения немного отклоняется от вертикали
            float angle = MathHelper.ToRadians(Main.rand.NextFloat(-5f, 5f));
            float speed = Main.rand.NextFloat(6f, 7f);
            Projectile.velocity = Vector2.UnitY.RotatedBy(angle) * speed;
        }

        public override void OnSpawn(IEntitySource source)
        {
            Projectile.frame = Main.rand.Next(0, 6);
        }
        public override void AI()
        {
            EmitTails(); 
            ProjectileUtils.AI_NoCeiling(Projectile);
        }

        private void EmitTails()
        {
            // Первый хвост: V-образный (по бокам)
            for (int i = 0; i < 2; i++)
            {
                Vector2 offset = Projectile.velocity.RotatedBy(MathHelper.PiOver4 * (i == 0 ? 1 : -1)) * Main.rand.NextFloat(0.2f, 0.5f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, offset.X, offset.Y, 150, default, 0.7f);
            }

            // Второй хвост: I-образный, по направлению снаряда
            Vector2 tailDir = -Projectile.velocity.SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(0.5f, 1.2f);
            Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, tailDir.X, tailDir.Y, 150, default, 1.2f);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.penetrate == 2)
            {
                SmallExplosion(Projectile);
                if (target.boss) 
                {
                    BossExplosion(Projectile);
                }
            }
            else
            {
                BigExplosion(Projectile);
            }   
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SmallExplosion(Projectile);
            return true;
        }

        private static void SmallExplosion(Projectile projectile)
        {
            Projectile proj = Projectile.NewProjectileDirect(
                    projectile.GetSource_FromThis(),
                projectile.Center,
                Vector2.Zero,
                ModContent.ProjectileType<AsteroidsExplosion>(),
                projectile.damage / 3,
                0f,
                projectile.owner
            );
            proj.scale = 1.25f;
            proj.penetrate = 3;
        }

        private static void BigExplosion(Projectile projectile)
        {
            Projectile proj = Projectile.NewProjectileDirect(
                projectile.GetSource_FromThis(),
                projectile.Center,
                Vector2.Zero,
                ModContent.ProjectileType<AsteroidsExplosion>(),
                projectile.damage / 2,
                1f, 
                projectile.owner
            );
            proj.scale = 2.15f;
            proj.penetrate = 6;
        }

        private static void BossExplosion(Projectile projectile)
        {
            Projectile proj = Projectile.NewProjectileDirect(
                projectile.GetSource_FromThis(),
                projectile.Center,
                Vector2.Zero,
                ModContent.ProjectileType<AsteroidsExplosion>(),
                projectile.damage,
                5f,
                projectile.owner
            );
            proj.scale = 2.5f;
            proj.penetrate = 1;
            proj.CritChance += 100;
        }
    }
}
