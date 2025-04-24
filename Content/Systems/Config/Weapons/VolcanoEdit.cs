using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Vanila;

namespace WoS.Content.Config.Weapons;

public class VolcanoEdit : GlobalItem
{
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.FieryGreatsword;
    }
    public override bool IsLoadingEnabled(Mod mod)
    {
        return ModContent.GetInstance<VanilaConfig>().ReworkVolcano;
    }
    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.noMelee = true;
        item.shoot = ModContent.ProjectileType<VolcanoSlash>();
        item.shootsEveryUse = true;
        item.autoReuse = true;

        item.damage = 30;

        //		item.useAnimation *= 2;
        //		item.useTime *= 2;
    }
    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        float adjustedItemScale = player.GetAdjustedItemScale(item); // Get the melee scale of the player and item.
        Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction, 0f), type, damage, knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax, adjustedItemScale);
        NetMessage.SendData(MessageID.PlayerControls, -1, -1, null, player.whoAmI); // Sync the changes in multiplayer.
                                                                                    //		Projectile.NewProjectileDirect(source, player.Center, velocity, type, damage, knockback, player.whoAmI);
        return false;
    }
}