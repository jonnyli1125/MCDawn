using System;
using System.IO;

namespace MCDawn
{
    public class CmdReferee : Command
    {
        public override string name { get { return "referee"; } }
        public override string[] aliases { get { return new string[] { "ref" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdReferee() { }

        public override void Use(Player p, string message)
        {
            //if (p == null && message != "") { Player.SendMessage(p, "This command can only be used in-game!"); return; }
            Player who;
            if (message != "")
            {
                who = Player.Find(message);
            }
            else
            {
                who = p;
            }

            if (who == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }

            if (who.level.zombiegame == true)
            {
                if (who.referee)
                {
                    if (who.infected) { who.level.infection.ToHuman(p); }
                    who.referee = false;
                    Player.GlobalChat(p, who.color + who.name + Server.DefaultColor + " is no longer a referee!", false);
                }
                else
                {
                    if (who.infected) { who.level.infection.ToHuman(p); }
                    who.referee = true;
                    Player.GlobalChat(p, who.color + who.name + Server.DefaultColor + " is now a referee!", false);
                }
                who.UpdateDetail();
                who.level.infection.Check();
                return;
            }

            string oldtitle = who.prefix;
            if (who.referee == true)
            {
                who.referee = false;
                if (who.title.ToLower() == "referee" || who.title.ToLower() == "ref") { who.title = oldtitle; }
                Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " is no longer referee.");
            }
            else
            {
                who.referee = true;
                if (who.title.ToLower() != "referee" && who.title.ToLower() != "ref") { who.title = "Referee"; }
                Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " is now referee.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/referee [name] - Gives referee status to a player.");
            Player.SendMessage(p, "If [name] is given, that player is given referee status.");
            Player.SendMessage(p, "NOTE: If you are the current referee, giving someone referee will make you lose referee.");
        }
    }
}