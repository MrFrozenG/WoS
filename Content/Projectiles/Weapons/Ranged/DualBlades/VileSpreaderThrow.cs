using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Ranged.DualBlades
{
    public class VileSpreaderThrow : ModProjectile
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
                ModContent.ProjectileType<VileBlade>(), //First blade
                ModContent.ProjectileType<DemoniteBlade>(), //Second blade
                0.4f, //First blade damage
                0.6f, //Second blade damage
                25f,   // Rotation
                8f    // Speed
                );

                Projectile.Kill();
            }
        }

        private void visual()
        {
            Projectile.rotation += 0.25f;
            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Demonite, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
            if (Main.rand.NextBool(4))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.VilePowder, 0, 0, 254, Scale: 0.3f);
                dust.velocity += Projectile.velocity * 0.5f;
                dust.velocity *= 0.5f;
            }
        }
    }
}


/*                Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.UnitX); 
    Vector2 leftOffset = direction.RotatedBy(MathHelper.ToRadians(-25)) * speed;
    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, leftOffset,
        ModContent.ProjectileType<VileBlade>(), DamageFirstBlade, Projectile.knockBack, Projectile.owner);

    Vector2 rightOffset = direction.RotatedBy(MathHelper.ToRadians(25)) * speed;
    Projectile.NewProjectileDirect(Projectile.GetSource_FromThis(), Projectile.Center, rightOffset,
        ModContent.ProjectileType<DemoniteBlade>(), DamageSecondBlade, Projectile.knockBack, Projectile.owner);
*/
