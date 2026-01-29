using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Melee.YoYo;

namespace WoS.Content.Items.Weapons.Melee.YoYo
{
    public class NewProtocol : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.75f;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.Yoyo[Item.type] = true;
            ItemID.Sets.GamepadExtraRange[Item.type] = 15;
            ItemID.Sets.GamepadSmartQuickReach[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 30;
            Item.height = 26;

            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.UseSound = SoundID.Item1;

            Item.damage = 75;
            Item.DamageType = DamageClass.MeleeNoSpeed;
            Item.knockBack = 3.5f;
            Item.channel = true;
            Item.rare = ItemRarityID.LightRed;
            Item.value = ItemValues.Cost(0, silver: 50, gold: 1, 0);

            Item.shoot = ModContent.ProjectileType<NewProtocolThrow>();
            Item.shootSpeed = 16f;
        }
    }
}
