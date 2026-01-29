
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using System;
using Terraria.GameContent.Drawing;

namespace WoS.Content.Projectiles.Weapons.Melee.Swords
{
    public class HighTideSlash : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.AllowsContactDamageFromJellyfish[Type] = true;
            Main.projFrames[Type] = 4;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = 3;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true; 
            Projectile.ownerHitCheckDistance = 224f;
            Projectile.usesOwnerMeleeHitCD = true;

            Projectile.stopsDealingDamageAfterPenetrateHits = true;
            Projectile.aiStyle = -1;
            Projectile.noEnchantmentVisuals = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Projectile.localAI[0]++; // текущий "таймер жизни" 

            float percentage = Projectile.localAI[0] / Projectile.ai[1];
            float direction = Projectile.ai[0];

            // --- Вращение ---
            Projectile.rotation = direction * MathHelper.Pi * 2f * percentage + Projectile.velocity.ToRotation();

            // --- Вылет вперед, как у True Night's Edge / Terra Blade ---
            float travelDistance = 77f; // длина вылета ванили
            float progress = Utils.Remap(percentage, 0f, 1f, 0f, 1f);
            Projectile.Center = player.RotatedRelativePoint(player.MountedCenter) - Projectile.velocity + Projectile.velocity * progress * travelDistance;

            // --- Уменьшенный размер и плавный рост ---
            float baseScale = 0.6f; // половина стандартного
            float scaleGrowth = 0.35f;
            Projectile.scale = (baseScale + scaleGrowth * percentage) * Projectile.ai[2];

            // --- Всплески и частицы ---
            SpawnDustEffects();

            // --- Эффекты зачарования ---
            SpawnEnchantmentVisuals();

