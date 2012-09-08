using System;

namespace MCDawn
{
    public class CmdXban : Command
    {
        public override string name { get { return "xban"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public CmdXban() { }
        public override void Use(Player p, string message)
        {

            if (message == "") { Help(p); return; }

            Player who = Player.Find(message.Split(' ')[0]);
            string msg = message.Split(' ')[0];
            if (Server.devs.Contains(who == null ? msg.ToLower() : who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't Xban a MCDawn Developer!");
                if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to Xban a MCDawn Developer!");
                else Player.GlobalMessage("The Console is crazy! Trying to Xban a MCDawn Developer!");
                return;
            }
            if (Server.staff.Contains(who == null ? msg.ToLower() : who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't Xban a MCDawn Staff Member!");
                if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to Xban a MCDawn Staff Member!");
                else Player.GlobalMessage("The Console is crazy! Trying to Xban a MCDawn Staff Member!");
                return;
            }
            if (Server.administration.Contains(who == null ? msg.ToLower() : who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't Xban a MCDawn Administrator!");
                if (p != null) Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to Xban a MCDawn Administrator!");
                else Player.GlobalMessage("The Console is crazy! Trying to Xban a MCDawn Administrator!");
                return;
            }
            if (p != null) { p.ignorePermission = true; }
            try
            {
                if (message.Split(' ').Length == 1)
                {
                    if (Server.customBan) message += " " + Server.customBanMessage;
                    else
                    {
                        if (p != null) message += " You were banned by " + p.name + "!";
                        else message += " You were banned by the Console!";
                    }
                }
                if (who != null)
                {
                    Command.all.Find("undo").Use(p, msg + " all");
                    Command.all.Find("ban").Use(p, message);
                    Command.all.Find("banip").Use(p, "@" + msg);
                    Command.all.Find("kick").Use(p, message);
                }
                else
                {
                    Command.all.Find("undo").Use(p, msg + " all");
                    Command.all.Find("ban").Use(p, message);
                    Command.all.Find("banip").Use(p, "@" + msg);
                }
            }
            finally { if (p != null) p.ignorePermission = false; }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xban <name> <message> - Bans, IP-Bans, Undoes, and Kicks a player.");
        }
    }
}