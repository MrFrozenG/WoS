using Terraria.ModLoader;
using Terraria;

namespace WoS.Content.Prefixes
{
    public class SupportPrefixGlobal : GlobalItem
    {
        public int supportPointsBonus = 0;

        public override bool InstancePerEntity => true;

        public override GlobalItem Clone(Item item, Item itemClone)
        {
            var clone = (SupportPrefixGlobal)base.Clone(item, itemClone);
            clone.supportPointsBonus = supportPointsBonus;
            return clone;
        }
    }
}
