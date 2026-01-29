using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using Microsoft.Xna.Framework;

namespace WoS.Content.Core.CritSystem
{

    public class CritGlobalItem : GlobalItem
    {
        private float itemBaseCrit;

        public override bool InstancePerEntity => true;

        public override void Load()
        {
        }
        public override bool AppliesToEntity(Item item, bool late) => item.ModItem is ICriticalDamageProvider;

        public override void SetDefaults(Item item)
        {
            if (item.ModItem is ICriticalDamageProvider prov)
                itemBaseCrit = prov.BaseCriticalDamage;
        }

        public override void ModifyWeaponCrit(Item item, Player player, ref float crit)
        {
            // Стандартный шанс крита остаётся, без изменения
        }

        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            float bonus = itemBaseCrit + player.GetModPlayer<MainPlayer>().CritDamageBonus;
            modifiers.CritDamage += bonus;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!(item.ModItem is ICriticalDamageProvider)) return;

            float bonus = itemBaseCrit + Main.LocalPlayer.GetModPlayer<MainPlayer>().CritDamageBonus;
            if (bonus <= 0) return;

            string label = Language.GetTextValue("Mods.WoS.General.GlobalTips.CritStrike");
            string text = $"{(int)((1f + bonus) * 100)}% {label}";
            var line = new TooltipLine(Mod, "CritDamage", text);

            int idx = tooltips.FindIndex(t => t.Mod == "Terraria" && t.Name == "CritChance");
            if (idx != -1) tooltips.Insert(idx + 1, line);
            else tooltips.Add(line);
        }
    }
}