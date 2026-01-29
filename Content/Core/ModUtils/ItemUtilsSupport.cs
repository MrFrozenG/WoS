using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Prefixes;
using WoS.Content.Core.Interfaces;

namespace WoS.Content.Core.ModUtils
{
    public class ItemUtilsSupport : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public int SupportPoints = 0;

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (item.ModItem is not ISupportWeapon)
                return;

            int totalPoints = item.GetSupportPoints(Main.LocalPlayer);
            if (totalPoints <= 0)
                return;

            string localizedName = Language.GetTextValue(
                "Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName"
            );

            TooltipLine line = new TooltipLine(
                Mod,
                "SupportPoints",
                $"{localizedName}: {totalPoints}"
            )
            {
                OverrideColor = Color.LightGreen
            };

            int index = tooltips.FindIndex(
                t => t.Mod == "Terraria" && t.Name == "Damage"
            );

            if (index != -1)
                tooltips.Insert(index + 1, line);
            else
                tooltips.Add(line);
        }
    }
}
/*
public override void UpdateInventory(Item item, Player player)
{
    if (item.ModItem is ISupportWeapon supportWeapon)
    {
        var modPlayer = player.GetModPlayer<MainPlayer>();
        var prefixGlobal = item.GetGlobalItem<SupportPrefixGlobal>();

        int baseValue = supportWeapon.BaseSupportPoints;

        int totalPoints = modPlayer.GetModifiedSupportPoints(baseValue);

        // Записываем в интерфейс оружия
        supportWeapon.TotalSupportPoints = totalPoints;
    }
}

public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
{
    if (item.ModItem is ISupportWeapon supportWeapon)
    {
        Player player = Main.LocalPlayer;
        var modPlayer = player.GetModPlayer<MainPlayer>();

        int basePoints = supportWeapon.BaseSupportPoints;
        int bonusFlat = modPlayer.SupportPointsBonus;
        float bonusMult = modPlayer.SupportPointsBonus2;

        int totalPoints = (int)((basePoints + bonusFlat) * bonusMult);

        if (totalPoints > 0)
        {
            string localizedName = Language.GetTextValue("Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName");
            TooltipLine line = new TooltipLine(Mod, "SupportPoints", $"{localizedName}: {totalPoints}")
            {
                OverrideColor = Color.LightGreen
            };

            int index = tooltips.FindIndex(t => t.Mod == "Terraria" && t.Name == "Damage");
            if (index != -1)
                tooltips.Insert(index + 1, line);
            else
                tooltips.Add(line);
        }
    }
}*/