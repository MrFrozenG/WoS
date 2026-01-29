using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace WoS.Content.Tiles.CraftPlaces
{
    public class Celestial_Altar : ModTile
    {
        private Asset<Texture2D> glowTexture;
        private const int FrameHeight = 56;
        private const int TotalFrames = 5;
        public override void SetStaticDefaults()
        {
            //Main.tileSolidTop[Type] = true; //Больше не актуально, но может измениться
            Main.tileNoAttach[Type] = true;
            Main.tileFrameImportant[Type] = true;
            TileID.Sets.DisableSmartCursor[Type] = true; 
            TileID.Sets.IgnoredByNpcStepUp[Type] = true;
            Main.tileLighted[Type] = true;

            AdjTiles = [
                TileID.MythrilAnvil,
                TileID.Anvils,
                TileID.DemonAltar,
                TileID.CrystalBall,
                TileID.WorkBenches
                ];
            DustType = DustID.GoldCoin;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3);
            TileObjectData.newTile.StyleHorizontal = true;
            TileObjectData.newTile.CoordinateHeights = [16, 16, 18];
            TileObjectData.addTile(Type);

            glowTexture = ModContent.Request<Texture2D>(Texture + "_Glow");

            AnimationFrameHeight = FrameHeight; // важнейшая строка

            AddMapEntry(new Color(200, 200, 200), Language.GetText("Mods.WoS.Items.CelestialAltar.DisplayName")); 
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            r = 1f;
            g = 0.8f;
            b = 0.3f;
        }
        public override void NumDust(int x, int y, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++;

            // скорость анимации — увеличь до 20–30 чтобы замедлить
            if (frameCounter > 10)
            {
                frameCounter = 0;
                frame++;

                if (frame >= TotalFrames)
                    frame = 0;
            }
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];

            Vector2 off = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);

            // Определяем высоту кусочка (нижний сегмент — 18px)
            int localY = tile.TileFrameY % FrameHeight;
            int height = (localY >= 32) ? 18 : 16;

            // Текущий кадр
            int frameYOffset = Main.tileFrame[Type] * FrameHeight;

            // Прямоугольник исходной текстуры
            Rectangle frameRect = new Rectangle(
                tile.TileFrameX,
                tile.TileFrameY + frameYOffset,
                16,
                height
            );

            Vector2 drawPos = new Vector2(
                i * 16 - (int)Main.screenPosition.X,
                j * 16 - (int)Main.screenPosition.Y
            ) + off;

            // 1) Основная текстура
            spriteBatch.Draw(
                TextureAssets.Tile[Type].Value,
                drawPos,
                frameRect,
                Lighting.GetColor(i, j)
            );

            // 2) Glowmask
            spriteBatch.Draw(
                glowTexture.Value,
                drawPos,
                frameRect,
                Color.White
            );

            return false; // отменяем ванильный draw
        }
    }
}
