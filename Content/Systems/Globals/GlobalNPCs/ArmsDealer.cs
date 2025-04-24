using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Ammo.Bullets;
using WoS.Content.Items.Weapons.Ranged;

namespace WoS.Content.Systems.Globals.GlobalNPCs;
public class ArmsDealer : GlobalNPC
{
    private static int silverC = 100;
    private static int goldenC = 10000;
    private static int PlatinumC = 1000000;

    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == NPCID.ArmsDealer)
        {
            shop.Add(new Item(ItemID.SlimeCrown)
            {
            });
            if (Main.hardMode)
            {
                shop.Add(new Item(ModContent.ItemType<FrostBullet>())
                {
                    shopCustomPrice = (silverC)
                });
                shop.Add(new Item(ModContent.ItemType<MoltenBullet>())
                {
                    shopCustomPrice = (silverC + 25)
                });
            }
        }
    }
}
