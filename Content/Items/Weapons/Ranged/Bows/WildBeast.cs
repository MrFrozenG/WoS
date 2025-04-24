using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Ranged.Bows
{
    public class WildBeast : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 15;
            Item.knockBack = 1.1f;
            Item.crit = 12;
            Item.noMelee = true;

            Item.width = 22;
            Item.height = 58;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.UseSound = SoundID.Item5;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 1, 50, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                int newDamage = damage;
                newVelocity *= 1f + 0.75f;
                newDamage *= (int)1.5f;
                Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, newDamage, knockback, player.whoAmI);
                proj.CritChance += 16;
            }
            else
            {
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }
    }
}
