using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Core.Changes.Weapons
{
    public class VC_Phasesabers : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod)
        {
            return false; // ModContent.GetInstance<MainConfig>().PhasesabersMeltingDown;
        }
        public override bool AppliesToEntity(Item i, bool lateInstantiation)
        {
            switch (i.type)
            {
                case ItemID.WhitePhasesaber:
                case ItemID.YellowPhasesaber:
                case ItemID.GreenPhasesaber:
                case ItemID.RedPhasesaber:
                case ItemID.OrangePhasesaber:
                case ItemID.PurplePhasesaber:
                case ItemID.BluePhasesaber:
                    return true;

                default:
                    return false;
            }
        }
        public override void SetDefaults(Item item)
        {
            item.damage += 15;
            item.scale *= 1.3f;
            item.ArmorPenetration = 25;
            item.StatsModifiedBy.Add(Mod);
        }
        public override void ModifyHitNPC(Item item, Player player, NPC target, ref NPC.HitModifiers modifiers)
        {
            if (target.defense > 0)
            {
                modifiers.ArmorPenetration += (int)(target.defense * 0.35f);
            }
        }
    }
}
