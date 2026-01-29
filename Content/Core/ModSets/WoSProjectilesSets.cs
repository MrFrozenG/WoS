using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Weapons.Magic;

namespace WoS.Content.Core.ModSets
{
    [ReinitializeDuringResizeArrays]
    public static class WoSProjectilesSets
    {
        public static bool[] IsProjectileBreachEffected = ProjectileID.Sets.Factory.CreateNamedSet("IsProjectileBreachEffected")
            .Description("[WoS] Group for 'Breach Effect' mechanic")
            .RegisterBoolSet(false,
             ProjectileID.Flames,
             ProjectileID.Sunfury,
             ProjectileID.FlamingMace,
             ProjectileID.DD2FlameBurstTowerT1Shot,
             ProjectileID.DD2FlameBurstTowerT2Shot,
             ProjectileID.DD2FlameBurstTowerT3Shot
            );

        public static bool[] ProjectileWetStatus = ProjectileID.Sets.Factory.CreateNamedSet("ProjectileWetStatus")
            .Description("[WoS] Projectile's that can add Wet Status on Hit")
            .RegisterBoolSet(false,
             ProjectileID.WaterBolt,
             ProjectileID.WaterStream,
             ProjectileID.Bubble,
             ProjectileID.Typhoon,
             ProjectileID.Flairon,
             ProjectileID.FlaironBubble,
             ProjectileID.RainCloudRaining,
             ProjectileID.RainFriendly
            );

        public static bool[] IsProjectileShockRelated = ProjectileID.Sets.Factory.CreateNamedSet("IsProjectileShockRelated")
            .Description("[WoS] Group for 'Shock Resonance' mechanic")
            .RegisterBoolSet(false, 
            ProjectileID.Gungnir);
    }
}
