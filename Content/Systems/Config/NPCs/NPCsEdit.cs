using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Config;
using WoS.Content.Items.Weapons.Ranged;

namespace WoS.Content.Config.NPCs;

public class NPCsEdit : GlobalNPC
{
    private static int silverC = 100;
    private static int goldenC = 10000;
    private static int PlatinumC = 1000000;
    public override bool InstancePerEntity
    {
        get
        {
            return true;
        }
    }
    public override void SetDefaults(NPC npc)
    {
        if (ModContent.GetInstance<VanilaConfig>().InvincibleTownNPCS)
        {
            if (npc.townNPC)
            {
                npc.dontTakeDamageFromHostiles = true;
            }
        }
    }

    public override void ModifyShop(NPCShop shop)
    {
        if (ModContent.GetInstance<VanilaConfig>().InvincibleTownNPCS)
        {
            if (shop.NpcType == NPCID.DyeTrader)
            {
                shop.Add(new Item(ItemID.DyeTradersScimitar)
                {
                    shopCustomPrice = goldenC * 5 + silverC * 50
                });
            }
            if (shop.NpcType == NPCID.Painter)
            {
                shop.Add(new Item(ItemID.PainterPaintballGun)
                {
                    shopCustomPrice = goldenC * 5
                });
            }
            if (shop.NpcType == NPCID.DD2Bartender)
            {
                shop.Add(new Item(ItemID.AleThrowingGlove)
                {
                    shopCustomPrice = goldenC * 5 + silverC * 50
                });
            }
            if (shop.NpcType == NPCID.Stylist)
            {
                shop.Add(new Item(ItemID.StylistKilLaKillScissorsIWish)
                {
                    shopCustomPrice = goldenC * 5 + silverC * 50
                });
            }
            if (shop.NpcType == NPCID.Clothier)
            {
                shop.Add(new Item(ItemID.RedHat)
                {
                    shopCustomPrice = goldenC * 5 + silverC * 50
                });
            }
            if (shop.NpcType == NPCID.Mechanic)
            {
                shop.Add(new Item(ItemID.CombatWrench)
                {
                    shopCustomPrice = goldenC * 5 + silverC * 50
                });
            }
            if (shop.NpcType == NPCID.PartyGirl)
            {
                shop.Add(new Item(ItemID.PartyGirlGrenade)
                {
                    shopCustomPrice = silverC * 2 + 50
                });
            }

            if (shop.NpcType == NPCID.Merchant && Main.hardMode)
            {
                if (NPC.AnyNPCs(NPCID.TaxCollector))
                {
                    shop.Add(new Item(ItemID.TaxCollectorsStickOfDoom)
                    {
                        shopCustomPrice = silverC * 2 + 50
                    });
                }
            }

            if (shop.NpcType == NPCID.Princess && Main.hardMode)
            {
                shop.Add(new Item(ItemID.PrincessWeapon)
                {
                    shopCustomPrice = PlatinumC
                });
            }
        }
    }
}