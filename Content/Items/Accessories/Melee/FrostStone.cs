using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

using WoS.Content.Systems.SysPlayers;

namespace WoS.Content.Items.Accessories.Melee;

public class FrostStone : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 28;
		Item.maxStack = 1;
		Item.value = Item.sellPrice(0, 0, 70, 0);
		Item.rare = ItemRarityID.Orange;
		Item.accessory = true;
		Item.defense = 1;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WoSPlayerMelee>().FrostStone = true;
	}
}
