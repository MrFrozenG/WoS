using Terraria.ID;
using Terraria;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace WoS.Content.Core.ModUtils
{
    public class SupportPlayerUtils
    {
        public static void RestoreManaToTeam(Player owner, int manaAmount, float radius)
        {
            if (!owner.active) return;

            // Если мы не сервер — изменяем только локально для владельца (SP or client local view)
            if (Main.netMode != NetmodeID.Server)
            {
                owner.statMana += manaAmount;
                owner.ManaEffect(manaAmount);
                return;
            }

            // --- Здесь мы на сервере: изменяем statMana для владельца и союзников и синхронизируем клиентов ---
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player p = Main.player[i];
                if (!p.active) continue;

                bool isOwner = p.whoAmI == owner.whoAmI;
                bool isAlly = (owner.team != 0 && owner.team == p.team) && Vector2.Distance(owner.Center, p.Center) <= radius * 16f;

                if (isOwner || isAlly)
                {
                    p.statMana += manaAmount;
                    NetMessage.SendData(MessageID.PlayerMana, number: p.whoAmI); // обновляем UI на клиенте p

                    // Рассылаем кастомный пакет визуала всем клиентам (чтобы они увидели числа над p)
                    NetworkUtils.SendSupportEffect(-1, -1, WoS.SupportEffectType.RestoreMana, p.whoAmI, manaAmount);
                }
            }
        }
        public static void RestoreManaToTeamUniversal(Player owner, int manaAmount, float radius)
        {
            if (!owner.active) return;

            if (Main.netMode == NetmodeID.SinglePlayer || Main.netMode == NetmodeID.Server)
            {
                // SP или сервер выполняют напрямую
                RestoreManaToTeam(owner, manaAmount, radius);
            }
            else if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                // Клиент отправляет серверу запрос
                ModPacket packet = ModContent.GetInstance<WoS>().GetPacket();
                packet.Write((byte)WoS.MessageType.SupportEffectRequest);
                packet.Write((byte)WoS.SupportEffectType.RestoreMana);
                packet.Write((byte)owner.whoAmI);
                packet.Write((int)manaAmount);
                packet.Write((float)radius);
                packet.Send();
            }
        }
    }
}
