// Written by jonnyli1125 for MCDawn
using System;

namespace MCDawn
{
    class CmdHKick : Command
    {

        public override string name { get { return "hkick"; } }
        public override string[] aliases { get { return new string[] { "hk" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHKick() { }

        public override void Use(Player p, string message)
        {
            string prefix = Server.HomePrefix;
            if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hkick"); return; }
            Player who = Player.Find(message);
            if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (who == p) { Player.SendMessage(p, "Why kick yourself?"); return; }
            if (who.group.Permission >= LevelPermission.Operator) { Player.SendMessage(p, "Cannot kick Operators from your map."); }
            Command.all.Find("main").Use(who, "");
            Player.SendMessage(p, "Kicked " + who.color + who.name + Server.DefaultColor + " from your home map.");
            Player.SendMessage(who, "You were kicked from the home map of " + p.color + p.name + Server.DefaultColor + "!");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hkick <player> - Kicks other players from your map.");
        }
    }
}
