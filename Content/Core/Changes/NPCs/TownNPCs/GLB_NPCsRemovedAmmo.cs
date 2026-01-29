using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Core.ModNPCs.TownNPCs
{
    public class GLB_NPCsRemovedAmmo : GlobalNPC
    {
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Merchant && shop.NpcType == NPCID.ArmsDealer)
            {
                RemoveAmmunationsFromShop(shop);
                /*            shop.Add(new Item(ModContent.ItemType<RemembranceSigil>())
                            {
                                shopCustomPrice = 2,
                                shopSpecialCurrency = WoS.ClownEventCurrencyId
                            });*/
            }

        }
        private void RemoveAmmunationsFromShop(NPCShop shop)
        {
            int[] pylons = new int[]
            {
            ItemID.WoodenArrow,
            ItemID.Nail,
            ItemID.UnholyArrow,
            ItemID.Stake,
            ItemID.CandyCorn,
            ItemID.StyngerBolt,
            ItemID.ExplosiveJackOLantern,
            };

            foreach (int pylon in pylons)
            {
                if (shop.TryGetEntry(pylon, out NPCShop.Entry entry))
                {
                    entry.Disable();
                }
            }
        }
    }
}
