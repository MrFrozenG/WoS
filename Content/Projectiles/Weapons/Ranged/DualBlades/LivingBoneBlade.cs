using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Ranged.DualBlades
{
    public class LivingBoneBlade : ModProjectile
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
            Projectile.scale = 0.9f;
        }
        private int count;
        private bool HasHit;
        private int HitCount = 0;
        public override void AI()
        {
            Projectile.tileCollide = true;
            count++;
            visual();
            if (count > 100 || HasHit)
            {
                Projectile.tileCollide = false;
                Return();
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            HasHit = true;
            HitCount++;
            Player owner = Main.player[Projectile.owner];
            int percLife75 = owner.statLifeMax2 / 100 * 75;
            if (owner.statLife <= percLife75 && HitCount < 4)
            {
                owner.Heal(5);
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
            Projectile.velocity = direction * 16f;
            if (Vector2.Distance(Projectile.Center, player.Center) < 10f)
            {
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
