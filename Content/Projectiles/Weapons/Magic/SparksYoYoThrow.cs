using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework.Graphics;
using WoS.Content.Core.ModUtils;
using WoS.Content.Items.Weapons.Magic;

namespace WoS.Content.Projectiles.Weapons.Magic;

public class SparksYoYoThrow : ModProjectile
{
    public int ManaCounter
    {
        get => (int)Projectile.ai[0];
        set => Projectile.ai[0] = value;
    }
    private int manaCounter;
    private int ManaActive => (int)Projectile.localAI[0];
    public override void SetStaticDefaults()
    {
        //DO NOT CHANGE
        ProjectileID.Sets.YoyosLifeTimeMultiplier[Projectile.type] = -1f;
        ProjectileID.Sets.YoyosMaximumRange[Projectile.type] = 250f;
        ProjectileID.Sets.YoyosTopSpeed[Projectile.type] = 13.5f;

        ProjectileID.Sets.TrailCacheLength[Type] = 4;
        ProjectileID.Sets.TrailingMode[Type] = 2;
    }

    public override void SetDefaults()
    {
        //DO NOT CHANGE
        Projectile.width = 16;
        Projectile.height = 16;
        Projectile.aiStyle = ProjAIStyleID.Yoyo;
        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Magic;
        Projectile.penetrate = -1;
        Projectile.GetGlobalProjectile<ProjectileUtilsGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Projectiles/Weapons/Melee/YoYo/NewProtocolThrow_Glow").Value;
    }

    public override void PostDraw(Color lightColor)
    {
        Projectile.netUpdate = true;
        float time = Main.GlobalTimeWrappedHourly % 1f;

        VisualUtils.DrawPrettyStarSparkle(
            opacity: 1f,
            dir: SpriteEffects.None,
            drawPos: Projectile.Center - Main.screenPosition,
            drawColor: Color.White,
            shineColor: Color.Gold,
            flareCounter: time,
            fadeInStart: 0f,
            fadeInEnd: 0.1f,
            fadeOutStart: 0.9f,
            fadeOutEnd: 1f,
            rotation: Main.GlobalTimeWrappedHourly * MathHelper.TwoPi,
            scale: new Vector2(1.2f),
            fatness: new Vector2(1f, 2f)
        );

        // Звёздочки от попаданий
        if (Main.myPlayer != Projectile.owner || sparklePositions.Count == 0)
            return;

        foreach (var pos in sparklePositions)
        {
            Vector2 drawPos = pos - Main.screenPosition;
            float rotation = Main.rand.NextFloat(MathHelper.TwoPi);

            VisualUtils.DrawPrettyStarSparkle(
                opacity: 1.5f, // Ярче (было 1f)
                dir: SpriteEffects.None,
                drawPos: drawPos,
                drawColor: Color.White,
                shineColor: Color.Gold,
                flareCounter: time,
                fadeInStart: 0f,
                fadeInEnd: 0.1f,
                fadeOutStart: 0.9f,
                fadeOutEnd: 1f,
                rotation: rotation,
                scale: new Vector2(1.6f), // Было 1.2f — теперь крупнее
                fatness: new Vector2(1.2f, 2.5f) // чуть жирнее по вертикали
            );
        }
    }

    private void VisualPassive()
    {
        for (int i = 0; i < 2; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.HallowedWeapons, Scale: 0.7f);
            dust.noGravity = true;
            Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
            dust.velocity = vector;
            dust.position = Projectile.Center - (vector * 24f);
        }

