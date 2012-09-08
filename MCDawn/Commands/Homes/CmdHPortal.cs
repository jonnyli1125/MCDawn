// Written by jonnyli1125 for MCDawn
using System;
using System.IO;

namespace MCDawn
{
    public class CmdHPortal : Command
    {
        public override string name { get { return "hportal"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHPortal() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hportal"); return; }
                Command.all.Find("portal").Use(p, message);
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hportal [orange/blue/air/water/lava/home] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/hportal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/hportal show - Shows portals, green = in, red = out.");
            Player.SendMessage(p, "Using /hportal home [multi] creates a portal that brings players to their player home.");
        }
    }
}