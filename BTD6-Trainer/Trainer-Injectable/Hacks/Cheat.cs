using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Trainer_Injectable.Hacks
{
    public abstract class Cheat
    {
        public string name;
        public Cheat(string name)
        {
            this.name = name;
            CheatManager.manager.cheats.Add(this);
        }

        public void addToggleToWindow(int x, int y)
        {
            CheckBox toggle = new CheckBox();
            toggle.CheckedChanged += (object sender, EventArgs args) =>
            {
                if (toggle.Checked)
                    onEnable();
                else
                    onDisable();
            };
            toggle.Font = new Font("Arial", 16, FontStyle.Regular);
            toggle.Text = name;
            toggle.Width = 400;
            toggle.Visible = true;
            Trainer.mainWindow.Controls.Add(toggle);
        }

        public virtual void onEnable()
        {

        }
        public virtual void onDisable()
        {

        }
    }
}
