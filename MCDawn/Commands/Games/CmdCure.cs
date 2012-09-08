//Coded by jonnyli1125

using System;

namespace MCDawn
{
    public class CmdCure : Command
    {
        public override string name { get { return "cure"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdCure() { }
        public override void Use(Player p, string message)
        {
            Player who = null;
            if (message == "") { who = p; message = p.name; } else { who = Player.FindOriginal(message); }
            if (!p.level.zombiegame)
            {
                Player.SendMessage(p, "This command can only be used when Infection is on.");
            }
            if (!who.infected)
            {
                p.SendMessage(who.color + who.name + Server.DefaultColor + " is not infected!");
            }
            else
            {
                p.level.infection.ToHuman(who);
                Player.GlobalMessageLevel(p.level, who.color + who.originalName + " &awas CURED by " + p.color + p.originalName);
                p.level.infection.Check();
            }   
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cure [name] - Cures the player from being Infected.");
        }
    }
}
