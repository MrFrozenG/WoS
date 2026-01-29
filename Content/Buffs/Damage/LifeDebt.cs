using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModNPCs;

namespace WoS.Content.Buffs.Damage
{
    public class LifeDebt : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = true; 
            Main.pvpBuff[Type] = true; 
            Main.buffNoSave[Type] = true; 
            BuffID.Sets.LongerExpertDebuff[Type] = true; 
        }
        public override void Update(NPC npc, ref int buffIndex)
        {
            npc.GetGlobalNPC<GLB_NpcsBuffs>().lifeDebt = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().lifeDebt = true;
        }
    }
}
