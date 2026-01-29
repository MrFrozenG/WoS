using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Resources;

namespace WoS.Content.Core.ModWorld.Generation
{
    public class SkyChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Tile chestTile = Main.tile[chest.x, chest.y];

                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 13 * 36)
                {
                    for (int i = 0; i < Chest.maxItems; i++)
                    {
                        if (chest.item[i] != null && chest.item[i].type == ItemID.Starfury)
                        {
                            chest.item[i].SetDefaults(ModContent.ItemType<FragmentofFallenStar>());

                            // устанавливаем количество (1–3 или 2–4)
                            chest.item[i].stack = Main.expertMode
                                ? Main.rand.Next(2, 5)
                                : Main.rand.Next(1, 4);
                        }
                    }
                }
            }
        }
    }
}
