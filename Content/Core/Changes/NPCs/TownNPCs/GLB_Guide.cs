using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace WoS.Content.Core.ModNPCs.TownNPCs
{
    public class GLB_Guide : GlobalNPC
    {
        public override void GetChat(NPC npc, ref string chat)
        {
            string WoSPath = "Mods.WoS.NPCs.Chat.";
            int Hunter = 0; //TO DO

            WeightedRandom<string> chatNew = new WeightedRandom<string>();

            if (npc.type == NPCID.Guide)
            {
                //                if (Main.rand.NextBool(10) && Main.dayTime)
                //                    chat = Language.GetTextValue("Mods.WoS.NPCs.Chat.Guide.DayTime"); //Main.rand.Next(1, 2 + 1));
                //                if (Main.rand.NextBool(10) && !Main.dayTime)
                //                    chat = Language.GetTextValue("Mods.WoS.NPCs.Chat.Guide.NightTime");
//                chat = Language.GetTextValue(WoSPath + "Guide.AEDWeapons");
                chatNew.Add(Language.GetTextValue(WoSPath + "Guide.AEDWeapons"), 7);


                if (Main.rand.NextBool(15))
                {
                    chat = chatNew;
                }
            }
        }
    }
}
