using Terraria.ModLoader;

namespace WoS.Content.Systems.SysPlayers;

public class WoSPlayerMelee : ModPlayer
{
	public bool Bloodgem;
	public bool BloodgemRing;

	public override void ResetEffects()
	{
		Bloodgem = false;
		BloodgemRing = false;
	}
	
	public override void UpdateEquips()
	{
		if (Bloodgem) 
		{
			Player.GetDamage(DamageClass.Melee).Base += 3f;
			Player.GetDamage(DamageClass.Ranged).Base += 3f;
			Player.statLifeMax2 += 10;
			Player.GetCritChance(DamageClass.Melee) += 1f;
			Player.GetCritChance(DamageClass.Ranged) += 1f;
		}
	}
}
	