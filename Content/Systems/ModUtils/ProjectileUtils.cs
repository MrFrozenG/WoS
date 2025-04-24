using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace WoS.Content.Systems.ModUtils
{
    public class ProjectileUtils
    {

        public static NPC FindClosestNPC(Vector2 position, float maxDetectDistance)
        {
            NPC closestNPC = null;
            float closestDistance = maxDetectDistance;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && !npc.friendly && npc.active)
                {
                    float distance = Vector2.Distance(position, npc.Center);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }
        public static bool ProjectileTrailVisualPreDraw(Projectile projectile, ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[projectile.type].Value;
            Vector2 origin = texture.Size() / 2f;
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] + projectile.Size / 2f - Main.screenPosition;
                float opacity = (projectile.oldPos.Length - i) / (float)projectile.oldPos.Length;

                Main.spriteBatch.Draw(
                    texture,
                    drawPos,
                    null,
                    projectile.GetAlpha(lightColor) * opacity * 0.5f,
                    projectile.rotation,
                    origin,
                    projectile.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            return true;
        }

        public static void DualBladesPattern_Split(Projectile originalProjectile, int firstBladeType, int secondBladeType, float damagePercentFirst,
                float damagePercentSecond, float splitAngleDegrees = 25f, float bladeSpeed = 8f)
        {
            Player player = Main.player[originalProjectile.owner];
            if (Main.myPlayer != originalProjectile.owner)
                return;

            Vector2 baseDirection = originalProjectile.velocity.SafeNormalize(Vector2.UnitX);

            Vector2 leftDirection = baseDirection.RotatedBy(MathHelper.ToRadians(-splitAngleDegrees)) * bladeSpeed;
            Vector2 rightDirection = baseDirection.RotatedBy(MathHelper.ToRadians(splitAngleDegrees)) * bladeSpeed;

            int damageFirst = (int)(originalProjectile.damage * damagePercentFirst);
            int damageSecond = (int)(originalProjectile.damage * damagePercentSecond);

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                leftDirection,
                firstBladeType,
                damageFirst,
                originalProjectile.knockBack,
                originalProjectile.owner
            );

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                rightDirection,
                secondBladeType,
                damageSecond,
                originalProjectile.knockBack,
                originalProjectile.owner
            );
            originalProjectile.Kill();
        }

        public static void DualBladesPattern_SplitWithoutKill(Projectile originalProjectile, int firstBladeType, int secondBladeType, float damagePercentFirst,
                float damagePercentSecond, float splitAngleDegrees = 25f, float bladeSpeed = 8f)
        {
            Player player = Main.player[originalProjectile.owner];
            if (Main.myPlayer != originalProjectile.owner)
                return;

            Vector2 baseDirection = originalProjectile.velocity.SafeNormalize(Vector2.UnitX);

            Vector2 leftDirection = baseDirection.RotatedBy(MathHelper.ToRadians(-splitAngleDegrees)) * bladeSpeed;
            Vector2 rightDirection = baseDirection.RotatedBy(MathHelper.ToRadians(splitAngleDegrees)) * bladeSpeed;

            int damageFirst = (int)(originalProjectile.damage * damagePercentFirst);
            int damageSecond = (int)(originalProjectile.damage * damagePercentSecond);

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                leftDirection,
                firstBladeType,
                damageFirst,
                originalProjectile.knockBack,
                originalProjectile.owner
            );

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                rightDirection,
                secondBladeType,
                damageSecond,
                originalProjectile.knockBack,
                originalProjectile.owner
            );
        }
    }
}
