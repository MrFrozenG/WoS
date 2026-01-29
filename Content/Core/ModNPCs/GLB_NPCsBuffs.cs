using Microsoft.Xna.Framework;
using System.Reflection.Metadata.Ecma335;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Damage;
using WoS.Content.Buffs.Weakness;

namespace WoS.Content.Core.ModNPCs;

public class GLB_NpcsBuffs : GlobalNPC
{
    public override bool InstancePerEntity => true;
    /// <summary>
    /// Deals bleeding damage to the enemy over time.
    /// Уменьшает здоровье цели на 25% от max life + бонусы. Активно пока действует.
    /// </summary>
    public bool lifeDebt;

    /// <summary>
    /// Stronger bleeding effect than LifeDebt.
    /// Уменьшает здоровье цели на 50% от max life + бонусы. Активно пока действует.
    /// </summary>
    public bool Bloodlust;

    /// <summary>
    /// Increases critical damage taken by the enemy by 20%.
    /// Увеличивает получаемый критический урон целью на 20%.
    /// </summary>
    public bool Breaking;

    /// <summary>
    /// Increases all damage taken by the enemy by 15%.
    /// Увеличивает получаемый целью урон на 15%.
    /// </summary>
    public bool PearlescentWeakness;

    /// <summary>
    /// Reduces enemy defense by 10% and reduces contact damage dealt by 25%.
    /// Понижает защиту цели на 10% и наносимый ею контактный урон на 25%.
    /// </summary>
    public bool DivineIntervention;

    /// <summary>
    /// Deals damage when the enemy moves. Damage depends on enemy speed.
    /// Наносит урон если цель двигается. Урон зависит от скорости цели. Накапливает Заряд Шока (1) при нанесении урона.
    /// </summary>
    public bool Discharge;

    /// <summary>
    /// Enhanced version of Discharge. Deals more damage and generates more Shock Resonance charges.
    /// Усиленная версия Discharge. Наносит урон если цель двигается. Урон зависит от скорости цели. Накапливает Заряд Шока (3) при нанесении урона.
    /// </summary>
    public bool HighVoltage;

    /// <summary>
    /// Additional bleeding damage for LifeDebt.
    /// Дополнительный урон при LifeDebt.
    /// </summary>
    public int BleedingBonus = 0;

    /// <summary>
    /// Additional bleeding damage for Bloodlust.
    /// Дополнительный урон при Bloodlust.
    /// </summary>
    public int BleedingBonusVK = 0;
    public override void ResetEffects(NPC npc)
    {
        lifeDebt = false;
        Breaking = false;
        Bloodlust = false;
        Discharge = false;
        DivineIntervention = false;
        PearlescentWeakness = false;
    }
    public override void ModifyIncomingHit(NPC npc, ref NPC.HitModifiers modifiers)
    {
        if (DivineIntervention)
        {
            modifiers.Defense *= 0.9f; 
        }
        if (PearlescentWeakness)
        {
            modifiers.FinalDamage *= 1.15f;
        }
        if (Breaking)
        {
            modifiers.CritDamage += 0.20f;
        }
    }
    public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
    {
        if (DivineIntervention)
        {
            modifiers.FinalDamage *= 0.25f; 
        }
    }
    public override void DrawEffects(NPC npc, ref Color drawColor)
    {
        // This simple color effect indicates that the buff is active
        if (DivineIntervention)
        {
            //drawColor.G = 0;
        }
    }
    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        int lifePerc = npc.lifeMax / 100;
        int BleedingDamage = (int)(lifePerc * 0.25f + BleedingBonus + 10);
        int BleedingDamageVK = (int)(lifePerc * 0.5f + BleedingBonusVK + 10);
        if (lifeDebt)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= BleedingDamage * 2;

            if (damage < BleedingDamage)
            {
                damage = BleedingDamage;
            }
        }

        if (Bloodlust)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= BleedingDamageVK * 2;

            if (damage < BleedingDamageVK)
            {
                damage = BleedingDamageVK;
            }
        }

        if (Discharge)
        {
            if (npc.lifeRegen > 0)
                npc.lifeRegen = 0;

            float speed = npc.velocity.Length();
            int dischargeDamage = 0;

            if (speed <= 0.05f)
            {
                // Стоит
                dischargeDamage = 5;
            }
            else
            {
                // Двигается
                dischargeDamage = 25;

                // Быстро двигается → раз в 0.5 сек
                if (speed > 3.5f)
                {
                    if (Main.GameUpdateCount % 30 != 0)
                        dischargeDamage = 0;
                }
            }

            if (dischargeDamage > 0)
            {
                npc.lifeRegen -= dischargeDamage * 2;

                if (damage < dischargeDamage)
                    damage = dischargeDamage;

                ApplyShockResonance(npc);
            }
        }
    }
    private void ApplyShockResonance(NPC npc)
    {
        if (npc.realLife >= 0)
        {
            NPC main = Main.npc[npc.realLife];
            npc.GetGlobalNPC<GLB_NPCsStatuses>().ShockResonance_Charge++;
        }
        else
        {
            npc.GetGlobalNPC<GLB_NPCsStatuses>().ShockResonance_Charge++;
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
    }

    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
    }
}
