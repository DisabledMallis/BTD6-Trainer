using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer_Injectable.Hacks.Cheats;

namespace Trainer_Injectable.Hacks
{
    public class CheatManager
    {
        public static CheatManager manager;
        public List<Cheat> cheats = new List<Cheat>();
        public CheatManager()
        {
            Console.WriteLine("Loading cheats...");
            manager = this;
            //Load cheats here
            new InfCash();
            new InfMonkeyMoneyReward();
            new InfMonkeyMoney();
            new PlaceAnywhere();
            Console.WriteLine("Cheats loaded!");
        }
    }
}
