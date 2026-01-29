using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Accessories.Support;
using WoS.Content.Items.Ammo.Bullets;

namespace WoS.Content.Core.ModWorld.Generation
{
    public class JungleChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            Dictionary<int, (int min, int max, float chance)> itemQuantityRanges = new()
            {
                [ModContent.ItemType<HiveBullet>()] = (23, 66, 0.8f),
                //                [ModContent.ItemType<ExampleLightPetItem>()] = (1, 50),   
                [ItemID.Stinger] = (1, 5, 0.5f)
            };

            int itemsPlaced = 0;

            // Перебираем все сундуки
            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Tile chestTile = Main.tile[chest.x, chest.y];

                // Проверяем, что это сундук из джунглей (Mahogany или Ivy или Temple)
                if (chestTile.TileType == TileID.Containers && (chestTile.TileFrameX == 8 * 36 || chestTile.TileFrameX == 10 * 36 || chestTile.TileFrameX == 16 * 36))
                {
                    // С вероятностью 1/3 пропускаем сундук
                    //                    if (WorldGen.genRand.NextBool(3))
                    //                        continue;

                    // Перебираем все предметы, которые нужно добавить в сундук
                    foreach (var itemEntry in itemQuantityRanges)
                    {
                        int itemID = itemEntry.Key;
                        (int min, int max, float chance) = itemEntry.Value;

                        // Проверяем, с каким шансом добавлять этот предмет
                        if (WorldGen.genRand.NextFloat() <= chance)
                        {
                            // Генерируем случайное количество для этого предмета
                            int randomQuantity = WorldGen.genRand.Next(min, max + 1);

                            // Ищем пустой слот в сундуке и добавляем предмет с количеством
                            for (int i = 0; i < Chest.maxItems; i++)
                            {
                                if (chest.item[i].type == ItemID.None)
                                {
                                    chest.item[i].SetDefaults(itemID);
                                    chest.item[i].stack = randomQuantity;  // Устанавливаем количество предмета
                                    itemsPlaced++;

                                    break; // Переходим к следующему предмету
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}