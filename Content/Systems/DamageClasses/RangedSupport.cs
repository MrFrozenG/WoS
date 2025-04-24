using Terraria.ModLoader;

namespace WoS.Content.Systems.DamageClasses
{
    public class RangedSupport : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Ranged || damageClass == ModContent.GetInstance<SupportClass>())
            {
                return StatInheritanceData.Full;
            }
            return new StatInheritanceData(1f, 1f, 1f, 1f, 1f);
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            if (damageClass == DamageClass.Ranged || damageClass == ModContent.GetInstance<SupportClass>())
                return true;
            return false;
        }
    }
}
