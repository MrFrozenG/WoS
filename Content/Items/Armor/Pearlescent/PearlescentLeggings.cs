using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.DamageClasses;

namespace WoS.Content.Items.Armor.Pearlescent
{
    [AutoloadEquip(EquipType.Legs)]
    public class PearlescentLeggings : ModItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor";
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 22;
            Item.value = Item.sellPrice(silver: 10);
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }
        public override void UpdateEquip(Player player)
        {
            var MP = player.GetModPlayer<MainPlayer>();
            MP.SupportPointsBonus += 1;
            player.GetDamage(ModContent.GetInstance<SupportClass>()) += 0.1f;
            player.moveSpeed += 0.1f;
        }
    }
}
