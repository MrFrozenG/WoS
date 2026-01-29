using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WoS.Content.Projectiles.Ammo;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Common;

namespace WoS.Content.Items.Weapons.Ranged.Firearm
{
    public class Embermaw : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 28;
            Item.knockBack = 1.5f;
            Item.crit = 11;
            Item.noMelee = true;

            Item.width = 84;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 35;
            Item.useTime = 35;
            Item.reuseDelay = 10;

            Item.autoReuse = false;

            Item.GetGlobalItem<ItemUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/Firearm/Embermaw_glow").Value;
            Item.UseSound = SoundID.Item36;

            Item.useAmmo = AmmoID.Bullet;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8.5f;

            Item.rare = ItemRarityID.Orange;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 2, 0);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.IllegalGunParts)
            .AddIngredient(ItemID.HellstoneBar, 12)
            .AddIngredient(ItemID.Bone, 30)
            .AddTile(TileID.Anvils)
                .Register();
        }

        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.15f;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-24f, 0f);
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            velocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int weaponDamage = damage;
            if (type == ProjectileID.Bullet || type == ProjectileID.SilverBullet) 
                type = ModContent.ProjectileType<MoltenBulletS>();
            int NumProjectiles = Main.rand.Next(4, 10 + 1);
            if (player.ZoneUnderworldHeight)
            {
                damage = (int)(damage * Main.rand.NextFloat(1.3f, 2.2f));
            }

            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
                newVelocity *= 1f - Main.rand.NextFloat(0.25f);
                Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                proj.usesLocalNPCImmunity = true;
                proj.localNPCHitCooldown = 5;
            }
            int EmbersProjectiles = Main.rand.Next(2, 4 + 1);
            for (int i = 0; i < EmbersProjectiles; i++)
            {
                Vector2 newVelocityA = velocity.RotatedByRandom(MathHelper.ToRadians(17));
                newVelocityA *= 0.5f + Main.rand.NextFloat(0.25f);
                Projectile projA = Projectile.NewProjectileDirect(source, position, newVelocityA, ModContent.ProjectileType<SmallEmbers>(), damage, knockback, player.whoAmI);
                projA.DamageType = DamageClass.Ranged;
                projA.damage = weaponDamage / 10;
            }
            return false;
        }
    }
}
