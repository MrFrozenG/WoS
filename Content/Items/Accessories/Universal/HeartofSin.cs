using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using Terraria.Localization;

namespace WoS.Content.Items.Accessories.Universal
{
    public class HeartofSin : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 36;
            Item.maxStack = 1;
            Item.value = Item.sellPrice(silver: 50);
            Item.rare = ItemRarityID.Orange;
            Item.accessory = true;
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            CurrentBonus = player.GetModPlayer<MainPlayer>().HeartofSinCrit * 100f;
        }
        public static readonly int MaxBonusCritPercent = 30;
        public float CurrentBonus;
        public override LocalizedText Tooltip =>
            base.Tooltip.WithFormatArgs(MaxBonusCritPercent, CurrentBonus);
    }
}
