using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WoS.Content.Core.Changes
{
    public class MainConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("Weapons")]
        /// <summary>
        /// Sky Fracture Rework: 
        /// Sky Fracture has a 25% chance, or always on critical hits, to spawn a Celestial Blade.
        ///
        /// Each active Celestial Blade increases:
        ///		• Magic damage by 2%
        ///		• Sky Fracture damage by 4%
        ///        Alt Fire:
        ///        Recall all Celestial Blades to the cursor position.
		///        Grants a buff that increases magic damage by 45%.

        /// </summary>
        [DefaultValue(true)]
        [ReloadRequired]
        public bool SkyFractureCelestialBlessing { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool ExplosivesAutouse { get; set; }


        [Increment(5)]
        [Range(10, 125)]
        [DefaultValue(25)]
        [Slider]
        public int ExplosivesThrowSpeed { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool ThrowingWeaponsAutouse { get; set; }


        [Increment(5)]
        [Range(10, 125)]
        [DefaultValue(15)]
        [Slider]
        public int ThrowingWeaponsSpeed { get; set; }


        [DefaultValue(false)]
        [ReloadRequired]
        public bool IceSickleDeepCold { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool MeteorStaffNoCeiling { get; set; } 

        [DefaultValue(true)]
        [ReloadRequired]
        public bool StarfuryNoCeiling { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool BlizzardStaffNoCeiling { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool ToxicFlaskPoisonousBreath { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool VampireKnivesBloodlust { get; set; }

        [Header("QoL")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ConsumabledBossItems { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool ClentaminatorSpeedBoost { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool TownNPCsInvincible { get; set; }


        [DefaultValue(true)]
        [ReloadRequired]
        public bool TownNPCsSpecialShop { get; set; }

        [Header("QoL_ModCompatibility")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ConsumabledBossItemsThorium { get; set; }

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ConsumabledBossItemsGensokyo { get; set; }
    }

    public class EnumsConfig
    {
        public enum PhoenixBlasterMode
        {
            Disabled,
            ChargeShot,
            UndyingSoul
        }
    }
}
