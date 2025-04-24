using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using WoS.Content.ModPlayers;

namespace WoS.Content.Buffs.SupportBuffs
{
    public class BuffPureDesire : ModBuff
    {
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = false;
            Main.buffNoTimeDisplay[Type] = false;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().PureDesire = true;
            for (int i = 0; i < 4; i++)
            {
                Dust dust = Dust.NewDustDirect(player.Center, player.width, player.height, DustID.RainbowTorch, Scale: 0.45f);
                dust.noGravity = true;

                Vector2 vector = Main.rand.NextVector2Unit() * Main.rand.NextFloat(2.9f, 4.1f);
                dust.velocity = vector;
                dust.position = player.Center - (vector * 24f);
            }
        }
    }
}
