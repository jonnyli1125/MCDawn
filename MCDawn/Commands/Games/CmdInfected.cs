// Coded by Gamemakergm

using System;

namespace MCDawn
{
    public class CmdInfected : Command
    {
        public override string name { get { return "infected"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdInfected() { }
        public override void Use(Player p, string message)
        {
            if (p.level.zombies.Count == 0)
            {
                Player.SendMessage(p, "No one is infected :)");
            }
            else
            {
                Player.SendMessage(p, "The &cInfected " + Server.DefaultColor + "players are ");
                p.level.zombies.ForEach(delegate(Player player)
                {
                    Player.SendMessage(p, player.color + player.originalName);
                });
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/infected - Shows infected players on your level.");
        }
    }
}
