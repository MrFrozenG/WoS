using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Items.Resources
{
    public class FragmentofFallenStar : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = Item.height = 32;
            Item.maxStack = Item.CommonMaxStack;
            Item.value = Item.sellPrice(0, 0, 50, 0);
            Item.rare = ItemRarityID.Green;
        }
    }
}
