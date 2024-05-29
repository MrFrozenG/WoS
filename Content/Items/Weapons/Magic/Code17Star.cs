using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace WoS.Content.Items.Weapons.Magic;

public class Code17Star : ModItem
{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Magic;
		Item.damage = 17;
		Item.knockBack = 1f;
		Item.crit = 12;
		Item.noMelee = true;
		Item.mana = 5;
	
		Item.width = 60;
		Item.height = 24;
		Item.scale = 1f;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useAnimation = 10;
		Item.useTime = 10;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item4;
		
		Item.shoot = ModContent.ProjectileType<CosmicEnergy>();
		Item.shootSpeed = 12f;
		
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 5, 35, 0);
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-12f, 0f);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(2));
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
	{
		Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
		newVelocity *= 1f - Main.rand.NextFloat(0.2f);
		Projectile.NewProjectileDirect(source, position, newVelocity, type, damage/2, knockback, player.whoAmI);
		Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
		return false;
	}
	public override void ModifyManaCost(Player player, ref float reduce, ref float mult) 
	{
			if (!Main.dayTime) 
			{
				mult *= 0f; 
			}
		}
}
	
public class CosmicEnergy : ModProjectile
{
	public override void SetStaticDefaults() 
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
	}
	
	public override void SetDefaults()
	{
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.aiStyle = 1;
		Projectile.friendly = true;
		Projectile.DamageType = DamageClass.Magic;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.light = 0.9f;
		Projectile.penetrate = 2;
		Projectile.scale = 0.5f;
		AIType = ProjectileID.Bullet;
	}
	
	public override void OnKill(int timeLeft) 
	{
		Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
		SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
	}
	public override void AI()
	{
		Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
		Projectile.rotation = Projectile.velocity.ToRotation();
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Projectile.penetrate--;
		if (Projectile.penetrate <= 2) Projectile.damage += (Projectile.damage*2);
	}
}