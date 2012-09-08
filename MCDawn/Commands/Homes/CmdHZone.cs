// Written by jonnyli1125 for MCDawn
using System;
using System.Collections.Generic;
using System.IO;

namespace MCDawn
{
    public class CmdHZone : Command
    {
        public override string name { get { return "hzone"; } }
        public override string[] aliases { get { return new string[] { "hz" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHZone() { }

        public override void Use(Player p, string message)
        {
            string prefix = Server.HomePrefix;
            if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hzone"); return; }
            if (!Server.useMySQL) { p.SendMessage("MySQL has not been configured! Please configure MySQL to use Zones!"); return; }
            if (message == "") { Command.all.Find("zone").Use(p, ""); return; }
            switch (message.Split(' ')[0].ToLower())
            {
                case "add":
                    Command.all.Find("zoneall").Use(p, message.Split(' ')[1]);
                    break;
                case "del":
                    Command.all.Find("zone").Use(p, "del");
                    break;
                default: Help(p); break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hzone [add] [name] - Adds [name] to the zone on your home.");
            Player.SendMessage(p, "/hzone [add] [rank] - Adds [rank] to the zone on your home.");
            Player.SendMessage(p, "/hzone [del] - Deletes all zones where clicked.");
            Player.SendMessage(p, "/hzone [del] [all] - Deletes all the zones on your home.");
        }
    }
}