using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Tools;

namespace WoS.Content.Items.Tools
{
    public class PureFuries : ModItem
    {
        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.width = 32;
            Item.height = 32;
            Item.UseSound = SoundID.Item8;

            Item.noMelee = true;
            Item.shoot = ModContent.ProjectileType<PureFuriesWave>();
            Item.shootSpeed = 0f;

            Item.mana = 100;
        }

        public override bool CanUseItem(Player player)
        {
            // Чтобы волна не спамилась
            return player.ownedProjectileCounts[ModContent.ProjectileType<PureFuriesWave>()] < 1;
        }
    }
}
