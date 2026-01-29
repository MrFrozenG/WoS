using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;
using WoS.Content.Core.Changes;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;
using WoS.Content.Core.CritSystem;

namespace WoS.Content.Items.Weapons.Ranged.Throw
{
    public class HuntingKnife : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.25f;

        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 13;
            Item.knockBack = 1.1f;
            Item.noMelee = true;
            Item.maxStack = 1;

            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = Item.useTime = ModContent.GetInstance<MainConfig>().ThrowingWeaponsSpeed - 3;
            Item.noUseGraphic = true;

            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;

            Item.shoot = ModContent.ProjectileType<HuntingKnifeThrow>();
            Item.shootSpeed = 9.5f;

            Item.rare = ItemRarityID.Blue;
            Item.value = ItemValues.Cost(0, 25, 1, 0);
        }

        public override bool MeleePrefix()
        {
            return true;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (player.GetModPlayer<MainPlayer>().HunterGloves && Main.rand.NextBool(20))
            {
                Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(3));
                newVelocity *= 1f - Main.rand.NextFloat(0.45f);
                Projectile.NewProjectileDirect(source, position, newVelocity, type, (int)(damage * 1.5f), knockback, player.whoAmI);
            }
            return true;
        }
    }
}
