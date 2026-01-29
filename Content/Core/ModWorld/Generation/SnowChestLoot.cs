
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Items.Ammo.Bullets;
using WoS.Content.Items.Weapons.Magic.Grimoires;

namespace WoS.Content.Core.ModWorld.Generation
{
    public class SnowChestLoot : ModSystem
    {
        public override void PostWorldGen()
        {
            HashSet<int> existingIceItems = new()
            {
                ItemID.SnowballCannon,
                ItemID.IceBlade,
                ItemID.IceBoomerang,
                ItemID.IceBow
            };

            List<int> guaranteedLoot = new()
            {
                ModContent.ItemType<FrostbrandGrimoire>()
            };

            for (int chestIndex = 0; chestIndex < Main.maxChests; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest == null)
                    continue;

                Tile chestTile = Main.tile[chest.x, chest.y];
                if (chestTile.TileType == TileID.Containers && chestTile.TileFrameX == 11 * 36)
                {
                    bool skip = false;
                    foreach (Item item in chest.item)
                    {
                        if (existingIceItems.Contains(item.type))
                        {
                            skip = true;
                            break;
                        }
                    }

                    if (skip)
                        continue;
                    foreach (int lootItem in guaranteedLoot)
                    {
                        for (int i = 0; i < Chest.maxItems; i++)
                        {
                            if (chest.item[i].type == ItemID.None)
                            {
                                chest.item[i].SetDefaults(lootItem);
                                chest.item[i].stack = 1;
                                goto NextChest;
                            }
                        }
                    }

                NextChest:;
                }
            }
        }
    }
}
