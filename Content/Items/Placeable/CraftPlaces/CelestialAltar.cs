using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core;
using WoS.Content.Tiles.CraftPlaces;

namespace WoS.Content.Items.Placeable.CraftPlaces
{
    public class CelestialAltar : ModItem
    {
        public override void SetDefaults()
        {
            Item.DefaultToPlaceableTile(ModContent.TileType<Celestial_Altar>());
            Item.width = 22;
            Item.height = 18;
            Item.value = 150;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrystalBall)
            .AddRecipeGroup(Recipes.anyWorkbench)
            .AddRecipeGroup(Recipes.anyMythrilAnvil)
            .AddIngredient(ItemID.CrystalShard, 25)
            .AddIngredient(ItemID.FallenStar, 15)
            .AddIngredient(ItemID.SoulofLight, 10)
            .AddIngredient(ItemID.SoulofNight, 10)
            .AddIngredient(ItemID.HallowedBar, 35)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
