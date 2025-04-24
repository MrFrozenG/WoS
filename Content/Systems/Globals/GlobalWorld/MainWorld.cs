using System.IO;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace WoS.Content.Systems.Globals.GlobalWorld
{
    public class MainWorld : ModSystem
    {
        public static bool CircusEventClear = false; //Is Event (Circus of Madness) cleared
        public static bool CircusEventStatus = false; //Status of Event, if True - In world Circus of Madness event is active.
        public static int CircusEventKills = 0; //Counter for Event (Circus of Madness)
        public static float CircusEventKillsTotal = 250; //Base value for counter of event (Circus of Madness)

//OLD       public static bool DownedArcticus = false;
        public override void OnWorldLoad()
        {
            CircusEventStatus = false;
            CircusEventKills = 0;
        }
        public override void OnWorldUnload()
        {
        }
        public override void ClearWorld()
        {
            CircusEventClear = false;
            //OLD             DownedArcticus = false;
        }
        public override void SaveWorldData(TagCompound tag)
        {
            if (CircusEventClear)
            {
                tag["downedCircusEvent"] = true;
            }
            //OLD    if (DownedArcticus)
            //OLD   {
            //OLD      tag["downedArcticus"] = true;
            //OLD   }
        }
        public override void LoadWorldData(TagCompound tag)
        {
            //OLD             DownedArcticus = tag.ContainsKey("downedArcticus");
            CircusEventClear = tag.ContainsKey("downedCircusEvent");
        }
        public override void NetSend(BinaryWriter writer)
        {
            var flagsE = new BitsByte();
            flagsE[0] = CircusEventClear;
            writer.Write(flagsE);

            //OLD        var flagsB = new BitsByte();
            //OLD          flagsB[0] = DownedArcticus;
            //OLD        writer.Write(flagsB);

        }
        public override void NetReceive(BinaryReader reader)
        {

            BitsByte flagsE = reader.ReadByte();
            CircusEventClear = flagsE[0];
            CircusEventKills = reader.ReadInt32();
            //OLD             BitsByte flagsB = reader.ReadByte();
            //OLD             DownedArcticus = flagsB[0];
        }
    }
}
