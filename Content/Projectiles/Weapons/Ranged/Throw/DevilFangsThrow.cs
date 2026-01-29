using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Ranged.Throw;

public class DevilFangsThrow : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 12;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
    }

    public override Color? GetAlpha(Color lightColor)
    {
        if (Main.bloodMoon) return new Color(140, 0, 15, 0) * Projectile.Opacity;
        else return new Color(255, 165, 0, 0) * Projectile.Opacity;
    }

    public int GravityDelayTimer
    {
        get => (int)Projectile.ai[2];
        set => Projectile.ai[2] = value;
    }
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 14;
        Projectile.aiStyle = 0;
        Projectile.friendly = true;
        Projectile.hostile = false;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.light = 0.1f;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        if (Main.rand.NextBool(3)) target.AddBuff(BuffID.OnFire, 180);
    }
    private const int GravityDelay = 95;
    public override void AI()
    {
        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();

        if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
        if (GravityDelayTimer >= GravityDelay)
        {
            GravityDelayTimer = GravityDelay;
            Projectile.velocity.X *= 0.98f;
            Projectile.velocity.Y += 0.35f;
        }
        if (Main.rand.NextBool(2))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Torch, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, 200, Scale: 1.2f);
            dust.velocity += Projectile.velocity * 0.3f;
            dust.velocity *= 0.2f;
        }
        if (Main.rand.NextBool(4))
        {
            Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Lava, 0, 0, 254, Scale: 0.3f);
            dust.velocity += Projectile.velocity * 0.5f;
            dust.velocity *= 0.5f;
        }
    }
    public override void OnKill(int timeLeft)
    {
        SoundEngine.PlaySound(SoundID.Dig, Projectile.position); // Play a death sound
        Vector2 usePos = Projectile.position;

        for (int i = 0; i < 20; i++)
        {
            Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Lava);
            dust.position = (dust.position + Projectile.Center) / 2f;
            dust.velocity += usePos * 2f;
            dust.velocity *= 0.5f;
            dust.noGravity = true;
            usePos -= usePos * 8f;
        }
    }

    public override bool PreDraw(ref Color lightColor)
    {
        return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
    }
}