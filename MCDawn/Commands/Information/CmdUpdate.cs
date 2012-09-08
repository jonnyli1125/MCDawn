using System;
using System.Net;
using System.Threading;

namespace MCDawn
{
    public class CmdUpdate : Command
    {
        public override string name { get { return "update"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdUpdate() { }

        public override void Use(Player p, string message)
        {
            if (p == null || p.group.Permission > LevelPermission.AdvBuilder) MCDawn_.Gui.Program.UpdateCheck(false, p);
            else Player.SendMessage(p, "Ask an Operator to do it!");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/update - Updates the server if it's out of date");
        }
    }
}