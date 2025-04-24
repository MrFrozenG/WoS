using Terraria.ModLoader;

namespace WoS.Content.Systems.DamageClasses
{
    public class MeleeSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Melee || damageClass == ModContent.GetInstance<SupportClass>())
            {
                return StatInheritanceData.Full;
            }
            return new StatInheritanceData(1f, 1f, 1f, 1f, 1f);
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Melee || damageClass == ModContent.GetInstance<SupportClass>())
                return true;
            return false;
        }
    }

    public class MeleeNoSpeedSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.MeleeNoSpeed || damageClass == ModContent.GetInstance<SupportClass>())
            {
                return StatInheritanceData.Full;
            }
            return new StatInheritanceData(1f, 1f, 1f, 1f, 1f);
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.MeleeNoSpeed || damageClass == ModContent.GetInstance<SupportClass>())
                return true;
            return false;
        }
    }
}
