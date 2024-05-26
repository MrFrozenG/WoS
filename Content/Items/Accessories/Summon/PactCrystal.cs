using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Items.Accessories.Summon;

public class PactCrystal : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 24;
		Item.height = 30;
		Item.accessory = true;
		Item.rare = 1;
		Item.value = Item.sellPrice(0, 0, 10, 0);
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.maxMinions++;
	}
}
