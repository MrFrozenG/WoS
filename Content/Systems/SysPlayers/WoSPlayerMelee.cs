using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;
using System;
using Microsoft.Build.Construction;

namespace WoS.Content.Systems.SysPlayers;

public class WoSPlayerMelee : ModPlayer
{
	public bool FrostStone;

	public override void ResetEffects()
	{
		FrostStone = false;
	}
	public override void UpdateEquips()
	{
	}
	public override void ModifyHitNPCWithItem(Item item, NPC target, ref NPC.HitModifiers modifiers)
	{
		if (FrostStone && item.CountsAsClass(DamageClass.Melee))
		{
        if (Main.rand.Next(4) == 0)
			target.AddBuff(BuffID.Frostburn2, 360);
		else if (Main.rand.Next(2) == 0)
			target.AddBuff(BuffID.Frostburn2, 240);
		else
			target.AddBuff(BuffID.Frostburn, 120);
		}
	}
	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
	{
		if (FrostStone && proj.CountsAsClass(DamageClass.Melee))
		{
        if (Main.rand.Next(4) == 0)
			target.AddBuff(BuffID.Frostburn2, 360);
		else if (Main.rand.Next(2) == 0)
			target.AddBuff(BuffID.Frostburn2, 240);
		else
			target.AddBuff(BuffID.Frostburn2, 120);
		}
	}

    public override void MeleeEffects(Item item, Rectangle hitbox)
    {
        base.MeleeEffects(item, hitbox);
		if (FrostStone && item.CountsAsClass(DamageClass.Melee) && !item.noMelee && !item.noUseGraphic && Main.rand.Next(3) != 0)
		{
        int index = Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, DustID.IceTorch, item.velocity.X * 0.2f + (float) (item.direction * 3), item.velocity.Y * 0.2f, 100, Scale: 2.5f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X *= 2f;
        Main.dust[index].velocity.Y *= 2f;
		}
    }
}

public class WoSPlayerMeleeProjectiles : GlobalProjectile
{
	public override bool InstancePerEntity => true;
	
	public override void PostAI(Projectile projectile) 
	{
		Player player = Main.player[projectile.owner];
		
		if (player.GetModPlayer<WoSPlayerMelee>().FrostStone && projectile.CountsAsClass(DamageClass.Melee) && Main.rand.Next(3) != 0) 
		{
			Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.IceTorch, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 2f);

        }
	}
}