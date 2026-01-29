using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Weapons.Vanila
{
    public class CelestialBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 38;
            Projectile.height = 56;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = -1; 
            Projectile.timeLeft = 1200; 
            Projectile.tileCollide = false; 
            Projectile.ignoreWater = true;
           
        }
        public Vector2 target;
        public Vector2 storedVelocity = Vector2.Zero; // СТОЛЬКО нужно
        public bool activated = false;
        public bool initializedVelocity = false; // чтобы гарантированно один раз применить storedVelocity
        public override void AI()
        {
            if (!activated)
            {
                Projectile.velocity = Vector2.Zero;
                Projectile.rotation = 0f;
                Projectile.friendly = false;
                Projectile.netUpdate = true;
                return;

            }

            // Если активирован, устанавливаем скорость один раз (на случай, если её не поставили в CanUseItem)
            if (!initializedVelocity)
            {
                if (storedVelocity == Vector2.Zero)
                {
                    // запасной вариант: вычислим направление один раз
                    Vector2 dir = target - Projectile.Center;
                    if (dir != Vector2.Zero) dir.Normalize();
                    storedVelocity = dir * 3.4f;
                }

                Projectile.velocity = storedVelocity;
                initializedVelocity = true;
                Projectile.netUpdate = true;
            }
            // Держим постоянную скорость (не переопределяем каждый тик)
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.friendly = true;
            Projectile.netUpdate = true;
        }
    }
}
