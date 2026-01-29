using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;

namespace WoS.Content.Core.Changes.Weapons
{
    public class G_EnchantedSword : GlobalItem
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.EnchantedSword;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            int critChance = item.crit;

            string text = string.Format(
                Language.GetTextValue("Mods.WoS.General.WeaponTips.EnchantedSword"),
                critChance
            );
            
            TooltipLine line = new TooltipLine(Mod, "EnchantedSwordTip", text);
            tooltips.Add(line);
        }
        public override void SetDefaults(Item item)
        {
            item.DamageType = DamageClass.Magic;
            item.mana = 4;
            item.useTime = 24;
            item.useAnimation = 25;
            item.reuseDelay = 1;
            item.damage += 5;
            item.crit += 4;
            item.StatsModifiedBy.Add(Mod);
        }

        public override bool Shoot(
            Item item,
            Player player,
            EntitySource_ItemUse_WithAmmo source,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback)
        {
            Projectile.NewProjectile(
                source,
                position,
                velocity,
                type,
                damage,
                knockback,
                player.whoAmI
            );
            int critChance = item.crit;
            if (Main.rand.Next(100) < critChance)
            {
                Vector2 extraVelocity =
                    velocity.RotatedByRandom(MathHelper.ToRadians(1f)) * 0.5f;

                Projectile.NewProjectile(
                    source,
                    position,
                    extraVelocity,
                    type,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }
            return false; 
        }
    }
    public class G_EnchantedSwordProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.EnchantedBeam;
        }
        public override void SetDefaults(Projectile proj)
        {
            proj.DamageType = DamageClass.Magic;
        }
    }
}
