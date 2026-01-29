using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModSets;

namespace WoS.Content.Core.Resocances
{
    public class SteamBreachProjectiles : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile entity, bool lateInstantiation)
        {
            return WoSProjectilesSets.ProjectileWetStatus[entity.type];
        }

        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Wet, 300);
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
        public override void OnHitPlayer(Projectile projectile, Player target, Player.HurtInfo info)
        {
            target.AddBuff(BuffID.Wet, 300);
            base.OnHitPlayer(projectile, target, info);
        }
    }
}
