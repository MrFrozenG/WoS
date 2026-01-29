using Terraria.ID;
using Terraria;
using WoS.Content.Core.Globals;

namespace WoS.Content.Core.Resocances
{/*
    public interface ISteamWetSource { }
    public interface ISteamBurningSource { }
    public class SteamBreach
    {
        public static void TrySteamReaction(Projectile projectile, NPC target)
        {
            bool targetWet = target.HasBuff(BuffID.Wet);
            bool targetBurning = BuffsCategories.HasDebuffFromCategory(target, "Burning");

            bool projectileIsWet = projectile.ModProjectile is ISteamWetSource;
            bool projectileIsBurning = projectile.ModProjectile is ISteamBurningSource;

            // Условие реакции
            if ((projectileIsWet && targetBurning) ||
                (projectileIsBurning && targetWet))
            {
                TriggerSteam(target, projectile.damage, projectile.knockBack);
            }
        }

        private static void TriggerSteam(NPC target, int baseDamage, float knockback)
        {
            // 1) Удаляем дебаффы
            target.DelBuff(target.FindBuffIndex(BuffID.Wet));
            BuffsCategories.ClearCategory(target, "Burning");

            // 2) Урон пара (x3)
            int steamDamage = baseDamage * 3;

            // 3) Strike
            target.StrikeNPC(steamDamage, knockback, 0);

            // 4) Combat text
            CombatText.NewText(target.Hitbox, Terraria.ID.Color.Silver, "Steam Hit!");
        }
    }*/
}
