
using Terraria.DataStructures;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.Core.CritSystem
{
    public class CritGlobalProj : GlobalProjectile
    {
        public override bool InstancePerEntity => true;
        private float projBaseCrit;

        public override void OnSpawn(Projectile projectile, IEntitySource source)
        {
            // Если источник - Item, читаем бонус от оружия
            if (source is EntitySource_ItemUse itemSrc && itemSrc.Item.ModItem is ICriticalDamageProvider prov)
                projBaseCrit = prov.BaseCriticalDamage;
            // Если снаряд сам реализует ICriticalDamageProvider
            else if (projectile.ModProjectile is ICriticalDamageProvider provP)
                projBaseCrit = provP.BaseCriticalDamage;
        }

        public override void ModifyHitNPC(Projectile projectile, NPC target, ref NPC.HitModifiers modifiers)
        {
            float bonusPlayer = Main.player[projectile.owner].GetModPlayer<MainPlayer>().CritDamageBonus;
            modifiers.CritDamage += projBaseCrit + bonusPlayer;
        }
    }
}
