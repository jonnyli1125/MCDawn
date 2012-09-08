//Coded by Gamemakergm
using System;

namespace MCDawn
{
    public class CmdQueue : Command
    {
        public override string name { get { return "queue"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdQueue() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower().Trim() == "clear" || message.Trim() == "") p.level.queuename = "";
            else
            {
                Player who = Player.Find(message);
                p.level.queuename = who.name;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/queue <player/clear> - Queue a player to become a zombie (Infection).");
        }
    }
}