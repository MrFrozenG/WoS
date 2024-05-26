using Terraria.ModLoader;

namespace WoS.Content.Systems.SysPlayers;

public class WoSPlayerMagic : ModPlayer
{
	public bool SilverGoblet;
	public bool GoldenGoblet;

	public override void ResetEffects()
	{
		SilverGoblet = false;
		GoldenGoblet = false;
	}
	
	public override void UpdateEquips()
	{
		if (SilverGoblet) 
		{
			Player.GetDamage(DamageClass.Magic).Base += 3f;
			Player.GetDamage(DamageClass.Summon).Base += 3f;
			Player.statManaMax2 += 10;
			Player.manaCost -= 0.03f;
		}
		if (GoldenGoblet) 
		{
			Player.GetDamage(DamageClass.Magic).Base += 2f;
			Player.GetDamage(DamageClass.Summon).Base += 2f;
			Player.statManaMax2 += 10;
		}
	}
}
	