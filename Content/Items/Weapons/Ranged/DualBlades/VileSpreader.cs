using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WoS.Content.Projectiles.Weapons.Ranged.DualBlades;

namespace WoS.Content.Items.Weapons.Ranged.DualBlades
{
    public class VileSpreader : ModItem
    {
        public override void SetDefaults()
        {
            //Static for Dual Blades:
            Item.damage = 22;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.PurificationPowder; //Because we don't use any base shot
            Item.shootSpeed = 8f;

            //Changeable values
            Item.DamageType = DamageClass.Ranged;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            Item.width = Item.height = 42;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 55, 30);

            Item.knockBack = 1.8f;
            Item.crit = 2;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.DemoniteBar, 15)
            .AddIngredient(ItemID.ShadowScale, 5)
            .AddIngredient(ItemID.Bone, 24)
            .AddIngredient(ItemID.VileMushroom, 5)
            .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool RangedPrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.whoAmI = Main.myPlayer;
            if (player.ownedProjectileCounts[ModContent.ProjectileType<VileSpreaderThrow>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<DemoniteBlade>()] == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<VileBlade>()] == 0)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<VileSpreaderThrow>(), damage, knockback, player.whoAmI, 0);
            }
            return false;
        }
    }
}

/*if (player.altFunctionUse != 2)
{
if (player.ownedProjectileCounts[FirstBlade] < 1)
{
Projectile.NewProjectileDirect(source, position, velocity, FirstBlade, FirstBladeDamage, knockback, player.whoAmI, 0);
}
}
if (player.altFunctionUse == 2)
{
if (player.ownedProjectileCounts[SecondBlade] < 1)
{
Projectile.NewProjectileDirect(source, player.Center, velocity, SecondBlade, SecondBladeDamage, knockback, player.whoAmI);
}
}
*/
