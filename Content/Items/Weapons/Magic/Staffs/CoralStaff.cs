using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Magic.Staffs;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Magic.Staffs
{
    public class CoralStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 14;
            Item.knockBack = 1.5f;
            Item.noMelee = true;
            Item.mana = 3;

            Item.width = 44;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item34;

            Item.shoot = ModContent.ProjectileType<CoralFragments>();
            Item.shootSpeed = 12.3f;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int CoralCount = Main.rand.Next(1, 5);
            for (int i = 0; i < CoralCount; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
                newVelocity *= 1f - Main.rand.NextFloat(0.25f);
                Projectile.NewProjectile(
                    source,
                    position,
                    newVelocity,
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
