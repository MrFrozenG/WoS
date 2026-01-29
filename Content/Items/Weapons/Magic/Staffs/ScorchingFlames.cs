using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Magic.Staffs
{
    public class ScorchingFlames : ModItem
    {
        
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 36;
            Item.knockBack = 7.5f;
            Item.crit = 4;
            Item.noMelee = true;
            Item.mana = 15;

            Item.width = 28;
            Item.height = 30;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 30;
            Item.useTime = 6;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item34;

            Item.shoot = ProjectileID.Flames;
            Item.shootSpeed = 7.3f;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 1, 25, 0);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile proj = Projectile.NewProjectileDirect(source, position, velocity, type, damage / 2, knockback, player.whoAmI);
            proj.DamageType = DamageClass.Magic;
            proj.penetrate = 5;
            proj.ArmorPenetration = 5;
            proj.CritChance = Item.crit;
            proj.scale = 0.75f;

            return false;
        }
    }
}
