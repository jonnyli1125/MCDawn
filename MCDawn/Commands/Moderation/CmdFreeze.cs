using System;

namespace MCDawn
{
    public class CmdFreeze : Command
    {
        public override string name { get { return "freeze"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdFreeze() { }

        public override void Use(Player p, string message)
        {
            if (message.Trim() == "") { Help(p); return; }

            string[] splitted; 
            if (message.Trim().IndexOf(" ") != -1) splitted = message.Split(' '); 
            else splitted = new string[] { message };
            int froze = 0, defroze = 0;
            for (int i = 0; i < splitted.Length; i++)
            {
                Player who = Player.Find(splitted[i]);
                if (who == null) { Player.SendMessage(p, "Could not find player " + splitted[i] + "."); continue; }
                else if (Server.devs.Contains(who.originalName.ToLower()) || Server.devs.Contains(message.ToLower()))
                {
                    Player.SendMessage(p, "Woah!! You can't freeze a MCDawn Developer!");
                    if (p != null)
                        Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to freeze a MCDawn Developer!");
                    else
                        Player.GlobalMessage("The Console is crazy! Trying to freeze a MCDawn Developer!");
                    continue;
                }
                else if (who == p && p != null) { Player.SendMessage(p, "Cannot freeze yourself."); continue; }
                else if (p != null && who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "Cannot freeze someone of equal or greater rank."); continue; }

                if (!who.frozen)
                {
                    who.frozen = true;
                    if (p != null) 
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &bfrozen" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".", false);
                    else
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &bfrozen by the Console.", false);
                    froze++;
                }
                else
                {
                    who.frozen = false;
                    if (p != null)
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &adefrosted" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".", false);
                    else
                        Player.GlobalChat(null, who.color + who.name + Server.DefaultColor + " has been &adefrosted by the Console.", false);
                    defroze++;
                }
            }
            Player.SendMessage(p, "Froze " + froze + " players, defrosted " + defroze + " players.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/freeze <player1> <player2> <player3> etc. - Stops all players from moving until unfrozen.");
        }
    }
}