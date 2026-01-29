using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Projectiles.Weapons.Vanila;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using Terraria.Localization;
using Terraria.GameContent.ItemDropRules;
using WoS.Content.Projectiles.Weapons.Common;

namespace WoS.Content.Core.Changes.Weapons
{
    public class GC_IceSickle : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.IceSickle;
        }
        public static bool isActive => ModContent.GetInstance<MainConfig>().IceSickleDeepCold || Main.hardMode;
        public override void SetDefaults(Item item)
        {
            if (isActive)
            {
                item.autoReuse = true;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.shoot = ModContent.ProjectileType<IceSickleDeepCold>();
                item.channel = true;
            }
            item.StatsModifiedBy.Add(Mod);
        }

        public override void UpdateInventory(Item item, Player player)
        {
            if (item.type == ItemID.IceSickle && isActive)
            {
                item.autoReuse = true;
                item.noMelee = true;
                item.noUseGraphic = true;
                item.channel = true;
                item.shoot = ModContent.ProjectileType<IceSickleDeepCold>();
            }
        }
        public override void ModifyWeaponDamage(Item item, Player player, ref StatModifier damage)
        {
            if (Main.hardMode)
            {
                damage *= 1.2f;
            }
            else damage *= 0.5f;
        }
        public override float UseSpeedMultiplier(Item item, Player player)
        {
            if (isActive)
            {
                return 1.25f;
            }
            else return 1f;
        }
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            string key;
            if (ModContent.GetInstance<MainConfig>().IceSickleDeepCold)
            {
                key = "IceSickle_DeepCold";
            }
            else if (Main.hardMode)
            {
                key = "IceSickle_DeepCold_IceStorm";
            }
            else 
            {
                key = "IceSickle_DeepCold_NotAwaked";
            }

            TooltipLine line = new TooltipLine(Mod, "IceSickle_DeepCold", Language.GetTextValue("Mods.WoS.General.WeaponTips." + key));
            tooltips.Add(line);
        }
        public override bool Shoot(Item item, Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (isActive)
            {
                Vector2 spawnPos = player.MountedCenter;

                // Смещение чуть влево или вправо, чтобы центр косы оказался на руке
                float offsetX = player.direction == 1 ? 6f : -6f;
                spawnPos.X += offsetX;
                int IceSickleDamage = (int)(damage * 1.5f);
                // Проверка: не создавать повторно, если уже есть
                if (player.ownedProjectileCounts[type] < 1)
                {
                    Projectile.NewProjectile(
                        player.GetSource_ItemUse(item),
                        spawnPos,
                        Vector2.Zero, // Скорость не нужна, движение внутри снаряда
                        type,
                        IceSickleDamage,
                        knockback,
                        player.whoAmI
                    );
                }
                SpawnRandomIceSickles(player, source, damage, knockback);
                return false;
            }
            else return true;
        }
        private void SpawnRandomIceSickles(Player player, EntitySource_ItemUse_WithAmmo source, int damage, float knockback)
        {
            int count = Main.rand.Next(1, 3); 

            for (int i = 0; i < count; i++)
            {
                float angle = Main.rand.NextFloat(MathHelper.TwoPi);
                Vector2 randomDir = angle.ToRotationVector2() * 7.5f; 

                Projectile.NewProjectile(
                    source,
                    player.Center,
                    randomDir,
                    ProjectileID.IceSickle,
                    (int)(damage * 0.5f),
                    knockback,
                    player.whoAmI
                );
            }
        }
    }
    public class GC_IceSickleProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;

        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.IceSickle;
        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            TrySpawnIceCrystals(projectile, target.Center);
        }
        private void TrySpawnIceCrystals(Projectile projectile, Vector2 targetPosition)
        {
            if (!Main.hardMode)
                return;

            if (Main.rand.NextFloat() > 0.15f) // 15% шанс
                return;

            int count = Main.rand.Next(3, 5); // 3–4 кристалла

            for (int i = 0; i < count; i++)
            {
                float offsetX = Main.rand.NextFloat(-50f, 50f);
                Vector2 spawnPos = new Vector2(targetPosition.X + offsetX, targetPosition.Y - 600f); 

                Vector2 direction = targetPosition - spawnPos;
                direction.Normalize();
                direction *= Main.rand.NextFloat(15f, 26f); 

                int proj = Projectile.NewProjectile(
                    projectile.GetSource_FromThis(),
                    spawnPos,
                    direction,
                    ModContent.ProjectileType<IceCrystals>(),
                    (int)(projectile.damage * 0.25f),
                    projectile.knockBack,
                    projectile.owner
                );

                Main.projectile[proj].ai[0] = 1;
            }
        }
    }

    public class IceSickleDropFromNPC : GlobalNPC
    {
        public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
        {
            // Убираем ванильный дроп
            if (npc.type == NPCID.ArmoredViking ||
                npc.type == NPCID.IceTortoise ||
                npc.type == NPCID.IcyMerman ||
                npc.type == NPCID.IceElemental)
            {
                npcLoot.RemoveWhere(rule =>
                    rule is CommonDrop drop
                    && drop.itemId == ItemID.IceSickle
                );
            }
            if (npc.type == NPCID.UndeadViking)
            {
                npcLoot.Add(
                    ItemDropRule.Common(ItemID.IceSickle, chanceDenominator: 200)
                );
            }
        }
    }
}
