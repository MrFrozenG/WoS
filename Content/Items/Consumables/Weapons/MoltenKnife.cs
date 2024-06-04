using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

namespace WoS.Content.Items.Consumables.Weapons;

public class MoltenKnife : ModItem
{		
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Ranged;
		Item.damage = 22;
		Item.knockBack = 1.8f;			
		Item.crit = 4;
		Item.noMelee = true;
		Item.maxStack = Item.CommonMaxStack;
			
		Item.width = 14;
		Item.height = 30;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 13;
		Item.useTime = 13;
		Item.noUseGraphic = true;
			
		Item.consumable = true;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
	
		Item.shoot = ModContent.ProjectileType<MoltenKnifeThrow>();
		Item.shootSpeed = 10f;
		
		Item.rare = ItemRarityID.Orange;
		Item.value = Item.sellPrice(0, 0, 1, 35);
	}
	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		velocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
	}
}

public class MoltenKnifeThrow : ModProjectile
{
	public override void SetStaticDefaults() 
	{
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}
	
	public int GravityDelayTimer 
	{
		get => (int)Projectile.ai[2];
		set => Projectile.ai[2] = value;
	}
	public override void SetDefaults()
	{
		Projectile.width = 16;
		Projectile.height = 14;
		Projectile.aiStyle = 0;
		Projectile.friendly = true;
		Projectile.hostile = false;
		Projectile.DamageType = DamageClass.Ranged;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.light = 0.1f;
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 120);
	}
	private const int GravityDelay = 45;
	public override void AI() 
	{
		Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
		Projectile.rotation = Projectile.velocity.ToRotation();
		
		if (GravityDelayTimer < GravityDelay) GravityDelayTimer++; 
		if (GravityDelayTimer >= GravityDelay) 
		{
			GravityDelayTimer = GravityDelay;
			Projectile.velocity.X *= 0.98f;
			Projectile.velocity.Y += 0.35f;
		}
		if (Main.rand.NextBool(2)) 
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
			dust.velocity += Projectile.velocity * 0.3f;
			dust.velocity *= 0.2f;
		}
		if (Main.rand.NextBool(4)) 
		{
			Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Lava, 0, 0, 254, Scale: 0.3f);
			dust.velocity += Projectile.velocity * 0.5f;
			dust.velocity *= 0.5f;
		}
	}
	public override void OnKill(int timeLeft) 
	{
		SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
		Vector2 usePos = Projectile.position;
		
		for (int i = 0; i < 20; i++) 
		{
			Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Lava);
			dust.position = (dust.position + Projectile.Center) / 2f;
			dust.velocity += usePos * 2f;
			dust.velocity *= 0.5f;
			dust.noGravity = true;
			usePos -= usePos * 8f;
		}
		if (Projectile.owner == Main.myPlayer) 
		{
			int item = 0;
			if (Main.rand.NextBool(18)) 
			{
				item = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<MoltenKnife>());
			}
			if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0) 
			{
				NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
			}
		}
	}
}