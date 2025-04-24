using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Systems.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Magic
{
    public class SpectreKunaiThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
        }

        private bool IsChild
        {
            get => Projectile.localAI[0] == 1;
            set => Projectile.localAI[0] = value.ToInt();
        }
        public override void SetDefaults()
        {
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.light = 1f;
            Projectile.scale = 0.75f;
            Projectile.penetrate = 1;
            Projectile.alpha = 95;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 2;
        }

        public override void AI()
        {
            Visual();
            if (IsChild)
            {
                ChildKunai();
            }
        }

        private void Visual()
        {
            Projectile.spriteDirection = (Vector2.Dot(Projectile.velocity, Vector2.UnitX) >= 0f).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2 - MathHelper.PiOver4 * Projectile.spriteDirection;
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return new Color(255, 255, 255, 95) * Projectile.Opacity;
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            int numberOfCopies = Main.rand.Next(4, 7);
            if (!IsChild)
            {
                for (int i = 0; i < numberOfCopies; i++)
                {
                    Vector2 offsetPosition = target.Center + new Vector2(Main.rand.Next(-75, 76), Main.rand.Next(-75, 76));
                    Vector2 direction = (target.Center - offsetPosition).SafeNormalize(Vector2.Zero) * 12f;

                    Projectile child = Projectile.NewProjectileDirect(
                        Projectile.GetSource_FromThis(),
                        offsetPosition,
                        direction,
                        Projectile.type,
                        Projectile.damage,
                        Projectile.knockBack,
                        Main.myPlayer,
                        0,
                        1);
                    (child.ModProjectile as SpectreKunaiThrow).IsChild = true;
                    child.ai[0] = target.whoAmI;
                    child.tileCollide = false;
                    child.penetrate = 1;
                    child.CritChance = -1024;
                    child.damage *= 2;
                    child.localNPCHitCooldown = 0;
                }
            }
            /*
                        Player player = Main.player[Projectile.owner];

                        if (!player.ghostHeal || !player.ghostHurt && player.statLife < player.statLifeMax2/2 && Main.rand.NextBool(10))
                        {
                            float ai1 = (float)damageDone * 0.075f;
                            Main.player[Main.myPlayer].lifeSteal -= ai1;
                            Projectile heal = Projectile.NewProjectileDirect(
                                   Projectile.GetSource_FromThis(),
                                   Projectile.Center,
                                   Projectile.velocity,
                                   ProjectileID.SpiritHeal,
                                   100,
                                   Projectile.knockBack,
                                   Main.myPlayer,
                                   0,
                                   ai1);
                        }
            */
        }

        private void ChildKunai()
        {
            NPC target = Main.npc[(int)Projectile.ai[0]]; // Получаем цель из ai[0]
            if (target != null && target.active)
            {
                Vector2 direction = (target.Center - Projectile.Center).SafeNormalize(Vector2.Zero) * 9f;
                Projectile.velocity = direction;
            }
        }
    }
}
