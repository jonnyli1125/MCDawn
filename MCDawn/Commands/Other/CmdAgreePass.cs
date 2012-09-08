using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCDawn
{
    class CmdAgreePass : Command
    {
        public override string name { get { return "agreepass"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdAgreePass() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length > 1) { p.SendMessage("Agree password may not contain spaces!"); return; }
            if (message.Length > 0)
            {
                Server.agreePass = message;
                if (p != null) { p.SendMessage("Agree Password set as: &b" + message); }
                Server.s.Log("Agree Password set as: " + message);
            }
            else
            {
                Server.agreePass = message;
                if (p != null) { p.SendMessage("Agree Password turned off."); }
                Server.s.Log("Agree Password turned off.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/agreepass <password> - Set the password for /agree (Leave password blank to turn off).");
        }
    }
}
