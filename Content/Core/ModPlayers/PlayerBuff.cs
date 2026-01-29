using Terraria.ModLoader;

namespace WoS.Content.Core.ModPlayers
{
    public class PlayerBuff : ModPlayer
    {
        public bool DivineIntervention;
        public bool lifeDebt;
        public bool TwilightPhantomSigil;
        public int TwilightPhantomSigilSP;
        public override void ResetEffects()
        {
            lifeDebt = false;
            DivineIntervention = false;
        }

        public override void PostUpdateBuffs()
        {
            Player.statDefense *= 0.9f;
            Player.GetDamage(DamageClass.Generic) *= 0.75f;
        }

        public override void UpdateBadLifeRegen()
        {
            int BleedingDamage = Player.statLifeMax2 / 100 + 3;
            if (lifeDebt)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= BleedingDamage / 2;
            }
        }
    }

}
