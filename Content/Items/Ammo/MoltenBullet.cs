using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Items.Ammo;

public class MoltenBullet : ModItem
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
		Item.shootSpeed = 16f;
		Item.shoot = ModContent.ProjectileType<MoltenBulletShot>();
		
		Item.value = Item.sellPrice(0, 0, 5, 5);
		Item.rare = ItemRarityID.Green;
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

public class MoltenBulletShot : ModProjectile
{
	public override void SetStaticDefaults() 
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}
	
	public override void SetDefaults()
	{
		Projectile.width = 2;
		Projectile.height = 2;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.light = 0.1f;
		AIType = ProjectileID.Bullet;
	}
	
	public override void OnKill(int timeLeft) 
	{
		Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
	}
	
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 120);
	}
	public override void AI()
	{
		Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
}