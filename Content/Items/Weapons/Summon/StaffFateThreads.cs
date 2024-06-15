using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Items.Weapons.Summon;

public class StaffFateThreads : ModItem
{

    public override void SetStaticDefaults()
    {
        ItemID.Sets.GamepadWholeScreenUseRange[Item.type] = true; 
        ItemID.Sets.LockOnIgnoresCollision[Item.type] = true;

        ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; 
    }
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Summon;
        Item.damage = 13;
        Item.knockBack = 1f;
        Item.crit = 6;
        Item.noMelee = true;
        Item.mana = 13;

        Item.width = 74;
        Item.height = 28;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 15;
        Item.useTime = 15;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item11;

        Item.shoot = ModContent.ProjectileType<DisciplineMark>();
        Item.shootSpeed = 12f;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 5, 35, 0);
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        position = Main.MouseWorld;
    }
}
public class DisciplineMark : ModProjectile
{
    public override void SetDefaults()
    {
        Projectile.width = Projectile.height = 32;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = -1;
    }
}