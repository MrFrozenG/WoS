using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Common;
using WoS.Content.Projectiles.Weapons.Ranged.Bow;

namespace WoS.Content.Items.Weapons.Ranged.Bows
{
    public class SilverFalcon : ModItem, ICriticalDamageProvider
    {
        private int shotCounter = 0;

        public float BaseCriticalDamage => 0.75f;

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 55;
            Item.knockBack = 1.1f;
            Item.noMelee = true;

            Item.width = 32;
            Item.height = 66;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 42;
            Item.useTime = 14;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item33;

            Item.rare = ItemRarityID.LightRed;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 1, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 13.5f;

            Item.GetGlobalItem<ItemUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/Bows/SilverFalcon_Glow").Value;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float[] angles = { 0f, 25f, -25f };

            foreach (float angle in angles)
            {
                Vector2 vel = velocity.RotatedBy(MathHelper.ToRadians(angle));
                Projectile proj = Projectile.NewProjectileDirect(
                    source,
                    position,
                    vel,
                    ModContent.ProjectileType<RedLaser>(),
                    damage,
                    knockback,
                    player.whoAmI
                );
                proj.DamageType = DamageClass.Ranged;
                proj.arrow = true;
            }

            shotCounter++;
            if (shotCounter == 2)
            {
                shotCounter = 0;

                // Дополнительный снаряд SilverFalconShot
                Projectile.NewProjectile(
                    source,
                    position,
                    velocity * 1.5f,
                    ModContent.ProjectileType<SilverFalconShot>(),
                    (int)(damage * 1.2f), 
                    knockback,
                    player.whoAmI
                );
            }
            return false;
        }
    }
}
