using System.Collections.Generic;
using Terraria.ID;
using Terraria;

namespace WoS.Content.Core.Resocances
{
    internal class SteamBreachFlamingDebuffs
    {
        /// <summary>
        /// Список всех огненных дебаффов, которые считаются "горением".
        /// Этот список идентичен BuffsCategories["Burning"].
        /// </summary>
        public static readonly List<int> Burning = new List<int>()
        {
            BuffID.OnFire,
            BuffID.OnFire3,
            BuffID.CursedInferno,
            BuffID.ShadowFlame,
            BuffID.Frostburn,
            BuffID.Frostburn2
        };

        /// <summary>
        /// Проверяет, находится ли NPC под любым огненным дебаффом.
        /// </summary>
        public static bool HasBurningDebuff(NPC npc)
        {
            foreach (int debuff in Burning)
            {
                if (npc.HasBuff(debuff))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет все огненные дебаффы с NPC.
        /// </summary>
        public static void ClearBurning(NPC npc)
        {
            foreach (int debuff in Burning)
            {
                int index = npc.FindBuffIndex(debuff);
                if (index >= 0)
                    npc.DelBuff(index);
            }
        }

        /// <summary>
        /// Накладывает случайный огненный дебафф.
        /// </summary>
        public static void ApplyRandomBurning(NPC npc, int duration)
        {
            if (Burning.Count == 0)
                return;

            int selected = Main.rand.Next(Burning);
            npc.AddBuff(selected, duration);
        }
    }
}
