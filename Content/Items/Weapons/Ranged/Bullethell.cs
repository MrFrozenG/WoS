using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Items.Guns
{
	public class Bullethell : ModItem
	{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.damage = 9;
		Item.knockBack = 1f;
		Item.crit = 4;
		Item.noMelee = true;
		
		Item.width = 74;
		Item.height = 28;
		Item.scale = 1f;
		Item.useStyle = 5;
		Item.useAnimation = 11;
		Item.useTime = 11;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item11;
		
		Item.useAmmo = AmmoID.Bullet;
		Item.shoot = 10;
		Item.shootSpeed = 5f;
		
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 5, 35, 0);
	}
	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.NextFloat() >= 0.25f;
	}
	public override Vector2? HoldoutOffset()
	{
		return new Vector2(-12f, 0f);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
	}
	}
}