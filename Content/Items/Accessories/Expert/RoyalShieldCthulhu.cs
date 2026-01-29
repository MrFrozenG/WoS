using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModSets;

namespace WoS.Content.Items.Accessories.Expert;
[AutoloadEquip(EquipType.Shield)]
public class RoyalShieldCthulhu : ModItem
{
    public override void SetDefaults()
    {
        Item.width = 30;
        Item.height = 32;
        Item.accessory = true;
        Item.rare = ItemRarityID.Expert;
        Item.expert = true;
        Item.defense = 2;
        Item.value = Item.sellPrice(0, 3, 0, 0);
    }

    public override void UpdateAccessory(Player pl, bool hideVisual)
    {
        pl.dashType = 2;
    }

    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient(ItemID.RoyalGel)
        .AddIngredient(ItemID.EoCShield)
        .AddTile(TileID.TinkerersWorkbench)
        .Register();
    }
}