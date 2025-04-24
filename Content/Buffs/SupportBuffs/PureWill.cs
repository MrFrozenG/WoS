using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.ModPlayers;

namespace WoS.Content.Buffs.SupportBuffs
{
    public class BuffPureWill : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().PureWill = true;
            for (int i = 0; i < 3; i++)
            {
                Dust dust = Dust.NewDustDirect(player.Center, player.width, player.height, DustID.RainbowTorch, Scale: 0.5f);
                dust.noGravity = true;

                Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(3.0f, 6.0f);
                dust.velocity = vector;
                dust.position = player.Center - (vector * 24f);
            }
        }
    }
}
