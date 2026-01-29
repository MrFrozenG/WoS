using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Core.Globals
{
    public class GI_QuickUseSummon : GlobalItem
    {
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            // Применяем только к StaffMinion
            return ItemID.Sets.StaffMinionSlotsRequired[item.type] > 0;
        }

        private float GetQuickUseMultiplier(Item item, Player player, bool isUseTime)
        {
            var modPlayer = player.GetModPlayer<MainPlayer>();
            if (modPlayer.QuickUseSummon == null)
                return 1f;

            int freeMinions = player.maxMinions - player.numMinions;
            if (freeMinions <= 0)
                return 1f;

            if (isUseTime)
            {
                // UseTime сокращаем пропорционально свободным слотам
                // Например, анимация 20, слотов 4 → UseTime = 20 / 4 = 5
                return 1f / freeMinions;
            }
            else
            {
                // UseAnimation оставляем без изменений, чтобы анимация была полной
                return 1f;
            }
        }

        public override float UseTimeMultiplier(Item item, Player player)
        {
            return GetQuickUseMultiplier(item, player, true);
        }

        public override float UseAnimationMultiplier(Item item, Player player)
        {
            return GetQuickUseMultiplier(item, player, false);
        }
    }
}