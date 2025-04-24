using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace WoS.Content.Projectiles.Weapons.Magic;

public class StaffofBlizzardShot : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
        ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
    }
    public override void SetDefaults()
    {
        Projectile.DamageType = DamageClass.Magic;
        Projectile.knockBack = 0.1f;
        Projectile.hostile = false;
        Projectile.friendly = true;
        Projectile.width = Projectile.height = 24;
        Projectile.tileCollide = false;
    }
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.Frostburn, 120);
    }
    public override void AI()
    {
        Player player = Main.player[Projectile.owner];
        if (Projectile.position.Y >= player.position.Y)
        {
            Projectile.tileCollide = true;
        }

        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();
        for (int i = 0; i < 4; i++)
        {
            Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-15, 15), Main.rand.Next(-15, 15));
            Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.SnowflakeIce, 0f, 0f, 100, default, 0.5f);
        }
    }
    public override void OnKill(int timeLeft)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector2 dustPosition = Projectile.Center + new Vector2(Main.rand.Next(-25, 25), Main.rand.Next(-25, 25));
            Dust.NewDust(dustPosition, Projectile.width, Projectile.height, DustID.Ice, 0f, 0f, 100, default(Color), 0.75f);
        }
    }
}
