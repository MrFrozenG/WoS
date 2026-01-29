using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using WoS.Content.Buffs.Weakness;
using WoS.Content.Core.Globals;

namespace WoS.Content.Core.Changes.Weapons
{
    public class G_TerraBlade : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.TerraBlade;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DivineIntervention>(), 480);
            target.AddBuff(BuffID.CursedInferno, 480);
            BuffsCategories.ApplyRandomDebuffFromCategory(target, "DoT", 480);
            base.OnHitNPC(item, player, target, hit, damageDone);
        }
        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(ModContent.BuffType<DivineIntervention>(), 480);
            target.AddBuff(BuffID.CursedInferno, 480);
            BuffsCategories.ApplyRandomDebuffFromCategoryToPlayer(target, "DoT", 480);
            base.OnHitPvp(item, player, target, hurtInfo);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "TerraBladeTip",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.TerraBlade"));
            tooltips.Add(line);
        }
    }
    public class G_TerraBladeSwing : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            switch (projectile.type)
            {
                case ProjectileID.TerraBlade2:
                case ProjectileID.TerraBlade2Shot:
                case ProjectileID.TerraBeam:
                    return true;
                default:
                    return false;
            }
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(ModContent.BuffType<DivineIntervention>(), 480);
            target.AddBuff(BuffID.CursedInferno, 480);
            BuffsCategories.ApplyRandomDebuffFromCategory(target, "DoT", 480);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<DivineIntervention>(), 480);
            target.AddBuff(BuffID.CursedInferno, 480);
            BuffsCategories.ApplyRandomDebuffFromCategoryToPlayer(target, "DoT", 480);
            base.OnHitPlayer(projectile, target, info);
        }
    }
}
