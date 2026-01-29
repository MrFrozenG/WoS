using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Ranged.Bow
{
    public class SilverFalconShot : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 0.05f;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Ranged/Bow/SilverFalconShot_Glow").Value;
        }
        public override void AI()
        {
            ProjectileUtils.AI_HomingAtHighHP(Projectile);
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Explode();
        }
        private void Explode()
        {
            Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);

            Dust d = Dust.NewDustPerfect(Projectile.Center, DustID.BlueCrystalShard, speed * 5, Scale: 2.5f);
            d.noGravity = true;
        }
    }
}
