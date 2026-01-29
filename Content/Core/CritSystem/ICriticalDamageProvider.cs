using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WoS.Content.Core.CritSystem
{
    public interface ICriticalDamageProvider
    {
        float BaseCriticalDamage { get; }
    }
}
