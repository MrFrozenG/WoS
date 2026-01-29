using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using System;
using WoS.Content.Core.ModUtils;
using WoS.Content.Buffs.Minion;

namespace WoS.Content.Projectiles.Weapons.Summon.Minions
{
    public class LittleGraniteElemental : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
            Main.projFrames[Projectile.type] = 11;
        }
        public override void SetDefaults()
        {
            Projectile.width = 32;
            Projectile.height = 32;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.minion = true;
            Projectile.minionSlots = 1f;
            Projectile.timeLeft = 2;
            Projectile.penetrate = -1;

            Projectile.usesLocalNPCImmunity = true;
        }
        public override bool MinionContactDamage()
        {
            return true;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            if (!ProjectileUtils.AI_MinionCheckOwner(
                Projectile,
                ModContent.BuffType<LittleGraniteElementalMinionBuff>(),
                owner))
            {
                return;
            }

            NPC target = ProjectileUtils.AI_MinionGetTarget(Projectile, owner, 880f);

            if (target != null)
            {
                ProjectileUtils.AI_MinionContactDamage_FloatingSimple(
                    Projectile,
                    target
                );
            }
            else
            {
                IdleBehaviour(owner);
            }
            Visuals();
        }
        private void Visuals()
        {
            // So it will lean slightly towards the direction it's moving
            Projectile.rotation = Projectile.velocity.X * 0.05f;

            // This is a simple "loop through all frames from top to bottom" animation
            int frameSpeed = 11;

            Projectile.frameCounter++;

            if (Projectile.frameCounter >= frameSpeed)
            {
                Projectile.frameCounter = 0;
                Projectile.frame++;

                if (Projectile.frame >= Main.projFrames[Type])
                {
                    Projectile.frame = 0;
                }
            }
            Lighting.AddLight(Projectile.Center, Color.Blue.ToVector3() * 0.78f);
            int DustCount = 0;
            if (DustCount <= 10)
            {
                DustCount++;
            }
            else
            {
                Dust.NewDust(Projectile.Center, Projectile.width / 2, Projectile.height / 2, DustID.BlueTorch, 1f, 1f, 0, default, 0.5f);
                DustCount = 0;
            }
        }
        private void IdleBehaviour(Player player)
        {
            // Направление "за спиной" игрока
            int dir = -player.direction;

            // Индекс миньона среди остальных
            int index = Projectile.minionPos;

            // Параметры формации
            float horizontalSpacing = 40f;
            float verticalSpacing = 32f;
            float backOffset = 64f;

            // Раскладка по сетке
            int row = index % 2;        // 0 / 1
            int column = index / 2;     // 0,1,2,3...

            bool offset = row == 1;

            // Базовая позиция — центр игрока
            Vector2 idlePosition = player.Center;

            // Смещение за спину
            idlePosition.X += dir * backOffset;

            // Горизонтальное распределение
            idlePosition.X += dir * column * horizontalSpacing;

            // Шахматное смещение
            if (offset)
                idlePosition.X += dir * (horizontalSpacing * 0.5f);

            // Вертикальный ряд
            idlePosition.Y += (row == 0 ? -verticalSpacing : verticalSpacing);

            // Плавное движение к позиции
            Vector2 toIdle = idlePosition - Projectile.Center;

            float inertia = 12f;
            Projectile.velocity = (Projectile.velocity * (inertia - 1) + toIdle * 0.2f) / inertia;

            // Поворот в сторону игрока (опционально, можно убрать)
            if (Projectile.velocity.X != 0)
                Projectile.spriteDirection = Projectile.velocity.X > 0 ? 1 : -1;
        }
    }
}
