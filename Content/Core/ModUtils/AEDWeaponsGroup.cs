using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.Core.ModSets;

namespace WoS.Content.Core.ModUtils
{
    public class AEDWeaponsGroup : GlobalItem
    {
        public override bool InstancePerEntity => false;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // Проверка на принадлежность к группе "IsAED"
            if (WoSItemsSets.IsAED[item.type])
            {
                // Добавляем строку из локализации
                tooltips.Add(new TooltipLine(ModContent.GetInstance<WoS>(), "AEDWeapon",
                    Language.GetTextValue("Mods.WoS.General.GlobalTips.Tooltip.AEDWeapon")));
            }
        }
    }
}
