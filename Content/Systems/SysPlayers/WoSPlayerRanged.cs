using Terraria.ModLoader;

namespace WoS.Content.Systems.SysPlayers;

public class WoSPlayerRanged: ModPlayer
{
	public bool Acc1;

	public bool Acc2;

	public override void ResetEffects()
	{
		Acc1 = false;
		Acc2 = false;
	}
	
	public override void UpdateEquips()
	{
		 if (Acc1) Player.GetDamage(DamageClass.Generic).Base += 1f;
	}
}
	