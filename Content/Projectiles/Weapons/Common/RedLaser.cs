using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Common
{
    public class RedLaser : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 0.1f;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Bullet;
            Projectile.ArmorPenetration = 8;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            // Ярко-красный лазер, полностью игнорирующий затемнение
            return new Color(255, 80, 80, 255);
        }
        public override void OnKill(int timeLeft)
        {
            Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        }
        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
    }
}
