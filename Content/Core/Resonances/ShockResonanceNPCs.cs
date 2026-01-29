using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace WoS.Content.Core.Resonances
{
    public class ShockResonanceNPCs : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public int ShockCharge = 0;
        public int ShockStunTimer = 0;
        public int LastHitPlayer = -1;
        public override bool PreAI(NPC npc)
        {
            if (ShockStunTimer > 0)
            {
                npc.velocity = Vector2.Zero;
                return false; // полностью блокируем AI
            }

            return true;
        }

        public override void PostAI(NPC npc)
        {
            if (ShockStunTimer > 0)
                ShockStunTimer--;
        }
    }
}
