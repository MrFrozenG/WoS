using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Resources;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Items.Ammo.Bullets
{
    public class FrostBullet : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 10;
            Item.knockBack = 1f;
            Item.crit = 4;

            Item.consumable = true;
            Item.maxStack = Item.CommonMaxStack;

            Item.width = 8;
            Item.height = 16;

            Item.ammo = AmmoID.Bullet;
            Item.shootSpeed = 4f;
            Item.shoot = ModContent.ProjectileType<FrostBulletS>();

            Item.value = Item.sellPrice(0, 0, 0, 50);
            Item.rare = ItemRarityID.Blue;
        }
    }
}