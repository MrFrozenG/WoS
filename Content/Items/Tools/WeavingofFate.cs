using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals;

namespace WoS.Content.Items.Tools
{
    public class WeavingofFate : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Expert;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.mana = 200;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                if (Main.invasionType != 0)
                {
                    Main.invasionType = 0;
                    Main.invasionSize = 0;
                    Main.invasionProgress = 0;
                    Main.invasionDelay = 0;
                    Main.invasionWarn = 0;
                }
                if (Main.eclipse)
                {
                    Main.eclipse = false;
                }
                if (Main.bloodMoon)
                {
                    Main.bloodMoon = false;
                }
                if (Main.invasionType == InvasionID.MartianMadness)
                {
                    Main.invasionType = 0;
                    Main.invasionSize = 0;
                    Main.invasionProgress = 0;
                    Main.invasionDelay = 0;
                }
 /*             if (GlobalWorld.CircusEventStatus)
                {
                    GlobalWorld.CircusEventStatus = false;
                }
 */
                return true;
            }
            return false;
        }
    }
}