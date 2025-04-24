using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Systems.DamageClasses;

<<<<<<<< Updated upstream:Content/Systems/DamageClasses/AncientClass.cs
public class AncientClass : DamageClass
========
public class ForbiderDamage : DamageClass
>>>>>>>> Stashed changes:Content/Systems/DamageClasses/ForbiderDamage.cs
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
<<<<<<<< Updated upstream:Content/Systems/DamageClasses/AncientClass.cs
		player.GetCritChance<AncientClass>() = -4f;
		player.GetArmorPenetration<AncientClass>() = 1f;
========
		player.GetCritChance<ForbiderDamage>() = -4f;
		player.GetArmorPenetration<ForbiderDamage>() = 1f;
>>>>>>>> Stashed changes:Content/Systems/DamageClasses/ForbiderDamage.cs
	}
}
	