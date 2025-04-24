using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Systems.Globals.GlobalNPCs;

public class GNPCsBuffs : GlobalNPC
{
    public override bool InstancePerEntity => true;
    public bool Bleeding;
    public bool Breaking;
    public int BleedingBonus = 0;
    public override void ResetEffects(NPC npc)
    {
        Bleeding = false;
        Breaking = false;
    }

    public override void UpdateLifeRegen(NPC npc, ref int damage)
    {
        int BleedingDamage = npc.lifeMax / 100 + BleedingBonus + 5;
        if (Bleeding)
        {
            if (npc.lifeRegen > 0)
            {
                npc.lifeRegen = 0;
            }
            npc.lifeRegen -= BleedingDamage * 2;
            npc.lifeRegen -= BleedingDamage * 3;

            if (damage < BleedingDamage)
            {
                damage = BleedingDamage;
            }
        }
    }

    public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
    {
        if (Breaking)
        {
            modifiers.CritDamage += 0.20f; 
        }
    }

    public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
    {
        if (Breaking)
        {
            modifiers.CritDamage += 0.20f; 
        }
    }
}
