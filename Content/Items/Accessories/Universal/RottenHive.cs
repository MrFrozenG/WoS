using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModSets;
using WoS.Content.Core.ModUtils;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Items.Accessories.Expert
{
    [AutoloadEquip(EquipType.Back)]
    public class RottenHive : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
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
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HiveBackpack)
            .AddIngredient(ItemID.WormScarf)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
