using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Core.Changes.Misc
{
    public class FallingStarsEdit : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.FallingStar;
        }
        public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            // Разрешаем "проваливаться" сквозь платформы
            fallThrough = true;
            return true;
        }
    }
}
