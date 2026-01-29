using System.Linq;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils.ItemDropUtils;
using WoS.Content.Items.Ammo.Bullets;
using WoS.Content.Items.Weapons.Magic.Grimoires;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Core.ModItems
{
    public class FishingCratesLoot : GlobalItem
    {
        public override void ModifyItemLoot(Item item, ItemLoot itemLoot)
        {
            switch (item.type)
            {
                case ItemID.JungleFishingCrate:
                case ItemID.JungleFishingCrateHard:
                    
                    {
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<HiveBullet>(), 4, 25, 68));
                        // itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<>(), 3, 25, 68));
                    }
                    break;

                case ItemID.FrozenCrate:
                case ItemID.FrozenCrateHard:
                    {
                        int[] themedDrops = [
                        ItemID.IceSickle,
                        ModContent.ItemType<FrostbrandGrimoire>()
                        ];
                        itemLoot.Add(ItemDropRule.OneFromOptionsNotScalingWithLuck(4, themedDrops));
                    }
                    break;
                case ItemID.LavaCrate:
                case ItemID.LavaCrateHard:
                     // Включаем вложенные правила
                    {
                        itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<MoltenBullet>(), 4, 25, 68));
                        //itemLoot.Add(ItemDropRule.ByCondition(new DownedBoss1DropCondition(), ModContent.ItemType<FrostbrandGrimoire>(), 5, 1));
                    }
                    break;
            }
        }
    }
}
