using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.ModPlayers;

namespace WoS.Content.Items.Accessories.Support
{
    public class DryadsFlower : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.maxStack = 1;
            Item.value = 5000;
            Item.rare = ItemRarityID.Cyan;
            Item.accessory = true;
            Item.vanity = false;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MainPlayer>().SupportPointsBonus += 1;
//            player.GetModPlayer<MainPlayer>().SupportPointsBonus2 += 0.5f;
        }
    }
}