        for (int i = 0; i < 3; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.YellowTorch, Scale: 0.3f);
            dust.noGravity = true;
            Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
            dust.velocity = vector;
            dust.position = Projectile.Center - (vector * 24f);
        }
    }
    private List<Vector2> sparklePositions = new();
    private int sparkleTimer;
    public override void AI()
    {
        Player owner = Main.player[Projectile.owner];

        VisualPassive();
        if (owner.channel && owner.statMana >= 15)
        {
            manaCounter = ++manaCounter % 60;
            if (manaCounter % 20 == 0)
            {
                bool spentMana = false;

                int targetsHit = 0;
                for (int i = 0; i < Main.maxNPCs && targetsHit < 5; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.CanBeChasedBy(Projectile) && npc.Distance(Projectile.Center) < 280f && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
                    {
                        int damage = Projectile.damage / 2;
                        bool crit = Main.rand.NextFloat() < owner.GetCritChance(DamageClass.Magic) / 100f;
                        float knockBack = Projectile.knockBack;
                        owner.ApplyDamageToNPC(npc, damage, knockBack, npc.position.X < Projectile.position.X ? -1 : 1, crit);

                        spentMana = true;
                        targetsHit++;

                        for (int d = 0; d < 5; d++)
                        {
                            Vector2 offset = Main.rand.NextVector2Circular(10f, 10f);
                            Vector2 sparklePos = npc.Center + offset;
                            Dust dust = Dust.NewDustPerfect(sparklePos, DustID.GoldFlame, offset.SafeNormalize(Vector2.One) * 1.5f, 150, Color.Yellow, 1.2f);
                            dust.noGravity = true;
                        }

                        if (Main.myPlayer == owner.whoAmI)
                        {
                            sparklePositions.Add(npc.Center);
                            sparkleTimer = 10;
                        }
                    }
                }

                if (spentMana)
                {
                    owner.statMana = Math.Max(owner.statMana - 15, 0);
                }
            }
        }

        if (owner.statMana < 15)
            owner.channel = false;

        // Таймер для звёздочек
        if (sparkleTimer > 0)
            sparkleTimer--;
        else
            sparklePositions.Clear();
    }
}
/*
OLD AI


private void CastMagic(Vector2 velocity)
{
    int type = ModContent.ProjectileType<SunshineShot>();
    Player owner = Main.player[Projectile.owner];
    owner.statMana = System.Math.Max(owner.statMana - 5, 0);

    Projectile proj = Projectile.NewProjectileDirect(Projectile.GetSource_FromAI(), Projectile.Center, velocity, type, Projectile.damage, Projectile.knockBack / 2f, Projectile.owner, 0f, 0f);
    proj.friendly = true;
    proj.hostile = false;
    proj.netUpdate = true;
}

public override void AI()
{
    for (int i = 0; i < 2; i++)
    {
        Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.HallowedWeapons, Scale: 0.7f);
        dust.noGravity = true;

        Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
        dust.velocity = vector;
        dust.position = Projectile.Center - (vector * 24f);
    }

    for (int i = 0; i < 3; i++)
    {
        Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.YellowTorch, Scale: 0.3f);
        dust.noGravity = true;

        Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
        dust.velocity = vector;
        dust.position = Projectile.Center - (vector * 24f);
    }

    Player owner = Main.player[Projectile.owner];
    if (owner.channel && owner.statMana > 0)
    {
        manaCounter = ++manaCounter % 60;
        if (manaCounter % (60 / 5) == 0) owner.statMana--;

        if (owner.whoAmI == Main.myPlayer && /*owner.controlUseTile &&
Projectile.frameCounter <= 0)
        {
            var target = Main.npc.Where(x => x.Distance(Projectile.Center) < 640 && x.CanBeChasedBy(Projectile)
                && Collision.CanHit(Projectile.position, Projectile.width, Projectile.height, x.position, x.width, x.height)).OrderBy(x => x.Distance(Projectile.Center)).FirstOrDefault();
            if (target != default)
            {
                CastMagic(Projectile.DirectionTo(target.Center)* 10);
Projectile.velocity = Projectile.DirectionFrom(target.Center)* 2;
                Projectile.frameCounter = 20;
                Projectile.netUpdate = true;
            }
        }
        if (Projectile.frameCounter > 0) Projectile.frameCounter--;
    }
    if (owner.statMana <= 0) owner.channel = false;
}





    /*
    public int manaPassive //Mana usage per second (60 ticks)
    {
        get => (int)Projectile.ai[1];
        set => Projectile.ai[1] = value;
    }
    public int manaActive //Mana cost *5, value from Weapon
    {
        get => (int)Projectile.ai[2];
        set => Projectile.ai[2] = value;
    }
    public float useTime; //Weapon useTime, used for decreasing AttackCDs

    public int attackCD = 270; //4.5 secs, base value for attack (Weapon useTime: 60, Default value)
                               //The more weapon useTime, the more AttackCD, the lesser useTime > Lesser AttackCD
    public static int AttackCDMin = 60; //Minimum value of Attack rate, cannot be lesser than that 

    private List<Vector2> sparklePositions = new();
    private int sparkleTimer;

    private float distance = (18 * 16) * 1f; //18 Tiles range
    public int MaxiumumHits = 5;
    */