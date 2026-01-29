using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.Misc
{
    public class ModSummonItemEditRecipes : ModSystem
    {
       
        bool ThoriumActive = ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
        bool GensokyoActive = ModLoader.TryGetMod("Gensokyo", out Mod gensokyo);
        public override void PostAddRecipes()
        {
            var cfg = ModContent.GetInstance<MainConfig>();

            if (cfg.ConsumabledBossItemsThorium && ThoriumActive)
            {
                HandleThoriumRecipes();
            }

            if (cfg.ConsumabledBossItemsGensokyo && GensokyoActive)
            {
                HandleGensokyoRecipes();
            }
        }
        private void HandleGensokyoRecipes()
        {
            var cfg = ModContent.GetInstance<MainConfig>();
            if (!cfg.ConsumabledBossItemsGensokyo)
                return;

            Mod gensokyo = ModLoader.GetMod("Gensokyo");
            if (gensokyo == null)
                return;

            // Все спавнеры — TryFind заранее, как в Thorium
            gensokyo.TryFind("LilyWhiteSpawner", out ModItem lilyWhite);
            gensokyo.TryFind("RumiaSpawner", out ModItem rumia);
            gensokyo.TryFind("EternityLarvaSpawner", out ModItem eternity);
            gensokyo.TryFind("NazrinSpawner", out ModItem nazrin);
            gensokyo.TryFind("HinaKagiyamaSpawner", out ModItem hina);
            gensokyo.TryFind("SekibankiSpawner", out ModItem sekibanki);
            gensokyo.TryFind("SeiranSpawner", out ModItem seiran);
            gensokyo.TryFind("NitoriKawashiroSpawner", out ModItem nitori);
            gensokyo.TryFind("MedicineMelancholySpawner", out ModItem medicine);
            gensokyo.TryFind("CirnoSpawner", out ModItem cirno);
            gensokyo.TryFind("MinamitsuMurasaSpawner", out ModItem murasa);
            gensokyo.TryFind("AliceMargatroidSpawner", out ModItem alice);
            gensokyo.TryFind("SakuyaIzayoiSpawner", out ModItem sakuya);
            gensokyo.TryFind("SeijaKijinSpawner", out ModItem seija);
            gensokyo.TryFind("MayumiJoutouguuSpawner", out ModItem mayumi);
            gensokyo.TryFind("ToyosatomimiNoMikoSpawner", out ModItem miko);
            gensokyo.TryFind("KaguyaHouraisanSpawner", out ModItem kaguya);
            gensokyo.TryFind("UtsuhoReiujiSpawner", out ModItem utsuho);
            gensokyo.TryFind("TenshiHinanawiSpawner", out ModItem tenshi);
            gensokyo.TryFind("KoishiKomeijiSpawner", out ModItem koishi);
            gensokyo.TryFind("MystiaLoreleiSpawner", out ModItem mystia);

            // Массив всех найденных спавнеров
            ModItem[] spawners =
            {
            lilyWhite, rumia, eternity, nazrin, hina, sekibanki, seiran, nitori,
            medicine, cirno, murasa, alice, sakuya, seija, mayumi, miko,
            kaguya, utsuho, tenshi, koishi, mystia
            };

            // PointItem — проверяем заранее
            gensokyo.TryFind("PointItem", out ModItem pointItem);
            if (pointItem == null)
                return;

            int pointType = pointItem.Type;

            // Перебор рецептов
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe == null || recipe.createItem == null)
                    continue;

                string internalName = recipe.createItem.ModItem?.Name;
                if (internalName == null)
                    continue;

                // Проверяем, относится ли рецепт к одному из спавнеров
                foreach (var spawn in spawners)
                {
                    if (spawn == null)
                        continue;

                    if (recipe.createItem.type == spawn.Type)
                    {
                        if (recipe.TryGetIngredient(pointType, out Item ingredient))
                        {
                            ingredient.stack *= 10;
                        }
                    }
                }
            }
        }

        private void HandleThoriumRecipes()
        {
            var cfg = ModContent.GetInstance<MainConfig>();
            ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
            if (!cfg.ConsumabledBossItemsThorium)
            {
                return;
            }
            //Summon items
            thorium.TryFind("StormFlare", out ModItem stormFlare);
            thorium.TryFind("JellyfishResonator", out ModItem jellyfishResonator);
            thorium.TryFind("StriderTear", out ModItem striderTear);
            thorium.TryFind("VoidLens", out ModItem voidLens);
            thorium.TryFind("DoomSayersCoin", out ModItem doomSlayerCoin);
            thorium.TryFind("UnstableCore", out ModItem unstableCore);
            thorium.TryFind("AncientBlade", out ModItem ancientBlade);
            thorium.TryFind("StarCaller", out ModItem starCaller);
           //Resources
            thorium.TryFind("Talon", out ModItem talon);
            thorium.TryFind("GraniteEnergyCore", out ModItem graniteCore);
            thorium.TryFind("BronzeAlloyFragments", out ModItem bronze);
            thorium.TryFind("StrangeAlienTech", out ModItem alienTech);
            thorium.TryFind("SoulofPlight", out ModItem soulPlight);
            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];

                if (recipe == null || recipe.createItem == null)
                    continue;

                string internalName = recipe.createItem.ModItem?.Name;
                if (internalName == null)
                    continue;

                switch (internalName)
                {
                    case "StormFlare":
                        if (recipe.TryGetIngredient(ItemID.FallenStar, out Item star))
                            star.stack *= 3;

                        if (talon != null && recipe.TryGetIngredient(talon.Type, out Item talonDrop))
                            talonDrop.stack *= 4;
                        break;

                    case "UnstableCore":
                        if (recipe.TryGetIngredient(ItemID.Granite, out Item granite))
                            granite.stack *= 5;

                        if (graniteCore != null && recipe.TryGetIngredient(graniteCore.Type, out Item graniteEnergy))
                            graniteEnergy.stack *= 4;
                        break;

                    case "AncientBlade":
                        if (recipe.TryGetIngredient(ItemID.Marble, out Item marble))
                            marble.stack *= 5;

                        if (bronze != null && recipe.TryGetIngredient(bronze.Type, out Item bronzeFrag))
                            bronzeFrag.stack *= 4;
                        break;

                    case "StarCaller":
                        if (recipe.TryGetIngredient(ItemID.MeteoriteBar, out Item meteorite))
                            meteorite.stack *= 3;

                        if (alienTech != null && recipe.TryGetIngredient(alienTech.Type, out Item alienItem))
                            alienItem.stack *= 3;
                        break;

                    case "VoidLens":
                        if (soulPlight != null && recipe.TryGetIngredient(soulPlight.Type, out Item soulItem))
                            soulItem.stack *= 3;

                        if (recipe.TryGetIngredient(ItemID.Glass, out Item glass))
                            glass.stack *= 2;

                        // Добавление новой группы в рецепт
                        recipe.AddRecipeGroup(nameof(ItemID.SilverBar), 12);
                        break;

                    case "DoomSayersCoin":
                        foreach (var ingredient in recipe.requiredItem)
                        {
                            ingredient.stack *= 10;
                        }
                        break;
                }
            }
        }
    }
}
