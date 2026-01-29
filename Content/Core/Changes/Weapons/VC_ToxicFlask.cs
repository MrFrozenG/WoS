
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using WoS.Content.Core.Globals;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_ToxicFlask : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.ToxicFlask;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().ToxicFlaskPoisonousBreath;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "ToxicFlask",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.ToxicFlask"));
            tooltips.Add(line);
        }
    }
    public class VC_ToxicFlaskProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            switch (projectile.type)
            {
                case ProjectileID.ToxicCloud:
                case ProjectileID.ToxicCloud2:
                case ProjectileID.ToxicCloud3:
                    return true;
                default:
                    return false;
            }
        }
        public override void SetDefaults(Projectile projectile)
        {
            projectile.penetrate += 4;
            projectile.timeLeft += 240;
            projectile.scale += 0.5f;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().ToxicFlaskPoisonousBreath;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            BuffsCategories.ApplyRandomDebuffFromCategory(target, "Poisonous", 60 * Main.rand.Next(3,9 + 1)); 
            if (Main.rand.NextBool(5))
            {
                BuffsCategories.ApplyRandomDebuffFromCategory(target, "DoT", 90 * Main.rand.Next(2, 5 + 1));
            }
        }
    }
}
