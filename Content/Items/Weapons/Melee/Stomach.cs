using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using WoS.Content.Core.Interfaces;
using WoS.Content.Core.CritSystem;

namespace WoS.Content.Items.Weapons.Melee;

public class Stomach : ModItem, ICriticalDamageProvider
{
    public float BaseCriticalDamage => 0.25f;
    public override void SetStaticDefaults()
    {
       // ItemID.Sets.Spears[Item.type] = true;
    }
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Melee;
        Item.damage = 44;
        Item.knockBack = 1.1f;

        Item.width = Item.height = 56;

        Item.useStyle = ItemUseStyleID.Swing;
        Item.useAnimation = 20;
        Item.useTime = 20;
        Item.autoReuse = false;
        Item.UseSound = SoundID.Item1;

        Item.rare = ItemRarityID.Orange;
        Item.shoot = ModContent.ProjectileType<StomachShot>();
        Item.shootSpeed = 7.3f;
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        /*        Vector2 newVelocity = velocity.RotatedBy(15f);
                newVelocity *= 1f;
                Projectile.NewProjectileDirect(source, position, newVelocity, type, damage, knockback, player.whoAmI);
                return false;
        */
        float numberProjectiles = 3; // Количество снарядов
        float rotation = MathHelper.ToRadians(10); // Угол разброса (10 градусов)

        for (int i = 0; i < numberProjectiles; i++)
        {
            // Смещаем угол для каждого снаряда
            Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * 0.75f;

            // Создаём снаряд
            Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
        }
        return false;
    }

    public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
    {
        damageDone *= (int) (1f + BaseCriticalDamage);
    }
}
