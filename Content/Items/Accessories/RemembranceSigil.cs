using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
<<<<<<< Updated upstream
using Terraria.DataStructures;
=======
using Microsoft.Xna.Framework;
using System;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.CritSystem;
>>>>>>> Stashed changes

namespace WoS.Content.Items.Accessories;

public class RemembranceSigil : ModItem, ICritAccessoryBonus
{
<<<<<<< Updated upstream
	public override void SetDefaults()
=======
    public float GetCritDamageBonus(Player player) => 0.50f;

    public override void SetDefaults()
>>>>>>> Stashed changes
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
<<<<<<< Updated upstream
=======
		player.GetModPlayer<MainPlayer>().SupportPointsBonus += 100;
    }
    public override bool CanUseItem(Player player)
	{
		return true;
>>>>>>> Stashed changes
	}
}