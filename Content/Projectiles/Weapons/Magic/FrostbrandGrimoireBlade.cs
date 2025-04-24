using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace WoS.Content.Projectiles.Weapons.Magic;

public class FrostbrandGrimoireBlade : ModProjectile
{
    private int passThroughTime = 90;
    private int startDelay = 30;
    private Vector2 storedVelocity;

    public override void SetStaticDefaults()
    {
        Main.projFrames[Projectile.type] = 2;
    }
    public int TimeLeft = 320;
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = false;
        Projectile.tileCollide = true;
        Projectile.light = 0.3f;
        Projectile.penetrate = 3;
        Projectile.timeLeft = TimeLeft;
        Projectile.aiStyle = -1;
        Projectile.tileCollide = false;
    }
    public override void AI()
    {
        if (startDelay > 0)
        {
            startDelay--;

            if (startDelay == 0)
            {
                Projectile.velocity = storedVelocity;
            }
            else
            {
                return;
            }
        }

        if (passThroughTime > 0)
        {
            passThroughTime--;

            if (passThroughTime <= 0)
            {
                Projectile.tileCollide = true;
            }
        }

        for (int i = 0; i < 3; i++)
        {
            Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15));
            Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.IceTorch, 0f, 0f, 100, default, 0.5f);
        }

        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();

        if (Projectile.timeLeft < TimeLeft / 2)
        {
            Projectile.frame = 1;
        }
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(3)) target.AddBuff(BuffID.Frostburn, 120);
    }

    public override void OnSpawn(IEntitySource source)
    {
        storedVelocity = Projectile.velocity;
        Projectile.velocity = Vector2.Zero;

        // Устанавливаем правильное вращение сразу
        Projectile.rotation = storedVelocity.ToRotation();
        Projectile.direction = (storedVelocity.X > 0).ToDirectionInt();

        Vector2 center = Projectile.Center;
        for (int i = 0; i < 20; i++)
        {
            float angle = MathHelper.TwoPi * i / 20f;
            float distance = 60f;
            Vector2 spawnPos = center + angle.ToRotationVector2() * distance;
            Vector2 velocity = (center - spawnPos).SafeNormalize(Vector2.Zero) * 4f;

            int DustSnow = Dust.NewDust(spawnPos, 0, 0, DustID.SnowflakeIce);
            Main.dust[DustSnow].velocity = velocity;
            Main.dust[DustSnow].scale = 3.5f;
            Main.dust[DustSnow].noGravity = true;
            Main.dust[DustSnow].fadeIn = 1.2f;

            int DustIce = Dust.NewDust(spawnPos, 0, 0, DustID.IceTorch);
            Main.dust[DustIce].velocity = velocity;
            Main.dust[DustIce].scale = 1.5f;
            Main.dust[DustIce].noGravity = true;
            Main.dust[DustIce].fadeIn = 1.2f;
        }
    }
    public override bool OnTileCollide(Vector2 oldVelocity)
    {
        Projectile.penetrate--;
        if (Projectile.penetrate <= 0)
        {
            Projectile.Kill();
        }
        else
        {
            Projectile.ai[0] += 0.1f;
            if (Projectile.velocity.X != oldVelocity.X)
            {
                Projectile.velocity.X = -oldVelocity.X;
            }
            if (Projectile.velocity.Y != oldVelocity.Y)
            {
                Projectile.velocity.Y = -oldVelocity.Y;
            }
            Projectile.velocity *= 1.25f;
            Projectile.damage = (int)(Projectile.damage * 0.75f);
        }
        return false;
    }
}
