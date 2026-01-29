using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Minion;
using WoS.Content.Projectiles.Weapons.Summon.Minions;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WoS.Content.Items.Weapons.Summon.Minions
{
    public class GraniteElementalStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.GamepadWholeScreenUseRange[Type] = true; 
            ItemID.Sets.LockOnIgnoresCollision[Type] = true;

            ItemID.Sets.StaffMinionSlotsRequired[Type] = 1f; 
        }
        public override void SetDefaults()
        {
            Item.damage = 12;
            Item.knockBack = 10f;
            Item.mana = 10; 
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 36;
            Item.useAnimation = 36;
            Item.useStyle = ItemUseStyleID.Swing; 
            Item.value = Item.sellPrice(gold: 30);
            Item.rare = ItemRarityID.Cyan;
            Item.UseSound = SoundID.Item44; 

            Item.noMelee = true; 
            Item.DamageType = DamageClass.Summon; 
            Item.buffType = ModContent.BuffType<LittleGraniteElementalMinionBuff>();
            Item.shoot = ModContent.ProjectileType<LittleGraniteElemental>(); 
        }
        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            position = Main.MouseWorld;
            player.LimitPointToPlayerReachableArea(ref position);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            player.AddBuff(Item.buffType, 2);
            return true;
        }
    }
}
