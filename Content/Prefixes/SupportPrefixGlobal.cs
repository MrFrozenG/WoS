using Terraria.ModLoader;
using Terraria;

namespace WoS.Content.Prefixes
{
    public class SupportPrefixGlobal : GlobalItem
    {
        public int supportPointsBonus = 0;

        public override bool InstancePerEntity => true;
    }
}
