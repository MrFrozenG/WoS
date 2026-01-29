using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;
using WoS.Content.Items.Weapons.Melee.Sword;
using static WoS.WoS;

namespace WoS.Content.Projectiles.Weapons.Melee.Swords
{
    public class TwilightPhantomBlade : ModProjectile
    {
        private const float Radius = 55f;
        private int bladeIndex => (int)Projectile.ai[2];
        private int bladesCount => (int)Projectile.ai[0]; 
        private int supportPoints => (int)Projectile.ai[1]; 
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4; // длина шлейфа
            ProjectileID.Sets.TrailingMode[Projectile.type] = 2;     // как у мечей
        }
        public override void SetDefaults()
        {
            Projectile.width = 54;
            Projectile.height = 54;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = -1;
            Projectile.alpha = 0;
            Projectile.netImportant = true; 
            Projectile.localNPCHitCooldown = 10;
            Projectile.usesLocalNPCImmunity = true;
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // Существуют только пока зажат channel и предмет в руках
            if (!player.channel || player.HeldItem.type != ModContent.ItemType<TwilightPhantom>())
            {
                Projectile.alpha += 15;
                if (Projectile.alpha >= 255)
                    Projectile.Kill();
                return;
            }

            if (bladesCount <= 0)
                return;

            // 🔹 Точка основы — центр игрока
            Vector2 center = player.Center;

            // Скорость вращения от attack speed
            float attackSpeed = player.GetTotalAttackSpeed(Projectile.DamageType);
            float rotationSpeed = 0.05f * attackSpeed;

            // Распределение по окружности
            float angleStep = MathHelper.TwoPi / bladesCount;
            float angle =
                angleStep * bladeIndex +
                Main.GameUpdateCount * rotationSpeed;

            float pulseOffset = 0f;

            // Пульсация только если мечей больше 4
            if (bladesCount > 4)
            {
                float pulseSpeed = 0.06f * attackSpeed;     // скорость волны
                float pulseAmplitude = 12f;                  // насколько выдвигаются
                float phaseOffset = bladeIndex * 0.5f;      // сдвиг фазы между мечами

                pulseOffset = (float)Math.Sin(
                    Main.GameUpdateCount * pulseSpeed + phaseOffset
                ) * pulseAmplitude;
            }

            // Итоговый радиус с пульсацией
            float finalRadius = Radius + pulseOffset;

            // Позиция
            Vector2 offset = angle.ToRotationVector2() * finalRadius;
            Projectile.Center = center + offset;

            // Лезвие смотрит строго по радиусу
            // Спрайт наклонён на 45°
            Projectile.rotation = angle + MathHelper.PiOver4;

            Projectile.spriteDirection = 1;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            Vector2 bladeCenter = Projectile.Center;
            Vector2 bladeDirection = Projectile.rotation.ToRotationVector2();
            float bladeLength = 32f;
            float bladeWidth = 10f;

            Vector2 bladeStart = bladeCenter - bladeDirection * bladeLength * 0.3f;
            Vector2 bladeEnd = bladeCenter + bladeDirection * bladeLength * 0.7f;

            float collisionPoint = 0f;

            if (Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(),
                targetHitbox.Size(),
                bladeStart,
                bladeEnd,
                bladeWidth,
                ref collisionPoint))
            {
                return true;
            }

            return false;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;

            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);

            for (int i = 0; i < Projectile.oldPos.Length; i++)
            {
                float progress = 1f - i / (float)Projectile.oldPos.Length;

                Color trailColor = lightColor * progress * 0.5f;

                Vector2 drawPos = Projectile.oldPos[i] + Projectile.Size / 2f - Main.screenPosition;
                float rotation = Projectile.oldRot[i];

                Main.EntitySpriteDraw(
                    texture,
                    drawPos,
                    null,
                    trailColor,
                    rotation,
                    origin,
                    Projectile.scale,
                    SpriteEffects.None,
                    0
                );
            }

            return true; // основной спрайт рисуется стандартно
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];

            // 🔹 Добавляем SP владельцу
            player.GetModPlayer<PlayerBuff>().TwilightPhantomSigilSP += supportPoints;

            // 🔹 Раздаём SP другим игрокам той же команды / свободной команде рядом
            for (int i = 0; i < Main.maxPlayers; i++)
            {
                Player other = Main.player[i];
                if (!other.active || other.whoAmI == player.whoAmI)
                    continue;

                if (PlayersInteractionsUtils.IsSameTeamOrFreeTeam(player, other) &&
                    PlayersInteractionsUtils.IsWithinRadius(player, other, Radius / 16f))
                {
                    // Локально увеличиваем SP
                    other.GetModPlayer<PlayerBuff>().TwilightPhantomSigilSP += supportPoints;

                    // 🔹 Отправляем пакет на клиента (если сервер)
                    if (Main.netMode == NetmodeID.Server)
                    {
                        NetworkUtils.SendSupportEffect(
                            toWho: other.whoAmI,
                            fromWho: player.whoAmI,
                            type: WoS.SupportEffectType.Custom,
                            playerID: other.whoAmI,
                            value: supportPoints
                        );
                    }
                }
            }
        }
    }
}
