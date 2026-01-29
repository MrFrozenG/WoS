using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;
using WoS.Content.NPCs.Town;

namespace WoS
{
    partial class WoS
    {
        public enum MessageType : byte
        {
            NPCTeleportToStatue,
            SupportEffect,
            SupportEffectRequest // <- новый тип
        }
        public enum SupportEffectType : byte
        {
            RestoreMana,
            GiveBuff,
            Heal,
            Custom
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            MessageType msgType = (MessageType)reader.ReadByte();

            switch (msgType)
            {
                case MessageType.NPCTeleportToStatue:
                    {
                        byte npcID = reader.ReadByte();
                        if (Main.npc[npcID].ModNPC is HunterTrader npct && npct.NPC.active)
                        {
                            npct.StatueTeleport();
                        }
                        break;
                    }

                case MessageType.SupportEffect:
                    {
                        SupportEffectType effectType = (SupportEffectType)reader.ReadByte();
                        byte playerID = reader.ReadByte();
                        int value = reader.ReadInt32();

                        Player target = Main.player[playerID];
                        if (target.active)
                        {
                            ApplySupportEffect(effectType, target, value, whoAmI);
                        }
                        break;
                    }
                case MessageType.SupportEffectRequest:
                    {
                        // Этот пакет приходит от клиента на сервер с запросом "восстановить ману"
                        // Порядок чтения должен точно соответствовать порядку записи в клиенте.
                        WoS.SupportEffectType effectType = (WoS.SupportEffectType)reader.ReadByte();
                        byte ownerPlayerID = reader.ReadByte();
                        int manaAmount = reader.ReadInt32();
                        float radius = reader.ReadSingle();

                        // Проверяем, что этот код выполняется на сервере
                        if (Main.netMode == NetmodeID.Server)
                        {
                            Player owner = Main.player[ownerPlayerID];
                            // Защита: убедимся, что инициатор whoAmI соответствует ownerPlayerID (безопасность)
                            // whoAmI — id клиента, который послал пакет.
                            if (whoAmI == ownerPlayerID)
                            {
                                // Вызываем серверный метод — он обновит statMana и разошлёт всё
                                SupportPlayerUtils.RestoreManaToTeam(owner, manaAmount, radius);
                            }
                        }
                        break;
                    }
            }
        }

        // Применяем эффект на игрока
        public void ApplySupportEffect(SupportEffectType type, Player target, int value, int fromWho)
        {
            switch (type)
            {
                case SupportEffectType.RestoreMana:
                    target.statMana += value;
                    target.ManaEffect(value);
                    break;

                case SupportEffectType.Heal:
                    target.statLife += value;
                    target.HealEffect(value);
                    break;

                case SupportEffectType.GiveBuff:
                    // value = ID баффа, пример на 60 тиков
                    target.AddBuff(value, 60);
                    break;

                case SupportEffectType.Custom:
                    target.GetModPlayer<PlayerBuff>().TwilightPhantomSigilSP += value;
                    break;
            }
        }
    }
    internal static class NetworkUtils
    {
        // Отправка эффекта поддержки
        public static void SendSupportEffect(int toWho, int fromWho, WoS.SupportEffectType type, int playerID, int value = 0)
        {
            ModPacket packet = ModContent.GetInstance<WoS>().GetPacket();
            packet.Write((byte)WoS.MessageType.SupportEffect); // общий тип пакета
            packet.Write((byte)type);                      // какой эффект
            packet.Write((byte)playerID);                  // игрок, на которого эффект
            packet.Write(value);                           // значение (например количество хп/маны)
            packet.Send(toWho, fromWho);
        }

        // Отправка телепортации NPC
        public static void SendNPCTeleport(int toWho, int fromWho, int npcID)
        {
            ModPacket packet = ModContent.GetInstance<WoS>().GetPacket();
            packet.Write((byte)WoS.MessageType.NPCTeleportToStatue); // общий тип пакета
            packet.Write((byte)npcID);                            // какой NPC телепортировать
            packet.Send(toWho, fromWho);
        }
    }
}
