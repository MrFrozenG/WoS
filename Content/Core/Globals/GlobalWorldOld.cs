using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using Terraria.UI;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;

namespace WoS.Content.Core.Globals
{
    public class GlobalWorldOld : ModSystem
    {
        /*
        public override void PreUpdateWorld()
        {
            if (CircusEventKills > CircusEventKillsTotal)
            {
                CircusEventStatus = false;
                if (!CircusEventClear) CircusEventClear = true;
                if (Main.netMode == NetmodeID.Server)
                    NetMessage.SendData(MessageID.WorldData);
                string key = Language.GetText("Mods.WoS.CircusEvent.ClearMessage").Value;
                Color messageColor = Color.Orange;
                if (Main.netMode == NetmodeID.Server) // Server
                {
                    Terraria.Chat.ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), messageColor);
                }
                else if (Main.netMode == NetmodeID.SinglePlayer) // Singleplayer
                {
                    Main.NewText(Language.GetTextValue(key), messageColor);
                }
                CircusEventKills = 0;
            }
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            if (CircusEventStatus)
            {
                int index = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
                LegacyGameInterfaceLayer orionProgress = new LegacyGameInterfaceLayer(Language.GetText("Mods.WoS.ClownEvent.Title").Value,
                    delegate
                    {
                        DrawCircusEvent(Main.spriteBatch);
                        return true;
                    },
                    InterfaceScaleType.UI);
                layers.Insert(index, orionProgress);
            }
        }
        public void DrawCircusEvent(SpriteBatch spriteBatch)
        {
            if (CircusEventStatus && !Main.gameMenu)
            {
                float scaleMultiplier = 0.5f + 1 * 0.5f;
                float alpha = 0.5f;
                Texture2D progressBg = TextureAssets.ColorBar.Value;
                Texture2D progressColor = TextureAssets.ColorBar.Value;
                Texture2D orionIcon = ModContent.Request<Texture2D>("WoS/Content/Items/Currencies/ClownCoin").Value;
                Color descColor = new Color(39, 86, 134);

                Color waveColor = new Color(255, 241, 51);
                Color barrierColor = new Color(255, 241, 51);

                try
                {
                    //draw the background for the waves counter
                    const int offsetX = 20;
                    const int offsetY = 20;
                    int width = (int)(200f * scaleMultiplier);
                    int height = (int)(46f * scaleMultiplier);
                    Rectangle waveBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth - offsetX - 100f, Main.screenHeight - offsetY - 23f), new Vector2(width, height));
                    Utils.DrawInvBG(spriteBatch, waveBackground, new Color(63, 65, 151, 255) * 0.785f);

                    //draw wave text

                    string waveText = Language.GetText("Mods.WoS.ProgressBar").Value + (int)(((float)CircusEventKills / CircusEventKillsTotal) * 100) + "%";
                    Utils.DrawBorderString(spriteBatch, waveText, new Vector2(waveBackground.X + waveBackground.Width / 2, waveBackground.Y), Color.White, scaleMultiplier, 0.5f, -0.1f);

                    // Main.NewText(MathHelper.Clamp((modWorld.DinoKillCount/modWorld.MaxDinoKillCount), 0f, 1f));
                    Rectangle waveProgressBar = Utils.CenteredRectangle(new Vector2(waveBackground.X + waveBackground.Width * 0.5f, waveBackground.Y + waveBackground.Height * 0.75f), new Vector2(progressColor.Width, progressColor.Height));
                    Rectangle waveProgressAmount = new Rectangle(0, 0, (int)(progressColor.Width * MathHelper.Clamp(((float)CircusEventKills / CircusEventKillsTotal), 0f, 1f)), progressColor.Height);
                    Vector2 offset = new Vector2((waveProgressBar.Width - (int)(waveProgressBar.Width * scaleMultiplier)) * 0.5f, (waveProgressBar.Height - (int)(waveProgressBar.Height * scaleMultiplier)) * 0.5f);

                    spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, null, Color.White * alpha, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);
                    spriteBatch.Draw(progressBg, waveProgressBar.Location.ToVector2() + offset, waveProgressAmount, waveColor, 0f, new Vector2(0f), scaleMultiplier, SpriteEffects.None, 0f);

                    //draw the icon with the event description

                    //draw the background
                    const int internalOffset = 6;
                    Vector2 descSize = new Vector2(154, 40) * scaleMultiplier;
                    Rectangle barrierBackground = Utils.CenteredRectangle(new Vector2(Main.screenWidth - offsetX - 100f, Main.screenHeight - offsetY - 19f), new Vector2(width, height));
                    Rectangle descBackground = Utils.CenteredRectangle(new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), descSize);
                    Utils.DrawInvBG(spriteBatch, descBackground, descColor * alpha);

                    //draw the icon
                    int descOffset = (descBackground.Height - (int)(32f * scaleMultiplier)) / 2;
                    Rectangle icon = new Rectangle(descBackground.X + descOffset, descBackground.Y + descOffset, (int)(32 * scaleMultiplier), (int)(32 * scaleMultiplier));
                    spriteBatch.Draw(orionIcon, icon, Color.White);

                    //draw text

                    Utils.DrawBorderString(spriteBatch, Language.GetText("Mods.WoS.CircusEvent.Title").Value, new Vector2(barrierBackground.X + barrierBackground.Width * 0.5f, barrierBackground.Y - internalOffset - descSize.Y * 0.5f), Color.White, 0.80f, 0.3f, 0.4f);
                }
                catch
                {
                }
            }
        }*/
    }
}