using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using Terraria.Localization;
using WoS.Content.Core.DamageClasses;

namespace WoS.Content.Items.Armor.Pearlescent
{
    [AutoloadEquip(EquipType.Head)]
    public class PearlescentHelmet : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor";
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 5;
        }
        public override void UpdateEquip(Player player)
        {
            var MP = player.GetModPlayer<MainPlayer>();
            MP.SupportPointsBonus += 1;
            player.GetDamage(ModContent.GetInstance<SupportClass>()) += 0.1f;
        }
        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<PearlescentChestplate>() && legs.type == ModContent.ItemType<PearlescentLeggings>();
        }
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.WoS.Items.Armor.PearlescentHelmet.SetBonus"); // This is the setbonus tooltip: "Increases dealt damage by 20%"
            player.GetModPlayer<MainPlayer>().PearlescentSet = true;
        }
    }
}
