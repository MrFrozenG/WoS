using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

using WoS.Content.Buffs.SupportBuffs;
using WoS.Content.Projectiles.Weapons.Magic;
using WoS.Content.Systems.DamageClasses;
using WoS.Content.Systems.Interfaces;

namespace WoS.Content.Items.Weapons.Magic;
public class StaffCelestialOrder : ModItem, ISupportWeapon
{
    private IEntitySource source;
    public int BaseSupportPoints => 10;

    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true;
    }
    public override void SetDefaults()
    {
        Item.DamageType = ModContent.GetInstance<MagicSupport>();
        Item.damage = 24;
        Item.knockBack = 1f;
        Item.crit = 12;
        Item.noMelee = true;
        Item.mana = 15;

        Item.width = Item.height = 42;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 25;
        Item.useTime = 25;
        Item.reuseDelay = 25;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item4;

        Item.shoot = ModContent.ProjectileType<CelestialOrderBlades>();
        Item.shootSpeed = 12f;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 5, 35, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        ApplyAttack(player, velocity);
        return false;
    }
    public override bool? UseItem(Player player)
    {

        int radius = 60 * 16; // 60 блоков в пикселях (1 блок = 16 пикселей)
        int buffCount = 0;

        foreach (Player teammate in Main.player)
        {
            if (teammate.active && teammate != player && teammate.team == player.team && teammate.Distance(player.Center) <= radius)
            {
                ApplyBuffs(teammate);
                buffCount++;
            }
        }

        ApplyUserBuff(player, buffCount);

        return true;
    }

    private void AlterAttack(Player player, Vector2 velocity)
    {
        int NumProjectiles = Main.rand.Next(4, 8);
        for (int i = 0; i < NumProjectiles; i++)
        {
            Vector2 newVelocity = velocity.RotatedByRandom(MathHelper.ToRadians(5));
            newVelocity *= 1f - Main.rand.NextFloat(0.2f);
            Projectile.NewProjectileDirect(source, player.Center, newVelocity, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
        }
    }
    private void ApplyAttack(Player player, Vector2 velocity)
    {
        int DistanceToEnemy = 48 * 16;
        int projectileCount = Main.rand.Next(4, 8); // Случайное количество снарядов от 4 до 7
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPosition = player.Center + new Vector2(Main.rand.Next(-780, 780), -DistanceToEnemy); // Позиция появления снаряда
            NPC target = GetNearestEnemy(player);

            if (target != null)
            {
                Vector2 direction = target.Center - spawnPosition;
                direction.Normalize();
                direction *= Item.shootSpeed * 1.5f;

               Projectile.NewProjectileDirect(source, spawnPosition, direction, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
            else
            {
                Vector2 direction = Main.MouseWorld - spawnPosition;
                direction.Normalize();
                direction *= Item.shootSpeed * 1.5f;

                Projectile.NewProjectileDirect(source, spawnPosition, direction, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
        }
    }

    private NPC GetNearestEnemy(Player player)
    {
        NPC nearestNPC = null;
        float minDistance = float.MaxValue;

        foreach (NPC npc in Main.npc.Where(npc => npc.active && !npc.friendly && npc.lifeMax > 5 && !npc.dontTakeDamage && npc.type != NPCID.TargetDummy))
        {
            float distance = Vector2.Distance(player.Center, npc.Center);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestNPC = npc;
            }
        }

        return nearestNPC;
    }
    private void ApplyBuffs(Player player)
    {
        int PercentofHP = player.statLifeMax2 / 100;
        player.AddBuff(ModContent.BuffType<BuffPureWill>(), 600);
        if (player.statLife <= player.statLifeMax2 / 2)
        {
            player.Heal(PercentofHP * 2);
        }
    }

    private void ApplyUserBuff(Player player, int count)
    {
        int PercentofHP = player.statLifeMax2 / 100;
        int BuffTime = 140 + (count * 40);
        if (count > 0)
        {
            player.AddBuff(ModContent.BuffType<BuffPureDesire>(), BuffTime);
            if (player.statLife <= player.statLifeMax2/2)
            {
                player.Heal(PercentofHP * 2);
            }
        }
    }
    public override void AddRecipes()
    {
        CreateRecipe()
        .AddIngredient(ItemID.GoldBar, 25)
        .AddIngredient(ItemID.FallenStar, 10)
        .AddIngredient(ItemID.Feather, 5)
        .AddTile(TileID.Anvils)
            .Register();
        var resultItem = this;
        resultItem.CreateRecipe()
        .AddIngredient(ItemID.PlatinumBar, 25)
        .AddIngredient(ItemID.FallenStar, 10)
        .AddIngredient(ItemID.Feather, 5)
        .AddTile(TileID.Anvils)
            .Register();
    }
}