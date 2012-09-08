using System;

namespace MCDawn
{
    public class CmdImpersonate : Command
    {
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public override bool museumUsable { get { return true; } }
        public override string name { get { return "impersonate"; } }
        public override string[] aliases { get { return new string[] { "imp" }; } }
        public override string type { get { return "other"; } }
        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length < 2) { Help(p); return; }
            string[] msg = message.Split(new char[] { ' ' }, 2);
            Player who = Player.Find(msg[0]);
            if (msg[1] == "") { Help(p); return; }
            if (who == null)
            {
                Player.GlobalMessage(msg[0] + ": &2" + msg[1]);
                if (Server.irc) { IRCBot.Say(msg[0] + ": " + msg[1]); }
                if (p != null) { Server.s.Log("(" + p.name + " impersonating " + msg[0] + ") " + msg[0] + ": " + msg[1]); }
                else { Server.s.Log("(Console impersonating " + msg[0] + ") " + msg[0] + ": " + msg[1]); }
            }
            else
            {
                if (p != null && who.group.Permission >= p.group.Permission) { Player.SendMessage(p, "You can't impersonate a player of same or higher rank!"); return; }
                Player.GlobalChat(who, msg[1], true);
                IRCBot.Say(who.prefix + who.displayName + ": " + msg[1]);
                if (p != null) { Server.s.Log("(" + p.name + " impersonating " + who.displayName + ") " + who.prefix + who.displayName + ": " + msg[1]); }
                else { Server.s.Log("(Console impersonating " + who.name + ") " + who.prefix + who.displayName + ": " + msg[1]); }
            }
        }
        public override void Help(Player p) 
        { 
            Player.SendMessage(p, "/impersonate <player> <message> - Sends a message as if it came from <player>"); 
        }
    }
}