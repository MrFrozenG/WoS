using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

using WoS.Content.Systems.Globals;
using WoS.Content.Items.Weapons.Melee;
using WoS.Content.Projectiles.Weapons.Magic;
using Terraria.DataStructures;

namespace WoS.Content.Items.Weapons.Magic;

public class TrustYourHeart : ModItem
{
	public override void SetStaticDefaults() 
	{
		ItemID.Sets.Yoyo[Item.type] = true; 
		ItemID.Sets.GamepadExtraRange[Item.type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; 
	}
	public override void SetDefaults() 
	{
		Item.width = 30; 
		Item.height = 26;

		Item.useStyle = ItemUseStyleID.Shoot; 
		Item.useTime = 20; 
		Item.useAnimation = 20; 
		Item.noMelee = true;
		Item.noUseGraphic = true; 
		Item.UseSound = SoundID.Item1;

		Item.damage = 23; 
		Item.mana = 15;
		Item.DamageType = DamageClass.Magic;
		Item.knockBack = 2.5f; 
		Item.crit = 8; 
		Item.channel = true; 
		Item.rare = ItemRarityID.Green;
		Item.value = Item.buyPrice(gold: 1);
		Item.value = Item.sellPrice(0, 5, 35, 0);

		Item.shoot = ModContent.ProjectileType<TrustYourHeartY>();
		Item.shootSpeed = 16f;	
	}

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
        Main.projectile[proj].localAI[0] = Item.mana;   //Затраты маны на особую атаку
        return false; // Создали вручную
    }
}