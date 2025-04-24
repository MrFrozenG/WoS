using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Vanila;

namespace WoS.Content.Config.Weapons;

public class PhoenixBlasterEdit : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.PhoenixBlaster;
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<VanilaConfig>().ReworkPhoenixBlaster;
    }

    public int Counter = 4;
    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.autoReuse = true;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Counter--;

        if (player.altFunctionUse == 2 && player.whoAmI == Main.myPlayer)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PhoenixBlasterShot>(), damage, knockback, player.whoAmI);
            Counter = 4;
        }

        return true;
    }

    public override bool AltFunctionUse(Item item, Player player)
    {
        return Counter <= 0;
    }

    public override bool CanUseItem(Item item, Player player)
    {
        if (player.altFunctionUse == 2)
        {
            if (Counter > 0)
                return true;
        }

        return true;
    }
}