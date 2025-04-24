using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.ModPlayers;

namespace WoS.Content.Items.Accessories.Summon
{
    public class PossessedSkull : ModItem
    {
        public override void SetDefaults()
        {
            int silver = 100;
            int gold = silver * 100;
            Item.width = 22;
            Item.height = 28;
            Item.maxStack = 1;
            Item.value = gold;
            Item.rare = ItemRarityID.Blue;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MainPlayer>().PossessedSkull = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.Bone, 10)
            .AddIngredient(ItemID.Cobweb, 50)
            .AddIngredient(ItemID.FallenStar)
            .AddTile(TileID.WorkBenches)
            .AddCondition(Condition.InGraveyard)
            .Register();
        }
    }
}
