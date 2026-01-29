using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.Misc
{
    public class RecipesEdit : ModSystem
    {
        public override void PostAddRecipes()
        {
            foreach (var recipe in Main.recipe)
            {
                if (recipe != null && recipe.createItem.type == ItemID.Beenade)
                {
                    recipe.DisableRecipe();
                }
            }
            Recipe.Create(ItemID.Beenade, 100)
            .AddIngredient(ItemID.Grenade, 100)
            .AddIngredient(ItemID.BeeWax, 10)
            .AddTile(TileID.Anvils)
                .Register();
        }
        public override void AddRecipes()
        {
            //Beenade's new recipe
            Recipe.Create(ItemID.Beenade, 100)
            .AddIngredient(ItemID.Grenade, 100)
            .AddIngredient(ItemID.BeeWax, 10)
            .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
