using System;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    class CmdPlayers : Command
    {
        public override string name { get { return "players"; } }
        public override string[] aliases { get { return new string[] { "who", "list" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdPlayers() { }

        struct groups { public Group group; public List<string> players; }
        public override void Use(Player p, string message)
        {
            try
            {
                List<groups> playerList = new List<groups>();

                foreach (Group grp in Group.GroupList)
                {
                    if (grp.name != "nobody")
                    {
                        if (String.IsNullOrEmpty(message) || !Group.Exists(message))
                        {
                            groups groups;
                            groups.group = grp;
                            groups.players = new List<string>();
                            playerList.Add(groups);
                        }
                        else
                        {
                            Group grp2 = Group.Find(message);
                            if (grp2 != null && grp == grp2)
                            {
                                groups groups;
                                groups.group = grp;
                                groups.players = new List<string>();
                                playerList.Add(groups);
                            }
                        }
                    }
                }

                string devs = "";
                string unverified = "";
                //string devUnverified = "";
                int totalPlayers = 0;
                foreach (Player pl in Player.players)
                {
                    if (pl.group.Permission == LevelPermission.Nobody && !Server.devs.Contains(pl.name.ToLower())) { continue; }
                    if (p == null || !pl.hidden || p.group.Permission >= pl.group.Permission || Server.devs.Contains(p.name.ToLower()))
                    {
                        totalPlayers++;
                        string foundName = pl.name;

                        if (Server.afkset.Contains(pl.name))
                        {
                            foundName = pl.name + "-afk";
                        }

                        if (Server.devs.Contains(pl.name.ToLower()) && !pl.devUnverified && !pl.unverified)
                        {
                            if (pl.voice)
                                devs += " " + "&f+" + Server.DefaultColor + foundName + " (" + pl.level.name + "),";
                            else
                                devs += " " + foundName + " (" + pl.level.name + "),";
                        }
                        else if (pl.unverified || pl.devUnverified)
                        {
                            if (pl.voice)
                                unverified += " " + "&f+" + Server.DefaultColor + foundName + " (" + pl.level.name + "),";
                            else
                                unverified += " " + foundName + " (" + pl.level.name + "),";
                        }
                        else
                        {
                            if (pl.voice)
                                playerList.Find(grp => grp.group == pl.group).players.Add("&f+" + Server.DefaultColor + foundName + " (" + pl.level.name + ")");
                            else
                                playerList.Find(grp => grp.group == pl.group).players.Add(foundName + " (" + pl.level.name + ")");
                        }
                    }
                }
                Player.SendMessage(p, "There are " + totalPlayers + " players online " + 
                    (Server.irc ? ("(" + IRCBot.GetChannelUsers(Server.ircChannel).Count + " users on IRC" + 
                    ((p == null || (p != null && p.group.Permission > Server.opchatperm)) ? ", " + 
                    IRCBot.GetChannelUsers(Server.ircOpChannel).Count + " users on OP IRC" : "") + ")") : "") + ".");
                if (devs.Length > 0) { Player.SendMessage(p, ":&9Developers:" + Server.DefaultColor + devs.Trim(',')); }
                for (int i = playerList.Count - 1; i >= 0; i--)
                {
                    groups groups = playerList[i];
                    string appendString = "";

                    foreach (string player in groups.players)
                    {
                        appendString += ", " + player;
                    }

                    if (appendString != "")
                        appendString = appendString.Remove(0, 2);
                    appendString = ":" + groups.group.color + getPlural(groups.group.trueName) + ": " + appendString;

                    Player.SendMessage(p, appendString);
                }
                if (unverified.Length > 0) { Player.SendMessage(p, ":&3Admin Security System:" + Server.DefaultColor + unverified.Trim(',')); }
                //if (devUnverified.Length > 0) { Player.SendMessage(p, ":&3Developer Security System:" + Server.DefaultColor + unverified.Trim(',')); }
                if (Server.irc)
                {
                    Player.SendMessage(p, Server.IRCColour + "(IRC) " + Server.ircChannel + ": " + String.Join(", ", IRCBot.GetChannelUsers(Server.ircChannel).ToArray()));
                    if (p == null || (p != null && p.group.Permission > Server.opchatperm))
                        Player.SendMessage(p, Server.IRCColour + "(OP IRC) " + Server.ircOpChannel + ": " + String.Join(", ", IRCBot.GetChannelUsers(Server.ircOpChannel).ToArray()));
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }

        public string getPlural(string groupName)
        {
            try
            {
                string last2 = groupName.Substring(groupName.Length - 2).ToLower();
                if ((last2 != "ed" || groupName.Length <= 3) && last2[1] != 's')
                {
                    return groupName + "s";
                }
                return groupName;
            }
            catch
            {
                return groupName;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/players [rank] - Shows name and general rank of all players");
        }
    }
}
