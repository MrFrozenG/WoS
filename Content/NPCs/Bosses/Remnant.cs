using Microsoft.Xna.Framework;
using Mono.Cecil;
using System;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.NPCs.Base;

namespace WoS.Content.NPCs.Bosses
{
    public static class GlobalVariables
    {
        public static bool Phase2 = false;
    }
    internal class RemnantBossHead : WormHead
    {
        public override int BodyType => ModContent.NPCType<RemnantBossBody>();
        public override int TailType => ModContent.NPCType<RemnantBossTail>();
        public override void SetStaticDefaults()
        {
            var drawModifier = new NPCID.Sets.NPCBestiaryDrawModifiers()
            { // Influences how the NPC looks in the Bestiary
              //           CustomTexturePath = "ExampleMod/Content/NPCs/ExampleWorm_Bestiary", // If the NPC is multiple parts like a worm, a custom texture for the Bestiary is encouraged.
                Position = new Vector2(40f, 24f),
                PortraitPositionXOverride = 0f,
                PortraitPositionYOverride = 12f
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, drawModifier);
        }

        public override void SetDefaults()
        {
            NPC.width = NPC.height = 40;

            NPC.scale = 1.2f;
            NPC.knockBackResist = 0f;

            NPC.boss = true;
            NPC.npcSlots = 10f;

            NPC.damage = 70;
            NPC.defense = 0;
            NPC.lifeMax = 20000;
            NPC.aiStyle = -1;

            NPC.noGravity = true;
            NPC.noTileCollide = true;
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            // We can use AddRange instead of calling Add multiple times in order to add multiple items at once
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				// Sets the spawning conditions of this NPC that is listed in the bestiary.
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Underground,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Caverns,

				// Sets the description of this NPC that is listed in the bestiary.
				new FlavorTextBestiaryInfoElement("Looks like a Digger fell into some aqua-colored paint. Oh well.")
            });
        }

        public override void Init()
        {
            if (Main.expertMode)
            {
                MinSegmentLength = MaxSegmentLength = 45;
                NPC.damage = 90;
            }
            if (Main.masterMode)
            {
                MinSegmentLength = MaxSegmentLength = 80;
                NPC.damage = 120;
            }
            else
            {
                MinSegmentLength = MaxSegmentLength = 25;
            }
            CommonWormInit(this);
        }

        // This method is invoked from ExampleWormHead, ExampleWormBody and ExampleWormTail
        internal static void CommonWormInit(Worm worm)
        {
            // These two properties handle the movement of the worm
            worm.MoveSpeed = 14.5f;
            worm.Acceleration = 0.095f;
        }

        private int attackCounter;
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(attackCounter);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            attackCounter = reader.ReadInt32();
        }

        public override void AI()
        {
            MovePatternCircle();
        }

        private void MovePatternCircle()
        {
            Player player = Main.player[NPC.target];
            float distance = 300f; // Дистанция, на которой враг должен вращаться вокруг игрока

            if (NPC.Distance(player.Center) <= distance)
            {
                Vector2 targetPosition = player.Center + new Vector2(distance, 0f).RotatedBy(NPC.ai[1]); // Позиция вокруг игрока

                // Движение врага к целевой позиции
                float speed = 4f; // Скорость движения врага
                Vector2 moveDirection = targetPosition - NPC.Center;
                float magnitude = (float)Math.Sqrt(moveDirection.X * moveDirection.X + moveDirection.Y * moveDirection.Y);
                if (magnitude > 0f)
                {
                    moveDirection *= speed / magnitude;
                }

                NPC.velocity = moveDirection;

                // Установка угла вращения для врага
                NPC.rotation = moveDirection.ToRotation();

                // Обновление AI
                NPC.ai[1] += 0.02f; // Угол поворота можно изменить для разной скорости вращения
            }
        }
    }

    internal class RemnantBossBody : WormBody
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<RemnantBossHead>());

            NPC.damage = 35;
            NPC.defense = 15;
        }

        public override void Init()
        {
            RemnantBossHead.CommonWormInit(this);
        }
        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write(AttackCounter);
            writer.Write(AttackPhase);
            writer.Write(ShadowRainTimerCounter);
        }
        public override void ReceiveExtraAI(BinaryReader reader)
        {
            AttackCounter = reader.ReadInt32();
            AttackPhase = reader.ReadInt32();
            ShadowRainTimerCounter = reader.ReadInt32();
        }

//        public bool Phase2 = false;
        public int AttackCounter;
        public int AttackPhase = 0;
// 0 = Shadow Rain
// 1 = Flames
        public int ShadowRainTimerCounter;
        public override void AI()
        {
            AttackCounter--;
            Player target = Main.player[NPC.target];
            if (NPC.life <= NPC.lifeMax / 2) GlobalVariables.Phase2 = true;
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!GlobalVariables.Phase2)
                {
                    if (AttackCounter <= 0 && Vector2.Distance(NPC.Center, target.Center) < 450 && Collision.CanHit(NPC.Center, 1, 1, target.Center, 1, 1))
                    {
                        Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                        direction = direction.RotatedByRandom(MathHelper.ToRadians(10));

                        int projectile = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 1, ProjectileID.ShadowBeamHostile, NPC.damage, 0, Main.myPlayer);
                        Main.projectile[projectile].timeLeft = 600;
                        AttackCounter = 250;
                        NPC.netUpdate = true;
                    }
                }
                else
                {
                    if (AttackPhase == 0)
                    {
                        if (Main.expertMode) ShadowRainTimerCounter = 420;
                        if (Main.masterMode) ShadowRainTimerCounter = 640;
                        else ShadowRainTimerCounter = 240;
                        int ShadowRainTimer;
                        for  (ShadowRainTimer = 0; ShadowRainTimer < ShadowRainTimerCounter; ShadowRainTimer++)
                        {
                            NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCID.ChaosBall, NPC.whoAmI, 0, Main.myPlayer);
                        }
                        if (ShadowRainTimer >= ShadowRainTimerCounter)
                        {
                            AttackPhase = 1;
                        }
                    }
                    if (AttackPhase == 1)
                    {
                        const int NumProjectiles = 3;
                        for (int i = 0; i < NumProjectiles; i++)
                        {
                            Vector2 direction = (target.Center - NPC.Center).SafeNormalize(Vector2.UnitX);
                            direction = direction.RotatedByRandom(MathHelper.ToRadians(10));
                            Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, direction * 1, ProjectileID.Shadowflames, NPC.damage, 0, Main.myPlayer);
                        }
                    }
                }
                NPC.netUpdate = true;
            }
        }
    }

        internal class RemnantBossTail : WormTail
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true // Hides this NPC from the Bestiary, useful for multi-part NPCs whom you only want one entry.
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.CloneDefaults(ModContent.NPCType<RemnantBossHead>());

            NPC.damage = 5;
            NPC.defense = 50;
            NPC.lifeMax = 10000;
        }

        public override void Init()
        {
            RemnantBossHead.CommonWormInit(this);
        }
    }
}
