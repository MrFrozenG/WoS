using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Config;

namespace WoS.Content.Config.Weapons;

public class GrenadeEdit : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstatiation)
    {
        switch (item.type)
        {
            case ItemID.Grenade:
            case ItemID.StickyGrenade:
            case ItemID.BouncyGrenade:
            case ItemID.PartyGirlGrenade:
            case ItemID.Beenade:

            case ItemID.Bomb:
            case ItemID.StickyBomb:
            case ItemID.BouncyBomb:
            case ItemID.BombFish:
            case ItemID.DirtBomb:
            case ItemID.DirtStickyBomb:
            case ItemID.DryBomb:
            case ItemID.HoneyBomb:
            case ItemID.WetBomb:
            case ItemID.LavaBomb:
            case ItemID.ScarabBomb:

            case ItemID.Dynamite:
            case ItemID.StickyDynamite:
            case ItemID.BouncyDynamite:
                return true;
            default:
                return false;
        }
    }
    public override void SetDefaults(Item item)
    {
        if (ModContent.GetInstance<VanilaConfig>().ConfigExplosivesAutouse)
        {
            item.autoReuse = true;
        }
        item.useTime = item.useAnimation = ModContent.GetInstance<VanilaConfig>().ConfigExplosivesThrowSpeed;
        item.StatsModifiedBy.Add(Mod);
    }
    public override void UpdateInventory(Item item, Player player)
    {
        if (ModContent.GetInstance<VanilaConfig>().ConfigExplosivesAutouse)
        {
            item.autoReuse = true;
        }
        item.useTime = item.useAnimation = ModContent.GetInstance<VanilaConfig>().ConfigExplosivesThrowSpeed;
    }
}

