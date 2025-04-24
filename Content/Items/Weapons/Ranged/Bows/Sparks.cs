using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Melee;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Items.Weapons.Ranged.Bows;
public class Sparks : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        Item.damage = 28;
        Item.knockBack = 1.3f;
        Item.crit = 16;
        Item.noMelee = true;

        Item.width = 28;
        Item.height = 58;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 30;
        Item.useTime = 24;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item5;
        Item.GetGlobalItem<ItemsUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/Bows/Sparks_glow").Value;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 5, 35, 0);

        Item.useAmmo = AmmoID.Arrow;
        Item.shoot = ProjectileID.PurificationPowder;
        Item.shootSpeed = 10.5f;

        string path = Item.ModItem?.Texture + "_glow";
        if (ModContent.HasAsset(path))
        {
            Item.GetGlobalItem<ItemsUtilsGlow>().glowTexture = ModContent.Request<Texture2D>(path).Value;
        }
    }

    public override bool CanConsumeAmmo(Item ammo, Player player)
    {
        if (Main.dayTime)
        {
            return Main.rand.NextFloat() >= 1f;
        }
        return true;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI);
        if (player.ownedProjectileCounts[type = ModContent.ProjectileType<SunshineShot>()] < 6)
        {
            type = ModContent.ProjectileType<SunshineShot>();
            damage = Item.damage;
            const int NumProjectiles = 3;
            for (int i = 0; i < NumProjectiles; i++)
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
                newVelocity *= 1f - Main.rand.NextFloat(0.2f);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage / 2, knockback, player.whoAmI);
            }
        }
        return false;
    }
}