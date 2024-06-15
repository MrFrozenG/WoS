using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Items.Weapons.Vanila;

public class PhoenixBlasterEdit : GlobalItem
{
    public override bool InstancePerEntity => true;
    public override bool AppliesToEntity(Item item, bool lateInstantiation)
    {
        return item.type == ItemID.PhoenixBlaster;
    }

    public int Counter = 4;
    public override void SetDefaults(Item item)
    {
        item.StatsModifiedBy.Add(Mod);
        item.autoReuse = true;
    }

    public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        Counter--;

        if (player.altFunctionUse == 2 && player.whoAmI == Main.myPlayer)
        {
            Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<PhoenixShot>(), damage, knockback, player.whoAmI);
            Counter = 4;
        }

        return true;
    }

    public override bool AltFunctionUse(Item item, Player player)
    {
        return (Counter <= 0);
    }

    public override bool CanUseItem(Item item, Player player)
    {
        if (player.altFunctionUse == 2)
        {
            if (Counter > 0)
                return true;
        }

        return true;
    }
}

public class PhoenixShot : ModProjectile
{
    public override void SetStaticDefaults()
    {
        ProjectileID.Sets.AllowsContactDamageFromJellyfish[Type] = true;
        Main.projFrames[Type] = 6;
    }

    public int Hits = 0;
    public override void SetDefaults()
    {
        Projectile.width = 16;
        Projectile.height = 16;

        Projectile.friendly = true;
        Projectile.DamageType = DamageClass.Ranged;
        Projectile.ignoreWater = true;
        Projectile.tileCollide = false;
        Projectile.penetrate = -1;
        Projectile.scale = 1f;

        Projectile.alpha = 255; // How transparent to draw this projectile. 0 to 255. 255 is completely transparent.
    }

    public override Color? GetAlpha(Color lightColor)
    {
        // return Color.White; Color(255, 165, 0);
        return new Color(255, 165, 0, 100) * Projectile.Opacity;
    }
    
    public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
    {
        target.AddBuff(BuffID.OnFire, 360);
        Hits++;
        if (Hits >= 4)
        {
            Projectile.tileCollide = true;
            Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, ProjectileID.ExplosiveBullet, Projectile.damage + (Hits*2), Projectile.knockBack, Main.myPlayer, 0, 1);
        }
    }
    public override void AI()
    {
        FadeInAndOut();
        Visual();
        if (++Projectile.frameCounter >= 6)
        {
            Projectile.frameCounter = 0;
            if (++Projectile.frame >= Main.projFrames[Projectile.type])
                Projectile.frame = 0;
        }

        if (Projectile.ai[0] >= 20f)
            Projectile.Kill();

        Projectile.direction = (Projectile.velocity.X > 0).ToDirectionInt();
        Projectile.rotation = Projectile.velocity.ToRotation();
    }
    public void Visual()
    {
        for (int i = 0; i < 3; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.Torch, Scale: 0.3f);
            dust.noGravity = true;

            Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.0f, 4.0f);
            dust.velocity = vector;
            dust.position = Projectile.Center - (vector * 24f);
        }
        for (int i = 0; i < 4; i++)
        {
            Dust dust = Dust.NewDustDirect(Projectile.Center, Projectile.width, Projectile.height, DustID.Lava, Scale: 0.45f);
            dust.noGravity = true;

            Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(1.0f, 2.0f);
            dust.velocity = vector;
            dust.position = Projectile.Center - (vector * 24f);
        }
    }
    
    public void FadeInAndOut()
    {
        // If last less than 50 ticks — fade in, than more — fade out
        if (Projectile.ai[0] <= 50f)
        {
            // Fade in
            Projectile.alpha -= 25;
            // Cap alpha before timer reaches 50 ticks
            if (Projectile.alpha < 100)
                Projectile.alpha = 100;

            return;
        }
    }
}