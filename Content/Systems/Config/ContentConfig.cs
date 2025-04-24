using System.ComponentModel;
using Terraria.ModLoader.Config;

namespace WoS.Content.Config
{
    public class ContentConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ServerSide;
        [Header("Familiars")]
        [DefaultValue(true)]
        public bool FamiliarsSystem { get; set; }
    }
}
