using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Magic.Staffs;

namespace WoS.Content.Items.Weapons.Magic.Staffs
{
    public class VoodooStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Type] = true;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Magic;
            Item.damage = 60;
            Item.knockBack = 9.5f;
            Item.noMelee = true;
            Item.mana = 21;

            Item.width = 44;
            Item.height = 44;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 36;
            Item.useTime = 36;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item34;

            Item.shoot = ModContent.ProjectileType<VoodooEnergy>();
            Item.shootSpeed = 7.3f;

            Item.rare = ItemRarityID.Orange;
            Item.value = Item.sellPrice(0, 0, 25, 0);
        }
    }
}
