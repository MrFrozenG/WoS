using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using WoS.Content.Projectiles.Weapons.Melee.DualBlades;

namespace WoS.Content.Items.Weapons.Melee.DualBlades
{
    public class MoltenDualBlades : ModItem
    {
        public override void SetDefaults()
        {
            //Static for Dual Blades:
            Item.damage = 35;
            Item.noMelee = true;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ProjectileID.PurificationPowder; //Because we don't use any base shot
            Item.shootSpeed = 8f;
            //Changeable values
            Item.DamageType = DamageClass.Melee;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.autoReuse = true;

            Item.width = 42;
            Item.height = 40;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 0, 0);

            Item.knockBack = 1.8f;
            Item.crit = 8;
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override void AddRecipes()
        {
            CreateRecipe()
            .AddIngredient(ItemID.HellstoneBar, 15)
            .AddTile(TileID.Anvils)
                .Register();
        }
        public override bool RangedPrefix()
        {
            return true;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.altFunctionUse == 2 && player.whoAmI == Main.myPlayer &&
                player.ownedProjectileCounts[ModContent.ProjectileType<ObsidianBlade>()] <= 3 && player.ownedProjectileCounts[ModContent.ProjectileType<MoltenBlade>()] <= 3
                )
            {
                // Удаление всех MoltenDualBlade, принадлежащих игроку
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<MoltenDualBladesThrow>())
                    {
                        if (p.ModProjectile is MoltenDualBladesThrow blade)
                        {
                            blade.AlterUsage = true;
                        }
                    }
                }
            }
            else
            {
                if (player.ownedProjectileCounts[ModContent.ProjectileType<MoltenDualBladesThrow>()] < 3)
                {
                    Projectile.NewProjectileDirect(source, player.Center, velocity, ModContent.ProjectileType<MoltenDualBladesThrow>(), damage, knockback, player.whoAmI);
                }
            }
            return false;
        }
    }
}
