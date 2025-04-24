using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals.GlobalNPCs;

namespace WoS.Content.Buffs
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
            npc.GetGlobalNPC<GNPCsBuffs>().Breaking = true;
        }
    }
}
