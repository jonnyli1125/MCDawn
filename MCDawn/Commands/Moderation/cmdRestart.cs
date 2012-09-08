using System;
using System.Threading;

namespace MCDawn
{
    public class CmdRestart : Command
    {
        public override string name { get { return "restart"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRestart() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            Player.GlobalMessage("Server Restart! Rejoin!");
            Thread.Sleep(750);
            MCDawn_.Gui.Program.ExitProgram(true);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restart - Restarts the server!  Use carefully!");
        }
    }
}
