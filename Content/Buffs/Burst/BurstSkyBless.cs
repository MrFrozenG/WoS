using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Buffs.Burst
{
    public class BurstSkyBless : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.debuff[Type] = false;
            Main.persistentBuff[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetDamage(DamageClass.Magic) += 0.15f;
            player.GetCritChance(DamageClass.Magic) += 12f;
        }

        public override bool RightClick(int buffIndex)
        {
            return false;
        }
    }
}
