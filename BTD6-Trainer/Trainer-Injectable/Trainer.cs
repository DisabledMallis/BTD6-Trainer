using System.Windows.Forms;
using Trainer_Injectable.Hacks;

namespace Trainer_Injectable
{
    public partial class Trainer : Form
    {
        public static Trainer mainWindow;
        public Trainer()
        {
            mainWindow = this;
            InitializeComponent();
            CheatManager manager = new CheatManager();
            int x = 13;
            int y = 13;
            foreach(Cheat cheat in manager.cheats)
            {
                cheat.addToggleToWindow(x,y);
                y += 23;
            }
        }
    }
}
