using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Buffs.Burst;
using WoS.Content.Projectiles.Weapons.Vanila;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.DataStructures;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_SkyFracture : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public static bool isActive => ModContent.GetInstance<MainConfig>().SkyFractureCelestialBlessing || NPC.downedGolemBoss;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.SkyFracture;
        }
        public override void SetDefaults(Item item)
        {
            if (isActive)
            {
                item.StatsModifiedBy.Add(Mod);
            }
        }
        public override bool AltFunctionUse(Item item, Player player) => isActive;
        public override bool CanUseItem(Item item, Player player)
        {
            // Проверка на ПКМ
            if (player.altFunctionUse == 2 && isActive && !player.HasBuff(ModContent.BuffType<BurstSkyBless>()))
            {
                bool gaveBuff = false;
                float bladeSpeed = 3.4f; // тот же speed, что в AI

                // Ищем все снаряды CelestialBlade, принадлежащие игроку
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile proj = Main.projectile[i];
                    if (proj.active && proj.owner == player.whoAmI && proj.ModProjectile is CelestialBlade blade)
                    {
                        // Передаем позицию курсора и активируем клинок через поля ModProjectile
                        blade.target = Main.MouseWorld; // сохраняем цель (для эффектов, визуала)
                        blade.activated = true;         // включаем состояние полёта
                                                        // задаём стартовую скорость один раз
                        Vector2 dir = (Main.MouseWorld - proj.Center);
                        if (dir != Vector2.Zero)
                            dir.Normalize();
                        blade.storedVelocity = dir * bladeSpeed;
                        proj.velocity = blade.storedVelocity;

                        proj.timeLeft = 360;

                        gaveBuff = true;
                    }
                }

                if (gaveBuff)
                {
                    // Даем игроку бафф BurstSkyFracture один раз
                    player.AddBuff(ModContent.BuffType<BurstSkyBless>(), 800); // 10 секунд
                }
            }

            return base.CanUseItem(item, player);
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key;
            if (NPC.downedGolemBoss || ModContent.GetInstance<MainConfig>().SkyFractureCelestialBlessing)
            {
                key = "SkyFracture_CelestialBlades"; 
            }
            else
            {
                key = "SkyFracture_CelestialBlades_NotAwaked";
            }

            TooltipLine line = new TooltipLine(Mod, "SkyFracture", Language.GetTextValue("Mods.WoS.General.WeaponTips." + key));
            tooltips.Add(line);
        }
    }
}