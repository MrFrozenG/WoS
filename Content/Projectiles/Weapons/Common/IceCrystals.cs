using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Common
{
    public class IceCrystals : ModProjectile
    {
        public override string Texture => $"Terraria/Images/Projectile_{ProjectileID.Blizzard}";
        public override void SetDefaults()
        {
            Projectile.width = 7;
            Projectile.height = 18;
            Projectile.DamageType = DamageClass.Generic;
            Projectile.penetrate = 1;
            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 300;
            Projectile.friendly = true;
        }
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 5;
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextFloat() <= 0.2f) // 20% шанс
            {
                target.AddBuff(BuffID.Frostburn, 180); // 3 секунды, 60 тиков = 1 секунда
            }
        }
        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            if (Main.rand.NextFloat() <= 0.2f)
            {
                target.AddBuff(BuffID.Frostburn, 180);
            }
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            width = 6;
            height = 6;
            return true;
        }

        // Разлетается при столкновении с блоком
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            for (int i = 0; i < 5; i++)
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, DustID.IceTorch);
                dust.velocity *= 0.5f;
                dust.scale = 1.2f;
                dust.noGravity = true;
            }

            Projectile.Kill(); // Уничтожаем снаряд
            return false;
        }
        /// <summary>
        /// ai[0] = 1 >> Снаряд игнорирующий блоки, если находится выше точки игрока. 
        /// </summary>
        public override void AI()
        {
            // Поворот снаряда
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;

            // Шлейф частиц
            for (int i = 0; i < 2; i++)
            {
                Vector2 offset = -Projectile.velocity * 0.3f * i;
                Dust dust = Dust.NewDustDirect(Projectile.position + offset, Projectile.width, Projectile.height, DustID.IceTorch);
                dust.scale = 0.8f;
                dust.velocity *= 0.1f;
                dust.noGravity = true;
            }

            float angle = Projectile.velocity.ToRotation(); // -Pi .. Pi
            angle += MathHelper.Pi; // смещаем диапазон на 0..2Pi
            int frame = (int)(angle / (2 * MathHelper.Pi) * 5f); // делим на 5 кадров
            frame = frame % 5;

            Projectile.frame = frame;
            if (Projectile.ai[0] == 1)
            {
                ProjectileUtils.AI_NoCeiling(Projectile);
            }
        }
    }
}
