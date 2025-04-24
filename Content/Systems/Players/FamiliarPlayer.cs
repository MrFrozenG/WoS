
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.SupportBuffs;
using WoS.Content.Systems.Familiars;

namespace WoS.Content.ModPlayers
{
    public class FamiliarPlayer : ModPlayer
    {
        public int AdditionalDamage = 0; // Дополнительный урон к фамильяру
        public float AdditionalPower = 0f; // Доп. значение "Силы"
        public float AdditionalPower1 = 1f; // Множитель "Силы"

        public float SoulEnergy = 0f; // Текущая энергия
        public float SoulEnergyMax = 0f; // Максимальная энергия (SoulCost фамильяра)
        public int SoulEnergyRegen = 0; // Скорость восстановления энергии

        private int regenTimer = 0; // Таймер для восстановления энергии

        public override void ResetEffects()
        {
            AdditionalDamage = 0;
            AdditionalPower = 0f;
            AdditionalPower1 = 1f;
            SoulEnergyRegen = 0;
        }

        public override void PreUpdate()
        {
            if (SoulEnergy < SoulEnergyMax)
            {
                regenTimer++;
                int regenDelay = Math.Max(60, 120 - (SoulEnergyRegen / 2) * 10); // Каждые 2 регена уменьшает время на 0.1с
                if (regenTimer >= regenDelay)
                {
                    SoulEnergy += SoulEnergyRegen;
                    if (SoulEnergy > SoulEnergyMax)
                        SoulEnergy = SoulEnergyMax;
                    regenTimer = 0;
                }
            }
        }

        public void SetFamiliarStats(IFamiliarItem familiarItem)
        {
            if (familiarItem != null)
            {
                SoulEnergyMax = familiarItem.SoulCost;
                SoulEnergyRegen = familiarItem.SoulRegen;
            }
        }

        public void SummonFamiliar(IFamiliarItem familiarItem)
        {
            if (familiarItem == null) return;

            // Удаляем старых фамильяров, если они есть
            foreach (Projectile p in Main.projectile)
            {
                if (p.active && p.owner == Player.whoAmI &&
                    (p.type == familiarItem.FamiliarA || p.type == familiarItem.FamiliarB))
                {
                    p.Kill();
                }
            }

            // Призываем первого фамильяра
            int projA = Projectile.NewProjectile(
                Player.GetSource_FromThis(),
                Player.Center,
                Vector2.Zero,
                familiarItem.FamiliarA,
                familiarItem.BaseDamage + AdditionalDamage,
                0,
                Player.whoAmI
            );

            // Призываем второго фамильяра, если он другой
            if (familiarItem.FamiliarA != familiarItem.FamiliarB)
            {
                int projB = Projectile.NewProjectile(
                    Player.GetSource_FromThis(),
                    Player.Center,
                    Vector2.Zero,
                    familiarItem.FamiliarB,
                    familiarItem.BaseDamage + AdditionalDamage,
                    0,
                    Player.whoAmI
                );
            }

            // Даем бафф на фамильяра
            Player.AddBuff(ModContent.BuffType<BuffPureDesire>(), 2);
        }
    }
}
