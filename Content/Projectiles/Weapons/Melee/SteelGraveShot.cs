using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Weapons.Ranged.Throw;

namespace WoS.Content.Projectiles.Weapons.Melee
{
    public class SteelGraveShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 9;
        }
        int CurrentFrame;
        public override void SetDefaults()
        {
            Projectile.height = Projectile.width = 32;
            Projectile.aiStyle = -1;
            Projectile.friendly = true;
            Projectile.penetrate = 2;
            Projectile.tileCollide = true;
            Projectile.scale = 1f;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.timeLeft = 360;
            CurrentFrame = Main.rand.Next(0, 10);
        }
        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        private const int GravityDelay = 45;

        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation += 0.1f;
            Projectile.frame = CurrentFrame;
            if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.35f;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound

            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(oldVelocity, Projectile.width, Projectile.height, DustID.Stone);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += oldVelocity * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                oldVelocity -= oldVelocity * 8f;
            }
            return true;
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
            Vector2 usePos = Projectile.position;

            for (int i = 0; i < 20; i++)
            {
                Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Stone);
                dust.position = (dust.position + Projectile.Center) / 2f;
                dust.velocity += usePos * 2f;
                dust.velocity *= 0.5f;
                dust.noGravity = true;
                usePos -= usePos * 8f;
            }
        }
    }
}
