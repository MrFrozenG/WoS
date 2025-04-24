using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Accessories;

namespace WoS.Content.Systems.Globals.GlobalNPCs;
public class Merchant : GlobalNPC
{
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.Merchant)
        {
/*            shop.Add(new Item(ModContent.ItemType<RemembranceSigil>())
            {
                shopCustomPrice = 2,
                shopSpecialCurrency = WoS.ClownEventCurrencyId
            });*/
        }
    }
}