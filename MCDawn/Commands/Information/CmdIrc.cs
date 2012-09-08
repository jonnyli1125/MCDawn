using System;

namespace MCDawn
{
    public class CmdIrc : Command
    {
        public override string name { get { return "irc"; } }
        public override string[] aliases { get { return new string[] { "hasirc" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdIrc() { }

        public override void Use(Player p, string message)
        {
            if (message != "")
            {
                Help(p);
                return;
            }
            else
            {
                string hasirc;
                string ircdetails = "";
                if (Server.irc)
                {
                    hasirc = "&aEnabled" + Server.DefaultColor + ".";
                    ircdetails = Server.ircServer + " > " + Server.ircChannel;
                }
                else
                {
                    hasirc = "&cDisabled" + Server.DefaultColor + ".";
                }
                Player.SendMessage(p, "IRC is " + hasirc);
                if (ircdetails != "")
                {
                    Player.SendMessage(p, "Location: " + ircdetails);
                    Player.SendMessage(p, "Current users in IRC channel:");
                    try
                    {
                        if (IRCBot.GetConnectedUsers().Length > 0)
                            try
                            {
                                foreach (string user in IRCBot.GetConnectedUsers())
                                {
                                    try
                                    {
                                        if (!Char.IsLetter(user[0]))
                                            Player.SendMessage(p, user.Substring(1));
                                        else
                                            Player.SendMessage(p, user);
                                    }
                                    catch { }
                                }
                            }
                            catch { }
                        else
                            Player.SendMessage(p, "None");
                    }
                    catch { }
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/irc - Shows the server's IRC server, channel name, and users in the channel.");
        }
    }
}
