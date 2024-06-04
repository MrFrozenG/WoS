using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.DamageClasses;

namespace WoS.Content.Items.Weapons.Forbidden
{
	public class Hive : ModItem
	{
		
		public static readonly int firstShotCost = 5;
		public static readonly int secondShotCost = 50;
		public static readonly int thirdShotCost = 100;
		
		public override void SetDefaults()
		{
			Item.DamageType = ModContent.GetInstance<ForbiddenClass>();;
			Item.damage = 11;
			Item.knockBack = 1f;
			Item.crit = 6;
			Item.noMelee = true;
			Item.mana = 3;
		
			Item.width = 74;
			Item.height = 28;
			Item.scale = 1f;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.useAnimation = 15;
			Item.useTime = 5;
			Item.reuseDelay = 15;
			Item.autoReuse = true;
			Item.UseSound = SoundID.Item11;
		
			Item.useAmmo = AmmoID.Bullet;
			Item.shoot = ProjectileID.PurificationPowder;
			Item.shootSpeed = 12.5f;
		
			Item.rare = ItemRarityID.Green;
			Item.value = Item.sellPrice(0, 5, 35, 0);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player) 
		{
			if (player.ItemUsesThisAnimation == 0)
				return Main.rand.NextFloat() >= firstShotCost / 100f;
			else if (player.ItemUsesThisAnimation == 1)
				return Main.rand.NextFloat() >= secondShotCost / 100f;
			else if (player.ItemUsesThisAnimation == 2)
				return Main.rand.NextFloat() >= thirdShotCost / 100f;
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-12f, 0f);
		}
		public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
		{
			velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
			if (type == ProjectileID.Bullet || type == ProjectileID.SilverBullet) 
			{
				type = ProjectileID.Bee; 
				Item.shootSpeed = 5.5f;
				Item.reuseDelay = 5;
			}
			else 
			{
				Item.reuseDelay = 15;
				Item.shootSpeed = 12.5f;
			}
		}
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
		{
			if (type != ProjectileID.Bee)
			{
				Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
			}
			else
			{
				for (int b = 0; b <  Main.rand.Next(2,4); b++) 
				{
					Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
					newVelocity *= 1f - Main.rand.NextFloat(0.2f);
					Projectile.NewProjectileDirect(source, position, newVelocity, type, Item.damage/2, knockback, player.whoAmI);
					if (player.strongBees && Main.rand.NextBool(4))
					{
						for (int c = 0; c <  Main.rand.Next(0,2); c++) 
						{
							Projectile.NewProjectileDirect(source, position, velocity, ProjectileID.GiantBee, Item.damage, knockback, player.whoAmI);
						}
					}
				}
			}
		return false;
		}
	}
}