using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Buffs;
using WoS.Content.Buffs.SupportBuffs;

namespace WoS.Content.Systems.Globals
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
            { "Poison", new List<int> { BuffID.Poisoned, BuffID.Venom } },
            { "Slow", new List<int> { BuffID.Slow, BuffID.Webbed /*ModContent.BuffType<DebuffSlowness>()*/ } },
            { "Support", new List<int> { ModContent.BuffType<BuffPureWill>(), ModContent.BuffType<BuffPureDesire>() } },
            { "SupportDebuff", new List<int> { ModContent.BuffType<Breaking>(), ModContent.BuffType<DebuffDeepIllusions>() } },
            { "DoT", new List<int> {
                BuffID.OnFire, BuffID.OnFire3,
                BuffID.CursedInferno, BuffID.ShadowFlame,
                BuffID.Frostburn, BuffID.Frostburn2,
                BuffID.Poisoned, BuffID.Venom
 //               ModContent.BuffType<Bleeding>() 
                } 
            }
        };

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
    }
}
