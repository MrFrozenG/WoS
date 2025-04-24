using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Magic;

public class CelestialOrderBlades : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
        Main.projFrames[Projectile.type] = 8;
    }
    int CurrentFrame;
    public override void SetDefaults()
    {
        Projectile.damage = 10;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.knockBack = 0.1f;
        Projectile.hostile = false;
        Projectile.friendly = true;
        Projectile.width = Projectile.height = 48;
        CurrentFrame = Main.rand.Next(0, 9);
        Projectile.tileCollide = false;
    }

    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (Projectile.position.Y >= player.position.Y)
        {
            Projectile.tileCollide = true;
        }
        SetVisualOffsets();

        if (Main.rand.NextBool(3))
        {
            Projectile.alpha = (int)(100 + 100 * Math.Sin(Main.GameUpdateCount * 0.1));

            // Пульсация размера
            float scale = 1f + 0.1f * (float)Math.Sin(Main.GameUpdateCount * 0.1);
            Projectile.scale = scale;

        }
        Projectile.frame = CurrentFrame;
    }

    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Player player = Main.player[Projectile.owner];
        if (player.statLife <= player.statLifeMax2 / 3) damageDone *= 5;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        return new Color(255, 215, 0, 255) * Projectile.Opacity;
    }

    private void SetVisualOffsets()
    {
        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();
        DrawOffsetX = (int)-DrawOriginOffsetX * 2;
        DrawOriginOffsetY = 0;

        if (Main.rand.NextBool(2))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.HallowedTorch, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
            dust.velocity += Projectile.velocity * 0.3f;
            dust.velocity *= 0.2f;
        }
        if (Main.rand.NextBool(4))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.HallowedWeapons, 0, 0, 254, Scale: 0.9f);
            dust.velocity += Projectile.velocity * 0.5f;
            dust.velocity *= 0.5f;
        }
    }
}
