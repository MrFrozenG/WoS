using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Minion;
using WoS.Content.Projectiles.Weapons.Summon.Minions;

namespace WoS.Content.Items.Weapons.Summon.Minions
{
    public class StrangeSnowball : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.StaffMinionSlotsRequired[Type] = 0f; // The default value is 1, but other values are supported. See the docs for more guidance.
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 0f;
            Item.mana = 10; // mana cost
            Item.width = 22;
            Item.height = 22;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.value = 1000;
            Item.rare = ItemRarityID.Blue;
            Item.UseSound = SoundID.Item44; 

            Item.noMelee = true; 
            Item.DamageType = DamageClass.Summon;
            Item.buffType = ModContent.BuffType<SnowmanMinionBuff>();
            Item.shoot = ModContent.ProjectileType<StrangeSnowballThrow>();
            Item.shootSpeed = 10f;
        }
    }
}
