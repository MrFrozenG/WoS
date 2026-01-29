using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace WoS.Content.Core.ModWorld
{
    public class WorldFlags : ModSystem
    {
        public static bool downedBloodMoon = false;
        public static bool downedEclipse = false;
        public static bool downedFrostMoon = false;
        public static bool downedPumpkinMoon = false;
        public override void ClearWorld()
        {
            downedBloodMoon = false;
            downedEclipse = false;
            downedFrostMoon = false;
            downedPumpkinMoon = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (downedBloodMoon)
            {
                tag["downedBloodMoon"] = true;
            }
            if (downedEclipse)
            {
                tag["downedEclipse"] = true;
            }
            if (downedFrostMoon)
            {
                tag["downedFrostMoon"] = true;
            }
            if (downedPumpkinMoon)
            {
                tag["downedPumpkinMoon"] = true;
            }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            downedBloodMoon = tag.ContainsKey("downedBloodMoon");
            downedEclipse = tag.ContainsKey("downedEclipse");
            downedFrostMoon = tag.ContainsKey("downedFrostMoon");
            downedPumpkinMoon = tag.ContainsKey("downedPumpkinMoon");
        }
        public override void NetSend(BinaryWriter writer)
        {
            // Order of parameters is important and has to match that of NetReceive
            writer.WriteFlags(downedBloodMoon, downedEclipse, downedFrostMoon, downedPumpkinMoon);
        }
        public override void NetReceive(BinaryReader reader)
        {
            reader.ReadFlags(out downedBloodMoon, out downedEclipse, out downedFrostMoon, out downedPumpkinMoon);
        }
        public override void PostUpdateWorld()
        {
            if (Main.bloodMoon && !downedBloodMoon)
            {
                downedBloodMoon = true;
            }
            if (Main.eclipse && !downedEclipse)
            {
                downedEclipse = true;
            }
            if (Main.snowMoon && Main.invasionProgressWave >= 15 && !downedFrostMoon)
            {
                downedFrostMoon = true;
            }
            if (Main.pumpkinMoon && Main.invasionProgressWave >= 15 && !downedPumpkinMoon)
            {
                downedPumpkinMoon = true;
            }
        }
    }
}
