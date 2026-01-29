using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Magic;
using WoS.Content.Projectiles.Weapons.Magic.Spellbooks;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Magic.Spellbooks
{
    public class Snowfall : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 11;
            Item.knockBack = 1f;
            
            Item.noMelee = true;
            Item.mana = 3;

            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 8;
            Item.autoReuse = true;
            Item.reuseDelay = 25;
            Item.UseSound = SoundID.Item4;

            Item.shoot = ModContent.ProjectileType<SnowfallSnowflakes>();
            Item.shootSpeed = 5.4f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 55, 75);
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int snowflakeCount = Main.rand.Next(3, 6);

            float arc = MathHelper.ToRadians(30f);
            float startAngle = velocity.ToRotation() - arc / 2f;
            float deltaAngle = arc / (snowflakeCount - 1);

            for (int i = 0; i < snowflakeCount; i++)
            {
                float angle = startAngle + deltaAngle * i;
                Vector2 shootVelocity = angle.ToRotationVector2() * velocity.Length();

                Projectile.NewProjectile(
                    source,
                    position,
                    shootVelocity,
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
