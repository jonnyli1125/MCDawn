using System;
namespace MCDawn
{
    public class CmdSetRank : Command
    {
        public override string name { get { return "setrank"; } }
        public override string[] aliases { get { return new string[] { "rank" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdSetRank() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2) { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            Group newRank = Group.Find(message.Split(' ')[1]);
            string msgGave;

            if (p != null)
                if (message.Split(' ').Length > 2) msgGave = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1)) + " (" + p.name + ")"; else msgGave = "No reason given. (" + p.name + ").";
            else
                if (message.Split(' ').Length > 2) msgGave = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1)) + " (Console)"; else msgGave = "No reason given. (Console).";
            if (newRank == null) { Player.SendMessage(p, "Could not find specified rank."); return; }

            Group bannedGroup = Group.findPerm(LevelPermission.Banned);
            if (who == null)
            {
                //if (Server.devs.Contains(message.ToLower())) { p.SendMessage("Can't let you do that, Starfox."); return; }
                string foundName = message.Split(' ')[0];
                if (Group.findPlayerGroup(foundName) == bannedGroup || newRank == bannedGroup)
                {
                    Player.SendMessage(p, "Cannot change the rank to or from \"" + bannedGroup.name + "\".");
                    return;
                }

                if (p != null)
                {
                    if (Group.findPlayerGroup(foundName).Permission >= p.group.Permission || newRank.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot change the rank of someone equal or higher than you"); return;
                    }
                }

                Group oldGroup = Group.findPlayerGroup(foundName);
                oldGroup.playerList.Remove(foundName);
                oldGroup.playerList.Save();

                newRank.playerList.Add(foundName);
                newRank.playerList.Save();

                msgGave = msgGave.Trim();
                Player.GlobalMessage(foundName + " &f(offline)" + "&g's rank was set to " + newRank.color + newRank.name);
                MySQL.executeQuery("UPDATE Players SET lastRankReason = '[" + DateTime.Now.ToString() + "] " + msgGave.Replace("'", "\\'") + "' WHERE Name = '" + foundName + "'");
                Player.GlobalChat(null, "&6Reason: &f" + msgGave, false);
            }
            else
            {
                //if (!Server.devs.Contains(p.name) && Server.devs.Contains(who.name)) { p.SendMessage("Can't let you do that, Starfox."); return; }
                if (p != null)
                {
                    if (who.group == bannedGroup || newRank == bannedGroup)
                    {
                        Player.SendMessage(p, "Cannot change the rank to or from \"" + bannedGroup.name + "\".");
                        return;
                    }

                    if (who.group.Permission >= p.group.Permission || newRank.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "Cannot change the rank of someone equal or higher to yourself."); return;
                    }
                }

                who.group.playerList.Remove(who.name);
                who.group.playerList.Save();

                newRank.playerList.Add(who.name);
                newRank.playerList.Save();

                msgGave = msgGave.Trim();
                Player.GlobalChat(who, who.color + who.name + "&g's rank was set to " + newRank.color + newRank.name, false);
                Player.GlobalChat(null, "&6Reason: &f" + msgGave, false);
                who.group = newRank;
                who.color = who.group.color;
                Player.GlobalDie(who, false);
                who.SendMessage("You are now ranked " + newRank.color + newRank.name + "&g, type /help for your new set of commands.");
                Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                try { if (!Server.cli) { MCDawn.Gui.Window.thisWindow.UpdateClientList(Player.players); } }
                catch { }
                MySQL.executeQuery("UPDATE Players SET lastRankReason = '[" + DateTime.Now.ToString() + "] " + msgGave.Replace("'", "\\'") + "' WHERE Name = '" + who.originalName + "'");
                who.lastRankReason = "[" + DateTime.Now.ToString() + "] " + msgGave;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setrank <player> <rank> [reason] - Sets or returns a players rank.");
            Player.SendMessage(p, "You may use /rank as a shortcut");
            Player.SendMessage(p, "Valid Ranks are: " + Group.concatList(true, true));
            Player.SendMessage(p, "Reason is optional.");
        }
    }
}
