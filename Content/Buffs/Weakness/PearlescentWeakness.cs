using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModNPCs;

namespace WoS.Content.Buffs.Weakness
{
    public class PearlescentWeakness : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().PearlescentWeakness = true;
        }
    }
}
