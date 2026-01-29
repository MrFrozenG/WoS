using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Summon.Whips;

namespace WoS.Content.Items.Weapons.Summon.Whips
{
    public class WishDragon : ModItem
    {
        public override void SetStaticDefaults()
        {
        }
        public override void SetDefaults()
        {
            Item.autoReuse = false;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.width = 18;
            Item.height = 18;
            Item.shoot = ModContent.ProjectileType<WishDragonWhip>();
            Item.noMelee = true;
            /*
            summon = true;
            */
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.DamageType = DamageClass.SummonMeleeSpeed;
            Item.noUseGraphic = true;
            Item.damage = 24;
            Item.knockBack = 0.6f;
            Item.shootSpeed = 4;
            Item.UseSound = SoundID.Item152;
            Item.rare = ItemRarityID.LightRed;
        }
        public override bool MeleePrefix()
        {
            return true;
        }
    }
}