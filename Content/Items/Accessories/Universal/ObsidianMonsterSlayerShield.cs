using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModSets;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Items.Accessories.Expert
{
    [AutoloadEquip(EquipType.Shield)]
    public class ObsidianMonsterSlayerShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 30;
            Item.accessory = true;
            Item.rare = ItemRarityID.Expert;
            Item.expert = true;
            Item.defense = 3;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 2, 0);
        }
        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
            pl.dashType = 2;
            pl.noKnockback = true;
            pl.fireWalk = true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ModContent.ItemType<MonsterSlayerShield>())
            .AddIngredient(ItemID.ObsidianSkull)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
            Recipe.Create(ModContent.ItemType<ObsidianMonsterSlayerShield>())
            .AddIngredient(ModContent.ItemType<RoyalShieldCthulhu>())
            .AddIngredient(ItemID.ObsidianSkull)
            .AddIngredient(ItemID.CobaltShield)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
            Recipe.Create(ModContent.ItemType<ObsidianMonsterSlayerShield>())
            .AddIngredient(ModContent.ItemType<RoyalShieldCthulhu>())
            .AddIngredient(ItemID.ObsidianShield)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
