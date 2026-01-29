
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using System.Collections.Generic;
using Terraria.Localization;
using WoS.Content.Buffs.Weakness;
using WoS.Content.Core.Resonances;

namespace WoS.Content.Core.Changes.Weapons
{
    public class G_Gungnir : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Item item, bool lateInstantiation)
        {
            return item.type == ItemID.Gungnir;
        }
        public override void SetDefaults(Item item)
        {
            item.StatsModifiedBy.Add(Mod);
        }
    }
    public class G_GungnirSpear : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(Projectile projectile, bool lateInstantiation)
        {
            return projectile.type == ProjectileID.Gungnir;

        }
        public override void OnHitNPC(Projectile projectile, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.GetGlobalNPC<ShockResonanceNPCs>().ShockCharge += 20;
            base.OnHitNPC(projectile, target, hit, damageDone);
        }
    }
}
