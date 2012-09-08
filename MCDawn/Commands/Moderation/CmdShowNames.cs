// Written by jonnyli1125 for use with MCDawn only.
using System;

namespace MCDawn
{
    public class CmdShowNames : Command
    {
        public override string name { get { return "shownames"; } }
        public override string[] aliases { get { return new string[] { "showrealnames", "showreal" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdShowNames() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (p.showRealNames)
            {
                p.showRealNames = false;
                Player.SendMessage(p, "All messages will now attempt to replace display names with real names.");
            }
            else
            {
                p.showRealNames = true;
                Player.SendMessage(p, "All messages will now use display names instead of real names.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/shownames - Show real names instead of display names.");
        }
    }
}