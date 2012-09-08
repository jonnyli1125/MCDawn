//Coded by Gamemakergm

using System;
using System.IO;

namespace MCDawn
{
    public class CmdAlive : Command
    {
        public override string name { get { return "alive"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdAlive() { }
        public override void Use(Player p, string message)
        {
            if (p.level.humans.Count == 0)
            {
                Player.SendMessage(p, "No one is alive D:");
            }
            else
            {
                Player.SendMessage(p, "The current &aAlive " + Server.DefaultColor + "players are ");
                p.level.humans.ForEach(delegate(Player player)
                {
                    Player.SendMessage(p, player.color + player.name);
                });
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/alive - Shows alive players.");
        }
}
}
