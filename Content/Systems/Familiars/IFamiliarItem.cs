using Terraria;


namespace WoS.Content.Systems.Familiars
{
    public interface IFamiliarItem
    {
        int BaseDamage { get; } // Базовый урон фамильяра
        float SoulPower { get; } // Сила фамильяра
        float SoulCost { get; } // Энергия для особого навыка
        int SoulRegen { get; } // Скорость восстановления энергии
        void UseSpecialAbility(Player player); // Метод для особого свойства фамильяра

        int FamiliarA { get; }
        int FamiliarB { get; }
    }
}
