using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.Changes;

namespace WoS.Content.Core.Changes.Misc
{ 
    public class SummonItemsEdit : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            switch (item.type)
            {
                case ItemID.SlimeCrown:
                case ItemID.SuspiciousLookingEye:
                case ItemID.MechanicalEye:
                case ItemID.Abeemination:
                case ItemID.WormFood:
                case ItemID.BloodySpine:
                case ItemID.DeerThing:
                case ItemID.QueenSlimeCrystal:
                case ItemID.MechanicalWorm:
                case ItemID.MechanicalSkull:
                case ItemID.CelestialSigil:
                case ItemID.GoblinBattleStandard:
                case ItemID.PirateMap:
                case ItemID.SolarTablet:
                case ItemID.PumpkinMoonMedallion:
                case ItemID.NaughtyPresent:
                case ItemID.BloodMoonStarter:
                case ItemID.MechdusaSummon:
                    return true;
                default:
                    return false;
            }
        }

        public override void SetDefaults(Item item)
        {
            int silver = 100;
            int gold = silver * 100;

            if (ModContent.GetInstance<MainConfig>().ConsumabledBossItems)
            {
                item.consumable = false;
                item.useTime = 60;
                item.StatsModifiedBy.Add(Mod);
            }

            item.value = Item.buyPrice(gold: 1, silver: 50);
            item.shopCustomPrice = item.value;
            item.shopSpecialCurrency = -1; 
            item.value = 0; 
        }
    }
}

