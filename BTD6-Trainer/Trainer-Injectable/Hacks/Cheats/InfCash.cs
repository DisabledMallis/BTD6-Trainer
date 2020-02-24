using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trainer_Injectable.Hacks.Cheats
{
    public class InfCash : Cheat
    {
        ulong address;
        byte[] patch;
        byte[] original;
        public InfCash():base("Infinite Cash")
        {
            address = (ulong)MommyMem.gameAssembly + 0x3EF310;
            patch = MommyMem.ceByte2Bytes("ff f5 48 89 e5 f2 0f 10 05 02 00 00 00 5d c3 FF FF FF FF FF FF EF 7F cc");
            MommyMem.unprotectMemory(address, (ulong)patch.Length);
            original = MommyMem.readAob(address, (ulong)patch.Length);
        }

        public override void onEnable()
        {
            MommyMem.writeAob(address, patch);
        }
        public override void onDisable()
        {
            MommyMem.writeAob(address, original);
        }
    }
}
