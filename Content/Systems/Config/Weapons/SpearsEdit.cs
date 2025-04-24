using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals.GlobalNPCs;
using Microsoft.Xna.Framework;
using WoS.Content.Buffs.Damage;
using WoS.Content.Buffs;

namespace WoS.Content.Config.Weapons
{
    public class SpearsEditP : GlobalProjectile
    {
        public override bool AppliesToEntity(Projectile proj, bool lateInstantiation)
        {
            switch (proj.type)
            {
                case ProjectileID.CobaltNaginata:
                case ProjectileID.PalladiumPike:
                case ProjectileID.MythrilHalberd:
                case ProjectileID.OrichalcumHalberd:
                case ProjectileID.AdamantiteGlaive:
                case ProjectileID.TitaniumTrident:
                case ProjectileID.TheRottedFork:
                    return true;
                default:
                    return false;
            }
        }
        public override bool IsLoadingEnabled(Mod mod)
        {
            return ModContent.GetInstance<VanilaConfig>().ReworkSpears;
        }
        public override void OnHitNPC(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (proj.type == ProjectileID.CobaltNaginata || proj.type == ProjectileID.PalladiumPike)
            {
                target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
                target.GetGlobalNPC<GNPCsBuffs>().BleedingBonus = 5;
            }
            if (proj.type == ProjectileID.MythrilHalberd || proj.type == ProjectileID.OrichalcumHalberd)
            {
                target.AddBuff(ModContent.BuffType<Bleeding>(), 140);
                target.GetGlobalNPC<GNPCsBuffs>().BleedingBonus = 10;
            }
            if (proj.type == ProjectileID.AdamantiteGlaive || proj.type == ProjectileID.TitaniumTrident)
            {
                target.AddBuff(ModContent.BuffType<Bleeding>(), 160);
                target.AddBuff(ModContent.BuffType<Breaking>(), 320);
                target.GetGlobalNPC<GNPCsBuffs>().BleedingBonus = 15;
            }
            if (proj.type == ProjectileID.TheRottedFork)
            {
                target.AddBuff(ModContent.BuffType<Bleeding>(), 40);
                target.GetGlobalNPC<GNPCsBuffs>().BleedingBonus = 2;
            }
        }

        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            target.AddBuff(ModContent.BuffType<Bleeding>(), 120);
        }

        public override void AI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];

            if (player.altFunctionUse == 2 && projectile.type == ProjectileID.CobaltNaginata)
            {
                HandleDefensiveMode(projectile, player);
            }
        }
        private void HandleDefensiveMode(Projectile projectile, Player player)
        {
            float rotationSpeed = 0.1f;
            projectile.rotation += rotationSpeed;

            Vector2 offset = new Vector2(0, 40).RotatedBy(projectile.rotation);
            projectile.Center = player.Center + offset;

            projectile.velocity = Vector2.Zero;
        }
    }

    /*   public class SpearsEditI : GlobalItem
       {
           public override bool InstancePerEntity => true;
           public override bool AppliesToEntity(Item item, bool lateInstantiation)
           {
               return item.type == ItemID.CobaltNaginata;
           }
           public override void SetDefaults(Item item)
           {
               item.StatsModifiedBy.Add(Mod);
           }
           public override bool IsLoadingEnabled(Mod mod)
           {
               return ModContent.GetInstance<VanilaConfig>().ReworkSpears;
           }
           public override bool AltFunctionUse(Item item, Player player)
           {
               return true;
           }

           public override bool CanUseItem(Item item, Player player)
           {
               if (player.altFunctionUse == 2)
               {
                   item.autoReuse = true;
                   item.channel = true;
               }
               else
               {
                   item.autoReuse = false;
                   item.channel = false;
               }
               return true;
           }

           public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
           {
               if (player.altFunctionUse == 2)
               {
                   if (Main.myPlayer == player.whoAmI)
                   {
                       Projectile.NewProjectile(
                           source,
                           player.Center,
                           Vector2.Zero,
                           item.shoot,
                           item.damage,
                           item.knockBack,
                           player.whoAmI
                       );
                   }
               }
               return false;
           }
       }*/
}
