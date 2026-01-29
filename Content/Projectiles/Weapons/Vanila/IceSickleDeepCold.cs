using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Projectiles.Weapons.Vanila
{
    public class IceSickleDeepCold : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 80;
            Projectile.height = 80;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.scale = 1.5f;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.ownerHitCheck = true; 
            Projectile.timeLeft = 2;
            Projectile.netImportant = true;
        }

        public override bool? CanHitNPC(NPC target)
        {
            return null;
        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float bladeLength = 32f;
            float bladeThickness = 24f;

            Vector2 center = Projectile.Center;
            Vector2 bladeDir = Projectile.rotation.ToRotationVector2();

            Vector2 bladeOffset = bladeDir * 40f;

            // Первый лезвие (вперёд)
            Vector2 blade1Start = center + bladeOffset - bladeDir * (bladeLength / 2);
            Vector2 blade1End = center + bladeOffset + bladeDir * (bladeLength / 2);

            // Второй лезвие (назад)
            Vector2 blade2Start = center - bladeOffset - bladeDir * (bladeLength / 2);
            Vector2 blade2End = center - bladeOffset + bladeDir * (bladeLength / 2);

            float collisionPoint = default;

            bool hitBlade1 = Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(), targetHitbox.Size(),
                blade1Start, blade1End, bladeThickness, ref collisionPoint
            );

            bool hitBlade2 = Collision.CheckAABBvLineCollision(
                targetHitbox.TopLeft(), targetHitbox.Size(),
                blade2Start, blade2End, bladeThickness, ref collisionPoint
            );

            return hitBlade1 || hitBlade2;
        }

        private void Visual()
        {
            Vector2 center = Projectile.Center;
            Vector2 bladeDir = Projectile.rotation.ToRotationVector2();
            Vector2 bladeOffset = bladeDir * 40f;

            float bladeLength = 32f; 
            Vector2 blade1End = center + bladeOffset + bladeDir * (bladeLength / 2);

            Vector2 blade2End = center - bladeOffset + bladeDir * (bladeLength / 2);

            // Создаем пыль на обоих концах
            for (int i = 0; i < 2; i++)
            {
                Vector2 pos = (i == 0) ? blade1End : blade2End;
                Dust dust = Dust.NewDustPerfect(pos, DustID.IceTorch);
                dust.noGravity = true;
                dust.scale = 1.2f;
                dust.velocity = Vector2.Zero;
            }
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];

            // 1. Проверка на смерть/отключение канала
            if (!player.channel || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            // 2. Проверка, что игрок всё ещё держит Ice Sickle
            Item heldItem = player.HeldItem;
            if  (heldItem.type != ItemID.IceSickle) // vanilla тоже поддерживаем
            {
                Projectile.Kill();
                return;
            }

            // Центр на игроке
            Projectile.Center = player.Center;

            // Инициализация ai[1]
            if (Projectile.ai[1] == 0f)
                Projectile.ai[1] = 1f;

            // Вращение
            float rotationSpeed = 0.3f;
            Projectile.ai[0] += rotationSpeed * player.direction;
            Projectile.rotation = Projectile.ai[0];

            Projectile.direction = player.direction;
            Projectile.spriteDirection = 1;
            Projectile.timeLeft = 2;
            Visual();

            if (Main.netMode != NetmodeID.SinglePlayer)
                Projectile.netUpdate = true;
        }
        public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
        {
            overPlayers.Add(index);
        }
        public override bool PreDraw(ref Color lightColor)
        {
            SpriteEffects effects = SpriteEffects.None;
            float scale = Projectile.scale;
            Texture2D texture = Terraria.GameContent.TextureAssets.Projectile[Projectile.type].Value;
            Vector2 origin = new Vector2(texture.Width / 2f, texture.Height / 2f);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, lightColor, Projectile.rotation, origin, scale, effects, 0f);

            return false; 
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(Projectile.ai[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            Projectile.ai[0] = reader.ReadSingle();
        }
    }
}
/*
private void DrawLine(Vector2 start, Vector2 end, Color color)
{
Texture2D tex = TextureAssets.MagicPixel.Value;
Vector2 edge = end - start;
float rotation = edge.ToRotation();
float length = edge.Length();
Main.spriteBatch.Draw(tex, start - Main.screenPosition, null, color,
rotation, Vector2.Zero, new Vector2(length, 2f), SpriteEffects.None, 0f);
}

Color debugColor = Color.Red * 0.7f;
DrawLine(blade1Start, blade1End, debugColor);
DrawLine(blade2Start, blade2End, debugColor);

            // Debug: рисуем зоны урона
            float bladeReach = 40f;
            Vector2 center = Projectile.Center;
            Vector2 dir = Projectile.rotation.ToRotationVector2();

            Vector2 blade1Start = center;
            Vector2 blade1End = center + dir * bladeReach;

            Vector2 blade2Start = center;
            Vector2 blade2End = center - dir * bladeReach;

*/