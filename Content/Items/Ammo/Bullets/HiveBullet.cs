using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Items.Ammo.Bullets;
public class HiveBullet : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 5;
        Item.knockBack = 1f;
        Item.crit = 4;

        Item.consumable = true;
        Item.maxStack = Item.CommonMaxStack;

        Item.width = 8;
        Item.height = 16;

        Item.ammo = AmmoID.Bullet;
        Item.shootSpeed = 6f;
        Item.shoot = ModContent.ProjectileType<HiveBulletS>();

        Item.value = Item.sellPrice(0, 0, 0, 33);
        Item.rare = ItemRarityID.Blue;
    }
}