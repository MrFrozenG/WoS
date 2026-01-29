using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Core.ModUtils
{
    public class ProjectileUtils
    {
        public static void AI_NoCeiling(Projectile proj)
        {
            Player player = Main.player[proj.owner];
            if (proj.position.Y <= player.position.Y)
                proj.tileCollide = false;
            else 
                proj.tileCollide = true;
        }

        public static bool AI_MinionCheckOwner(Projectile proj, int buffIndex, Player owner)
        {
            if (owner.dead || !owner.active)
            {
                owner.ClearBuff(buffIndex);
                proj.Kill();
                return false;
            }

            if (owner.HasBuff(buffIndex))
            {
                proj.timeLeft = 2;
            }

            return true;
        }
        public static NPC AI_MinionGetTarget(Projectile proj, Player owner, float maxDetectRadius)
        {
            int forcedTarget = owner.MinionAttackTargetNPC;
            if (forcedTarget >= 0)
            {
                NPC npc = Main.npc[forcedTarget];
                if (npc.active && npc.CanBeChasedBy(proj))
                {
                    float dist = Vector2.Distance(proj.Center, npc.Center);
                    if (dist <= maxDetectRadius)
                        return npc;
                }
            }

            // 2. Поиск ближайшей валидной цели (учитывает whip mark)
            NPC closestNPC = null;
            float closestDistance = maxDetectRadius;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];

                if (!npc.CanBeChasedBy(proj))
                    continue;

                float distance = Vector2.Distance(proj.Center, npc.Center);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestNPC = npc;
                }
            }

            return closestNPC;
        }
        public static void AI_MinionContactDamage_FloatingSimple(Projectile proj, NPC target)
        {
            if (target == null)
            {
                return;
            }
            float speed = 9f;
            float inertia = 16f;

            Vector2 dir = target.Center - proj.Center;
            float distToTarget = dir.Length();

            if (distToTarget > 24f)
            {
                dir.Normalize();
                dir *= speed;

                proj.velocity =
                    (proj.velocity * (inertia - 1) + dir) / inertia;
            }
            else
            {
                proj.velocity *= 0.85f;
            }

        }
        public static void AI_HomingAtHighHP(Projectile proj)
        {
            float maxDetect = 600f;       // радиус поиска цели
            float homingStrength = 16f;   // “резкость” поворота

            NPC target = null;
            float highestLife = -1f;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (npc.CanBeChasedBy(proj) && !npc.friendly)
                {
                    float distance = Vector2.Distance(proj.Center, npc.Center);

                    if (distance < maxDetect && npc.lifeMax > highestLife)
                    {
                        highestLife = npc.lifeMax;
                        target = npc;
                    }
                }
            }

            if (target != null)
            {
                Vector2 direction = target.Center - proj.Center;
                direction.Normalize();
                direction *= 12f; // скорость полёта

                proj.velocity =
                    (proj.velocity * (homingStrength - 1) + direction) / homingStrength;
            }
        }
        public static void AI_MeteorVertical(Projectile proj, int dustType = 6, float fallSpeed = 16f, float dustSpread = 1.2f)
        {
            // Если угол не задан - инициализируем его (ai[0] == 0 по умолчанию)
            if (proj.ai[0] == 0f && proj.localAI[0] == 0f)
            {
                // Рандомный угол в пределах [-10°, +10°] от вертикали
                float randomAngle = MathHelper.ToRadians(Main.rand.NextFloat(-10f, 10f));
                proj.ai[0] = randomAngle;

                // Чтобы не пересоздавался угол каждый тик
                proj.localAI[0] = 1f;
            }

            // Берём угол из ai[0]
            float angle = proj.ai[0];
            Vector2 baseVel = new Vector2(0, 1f).RotatedBy(angle) * fallSpeed;
            proj.velocity = baseVel;

            // Поворот для визуала
            proj.rotation = proj.velocity.ToRotation();

            // Конусный хвост из пыли
            for (int i = 0; i < 3; i++)
            {
                float progress = i / 3f;
                Vector2 offset = new Vector2(
                    Main.rand.NextFloat(-dustSpread, dustSpread) * (1f - progress),
                    Main.rand.NextFloat(-dustSpread, dustSpread) * (1f - progress)
                );

                int d = Dust.NewDust(proj.Center - proj.velocity * progress, 0, 0, dustType);
                Main.dust[d].position += offset * 10f * progress;
                Main.dust[d].scale = 1.2f - progress * 0.7f;
                Main.dust[d].velocity *= 0.2f;
                Main.dust[d].noGravity = true;
            }
        }
        public static NPC GetHomingTarget_Simple(Vector2 position, float radiusSimple, float radiusTiled)
        {
            float maxDetectRadius = radiusSimple + (radiusTiled * 16f);

            NPC closestNPC = null;
            float closestDistance = maxDetectRadius;

            foreach (NPC npc in Main.npc)
            {
                if (npc.CanBeChasedBy() && npc.active && !npc.friendly && !npc.immortal)
                {
                    float dist = Vector2.Distance(position, npc.Center);

                    if (dist < closestDistance)
                    {
                        closestDistance = dist;
                        closestNPC = npc;
                    }
                }
            }

            return closestNPC;
        }
        public static void HomingAtClosestNPC_Simple(Projectile proj, float speed, int delay, float radiusSimple, float radiusTiled)
        {
            if (proj.ai[1] < delay)
            {
                proj.ai[1]++;
                return;
            }

            NPC target = GetHomingTarget_Simple(proj.Center, radiusSimple, radiusTiled);
            if (target == null)
                return;

            Vector2 direction = target.Center - proj.Center;
            direction.Normalize();
            proj.velocity = (proj.velocity * 10f + direction * speed) / 11f;
        }
        public static bool ProjectileTrailVisualPreDraw(Projectile projectile, ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[projectile.type].Value;
            Vector2 origin = texture.Size() / 2f;
            for (int i = 0; i < projectile.oldPos.Length; i++)
            {
                Vector2 drawPos = projectile.oldPos[i] + projectile.Size / 2f - Main.screenPosition;
                float opacity = (projectile.oldPos.Length - i) / (float)projectile.oldPos.Length;

                Main.spriteBatch.Draw(
                    texture,
                    drawPos,
                    null,
                    projectile.GetAlpha(lightColor) * opacity * 0.5f,
                    projectile.rotation,
                    origin,
                    projectile.scale,
                    SpriteEffects.None,
                    0f
                );
            }
            return true;
        }
        public static void DualBladesPattern_Split(Projectile originalProjectile, int firstBladeType, int secondBladeType, float damagePercentFirst,
                float damagePercentSecond, float splitAngleDegrees = 25f, float bladeSpeed = 8f)
        {
            Player player = Main.player[originalProjectile.owner];
            if (Main.myPlayer != originalProjectile.owner)
                return;

            Vector2 baseDirection = originalProjectile.velocity.SafeNormalize(Vector2.UnitX);

            Vector2 leftDirection = baseDirection.RotatedBy(MathHelper.ToRadians(-splitAngleDegrees)) * bladeSpeed;
            Vector2 rightDirection = baseDirection.RotatedBy(MathHelper.ToRadians(splitAngleDegrees)) * bladeSpeed;

            int damageFirst = (int)(originalProjectile.damage * damagePercentFirst);
            int damageSecond = (int)(originalProjectile.damage * damagePercentSecond);

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                leftDirection,
                firstBladeType,
                damageFirst,
                originalProjectile.knockBack,
                originalProjectile.owner
            );

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                rightDirection,
                secondBladeType,
                damageSecond,
                originalProjectile.knockBack,
                originalProjectile.owner
            );
            originalProjectile.Kill();
        }

        public static void DualBladesPattern_SplitWithoutKill(Projectile originalProjectile, int firstBladeType, int secondBladeType, float damagePercentFirst,
                float damagePercentSecond, float splitAngleDegrees = 25f, float bladeSpeed = 8f)
        {
            Player player = Main.player[originalProjectile.owner];
            if (Main.myPlayer != originalProjectile.owner)
                return;

            Vector2 baseDirection = originalProjectile.velocity.SafeNormalize(Vector2.UnitX);

            Vector2 leftDirection = baseDirection.RotatedBy(MathHelper.ToRadians(-splitAngleDegrees)) * bladeSpeed;
            Vector2 rightDirection = baseDirection.RotatedBy(MathHelper.ToRadians(splitAngleDegrees)) * bladeSpeed;

            int damageFirst = (int)(originalProjectile.damage * damagePercentFirst);
            int damageSecond = (int)(originalProjectile.damage * damagePercentSecond);

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                leftDirection,
                firstBladeType,
                damageFirst,
                originalProjectile.knockBack,
                originalProjectile.owner
            );

            Projectile.NewProjectileDirect(
                originalProjectile.GetSource_FromThis(),
                originalProjectile.Center,
                rightDirection,
                secondBladeType,
                damageSecond,
                originalProjectile.knockBack,
                originalProjectile.owner
            );
        }
        public static void ApplyWindDrift(Projectile proj, float driftFactor = 0.05f)
        {
            // читаем текущую скорость ветра: положительная — вправо, отрицательная — влево
            float wind = Main.windSpeedCurrent;

            // добавляем к горизонтальной скорости небольшую фракцию ветра
            proj.velocity.X += wind * driftFactor;
        }
        public static void StartFadeDeath(Projectile proj, int durationTicks = 30)
        {
            // сохраняем общую длительность
            proj.localAI[0] = durationTicks;
            // текущий «обратный» таймер
            proj.localAI[1] = durationTicks;

            // выключаем логику столкновений/урона
            proj.friendly = false;
            proj.hostile = false;
            proj.tileCollide = false;
            proj.penetrate = -1;
        }
        public static bool HandleFadeDeath(Projectile proj, int smokeDustType = DustID.Smoke, int dustCount = 10)
        {
            // если ещё не стартовали – сразу выходим
            if (proj.localAI[1] <= 0 || proj.localAI[0] <= 0)
                return false;

            // уменьшаем таймер
            proj.localAI[1]--;
            // нормируем прогресс (от 1.0 до 0.0)
            float progress = proj.localAI[1] / proj.localAI[0];
            // плавно уменьшаем размер
            proj.scale = progress;
            // останавливаемся на месте
            proj.velocity = Vector2.Zero;

            // если таймер истёк – порождаем дым и убиваем
            if (proj.localAI[1] <= 0)
            {
                for (int i = 0; i < dustCount; i++)
                {
                    var d = Dust.NewDustDirect(proj.position, proj.width, proj.height, smokeDustType);
                    d.velocity *= 1.2f;
                    d.noGravity = true;
                }
                proj.Kill();
            }

            // сообщаем, что остальной AI не должен выполняться
            return true;
        }
        public static void SpearBasePreAI(Projectile Projectile, float HoldoutRangeMin, float HoldoutRangeMax)
        {
            Player player = Main.player[Projectile.owner];
            int duration = player.itemAnimationMax;
            player.heldProj = Projectile.whoAmI;

            if (Projectile.timeLeft > duration)
            {
                Projectile.timeLeft = duration;
            }

            Projectile.velocity = Vector2.Normalize(Projectile.velocity);

            float halfDuration = duration * 0.5f;
            float progress;

            if (Projectile.timeLeft < halfDuration)
            {
                progress = Projectile.timeLeft / halfDuration;
            }
            else
            {
                progress = (duration - Projectile.timeLeft) / halfDuration;
            }

            Projectile.Center = player.MountedCenter + Vector2.SmoothStep(Projectile.velocity * HoldoutRangeMin, Projectile.velocity * HoldoutRangeMax, progress);
            if (Projectile.spriteDirection == -1)
            {
                Projectile.rotation += MathHelper.ToRadians(45f);
            }
            else
            {
                Projectile.rotation += MathHelper.ToRadians(135f);
            }
        }
    }
}
