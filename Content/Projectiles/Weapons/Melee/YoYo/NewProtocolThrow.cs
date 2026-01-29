using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;
using WoS.Content.Projectiles.Weapons.Common;
using Microsoft.Xna.Framework.Graphics;
using WoS.Content.Core.ModUtils;

namespace WoS.Content.Projectiles.Weapons.Melee.YoYo
{
    public class NewProtocolThrow : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
            ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 220f;
            ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 11.5f;

            ProjectileID.Sets.TrailCacheLength[Type] = 4;
            ProjectileID.Sets.TrailingMode[Type] = 2;
        }
        public override void SetDefaults()
        {
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = ProjAIStyleID.Yoyo;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.MeleeNoSpeed;
            Projectile.penetrate = -1;
            Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Melee/YoYo/NewProtocolThrow_Glow").Value;
        }

        public int CD = 60;
        int timer = 0;
        public override void AI()
        {
            timer++;

            if (timer >= CD)
            {
                timer = 0;

                // ---- 1. Сбор потенциальных целей ----
                List<NPC> targets = new List<NPC>();

                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(Projectile)
                        && npc.Distance(Projectile.Center) < 360f
                        && Collision.CanHit(
                            Projectile.position, Projectile.width, Projectile.height,
                            npc.position, npc.width, npc.height))
                    {
                        targets.Add(npc);
                    }
                }

                if (targets.Count == 0)
                    return;

                // Сортировка по расстоянию (ближайшие — первые)
                targets.Sort((a, b) =>
                    a.Distance(Projectile.Center).CompareTo(b.Distance(Projectile.Center)));

                // Берём только первых 4 врагов
                int realTargetCount = Math.Min(targets.Count, 4);

                // ---- 2. Определяем, сколько лазеров выдаём каждой цели ----
                // Всего лазеров — 4
                int lasersToShoot = 4;
                int lasersPerTarget = lasersToShoot / realTargetCount;
                int remainder = lasersToShoot % realTargetCount;

                for (int t = 0; t < realTargetCount; t++)
                {
                    NPC npc = targets[t];

                    // На случай 1–2 целей — посылаем лишние лазеры в первые цели
                    int lasersForThisNpc = lasersPerTarget + (t < remainder ? 1 : 0);

                    for (int l = 0; l < lasersForThisNpc; l++)
                    {
                        // ---- 3. Направление на NPC ----
                        Vector2 direction = npc.Center - Projectile.Center;
                        direction.Normalize();

                        // Умеренный разброс (около ±6 градусов)
                        direction = direction.RotatedByRandom(MathHelper.ToRadians(2f));

                        direction *= 16f; // скорость PinkLaser

                        Projectile laser = Projectile.NewProjectileDirect(
                            Projectile.GetSource_FromThis(),
                            Projectile.Center,
                            direction,
                            ModContent.ProjectileType<RedLaser>(),
                            (int)(Projectile.damage * 0.75f),
                            Projectile.knockBack,
                            Projectile.owner
                        );
                        laser.friendly = true;
                        laser.hostile = false;
                        laser.DamageType = DamageClass.Melee;
                        laser.penetrate = 1;
                        laser.CritChance = 100;
                    }
                }
            }
        }
    }
}
