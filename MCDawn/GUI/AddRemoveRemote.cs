using System;
using System.Windows.Forms;

namespace MCDawn.Gui
{
    public partial class AddRemoveRemote : Form
    {
        public AddRemoveRemote()
        {
            InitializeComponent();
        }

        private void AddRemoveRemote_Load(object sender, EventArgs e)
        {
            try
            {
                if (Window.thisWindow.addrcuser) { btnGo.Text = "Add User"; }
                else { txtPass.Visible = false; label2.Visible = false; btnGo.Text = "Remove User"; }
            }
            catch { }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (Window.thisWindow.addrcuser)
                    RemoteServer.AddUser(txtUsername.Text, txtPass.Text);
                else
                    RemoteServer.RemoveUser(txtUsername.Text);
                RemoteServer.LoadUsers();
                Hide(); Dispose();
            }
            catch { }
        }
    }
}
