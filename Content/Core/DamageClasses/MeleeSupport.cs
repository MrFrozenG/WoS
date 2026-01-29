using Terraria.ModLoader;

namespace WoS.Content.Core.DamageClasses
{
    public class MeleeSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Melee)
                return new StatInheritanceData(0.5f, 1f, 0.5f, 1f, 1f);

            if (damageClass == ModContent.GetInstance<SupportClass>())
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return damageClass == Melee || damageClass == ModContent.GetInstance<SupportClass>();
        }
    }

    public class MeleeNoSpeedSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == MeleeNoSpeed)
                return new StatInheritanceData(0.5f, 1f, 0.5f, 1f, 1f);

            if (damageClass == ModContent.GetInstance<SupportClass>())
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return damageClass == MeleeNoSpeed || damageClass == ModContent.GetInstance<SupportClass>();
        }
    }
}
