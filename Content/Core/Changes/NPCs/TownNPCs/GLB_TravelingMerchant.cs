using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Weapons.Magic;
using WoS.Content.Items.Weapons.Ranged.Bows;
//using WoS.Content.Items.Weapons.Summon;

namespace WoS.Content.Core.ModNPCs.TownNPCs;

class GLB_TravelingMerchant : GlobalNPC
{
    private static int silverC = 100;
    private static int goldenC = 10000;
    private static int PlatinumC = 1000000;

    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.TravellingMerchant)
        {
            if (Main.rand.NextBool(2) && NPC.downedBoss3)
            {
                //shop.Add(new Item(ModContent.ItemType<TrustYourHeart>())
               // {
                //    shopCustomPrice = goldenC * 13
               // });
            }
            //            if (Main.rand.NextBool(2) && NPC.downedMechBossAny)
            //            {
            //                shop.Add(new Item(ModContent.ItemType<Reawakening>())
            //                {
            //                    shopCustomPrice = (PlatinumC * 1 + goldenC * 50)
            //                });
            //            }
        }
    }
}
