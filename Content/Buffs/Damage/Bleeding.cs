using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals.GlobalNPCs;
using WoS.Content.ModPlayers;

namespace WoS.Content.Buffs.Damage
{
    public class Bleeding : ModBuff
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
            npc.GetGlobalNPC<GNPCsBuffs>().Bleeding = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().Bleeding = true;
        }
    }
}
