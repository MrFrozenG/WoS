using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Ammo.Bullets;

namespace WoS.Content.Systems.Globals.GlobalItems
{
    internal class FishingCratesLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            if (item.type == ItemID.JungleFishingCrate || item.type == ItemID.JungleFishingCrateHard)
            {
                foreach (var rule in itemLoot.Get())
                {
                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HiveBullet>(), 4, 25, 68));
//                    itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<>(), 3, 25, 68));
                }
            }
        }
    }
}
