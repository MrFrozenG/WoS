using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.Globals;

namespace WoS.Content.Projectiles.Weapons.Magic.Staffs
{
    public class VoodooEnergy : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            Main.projFrames[Type] = 6;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 2;
            Projectile.scale = 1f;
            Projectile.alpha = 120; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
            Projectile.light = 0.25f;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int dotCount = BuffsCategories.CountDebuffsFromCategory(target, "DoT");
            dotCount = Utils.Clamp(dotCount, 0, 4);

            int weaknessCount = BuffsCategories.CountDebuffsFromCategory(target, "Weakness");
            weaknessCount = Utils.Clamp(weaknessCount, 0, 4);

            float damageMultiplier = 1f + weaknessCount * 0.25f;
            int finalDamage = (int)(Projectile.damage * damageMultiplier);

            for (int i = 0; i < dotCount; i++)
            {
                target.SimpleStrikeNPC(
                    finalDamage,
                    hit.HitDirection,
                    crit: false,
                    knockBack: 0f,
                    damageType: Projectile.DamageType
                );
            }
            SpawnHitEffects(target.Center, dotCount > 0 || weaknessCount > 0);
        }
        private void SpawnHitEffects(Vector2 center, bool empowered)
        {
            for (int i = 0; i < 10; i++)
            {
                Dust dust = Dust.NewDustDirect(
                    center,
                    0, 0,
                    DustID.PurpleTorch,
                    Scale: Main.rand.NextFloat(0.6f, 0.9f)
                );

                dust.noGravity = true;
                dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
            }

            if (empowered)
            {
                for (int i = 0; i < 8; i++)
                {
                    Dust dust = Dust.NewDustDirect(
                        center,
                        0, 0,
                        DustID.PinkTorch,
                        Scale: Main.rand.NextFloat(0.7f, 1.1f)
                    );

                    dust.noGravity = true;
                    dust.velocity = Main.rand.NextVector2Circular(3f, 3f);
                }
            }
        }
        public override void AI()
        {
            FadeInAndOut();
            Visual();
            if (++Projectile.frameCounter >= 6)
            {
                Projectile.frameCounter = 0;
                if (++Projectile.frame >= Main.projFrames[Projectile.type])
                    Projectile.frame = 0;
            }

            if (Projectile.ai[0] >= 20f)
                Projectile.Kill();

            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();
        }
        public void FadeInAndOut()
        {
            // If last less than 50 ticks — fade in, than more — fade out
            if (Projectile.ai[0] <= 50f)
            {
                // Fade in
                Projectile.alpha -= 25;
                // Cap alpha before timer reaches 50 ticks
                if (Projectile.alpha < 100)
                    Projectile.alpha = 100;

                return;
            }
        }
        public void Visual()
        {
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.PurpleTorch, Scale: 0.3f);
                dust.noGravity = true;

                Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
                dust.velocity = vector;
                dust.position = Projectile.Center - (vector * 24f);
            }
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.CursedTorch, Scale: 0.45f);
                dust.noGravity = true;

                Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1.0f, 2.0f);
                dust.velocity = vector;
                dust.position = Projectile.Center - (vector * 24f);
            }
        }
    }
}
