using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.DamageClasses;

namespace WoS.Content.Projectiles.Ammo
{
    public class MoltenNailS : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.NailFriendly);
            Projectile.width = Projectile.height = 10;
            Projectile.aiStyle = 93;
            Projectile.friendly = true;
            Projectile.penetrate = 3;
            Projectile.DamageType = DamageClass.Ranged;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire3, 240);
            target.AddBuff(BuffID.OnFire, 240);
        }

        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }

        public override void OnKill(int timeLeft)
        {
            Projectile Proj = Projectile.NewProjectileDirect(
                Projectile.GetSource_FromAI(),
                Projectile.Center,
                Projectile.velocity,
                ProjectileID.NailFriendly,
                Projectile.damage + 5,
                Projectile.knockBack,
                Projectile.owner);
            Proj.alpha = 255;
            Proj.timeLeft = 20;
            if (Projectile.DamageType == ModContent.GetInstance<ForbiderDamage>())
            {
                Proj.DamageType = ModContent.GetInstance<ForbiderDamage>();
            }
        }
    }
}
