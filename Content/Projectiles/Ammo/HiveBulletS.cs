using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Ammo
{
    public class HiveBulletS : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.extraUpdates = 1;
            Projectile.light = 0.1f;
            AIType = ProjectileID.Bullet;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.LocalPlayer;
            for (int b = 0; b < Main.rand.Next(1, 6); b++)
            {
                Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(3));
                newVelocity *= 0.6f - Main.rand.NextFloat(0.2f);
                Projectile Proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, newVelocity, ProjectileID.Bee, 10, Projectile.knockBack, Projectile.owner);
                Proj.penetrate = 3;
                Proj.ArmorPenetration = 5;
                Proj.DamageType = DamageClass.Ranged;
                Proj.timeLeft /= 2;
                if (player.strongBees && Main.rand.NextBool(3))
                {
                    Projectile ProjA = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, newVelocity, ProjectileID.GiantBee, 15, Projectile.knockBack, Projectile.owner);
                    ProjA.penetrate = 3;
                    ProjA.ArmorPenetration = 7;
                    ProjA.DamageType = DamageClass.Ranged;
                    ProjA.timeLeft /= 2;
                }
            }
            return base.OnTileCollide(oldVelocity);
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.LocalPlayer;
            for (int b = 0; b < Main.rand.Next(1, 6); b++)
            {
                Vector2 newVelocity = Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(3));
                newVelocity *= 0.6f - Main.rand.NextFloat(0.2f);
                Projectile Proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.Center, newVelocity, ProjectileID.Bee, 10, Projectile.knockBack, Projectile.owner);
                Proj.penetrate = 3;
                Proj.ArmorPenetration = 5;
                Proj.DamageType = DamageClass.Ranged;
                Proj.timeLeft /= 2;
                if (player.strongBees && Main.rand.NextBool(3))
                {
                    Projectile ProjA = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), target.Center, newVelocity, ProjectileID.Bee, 15, Projectile.knockBack, Projectile.owner);
                    ProjA.penetrate = 3;
                    ProjA.ArmorPenetration = 7;
                    ProjA.DamageType = DamageClass.Ranged;
                    ProjA.timeLeft /= 2;
                }
            }
        }
        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}
