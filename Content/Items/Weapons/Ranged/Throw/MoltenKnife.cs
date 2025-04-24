using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;

namespace WoS.Content.Items.Weapons.Ranged.Throw;

public class MoltenKnife : ModItem
{		
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.damage = 22;
		Item.knockBack = 1.8f;			
		Item.noMelee = true;
		Item.maxStack = Item.CommonMaxStack;
			
		Item.width = 14;
		Item.height = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 13;
		Item.useTime = 13;
		Item.noUseGraphic = true;
			
		Item.consumable = true;
		Item.UseSound = SoundID.Item1;
	
		Item.shoot = ModContent.ProjectileType<MoltenKnifeThrow>();
		Item.shootSpeed = 10f;
		
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 1, 35);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
	}
    public override void AddRecipes()
    {
        CreateRecipe(150)
        .AddIngredient(ItemID.HellstoneBar)
        .AddTile(TileID.Anvils)
            .Register();
        var resultItem = this;
        resultItem.CreateRecipe(200)
        .AddIngredient(ItemID.ThrowingKnife, 200)
        .AddIngredient(ItemID.HellstoneBar)
        .AddTile(TileID.Anvils)
            .Register();
    }
}