using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Buffs;
using WoS.Content.Buffs.Damage;
using WoS.Content.Buffs.Weakness;

namespace WoS.Content.Core.Globals
{
    public static class BuffsCategories
    {
        public static readonly Dictionary<string, List<int>> DebuffLists = new Dictionary<string, List<int>>()
        {
            { "Burning", new List<int> {
                BuffID.OnFire, BuffID.OnFire3,
                BuffID.CursedInferno, BuffID.ShadowFlame,
                BuffID.Frostburn, BuffID.Frostburn2 
                }
            },

            { "Poisonous", new List<int> 
                { BuffID.Poisoned, BuffID.Venom }
            },

            { "Slow", new List<int> { BuffID.Slow, BuffID.Webbed /*ModContent.BuffType<DebuffSlowness>()*/ } },

            //{ "Support", new List<int> { ModContent.BuffType<BuffPureWill>(), ModContent.BuffType<BuffPureDesire>() } },

            { "Weakness", new List<int> { ModContent.BuffType<Breaking>(), ModContent.BuffType<DivineIntervention>() } },

            { "DoT", new List<int> {
                BuffID.OnFire, BuffID.OnFire3,
                BuffID.CursedInferno, BuffID.ShadowFlame,
                BuffID.Frostburn, BuffID.Frostburn2,
                BuffID.Poisoned, BuffID.Venom,
                ModContent.BuffType<LifeDebt>() 
                } 
            },
            { "DoT_1", new List<int> {
            BuffID.OnFire,
            BuffID.ShadowFlame,
            BuffID.Frostburn,
            BuffID.Poisoned,
            ModContent.BuffType<LifeDebt>()
                }
            },
            { "DoT_2", new List<int> {
            BuffID.OnFire3,
            BuffID.ShadowFlame, BuffID.CursedInferno,
            BuffID.Frostburn2,
            BuffID.Venom,
            ModContent.BuffType<LifeDebt>()
                }
            }
        };
        public static int CountDebuffsFromCategory(NPC npc, string category)
        {
            int count = 0;

            if (DebuffLists.TryGetValue(category, out List<int> debuffList))
            {
                foreach (int debuff in debuffList)
                {
                    if (npc.HasBuff(debuff))
                        count++;
                }
            }

            return count;
        }
        public static bool HasDebuffFromCategory(NPC npc, string category)
        {
            if (DebuffLists.TryGetValue(category, out List<int> debuffList))
            {
                foreach (int debuff in debuffList)
                {
                    if (npc.HasBuff(debuff))
                        return true;
                }
            }
            return false;
        }

        public static void ApplyRandomDebuffFromCategory(NPC target, string category, int durationInTicks)
        {
            if (DebuffLists.TryGetValue(category, out var debuffList) && debuffList.Count > 0)
            {
                int debuff = Main.rand.Next(debuffList); // Выбираем случайный дебафф
                target.AddBuff(debuff, durationInTicks);
            }
        }

        public static void ApplyRandomDebuffFromCategoryToPlayer(Player target, string category, int durationInTicks)
        {
            if (DebuffLists.TryGetValue(category, out var debuffList) && debuffList.Count > 0)
            {
                int debuff = Main.rand.Next(debuffList); // Выбираем случайный дебафф
                target.AddBuff(debuff, durationInTicks);
            }
        }
    }
}
