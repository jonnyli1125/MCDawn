// Written by jonnyli1125 for MCDawn, very simple command.
// NO STEALING XD

using System;

namespace MCDawn
{
    public class CmdFacepalm : Command
    {
        public override string name { get { return "facepalm"; } }
        public override string[] aliases { get { return new string[] { "fp" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdFacepalm() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            if (p == null) { Player.GlobalMessage("The Console facepalmed."); }
            else { Player.GlobalMessage(p.color + p.prefix + p.name + Server.DefaultColor + " facepalmed."); }
        }

        public override void Help(Player p) { Player.SendMessage(p, "/facepalm - Feel free to use when you feel like facepalming. XD"); }
    }
}