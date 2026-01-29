using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using Terraria.DataStructures;
using WoS.Content.Projectiles.Ammo;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Ranged.Bows
{
    public class StarSplittingBow : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.15f;
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 23;
            Item.knockBack = 1.1f;
            Item.noMelee = true;

            Item.width = 26;
            Item.height = 50;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 36;
            Item.useTime = 24;
            Item.UseSound = SoundID.Item5;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 1, 0, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 7.5f;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ProjectileID.WoodenArrowFriendly || 
                type == ProjectileID.FrostArrow ||
                type == ProjectileID.FlamingArrow ||
                type == ProjectileID.UnholyArrow ||
                type == ProjectileID.FireArrow 
                )
            {
                Projectile Proj = Projectile.NewProjectileDirect(source, position, velocity * 1.5f, ProjectileID.JestersArrow, damage * 2, knockback * 1.5f, player.whoAmI);
                Proj.ArmorPenetration += 5;
                return false;
            }
            return true;
        }
    }
}
