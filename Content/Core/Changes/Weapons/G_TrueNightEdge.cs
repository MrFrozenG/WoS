using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class G_TrueNightEdge : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.TrueNightsEdge;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
        public override void OnHitNPC(Item item, Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.CursedInferno, 60);
            base.OnHitNPC(item, player, target, hit, damageDone);
        }
        public override void OnHitPvp(Item item, Player player, Player target, Player.HurtInfo hurtInfo)
        {
            target.AddBuff(BuffID.CursedInferno, 60);
            base.OnHitPvp(item, player, target, hurtInfo);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "TrueNightsEdgeTip",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.TrueNightsEdge"));
            tooltips.Add(line);
        }
    }
    public class TrueNightEdgeSwing : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.TrueNightsEdge;
        }
        public override void SetDefaults(Projectile projectile)
        {
            projectile.penetrate += 3;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Ichor, 120);
            target.AddBuff(BuffID.CursedInferno, 60);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Ichor, 120);
            target.AddBuff(BuffID.CursedInferno, 60);
            base.OnHitPlayer(projectile, target, info);
        }
    }
}
