using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace WoS.Content.Core.Changes.Weapons
{
    public class G_StarFury : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Starfury;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
            item.useTime = 24;
            item.useAnimation = 20;
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; i++)
            {
                int starDamage = (int)(damage * 0.5f);

                float xOffset = Main.rand.NextFloat(-100f, 100f);
                float yOffset = Main.rand.NextFloat(750f, 1250f);
                Vector2 spawnPos = new Vector2(Main.MouseWorld.X + xOffset, Main.MouseWorld.Y - yOffset);
                Vector2 direction = Main.MouseWorld - spawnPos;
                direction.Normalize(); 
                float speed = 15f + Main.rand.NextFloat(-2f, 2f);
                Vector2 starVelocity = direction * speed;

                Projectile proj = Projectile.NewProjectileDirect(source, spawnPos, starVelocity, ProjectileID.Starfury,
                                         starDamage, knockback, player.whoAmI);
                proj.usesLocalNPCImmunity = true;
                proj.localNPCHitCooldown = 4;
            }

            return false;
        }
    }

    public class G_StarFuryStars : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        // Проверяем, что это звезда StarFury
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.Starfury;
        }
        public override bool TileCollideStyle(Projectile projectile, ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return true;
        }
    }
}
