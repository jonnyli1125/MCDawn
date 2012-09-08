using System;
using System.Windows.Forms;

namespace MCDawn
{
    public class CmdCallConsole : Command
    {
        public override string name { get { return "callconsole"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdCallConsole() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Server.s.Log("Command not usable from Console."); return; }
            bool orig = true;
            if (!Server.consoleSound) { Server.consoleSound = true; orig = false; }
            if (message == "") 
            {
                Server.s.Log(p.name + " is calling you.");
                try { MessageBox.Show(p.name + " is calling you.", "MCDawn Console", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification); }
                catch { }
            }
            else 
            { 
                Server.s.Log(p.name + " is calling you: " + message);
                try { MessageBox.Show(p.name + " is calling you: " + message, "MCDawn Console", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification); }
                catch { }
            }
            //if (MCDawn.Gui.Window.thisWindow.WindowState == FormWindowState.Minimized) { MCDawn.Gui.Window.thisWindow.WindowState = FormWindowState.Normal; }
            if (!orig) { Server.consoleSound = false; }
            Player.SendMessage(p, "Calling the Console...");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/callconsole <message> - Calls the Console.");
        }
    }
}