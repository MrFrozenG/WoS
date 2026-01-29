using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Melee.Scythe;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Melee.Scythe
{
    public class CriticalException : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.75f;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 65;
            Item.DamageType = DamageClass.Melee;
            Item.knockBack = 5.5f;
            Item.channel = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 1, 0);

            Item.shoot = ModContent.ProjectileType<CriticalExceptionSwing>();
            Item.shootSpeed = 6.4f;
        }
        public bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            
            Vector2 spawnPos = player.MountedCenter;

            float offsetX = player.direction == 1 ? 6f : -6f;
            spawnPos.X += offsetX;
            if (player.ownedProjectileCounts[type] < 1)
            {
                Projectile.NewProjectile(
                    player.GetSource_ItemUse(item),
                    spawnPos,
                    Vector2.Zero, 
                    type,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }
            
            return false;
        }
    }
}
