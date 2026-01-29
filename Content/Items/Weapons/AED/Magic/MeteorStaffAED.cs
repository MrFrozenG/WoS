using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using WoS.Content.Projectiles.Weapons.Magic;
using WoS.Content.Core.CritSystem;
using WoS.Content.Projectiles.Weapons.AED.Magic;

namespace WoS.Content.Items.Weapons.AED.Magic
{
    public class StaffofConstellations : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.65f;

        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 125;
            Item.crit = 4;
            Item.knockBack = 5.5f;
            Item.noMelee = true;

            Item.mana = 14;

            Item.width = Item.height = 46;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 24;
            Item.useAnimation = 24;
            Item.shootSpeed = 7.5f;
            Item.autoReuse = true;

            Item.rare = ItemRarityID.Orange;
            Item.UseSound = SoundID.Item88;

            Item.shoot = ProjectileID.PurificationPowder; 
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.MouseWorld;

            // --- Расчёт бонусной скорости снарядов ---
            float useSpeedFactor = (40f / Item.useTime); // чем меньше useTime, тем быстрее каст
            float speedMultiplier = 1.0f + (Item.shootSpeed * 0.10f) + (useSpeedFactor * 0.25f);

            // --- Звёзды ---
            int starCount = Main.rand.Next(1, 4); // 0–2
            for (int i = 0; i < starCount; i++)
            {
                int starType = ModContent.ProjectileType<ConstellationsStars>();
                //int starType = ProjectileID.FallingStarSpawner;

                // Более широкий разброс по X и Y
                Vector2 spawnPos = target + new Vector2(
                    Main.rand.NextFloat(-280f, 280f),
                    Main.rand.NextFloat(-800f, -1050f)
                );

                float angleOffset = MathHelper.ToRadians(Main.rand.NextFloat(-4f, 4f)); // чуть шире угол
                float baseSpeedStar = Item.shootSpeed * Main.rand.NextFloat(2.7f, 3.7f) * speedMultiplier;
                Vector2 velocityStar = Vector2.UnitY.RotatedBy(angleOffset) * baseSpeedStar;

                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    spawnPos,
                    velocityStar,
                    starType,
                    damage / 2,
                    knockback,
                    player.whoAmI
                );
            }

            // --- Астероиды ---
            int asteroidCount = Main.rand.Next(2, 5); // 2–4
            for (int i = 0; i < asteroidCount; i++)
            {
                int asteroidType = ModContent.ProjectileType<Asteroids>();

                // Оставляем прежнюю высоту и разброс
                Vector2 spawnPos = target + new Vector2(
                    Main.rand.NextFloat(-240f, 240f),
                    Main.rand.NextFloat(-800f, -1050f)
                );

                // Направляем ближе к курсору
                Vector2 dir = target - spawnPos;
                dir.Normalize();

                // Добавляем лёгкий разброс (±4 градуса)
                dir = dir.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-4f, 4f)));

                float baseSpeedAsteroid = Item.shootSpeed * Main.rand.NextFloat(0.8f, 1.2f) * speedMultiplier;
                Vector2 velocityAsteroid = dir * baseSpeedAsteroid;

                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    spawnPos,
                    velocityAsteroid,
                    asteroidType,
                    damage,
                    knockback,
                    player.whoAmI
                );
            }

            return false;
        }
    }
}