using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Weapons.Common;

namespace WoS.Content.Projectiles.Ammo.Rockets
{
    public class BreakpointRocketShot : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
            Main.projFrames[Projectile.type] = 3;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Ranged;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 0.05f;
            Projectile.extraUpdates = 1;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Ammo/Rockets/BreakpointRocketShot_Glow").Value;
        }
        private int lifeTimer = 0;
        public override void AI()
        {
            Visual();

            if (Projectile.ai[1] == 1f)
            {
                // Режим прямого полёта (запущено косой)
                DirectFlightMode();
            }
            else
            {
                // Режим самонаведения
                Homing();
            }
        }
        private void DirectFlightMode()
        {
            Projectile.rotation = Projectile.velocity.ToRotation();

            // ракета не должна падать → отключаем всё влияние гравитации
            // (ваниль сама гравитацию НЕ добавляет, если нет aiStyle)

            // если почти остановилась — взрываем
            if (Projectile.velocity.LengthSquared() < 0.1f)
            {
                Explode();
                return;
            }

            lifeTimer++;

            if (lifeTimer >= 300)
            {
                Explode();
            }
        }
        private void Visual()
        {
            if (++Projectile.frameCounter >= 5)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        private void Homing()
        {
            float maxDetect = 600f;       // радиус поиска цели
            float homingStrength = 16f;   // “резкость” поворота

            NPC target = null;
            float highestLife = -1f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(this) && !npc.friendly)
                {
                    float distance = Vector2.Distance(Projectile.Center, npc.Center);

                    if (distance < maxDetect && npc.lifeMax > highestLife)
                    {
                        highestLife = npc.lifeMax;
                        target = npc;
                    }
                }
            }

            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();
                direction *= 12f; // скорость полёта

                Projectile.velocity =
                    (Projectile.velocity * (homingStrength - 1) + direction) / homingStrength;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Explode();
            return true;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Explode();
        }
        private void Explode()
        {

            if (Projectile.owner == Main.myPlayer)
            {
                Projectile proj = Projectile.NewProjectileDirect(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    Vector2.Zero,
                    ModContent.ProjectileType<Explosion>(),
                    Projectile.damage,
                    Projectile.knockBack,
                    Projectile.owner
                );

                proj.scale = 1f;
                if (Projectile.ai[1] == 1f)
                {
                    proj.CritChance = 0;
                    proj.damage /= 2;
                }
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
            Projectile.Kill();
        }
    }
}
