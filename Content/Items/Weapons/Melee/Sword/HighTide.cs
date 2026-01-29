using Terraria.DataStructures;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using WoS.Content.Projectiles.Weapons.Melee.Swords;

namespace WoS.Content.Items.Weapons.Melee.Sword
{
    public class HighTide : ModItem
    {
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 16;
            Item.knockBack = 0.9f;
            Item.crit = 2;

            Item.width = 48 ;
            Item.height = 58;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<HighTideSlash>();
            Item.shootSpeed = 7.3f; 
            //Item.noMelee = true; 
            Item.shootsEveryUse = true;
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float adjustedItemScale = player.GetAdjustedItemScale(Item);

            Projectile.NewProjectile(
                source,
                player.MountedCenter,
                Vector2.Zero, // нулевая скорость, снаряд сам вылетает
                type,
                damage,
                knockback,
                player.whoAmI,
                player.direction * player.gravDir, // ai[0]
                player.itemAnimationMax,           // ai[1]
                adjustedItemScale                 // ai[2]
            );

            return false; // чтобы Terraria не создавала снаряд автоматически
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 300);
        }
    }
}
