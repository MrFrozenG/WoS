using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Systems.Globals.GlobalNPCs
{
    public class GNPCShop : GlobalNPC
    {
        private static int silverC = 100;
        private static int goldenC = 10000;
        private static int PlatinumC = 1000000;
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.DD2Bartender && NPC.downedGolemBoss)
            {
                RemovePylonsFromShop(shop);
            }
        }
        private void RemovePylonsFromShop(NPCShop shop)
        {
            int[] pylons = new int[]
            {
            ItemID.TeleportationPylonPurity,
            ItemID.TeleportationPylonJungle,
            ItemID.TeleportationPylonDesert,
            ItemID.TeleportationPylonHallow,
            ItemID.TeleportationPylonSnow,
            ItemID.TeleportationPylonOcean,
            ItemID.TeleportationPylonMushroom,
            ItemID.TeleportationPylonUnderground,
            ItemID.TeleportationPylonVictory,
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
