using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Enums;
using WoS.Content.Items.Placeable.Banners;

namespace WoS.Content.Tiles.Banners
{
    public class BaseBannerTile : ModTile
    {
        public readonly static (string item, string npc)[] BannersIndex = new (string, string)[]
        {
            (nameof(BigMimicBloomingBanner), "BigMimicBlooming"),
        };
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom | AnchorType.Platform, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 118;
            TileObjectData.addTile(Type);

            DustType = -1;
            TileID.Sets.DisableSmartCursor[Type] = true;

            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(13, 88, 130), name);
        }
        public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
        {
            if (OnPlatform(i, j)) // Offset banners on non-sloped platforms
                offsetY -= 8;
        }

        private static bool OnPlatform(int i, int j)
        {
            int offY = Main.tile[i, j].TileFrameY / 18;
            bool isOnPlatform = TileID.Sets.Platforms[Main.tile[i, j - offY - 1].TileType];
            bool isPlatformHammered = Main.tile[i, j - offY - 1].Slope != SlopeType.Solid;
            return isOnPlatform && !isPlatformHammered;
        }

        public override void NearbyEffects(int i, int j, bool closer)
        {
            int style = Main.tile[i, j].TileFrameX / 18;
            var npc = BannersIndex[style].npc;

            Main.SceneMetrics.NPCBannerBuff[Mod.Find<ModNPC>(npc).Type] = true;
            Main.SceneMetrics.hasBanner = true;
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
                spriteEffects = SpriteEffects.FlipHorizontally;
        }
    }
}
