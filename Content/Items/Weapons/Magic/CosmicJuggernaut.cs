using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.Systems.Globals;

namespace WoS.Content.Items.Weapons.Magic;

public class CosmicJuggernaut : ModItem
{
	public override void SetDefaults()
	{
		Item.DamageType = DamageClass.Magic;
		Item.damage = 25;
		Item.knockBack = 1.3f;
		Item.crit = 16;

		Item.width = 56;
		Item.height = 56;
		Item.scale = 1.2f;
		Item.useStyle = ItemUseStyleID.Swing;
		Item.useAnimation = 15;
		Item.useTime = 15;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
		Item.GetGlobalItem<ItemUseGlow>().glowTexture = ModContent.Request<Texture2D>("WoS/Content/Items/Weapons/Magic/CosmicJuggernaut_glow").Value;
		
		Item.rare = ItemRarityID.Green;
		Item.value = Item.sellPrice(0, 5, 35, 0);
	}
	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		int ManaRecover = player.statManaMax2 / 100;
		if (player.statMana < player.statManaMax2 / 2)
		{
			player.statMana += ManaRecover * Main.rand.Next(1,12);
		}
	}
}