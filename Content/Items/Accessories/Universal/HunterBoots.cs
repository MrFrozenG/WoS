using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModSets;

namespace WoS.Content.Items.Accessories.Universal
{
    [AutoloadEquip(EquipType.Shoes)]
    public class HunterBoots : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 24;
            Item.accessory = true;
            Item.rare = ItemRarityID.White;
            Item.sellPrice(0, 0, 75, 0);
            Item.buyPrice(0, 1, 25, 0);
        }
        public override void UpdateAccessory(Player pl, bool hideVisual)
        {
            pl.GetModPlayer<MainPlayer>().HunterBoots = true;
        }
    }
}
