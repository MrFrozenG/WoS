using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

namespace WoS.Content.Items.Accessories;

public class RemembranceSigil : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 16;
		Item.height = 16;
		Item.maxStack = 1;
		Item.value = Item.sellPrice(0, 50, 0, 0);
		Item.rare = ItemRarityID.Cyan;
		Item.accessory = true;
		Item.defense = 1;
	}
	
	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.moveSpeed += 0.1f;
		player.statManaMax2 += 10;
		player.statLifeMax2 += 10;
		player.pickSpeed -= 0.01f;
		player.statDefense += 1;
	}
}