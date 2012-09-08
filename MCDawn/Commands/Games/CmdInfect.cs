//Coded by Gamemakergm

using System;

namespace MCDawn
{
    public class CmdInfect : Command
    {
        public override string name { get { return "infect"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "games"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdInfect() { }
        public override void Use(Player p, string message)
        {
            Player who = null;
            if (message == "") { who = p; message = p.name; } else { who = Player.Find(message); }
            if (!p.level.zombiegame)
            {
                Player.SendMessage(p, "This command can only be used when Infection is on.");
                return;
            }
            if (who.infected)
            {
                p.SendMessage(who.color + who.name + Server.DefaultColor + " is already infected!");
            }
            else
            {
                p.level.infection.ToInfected(who);
                Player.GlobalMessageLevel(p.level, who.color + who.originalName + " &4was INFECTED by " + p.color + p.originalName);
                p.level.infection.Check();
            }   
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/infect [name] - Infects [name] while playing infection.. ");
        }
    }
}
