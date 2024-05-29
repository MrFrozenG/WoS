using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

using WoS.Content.Systems.Globals;
using WoS.Content.Projectiles;

namespace WoS.Content.Items.Weapons.Magic;

public class TrustYourHeart : ModItem
{
	public override void SetStaticDefaults() 
	{
		ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
		ItemID.Sets.GamepadExtraRange[Item.type] = 15; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
		ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
	}
	public override void SetDefaults() 
	{
		Item.width = 30; 
		Item.height = 26;

		Item.useStyle = ItemUseStyleID.Shoot; 
		Item.useTime = 25; 
		Item.useAnimation = 25; 
		Item.noMelee = true;
		Item.noUseGraphic = true; 
		Item.UseSound = SoundID.Item1;

		Item.damage = 33; 
		Item.mana = 3;
		Item.DamageType = DamageClass.Magic; // The type of damage the weapon does. MeleeNoSpeed means the item will not scale with attack speed.d
		Item.knockBack = 2.5f; // The amount of knockback the item inflicts.
		Item.crit = 8; // The percent chance for the weapon to deal a critical strike. Defaults to 4.
		Item.channel = true; // Set to true for items that require the attack button to be held out (e.g. yoyos and magic missile weapons)
		Item.rare = ItemRarityID.Green;
		Item.value = Item.buyPrice(gold: 1);
		Item.value = Item.sellPrice(0, 5, 35, 0);

		Item.shoot = ModContent.ProjectileType<TrustYourHeartY>();
		Item.shootSpeed = 16f;	
	}
}
public class TrustYourHeartY : ModProjectile
{
	public int ManaCounter
		{
			get => (int)Projectile.ai[0];
			set => Projectile.ai[0] = value;
		}
	private int manaCounter;
	public override void SetStaticDefaults() 
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 180f;
		ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 8.5f;
		
		ProjectileID.Sets.TrailCacheLength[Type] = 4;
			ProjectileID.Sets.TrailingMode[Type] = 0;
	}
	
	public override void SetDefaults() 
	{
		Projectile.width = 16; 
		Projectile.height = 16; 
		Projectile.aiStyle = ProjAIStyleID.Yoyo; 
		Projectile.friendly = true; 
		Projectile.DamageType = DamageClass.Magic; 
		Projectile.penetrate = -1;
	}
	
	public override void AI()
	{
		for (int i = 0; i < 2; i++)
		{
			Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.PurificationPowder, Scale: 0.6f);
			dust.noGravity = true;

			Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
			dust.velocity = vector;
			dust.position = Projectile.Center - (vector * 24f);
		}
		
		Player owner = Main.player[Projectile.owner];
		if (owner.channel && owner.statMana > 0)
		{
			manaCounter = ++manaCounter % 60;
			if (manaCounter % (60 / 10) == 0)	owner.statMana--;

			if (owner.whoAmI == Main.myPlayer && /*owner.controlUseTile &&*/ Projectile.frameCounter <= 0)
			{
				var target = Main.npc.Where(x => x.Distance(Projectile.Center) < 640 && x.CanBeChasedBy(Projectile) 
					&& Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, x.position, x.width, x.height)).OrderBy(x => x.Distance(Projectile.Center)).FirstOrDefault();
				if (target != default)
				{
					CastMagic(Projectile.DirectionTo(target.Center) * 10);
					Projectile.velocity = Projectile.DirectionFrom(target.Center) * 8;
					Projectile.frameCounter = 20;
					Projectile.netUpdate = true;
				}
			}
			if (Projectile.frameCounter > 0)	Projectile.frameCounter--;
		}
		if (owner.statMana <= 0) owner.channel = false;
	}
	private void CastMagic(Vector2 velocity)
	{
		int type = ModContent.ProjectileType<Sunlight>();
		Player owner = Main.player[Projectile.owner];
		owner.statMana = System.Math.Max(owner.statMana - 3, 0);

		Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity, type, Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 0f, 0f);
		proj.friendly = true;
		proj.hostile = false;
		proj.netUpdate = true;
	}
}