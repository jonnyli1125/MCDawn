using System;

namespace MCDawn
{
    public class CmdCaps : Command
    {
        public override string name { get { return "caps"; } }
        public override string[] aliases { get { return new string[] { "nocaps", "decaps" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdCaps() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message);
            if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (who.group.Permission > p.group.Permission && p != null) { Player.SendMessage(p, "Cannot turn off caps for player of higher rank."); return; }
            if (who.nocaps)
            {
                who.nocaps = false;
                Player.SendMessage(who, "You are now able to use capitals in chat messages.");
                Player.SendMessage(p, "Allowed caps permissions from " + who.color + who.name + Server.DefaultColor + ".");
            }
            else
            {
                who.nocaps = true;
                Player.SendMessage(who, "You are no longer able to use capitals in chat messages.");
                Player.SendMessage(p, "Removed caps permissions from " + who.color + who.name + Server.DefaultColor + ".");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/caps <player> - Toggle permissions for usage of caps for <player> in chat messages.");
        }
    }
}