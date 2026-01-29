using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.GameContent;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using Terraria.Audio;

namespace WoS.Content.Projectiles.Weapons.Summon.Whips
{
    public class WishDragonWhip : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.IsAWhip[Type] = true;
        }
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.ownerHitCheck = true;
            Projectile.extraUpdates = 1;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = -1;
            Projectile.DamageType = DamageClass.SummonMeleeSpeed;
            Projectile.WhipSettings.Segments = 16;
            Projectile.WhipSettings.RangeMultiplier = 1.75f;
        }
        private float Timer
        {
            get => Projectile.ai[0];
            set => Projectile.ai[0] = value;
        }
        public override void AI()
        {
            Player owner = Main.player[Projectile.owner];

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
            Projectile.Center = Main.GetPlayerArmPosition(Projectile) + Projectile.velocity * Timer;
            Projectile.spriteDirection = Projectile.velocity.X >= 0f ? 1 : -1;

            Timer++;

            float swingTime = owner.itemAnimationMax * Projectile.MaxUpdates;
            if (Timer >= swingTime || owner.itemAnimation <= 0)
            {
                Projectile.Kill();
                return;
            }

            owner.heldProj = Projectile.whoAmI;

            // crack sound
            if (Timer == swingTime / 2)
            {
                List<Vector2> points = Projectile.WhipPointsForCollision;
                Projectile.FillWhipControlPoints(Projectile, points);
                SoundEngine.PlaySound(SoundID.Item153, points[^1]);
            }

            // ДЕКОР-ЭФФЕКТЫ (можно удалить)
            float swingProgress = Timer / swingTime;
            if (Utils.GetLerpValue(0.1f, 0.7f, swingProgress, true) *
                Utils.GetLerpValue(0.9f, 0.7f, swingProgress, true) > 0.5f &&
                !Main.rand.NextBool(3))
            {
                List<Vector2> points = Projectile.WhipPointsForCollision;
                points.Clear();
                Projectile.FillWhipControlPoints(Projectile, points);

                int pointIndex = Main.rand.Next(points.Count - 10, points.Count);
                Rectangle spawnArea = Utils.CenteredRectangle(points[pointIndex], new Vector2(30f, 30f));

                int dustType = Main.rand.NextBool(3) ? DustID.TintableDustLighted : DustID.Enchanted_Gold;

                Dust dust = Dust.NewDustDirect(spawnArea.TopLeft(), spawnArea.Width, spawnArea.Height,
                    dustType, 0f, 0f, 100, Color.White);

                dust.position = points[pointIndex];
                dust.fadeIn = 0.3f;
                dust.noGravity = true;
                dust.velocity *= 0.5f;

                Vector2 spinning = points[pointIndex] - points[pointIndex - 1];
                dust.velocity += spinning.RotatedBy(owner.direction * (float)Math.PI / 2f);
                dust.velocity *= 0.5f;
            }
        }
        private void DrawLine(List<Vector2> list)
        {
            Texture2D texture = TextureAssets.FishingLine.Value;
            Rectangle frame = texture.Frame();
            Vector2 origin = new Vector2(frame.Width / 2, 2);

            Vector2 pos = list[0];

            int count = list.Count - 1;

            for (int i = 0; i < count; i++)
            {
                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;

                // t = от 0 (начало) до 1 (конец)
                float t = i / (float)(count - 1);

                // Градиент: фиолетовый → синий
                Color start = new Color(180, 0, 255); // фиолетовый
                Color end = new Color(0, 120, 255); // синий
                Color color = Color.Lerp(start, end, t);

                Color light = Lighting.GetColor(element.ToTileCoordinates());
                color = new Color(
                    (int)(color.R * (light.R / 255f)),
                    (int)(color.G * (light.G / 255f)),
                    (int)(color.B * (light.B / 255f)),
                    color.A
                );

                Vector2 scale = new Vector2(1f, (diff.Length() + 1) / frame.Height);

                Main.EntitySpriteDraw(texture, pos - Main.screenPosition, frame, color, rotation, origin, scale, SpriteEffects.None, 0);

                pos += diff;
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            //target.AddBuff(ModContent.BuffType<ExampleWhipDebuff>(), 240);
            Main.player[Projectile.owner].MinionAttackTargetNPC = target.whoAmI;
            Projectile.damage = (int)(Projectile.damage * 0.5f);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            List<Vector2> list = new List<Vector2>();
            Projectile.FillWhipControlPoints(Projectile, list);

            DrawLine(list);

            Texture2D texture = TextureAssets.Projectile[Type].Value;
            SpriteEffects flip = Projectile.spriteDirection < 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;

            Vector2 pos = list[0];
            int frameHeight = 28; // высота каждого кадра

            // Определяем последовательность сегментов (без рукоятки и кончика)
            int handleFrame = 0; // рукоятка
            int tipFrame = 4;    // кончик
            int[] middleFrames = { 1, 2, 3, 2 }; // повторяющийся паттерн для центральных сегментов

            for (int i = 0; i < list.Count - 1; i++)
            {
                int segmentIndex;

                if (i == 0)
                {
                    segmentIndex = handleFrame; // рукоятка
                }
                else if (i == list.Count - 2)
                {
                    segmentIndex = tipFrame; // кончик
                }
                else
                {
                    // вычисляем индекс паттерна для текущего сегмента
                    int patternIndex = (i - 1) % middleFrames.Length;
                    segmentIndex = middleFrames[patternIndex];
                }

                Rectangle frame = new Rectangle(0, frameHeight * segmentIndex, 26, frameHeight);
                Vector2 origin = new Vector2(13, 14);
                float scale = 1f;

                Vector2 element = list[i];
                Vector2 diff = list[i + 1] - element;

                float rotation = diff.ToRotation() - MathHelper.PiOver2;
                Color color = Lighting.GetColor(element.ToTileCoordinates());

                Main.EntitySpriteDraw(
                    texture,
                    pos - Main.screenPosition,
                    frame,
                    color,
                    rotation,
                    origin,
                    scale,
                    flip,
                    0
                );

                pos += diff;
            }

            return false;
        }
    }
}
