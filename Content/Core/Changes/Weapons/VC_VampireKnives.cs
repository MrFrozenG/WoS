using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using WoS.Content.Buffs.Damage;
using WoS.Content.Core.ModNPCs;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_VampireKnives : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.VampireKnives;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().VampireKnivesBloodlust;
        }
        public override void SetDefaults(Item item)
        {
            item.crit += 8;
            item.StatsModifiedBy.Add(Mod);
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            TooltipLine line = new TooltipLine(Mod, "VampireKnives",
            Language.GetTextValue("Mods.WoS.General.WeaponTips.VampireKnives"));
            tooltips.Add(line);
        }
    }

    public class VC_VampireKnivesProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.VampireKnife;
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<MainConfig>().VampireKnivesBloodlust;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(2) && target.CanBeChasedBy())
            {
                target.AddBuff(ModContent.BuffType<Bloodlust>(), Main.rand.Next(2, 6 + 1) * 60);
                target.GetGlobalNPC<GLB_NpcsBuffs>().BleedingBonus = 15;
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<LifeDebt>(), Main.rand.Next(2, 6 + 1) * 60);
        }
    }
}