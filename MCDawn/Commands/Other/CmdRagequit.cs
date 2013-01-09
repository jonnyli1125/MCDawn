using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn
{
    public class CmdRagequit : Command
    {
        public override string name { get { return "ragequit"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdRagequit() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            if (p == null)
            {
                Player.SendMessage(p, "Console can't ragequit, or else everyone gets disconnected!");
            }
            p.Kick("RAGEQUIT!!");

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ragequit - Ever gotten so angry you just wanted to...quit?");
        }
    }

}
