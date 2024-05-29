using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace WoS.Content.Projectiles;

public class Sunlight : ModProjectile
{
	public float CollisionWidth => 10f * ((ModProjectile)this).Projectile.scale;

	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(40f);
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 2;
		Projectile.tileCollide = false;
		Projectile.scale = 1f;
		Projectile.DamageType = DamageClass.Generic;
		Projectile.timeLeft = 200;
		Projectile.light = 1.2f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 100);
	}
	private void SetVisualOffsets() 
	{
		// 32 is the sprite size (here both width and height equal)
		const int HalfSpriteWidth = 40 / 2;
		const int HalfSpriteHeight = 40 / 2;

		int HalfProjWidth = Projectile.width / 2;
		int HalfProjHeight = Projectile.height / 2;
			
		DrawOriginOffsetX = 0;
		DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
		DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 120);
	}
	public override void AI()
	{
		Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
		SetVisualOffsets();
	}
}

public class SunlightA : ModProjectile
{
	public override string Texture => "WoS/Content/Projectiles/Sunlight";
	public float CollisionWidth => 10f * ((ModProjectile)this).Projectile.scale;

	public override void SetDefaults()
	{
		Projectile.Size = new Vector2(40f);
		Projectile.aiStyle = -1;
		Projectile.friendly = true;
		Projectile.penetrate = 4;
		Projectile.tileCollide = false;
		Projectile.scale = 1.3f;
		Projectile.DamageType = DamageClass.Melee;
		Projectile.timeLeft = 200;
		Projectile.light = 1.2f;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return new Color(255, 255, 255, 100);
	}
	private void SetVisualOffsets() 
	{
		// 32 is the sprite size (here both width and height equal)
		const int HalfSpriteWidth = 40 / 2;
		const int HalfSpriteHeight = 40 / 2;

		int HalfProjWidth = Projectile.width / 2;
		int HalfProjHeight = Projectile.height / 2;
			
		DrawOriginOffsetX = 0;
		DrawOffsetX = -(HalfSpriteWidth - HalfProjWidth);
		DrawOriginOffsetY = -(HalfSpriteHeight - HalfProjHeight);
	}
	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(BuffID.OnFire, 120);
	}
	public override void AI()
	{
		Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
		Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
		SetVisualOffsets();
	}
}