using System;

namespace MCDawn
{
    public class CmdBan : Command
    {
        public override string name { get { return "ban"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdBan() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "") { Help(p); return; }
                bool stealth = false; bool totalBan = false;
                if (message[0] == '#')
                {
                    message = message.Remove(0, 1).Trim();
                    stealth = true;
                    Server.s.Log("Stealth Ban Attempted");
                }
                else if (message[0] == '@')
                {
                    totalBan = true;
                    message = message.Remove(0, 1).Trim();
                }

                Player who = Player.Find(message.Split(' ')[0]);

                if (who == null)
                {
                    /*if (!Player.ValidName(message))
                    {
                        Player.SendMessage(p, "Invalid name \"" + message + "\".");
                        return;
                    }*/

                    if (Server.devs.Contains(message.Split(' ')[0].ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Developer!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Developer!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Developer!");
                        return;
                    }
                    if (Server.staff.Contains(message.Split(' ')[0].ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Staff Member!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Staff Member!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Staff Member!");
                        return;
                    }
                    if (Server.administration.Contains(message.Split(' ')[0].ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Administrator!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Administrator!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Administrator!");
                        return;
                    }

                    Group foundGroup = Group.findPlayerGroup(message.Split(' ')[0]);

                    if (foundGroup.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, "You can't ban a " + foundGroup.name + "!");
                        return;
                    }
                    if (foundGroup.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, message.Split(' ')[0] + " is already banned.");
                        return;
                    }
                    if (p != null && foundGroup.Permission >= p.group.Permission)
                    {
                        Player.SendMessage(p, "You cannot ban a person ranked equal or higher than you.");
                        return;
                    }

                    foundGroup.playerList.Remove(message.Split(' ')[0]);
                    foundGroup.playerList.Save();

                    if (p != null)
                    {
                        Player.GlobalMessage(message.Split(' ')[0] + " &f(offline)" + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".");
                    }
                    else
                    {
                        Player.GlobalMessage(message.Split(' ')[0] + " &f(offline)" + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by Console.");
                    }
                    Group.findPerm(LevelPermission.Banned).playerList.Add(message.Split(' ')[0]);
                    if (message.Trim().Split(' ').Length == 1) { message += " No reason given."; }
                    if (p != null) { if (!message.Substring(message.IndexOf(" ") + 1).ToLower().Contains(p.name.ToLower())) { message += " (" + p.name + ")"; } }
                    else { if (!message.Substring(message.IndexOf(" ") + 1).ToLower().Contains("console")) { message += " (Console)"; } }
                    MySQL.executeQuery("UPDATE Players SET lastRankReason = '[" + DateTime.Now.ToString() + "] " + message.Substring(message.IndexOf(" ") + 1).Replace("'", "\\'").Trim() + "' WHERE Name = '" + message.Split(' ')[0] + "'");
                    Player.GlobalChat(null, "&6Reason: &f" + message.Substring(message.IndexOf(" ") + 1).Trim(), false);
                }
                else
                {
                    if (!Player.ValidName(who.name))
                    {
                        Player.SendMessage(p, "Invalid name \"" + who.name + "\".");
                        return;
                    }
                    if (Server.devs.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Developer!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban an MCDawn Developer!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban an MCDawn Developer!");
                        return;
                    }
                    if (Server.staff.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Staff Member!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Staff Member!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Staff Member!");
                        return;
                    }
                    if (Server.administration.Contains(who.originalName.ToLower()))
                    {
                        Player.SendMessage(p, "Woah!! You can't ban a MCDawn Administrator!");
                        if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to ban a MCDawn Administrator!");
                        else Player.GlobalMessage("The Console is crazy! Trying to ban a MCDawn Administrator!");
                        return;
                    }

                    if (who.group.Permission >= LevelPermission.Operator)
                    {
                        Player.SendMessage(p, "You can't ban a " + who.group.name + "!");
                        return;
                    }
                    if (who.group.Permission == LevelPermission.Banned)
                    {
                        Player.SendMessage(p, who.name + " is already banned.");
                        return;
                    }

                    who.group.playerList.Remove(message.Split(' ')[0]);
                    who.group.playerList.Save();

                    if (p != null)
                    {
                        if (stealth) Player.GlobalMessageOps(who.color + who.name + Server.DefaultColor + " is now STEALTH &8banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".");
                        else Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".", false);
                    }
                    else
                    {
                        if (stealth) Player.GlobalMessageOps(who.color + who.name + Server.DefaultColor + " is now STEALTH &8banned" + Server.DefaultColor + " by Console.");
                        else Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by Console.", false);
                    }

                    if (message.Trim().Split(' ').Length == 1) { message += " No reason given."; }
                    if (p != null) { if (!message.Substring(message.IndexOf(" ") + 1).ToLower().Contains(p.name.ToLower())) { message += " (" + p.name + ")"; } }
                    else { if (!message.Substring(message.IndexOf(" ") + 1).ToLower().Contains("console")) { message += " (Console)"; } }
                    MySQL.executeQuery("UPDATE Players SET lastRankReason = '[" + DateTime.Now.ToString() + "] " + message.Substring(message.IndexOf(" ") + 1).Replace("'", "\\'").Trim() + "' WHERE Name = '" + who.name + "'");
                    who.lastRankReason = "[" + DateTime.Now.ToString() + "] " + message.Substring(message.IndexOf(" ") + 1).Trim();

                    who.group = Group.findPerm(LevelPermission.Banned);
                    who.color = who.group.color;
                    Player.GlobalDie(who, false);
                    Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
                    Group.findPerm(LevelPermission.Banned).playerList.Add(who.name);
                    Player.GlobalChat(null, "&6Reason: &f" + message.Substring(message.IndexOf(" ") + 1).Trim(), false);
                }
                Group.findPerm(LevelPermission.Banned).playerList.Save();

                if (p != null)
                {
                    IRCBot.Say(message.Split(' ')[0] + " is now banned by " + p.name + ".");
                    Server.s.Log("BANNED: " + message.Split(' ')[0].ToLower() + " by " + p.name + ".");
                }
                else
                {
                    IRCBot.Say(message.Split(' ')[0] + " is now banned by Console.");
                    Server.s.Log("BANNED: " + message.Split(' ')[0].ToLower() + " by Console.");
                }

                if (totalBan == true)
                {
                    Command.all.Find("undo").Use(p, message.Split(' ')[0] + " 0");
                    Command.all.Find("banip").Use(p, "@ " + message.Split(' ')[0]);
                }
            }
            catch (Exception e) { Server.ErrorLog(e); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ban <player> [reason] - Bans a player without kicking him.");
            Player.SendMessage(p, "Add # before name to stealth ban.");
            Player.SendMessage(p, "Add @ before name to total ban.");
        }
    }
}