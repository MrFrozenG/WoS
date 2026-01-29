using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.Changes;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_Throwable : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstatiation)
        {
            switch (item.type)
            {
                case ItemID.ThrowingKnife:
                case ItemID.PoisonedKnife:
                case ItemID.BoneDagger:
                case ItemID.SpikyBall:
                case ItemID.Shuriken:
                case ItemID.Bone:
                case ItemID.StarAnise:
                case ItemID.RottenEgg:
                    return true;
                default:
                    return false;
            }
        }
        public override void SetDefaults(Item item)
        {
            if (ModContent.GetInstance<MainConfig>().ThrowingWeaponsAutouse)
            {
                item.autoReuse = true;
            }
            if (item.type != ItemID.RottenEgg || item.type != ItemID.Bone)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed;
            }
            if (item.type == ItemID.RottenEgg)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed + 4;
            }
            if (item.type == ItemID.Bone)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed - 3;
            }

            item.StatsModifiedBy.Add(Mod);
        }
        public override void UpdateInventory(Item item, Player player)
        {
            if (ModContent.GetInstance<MainConfig>().ThrowingWeaponsAutouse)
            {
                item.autoReuse = true;
            }
            if (item.type != ItemID.RottenEgg || item.type != ItemID.Bone)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed;
            }
            if (item.type == ItemID.RottenEgg)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed + 4;
            }
            if (item.type == ItemID.Bone)
            {
                item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed - 3;
            }
        }


    }
}
