using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.ModPlayers;

namespace WoS.Content.Items.Accessories.Melee
{
    [AutoloadEquip(EquipType.Head)]
    public class VoidHairpin : ModItem
    {
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 48;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 75);
            Item.vanity = true;
            Item.accessory = true;
            Item.maxStack = 1;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<MainPlayer>().HoVHairpin = true;
        }
    }
}
