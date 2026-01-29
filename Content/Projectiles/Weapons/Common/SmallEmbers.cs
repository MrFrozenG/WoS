using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Common
{
    public class SmallEmbers : ModProjectile
    {
        int timer;
        public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
        }
        int CurrentFrame;

        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 960;
            Projectile.tileCollide = true; // Столкновение с блоками
            Projectile.ignoreWater = true;
            Projectile.scale = 0.75f;
            Projectile.aiStyle = 14;
            AIType = ProjectileID.SpikyBall;
            CurrentFrame = Main.rand.Next(0, 2);
            Projectile.usesIDStaticNPCImmunity = true;
            Projectile.idStaticNPCHitCooldown = 30;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return false;
        }
        public override void AI()
        {
            Visuals(); // Визуальные эффекты
        }

        // Метод для визуальных эффектов
        public void Visuals()
        {
            Projectile.frame = CurrentFrame;
            timer++;
            if (timer % 15 == 0) 
            {
                int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
                Main.dust[dust].noGravity = false;
                Main.dust[dust].scale = 1.5f;
                Main.dust[dust].noLight = true;
                int dustA = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke);
                Main.dust[dustA].noGravity = false;
                Main.dust[dustA].scale = 1f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.OnFire, damageDone * 2);
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.OnFire, info.Damage * 2);
        }
    }
}
