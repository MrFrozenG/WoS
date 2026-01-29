using System.ComponentModel;
using Terraria.ModLoader.Config;
namespace WoS.Content.Core.Changes
{
    public class ModContentConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("NPCs")]
        [DefaultValue(true)]
        [ReloadRequired]
        public bool HunterAlwaysSellsMaterials { get; set; }
    }
}
