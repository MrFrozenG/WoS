using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Core.ModUtils.ShopConditions
{
    public class PlayerHasItemCondition : Conditions
    {
        public static readonly Condition HasStynger =
             new Condition("Mods.WoS.General.Shops.HasStynger",
                 () => Main.LocalPlayer.HasItem(ItemID.Stynger));
        
        public static readonly Condition HasJackLauncher =
            new Condition("Mods.WoS.General.Shops.HasJackOLanternLauncher",
                () => Main.LocalPlayer.HasItem(ItemID.JackOLanternLauncher));

        public static readonly Condition HasCandyCornRifle =
            new Condition("Mods.WoS.General.Shops.HasCandyCornRifle",
                () => Main.LocalPlayer.HasItem(ItemID.CandyCornRifle));

        public static readonly Condition HasStakeLauncher =
            new Condition("Mods.WoS.General.Shops.HasStakeLauncher",
                () => Main.LocalPlayer.HasItem(ItemID.StakeLauncher));

        public static readonly Condition HasNailWeapon =
            new Condition("Mods.WoS.General.Shops.HasNailWeapon",
                () => PlayerHasAnyNailWeapon(Main.LocalPlayer));

        private static bool PlayerHasAnyNailWeapon(Player player)
        {
            // Проверяем все слоты: инвентарь, armor, miscEquips, банк — опционально
            for (int i = 0; i < player.inventory.Length; i++)
            {
                Item item = player.inventory[i];

                if (item != null && !item.IsAir)
                {
                    // Если оружие использует Nails как боеприпасы
                    if (item.useAmmo == AmmoID.NailFriendly)
                        return true;
                }
            }

            return false;
        }
    }
}
