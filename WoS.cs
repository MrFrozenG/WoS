using Terraria.ModLoader;
<<<<<<< Updated upstream
=======
using WoS.Content.Items.Consumables.Keys;
using WoS.Content.NPCs.Jungle;
>>>>>>> Stashed changes

namespace WoS
{
	public class WoS : Mod
	{
<<<<<<< Updated upstream
	}
=======
        public static int ClownEventCurrencyId;
        
        public override void Load()
        {
            //ClownEventCurrencyId = CustomCurrencyManager.RegisterCurrency(new Content.Items.Currencies.CircusEventCoupons(ModContent.ItemType<CircusCoupons>(), 999L, "Mods.WoS.Currencies.CircusCoupons"));
            Terraria.On_NPC.BigMimicSummonCheck += NPC_BigMimicSummonCheck;
        }
        public bool NPC_BigMimicSummonCheck(Terraria.On_NPC.orig_BigMimicSummonCheck orig, int x, int y, Terraria.Player player)
        {
            bool originalResult = orig(x, y, player);
            if (Main.netMode == NetmodeID.MultiplayerClient || !Main.hardMode)
                return false;

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
                return originalResult;

            int growthKeyCount = 0;
            int nonGrowthKeyCount = 0;

            for (int i = 0; i < 40; i++)
            {
                Item item = Main.chest[chestIndex].item[i];
                if (item != null && item.type > ItemID.None)
                {
                    if (item.type == ModContent.ItemType<GrowthKey>()) growthKeyCount += item.stack;
                    else
                        nonGrowthKeyCount++;
                }
            }

            if (nonGrowthKeyCount == 0 && growthKeyCount == 1)
            {
                if (TileID.Sets.BasicChest[(int)Main.tile[x, y].TileType])
                {
                    if ((int)Main.tile[x, y].TileFrameX % 36 != 0)
                        --x;
                    if ((int)Main.tile[x, y].TileFrameY % 36 != 0)
                        --y;

                    Chest.DestroyChest(x, y);
                    for (int i1 = x; i1 <= x + 1; i1++)
                    {
                        for (int i2 = y; i2 <= y + 1; i2++)
                        {
                            if (TileID.Sets.BasicChest[(int)Main.tile[i1, i2].TileType])
                                Main.tile[i1, i2].ClearTile();
                        }
                    }
                    Player player1 = Main.LocalPlayer;
                    int npcType = ModContent.NPCType<BigMimicBlooming>();
                    int npcIndex = NPC.NewNPC(player1.GetSource_TileInteraction(x, y), x * 16 + 16, y * 16 + 32, npcType);
                    Main.npc[npcIndex].whoAmI = npcIndex;
                    NetMessage.SendData(MessageID.SyncNPC, number: npcIndex);
                    Main.npc[npcIndex].BigMimicSpawnSmoke();
                }
            }
            return originalResult;
        }
    }
>>>>>>> Stashed changes
}