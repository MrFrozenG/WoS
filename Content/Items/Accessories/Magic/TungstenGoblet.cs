using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

using WoS.Content.Systems.SysPlayers;

namespace WoS.Content.Items.Accessories.Magic;

public class TungstenGoblet : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 28;
		Item.maxStack = 1;
		Item.value = Item.sellPrice(0, 0, 40, 0);
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
		Item.defense = 1;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WoSPlayerMagic>().SilverGoblet = true;
	}
	
	public override void AddRecipes()
	{
		CreateRecipe(1)
		.AddIngredient(ItemID.TungstenBar, 3)
		.AddTile(TileID.Anvils)
		.Register();
	}
}
