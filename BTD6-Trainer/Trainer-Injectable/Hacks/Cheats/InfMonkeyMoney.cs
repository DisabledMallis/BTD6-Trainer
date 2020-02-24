using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer_Injectable.Hacks.Cheats
{
    public class InfMonkeyMoney : PatchCheat
    {
        public InfMonkeyMoney() : base("Infinite MM", (ulong)MommyMem.gameAssembly + 0x2928D0, MommyMem.ceByte2Bytes("55 48 89 e5 f3 0f 10 05 02 00 00 00 5d c3 FF FF 7F 7F cc"))
        {
        }
    }
}
