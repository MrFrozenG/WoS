using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace WoS.Content.Systems.Globals.GlobalWorld
{
    public class PostHardmode : ModSystem
    {
        public static bool IsPostHardmode { get; private set; } = false;
        public override void OnWorldLoad()
        {

        }

        public override void SaveWorldData(TagCompound tag)
        {
            tag["IsPostHardmode"] = IsPostHardmode;
        }

        public override void LoadWorldData(TagCompound tag)
        {
            if (tag.ContainsKey("IsPostHardmode"))
            {
                IsPostHardmode = tag.GetBool("IsPostHardmode");
            }
        }

        public override void OnWorldUnload()
        {
            IsPostHardmode = false;
        }
        public override void PostUpdateWorld()
        {
            if (!IsPostHardmode && NPC.downedMoonlord)
            {
                ActivatePostHardmode();
            }
        }

        private void ActivatePostHardmode()
        {
            IsPostHardmode = true;
            if (Main.netMode == NetmodeID.SinglePlayer)
            {
                Main.NewText(Language.GetTextValue("Mods.WoS.General.Gamemode.PostHardmode.UnleashedChaos"), 255, 200, 0);
                Main.NewText(Language.GetTextValue("Mods.WoS.General.Gamemode.PostHardmode.UnleashedLight"), 255, 200, 0);
            }
            else if (Main.netMode == NetmodeID.Server)
            {
                Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.WoS.General.Gamemode.PostHardmode.UnleashedChaos"), new Microsoft.Xna.Framework.Color(255, 200, 0));
                Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Mods.WoS.General.Gamemode.PostHardmode.UnleashedLight"), new Microsoft.Xna.Framework.Color(255, 200, 0));
            }
        }
    }
}
