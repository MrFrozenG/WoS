using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModSets;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Items.Accessories.Expert
{
    [AutoloadEquip(EquipType.Back)]
    public class ParasiteHive : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.defense = 1;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 2, 0);
        }

        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
            pl.strongBees = true;
            pl.brainOfConfusionItem = Item;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HiveBackpack)
            .AddIngredient(ItemID.BrainOfConfusion)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
