using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.DamageClasses;
using WoS.Content.Core.Interfaces;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Magic;

namespace WoS.Content.Items.Weapons.Magic.Swords;

public class CosmicJuggernaut : ModItem, ISupportWeapon
{
    public int BaseSupportPoints => 2;

    public override void SetDefaults()
    {
        Item.DamageType = ModContent.GetInstance<MagicSupport>();
        Item.damage = 25;
        Item.knockBack = 1.3f;
        Item.crit = 16;

        Item.width = 56;
        Item.height = 56;
        Item.scale = 1.2f;
        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item1;
        Item.shoot = ModContent.ProjectileType<CosmicJuggernautShards>();
        Item.shootSpeed = 8.5f;
        //		Item.GetGlobalItem<ItemsUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Magic/CosmicJuggernaut_glow").Value;
        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 5, 35, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int supportPoints = Item.GetSupportPoints(player);

        // Ограничиваем максимум до 5 снарядов
        int projectileCount = Math.Min(supportPoints, 5);

        for (int i = 0; i < projectileCount; i++)
        {
            // Разброс ±18° и небольшая вариация скорости 70–100%
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(18));
            newVelocity *= 1f - Main.rand.NextFloat(0.3f);

            Projectile proj = Projectile.NewProjectileDirect(
                source,
                player.Center,
                newVelocity,
                type,
                damage / 2,
                knockback,
                player.whoAmI
            );

            // Передаём количество очков поддержки снаряду
            proj.ai[0] = supportPoints;
        }

        return false; // основной удар меча не трогаем
    }
    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient(ItemID.MeteoriteBar, 22)
        .AddTile(TileID.Anvils)
            .Register();
    }
}