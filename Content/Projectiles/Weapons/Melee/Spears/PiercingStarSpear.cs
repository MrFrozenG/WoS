using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Melee.Spears
{
    public class PiercingStarSpear : ModProjectile, ICriticalDamageProvider
    {
        protected virtual float HoldoutRangeMin => 24f;
        protected virtual float HoldoutRangeMax => 124f;

        public float BaseCriticalDamage => 0f;

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Trident);
            Projectile.friendly = true;
            Projectile.penetrate = 5;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.extraUpdates = 1;
            AIType = ProjectileID.Trident;
            Projectile.stopsDealingDamageAfterPenetrateHits = true;
        }

        public override void AI()
        {
            ProjectileUtils.SpearBasePreAI(Projectile, HoldoutRangeMin, HoldoutRangeMax);
            VisualUtils.StarryProjectilesVisual(Projectile);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Projectile.penetrate <= 1)
            {
                Projectile.CritChance = 100;
            }
            if (Projectile.penetrate > 1)
            {
                Projectile.CritChance += 5;
            }
            
        }
    }
}
