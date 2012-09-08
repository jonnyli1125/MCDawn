using System;

namespace MCDawn
{
    public class CmdShutdown : Command
    {
        public override string name { get { return "shutdown"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdShutdown() { }

        public override void Use(Player p, string message) { MCDawn_.Gui.Program.ExitProgram(false); }
        public override void Help(Player p) { Player.SendMessage(p, "/shutdown - Shuts down the server."); }
    }
}
