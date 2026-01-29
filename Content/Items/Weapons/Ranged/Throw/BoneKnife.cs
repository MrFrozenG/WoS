using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;
using Microsoft.Xna.Framework;
using WoS.Content.Core.Changes;

namespace WoS.Content.Items.Weapons.Ranged.Throw
{
    public class BoneKnife : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 6;
            Item.knockBack = 1.8f;
            Item.crit = 16;
            Item.noMelee = true;
            Item.maxStack = Item.CommonMaxStack;

            Item.width = 14;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = Item.useTime = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed - 3;
            Item.noUseGraphic = true;

            Item.consumable = true;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<BoneKnifeThrow>();
            Item.shootSpeed = 10f;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 0, 10);
        }

        public override void UpdateInventory(Player player)
        {
            Item.useTime = Item.useAnimation = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed - 3;
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
        }

        public override void AddRecipes()
        {
            CreateRecipe(25)
            .AddIngredient(ItemID.Bone)
            .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
