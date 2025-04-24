using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using WoS.Content.Projectiles.Weapons.Magic;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;
using System.Linq;
using Terraria.GameContent.Events;

namespace WoS.Content.Items.Weapons.Magic;

public class StaffofBlizzard : ModItem
{
    public override void SetStaticDefaults()
    {
        Item.staff[Type] = true;
    }
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Magic;
        Item.damage = 15;
        Item.knockBack = 1f;
        Item.crit = 12;
        Item.noMelee = true;
        Item.mana = 4;

        Item.width = Item.height = 64;
        Item.scale = 1f;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.useAnimation = 25;
        Item.useTime = 25;
        Item.reuseDelay = 15;
        Item.autoReuse = true;
        Item.UseSound = SoundID.Item4;

        Item.shoot = ModContent.ProjectileType<StaffofBlizzardShot>();
        Item.shootSpeed = 12f;

        Item.rare = ItemRarityID.Green;
        Item.value = Item.sellPrice(0, 1, 50, 0);
    }
    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        int DistanceToEnemy = 64 * 16;
        int projectileCount = Main.rand.Next(5, 13); 
        for (int i = 0; i < projectileCount; i++)
        {
            Vector2 spawnPosition = player.Center + new Vector2(Main.rand.Next(-780, 780), -DistanceToEnemy); // Позиция появления снаряда
            {
                Vector2 direction = Main.MouseWorld - spawnPosition;
                direction.Normalize();
                direction *= Item.shootSpeed * 1.7f;

                Projectile.NewProjectileDirect(source, spawnPosition, direction, Item.shoot, Item.damage, Item.knockBack, player.whoAmI);
            }
        }
        return false;
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
}