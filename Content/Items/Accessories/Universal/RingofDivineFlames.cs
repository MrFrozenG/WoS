
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;
using WoS.Content.Items.Accessories.Expert;

namespace WoS.Content.Items.Accessories.Universal
{
    public class RingofDivineFlames : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = ItemValues.Cost(0, 0, gold: 2, 0);
        }
        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<RingofLivingFlames>())
            .AddIngredient(ItemID.ObsidianRose)
            .AddIngredient(ItemID.SoulofMight, 5)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();

        }
    }
}
