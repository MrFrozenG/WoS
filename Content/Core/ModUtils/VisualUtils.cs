using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.GameContent;
using Terraria;
using Terraria.ID;

namespace WoS.Content.Core.ModUtils
{
    internal class VisualUtils
    {
        public static void DrawPrettyStarSparkle(float opacity, SpriteEffects dir, Vector2 drawPos, Color drawColor, Color shineColor, float flareCounter, float fadeInStart, float fadeInEnd, float fadeOutStart, float fadeOutEnd, float rotation, Vector2 scale, Vector2 fatness)
        {
            Texture2D sparkleTexture = TextureAssets.Extra[98].Value;
            Color bigColor = shineColor * opacity * 0.5f;
            bigColor.A = 0;
            Vector2 origin = sparkleTexture.Size() / 2f;
            Color smallColor = drawColor * 0.5f;
            float lerpValue = Utils.GetLerpValue(fadeInStart, fadeInEnd, flareCounter, clamped: true) * Utils.GetLerpValue(fadeOutEnd, fadeOutStart, flareCounter, clamped: true);
            Vector2 scaleLeftRight = new Vector2(fatness.X * 0.5f, scale.X) * lerpValue;
            Vector2 scaleUpDown = new Vector2(fatness.Y * 0.5f, scale.Y) * lerpValue;
            bigColor *= lerpValue;
            smallColor *= lerpValue;
            // Bright, large part
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, bigColor, MathHelper.PiOver2 + rotation, origin, scaleLeftRight, dir);
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, bigColor, 0f + rotation, origin, scaleUpDown, dir);
            // Dim, small part
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, smallColor, MathHelper.PiOver2 + rotation, origin, scaleLeftRight * 0.6f, dir);
            Main.EntitySpriteDraw(sparkleTexture, drawPos, null, smallColor, 0f + rotation, origin, scaleUpDown * 0.6f, dir);
        }

        public static void StarryProjectilesVisual(Projectile Projectile)
        {
            if (Main.rand.NextBool(5))
            {
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Enchanted_Pink, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f);
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.Enchanted_Gold, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f); 
                Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height,
                    DustID.MagicMirror, Projectile.velocity.X * 0.5f, Projectile.velocity.Y * 0.5f, 150, default, 1.2f);
            }
            // Создаем эффект не каждый тик, чтобы не засорять сцену
            if (Main.rand.NextBool(2))
            {
                Vector2 direction = Projectile.velocity.SafeNormalize(Vector2.Zero);
                Vector2 pos = Projectile.Center;

                // Определяем два направления под углом к движению
                float angle = MathHelper.ToRadians(25f); // угол между "хвостами"
                Vector2 dirLeft = direction.RotatedBy(angle) * -1f;  // влево-назад
                Vector2 dirRight = direction.RotatedBy(-angle) * -1f; // вправо-назад

                // Смещения точек появления частиц — чуть позади копья
                Vector2 basePos = pos - direction * 8f;

                // Создаём по одной-две частицы на каждую "ветку"
                for (int i = 0; i < 2; i++)
                {
                    Vector2 offset = dirLeft * (i * 3f) + Main.rand.NextVector2Circular(1f, 1f);
                    Dust dustL = Dust.NewDustPerfect(basePos + offset, DustID.Enchanted_Pink,
                        dirLeft * 2f + Main.rand.NextVector2Circular(0.5f, 0.5f), 150, default, 1.1f);
                    dustL.noGravity = true;

                    offset = dirRight * (i * 3f) + Main.rand.NextVector2Circular(1f, 1f);
                    Dust dustR = Dust.NewDustPerfect(basePos + offset, DustID.Enchanted_Gold,
                        dirRight * 2f + Main.rand.NextVector2Circular(0.5f, 0.5f), 150, default, 1.1f);
                    dustR.noGravity = true;
                }

                // Можно добавить мелкие "искры" в центр
                if (Main.rand.NextBool(4))
                {
                    Dust d = Dust.NewDustPerfect(basePos, DustID.YellowStarDust, -direction * 1.5f, 200, default, 1.2f);
                    d.noGravity = true;
                }
            }
        }
    }
}
