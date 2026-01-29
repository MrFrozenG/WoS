using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Items.Accessories.Universal
{
    public class RingofLivingFlames : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 20;
            Item.accessory = true;
            Item.rare = ItemRarityID.Orange;
            Item.value = ItemValues.Cost(0, 0, gold: 1, 0);
        }
        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
        }
    }
}
