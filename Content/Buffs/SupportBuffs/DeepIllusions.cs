using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace WoS.Content.Buffs.SupportBuffs
{
    public class DebuffDeepIllusions : ModBuff
    {
        public static readonly int TagDamageBase = 8;
        public static readonly float TagDamageMultiplier = 0.33f;

        public override void SetStaticDefaults()
        {
            BuffID.Sets.IsATagBuff[Type] = true;
        }
    }

    public class DeepIllusionsNPC : GlobalNPC
    {
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (projectile.npcProj || projectile.trap || !projectile.IsMinionOrSentryRelated)
                return;

            var projTagMultiplier = ProjectileID.Sets.SummonTagDamageMultiplier[projectile.type];
            if (npc.HasBuff<DebuffDeepIllusions>())
            {
                modifiers.FlatBonusDamage += DebuffDeepIllusions.TagDamageBase;
                modifiers.ScalingBonusDamage += DebuffDeepIllusions.TagDamageMultiplier * projTagMultiplier;
                if (Main.rand.NextBool(10) && !npc.boss)
                {
                    npc.velocity = Vector2.Zero;
                }
                if (Main.rand.NextBool(20) && npc.boss)
                {
                    npc.velocity = Vector2.Zero;
                }
            }
        }
    }
}
