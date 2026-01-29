using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Items.Weapons.Ranged.Bows
{
    public class WildBeast : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.15f;

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 15;
            Item.knockBack = 1.1f;
            Item.crit = 2;
            Item.noMelee = true;

            Item.width = 24;
            Item.height = 54;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 28;
            Item.useTime = 28;
            Item.UseSound = SoundID.Item5;

            Item.rare = ItemRarityID.Blue;
            Item.sellPrice(0, 0, 35, 0);
            Item.buyPrice(0, 1, 50, 0);

            Item.useAmmo = AmmoID.Arrow;
            Item.shoot = ProjectileID.PurificationPowder;
            Item.shootSpeed = 8.5f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(1));
            if (type == ProjectileID.WoodenArrowFriendly)
            {
                int newDamage = damage;
                newVelocity *= 1f + 0.75f;
                newDamage *= (int)1.5f;
                Projectile proj = Projectile.NewProjectileDirect(source, position, newVelocity, type, newDamage, knockback, player.whoAmI);
                proj.CritChance += 16;
            }
            else
            {
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
            }
            return false;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            var modPlayer = player.GetModPlayer<MainPlayer>();

            if (modPlayer.HunterGloves)
            {
                // Добавляем +3 к чистому урону (flat)
                // Flat добавляем через Additive часть, а не multiplicative — чтобы не ломать баланс
                damage.Base += 3f;
            }
        }

        public override void ModifyWeaponCrit(Player player, ref float crit)
        {
            var modPlayer = player.GetModPlayer<MainPlayer>();

            if (modPlayer.HunterGloves)
            {
                crit += 4f; // +4% крит-шанса
            }
        }
    }
}
