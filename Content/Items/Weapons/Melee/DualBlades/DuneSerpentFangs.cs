using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using WoS.Content.Projectiles.Weapons.Melee.DualBlades;
using Microsoft.Xna.Framework;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Items.Weapons.Melee.DualBlades
{
    public class DuneSerpentFangs : ModItem
    {
        public override void SetDefaults()
        {
            //Static for Dual Blades:
            Item.damage = 12;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<DuneSerpentFangsThrowSingle>(); //Because we don't use any base shot
            Item.shootSpeed = 9.35f;

            //Changeable values
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = 30;
            Item.useTime = Item.useAnimation / 2;
            Item.autoReuse = true;

            Item.width = 44;
            Item.height = 44;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 55, 0);

            Item.knockBack = 1.8f;
            Item.crit = 3;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool RangedPrefix()
        {
            return true;
        }
        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool Shoot(
            Player player,
            EntitySource_ItemUse_WithAmmo source,
            Vector2 position,
            Vector2 velocity,
            int type,
            int damage,
            float knockback)
        {
            var modPlayer = player.GetModPlayer<MainPlayer>();

            // ===== ПКМ =====
            if (player.altFunctionUse == 2 && player.ownedProjectileCounts[ModContent.ProjectileType<DuneSerpentFangsThrow>()] != 2)
            {
                if (modPlayer.DuneSerpentFangsHits < 4)
                    return false;

                modPlayer.DuneSerpentFangsHits = 0;

                Vector2 dir = Vector2.Normalize(Main.MouseWorld - player.Center);
                Vector2 shootVel = dir * 12f;

                // Сдвоенные клинки
                Projectile.NewProjectile(
                    source,
                    player.Center,
                    shootVel,
                    ModContent.ProjectileType<DuneSerpentFangsThrow>(),
                    damage,
                    knockback,
                    player.whoAmI
                );
            }

            return true;
        }
    }
}
