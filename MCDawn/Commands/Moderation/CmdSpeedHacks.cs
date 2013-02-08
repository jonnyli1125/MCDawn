using System;

namespace MCDawn
{
    public class CmdSpeedHacks : Command
    {
        public override string name { get { return "speedhacks"; } }
        public override string[] aliases { get { return new string[] { "speedhack" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdSpeedHacks() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Level l; Group g;
            if (message.Split(' ').Length > 1)
            {
                if (message.Split(' ').Length > 2) { Help(p); return; }
                l = Level.Find(message.Split(' ')[0]);
                if (l == null) { Player.SendMessage(p, "Level could not be found."); return; }
                g = Group.Find(message.Split(' ')[1]);
                if (g == null) { Player.SendMessage(p, "Rank could not be found."); return; }
                if (p.group.Permission < g.Permission && p != null) { Player.SendMessage(p, "Cannot change SpeedHack rank to a higher rank."); return; }
                if (p.group.Permission < l.speedHackRank.Permission && p != null) { Player.SendMessage(p, "Cannot change SpeedHack rank of a higher rank."); return; }
                l.speedHackRank = g;
                l.Save();
                Player.GlobalMessage("SpeedHack rank on " + l.name + " changed to " + g.name);
            }
            else
            {
                if (p == null) { Player.SendMessage(p, "Please specify a level if you are using this from Console."); return; }
                l = p.level;
                g = Group.Find(message);
                if (g == null) { Player.SendMessage(p, "Rank could not be found."); return; }
                if (p.group.Permission < g.Permission) { Player.SendMessage(p, "Cannot change SpeedHack rank to a higher rank."); return; }
                if (p.group.Permission < l.speedHackRank.Permission) { Player.SendMessage(p, "Cannot change SpeedHack rank of a higher rank."); return; }
                l.speedHackRank = g;
                l.Save();
                Player.GlobalMessage("SpeedHack rank on " + l.name + " changed to " + g.name);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/speedhacks [level] <rank> - Set minimum rank for speedhacks on [level].");
            Player.SendMessage(p, "If [leve] not given, player's current level is used.");
        }
    }
}