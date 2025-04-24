using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Ranged.Throw
{
    public class BoneKnifeShard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }

        public override void SetDefaults()
        {
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
        }

        private const int GravityDelay = 25;

        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.35f;
            }
        }

        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
            Vector2 usePos = Projectile.position;

            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Bone);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += usePos * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= usePos * 8f;
            }
        }

    }
}
