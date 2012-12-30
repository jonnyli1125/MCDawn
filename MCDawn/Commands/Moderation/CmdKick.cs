using System;

namespace MCDawn
{
    public class CmdKick : Command
    {
        public override string name { get { return "kick"; } }
        public override string[] aliases { get { return new string[] { "k" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdKick() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            //if (message.ToLower() == "caps" || message.ToLower() == "capitals") { message = "Let go of your Caps Lock button!"; return; }

            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player specified."); return; }
            if (Server.devs.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't kick a MCDawn Developer!");
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to kick a MCDawn Developer!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to kick a MCDawn Developer!");
                }
                return;
            }
            if (Server.staff.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't kick a MCDawn Staff Member!");
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to kick a MCDawn Staff Member!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to kick a MCDawn Staff Member!");
                }
                return;
            }
            if (Server.administration.Contains(who.originalName.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't kick a MCDawn Administrator!");
                if (p == null)
                {
                    Player.GlobalMessage("The Console is crazy! Trying to kick a MCDawn Administrator!");
                }
                else
                {
                    Player.GlobalMessage(p.color + p.name + Server.DefaultColor + " is crazy! Trying to kick a MCDawn Administrator!");
                }
                return;
            }
            if (message.Split(' ').Length > 1)
                message = message.Substring(message.IndexOf(' ') + 1);
            else
                message = "You broke the rules!";

            if (p != null)
                if (who == p)
                {
                    Player.SendMessage(p, "You cannot kick yourself!");
                    return;
                }
                else if (who.group.Permission >= p.group.Permission && p != null) 
                { 
                    Player.GlobalChat(p, p.color + p.name + Server.DefaultColor + " tried to kick " + who.color + who.name + " but failed.", false); 
                    return; 
                }
            if (p == null)
                who.Kick("Kicked by " + p.name + " | Reason: " + message);
            else
                who.Kick("Kicked by Console | Reason: " + message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/kick <player> [message] - Kicks a player.");
        }
    }
}