using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Summon.Minions;

namespace WoS.Content.Buffs.Minion
{
    public class LittleGraniteElementalMinionBuff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            if (player.ownedProjectileCounts[ModContent.ProjectileType<LittleGraniteElemental>()] > 0)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
