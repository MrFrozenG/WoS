using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;
using WoS.Content.Projectiles.Ammo.Rockets;

namespace WoS.Content.Projectiles.Weapons.Melee.Scythe
{
    public class CriticalExceptionSwing : ModProjectile
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
            Projectile.scale = 1.25f;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Melee/Scythe/CriticalExceptionSwing_Glow").Value;
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

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            if (rocketCooldown != 0)
            {
                rocketCooldown--;
            }
            if (rocketCooldown <= 0)
            {
                shootRockets(player, Projectile.damage, Projectile.knockBack);
            }
            if (!player.channel || player.dead || !player.active)
            {
                Projectile.Kill();
                return;
            }

            Projectile.Center = player.Center;

            if (Projectile.ai[1] == 0f)
                Projectile.ai[1] = 1f;

            float rotationSpeed = 0.3f;
            Projectile.ai[0] += rotationSpeed * player.direction;

            Projectile.rotation = Projectile.ai[0];

            Projectile.direction = player.direction;
            Projectile.spriteDirection = 1;
            Projectile.timeLeft = 2;

            if (Main.netMode != NetmodeID.SinglePlayer)
                Projectile.netUpdate = true;
        }
        private int rocketCooldown = 0;
        private void shootRockets(Player player, int damage, float knockback)
        {
            float detectRange = 16 * 48;
            int maxRockets = 5;   // Лимит
            int launched = 0;      // Сколько уже выпустили

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                if (launched >= maxRockets)
                    break; 

                NPC npc = Main.npc[i];

                if (npc.active && !npc.friendly && npc.CanBeChasedBy()
                    && Vector2.Distance(player.Center, npc.Center) <= detectRange)
                {
                    // направление в сторону цели
                    Vector2 direction = npc.Center - player.Center;
                    direction.Normalize();
                    direction *= 8f;

                    // создаём ракету BreakpointRocketShot
                    Projectile rocket = Projectile.NewProjectileDirect(
                        Projectile.GetSource_FromThis(),
                        player.Center,
                        direction,
                        ModContent.ProjectileType<BreakpointRocketShot>(),
                        damage * 2,
                        knockback,
                        player.whoAmI
                    );

                    rocket.ai[1] = 1f;
                    rocket.DamageType = DamageClass.Melee;

                    launched++; // ← увеличиваем счётчик
                }
            }

            rocketCooldown = 120;
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
