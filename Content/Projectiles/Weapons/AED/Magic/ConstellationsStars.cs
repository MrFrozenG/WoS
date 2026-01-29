using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Audio;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.AED.Magic
{
    public class ConstellationsStars : ModProjectile
    {
        private const int DelayTicks = 30; // 0.25 секунды
        private bool startedFalling = false;
        private int spawnTimer = 0;

        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.scale = 0.5f;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 300;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/AED/Magic/ConstellationsStars_Glow").Value;
        }
        public override void AI()
        {
            ProjectileUtils.AI_NoCeiling(Projectile);
            spawnTimer++;

            if (Projectile.ai[0]++ < DelayTicks)
                InitPhase();
            else
                FallingPhase();

            UpdateLighting();
            ProjectileUtils.HomingAtClosestNPC_Simple(Projectile, 18, 0, 120, 12);
            EmitDust();
        }

        private void InitPhase()
        {
            Projectile.velocity *= 0.9f;
        }

        private void FallingPhase()
        {
            if (!startedFalling)
            {
                startedFalling = true;
                float angleOffset = MathHelper.ToRadians(Main.rand.NextFloat(-3f, 3f));
                float speed = Main.rand.NextFloat(7f, 8f);
                Vector2 dir = Vector2.UnitY.RotatedBy(angleOffset);
                Projectile.velocity = dir * speed;
            }
        }
        private void UpdateLighting()
        {
            Lighting.AddLight(Projectile.Center, 0.3f, 0.45f, 0.9f);
        }

        private void EmitDust()
        {
            if (Main.rand.NextBool(8))
            {
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueCrystalShard,
                    Projectile.velocity.X * 0.2f, Projectile.velocity.Y * 0.2f, 150, default, 1.1f);
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 12; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.MagicMirror, vel.X, vel.Y, 150, default, 1.3f);
            }
            SoundEngine.PlaySound(SoundID.Item10, Projectile.position);

            return true;
        }

        public override void OnKill(int timeLeft)
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, vel.X, vel.Y, 150, default, 1.2f);
            }
        }

        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return true;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            for (int i = 0; i < 6; i++)
            {
                Vector2 vel = Main.rand.NextVector2Circular(2f, 2f);
                Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Enchanted_Gold, vel.X, vel.Y, 150, default, 1.2f);
            }
        }

        public override void PostDraw(Color lightColor)
        {
            Projectile.netUpdate = true;
            float time = Main.GlobalTimeWrappedHourly % 1f;

            // Основная сияющая звезда
            VisualUtils.DrawPrettyStarSparkle(
                opacity: 1f,
                dir: SpriteEffects.None,
                drawPos: Projectile.Center - Main.screenPosition,
                drawColor: Color.White,
                shineColor: Color.Gold,
                flareCounter: time,
                fadeInStart: 0f,
                fadeInEnd: 0.1f,
                fadeOutStart: 0.9f,
                fadeOutEnd: 1f,
                rotation: Main.GlobalTimeWrappedHourly * MathHelper.TwoPi,
                scale: new Vector2(1.2f),
                fatness: new Vector2(1f, 2f)
            );
        }
    }
}

