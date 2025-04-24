using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Ammo;

namespace WoS.Content.Items.Ammo.Bullets;
public class MoltenBullet : ModItem
{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.damage = 11;
		Item.knockBack = 1f;
		Item.crit = 4;
		
		Item.consumable = true;
		Item.maxStack = Item.CommonMaxStack;
		
		Item.width = 8;
		Item.height = 16;

		Item.ammo = AmmoID.Bullet;
		Item.shootSpeed = 4f;
		Item.shoot = ModContent.ProjectileType<MoltenBulletS>();
		
		Item.value = Item.sellPrice(silver: 1);
		Item.rare = ItemRarityID.Orange;
	}

	public override void AddRecipes()
	{
		CreateRecipe(333)
		.AddIngredient(ItemID.MusketBall, 333)
		.AddIngredient(ItemID.HellstoneBar)
		.AddTile(TileID.Anvils)
			.Register();
	}
}

public class MoltenBulletPouch : ModItem
{
	public override void SetDefaults()
	{
		Item.CloneDefaults(ModContent.ItemType<MoltenBullet>());
		Item.damage = 11;
		Item.consumable = false;
		Item.maxStack = 1;
		
		Item.width = 26;
		Item.height = 34;
		
		Item.shoot = ModContent.ProjectileType<MoltenBulletS>();
		
		Item.value = Item.sellPrice(0, 0, 5, 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe()
		.AddIngredient(ModContent.ItemType<MoltenBullet>(), 9999)
		.AddTile(TileID.Anvils)
			.Register();
	}
}