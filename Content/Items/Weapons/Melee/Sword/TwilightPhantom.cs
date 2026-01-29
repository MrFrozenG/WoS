using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WoS.Content.Core.DamageClasses;
using WoS.Content.Projectiles.Weapons.Melee.Swords;
using WoS.Content.Core.Interfaces;
using WoS.Content.Core.ModUtils;
using System;

namespace WoS.Content.Items.Weapons.Melee.Sword
{
    public class TwilightPhantom : ModItem, ISupportWeapon
    {
        public int BaseSupportPoints => 3;

        public override void SetDefaults()
        {
            Item.width = 56;
            Item.height = 56;
            Item.value = Item.sellPrice(gold: 5);
            Item.rare = ItemRarityID.Yellow;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.autoReuse = true;
            Item.channel = true; 

            // Weapon properties
            Item.damage = 9;
            Item.knockBack = 6;
            Item.DamageType = ModContent.GetInstance<MeleeSupport>();

            // Projectile
            Item.shoot = ModContent.ProjectileType<TwilightPhantomBlade>();
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            return false; 
        }
        public override void HoldItem(Player player)
        {
            // Если игрок зажал кнопку атаки
            if (player.channel)
            {
                int supportPoints = Item.GetSupportPoints(player);
                int bladesCount = Math.Min(supportPoints, 8);

                // Проверяем, есть ли уже снаряды этого типа у игрока
                bool exists = false;
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == Item.shoot)
                    {
                        exists = true;
                        break;
                    }
                }

                // Если снарядов ещё нет — спавним
                if (!exists)
                {
                    for (int i = 0; i < bladesCount; i++)
                    {
                        Projectile.NewProjectile(
                            player.GetSource_ItemUse(Item),
                            player.Center,
                            Vector2.Zero,
                            Item.shoot,
                            Item.damage,
                            Item.knockBack,
                            player.whoAmI,
                            bladesCount,   // ai[0]
                            supportPoints,// ai[1]
                            i             // ai[2]
                        );
                    }
                }
            }
            else
            {
                // Если игрок отпустил кнопку — убиваем все мечи
                for (int i = 0; i < Main.maxProjectiles; i++)
                {
                    Projectile p = Main.projectile[i];
                    if (p.active && p.owner == player.whoAmI && p.type == Item.shoot)
                    {
                        p.Kill();
                    }
                }
            }
        }

        public override bool MeleePrefix() => true;

        public override float UseSpeedMultiplier(Player player)
        {
            int supportPoints = Item.GetSupportPoints(player);
            if (supportPoints <= 10)
                return 1f;

            int excess = supportPoints - 10;

            float bonus = Math.Min(excess * 0.01f, 0.25f);

            return 1f + bonus;
        }
    }
}
