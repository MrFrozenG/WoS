using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.Tiles.CraftPlaces;

namespace WoS.Content.Core
{
    public class Recipes : ModSystem
    {
        public static int anySilverBar;
        public static int anyCobaltBar;
        public static int anyMythrilBar;
        public static int anyAdamantiteBar;
        public static int anyEvilBar;
        public static int anyTombstone;
        public static int anyWorkbench;
        public static int anyMythrilAnvil;
        public static int anyStaff;
        public static int anyEvilHardmodeMaterial;
        public override void Unload()
        {
            anySilverBar = 0;
            anyCobaltBar = 0;
            anyMythrilBar = 0;
            anyAdamantiteBar = 0;
            anyEvilBar = 0;
            anyTombstone = 0;
            anyWorkbench = 0;
            anyMythrilAnvil = 0;
            anyStaff = 0;
            anyEvilHardmodeMaterial = 0;
        }
        public static Condition recipeConditionBossEyeOfCthulhu = new Condition("Mods.WoS.General.Recipes.ConditionBossEOC", () => NPC.downedBoss1);
        public static Condition recipeConditionBossWorldEvil = new Condition("Mods.WoS.General.Recipes.ConditionBossWEB", () => NPC.downedBoss2);
        public static Condition recipeConditionBossSlimeKing = new Condition("Mods.WoS.General.Recipes.ConditionBossSK", () => NPC.downedSlimeKing);
        public static Condition recipeConditionBossGolem = new Condition("Mods.WoS.General.Recipes.ConditionBossGolem", () => NPC.downedGolemBoss);

        public override void AddRecipeGroups()
        {
            // Staffs
            RecipeGroup staffsGroup = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Language.GetTextValue("Mods.WoS.General.Recipes.RecipeGroupStaffs")}",
                ItemID.AmethystStaff,
                ItemID.TopazStaff,
                ItemID.SapphireStaff,
                ItemID.EmeraldStaff,
                ItemID.AmberStaff,
                ItemID.RubyStaff,
                ItemID.DiamondStaff,
                ItemID.ThunderStaff
            );
            anyStaff = RecipeGroup.RegisterGroup("WoS:AnyStaff", staffsGroup);

