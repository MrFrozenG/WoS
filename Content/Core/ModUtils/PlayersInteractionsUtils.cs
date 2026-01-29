using Microsoft.Xna.Framework;
using Terraria;

namespace WoS.Content.Core.ModUtils
{
    public class PlayersInteractionsUtils
    {
        /// <summary>
        /// Проверяет, можно ли игроку owner влиять на игрока other как на «свою команду».
        /// Включает поддержку для игроков без команды, если рядом есть другие игроки без команды.
        /// </summary>
        public static bool IsSameTeamOrFreeTeam(Player owner, Player other)
        {
            if (owner == null || other == null) return false;
            if (!owner.active || !other.active) return false;
            if (owner.whoAmI == other.whoAmI) return false;

            // Оба в команде и команды совпадают
            if (owner.team != 0 && owner.team == other.team)
                return true;

            // Оба без команды
            if (owner.team == 0 && other.team == 0)
            {
                // Проверяем, есть ли рядом другие игроки без команды
                for (int i = 0; i < Main.maxPlayers; i++)
                {
                    Player p = Main.player[i];
                    if (!p.active || p.whoAmI == owner.whoAmI) continue;
                    if (p.team == 0 && Vector2.Distance(owner.Center, p.Center) <= 128f * 16f)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка, находится ли игрок в радиусе другого игрока
        /// </summary>
        public static bool IsWithinRadius(Player from, Player target, float radiusInBlocks)
        {
            return Vector2.Distance(from.Center, target.Center) <= radiusInBlocks * 16f;
        }
    }
}
