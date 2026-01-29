using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModSets;
using WoS.Content.Core.ModUtils;
using WoS.Content.Items.Accessories.Expert;

namespace WoS.Content.Items.Accessories.Universal
{
    [AutoloadEquip(EquipType.Shield)]
    public class PhoenixShield : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.accessory = true;
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 8;
            Item.value = ItemValues.Cost(0, 0, gold: 5, 0);
        }
        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
            if (!hideVisual)
            {
                int tileX = (int)(pl.position.X + pl.width / 2) / 16;
                int tileY = (int)(pl.position.Y + pl.height / 2) / 16;

                Lighting.AddLight(tileX, tileY, 1f, 0.8f, 0.3f);
            }
        }
        public override void AddRecipes()
        {
            CreateRecipe()
              .AddIngredient(ModContent.ItemType<RingofDivineFlames>())
              .AddIngredient(ItemID.ObsidianRose)
              .AddIngredient(ItemID.LavaCharm)
              .AddIngredient(ItemID.AnkhCharm)
              .AddIngredient(ItemID.ObsidianShield)
              .AddIngredient(ItemID.SoulofMight, 15)
              .AddIngredient(ItemID.SoulofFright, 15)
              .AddIngredient(ItemID.SoulofSight, 15)
              .AddTile(TileID.TinkerersWorkbench)
              .Register();
            Recipe.Create(ModContent.ItemType<PhoenixShield>())
                .AddIngredient(ModContent.ItemType<RingofDivineFlames>())
            .AddIngredient(ItemID.ObsidianRose)
            .AddIngredient(ItemID.LavaCharm)
            .AddIngredient(ItemID.AnkhCharm)
            .AddIngredient(ModContent.ItemType<ObsidianMonsterSlayerShield>())
            .AddIngredient(ItemID.SoulofMight, 15)
            .AddIngredient(ItemID.SoulofFright, 15)
            .AddIngredient(ItemID.SoulofSight, 15)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
            Recipe.Create(ModContent.ItemType<PhoenixShield>())
                .AddIngredient(ModContent.ItemType<RingofDivineFlames>())
            .AddIngredient(ItemID.ObsidianRose)
            .AddIngredient(ItemID.LavaCharm)
            .AddIngredient(ItemID.AnkhShield)
            .AddIngredient(ItemID.SoulofMight, 15)
            .AddIngredient(ItemID.SoulofFright, 15)
            .AddIngredient(ItemID.SoulofSight, 15)
            .AddTile(TileID.TinkerersWorkbench)
            .Register();
        }
    }
}
