using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals;
using WoS.Content.Projectiles;

namespace WoS.Content.Items.Weapons.Melee;

public class Sunshine : ModItem
{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Melee;
		Item.damage = 33;
		Item.knockBack = 1.3f;
		Item.crit = 16;

		Item.width = 56;
		Item.height = 56;
		Item.scale = 1.1f;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 30;
		Item.useTime = 24;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item71;
		Item.GetGlobalItem<ItemUseGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Melee/Sunshine_glow").Value;
		
		Item.shoot = ModContent.ProjectileType<SunlightA>();
		Item.shootSpeed = 5.3f;
		
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 5, 35, 0);
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
	{
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (player.ownedProjectileCounts[type = ModContent.ProjectileType<Sunlight>()] < 6)
		{
			type = ModContent.ProjectileType<Sunlight>();
			damage = Item.damage;
			const int NumProjectiles = 3;
			for (int i = 0; i < NumProjectiles; i++) 
			{
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
				newVelocity *= 1f - Main.rand.NextFloat(0.2f);
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage/2, knockback, player.whoAmI);
			}
		}
		return false;
	}
}