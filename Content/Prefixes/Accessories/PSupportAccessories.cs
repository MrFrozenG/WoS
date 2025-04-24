using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.ModPlayers;

namespace WoS.Content.Prefixes.Accessories
{
    [LegacyName("AidingPrefix")]
    public class Aiding : PSupportAccessories
    {
        public override int SupportPointsBonus => 1;
        public override float RollChance(Item item)
        {
            return 4.85f;
        }
    }

    [LegacyName("SupportivePrefix")]
    public class Supportive : PSupportAccessories
    {
        public override int SupportPointsBonus => 2;
        public override float RollChance(Item item)
        {
            return 4.75f;
        }
    }

    [LegacyName("EncouragingPrefix")]
    public class Encouraging : PSupportAccessories
    {
        public override int SupportPointsBonus => 4;
        public override float RollChance(Item item)
        {
            if (NPC.downedMechBossAny)
            {
                return 4.85f;
            }
            return 0f;
        }
    }

    [LegacyName("BeneficialPrefix")]
    public class Beneficial : PSupportAccessories
    {
        public override int SupportPointsBonus => 8;
        public override float RollChance(Item item)
        {
            if (NPC.downedMechBossAny)
            {
                return 4.85f;
            }
            return 0f;
        }
    }

    public abstract class PSupportAccessories : ModPrefix, ILocalizedModType
    {
        public new string LocalizationCategory => "Prefixes.Accessories.Support";

        public virtual int SupportPointsBonus => 0;
        public override PrefixCategory Category => PrefixCategory.Accessory;

        public override void ApplyAccessoryEffects(Player player)
        {
            player.GetModPlayer<MainPlayer>().SupportPointsBonus += SupportPointsBonus;
        }

        public override void ModifyValue(ref float valueMult)
        {
            float extraValue = 1f + (1.2f * SupportPointsBonus);
            valueMult *= extraValue;
        }

        public override IEnumerable<TooltipLine> GetTooltipLines(Item item)
        {
            if (SupportPointsBonus != 0)
            {
                //                string localized = Language.GetTextValue("Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName");
                string localized = Language.GetTextValue("Mods.WoS.DamageClasses.SupportClass.SupportPoints.DisplayName");
                string sign = SupportPointsBonus >= 0 ? "+" : "";
                yield return new TooltipLine(Mod, "SupportPrefixPointsBonus", $"{sign}{SupportPointsBonus} {localized}")
                {
                    IsModifier = true,
                    IsModifierBad = SupportPointsBonus < 0
                };
            }
        }
    }
}
