using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Projectiles.Ammo.Rockets;

namespace WoS.Content.Items.Ammo.Rockets
{
    public class BreakpointRocket : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 35;
            Item.knockBack = 1f;
            Item.crit = 4;

            Item.consumable = true;
            Item.maxStack = Item.CommonMaxStack;

            Item.width = 16;
            Item.height = 26;

            Item.ammo = AmmoID.Rocket;
            Item.shootSpeed = 10f;
            Item.shoot = ModContent.ProjectileType<BreakpointRocketShot>();

            Item.value = Item.sellPrice(0, 0, 4, 50);
            Item.rare = ItemRarityID.LightRed;
        }
    }
}
