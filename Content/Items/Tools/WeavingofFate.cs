using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Items.Tools
{
    public class WeavingofFate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.mana = 200;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            { 
                if (NPC.downedGoblins && Main.invasionType == InvasionID.GoblinArmy)
                {
                    Main.invasionType = 0;
                    Main.invasionProgress = 0;
                }
                if (NPC.downedPirates && Main.invasionType == InvasionID.PirateInvasion)
                {
                    Main.invasionType = 0;
                    Main.invasionProgress = 0;
                }
                if (Main.invasionType == InvasionID.MartianMadness)
                {
                    Main.invasionType = 0;
                    Main.invasionProgress = 0;
                }
                if (Main.eclipse)
                {
                    Main.eclipse = false;
                }
                if (Main.bloodMoon)
                {
                    Main.bloodMoon = false;
                }
                return true;
            }
            return false;
        }
    }
}
/*
 OLD CODE, KEEP FOR FUTURE
        if (GlobalWorld.CircusEventStatus)
        {
            GlobalWorld.CircusEventStatus = false;
        }
                

        int radius = 60 * 16;

        foreach (Player p in Main.player)
        {
            Player pl = Main.player[player.whoAmI];
        if (p.active && p != player && p.Distance(player.Center) <= radius)
             {
            p.team = pl.team; // например, ставим команду "зеленую"
            p.statLife -= 10;
             }
        }
          
*/