using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModNPCs;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Buffs.Damage
{
    public class Discharge : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().Discharge = true;
        }
    }
}
