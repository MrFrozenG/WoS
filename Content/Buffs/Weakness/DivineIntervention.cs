using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using WoS.Content.Core.ModNPCs;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Buffs.Weakness
{
    public class DivineIntervention : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.pvpBuff[Type] = true;
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().DivineIntervention = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().DivineIntervention = true;
        }
    }
}
