using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using System;
using WoS.Content.ModPlayers;

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

        Item.useStyle = ItemUseStyleID.HoldUp;
        Item.useAnimation = 17;
		Item.useTime = 17;
		Item.useTurn = true;
		Item.scale = 0.5f;
    }

    public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.moveSpeed += 0.1f;
		player.statManaMax2 += 10;
		player.statLifeMax2 += 10;
		player.pickSpeed -= 0.01f;
		player.statDefense += 1;
    }
    public override bool CanUseItem(Player player)
	{
		return true;
	}


    /*   public override bool? UseItem(Player player)
       {
           if (!GlobalWorld.CircusEventStatus)
           {
               GlobalWorld.CircusEventStatus = true;
           }
           else GlobalWorld.CircusEventStatus = false;
           return true;
       }*/
}