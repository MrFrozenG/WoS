using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Accessories
{
    public class MysteriousSkull : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.friendly = true; 
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 9999;
            Projectile.light = 0.5f;
            Projectile.netImportant = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override bool MinionContactDamage()
        {
            return false;
        }
        static int damagepertime = 6;
        public override void AI()
        {
            NPC target = Main.npc[(int)Projectile.ai[0]];
            if (!target.active || target.friendly || target.life <= 0)
            {
                Projectile.Kill();
                return;
            }

            Vector2 desiredPos = target.Center + new Vector2(0, -60f);
            Projectile.Center = Vector2.Lerp(Projectile.Center, desiredPos, 1f);

            Projectile.localAI[0]++; 
            if (Projectile.localAI[0] >= 60) 
            {
                Projectile.localAI[0] = 0;
                Projectile proj = Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Vector2.UnitY * 4f, 
                    ModContent.ProjectileType<MysteriousSkullShot>(),
                    damagepertime,
                    0f,
                    Projectile.owner
                );

                Projectile.ai[1]++;
                if (Projectile.ai[1] >= 4)
                    Projectile.Kill();
            }
        }
    }

    public class MysteriousSkullShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Summon;
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.minion = true;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.aiStyle = -1;
            Projectile.light = 0.1f;
            Projectile.oldPos = new Vector2[10];
        }

        public override void AI()
        {
            Projectile.rotation += 0.1f;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
        }
    }
}
