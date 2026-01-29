using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Summon
{
    public class DirectiveDrone : ModProjectile
    {
        private int updateCount = 0; 
        public override void SetDefaults()
        {
            Projectile.width = Projectile.height = 34;
            Projectile.friendly = false;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Summon;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.light = 0.05f;
            Projectile.timeLeft = 1800;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Summon/DirectiveDrone_Glow").Value;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];
            if (!owner.active || owner.dead)
            {
                Projectile.Kill();
                return;
            }

            FollowOwner(owner);

            NPC target = FindHighestHPEnemy(750f);

            if (target != null)
            {
                // === есть цель ===
                if (owner.MinionAttackTargetNPC != target.whoAmI)
                {
                    owner.MinionAttackTargetNPC = target.whoAmI;

                    CreateMarkExplosion(target);
                    updateCount++;
                    if (updateCount >= 4)
                    {
                        ExplodeDrone();
                        return;
                    }
                }

                Vector2 toTarget = target.Center - Projectile.Center;
                Projectile.rotation = toTarget.ToRotation();
                Projectile.spriteDirection = Projectile.direction = (toTarget.X > 0 ? 1 : -1);
            }
            else
            {
                // === без цели смотрим туда, куда смотрит игрок ===
                Projectile.rotation = owner.direction;
                Projectile.direction = owner.direction;
            }
        }

        private void FollowOwner(Player owner)
        {
            Vector2 idlePosition = owner.Center + new Vector2(0, -40f);
            Vector2 toIdle = idlePosition - Projectile.Center;

            float speed = 12f;
            float inertia = 20f;

            if (toIdle.Length() > 40f)
                Projectile.velocity =
                    (Projectile.velocity * (inertia - 1) + toIdle.SafeNormalize(Vector2.Zero) * speed) / inertia;
            else
                Projectile.velocity *= 0.95f;
        }

        private void CreateMarkExplosion(NPC target)
        {
            Vector2 speed = Main.rand.NextVector2CircularEdge(2.5f, 2.5f);
            for (int i = 0; i < 22; i++)
            {
                Dust d = Dust.NewDustPerfect(target.Center, DustID.BlueCrystalShard, speed * 5, Scale: 2.5f);
                d.noGravity = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, target.Center);
        }

        private void ExplodeDrone()
        {
            Vector2 speed = Main.rand.NextVector2Unit();
            for (int i = 0; i < 18; i++)
            {
                Dust d = Dust.NewDustPerfect(
                    Projectile.Center,
                    DustID.Electric,
                    Main.rand.NextVector2Circular(3.2f, 3.2f),
                    150,
                    new Color(120, 255, 255),
                    1.6f
                );
                d.noGravity = true;
            }

            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
            Projectile.Kill();
        }

        private NPC FindHighestHPEnemy(float maxRange)
        {
            NPC best = null;
            int bestHP = 0;

            foreach (NPC npc in Main.npc)
            {
                if (!npc.active || !npc.CanBeChasedBy())
                    continue;

                float dist = Vector2.Distance(npc.Center, Projectile.Center);
                if (dist > maxRange)
                    continue;

                if (npc.lifeMax > bestHP)
                {
                    bestHP = npc.lifeMax;
                    best = npc;
                }
            }

            return best;
        }
    }
}
