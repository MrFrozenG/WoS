using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Systems.DamageClasses;

public class OtherworldClass : DamageClass
{
	public override bool UseStandardCritCalcs => true;

	public override StatInheritanceData GetModifierInheritance(DamageClass damageClass)
	{
		if (damageClass == DamageClass.Generic)
		{
			return StatInheritanceData.Full;
		}
		return new StatInheritanceData(0f, 0f, 1f, 1f, 1f);
	}
	
	public override bool GetEffectInheritance(DamageClass damageClass)
	{
		return false;
	}

	public override void SetDefaultStats(Player player)
	{
		player.GetCritChance<OtherworldClass>() = -4f;
		player.GetArmorPenetration<OtherworldClass>() = 1f;
	}
}
	