            // --- Завершение жизни ---
            if (Projectile.localAI[0] >= Projectile.ai[1])
                Projectile.Kill();
        }

        private void SpawnDustEffects()
        {
            // First dust type (colored)
            if (Main.rand.NextFloat() * 2f < Projectile.Opacity)
            {
                SpawnColoredDust();
            }

            // Second dust type (tinted)
            if (Main.rand.NextFloat() * 1.5f < Projectile.Opacity)
            {
                SpawnTintedDust();
            }
        }

        private void SpawnColoredDust()
        {
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 0.7f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

            // Цвета водных брызг
            Color dustColor = Color.Lerp(new Color(60, 180, 220), new Color(120, 230, 255), Main.rand.NextFloat() * 0.3f);
            Dust coloredDust = Dust.NewDustPerfect(
                Projectile.Center + dustRotation.ToRotationVector2() * (Main.rand.NextFloat() * 80f * Projectile.scale + 20f * Projectile.scale),
                DustID.Water, // Используем водную пыль
                dustVelocity * 1f,
                100,
                dustColor,
                0.4f
            );
            coloredDust.fadeIn = 0.4f + Main.rand.NextFloat() * 0.15f;
            coloredDust.noGravity = true;
        }

        private void SpawnTintedDust()
        {
            float dustRotation = Projectile.rotation + Main.rand.NextFloatDirection() * MathHelper.PiOver2 * 0.7f;
            Vector2 dustPosition = Projectile.Center + dustRotation.ToRotationVector2() * 84f * Projectile.scale;
            Vector2 dustVelocity = (dustRotation + Projectile.ai[0] * MathHelper.PiOver2).ToRotationVector2();

            // Водяная пыль с голубым оттенком
            Dust.NewDustPerfect(
                dustPosition,
                DustID.FishronWings, // Альтернативный водный эффект
                dustVelocity,
                100,
                new Color(80, 200, 240) * Projectile.Opacity,
                1.2f * Projectile.Opacity
            );
        }

        private void SpawnEnchantmentVisuals()
        {
            for (float i = -MathHelper.PiOver4; i <= MathHelper.PiOver4; i += MathHelper.PiOver2)
            {
                Rectangle rectangle = Utils.CenteredRectangle(
                    Projectile.Center + (Projectile.rotation + i).ToRotationVector2() * 70f * Projectile.scale,
                    new Vector2(60f * Projectile.scale, 60f * Projectile.scale)
                );
                Projectile.EmitEnchantmentVisualsAt(rectangle.TopLeft(), rectangle.Width, rectangle.Height);
            }
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            return CheckFirstCone(targetHitbox) || CheckSecondCone(targetHitbox);
        }

        private bool CheckFirstCone(Rectangle targetHitbox)
        {
            float coneLength = 94f * Projectile.scale;
            float rotationOffset = MathHelper.Pi * 2f / 25f * Projectile.ai[0];
            float maxAngle = MathHelper.PiOver4;
            float coneRot = Projectile.rotation + rotationOffset;

            return targetHitbox.IntersectsConeSlowMoreAccurate(
                Projectile.Center,
                coneLength,
                coneRot,
                maxAngle
            );
        }

        private bool CheckSecondCone(Rectangle targetHitbox)
        {
            float backSwing = Utils.Remap(Projectile.localAI[0], Projectile.ai[1] * 0.3f, Projectile.ai[1] * 0.5f, 1f, 0f);
            if (backSwing <= 0f) return false;

            float coneLength = 94f * Projectile.scale;
            float rotationOffset = MathHelper.Pi * 2f / 25f * Projectile.ai[0];
            float maxAngle = MathHelper.PiOver4;
            float coneRot = Projectile.rotation + rotationOffset - MathHelper.PiOver4 * Projectile.ai[0] * backSwing;

            return targetHitbox.IntersectsConeSlowMoreAccurate(
                Projectile.Center,
                coneLength,
                coneRot,
                maxAngle
            );
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 pos = Projectile.Center - Main.screenPosition;
            Texture2D tex = TextureAssets.Projectile[Type].Value;
            Rectangle frame = tex.Frame(1, 4);
            Vector2 origin = frame.Size() / 2f;
            float scale = Projectile.scale * 1.1f;
            SpriteEffects effects = Projectile.ai[0] >= 0f ? SpriteEffects.None : SpriteEffects.FlipVertically;

            float perc = Projectile.localAI[0] / Projectile.ai[1];
            float lerp = Utils.Remap(perc, 0f, 0.6f, 0f, 1f) * Utils.Remap(perc, 0.6f, 1f, 1f, 0f);
            float lightFactor = Lighting.GetColor(Projectile.Center.ToTileCoordinates()).ToVector3().Length() / (float)Math.Sqrt(3);
            lightFactor = Utils.Remap(lightFactor, 0.2f, 1f, 0f, 1f);

            // Цвета слоя
            Color back = new Color(150, 240, 255);
            Color mid = new Color(120, 220, 240);
            Color front = new Color(90, 200, 230);

            // Back
            Main.EntitySpriteDraw(tex, pos, frame, back * lightFactor * lerp, Projectile.rotation, origin, scale, effects, 0f);
            // Middle
            Main.EntitySpriteDraw(tex, pos, frame, mid * lightFactor * lerp * 0.3f, Projectile.rotation, origin, scale, effects, 0f);
            // Front
            Main.EntitySpriteDraw(tex, pos, frame, front * lightFactor * lerp * 0.5f, Projectile.rotation, origin, scale * 0.975f, effects, 0f);

            return false;
        }
       
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 300); 
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-5f, -2f));
                Dust waterDust = Dust.NewDustPerfect(
                    Main.rand.NextVector2FromRectangle(target.Hitbox),
                    DustID.Water,
                    velocity,
                    100,
                    new Color(60, 180, 220),
                    Main.rand.NextFloat(0.8f, 1.2f)
                );
                waterDust.noGravity = true;
                waterDust.fadeIn = 0.5f;
            }
            SetHitDirection(target, hit);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Wet, 300);
            for (int i = 0; i < 10; i++)
            {
                Vector2 velocity = new Vector2(Main.rand.NextFloat(-3f, 3f), Main.rand.NextFloat(-5f, -2f));
                Dust waterDust = Dust.NewDustPerfect(
                    Main.rand.NextVector2FromRectangle(target.Hitbox),
                    DustID.Water,
                    velocity,
                    100,
                    new Color(60, 180, 220),
                    Main.rand.NextFloat(0.8f, 1.2f)
                );
                waterDust.noGravity = true;
                waterDust.fadeIn = 0.5f;
            }
            SetHitDirection(target, info);
        }

        private void SetHitDirection(NPC target, NPC.HitInfo hit)
        {
            hit.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }

        private void SetHitDirection(Player target, Player.HurtInfo info)
        {
            info.HitDirection = (Main.player[Projectile.owner].Center.X < target.Center.X) ? 1 : (-1);
        }
    }
}
