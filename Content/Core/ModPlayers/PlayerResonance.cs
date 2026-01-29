using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.Globals;
using Terraria.Localization;
using Microsoft.Xna.Framework;
using WoS.Content.Core.Resonances;
using WoS.Content.Core.ModSets;

namespace WoS.Content.Core.ModPlayers
{
    public class PlayerResonance : ModPlayer
    {
        private static float SteamDamageMultiplier = 3f;
        private static float BreachDamage = 1f;
        private static int SteamBreachDamage = 30;
        private static float BreachTimer = 1f;
        private static bool SteamCanCrit;
        private static int ShockChargeCost = 100;
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ProcessResonanceEffect(proj, null, target, hit);
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            ProcessResonanceEffect(null, item, target, hit);
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            ProcessResonanceEffect(null, null, target, hit);
        }

        public override void ResetEffects()
        {
            SteamDamageMultiplier = 3f;
            BreachDamage = 1f;
            SteamCanCrit = false;
        }
        private void ProcessResonanceEffect(Projectile proj, Item item, NPC target, NPC.HitInfo hit)
        {
            bool hasWet = target.HasBuff(BuffID.Wet);
            bool hasBurning = BuffsCategories.HasDebuffFromCategory(target, "Burning");

            if (hasWet && hasBurning)
            {
                SteamResonance(proj, target, hit);
            }
            ShockResonance(proj, item, target);
        }
        private void ShockResonance(Projectile proj, Item item, NPC target)
        {
            // Проверяем: удар относится к Shock
            bool isShock = false;

            if (proj != null && WoSProjectilesSets.IsProjectileShockRelated[proj.type])
                isShock = true;

            if (item != null && WoSItemsSets.IsItemShockRelated[item.type])
                isShock = true;

            if (!isShock)
                return;

            // Берём "главного" NPC, если сегменты
            NPC realTarget = target.realLife >= 0 ? Main.npc[target.realLife] : target;
            var shock = realTarget.GetGlobalNPC<ShockResonanceNPCs>();

            // Проверяем, накоплено ли достаточно
            if (shock.ShockCharge >= ShockChargeCost && shock.ShockStunTimer <= 0)
            {
                // Срабатывает Shock
                TriggerShock(realTarget, shock);

                // Снимаем 80% заряда
                shock.ShockCharge = (int)(shock.ShockCharge * 0.2f);
            }
        }
        private void TriggerShock(NPC target, ShockResonanceNPCs shock)
        {
            shock.ShockStunTimer = 30; // 0.5 секунды
                                       //shock.LastHitPlayer = Player.whoAmI;

            // Показываем текст резонанса, увеличенный в 2 раза
            /*CombatText.NewText(
                target.Hitbox,
                Color.LightBlue,
                Language.GetTextValue("Mods.WoS.General.BattleTips.Shock"),
                dramatic: true,   // делает текст более “драматичным”
                dot: false
            );*/
            int textIndex = CombatText.NewText(target.Hitbox, 
                Color.LightBlue, 
                Language.GetTextValue("Mods.WoS.General.BattleTips.Shock"), 
                dramatic: true);
            if (textIndex < 100)
            {
                Main.combatText[textIndex].scale = 4f; 
            }
        }
        private void SteamResonance(Projectile proj, NPC target, NPC.HitInfo hit)
        {
            ClearBurning(target);

            // Урон Пара: базовый урон * 3
            int baseDamage = proj != null ? proj.damage : hit.Damage;
            int steamDamage = (int)(baseDamage * SteamDamageMultiplier * BreachDamage);

            target.StrikeNPC(new NPC.HitInfo
            {
                Damage = steamDamage + SteamBreachDamage,
                Knockback = hit.Knockback,
                HitDirection = hit.HitDirection,
                Crit = SteamCanCrit
            });

            CombatText.NewText(
                target.Hitbox,
                Color.LightGray,
                Language.GetTextValue("Mods.WoS.General.BattleTips.SteamHit")
            );
            ShowSteamResonance(target, steamDamage);
            ReduceWetDuration(target, 120);
        }
        private void ReduceWetDuration(NPC target, int ticks = 120)
        {
            int index = target.FindBuffIndex(BuffID.Wet);
            if (index >= 0)
            {
                target.buffTime[index] -= ticks;
                if (target.buffTime[index] < 0)
                    target.DelBuff(index);
            }
        }
        private void ClearBurning(NPC target)
        {
            if (!BuffsCategories.DebuffLists.TryGetValue("Burning", out var burningDebuffs))
                return;

            foreach (int debuff in burningDebuffs)
            {
                int index = target.FindBuffIndex(debuff);
                if (index >= 0)
                    target.DelBuff(index);
            }
        }
        private void ShowSteamResonance(NPC target, int damage)
        {
            Color steamColor = Color.LightGray; 
            string damageText = $"{damage}";    
            CombatText.NewText(target.Hitbox, steamColor, damageText, dramatic: true);
        }
    }
}
