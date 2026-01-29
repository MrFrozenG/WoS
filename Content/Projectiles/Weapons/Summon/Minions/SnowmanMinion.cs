using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using WoS.Content.Buffs.Minion;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Summon.Minions
{
    public class SnowmanMinion : ModProjectile
    {
        private NPC target = null;
        private float idleDistanceX;
        private int type = 0; 
        private int frameCounter = 0;
        private const float ApproachRange = 380f; // если дальше — подходим
        private const float MaxTargetDistance = 600f;
        private const float ThrowRange = 420f;
        private const float MaxSpeed = 5f;
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 20;
        }
        public override void SetDefaults()
        {
            Projectile.width = 52;
            Projectile.height = 62;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 2;
            Projectile.penetrate = -1;

            // случайный тип снеговика
            type = Main.rand.Next(4);
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (!ProjectileUtils.AI_MinionCheckOwner(
                Projectile,
                ModContent.BuffType<SnowmanMinionBuff>(),
                player))
            {
                return;
            }

            FindTarget();
            HandleRangedAttack();
            HandleTeleport(player);
            HandleMovement(player);
            HandleJump();
            ApplyGravity();
            HandleStepUp();
            AnimationAI();
        }
        private void AnimationAI()
        {
            if (Projectile.velocity.X == 0 && Projectile.velocity.Y == 0)
            {
                Projectile.frame = type * 5; // 1-й кадр типа = простой стоячий
            }
            else
            {
                frameCounter++;
                if (frameCounter > 5)
                {
                    Projectile.frame++;
                    frameCounter = 0;
                    if (Projectile.frame > type * 5 + 4)
                        Projectile.frame = type * 5 + 1;
                }
            }
            Projectile.spriteDirection = -Projectile.direction;
        }
        private void FindTarget()
        {
            target = null;
            float maxDist = MaxTargetDistance;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || !npc.CanBeChasedBy(Projectile))
                    continue;

                float dist = Vector2.Distance(Projectile.Center, npc.Center);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    target = npc;
                }
            }
        }
        private void HandleRangedAttack()
        {
            if (target == null)
                return;

            float dist = Vector2.Distance(Projectile.Center, target.Center);

            if (shootCooldown > 0)
                shootCooldown--;

            if (dist <= ThrowRange &&
                shootCooldown <= 0 &&
                Projectile.velocity.Y == 0f &&
                Collision.CanHitLine(
                    Projectile.Center, 1, 1,
                    target.Center, 1, 1))
            {
                Vector2 dir = target.Center - Projectile.Center;
                dir.Normalize();
                dir *= 8f;

                if (Projectile.owner == Main.myPlayer)
                {
                    Projectile proj = Projectile.NewProjectileDirect(
                        Projectile.GetSource_FromThis(),
                        Projectile.Center + new Vector2(Projectile.direction * 8f, -8f),
                        dir,
                        ProjectileID.SnowBallFriendly,
                        Projectile.damage,
                        Projectile.knockBack,
                        Projectile.owner
                    );
                    proj.DamageType = DamageClass.Summon;
                }

                shootCooldown = 45;
                Projectile.velocity.X *= 0.7f;
            }
        }
        private void HandleTeleport(Player player)
        {
            if (Vector2.Distance(player.Center, Projectile.Center) > 2000f)
                Projectile.Center = player.Center;
        }
        private void HandleMovement(Player player)
        {
            Vector2 targetPos;

            if (target != null)
            {
                float dist = Vector2.Distance(Projectile.Center, target.Center);

                // ⚠️ ВАЖНО:
                // если ВРАГ ДАЛЕКО — подходим
                // если в радиусе метания — НЕ ПРЁМ В УПОР
                if (dist > ApproachRange)
                {
                    targetPos = target.Center;
                }
                else
                {
                    Projectile.velocity.X *= 0.85f;
                    return;
                }
            }
            else
            {
                targetPos = player.Center +
                    new Vector2(-player.direction * (40 + Projectile.minionPos * 40), 0);
            }

            Vector2 move = targetPos - Projectile.Center;
            int dir = Math.Sign(move.X);

            if (Math.Abs(move.X) > 6f)
                Projectile.velocity.X = MathHelper.Lerp(
                    Projectile.velocity.X,
                    MaxSpeed * dir,
                    0.15f);
            else
                Projectile.velocity.X *= 0.8f;
        }
        private void HandleJump()
        {
            if (Projectile.velocity.Y != 0f || Math.Abs(Projectile.velocity.X) <= 0.1f)
                return;

            int dir = Math.Sign(Projectile.velocity.X);
            int xCheck = (int)(Projectile.Center.X / 16f) + dir;
            int yTop = (int)(Projectile.Bottom.Y / 16f);
            int yBottom = yTop + 1;

            bool wallAhead = false;

            for (int y = yTop; y <= yBottom; y++)
            {
                if (WorldGen.InWorld(xCheck, y) && WorldGen.SolidTile(xCheck, y))
                {
                    wallAhead = true;
                    break;
                }
            }

            if (!wallAhead)
                return;

            int headX = (int)(Projectile.Center.X / 16f) + dir;
            int headY = (int)(Projectile.Top.Y / 16f);

            for (int y = headY; y > headY - 3; y--)
            {
                if (WorldGen.InWorld(headX, y) && WorldGen.SolidTile(headX, y))
                    return;
            }

            float jumpStrength = MathHelper.Lerp(
                5f, 7.5f,
                Math.Abs(Projectile.velocity.X) / MaxSpeed);

            Projectile.velocity.Y = -jumpStrength;
            Projectile.velocity.X *= 0.8f;
        }
        private void ApplyGravity()
        {
            Projectile.velocity.Y += 0.4f;
            if (Projectile.velocity.Y > 10f)
                Projectile.velocity.Y = 10f;
        }
        private void HandleStepUp()
        {
            Collision.StepUp(
                ref Projectile.position,
                ref Projectile.velocity,
                Projectile.width,
                Projectile.height,
                ref Projectile.stepSpeed,
                ref Projectile.gfxOffY
            );
        }
        private int shootCooldown;
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
    }
}
