using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Accessories.Universal;

namespace WoS.Content.Core.ModNPCs.Hell
{
    public class CommonDropHell : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            if (npc.type == NPCID.Lavabat ||
                npc.type == NPCID.BoneSerpentHead ||
                npc.type == NPCID.Demon ||
                npc.type == NPCID.VoodooDemon &&
                NPC.downedMechBossAny)
            {
                npcLoot.Add(ItemDropRule.NormalvsExpert(ModContent.ItemType<RingofLivingFlames>(), 1000, 200));
            }
        }
    }
}
