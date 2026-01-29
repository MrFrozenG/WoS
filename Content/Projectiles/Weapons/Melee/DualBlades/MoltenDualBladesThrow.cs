using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Melee.DualBlades
{
    public class MoltenDualBladesThrow : ModProjectile
    {
        public bool AlterUsage = false;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 9;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.1f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Melee/DualBlades/MoltenDualBladesThrow_glow").Value;
        }
        private int count;
        private int hits;
        public override void AI()
        {
            Projectile.tileCollide = true;
            count++;
            visual();
            if (count > 100)
            {
                Projectile.tileCollide = false;
                Return();
            }
            if (hits >= 3)
            {
                Projectile.tileCollide = false;
                Return();
            }
            if (AlterUsage)
            {
                Projectile.Kill();
            }
        }

        private void Return()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = player.Center - Projectile.Center;
            direction.Normalize();
            Projectile.velocity = direction * 18f; // Возвращаем бумеранг
            if (Vector2.Distance(Projectile.Center, player.Center) < 10f)
            {
                Projectile.Kill(); // Убираем снаряд после достижения игрока
            }
        }
        private void visual()
        {
            Projectile.rotation += 0.25f;
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Lava, 0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            hits++;
            Projectile.velocity *= 1.1f;
            target.AddBuff(BuffID.OnFire, 120);
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            // Если снаряд сталкивается с плитой, он будет отскакивать
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.95f; // Отскок по оси X (уменьшаем скорость на 30%)
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.95f; // Отскок по оси Y (уменьшаем скорость на 30%)
            }

            return false; // Возвращаем false, чтобы не уничтожать снаряд при столкновении с плитой
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
        }

        public override void Kill(int timeLeft)
        {
            if (AlterUsage && Main.myPlayer == Projectile.owner)
            {
                // Создаем два снаряда
                ProjectileUtils.DualBladesPattern_SplitWithoutKill(Projectile,
                ModContent.ProjectileType<ObsidianBlade>(), //First blade
                ModContent.ProjectileType<MoltenBlade>(), //Second blade
                0.3f, //First blade damage
                0.6f, //Second blade damage
                90f,   // Rotation
                8.5f    // Speed
                );
                ProjectileUtils.DualBladesPattern_SplitWithoutKill(Projectile,
                ModContent.ProjectileType<ObsidianBlade>(), //First blade
                ModContent.ProjectileType<MoltenBlade>(), //Second blade
                0.3f, //First blade damage
                0.6f, //Second blade damage
                45f,   // Rotation
                8.5f    // Speed
                );
            }
        }
    }
}
