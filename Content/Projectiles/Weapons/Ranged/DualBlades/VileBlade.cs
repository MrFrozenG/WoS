using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Ranged.DualBlades
{
    public class VileBlade : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.05f;
            Projectile.penetrate = -1;
            Projectile.scale = 1.15f;
        }
        private int count;
        private bool HasHit;
        public override void AI()
        {
            Projectile.tileCollide = true;
            count++;
            visual();
            if (count > 60 || HasHit)
            {
                Projectile.tileCollide = false;
                Return();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (!HasHit)
            {
                HasHit = true;
            }
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.Poisoned, Projectile.damage*2);
            }
            if (Main.rand.NextBool(2))
            {
                target.AddBuff(BuffID.ShadowFlame, Projectile.damage * 2);
            }
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
        private void Return()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 direction = player.Center - Projectile.Center;
            direction.Normalize();
            Projectile.velocity = direction * 16f; // Возвращаем бумеранг
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
