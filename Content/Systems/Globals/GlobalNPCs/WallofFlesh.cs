using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Systems.Globals.GlobalNPCs
{
    public class WallofFlesh : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.WallofFlesh)
            {
 //               npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<OccultEmblem>(), 5));
 //               npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<HeartofSin>(), 1));
            }
        }
    }
}
