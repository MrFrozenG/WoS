using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModWorld;

namespace WoS.Content.Items.Tools
{
    internal class OldCastle : ModItem
    {
        private const int Range = 32;
        private int Steps = 64;
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 30;
            Item.value = 10000;
            Item.rare = ItemRarityID.Orange;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 60;
            Item.useTime = 60;
            Item.mana = 100;
        }
        public override bool? UseItem(Player player)
        {
            int px = (int)(player.Center.X / 16f);
            int py = (int)(player.Center.Y / 16f);

            int startX = px - Range;
            int endX = px + Range;
            int startY = py - Range;
            int endY = py + Range;
            for (int i = 0; i < Steps; i++) 
            {
                for (int x = startX; x <= endX; x++)
                {
                    for (int y = startY; y <= endY; y++)
                    {
                        if (!WorldGen.InWorld(x, y)) continue;
                        int tileType = Main.tile[x, y].TileType;
                        if (tileType == TileID.Spikes && NPC.downedBoss3)
                        {
                            Item.NewItem(player.GetSource_ItemUse(Item), x * 16, y * 16, 16, 16, ItemID.Spike);
                            WorldGen.KillTile(x, y, false, false, true);
                        }

                        if (tileType == TileID.WoodenSpikes && NPC.downedPlantBoss)
                        {
                            Item.NewItem(player.GetSource_ItemUse(Item), x * 16, y * 16, 16, 16, ItemID.WoodenSpike);
                            WorldGen.KillTile(x, y, false, false, true);
                        }

                        if (tileType == TileID.Traps)
                        {
                            Item.NewItem(player.GetSource_ItemUse(Item), x * 16, y * 16, 16, 16, ItemID.DartTrap);
                            WorldGen.KillTile(x, y, false, false, true);
                        }
                        if (tileType == TileID.CrackedBlueDungeonBrick || tileType == TileID.CrackedGreenDungeonBrick || tileType == TileID.CrackedPinkDungeonBrick)
                        {
                            WorldGen.KillTile(x, y, false, false, true);
                        }
                    }
                }
            }
            return true;
        }
    }
}
