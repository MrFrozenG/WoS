using System.Collections.Generic;

namespace WoS.Content.Core.Globals
{
    public class BreachEffectRegistry
    {
        public class BreachDebuffInfo
        {
            public int BaseDamage;
            public BreachDebuffInfo(int baseDamage)
            {
                BaseDamage = baseDamage;
            }
        }
        public static IEnumerable<KeyValuePair<int, BreachDebuffInfo>> GetAll()
    => registry;

        private static readonly Dictionary<int, BreachDebuffInfo> registry = new();

        public static void Register(int buffType, int baseDamage)
        {
            if (!registry.ContainsKey(buffType))
                registry[buffType] = new BreachDebuffInfo(baseDamage);
        }

        public static bool TryGet(int buffType, out BreachDebuffInfo info)
    => registry.TryGetValue(buffType, out info);

        //if (BreachEffectRegistry.TryGet(buffID, out var info)) { ... }
    }
}
