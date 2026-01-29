using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Accessories;

namespace WoS.Content.Core.ModNPCs.TownNPCs;
public class GLB_Merchant : GlobalNPC
{
    private static int silverC = 100;
    private static int goldenC = 10_000;
    private static int PlatinumC = 1_000_000;
    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.Merchant)
        {
            shop.Add(new Item(ItemID.FlareGun)
            {
                shopCustomPrice = goldenC//,
                //shopSpecialCurrency = WoS.ClownEventCurrencyId
            });
        }

    }
    
}