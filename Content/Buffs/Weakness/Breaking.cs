using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModNPCs;

namespace WoS.Content.Buffs.Weakness
{
    public class Breaking : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true;
            Main.buffNoSave[Type] = true;
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().Breaking = true;
        }
    }
}
