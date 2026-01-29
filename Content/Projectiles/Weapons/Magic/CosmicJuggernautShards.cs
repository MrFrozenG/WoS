using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Core.DamageClasses;
using WoS.Content.Core.ModPlayers;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Magic
{
    public class CosmicJuggernautShards : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;

            Projectile.friendly = true;
            Projectile.hostile = false;

            Projectile.penetrate = 1;
            Projectile.timeLeft = 180;

            Projectile.tileCollide = true;
            Projectile.ignoreWater = true;

            Projectile.DamageType = ModContent.GetInstance<MagicSupport>();
        }
        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        private const int GravityDelay = 95;
        public override void AI()
        {
            Projectile.rotation += Projectile.velocity.Length() * 0.08f * Projectile.direction;
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
            if (!player.active) return;

            int supportPoints = (int)Projectile.ai[0];
            int manaRestore = player.GetModPlayer<MainPlayer>().MeteorSet ? 8 + supportPoints : 4 + supportPoints;

            // Единый универсальный вызов
            SupportPlayerUtils.RestoreManaToTeamUniversal(player, manaRestore, 512f);

            // Улучшение траектории, если полный сет
            if (player.GetModPlayer<MainPlayer>().MeteorSet)
            {
                Projectile.velocity = Vector2.Lerp(
                    Projectile.velocity,
                    Projectile.DirectionTo(target.Center) * Projectile.velocity.Length(),
                    0.15f
                );
            }
        }
    }
}
