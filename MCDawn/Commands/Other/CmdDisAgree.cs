using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCDawn
{
    class CmdDisAgree : Command
    {
        public override string name { get { return "disagree"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdDisAgree() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable from Console."); return; }
            if (!Server.agreeToRules) { p.SendMessage("Agree To Rules is currently off!"); return; }
            if (Server.agreedToRules.Contains(p.name)) { Server.agreedToRules.Remove(p.name); }

            Player.GlobalMessageOps("To Ops: " + p.color + p.name + Server.DefaultColor + " disagreed to the rules!");
            Server.s.Log(p.name + " disagreed to the rules.");
            p.Kick("Not such a smart idea to disagree to the rules, eh?"); 
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/disagree - Disagree to the /rules.");
        }
    }
}
