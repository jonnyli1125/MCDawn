// Written by jonnyli1125 for MCDawn
using System;

namespace MCDawn
{
    class CmdHSpawn : Command
    {

        public override string name { get { return "hspawn"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHSpawn() { }

        public override void Use(Player p, string message)
        {
            string prefix = Server.HomePrefix;
            if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hspawn"); return; }
            Command.all.Find("setspawn").Use(p, message);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hlockmap - Toggles Lock status for your Home map.");
        }
    }
}
