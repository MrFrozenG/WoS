using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Collections.Generic;
using Terraria.Localization;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_StarFury : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Starfury;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().StarfuryNoCeiling;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "StarfuryNoCeiling",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.StarfuryNoCeiling"));
            tooltips.Add(line);
        }
    }
    public class VC_Starfury : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.Starfury;
        }
        public override void AI(Projectile projectile)
        {
            ProjectileUtils.AI_NoCeiling(projectile);
            base.AI(projectile);
        }
    }
}
