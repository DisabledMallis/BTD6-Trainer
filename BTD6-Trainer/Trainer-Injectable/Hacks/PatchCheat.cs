using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer_Injectable.Hacks
{
    public class PatchCheat : Cheat
    {
        ulong address;
        byte[] patch;
        byte[] original;
        public PatchCheat(string name, ulong address, byte[] patch) : base(name)
        {
            this.address = address;
            this.patch = patch;
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
