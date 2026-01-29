using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Buffs.Support
{
    public class TwilightPhantomSigil : ModBuff
    {
        public const float DamageSpeedBonus = 1.5f;
        public override void SetStaticDefaults()
        {
            Main.buffNoSave[Type] = true;
            Main.persistentBuff[Type] = true;
        }
        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<PlayerBuff>().TwilightPhantomSigil = true;
        }
        public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
        {
            Player player = Main.LocalPlayer;
            var modPlayer = player.GetModPlayer<PlayerBuff>();

            int sp = Math.Min(modPlayer.TwilightPhantomSigilSP, 12);
            float bonusPercent = sp * DamageSpeedBonus;

            tip = Language.GetTextValue(
                "Mods.WoS.Buffs.TwilightPhantomSigil.Description",
                bonusPercent
            );
        }
    }
}
