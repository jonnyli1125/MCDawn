using System;

namespace MCDawn
{
    public class CmdTBracket : Command
    {
        public override string name { get { return "tbracket"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdTBracket() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "") { Help(p); return; }
                string[] args = message.Split(' ');
                Player who = Player.Find(args[0]);
                if (who == null)
                {
                    Player.SendMessage(p, "Could not find player.");
                    return;
                }
                if (args.Length == 1)
                {
                    who.titlebracket = 0;
                    Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " had their title bracket set to default.", false);
                    MySQL.executeQuery("UPDATE Players SET titleBracket = 0 WHERE Name = '" + who.name + "'");
                    who.SetPrefix();
                    return;
                }
                else
                {
                    int bracket = 0;
                    switch (bracket)
                    {
                        case 0:
                            who.tbracketstart = "[";
                            who.tbracketend = "]";
                            break;
                        case 1:
                            who.tbracketstart = "(";
                            who.tbracketend = ")";
                            break;
                        case 2:
                            who.tbracketstart = "{";
                            who.tbracketend = "}";
                            break;
                        case 3:
                            who.tbracketstart = "~";
                            who.tbracketend = "~";
                            break;
                        case 4:
                            who.tbracketstart = "<";
                            who.tbracketstart = ">";
                            break;
                        default:
                            who.tbracketstart = "[";
                            who.tbracketend = "]";
                            break;
                    }
                    if (bracket != 0 && bracket != 1 && bracket != 2 && bracket != 3 && bracket != 4) { Player.SendMessage(p, "The title brackets \"" + args[1] + "\" are not allowed."); return; }
                    else
                    {
                        MySQL.executeQuery("UPDATE Players SET titleBracket = " + bracket + " WHERE Name = '" + who.name + "'");
                        who.titlebracket = bracket;
                        who.SetPrefix();
                        Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " had their title bracket changed to " + who.tbracketstart + " and " + who.tbracketend + Server.DefaultColor + ".", false);
                        who.titlebracket = bracket;
                        who.SetPrefix();
                    }
                }
            }
            catch (Exception) { p.SendMessage("Error with Title Brackets."); }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tbracket <player> [bracket number] - Gives <player> the title bracket of [bracket number].");
            Player.SendMessage(p, "If no [bracket] is specified, bracket is set to default ([]).");
            Player.SendMessage(p, "1: [], 2: (), 3: {}, 4: ~~, 5: <>");
        }
    }
}
