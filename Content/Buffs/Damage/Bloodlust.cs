using Terraria.ModLoader;
using Terraria;
using WoS.Content.Core.ModNPCs;

namespace WoS.Content.Buffs.Damage
{
    public class Bloodlust : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().Bloodlust = true;
        }
    }
}