using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

using WoS.Content.Systems.SysPlayers;

namespace WoS.Content.Items.Accessories.Melee;

public class BloodgemRing : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 26;
		Item.height = 20;
		Item.accessory = true;
		Item.rare = ItemRarityID.Blue;
		Item.value = Item.sellPrice(0, 0, 30, 0);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetModPlayer<WoSPlayerMelee>().Bloodgem = true;
	}
}