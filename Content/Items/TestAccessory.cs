using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Items
{
    class TestAccessory : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.accessory = true;
            Item.rare = ItemRarityID.Master;
            Item.value = 0;
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            Player player = Main.LocalPlayer;

            tooltips.Add(new TooltipLine(Mod, "Separator", "──────── Stats ────────"));

            // Damage
            tooltips.Add(new TooltipLine(Mod, "MeleeDmg",
                $"Melee damage: {(int)(player.GetDamage(DamageClass.Melee).Additive * 100)}%"));

            tooltips.Add(new TooltipLine(Mod, "RangedDmg",
                $"Ranged damage: {(int)(player.GetDamage(DamageClass.Ranged).Additive * 100)}%"));

            tooltips.Add(new TooltipLine(Mod, "MagicDmg",
                $"Magic damage: {(int)(player.GetDamage(DamageClass.Magic).Additive * 100)}%"));

            tooltips.Add(new TooltipLine(Mod, "SummonDmg",
                $"Summon damage: {(int)(player.GetDamage(DamageClass.Summon).Additive * 100)}%"));

            // Crit
            tooltips.Add(new TooltipLine(Mod, "Separator2", "──────── Crit ────────"));

            tooltips.Add(new TooltipLine(Mod, "MeleeCrit",
                $"Melee crit: {player.GetCritChance(DamageClass.Melee)}%"));

            tooltips.Add(new TooltipLine(Mod, "RangedCrit",
                $"Ranged crit: {player.GetCritChance(DamageClass.Ranged)}%"));

            tooltips.Add(new TooltipLine(Mod, "MagicCrit",
                $"Magic crit: {player.GetCritChance(DamageClass.Magic)}%"));

            // Minions
            tooltips.Add(new TooltipLine(Mod, "Separator3", "──────── Summons ────────"));

            tooltips.Add(new TooltipLine(Mod, "Minions",
                $"Minions: {player.numMinions} / {player.maxMinions}"));

            List<Projectile> list = new List<Projectile>();
            for (int i = 0; i < 1000; i++)
            {
                if (Main.projectile[i].WipableTurret)
                    list.Add(Main.projectile[i]);
            }
            int currentTurrets = CountPlayerTurrets(player);
            tooltips.Add(new TooltipLine(Mod, "Turrets",
                $"Turrets: {currentTurrets} / {player.maxTurrets}"));
        }
        public static int CountPlayerTurrets(Player player)
        {
            int count = 0;

            for (int i = 0; i < Main.maxProjectiles; i++)
            {
                Projectile proj = Main.projectile[i];

                if (!proj.active)
                    continue;

                if (proj.owner != player.whoAmI)
                    continue;

                if (proj.WipableTurret)
                    count++;
            }

            return count;
        }
    }
}
