using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using WoS.Content.NPCs.Town;

namespace WoS.Content.Core.ModWorld
{
    public class World_NPCs : ModSystem
    {
        public static bool unlockedHunter = false;
        public override void ClearWorld()
        {
            unlockedHunter = false;
        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag[nameof(unlockedHunter)] = unlockedHunter;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            unlockedHunter = tag.GetBool(nameof(unlockedHunter));
            unlockedHunter |= NPC.AnyNPCs(ModContent.NPCType<HunterTrader>());
        }
        public override void NetSend(BinaryWriter writer)
        {
            writer.WriteFlags(unlockedHunter);
        }

        public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out unlockedHunter);
        }
    }
}
