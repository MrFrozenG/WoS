using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Magic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Magic;

public class FrostbrandGrimoire : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Magic;
        Item.damage = 35;
        Item.knockBack = 1f;
        Item.crit = 12;
        Item.noMelee = true;
        Item.mana = 3;

        Item.width = 28;
        Item.height = 30;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 21;
        Item.useTime = 3;
        Item.autoReuse = true;
        Item.reuseDelay = 15;
        Item.UseSound = SoundID.Item4;

        Item.shoot = ModContent.ProjectileType<FrostbrandGrimoireBlade>();
        Item.shootSpeed = 5.4f;

        Item.rare = ItemRarityID.Blue;
        Item.value = Item.sellPrice(0, 0, 55, 75);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        position = Main.MouseWorld;
        Vector2 targetPosition = Main.MouseWorld;
        float radius = 420f;
            Vector2 spawnOffset = new Vector2(Main.rand.NextFloat(-radius, radius), Main.rand.NextFloat(-radius, radius));
            Vector2 spawnPosition = targetPosition + spawnOffset;

            Vector2 direction = targetPosition - spawnPosition;
            direction.Normalize();
            direction *= Item.shootSpeed; 
            Projectile.NewProjectile(source, spawnPosition, direction, type, damage, Item.knockBack, player.whoAmI);
        
        return false;
    }
}
