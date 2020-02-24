using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer_Injectable.Hacks.Cheats
{
    public class PlaceAnywhere : PatchCheat
    {
        public PlaceAnywhere():base("Place Anywhere", (ulong)MommyMem.gameAssembly + 0x46B580, MommyMem.ceByte2Bytes("55 48 8b ec b8 01 00 00 00 5d c3"))
        {

        }
    }
}
