using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Items.Weapons.Ranged.Throw;

public class DevilFangs : ModItem
{		
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.damage = 22;
		Item.knockBack = 1.8f;			
		Item.crit = 9;
		Item.noMelee = true;

        Item.width = Item.height = 34;
        Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 13;
		Item.useTime = 13;
        Item.GetGlobalItem<ItemsUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/Throw/DevilFangs_glow").Value;

        Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
	
		Item.shoot = ModContent.ProjectileType<DevilFangsThrow>();
		Item.shootSpeed = 10f;
		
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 66, 6);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
	}
	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) 
	{
//		Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
		int NumProjectiles = Main.rand.Next(3,8);
		for (int i = 0; i < NumProjectiles; i++) 
		{
			Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(9));
			newVelocity *= 1f - Main.rand.NextFloat(0.3f);
			Projectile.NewProjectileDirect(source, position, newVelocity, type, damage/2, knockback, player.whoAmI);
		}
		return false;
	}
    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient(ModContent.ItemType<MoltenKnife>(), 3996)
        .AddTile(TileID.Anvils)
            .Register();
    }
}