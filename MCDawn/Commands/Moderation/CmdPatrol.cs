using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdPatrol : Command
    {
        public override string name { get { return "patrol"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdPatrol() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            Random rnd = new Random(); List<Player> guests = new List<Player>();
            if (message == "") { foreach (Player pl in Player.players) { if (pl.group.Permission == LevelPermission.Guest) { guests.Add(pl); } } }
            else 
            {
                Level foundLevel = Level.Find(message);
                if (foundLevel == null) { Player.SendMessage(p, "Level could not be found."); return; }
                foreach (Player pl in foundLevel.players) { if (pl.group.Permission == LevelPermission.Guest && pl != null) { guests.Add(pl); } }
            }
            if (guests.Count <= 0) { Player.SendMessage(p, "There are no guests to patrol."); return; }

            Command.all.Find("tp").Use(p, guests[rnd.Next(0, guests.Count)].name);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/patrol [level] - Teleport yourself to a random guest player.");
            Player.SendMessage(p, "If [level] is given, it will only teleport to guests in the given level.");
        }
    }
}