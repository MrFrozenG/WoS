using Terraria;
using WoS.Content.Core.Interfaces;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Prefixes;

namespace WoS.Content.Core.ModUtils
{
    public static class SupportPointExtensions
    {
        /*public static int GetSupportPoints(this Item item, Player player)
        {
            if (item?.ModItem is not ISupportWeapon supportWeapon)
                return 0;

            var modPlayer = player.GetModPlayer<MainPlayer>();
            return modPlayer.GetModifiedSupportPoints(
                supportWeapon.BaseSupportPoints
            );
        }*/
        public static int GetSupportPoints(this Item item, Player player)
        {
            if (item?.ModItem is not ISupportWeapon supportWeapon)
                return 0;

            var modPlayer = player.GetModPlayer<MainPlayer>();
            int basePoints = supportWeapon.BaseSupportPoints;

            // Добавляем бонус от игрока
            int modifiedPoints = modPlayer.GetModifiedSupportPoints(basePoints);

            // Добавляем бонус от префикса
            var prefixGlobal = item.GetGlobalItem<SupportPrefixGlobal>();
            modifiedPoints += prefixGlobal?.supportPointsBonus ?? 0;

            return modifiedPoints;
        }
    }
}
