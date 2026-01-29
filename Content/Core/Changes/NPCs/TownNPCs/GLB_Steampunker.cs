using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.NPCs.TownNPCs
{
    public class GLB_Steampunker : GlobalNPC
    {
        private static int silverC = 100;
        private static int goldenC = 10_000;
        private static int PlatinumC = 1_000_000;
        public override void ModifyShop(NPCShop shop)
        {
            if (shop.NpcType == NPCID.Steampunker)
            {
                RemoveVanillaSolutions(shop);
                AddAllSolutions(shop);
            }
        }

        private void RemoveVanillaSolutions(NPCShop shop)
        {
            int[] solutions = new int[]
             {
            ItemID.GreenSolution,
            ItemID.SandSolution,
            ItemID.SnowSolution,
            ItemID.DirtSolution,
            ItemID.BlueSolution,
            ItemID.PurpleSolution,
            ItemID.DarkBlueSolution,
            ItemID.RedSolution
             };

            foreach (int solution in solutions)
            {
                if (shop.TryGetEntry(solution, out NPCShop.Entry entry))
                {
                    entry.Disable();
                }
            }
        }

        private void AddAllSolutions(NPCShop shop)
        {
            shop.Add(ItemID.GreenSolution);
            shop.Add(ItemID.BlueSolution);
            shop.Add(ItemID.PurpleSolution);
            shop.Add(ItemID.DarkBlueSolution);
            shop.Add(ItemID.RedSolution);
            if (NPC.downedMoonlord)
            {
                shop.Add(ItemID.SandSolution);
                shop.Add(ItemID.SnowSolution);
                shop.Add(ItemID.DirtSolution);
            }
        }
    }
}
