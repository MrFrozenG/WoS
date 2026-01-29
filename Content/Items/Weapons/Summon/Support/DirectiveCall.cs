
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.DamageClasses;
using WoS.Content.Core.Interfaces;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Common;
using WoS.Content.Projectiles.Weapons.Summon;

namespace WoS.Content.Items.Weapons.Summon.Support
{
    public class DirectiveCall : ModItem, ISupportWeapon, ICriticalDamageProvider
    {
        public int BaseSupportPoints => 2; 

        public float BaseCriticalDamage => 1.25f;
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.autoReuse = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 1, 0);

            Item.DamageType = ModContent.GetInstance<SummonSupport>();
            Item.damage = 75;
            Item.knockBack = 2.5f;
            Item.GetGlobalItem<ItemUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Summon/Support/DirectiveCall_Glow").Value;
            Item.shoot = ModContent.ProjectileType<RedLaser>();
            Item.shootSpeed = 11f;
            Item.UseSound = SoundID.Item12;
        }
        public override bool Shoot(Player player, Terraria.DataStructures.EntitySource_ItemUse_WithAmmo source,
            Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            int supportPoints = 0;
            if (Item.GetGlobalItem<ItemUtilsSupport>() is ItemUtilsSupport global)
            {
                supportPoints = global.SupportPoints;
            }

            float damageBonusMultiplier = 1f + 0.05f * supportPoints;
            int modifiedDamage = (int)(damage * damageBonusMultiplier);

            // 1. Выстрел лазером
            Projectile proj = Projectile.NewProjectileDirect(
                source,
                position,
                velocity,
                type,
                modifiedDamage,
                knockback,
                player.whoAmI
            );

            // Присваиваем тип урона как Summon
            proj.DamageType = ModContent.GetInstance<SummonSupport>();

            // 2. Проверка дрона
            bool droneExists = false;
            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile p = Main.projectile[i];
                if (p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<DirectiveDrone>())
                {
                    droneExists = true;
                    break;
                }
            }

            if (!droneExists)
            {
                // Призываем дрон чуть выше игрока
                Vector2 spawnPos = player.Center + new Vector2(0, -48f);
                Projectile.NewProjectile(
                    player.GetSource_ItemUse(Item),
                    spawnPos,
                    Vector2.Zero,
                    ModContent.ProjectileType<DirectiveDrone>(),
                    0, // урон дрон сам не наносит напрямую
                    0f,
                    player.whoAmI
                );
            }

            return false; // чтобы не создавалось стандартное снаряжение
        }
    }
}
