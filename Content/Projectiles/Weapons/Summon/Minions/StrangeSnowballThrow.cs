
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Minion;

namespace WoS.Content.Projectiles.Weapons.Summon.Minions
{
    public class StrangeSnowballThrow : ModProjectile
    {
        private const int GravityDelay = 25;

        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        public override void SetDefaults()
        {
            Projectile.width = 12;
            Projectile.height = 12;
            Projectile.friendly = true; 
            Projectile.hostile = false;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true; 
            Projectile.penetrate = 1; 
            Projectile.DamageType = DamageClass.Summon;
            Projectile.aiStyle = 0; 
        }
        public override bool? CanHitNPC(NPC target)
        {
            return true; 
        }
        public override void AI()
        {
            Projectile.rotation += 0.3f * Projectile.direction;

            if (GravityDelayTimer < GravityDelay) GravityDelayTimer++;
            if (GravityDelayTimer >= GravityDelay)
            {
                GravityDelayTimer = GravityDelay;
                Projectile.velocity.X *= 0.98f;
                Projectile.velocity.Y += 0.35f;
            }
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            Player player = Main.player[Projectile.owner];
            player.AddBuff(ModContent.BuffType<SnowmanMinionBuff>(), 10);
            int minionType = ModContent.ProjectileType<SnowmanMinion>();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, minionType, 0, 0f, player.whoAmI);
            Projectile.Kill();
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Player player = Main.player[Projectile.owner];
            player.AddBuff(ModContent.BuffType<SnowmanMinionBuff>(), 10);
            int minionType = ModContent.ProjectileType<SnowmanMinion>();
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, minionType, Projectile.damage, 0f, player.whoAmI);
            Projectile.Kill();
            return false; 
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
        {
            fallThrough = true;
            return true;
        }
    }
}
