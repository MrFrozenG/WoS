using Microsoft.Xna.Framework;
using System;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Projectiles.Weapons.Ranged.Throw;

namespace WoS.Content.Core.ModNPCs
{
    public class GlobalNPCStatus : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        //Weapons statuses
        public bool EvilBladesStatus = false;
        public int EvilBladesTimer = 0;
        public bool VileMark = false;
        public int VileMarkTimer = 0;

        //Debuffs
        public bool Burning;

        public override void ResetEffects(NPC npc)
        {
            EvilBladesStatus = false;
            VileMark = false;
            Burning = false;
        }
        public void ApplyEffect(NPC npc, int duration)
        {
            EvilBladesStatus = true;
            VileMark = true;
            EvilBladesTimer = duration;
            VileMarkTimer = duration;
        }

        public override void AI(NPC npc)
        {
            if (EvilBladesTimer > 0)
            {
                EvilBladesTimer--;
                EvilBladesStatus = true;
                VisualsEvilBlades(npc);
            }
            else
            {
                EvilBladesStatus = false;
            }
            if (VileMarkTimer > 0)
            {
                VileMarkTimer--;
                VileMark = true;
                VileMarkVisuals(npc);
            }
            else
            {
                VileMark = false;
            }
        }
        public override void OnKill(NPC npc)
        {
            if (VileMark)
            {
                //               Projectile.NewProjectile(npc.GetSource_Death(), npc.Center, Vector2.Zero, ModContent.ProjectileType<VileCloud>(), 15, 0f, Main.myPlayer);
            }
            base.OnKill(npc);
        }

        public override void ModifyHitPlayer(NPC npc, Player target, ref Player.HurtModifiers modifiers)
        {
            if (EvilBladesStatus)
            {
                modifiers.FinalDamage -= 0.1f;
            }
            base.ModifyHitPlayer(npc, target, ref modifiers);
        }

        private void VisualsEvilBlades(NPC npc)
        {
            int particleCount = 15;
            float radius = 50f;
            float rotationSpeed = 0.3f;

            for (int i = 0; i < particleCount; i++)
            {
                // Расчет позиции частицы
                float angle = MathHelper.ToRadians(360f / particleCount * i + EvilBladesTimer * rotationSpeed);
                Vector2 particleOffset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;

                // Создание частицы
                Dust dust = Dust.NewDustDirect(npc.Center + particleOffset - new Vector2(4, 4), 0, 0, DustID.Cloud, 0f, 0f, 100, default, 1.2f);
                dust.noGravity = true; // Частица не подвержена гравитации
                dust.velocity = Vector2.Zero; // Частицы не движутся
                dust.color = new Color(0, 0, 0);
            }
        }
        private void VileMarkVisuals(NPC npc)
        {

            int particleCount = 30;
            float radius = 25f;
            float rotationSpeed = 0.1f;

            for (int i = 0; i < particleCount; i++)
            {
                // Расчет позиции частицы
                float angle = MathHelper.ToRadians(360f / particleCount * i + VileMarkTimer * rotationSpeed);
                Vector2 particleOffset = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle)) * radius;

                // Создание частицы
                Dust dust = Dust.NewDustDirect(npc.Center + particleOffset - new Vector2(4, 4), 0, 0, DustID.DemonTorch, 0f, 0f, 100, default, 1.2f);
                dust.noGravity = true; // Частица не подвержена гравитации
                dust.velocity = Vector2.Zero; // Частицы не движутся
                dust.color = new Color(146, 136, 204, 100);
            }
        }
    }
}
