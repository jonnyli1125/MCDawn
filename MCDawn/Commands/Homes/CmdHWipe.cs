// Written by jonnyli1125 for MCDawn
using System;
using System.IO;

namespace MCDawn
{
    public class CmdHWipe : Command
    {
        public override string name { get { return "hwipe"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHWipe() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hwipe"); return; }
                if (message == "") { p.SendMessage("Do you really want to wipe your home map? Type &a/hwipe confirm " + Server.DefaultColor + "to proceed."); return; }
                if (message.ToLower() == "confirm") { Command.all.Find("restore").Use(p, "wipe"); p.SendMessage("Home wiped."); return; }
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/wipe - Wipe your home map, use with caution :D");
        }
    }
}