// Written by jonnyli1125 for MCDawn
using System;

namespace MCDawn
{
    class CmdHLockMap : Command
    {

        public override string name { get { return "hlockmap"; } }
        public override string[] aliases { get { return new string[] { "hlock" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHLockMap() { }

        public override void Use(Player p, string message)
        {
            string prefix = Server.HomePrefix;
            //if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hlockmap"); return; }
            Command.all.Find("lockmap").Use(p, prefix + p.name);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hlockmap - Toggles Lock status for your Home map.");
        }
    }
}
