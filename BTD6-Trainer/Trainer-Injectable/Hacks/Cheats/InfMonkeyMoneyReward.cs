using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trainer_Injectable.Hacks.Cheats
{
    public class InfMonkeyMoneyReward : Cheat
    {
        ulong address;
        byte[] patch;
        byte[] original;
        public InfMonkeyMoneyReward() : base("Infinite MM Reward")
        {
            address = (ulong)MommyMem.gameAssembly + 0x48E110;
            patch = MommyMem.ceByte2Bytes("55 48 8b ec b8 FF FF FF 7F 5d c3");
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
