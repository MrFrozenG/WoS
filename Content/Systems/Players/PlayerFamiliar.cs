using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WoS.Content.Config;

namespace WoS.Content.ModPlayers
{
    public class PlayerFamiliar : ModPlayer
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<ContentConfig>().FamiliarsSystem;
        }
    }
}
