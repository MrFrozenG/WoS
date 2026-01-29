using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_MeteorStaff : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.MeteorStaff;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().MeteorStaffNoCeiling;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "MeteorStaffTip",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.MeteorStaff"));
            tooltips.Add(line);
        }
    }
    public class VC_MeteorStaffProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            switch (projectile.type)
            {
                case ProjectileID.Meteor1:
                case ProjectileID.Meteor2:
                case ProjectileID.Meteor3:
                    return true;
                default:
                    return false;
            }
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().BlizzardStaffNoCeiling;
        }
        public override void AI(Projectile projectile)
        {
            ProjectileUtils.AI_NoCeiling(projectile);
            base.AI(projectile);
        }
    }
}
