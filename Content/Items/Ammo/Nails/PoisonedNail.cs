
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Items.Ammo.Nails
{
    public class PoisonedNail : ModItem
    {
        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.Nail);
            Item.damage = 25;
            Item.knockBack = 2.5f;
            Item.shoot = ModContent.ProjectileType<PoisonedNailS>();
        }
        public override void AddRecipes()
        {
            CreateRecipe(300)
            .AddIngredient(ItemID.Nail, 300)
            .AddIngredient(ItemID.Stinger, 6)
            .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
