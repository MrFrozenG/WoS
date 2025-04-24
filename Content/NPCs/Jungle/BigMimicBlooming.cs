using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Items.Placeable.Banners;
using Terraria.GameContent.Bestiary;
using WoS.Content.Items.Consumables.Keys;

namespace WoS.Content.NPCs.Jungle
{
    public class BigMimicBlooming : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = Main.npcFrameCount[NPCID.BigMimicCorruption];
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { 
                Velocity = 0f 
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, value);
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.UndergroundJungle,
                new FlavorTextBestiaryInfoElement("Mods.WoS.Bestiary.JungleHardmode.BigMimicBlooming")
            });
        }
        public override void SetDefaults()
        {
            NPC.width = 28;
            NPC.height = 44;
            NPC.aiStyle = 87;
            NPC.damage = 90;
            NPC.defense = 34;
            NPC.lifeMax = 3500;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath6;
            NPC.value = 30000f;
            NPC.knockBackResist = 0.1f;
            NPC.rarity = 5;
            Banner = NPC.type;
            BannerItem = ModContent.ItemType<BigMimicBloomingBanner>();
            AnimationType = NPCID.BigMimicCorruption;
        }

 /*       public static bool BigMimicSummonCheck(int x, int y)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient || !Main.hardMode)
                return false;

            int chestIndex = Chest.FindChest(x, y);
            if (chestIndex < 0)
                return false;

            int growthKeyCount = 0;
            int nonGrowthKeyCount = 0;

            for (int i = 0; i < 40; i++)
            {
                Item item = Main.chest[chestIndex].item[i];
                if (item != null && item.type > 0)
                {
                    if (item.type == ModContent.ItemType<GrowthKey>())
                        growthKeyCount += item.stack;
                    else
                        nonGrowthKeyCount++;
                }
            }

            if (nonGrowthKeyCount == 0 && growthKeyCount == 1)
            {
                // Удаляем сундук и создаем мимика
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
                    Player player = Main.LocalPlayer;
                    int npcType = ModContent.NPCType<BigMimicBlooming>();
                    int npcIndex = NPC.NewNPC(player.GetSource_TileInteraction(x, y), x * 16 + 16, y * 16 + 32, npcType);
                    Main.npc[npcIndex].whoAmI = npcIndex;
                    NetMessage.SendData(MessageID.SyncNPC, number: npcIndex);
                    Main.npc[npcIndex].BigMimicSpawnSmoke();
                }
            }
            return false;
        }*/
    }
}