            // Evil materials (Ichor, Cursed Flames)
            RecipeGroup HMEvilMaterial = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Ichor) + " / " + Lang.GetItemNameValue(ItemID.CursedFlame)}",
                ItemID.Ichor,
                ItemID.CursedFlame
            );
            anyEvilHardmodeMaterial = RecipeGroup.RegisterGroup("WoS:anyEvilHardmodeMaterial", HMEvilMaterial);

            // Silver Bar group
            RecipeGroup silverGroup = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.SilverBar)}",
                ItemID.SilverBar,
                ItemID.TungstenBar
            );
            anySilverBar = RecipeGroup.RegisterGroup("WoS:AnySilverBar", silverGroup);

            // Evil Bar group
            RecipeGroup evilGroup = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.DemoniteBar)}",
                ItemID.DemoniteBar,
                ItemID.CrimtaneBar
            );
            anyEvilBar = RecipeGroup.RegisterGroup("WoS:AnyEvilBar", evilGroup);

            // Tombstone group
            RecipeGroup tombstoneGroup = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.Tombstone)}",
                ItemID.CrossGraveMarker,
                ItemID.GraveMarker,
                ItemID.Gravestone,
                ItemID.Obelisk,
                ItemID.RichGravestone1,
                ItemID.RichGravestone2,
                ItemID.RichGravestone3,
                ItemID.RichGravestone4,
                ItemID.RichGravestone5
            );
            anyTombstone = RecipeGroup.RegisterGroup("WoS:AnyTombstone", tombstoneGroup);

            //Workbenches group
            int[] workbenchIDs = ItemID.Sets.Workbenches.Select(i => (int)i).ToArray();
            RecipeGroup workbencheGroup = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.WorkBench)}",
                workbenchIDs
            );
            anyWorkbench = RecipeGroup.RegisterGroup("WoS:AnyWorkbench", workbencheGroup);

            // Evil Bar group
            RecipeGroup anyMythrilAnvils = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.MythrilAnvil)}",
                ItemID.MythrilAnvil,
                ItemID.OrichalcumAnvil
            );
            anyMythrilAnvil = RecipeGroup.RegisterGroup("WoS:AnyMythrilAnvils", anyMythrilAnvils);

            //Hardmode Metal bars
            RecipeGroup anyCobaltBars = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.CobaltBar)}",
                ItemID.CobaltBar,
                ItemID.PalladiumBar
            );
            anyCobaltBar = RecipeGroup.RegisterGroup("WoS:AnyCobaltBars", anyCobaltBars);

            RecipeGroup anyMythrilBars = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.MythrilBar)}",
                ItemID.MythrilBar,
                ItemID.OrichalcumBar
            );
            anyMythrilBar = RecipeGroup.RegisterGroup("WoS:AnyMythrilBars", anyMythrilBars);

            RecipeGroup anyAdamantiteBars = new RecipeGroup(
                () => $"{Language.GetTextValue("LegacyMisc.37")} {Lang.GetItemNameValue(ItemID.AdamantiteBar)}",
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar
            );
            anyAdamantiteBar = RecipeGroup.RegisterGroup("WoS:AnyAdamantiteBars", anyAdamantiteBars);
        }
        public override void PostAddRecipes()
        {
            OverridedCraftPlace();
            RemoveRecipesVanila();
        }
        public void RemoveRecipesVanila()
        {
            int[] restrictedItems = {
                ItemID.LightDisc,
                ItemID.MagicalHarp,
                ItemID.FairyBell,
                ItemID.TrueNightsEdge,
                ItemID.RainbowRod,
                ItemID.SkyFracture,
                ItemID.MeteorStaff,
                ItemID.SpiritFlame,
                ItemID.OnyxBlaster
            };

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe.Mod != Mod && restrictedItems.Any(recipe.HasResult))
                {
                    recipe.DisableRecipe();
                }
            }
        }
        public void OverridedCraftPlace()
        {
            // Список предметов, которые нужно переписать
            int[] restrictedItems = new int[]
            {
                ItemID.VenomStaff
            };

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe == null || recipe.createItem == null)
                    continue;

                // Если результат рецепта находится в списке
                if (restrictedItems.Contains(recipe.createItem.type))
                {
                    // Заменяем все станции на вашу
                    recipe.requiredTile = new List<int> { ModContent.TileType<Celestial_Altar>() };
                }
            }
        }
        public override void AddRecipes()
        {
            Recipe.Create(ItemID.LaserRifle)
            .AddIngredient(ItemID.SoulofNight, 5)
            .AddIngredient(ItemID.SoulofLight, 5)
            .AddIngredient(ItemID.SpaceGun)
            .AddRecipeGroup(anyCobaltBar, 9)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.CrystalSerpent)
            .AddIngredient(ItemID.CrystalShard, 25)
            .AddIngredient(ItemID.SoulofLight, 10)
            .AddRecipeGroup(anyMythrilBar, 9)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.PoisonStaff)
            .AddIngredient(ItemID.SoulofNight, 15)
            .AddIngredient(ItemID.SpiderFang, 6)
            .AddRecipeGroup(anyMythrilBar, 9)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.UnholyTrident)
            .AddIngredient(ItemID.SoulofNight, 20)
            .AddIngredient(ItemID.DemonScythe)
            .AddRecipeGroup(anyAdamantiteBar, 9)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.IceRod)
            .AddIngredient(ItemID.SoulofLight, 4)
            .AddIngredient(ItemID.SoulofNight, 4)
            .AddIngredient(ItemID.FrostCore)
            .AddRecipeGroup(anyCobaltBar, 5)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();

            //Golem Defeated
            Recipe.Create(ItemID.SpectreStaff)
            .AddIngredient(ItemID.SpectreBar, 15)
            .AddIngredient(ItemID.SoulofSight, 5)
            .AddRecipeGroup(anyAdamantiteBar, 5)
            .AddCondition(recipeConditionBossGolem)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.InfernoFork)
            .AddIngredient(ItemID.HellstoneBar, 15)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddRecipeGroup(anyAdamantiteBar, 5)
            .AddCondition(recipeConditionBossGolem)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
            Recipe.Create(ItemID.ShadowbeamStaff)
            .AddIngredient(ItemID.SoulofNight, 15)
            .AddIngredient(ItemID.SoulofFright, 5)
            .AddRecipeGroup(anyAdamantiteBar, 15)
            .AddRecipeGroup(anyStaff)
            .AddCondition(recipeConditionBossGolem)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();

            //New Recipes for Vanila Items
            Recipe.Create(ItemID.LightDisc)
            .AddIngredient(ItemID.CrystalShard, 10)
            .AddIngredient(ItemID.SoulofLight, 10)
            .AddRecipeGroup(anyCobaltBar, 20)
            .AddTile(TileID.Anvils)
            .Register();

            Recipe.Create(ItemID.MagicalHarp)
            .AddIngredient(ItemID.Harp)
            .AddIngredient(ItemID.CrystalShard, 25)
            .AddIngredient(ItemID.SoulofLight, 8)
            .AddIngredient(ItemID.SoulofNight, 8)
            .AddTile(TileID.Anvils)
            .Register();

            Recipe.Create(ItemID.FairyBell)
            .AddIngredient(ItemID.Bell)
            .AddIngredient(ItemID.PixieDust, 30)
            .AddIngredient(ItemID.SoulofLight, 12)
            .AddIngredient(ItemID.CrystalShard, 15)
            .AddTile(TileID.Anvils)
            .Register();

            Recipe.Create(ItemID.TrueNightsEdge)
            .AddIngredient(ItemID.NightsEdge)
            .AddIngredient(ItemID.SoulofSight, 25)
            .AddIngredient(ItemID.SoulofMight, 25)
            .AddIngredient(ItemID.SoulofFright, 25)
            .AddRecipeGroup(anyEvilHardmodeMaterial, 10)
            .AddTile(ModContent.TileType<Celestial_Altar>()) 
            .Register();

            Recipe.Create(ItemID.RainbowRod)
            .AddIngredient(ItemID.CrystalShard, 10)
            .AddIngredient(ItemID.UnicornHorn, 2)
            .AddIngredient(ItemID.PixieDust, 15)
            .AddIngredient(ItemID.SoulofLight, 8)
            .AddTile(TileID.Anvils)
            .Register();

            Recipe.Create(ItemID.SkyFracture)
            .AddRecipeGroup(anyCobaltBar, 12)
            .AddIngredient(ItemID.LightShard, 2)
            .AddIngredient(ItemID.SoulofLight, 16)
            .AddTile(TileID.Anvils)
            .Register();

            Recipe.Create(ItemID.MeteorStaff)
            .AddIngredient(ItemID.MeteoriteBar, 20)
            .AddIngredient(ItemID.PixieDust, 10)
            .AddIngredient(ItemID.SoulofLight, 15)
            .AddIngredient(ItemID.SoulofFlight, 15)
            .AddTile(TileID.Mythril)
            .Register();

            Recipe.Create(ItemID.SpiritFlame)
            .AddIngredient(ItemID.DjinnLamp)
            .AddIngredient(ItemID.DarkShard, 2) 
            .AddIngredient(ItemID.SoulofNight, 12)
            .AddIngredient(ItemID.SoulofSight, 15)
            .AddTile(ModContent.TileType<Celestial_Altar>()) 
            .Register();

            Recipe.Create(ItemID.OnyxBlaster)
            .AddIngredient(ItemID.Shotgun)
            .AddIngredient(ItemID.DarkShard, 2)
            .AddIngredient(ItemID.SoulofNight, 12)
            .AddIngredient(ItemID.SoulofMight, 15)
            .AddTile(ModContent.TileType<Celestial_Altar>())
            .Register();
        }
    }
}
