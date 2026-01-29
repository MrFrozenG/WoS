using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.Changes;

namespace WoS.Content.Core.Changes.Weapons;

public class VC_Explosives : GlobalItem
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
            case ItemID.MolotovCocktail:

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
        if (ModContent.GetInstance<MainConfig>().ExplosivesAutouse)
        {
            item.autoReuse = true;
        }
        if (item.type != ItemID.MolotovCocktail)
        {
            item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ExplosivesThrowSpeed;
        }
        else item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ExplosivesThrowSpeed + 15;

        item.StatsModifiedBy.Add(Mod);
    }
    public override void UpdateInventory(Item item, Player player)
    {
        if (ModContent.GetInstance<MainConfig>().ExplosivesAutouse)
        {
            item.autoReuse = true;
        }
        if (item.type != ItemID.MolotovCocktail)
        {
            item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ExplosivesThrowSpeed;
        }
        else item.useTime = item.useAnimation = ModContent.GetInstance<MainConfig>().ExplosivesThrowSpeed + 15;
    }
}

