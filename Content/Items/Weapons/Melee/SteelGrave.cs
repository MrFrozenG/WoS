using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core;

namespace WoS.Content.Items.Weapons.Melee
{
    public class SteelGrave : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.75f;
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 12;
            Item.knockBack = 1.5f;
            Item.crit = 24;

            Item.width = Item.height = 60;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 30;
            Item.useTime = 30;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<SteelGraveShot>();
            Item.shootSpeed = 7.3f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddRecipeGroup(Recipes.anyTombstone, 4)
            .AddIngredient(RecipeGroupID.IronBar, 8)
            .AddIngredient(RecipeGroupID.Wood, 25)
            .AddCondition(Condition.InGraveyard)
            .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int NumProjectiles = Main.rand.Next(1, 4);
            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(9));
                newVelocity *= 1f - Main.rand.NextFloat(0.3f);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback * 1.5f, player.whoAmI);
            }
            return false;
        }
    }
}