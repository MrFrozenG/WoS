using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.DataStructures;

using WoS.Content.Systems.SysPlayers;

namespace WoS.Content.Items.Accessories.Universal;
[AutoloadEquip(EquipType.Shield)]
public class RoyalCthulhuShield : ModItem
{
	public override void SetDefaults()
	{
		Item.width = 30;
		Item.height = 32;
		Item.accessory = true;
		Item.rare = ItemRarityID.Expert;
		Item.expert = true;
		Item.defense = 3;
		Item.value = Item.sellPrice(0, 3, 0, 0);
	}
	
	public override void UpdateAccessory(Player pl, bool hideVisual)
	{
		pl.dashType = 2;
		pl.npcTypeNoAggro[1] = true;
		pl.npcTypeNoAggro[16] = true;
		pl.npcTypeNoAggro[59] = true;
		pl.npcTypeNoAggro[71] = true;
		pl.npcTypeNoAggro[81] = true;
		pl.npcTypeNoAggro[138] = true;
		pl.npcTypeNoAggro[121] = true;
		pl.npcTypeNoAggro[122] = true;
		pl.npcTypeNoAggro[141] = true;
		pl.npcTypeNoAggro[147] = true;
		pl.npcTypeNoAggro[183] = true;
		pl.npcTypeNoAggro[184] = true;
		pl.npcTypeNoAggro[204] = true;
		pl.npcTypeNoAggro[225] = true;
		pl.npcTypeNoAggro[244] = true;
		pl.npcTypeNoAggro[302] = true;
		pl.npcTypeNoAggro[333] = true;
		pl.npcTypeNoAggro[335] = true;
		pl.npcTypeNoAggro[334] = true;
		pl.npcTypeNoAggro[336] = true;
		pl.npcTypeNoAggro[537] = true;
		pl.npcTypeNoAggro[535] = true;
		pl.npcTypeNoAggro[676] = true;
		pl.npcTypeNoAggro[667] = true;
	}
	
	public override void AddRecipes()
	{
		CreateRecipe(1)
		.AddIngredient(ItemID.RoyalGel)
		.AddIngredient(ItemID.EoCShield)
		.AddTile(TileID.TinkerersWorkbench)
		.Register();
	}
}