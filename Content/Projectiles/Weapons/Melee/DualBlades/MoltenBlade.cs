using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Melee.DualBlades
{
    public class MoltenBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 32;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.15f;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 95;
            Projectile.scale = 0.9f;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Melee/DualBlades/MoltenBlade_glow").Value;
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
            target.AddBuff(BuffID.OnFire, 360, false);
            HasHit = true;
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
            Projectile.velocity = direction * 24f;
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
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Obsidian, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
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

        public override bool PreDraw(ref Color lightColor)
        {
            return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
        }
    }
}
