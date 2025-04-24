using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.Systems.DamageClasses;

namespace WoS.Content.Prefixes
{
    [LegacyName("SacredHelpPrefix")]
    public class SacredHelp : SupportWeaponPrefix
    {
        public override float damageMult => 1.15f;
        public override float useTimeMult => 0.50f;
        public override int supportPointsBonus => 12;
        public override float RollChance(Item item)
        {
            return 25f;
        }
    }

    [LegacyName("InspiredPrefix")]
    public class Inspired : SupportWeaponPrefix
    {
        public override float damageMult => 1.05f;
        public override float useTimeMult => 0.95f;
        public override int supportPointsBonus => 2;
        public override float valueMult => 1.5f;

        public override float RollChance(Item item)
        {
            return 25f;
        }
    }
    public abstract class SupportWeaponPrefix : ModPrefix, ILocalizedModType
    {
        public new string LocalizationCategory => "Prefixes.Weapon.Support";
        public virtual float damageMult => 1f;
        public virtual float useTimeMult => 1f;
        public virtual float shootSpeedMult => 1f;
        public virtual float knockbackMult => 1f;
        public virtual float scaleMult => 1f;
        public virtual float manaMult => 1f;

        public virtual float valueMult => 1f;
        public virtual int critBonus => 0;
        public virtual int supportPointsBonus => 0;

        public override PrefixCategory Category => PrefixCategory.AnyWeapon;

        public override bool CanRoll(Item item)
        {
            return (item.DamageType == ModContent.GetInstance<MagicSupport>())
                && item.maxStack == 1;
        }

        public override void Apply(Item item)
        {
            item.GetGlobalItem<SupportPrefixGlobal>().supportPointsBonus = supportPointsBonus;
            Main.NewText($"Support Points Bonus applied: {supportPointsBonus}");
        }

        public override void ModifyValue(ref float valueMult)
        {
            valueMult = this.valueMult;
        }

        public override void SetStats(ref float damageMult, ref float knockbackMult, ref float useTimeMult, ref float scaleMult, ref float shootSpeedMult, ref float manaMult, ref int critBonus)
        {
            damageMult = this.damageMult;
            useTimeMult = this.useTimeMult;
            critBonus = this.critBonus;
            shootSpeedMult = this.shootSpeedMult;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            if (supportPointsBonus != 0)
            {
//                string localized = Language.GetTextValue("Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName");
                string localized = Language.GetTextValue("Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName");
                string sign = supportPointsBonus >= 0 ? "+" : "";
                yield return new TooltipLine(Mod, "SupportPrefixPointsBonus", $"{sign}{supportPointsBonus} {localized}")
                {
                    IsModifier = true,
                    IsModifierBad = supportPointsBonus < 0
                };
            }
        }
    }
}
