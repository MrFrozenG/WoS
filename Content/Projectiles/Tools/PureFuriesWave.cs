using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;

namespace WoS.Content.Projectiles.Tools
{
    public class PureFuriesWave : ModProjectile
    {
        private const float RadiusTiles = 120f; // дальность очистки
        private const int WaveDuration = 180;   // продолжительность волны (3 сек)

        public override void SetDefaults()
        {
            Projectile.width = 2;
            Projectile.height = 2;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;

            Projectile.timeLeft = WaveDuration; // волна начинается сразу

            Projectile.alpha = 255;     // невидимый снаряд
        }

        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            Projectile.Center = owner.Center;

            // ------ 1) АКТИВАЦИЯ ОДИН РАЗ ------
            if (Projectile.localAI[0] == 0f)
            {
                Projectile.localAI[0] = 1f;

                // очищаем блоки один раз, чтобы не лагало
                CleanCircle(owner.Center, RadiusTiles);

                //if (Main.netMode != NetmodeID.Server)
                //{
                //    Filters.Scene.Activate(, Projectile.Center)
                 //       .GetShader()
                //        .UseColor(3, 5, 15)           // rippleCount, rippleSize, rippleSpeed
                //        .UseTargetPosition(Projectile.Center);
                //}
            }

            // ------ 2) ПРОГРЕСС ВОЛНЫ ------
           /* if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
            {
                float progress = (WaveDuration - Projectile.timeLeft) / 60f;
                float opacity = 1f - (progress / 3f);

                var shader = Filters.Scene.Activate("Shockwave", Projectile.Center).GetShader();

                shader.UseColor(3, 5, 15);

                shader.UseTargetPosition(Projectile.Center / new Vector2(Main.screenWidth, Main.screenHeight));
            }*/
        }

        /*public override void Kill(int timeLeft)
        {
            if (Main.netMode != NetmodeID.Server && Filters.Scene["Shockwave"].IsActive())
                Filters.Scene["Shockwave"].Deactivate();
        }*/

        // ----------------------------------------------------
        //          ОЧИСТКА БИОМА – РАЗ В МОМЕНТ АКТИВАЦИИ
        // ----------------------------------------------------
        private void CleanCircle(Vector2 center, float radius)
        {
            int minX = (int)(center.X / 16f - radius);
            int maxX = (int)(center.X / 16f + radius);
            int minY = (int)(center.Y / 16f - radius);
            int maxY = (int)(center.Y / 16f + radius);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (!WorldGen.InWorld(x, y, 1))
                        continue;

                    float dx = x - center.X / 16f;
                    float dy = y - center.Y / 16f;

                    if (dx * dx + dy * dy <= radius * radius)
                        PurifyTile(x, y);
                }
            }
        }

        private void PurifyTile(int x, int y)
        {
            Tile tile = Framing.GetTileSafely(x, y);
            Tile wall = Framing.GetTileSafely(x, y);

            if (tile.TileType == TileID.Ebonstone || tile.TileType == TileID.Crimstone || tile.TileType == TileID.Pearlstone)
                tile.TileType = TileID.Stone;

            if (tile.TileType == TileID.CorruptGrass || tile.TileType == TileID.CrimsonGrass || tile.TileType == TileID.HallowedGrass)
                tile.TileType = TileID.Grass;

            if (tile.TileType == TileID.Ebonsand || tile.TileType == TileID.Crimsand || tile.TileType == TileID.Pearlsand)
                tile.TileType = TileID.Sand;

            if (tile.TileType == TileID.CorruptSandstone || tile.TileType == TileID.CrimsonSandstone || tile.TileType == TileID.HallowSandstone)
                tile.TileType = TileID.Sandstone;

            if (tile.TileType == TileID.CorruptHardenedSand || tile.TileType == TileID.CrimsonHardenedSand || tile.TileType == TileID.HallowHardenedSand)
                tile.TileType = TileID.HardenedSand;

            if (tile.TileType == TileID.CorruptIce || tile.TileType == TileID.FleshIce || tile.TileType == TileID.HallowedIce)
                tile.TileType = TileID.IceBlock;
            if (tile.TileType == TileID.CorruptJungleGrass || tile.TileType == TileID.CrimsonJungleGrass)
                tile.TileType = TileID.JungleGrass;

            if (wall.WallType == WallID.EbonstoneUnsafe || wall.WallType == WallID.CrimstoneUnsafe || wall.WallType == WallID.PearlstoneBrickUnsafe) 
                wall.WallType = WallID.Stone;
            if (wall.WallType == WallID.CorruptGrassUnsafe || wall.WallType == WallID.CrimsonGrassUnsafe || wall.WallType == WallID.HallowedGrassUnsafe) 
                wall.WallType = WallID.GrassUnsafe;
            if (wall.WallType == WallID.CorruptSandstone || wall.WallType == WallID.CrimsonSandstone || wall.WallType == WallID.HallowSandstone) 
                wall.WallType = WallID.Sandstone;
            if (wall.WallType == WallID.CorruptHardenedSand || wall.WallType == WallID.CrimsonHardenedSand || wall.WallType == WallID.HallowHardenedSand) 
                wall.WallType = WallID.HardenedSand;
            if (wall.WallType == WallID.Jungle || wall.WallType == WallID.CrimsonHardenedSand || wall.WallType == WallID.HallowHardenedSand) 
                wall.WallType = WallID.HardenedSand;
            

            WorldGen.SquareTileFrame(x, y, true);
        }
    }
}
