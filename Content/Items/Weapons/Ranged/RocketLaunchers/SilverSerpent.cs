using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModUtils;
using WoS.Content.Items.Ammo.Rockets;
using WoS.Content.Projectiles.Ammo;
using WoS.Content.Projectiles.Ammo.Rockets;

namespace WoS.Content.Items.Weapons.Ranged.RocketLaunchers
{
    public class SilverSerpent : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.75f;
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 85;
            Item.knockBack = 3.1f;
            Item.noMelee = true;

            Item.width = 56;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 40;
            Item.useTime = 40;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item33;

            Item.rare = ItemRarityID.LightRed;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 1, 0);

            Item.useAmmo = AmmoID.Rocket;
            Item.shoot = ModContent.ProjectileType<BreakpointRocketShot>();
            Item.shootSpeed = 7.5f;

            Item.GetGlobalItem<ItemUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Ranged/RocketLaunchers/SilverSerpent_Glow").Value;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-16f, 0f);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (source.AmmoItemIdUsed == ModContent.ItemType<BreakpointRocket>())
            {
                int numProjectiles = Main.rand.Next(2, 3 + 1); // 2–3 ракеты

                for (int i = 0; i < numProjectiles; i++)
                {
                    Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(4));
                    newVelocity *= 1f - Main.rand.NextFloat(0.25f);

                    Projectile proj = Projectile.NewProjectileDirect(
                        source,
                        position,
                        newVelocity,
                        type,
                        damage,        // нормальный урон
                        knockback,
                        player.whoAmI
                    );

                    proj.usesLocalNPCImmunity = true;
                    proj.localNPCHitCooldown = 5;
                    proj.DamageType = DamageClass.Ranged;
                }
            }
            else
            {
                float[] angles = { 0f, 15f, -15f };

                foreach (float angle in angles)
                {
                    Vector2 vel = velocity.RotatedBy(MathHelper.ToRadians(angle));

                    Projectile proj = Projectile.NewProjectileDirect(
                        source,
                        position,
                        vel,
                        type,
                        damage / 3,        // ослабленный урон
                        knockback / 3,
                        player.whoAmI
                    );
                    proj.ai[1] = 1f;
                    proj.DamageType = DamageClass.Ranged;
                }

            }
            return false;
        }
    }
}
