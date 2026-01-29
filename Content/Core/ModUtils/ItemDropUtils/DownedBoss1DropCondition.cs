using Terraria.GameContent.ItemDropRules;
using Terraria;

namespace WoS.Content.Core.ModUtils.ItemDropUtils
{
    public class DownedBoss1DropCondition : IItemDropRuleCondition
    {
        public bool CanDrop(DropAttemptInfo info)
        {
            return NPC.downedBoss1;
        }

        public bool CanShowItemDropInUI()
        {
            return true; // Показывать в справочниках
        }

        public string GetConditionDescription()
        {
            return "Drops after defeating the Eye of Cthulhu";
        }
    }
}
