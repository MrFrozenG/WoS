using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Items.Resources
{
    public class Ekuripusium : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 30;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 25, 50);
            Item.rare = ItemRarityID.Yellow;
        }
    }
}
