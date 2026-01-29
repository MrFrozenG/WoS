using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Accessories.Universal;

namespace WoS.Content.Core.ModNPCs.Bosses
{
    public class GLB_WallofFlesh : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<HeartofSin>(), 3, 1));
            }
        }
    }
}
