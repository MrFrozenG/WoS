using Terraria.ID;
using Terraria.ModLoader;
using Terraria;

namespace WoS.Content.Config.Misc
{
    public class ClentaminatorEdit : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            switch (item.type)
            {
                case ItemID.Clentaminator:
                case ItemID.Clentaminator2:
                case ItemID.BlueSolution:
                case ItemID.DarkBlueSolution:
                case ItemID.DirtSolution:
                case ItemID.GreenSolution:
                case ItemID.PurpleSolution:
                case ItemID.RedSolution:
                case ItemID.SandSolution:
                case ItemID.SnowSolution:
                    return true;
                default:
                    return false;
            }
        }
        public override void SetDefaults(Item item)
        {
            if (ModContent.GetInstance<VanilaConfig>().ReworkClentaminator)
            {
                if (item.type == ItemID.Clentaminator)
                {
                    item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
                    item.shootSpeed *= 2;
                }
                if (item.type == ItemID.Clentaminator2)
                {
                    item.StatsModifiedBy.Add(Mod); // Notify the game that we've made a functional change to this item.
                    item.shootSpeed *= 3;
                }
                if (item.type == ItemID.BlueSolution ||
                    item.type == ItemID.DarkBlueSolution ||
                    item.type == ItemID.DirtSolution ||
                    item.type == ItemID.GreenSolution ||
                    item.type == ItemID.PurpleSolution ||
                    item.type == ItemID.RedSolution ||
                    item.type == ItemID.SandSolution ||
                    item.type == ItemID.SnowSolution
                    )
                {
                    item.value *= 3;
                }
            }
        }
    }
}
