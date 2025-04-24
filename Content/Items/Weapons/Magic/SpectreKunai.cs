using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Magic;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Items.Weapons.Magic
{
    public class SpectreKunai : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 35;
            Item.knockBack = 1.1f;
            Item.crit = 16;
            Item.noMelee = true;
            Item.maxStack = 1;

            Item.mana = 5;
            Item.width = Item.height = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 24;
            Item.useTime = 8;
            Item.reuseDelay = 16;
            Item.noUseGraphic = true;

            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Lime;
            Item.value = Item.sellPrice(0, 0, 1, 35);

            Item.shoot = ModContent.ProjectileType<SpectreKunaiThrow>();
            Item.shootSpeed = 10f;

        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.SpectreBar, 10)
            .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
