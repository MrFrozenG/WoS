using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Weapons.AED.Magic;
using WoS.Content.Items.Weapons.Magic.Grimoires;
using WoS.Content.Items.Weapons.Melee.DualBlades;

namespace WoS.Content.Core.ModSets
{
    [ReinitializeDuringResizeArrays]
    public static class WoSItemsSets
    {
        public static bool[] GrimoireWeapon = ItemID.Sets.Factory.CreateNamedSet("GrimoireWeapon")
            .Description("[WoS] Grimoire weapons group. Used for uncommon magic books")
            .RegisterBoolSet(false, 
            ModContent.ItemType<FrostbrandGrimoire>()
            );
        public static bool[] DualBladesWeapon = ItemID.Sets.Factory.CreateNamedSet("DualBladesWeapon")
            .Description("[WoS] Dual blades weapons group")
            .RegisterBoolSet(false, 
            ModContent.ItemType<MoltenDualBlades>(),
            ModContent.ItemType<VileSpreader>(),
            ModContent.ItemType<BoneBreaker>()
            );
        public static bool[] IsAED = ItemID.Sets.Factory.CreateNamedSet("IsAED")
            .Description("[WoS] Group for 'Awakened Exalted Destiny' weapons & tools")
            .RegisterBoolSet(false,
            ModContent.ItemType<StaffofConstellations>()
            );
        public static bool[] ItemWetStatus = ItemID.Sets.Factory.CreateNamedSet("ItemWetStatus")
            .Description("[WoS] Item's that can add Wet Status on Hit")
            .RegisterBoolSet(false,
            ItemID.WaterBolt,
            ItemID.AquaScepter,
            ItemID.BubbleGun,
            ItemID.NimbusRod,
            ItemID.Flairon,
            ItemID.RazorbladeTyphoon
            );

        public static bool[] IsItemShockRelated = ItemID.Sets.Factory.CreateNamedSet("IsItemShockRelated")
           .Description("[WoS] Group for 'Shock Resonance' mechanic")
           .RegisterBoolSet(false,
           ItemID.Gungnir);
    }
}
