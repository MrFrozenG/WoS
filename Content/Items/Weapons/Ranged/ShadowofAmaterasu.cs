using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using WoS.Content.Systems.Globals;
using WoS.Content.Items.Ammo;

namespace WoS.Content.Items.Weapons.Ranged
{
	public class ShadowofAmaterasu : ModItem
	{		
		public override void SetDefaults()
		{
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 15;
			Item.knockBack = 1f;
			Item.crit = 10;
			Item.noMelee = true;
			
			Item.width = 74;
			Item.height = 28;
			Item.scale = 1f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 13;
			Item.useTime = 13;
			Item.GetGlobalItem<ItemUseGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/ShadowofAmaterasu_glow").Value;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item11;
		
			Item.useAmmo = AmmoID.Bullet;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 15f;
			
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.sellPrice(0, 5, 35, 0);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return Main.rand.NextFloat() >= 0.35f;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 0f);
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
		{
			if (type == ProjectileID.Bullet) type = ModContent.ProjectileType<MoltenBulletShot>();
			const int NumProjectiles = 3;
			for (int i = 0; i < NumProjectiles; i++) 
			{
				Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
				newVelocity *= 1f - Main.rand.NextFloat(0.2f);
				Projectile.NewProjectileDirect(source, position, newVelocity, type, damage/2, knockback, player.whoAmI);
			}
		return false;
		}
	}
}