using Terraria.ModLoader.Config;
using System.ComponentModel;

namespace WoS.Content.Config;

public class VanilaConfig : ModConfig
{
    public override ConfigScope Mode => ConfigScope.ServerSide;
    [Header("Weapons")]
    [DefaultValue(true)]
    public bool ReworkSpears { get; set; }

    [DefaultValue(true)] // This sets the configs default value.
    [ReloadRequired]
    public bool ReworkPhoenixBlaster { get; set; }

    [DefaultValue(true)] // This sets the configs default value.
    [ReloadRequired]
    public bool ReworkVolcano { get; set; }

    [DefaultValue(true)]
    public bool ConfigExplosivesAutouse { get; set; }

    [Increment(5)]
    [Range(10, 100)]
    [DefaultValue(25)]
    [Slider]
    public int ConfigExplosivesThrowSpeed { get; set; }

    [Header("Misc")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool ReworkSummonItems { get; set; }

    [DefaultValue(true)]
    [ReloadRequired]
    public bool ReworkClentaminator { get; set; }

    [Header("NPCs")]
    [DefaultValue(true)]
    [ReloadRequired]
    public bool InvincibleTownNPCS { get; set; }
}
