using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace WoS.Content.Systems.ModUtils
{
    public class ProjectileUtilsGlow : GlobalProjectile
    {
        public Texture2D glowTexture = null;  // Текстура для свечения
        public int glowOffsetY = 0;           // Смещение по оси Y
        public int glowOffsetX = 0;           // Смещение по оси X

        public override bool InstancePerEntity => true;

        public override GlobalProjectile Clone(Projectile projectile, Projectile projectileClone)
        {
            return base.Clone(projectile, projectileClone);
        }
        public override void PostDraw(Projectile projectile, Color lightColor)
        {
            if (glowTexture != null)
            {
                // Получаем текстуру снаряда
                Texture2D texture = glowTexture;

                // Позиция для рисования свечения снаряда (с учетом смещения)
                Vector2 drawPos = new Vector2(
                    projectile.position.X - Main.screenPosition.X + projectile.width * 0.5f + glowOffsetX,
                    projectile.position.Y - Main.screenPosition.Y + projectile.height * 0.5f + glowOffsetY
                );

                // Рисуем слой свечения
                Main.spriteBatch.Draw(
                    texture,
                    drawPos,
                    new Rectangle(0, 0, texture.Width, texture.Height),
                    Color.White,
                    projectile.rotation,
                    texture.Size() * 0.5f,
                    projectile.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            base.PostDraw(projectile, lightColor);
        }
    }
}
