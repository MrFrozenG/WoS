using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using WoS.Content.Core.Changes;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_BlizzardStaff : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.BlizzardStaff;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().BlizzardStaffNoCeiling;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "BlizzardStaffTip",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.BlizzardStaff"));
            tooltips.Add(line);
        }
    }
    public class VC_BlizzardStaffProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.Blizzard;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().BlizzardStaffNoCeiling;
        }
        public override void AI(Projectile projectile)
        {
            ProjectileUtils.AI_NoCeiling(projectile);
        }
    }
}
