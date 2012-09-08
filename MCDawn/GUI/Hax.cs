using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class Hax : Form
    {
        public Hax()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void btnBlue_Click(object sender, EventArgs e)
        {
            Window.thisWindow.ChangeColor(Color.DarkBlue, Color.Teal);
        }

        private void btnControl_Click(object sender, EventArgs e)
        {
            Window.thisWindow.ChangeColor(SystemColors.Control, Color.Black);
        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            Window.thisWindow.ChangeColor(Color.DarkRed, Color.Red);
        }
    }
}
