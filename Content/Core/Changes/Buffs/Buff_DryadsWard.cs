using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Core.Changes.Buffs
{
    public class Buff_DryadsWard : GlobalBuff
    { 
        public override void Update(int type, Player player, ref int buffIndex)
        {
            // Проверяем, что это именно DryadsWard
            if (type != BuffID.DryadsWard)
                return;

            // Определяем уровень усиления
            int level = GetBlessingLevel();

            if (level == 1) // базовый уровень
            {
                player.statDefense += 8;
                player.GetModPlayer<MainPlayer>().SupportPointsBonus += 1;
            }
            else if (level == 2) // хардмод
            {
                player.statDefense += 12;
                player.lifeRegen += 1;
                player.GetModPlayer<MainPlayer>().SupportPointsBonus += 1;
            }
            else if (level == 3) // после Плантеры
            {
                player.statDefense += 16;
                player.statLifeMax2 = (int)(player.statLifeMax2 * 1.2f);
                player.GetModPlayer<MainPlayer>().SupportPointsBonus += 2;
            }
        }

        private int GetBlessingLevel()
        {
            if (NPC.downedPlantBoss) return 3; // после Плантеры
            if (Main.hardMode) return 2;       // хардмод
            return 1;                          // базовый уровень
        }
    }
}
