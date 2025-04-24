using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.NPCs.Jungle;
using Microsoft.Xna.Framework;
using WoS.Content.Config;

namespace WoS.Content.Items.Consumables.Keys;
public class GrowthKey : ModItem
{
    public override void SetStaticDefaults()
    {
        ItemID.Sets.SortingPriorityBossSpawns[Type] = 3;
    }
    public override void SetDefaults()
    {
        Item.width = 14;
        Item.height = 22;
        Item.maxStack = 20;
        Item.rare = ItemRarityID.White;
        Item.consumable = false;
        Item.useTime = 20;
        Item.useAnimation = 20;
        Item.useStyle = ItemUseStyleID.HoldUp;
    }
/*    public override bool? UseItem(Player player)
    {
        if (ModContent.GetInstance<VanilaConfig>().ReworkSummonMimics)
        {
            int searchRadius = 10;
            bool chestFound = false;
            Point playerPosition = player.Center.ToTileCoordinates();

            for (int x = playerPosition.X - searchRadius; x <= playerPosition.X + searchRadius; x++)
            {
                for (int y = playerPosition.Y - searchRadius; y <= playerPosition.Y + searchRadius; y++)
                {
                    int chestIndex = Chest.FindChest(x, y);

                    // Проверка: сундук найден и пуст
                    if (chestIndex >= 0 && IsChestEmpty(Main.chest[chestIndex]))
                    {
                        // Удаление сундука и очищение тайлов
                        Chest.DestroyChest(x, y);
                        for (int i = x; i <= x + 1; i++)
                        {
                            for (int j = y; j <= y + 1; j++)
                            {
                                if (TileID.Sets.BasicChest[(int)Main.tile[i, j].TileType])
                                    Main.tile[i, j].ClearTile();
                            }
                        }

                        // Призыв кастомного мимика
                        int npcType = ModContent.NPCType<BigMimicBlooming>();
                        int npcIndex = NPC.NewNPC(player.GetSource_ItemUse(Item), x * 16 + 16, y * 16 + 32, npcType);
                        Main.npc[npcIndex].whoAmI = npcIndex;
                        NetMessage.SendData(MessageID.SyncNPC, number: npcIndex);

                        chestFound = true;
                        break; // Прекращаем цикл, как только нашли сундук и призвали мимика
                    }
                }
                if (chestFound) break; // Прерываем внешний цикл, если сундук найден
            }

            // Если пустого сундука не найдено
            if (!chestFound)
            {
                Main.NewText("No empty chests nearby!", 255, 50, 50);
            }
            return chestFound; // Возвращаем true, если был найден пустой сундук и призван мимик
        }
        return false;
    }
        
    bool IsChestEmpty(Chest chest)
    {
        foreach (Item item in chest.item)
        {
            if (item != null && item.type > ItemID.None)
                return false; // Если сундук не пустой
        }
        return true; // Если сундук пустой
    }
*/
}