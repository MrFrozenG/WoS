
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Items.Ammo.Nails
{
    public class MoltenNail : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Nail);
            Item.damage = 25;
            Item.knockBack = 2.5f;
            Item.shoot = ModContent.ProjectileType<MoltenNailS>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(300)
            .AddIngredient(ItemID.Nail, 300)
            .AddIngredient(ItemID.Hellstone, 3)
            .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
