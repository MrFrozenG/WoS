using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.Misc
{
    public class SummonItemsEditRecipes : ModSystem
    {
        public override void PostAddRecipes()
        {
            // Проверка на конфиг
            if (!ModContent.GetInstance<MainConfig>().ConsumabledBossItems)
                return;

            for (int i = 0; i < Recipe.numRecipes; i++)
            {
                Recipe recipe = Main.recipe[i];
                if (recipe == null || recipe.createItem == null)
                    continue;

                switch (recipe.createItem.type)
                {
                    // --- Подозрительный глаз ---
                    case ItemID.SuspiciousLookingEye:
                        if (recipe.TryGetIngredient(ItemID.Lens, out Item lens))
                            lens.stack *= 2;
                        recipe.AddIngredient(ItemID.FallenStar, 3);
                        break;

                    // --- Корона слизней ---
                    case ItemID.SlimeCrown:
                        if (recipe.TryGetIngredient(ItemID.Gel, out Item gel))
                            gel.stack = (int)(gel.stack * 1.5f);
                        break;

                    // --- Пища для червей ---
                    case ItemID.WormFood:
                        if (recipe.TryGetIngredient(ItemID.RottenChunk, out Item rottenChunk))
                            rottenChunk.stack = (int)(rottenChunk.stack * 1.5f);
                        if (recipe.TryGetIngredient(ItemID.VilePowder, out Item vilePowder))
                            vilePowder.stack *= 2;
                        break;

                    // --- Кровавый позвоночник ---
                    case ItemID.BloodySpine:
                        if (recipe.TryGetIngredient(ItemID.Vertebrae, out Item vertebrae))
                            vertebrae.stack = (int)(vertebrae.stack * 1.5f);
                        if (recipe.TryGetIngredient(ItemID.ViciousPowder, out Item viciousPowder))
                            viciousPowder.stack *= 2;
                        break;

                    // --- Пчелиная масса ---
                    case ItemID.Abeemination:
                        if (recipe.TryGetIngredient(ItemID.Stinger, out Item stinger))
                            stinger.stack *= 10;
                        break;

                    // --- Олене-штука ---
                    case ItemID.DeerThing:
                        if (recipe.TryGetIngredient(ItemID.FlinxFur, out Item flinxFur))
                            flinxFur.stack *= 2;
                        if (recipe.TryGetIngredient(ItemID.DemoniteOre, out Item demoniteOre))
                            demoniteOre.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.CrimtaneOre, out Item crimtaneOre))
                            crimtaneOre.stack *= 3;
                        break;

                    // --- Механический глаз (Близнецы) ---
                    case ItemID.MechanicalEye:
                        if (recipe.TryGetIngredient(ItemID.Lens, out Item lens2))
                            lens2.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.SoulofLight, out Item soulLight))
                            soulLight.stack *= 2;
                        if (recipe.TryGetIngredient(ItemID.IronBar, out Item ironEye))
                            ironEye.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.LeadBar, out Item leadEye))
                            leadEye.stack *= 3;
                        break;

                    // --- Механический червь (Уничтожитель) ---
                    case ItemID.MechanicalWorm:
                        if (recipe.TryGetIngredient(ItemID.RottenChunk, out Item rottenChunk2))
                            rottenChunk2.stack *= 4;
                        if (recipe.TryGetIngredient(ItemID.Vertebrae, out Item vertebrae2))
                            vertebrae2.stack *= 4;
                        if (recipe.TryGetIngredient(ItemID.IronBar, out Item ironWorm))
                            ironWorm.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.LeadBar, out Item leadWorm))
                            leadWorm.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.SoulofNight, out Item soulNight))
                            soulNight.stack *= 2;
                        break;

                    // --- Механический череп (Скелетрон Прайм) ---
                    case ItemID.MechanicalSkull:
                        if (recipe.TryGetIngredient(ItemID.Bone, out Item bone))
                            bone.stack *= 4;
                        if (recipe.TryGetIngredient(ItemID.IronBar, out Item ironSkull))
                            ironSkull.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.LeadBar, out Item leadSkull))
                            leadSkull.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.SoulofLight, out Item soulLightSkull))
                            soulLightSkull.stack *= 2;
                        if (recipe.TryGetIngredient(ItemID.SoulofNight, out Item soulNightSkull))
                            soulNightSkull.stack *= 2;
                        break;

                    // --- Медальон тыквенной луны ---
                    case ItemID.PumpkinMoonMedallion:
                        if (recipe.TryGetIngredient(ItemID.Pumpkin, out Item pumpkin))
                            pumpkin.stack *= 4;
                        if (recipe.TryGetIngredient(ItemID.Ectoplasm, out Item ectoplasmPumpkin))
                            ectoplasmPumpkin.stack *= 3;
                        break;

                    // --- Гадкий подарок ---
                    case ItemID.NaughtyPresent:
                        if (recipe.TryGetIngredient(ItemID.Silk, out Item silk))
                            silk.stack *= 2;
                        if (recipe.TryGetIngredient(ItemID.Ectoplasm, out Item ectoplasmPresent))
                            ectoplasmPresent.stack *= 3;
                        if (recipe.TryGetIngredient(ItemID.SoulofFright, out Item soulFright))
                            soulFright.stack *= 2;
                        break;
                }
            }
        }
    }
}

