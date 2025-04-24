using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using WoS.Content.Tiles.Banners;

namespace WoS.Content.Items.Placeable.Banners
{
    public abstract class BaseBanner : ModItem
    {
        protected abstract int Style { get; }

        public sealed override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 24;
            Item.maxStack = Item.CommonMaxStack;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 15;
            Item.useTime = 10;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.consumable = true;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.buyPrice(0, 0, 10, 0);
            Item.createTile = ModContent.TileType<BaseBannerTile>();
            Item.placeStyle = Style;
        }
    }
    public class BigMimicBloomingBanner : BaseBanner
    {
        protected override int Style => 0;
        public override void SetStaticDefaults()
        {
            ItemID.Sets.KillsToBanner[Type] = 25;
        }
    }
}
