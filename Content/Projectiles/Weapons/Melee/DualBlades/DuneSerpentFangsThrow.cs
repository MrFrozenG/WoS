using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WoS.Content.Projectiles.Weapons.Melee.DualBlades
{
    public class DuneSerpentFangsThrow : ModProjectile
    {
        private const float MaxDistance = 150f; 
        private const int SpinDuration = 210; 

        private Vector2 StartPosition;

        private bool IsSpinning
        {
            get => Projectile.ai[0] == 1f;
            set => Projectile.ai[0] = value ? 1f : 0f;
        }

        private int SpinTimer
        {
            get => (int)Projectile.localAI[0];
            set => Projectile.localAI[0] = value;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.DontAttachHideToAlpha[Type] = true;
        }

        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 1200;
            Projectile.knockBack = 3.5f;

            Projectile.localNPCHitCooldown = 30;
        }

        public override void AI()
        {
            if (Projectile.localAI[1] == 0f)
            {
                StartPosition = Projectile.Center;
                Projectile.localAI[1] = 1f;
            }

            if (!IsSpinning)
            {
                Projectile.rotation = Projectile.velocity.ToRotation();

                if (Vector2.Distance(StartPosition, Projectile.Center) >= MaxDistance)
                    StartSpin();
            }
            else
            {
                SpinTimer++;
                Projectile.rotation += 0.3f;

                if (SpinTimer >= SpinDuration)
                    Projectile.Kill();
            }
        }

        private void StartSpin()
        {
            IsSpinning = true;
            Projectile.velocity = Vector2.Zero;
            SpinTimer = 0;

            Projectile.Center = Projectile.Center;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            if (!IsSpinning)
                StartSpin();
            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Type].Value;

            Vector2 origin = texture.Size() * 0.5f; // ← делим спрайт пополам
            Vector2 drawPos = Projectile.Center - Main.screenPosition;

            Main.EntitySpriteDraw(
                texture,
                drawPos,
                null,
                lightColor,
                Projectile.rotation,
                origin,
                Projectile.scale,
                SpriteEffects.None,
                0
            );

            return false; // отключаем стандартную отрисовку
        }
    }
}
