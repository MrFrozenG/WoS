using Terraria.Audio;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Items.Weapons.Ranged.Throw;
using Microsoft.Xna.Framework;
using Terraria.Localization;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Ranged.Throw
{
    public class BoneKnifeThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
        }

        public int GravityDelayTimer
        {
            get => (int)Projectile.ai[2];
            set => Projectile.ai[2] = value;
        }
        private bool hasSplit = false;

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
        }

        private const int GravityDelay = 34;

        public override bool PreDraw(ref Color lightColor)
        {
            return ProjectileUtils.ProjectileTrailVisualPreDraw(Projectile, ref lightColor);
        }

        public override void AI()
        {
            Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
            Projectile.rotation = Projectile.velocity.ToRotation();

            if (!hasSplit && GravityDelayTimer >= GravityDelay)
            {
                SplitIntoShards();
                hasSplit = true;
                Projectile.Kill(); // Удалить оригинальный снаряд
                return;
            }

            GravityDelayTimer++;

            if (Main.rand.NextBool(2))
            {
                Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.height, Projectile.width, DustID.Bone, Projectile.velocity.X * .2f, Projectile.velocity.Y * .2f, Projectile.alpha, Scale: 0.95f);
                dust.velocity += Projectile.velocity * 0.3f;
                dust.velocity *= 0.2f;
            }
        }

        private void SplitIntoShards()
        {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);

            float baseRotation = Projectile.velocity.ToRotation();
            float[] angles = { -0.25f, 0f, 0.25f }; // Углы относительно направления (в радианах ~ -15°, 0°, +15°)

            for (int i = 0; i < 3; i++)
            {
                Vector2 shardVelocity = baseRotation.ToRotationVector2().RotatedBy(angles[i]) * Projectile.velocity.Length() * 0.75f;

                int shard = Projectile.NewProjectile(
                    Projectile.GetSource_FromThis(),
                    Projectile.Center,
                    shardVelocity,
                    ModContent.ProjectileType<BoneKnifeShard>(),
                    (int)(Projectile.damage * 0.75f),
                    Projectile.knockBack,
                    Projectile.owner
                );

                Main.projectile[shard].ai[2] = 0; // Устанавливаем таймер гравитации для шардов
            }
        }
        public override void OnKill(int timeLeft)
        {
            if (!hasSplit)
            {
                SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
                Vector2 usePos = Projectile.position;

                for (int i = 0; i < 20; i++)
                {
                    Dust dust = Dust.NewDustDirect(usePos, Projectile.width, Projectile.height, DustID.Bone);
                    dust.position = (dust.position + Projectile.Center) / 2f;
                    dust.velocity += usePos * 2f;
                    dust.velocity *= 0.5f;
                    dust.noGravity = true;
                    usePos -= usePos * 8f;
                }

                if (Projectile.owner == Main.myPlayer)
                {
                    int item = 0;
                    if (Main.rand.NextBool(18))
                    {
                        item = Item.NewItem(Projectile.GetSource_DropAsItem(), Projectile.getRect(), ModContent.ItemType<BoneKnife>());
                    }
                    if (Main.netMode == NetmodeID.MultiplayerClient && item >= 0)
                    {
                        NetMessage.SendData(MessageID.SyncItem, -1, -1, null, item, 1f);
                    }
                }
            }
        }
    }
}
