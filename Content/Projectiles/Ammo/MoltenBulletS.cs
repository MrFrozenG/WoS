using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Ammo
{
    public class MoltenBulletS : ModProjectile
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
            Projectile.light = 0.25f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
        }

        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            Lighting.AddLight(
                Projectile.Center,
                1.0f, 0.5f, 0.15f
            );
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Lighting.AddLight(
                Projectile.Center,
                1.0f, 0.5f, 0.15f
            );
            target.AddBuff(BuffID.OnFire, 120);
        }
        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
            Lighting.AddLight(
                Projectile.Center,
                0.8f, 0.4f, 0.1f // тёплый огненный оттенок
            );
        }
    }
}
