using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Ranged.DualBlades
{
    public class BoneBreakerThrow : ModProjectile
    {
        private int timer = 0;
        private int splitTime = 45; // Время до разделения

        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 42;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 0.05f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 120;
            Projectile.scale = 1f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X * 0.95f;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y * 0.95f;
            }

            return false;
        }

        public int HitCount;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (HitCount < 2)
            {
                HitCount++;
                splitTime += 10;
            }
        }

        public override void AI()
        {
            timer++;
            Projectile.rotation += 0.25f;
            visual();
            if (timer == splitTime && Main.myPlayer == Projectile.owner)
            {
                ProjectileUtils.DualBladesPattern_Split(Projectile,
                ModContent.ProjectileType<LivingBoneBlade>(), //First blade
                ModContent.ProjectileType<CrimtaneBlade>(), //Second blade
                0.5f, //First blade damage
                0.65f, //Second blade damage
                15f,   // Rotation
                6.5f    // Speed
                );

                Projectile.Kill();
            }
        }

        private void visual()
        {
            Projectile.rotation += 0.25f;
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.CrimtaneWeapons, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.ViciousPowder, 0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }
    }
}
