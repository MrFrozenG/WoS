using Terraria.ModLoader;

namespace WoS.Content.Core.DamageClasses
{
    public class SummonSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Summon)
                return new StatInheritanceData(0.5f, 1f, 0.5f, 1f, 1f);

            if (damageClass == ModContent.GetInstance<SupportClass>())
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return damageClass == Summon || damageClass == ModContent.GetInstance<SupportClass>();
        }
    }
    public class SummonMeleeSpeedSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == SummonMeleeSpeed)
                return new StatInheritanceData(0.5f, 1f, 0.5f, 1f, 1f);

            if (damageClass == ModContent.GetInstance<SupportClass>())
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            return damageClass == SummonMeleeSpeed || damageClass == ModContent.GetInstance<SupportClass>();
        }
    }
}
