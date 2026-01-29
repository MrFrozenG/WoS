using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Items.Armor.Elderguard
{
    [AutoloadEquip(EquipType.Head)]
    public class ElderguardHelmet : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor";
        public override void SetStaticDefaults()
        {
            ArmorIDs.Head.Sets.DrawHead[Item.headSlot] = false;
        }
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 1); 
            Item.rare = ItemRarityID.Purple; 
            Item.defense = 400; 
        }
        public override void UpdateEquip(Player player)
        {
            var mp = player.GetModPlayer<MainPlayer>();
            player.GetModPlayer<MainPlayer>().BonusCritDamageFlat += 500f;
            player.GetModPlayer<MainPlayer>().BonusCritDamageMult += 1f;
        }
    }
}