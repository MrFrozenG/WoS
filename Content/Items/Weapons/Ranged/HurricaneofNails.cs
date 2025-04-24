using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WoS.Content.Systems.DamageClasses;
using Microsoft.Xna.Framework.Graphics;
using WoS.Content.Items.Resources;
using WoS.Content.Projectiles.Ammo;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Items.Weapons.Ranged
{
    public class HurricaneofNails : ModItem
    {
        public override void SetDefaults()
        {
//            Item.CloneDefaults(ItemID.NailGun);
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 150;
            Item.knockBack = 0.15f;
            Item.crit += 12;
            Item.noMelee = true;

            Item.width = 44;
            Item.height = 22;
            Item.scale = 1f;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item108;

            Item.GetGlobalItem<ItemsUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/HurricaneofNails_Glow").Value;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.reuseDelay = 12;

            Item.useAmmo = AmmoID.NailFriendly;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 12f;

            Item.rare = ItemRarityID.Yellow;
            Item.value = Item.sellPrice(0, 15, 50, 0);
        }
        public override bool CanConsumeAmmo(Item ammo, Player player)
        {
            return Main.rand.NextFloat() >= 0.15f;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-12f, 0f);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int NumProjectiles = Main.rand.Next(3,7);
            for (int i = 0; i < NumProjectiles; i++) 
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                newVelocity *= 1f - Main.rand.NextFloat(0.2f);
                Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                proj.CountsAsClass(ModContent.GetInstance<ForbiderDamage>());
                proj.ArmorPenetration += 10;
                proj.scale += 0.1f;
            }

            if (Main.rand.NextBool(3))
            {
                Vector2 newVelocityA = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                newVelocityA *= 1f - Main.rand.NextFloat(0.2f);
                Projectile projA = Projectile.NewProjectileDirect(source, position, newVelocityA, ModContent.ProjectileType<PoisonedNailS>(), damage, knockback, player.whoAmI);
                projA.CountsAsClass(ModContent.GetInstance<ForbiderDamage>());
                projA.ArmorPenetration += 10;
                projA.scale += 0.1f;

                if (Main.rand.NextBool(1, 7))
                {
                    Vector2 newVelocityB = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                    newVelocityB *= 1f - Main.rand.NextFloat(0.2f);
                    Projectile projB = Projectile.NewProjectileDirect(source, position, newVelocityB, ModContent.ProjectileType<CursedNailS>(), damage, knockback, player.whoAmI);
                    projB.CountsAsClass(ModContent.GetInstance<ForbiderDamage>());
                    projB.ArmorPenetration += 10;
                    projB.scale += 0.1f;
                    projB.CritChance = 100;
                }
                if (Main.rand.NextBool(1, 4))
                {
                    Vector2 newVelocityC = velocity.RotatedByRandom(MathHelper.ToRadians(5));
                    newVelocityC *= 1f - Main.rand.NextFloat(0.2f);
                    Projectile projC = Projectile.NewProjectileDirect(source, position, newVelocityC, ModContent.ProjectileType<MoltenNailS>(), damage, knockback, player.whoAmI);
                    projC.CountsAsClass(ModContent.GetInstance<ForbiderDamage>());
                    projC.ArmorPenetration += 10;
                    projC.scale += 0.1f;
                    projC.CritChance = 100;
                }
            }

            return false;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.NailGun)
            .AddIngredient(ItemID.FragmentVortex, 5)
            .AddIngredient(ModContent.ItemType<Ekuripusium>(), 50)
            .AddTile(TileID.LunarCraftingStation)
                .Register();
        }
    }
}
