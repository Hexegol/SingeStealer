using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonkeyStealer.scripts
{
    public interface IStealer
    {
        public List<string> Steal();
    }
}
