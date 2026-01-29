using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Melee.Spears;
using Terraria.Audio;
using WoS.Content.Core.CritSystem;

namespace WoS.Content.Items.Weapons.Melee.Spears
{
    public class PiercingStar : ModItem, ICriticalDamageProvider
    {
        public float BaseCriticalDamage => 0.25f;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.SkipsInitialUseSound[Item.type] = true;
            ItemID.Sets.Spears[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.DamageType = DamageClass.Melee;
            Item.damage = 40;
            Item.noMelee = true;
            Item.knockBack = 3.5f;

            Item.width = Item.height = 42;
            Item.noUseGraphic = true;


            Item.useStyle = ItemUseStyleID.Shoot;
            Item.useAnimation = 12;
            Item.useTime = 18;
            Item.autoReuse = false;
            Item.UseSound = SoundID.Item1;

            Item.rare = ItemRarityID.Blue;
            Item.shoot = ModContent.ProjectileType<PiercingStarSpear>();
            Item.shootSpeed = 4.5f;
        }
        public override bool CanUseItem(Player player)
        {
            // Ensures no more than one spear can be thrown out, use this when using autoReuse
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
        public override bool? UseItem(Player player)
        {
            // Because we're skipping sound playback on use animation start, we have to play it ourselves whenever the item is actually used.
            if (!Main.dedServ && Item.UseSound.HasValue)
            {
                SoundEngine.PlaySound(Item.UseSound.Value, player.Center);
            }

            return null;
        }
    }
}
