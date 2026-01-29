using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Weapons.Magic.Spellbooks
{
    public class SnowfallSnowflakes : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.NorthPoleSnowflake}";
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;

            Projectile.tileCollide = true;
            Projectile.ignoreWater = false;

            Projectile.timeLeft = 240;
            Projectile.alpha = 40;
        }
        private const int GravityDelay = 35;
        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public override void AI()
        {
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.35f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.05f;

            if (Projectile.velocity.Y > 0.2f)
            {
                const float pushRadius = 16f;
                const float pushStrength = 0.04f;

                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile other = Main.projectile[i];

                    if (!other.active || other.whoAmI == Projectile.whoAmI)
                        continue;

                    if (other.type != Projectile.type)
                        continue;

                    float distance = Vector2.Distance(Projectile.Center, other.Center);
                    if (distance < pushRadius && distance > 0f)
                    {
                        Vector2 pushDir = Projectile.Center - other.Center;
                        pushDir.Normalize();

                        Projectile.velocity += pushDir * pushStrength;
                    }
                }
            }
        }
        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust.NewDust(
                    Projectile.position,
                    Projectile.width,
                    Projectile.height,
                    DustID.SnowflakeIce,
                    Projectile.velocity.X * 0.2f,
                    Projectile.velocity.Y * 0.2f
                );
            }
        }
    }
}
