using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.Misc
{
    public class ModSummonItemsEdit : GlobalItem
    {
        // Словарь содержит только моды, которые реально установлены
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            var cfg = ModContent.GetInstance<MainConfig>();
            bool ThoriumActive = ModLoader.TryGetMod("ThoriumMod", out Mod thorium);
            bool GensokyoActive = ModLoader.TryGetMod("gensokyo", out Mod gensokyo);

            if (cfg.ConsumabledBossItemsThorium && ThoriumActive)
            {
                thorium.TryFind("StormFlare", out ModItem A);
                thorium.TryFind("JellyfishResonator", out ModItem B);
                thorium.TryFind("StriderTear", out ModItem C);
                thorium.TryFind("VoidLens", out ModItem D);
                thorium.TryFind("DoomSayersCoin", out ModItem E);
                thorium.TryFind("UnstableCore", out ModItem F);
                thorium.TryFind("AncientBlade", out ModItem G);
                thorium.TryFind("StarCaller", out ModItem H);
                if ((item.type == A.Type) |
                (item.type == B.Type) |
                (item.type == C.Type) |
                (item.type == D.Type) |
                (item.type == E.Type) |
                (item.type == F.Type) |
                (item.type == G.Type) |
                (item.type == H.Type))
                    return true;
                else return false;
            }
       
            if (cfg.ConsumabledBossItemsGensokyo && GensokyoActive)
            {
                gensokyo.TryFind("LilyWhiteSpawner", out ModItem A);
                gensokyo.TryFind("RumiaSpawner", out ModItem B);
                gensokyo.TryFind("EternityLarvaSpawner", out ModItem C);
                gensokyo.TryFind("NazrinSpawner", out ModItem D);
                gensokyo.TryFind("HinaKagiyamaSpawner", out ModItem E);
                gensokyo.TryFind("SekibankiSpawner", out ModItem F);
                gensokyo.TryFind("SeiranSpawner", out ModItem G);
                gensokyo.TryFind("NitoriKawashiroSpawner", out ModItem H);
                gensokyo.TryFind("MedicineMelancholySpawner", out ModItem I);
                gensokyo.TryFind("CirnoSpawner", out ModItem J);
                gensokyo.TryFind("MinamitsuMurasaSpawner", out ModItem K);
                gensokyo.TryFind("AliceMargatroidSpawner", out ModItem L);
                gensokyo.TryFind("SakuyaIzayoiSpawner", out ModItem M);
                gensokyo.TryFind("SeijaKijinSpawner", out ModItem N);
                gensokyo.TryFind("MayumiJoutouguuSpawner", out ModItem O);
                gensokyo.TryFind("ToyosatomimiNoMikoSpawner", out ModItem P);
                gensokyo.TryFind("KaguyaHouraisanSpawner", out ModItem Q);
                gensokyo.TryFind("UtsuhoReiujiSpawner", out ModItem R);
                gensokyo.TryFind("TenshiHinanawiSpawner", out ModItem S);
                gensokyo.TryFind("KoishiKomeijiSpawner", out ModItem T);
                gensokyo.TryFind("MystiaLoreleiSpawner", out ModItem U);
                if (
                    (item.type == A.Type) ||
                    (item.type == B.Type) ||
                    (item.type == C.Type) ||
                    (item.type == D.Type) ||
                    (item.type == E.Type) ||
                    (item.type == F.Type) ||
                    (item.type == G.Type) ||
                    (item.type == H.Type) ||
                    (item.type == I.Type) ||
                    (item.type == J.Type) ||
                    (item.type == K.Type) ||
                    (item.type == L.Type) ||
                    (item.type == M.Type) ||
                    (item.type == N.Type) ||
                    (item.type == O.Type) ||
                    (item.type == P.Type) ||
                    (item.type == Q.Type) ||
                    (item.type == R.Type) ||
                    (item.type == S.Type) ||
                    (item.type == T.Type) ||
                    (item.type == U.Type)
                        )
                    return true;
                else return false;
            }

            return false;
        }

        public override void SetDefaults(Item item)
        {
            if (!AppliesToEntity(item, true))
                return;

            item.consumable = false;
            item.useTime = 60;
            item.value = 0;
            item.shopCustomPrice = Item.buyPrice(gold: 1, silver: 50);
            item.shopSpecialCurrency = -1;
        }

        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            if (!AppliesToEntity(item, true))
                return;

            if (tooltips[1].Text != "Equipped in social slot")
            {
                tooltips[1].Text = "Not Consumable\n" + tooltips[1].Text;
            }
        }
    }
}
