using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Terraria.Audio;

namespace WoS.Content.Projectiles.Weapons.Magic;
public class CosmicEnergy : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 3;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 1;
    }

    public override void SetDefaults()
    {
        Projectile.width = 26;
        Projectile.height = 26;
        Projectile.aiStyle = 1;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = true;
        Projectile.light = 0.9f;
        Projectile.penetrate = 2;
        Projectile.scale = 0.5f;
        AIType = ProjectileID.Bullet;
    }

    public override void OnKill(int timeLeft)
    {
        Collision.HitTiles(Projectile.position + Projectile.velocity, Projectile.velocity, Projectile.width, Projectile.height);
        SoundEngine.PlaySound(SoundID.Item4, Projectile.position);
    }
    public override void AI()
    {
        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        Projectile.penetrate--;
        if (Projectile.penetrate <= 2) Projectile.damage += (Projectile.damage * 2);
    }
}