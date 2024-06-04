using Terraria.ModLoader;
using Terraria.ID;
using Terraria;
using Terraria.DataStructures;

using Microsoft.Xna.Framework;
using System;

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
			target.AddBuff(BuffID.Frostburn, 360);
		else if (Main.rand.Next(2) == 0)
			target.AddBuff(BuffID.Frostburn, 240);
		else
			target.AddBuff(BuffID.Frostburn, 120);
		}
	}
	public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
	{
		if (FrostStone && proj.CountsAsClass(DamageClass.Melee))
		{
        if (Main.rand.Next(4) == 0)
			target.AddBuff(BuffID.Frostburn, 360);
		else if (Main.rand.Next(2) == 0)
			target.AddBuff(BuffID.Frostburn, 240);
		else
			target.AddBuff(BuffID.Frostburn, 120);
		}
	}

/*	public override void MeleeEffects(Item sItem)
	{
		if (FrostStone && sItem.melee && !sItem.noMelee && !sItem.noUseGraphic && Main.rand.Next(3) != 0)
		{
        int index = Dust.NewDust(new Vector2((float) itemRectangle.X, (float) itemRectangle.Y), itemRectangle.Width, itemRectangle.Height, 6, this.velocity.X * 0.2f + (float) (this.direction * 3), this.velocity.Y * 0.2f, 100, Scale: 2.5f);
        Main.dust[index].noGravity = true;
        Main.dust[index].velocity.X *= 2f;
        Main.dust[index].velocity.Y *= 2f;
		}
		return itemRectangle;
	}*/

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
	