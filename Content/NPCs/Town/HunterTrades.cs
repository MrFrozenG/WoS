using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils.ShopConditions;
using WoS.Content.Core.Changes;

namespace WoS.Content.NPCs.Town
{
    public class HunterTrades : GlobalNPC
    {
        private static int BaseEnemiesLoot = 100;
        private static int RareEnemiesLoot = 1_500;
        private static int HMRareEnemiesLoot = 5_050;
        public override void ModifyShop(NPCShop npcShop)
        {
            if (npcShop.NpcType == ModContent.NPCType<HunterTrader>() && npcShop.Name == HunterTrader.HunterShop)
            {
                if (ModContent.GetInstance<ModContentConfig>().HunterAlwaysSellsMaterials)
                {
                    npcShop.Add(new Item(ItemID.Gel) { shopCustomPrice = BaseEnemiesLoot });
                    npcShop.Add(new Item(ItemID.Feather) { shopCustomPrice = BaseEnemiesLoot * 3 }, Condition.DownedEowOrBoc);
                    npcShop.Add(new Item(ItemID.WormTooth) { shopCustomPrice = BaseEnemiesLoot }, Condition.DownedEowOrBoc);
                    npcShop.Add(new Item(ItemID.Stinger) { shopCustomPrice = BaseEnemiesLoot * 2 }, Condition.DownedQueenBee);
                    npcShop.Add(new Item(ItemID.FlinxFur) { shopCustomPrice = RareEnemiesLoot });
                    npcShop.Add(new Item(ItemID.AntlionMandible) { shopCustomPrice = BaseEnemiesLoot * 2 });
                    npcShop.Add(new Item(ItemID.Lens) { shopCustomPrice = BaseEnemiesLoot * 2 }, Condition.DownedEyeOfCthulhu);
                    npcShop.Add(new Item(ItemID.BlackLens) { shopCustomPrice = RareEnemiesLoot * 2 }, Condition.Hardmode);
                    npcShop.Add(new Item(ItemID.SpiderFang) { shopCustomPrice = HMRareEnemiesLoot }, Condition.Hardmode);
                    npcShop.Add(new Item(ItemID.SharkFin) { shopCustomPrice = RareEnemiesLoot });
                    npcShop.Add(new Item(ItemID.PixieDust) { shopCustomPrice = RareEnemiesLoot * 4 });
                    npcShop.Add(new Item(ItemID.UnicornHorn) { shopCustomPrice = HMRareEnemiesLoot }, Condition.Hardmode);
                    npcShop.Add(new Item(ItemID.Vine) { shopCustomPrice = BaseEnemiesLoot * 5 });
                    npcShop.Add(new Item(ItemID.TurtleShell) { shopCustomPrice = HMRareEnemiesLoot }, Condition.DownedPlantera);
                }
                else
                {
                    npcShop.Add(new Item(ItemID.Gel) { shopCustomPrice = BaseEnemiesLoot });
                    npcShop.Add(new Item(ItemID.Feather) { shopCustomPrice = BaseEnemiesLoot * 3 }, Condition.DownedEowOrBoc);
                    npcShop.Add(new Item(ItemID.WormTooth) { shopCustomPrice = BaseEnemiesLoot }, Condition.DownedEowOrBoc);
                    npcShop.Add(new Item(ItemID.Stinger) { shopCustomPrice = BaseEnemiesLoot * 2 }, Condition.DownedQueenBee);
                    npcShop.Add(new Item(ItemID.FlinxFur) { shopCustomPrice = RareEnemiesLoot }, Condition.InSnow);
                    npcShop.Add(new Item(ItemID.AntlionMandible) { shopCustomPrice = BaseEnemiesLoot * 2 }, Condition.InDesert);
                    npcShop.Add(new Item(ItemID.Lens) { shopCustomPrice = BaseEnemiesLoot * 2 }, Condition.DownedEyeOfCthulhu);
                    npcShop.Add(new Item(ItemID.BlackLens) { shopCustomPrice = RareEnemiesLoot * 2 }, Condition.Hardmode);
                    npcShop.Add(new Item(ItemID.SpiderFang) { shopCustomPrice = HMRareEnemiesLoot }, Condition.Hardmode, Condition.InRockLayerHeight);
                    npcShop.Add(new Item(ItemID.SharkFin) { shopCustomPrice = RareEnemiesLoot }, Condition.InBeach);
                    npcShop.Add(new Item(ItemID.PixieDust) { shopCustomPrice = RareEnemiesLoot * 4 }, Condition.InHallow);
                    npcShop.Add(new Item(ItemID.UnicornHorn) { shopCustomPrice = HMRareEnemiesLoot }, Condition.InHallow, Condition.Hardmode);
                    npcShop.Add(new Item(ItemID.Vine) { shopCustomPrice = BaseEnemiesLoot * 5 }, Condition.InJungle);
                    npcShop.Add(new Item(ItemID.TurtleShell) { shopCustomPrice = HMRareEnemiesLoot }, Condition.InJungle, Condition.DownedPlantera);
                }
            }
        }
    }
}
