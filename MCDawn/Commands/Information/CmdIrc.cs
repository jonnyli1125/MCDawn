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
                    hasirc = "&aEnabled" + "&g.";
                    ircdetails = Server.ircServer + " > " + Server.ircChannel;
                }
                else
                {
                    hasirc = "&cDisabled" + "&g.";
                }
                Player.SendMessage(p, "IRC is " + hasirc);
                if (ircdetails != "")
                {
                    Player.SendMessage(p, "Location: " + ircdetails);
                    Player.SendMessage(p, "Current users in IRC channels:");
                    string[] names = IRCBot.GetChannelUsers(Server.ircChannel).ToArray();
                    Player.SendMessage(p, "(IRC) " + Server.ircChannel + ": " + (names.Length > 0 ? String.Join(", ", names) : "None"));
                    if (p == null || (p != null && p.group.Permission > Server.opchatperm))
                    {
                        names = IRCBot.GetChannelUsers(Server.ircOpChannel).ToArray();
                        Player.SendMessage(p, "(OP IRC) " + Server.ircOpChannel + ": " + (names.Length > 0 ? String.Join(", ", names) : "None"));
                    }
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/irc - Shows the server's IRC server, channel name, and users in the channel.");
        }
    }
}
