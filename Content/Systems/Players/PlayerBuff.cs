using Terraria.ModLoader;

namespace WoS.Content.ModPlayers
{
    internal class PlayerBuff : ModPlayer
    {
        public bool PureWill;
        public bool PureDesire;
        public bool Bleeding;

        public override void ResetEffects()
        {
            PureWill = false;
            PureDesire = false;
            Bleeding = false;
        }

        public override void PostUpdateBuffs()
        {
            if (PureWill)
            {
                Player.GetDamage(DamageClass.Generic) += 0.07f;
                Player.endurance += 0.04f;
                if (Player.statLife < Player.statLifeMax / 2) Player.GetDamage(DamageClass.Generic) += 0.1f;
            }
            if (PureDesire)
            {
                Player.moveSpeed += 0.1f;
                Player.manaCost -= 0.05f;
                Player.GetDamage(DamageClass.Magic) += 0.04f;
            }
        }

        public override void UpdateBadLifeRegen()
        {
            int BleedingDamage = Player.statLifeMax2 / 100 + 3;
            if (Bleeding)
            {
                if (Player.lifeRegen > 0)
                    Player.lifeRegen = 0;
                Player.lifeRegenTime = 0;
                Player.lifeRegen -= BleedingDamage / 2;
            }
        }
    }

}
