using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Weapons.Magic.Staffs
{
    public class CoralFragments : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.penetrate = 1;

            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.timeLeft = 240;
            CoralFragmentType = Main.rand.Next(0, 4);
        }
        private int CoralFragmentType;
        private const int GravityDelay = 65;
        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public override void AI()
        {
            Projectile.frame = CoralFragmentType;
            if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.35f;
            }
            Projectile.rotation += Projectile.velocity.X * 0.05f;
        }
    }
}
