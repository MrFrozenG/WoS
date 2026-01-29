using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WoS.Content.Projectiles.Weapons.Melee.DualBlades;

namespace WoS.Content.Items.Weapons.Melee.DualBlades
{
    public class BoneBreaker : ModItem
    {
        public override void SetDefaults()
        {
            //Static for Dual Blades:
            Item.damage = 27;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.PurificationPowder; //Because we don't use any base shot
            Item.shootSpeed = 11f;

            //Changeable values
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.reuseDelay = 2;
            Item.autoReuse = true;

            Item.width = Item.height = 42;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 55, 30);

            Item.knockBack = 1.8f;
            Item.crit = -2;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.CrimtaneBar, 15)
            .AddIngredient(ItemID.TissueSample, 5)
            .AddIngredient(ItemID.Bone, 24)
            .AddIngredient(ItemID.ViciousMushroom, 5)
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
            if (player.ownedProjectileCounts[ModContent.ProjectileType<BoneBreakerThrow>()] < 1 && player.ownedProjectileCounts[ModContent.ProjectileType<CrimtaneBlade>()] == 0 && player.ownedProjectileCounts[ModContent.ProjectileType<LivingBoneBlade>()] == 0)
            {
                Projectile.NewProjectileDirect(source, position, velocity, ModContent.ProjectileType<BoneBreakerThrow>(), damage, knockback, player.whoAmI, 0);
            }
            return false;
        }
    }
}
