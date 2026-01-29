using Terraria.ModLoader;

namespace WoS.Content.Core.DamageClasses
{
    public class SupportClass : DamageClass
    {
        public override bool UseStandardCritCalcs => true;

        public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
        {
            if (damageClass == Generic)
                return StatInheritanceData.Full;

            return StatInheritanceData.None;
        }

        public override bool GetEffectInheritance(DamageClass damageClass)
        {
            // Эффекты распространяем только на Generic
            return damageClass == Generic;
        }
    }
}
