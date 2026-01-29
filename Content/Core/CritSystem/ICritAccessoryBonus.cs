using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace WoS.Content.Core.CritSystem
{
    public interface ICritAccessoryBonus
    {
        float GetCritDamageBonus(Player player);
    }
}
