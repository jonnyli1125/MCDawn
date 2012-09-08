using System;

namespace MCDawn
{
    public class CmdRemote : Command
    {
        public override string name { get { return "remote"; } }
        public override string[] aliases { get { return new string[] { "remotes" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRemote() { }
        
        public override void Use(Player p, string message)
        {
            if (message.Trim() == "")
            {
                Player.SendMessage(p, "There are " + Remote.remotes.Count + " remote consoles online.");
                Remote.remotes.ForEach(delegate(Remote r) { Player.SendMessage(p, r.username + " [&a" + r.name + Server.DefaultColor + "]"); });
                return;
            }
            else if (message.Split(' ')[0].ToLower() == "kick")
            {
                Remote r = Remote.Find(message.Split(' ')[1]);
                if (r == null) { Player.SendMessage(p, "Could not find remote."); return; }
                if (message.Split(new char[] { ' ' }, 3)[2] == "")
                    if (p != null) message.Split(new char[] { ' ' }, 3)[2] = "You were kicked by " + p.name + "!";
                    else message.Split(new char[] { ' ' }, 3)[2] = "You were kicked by the Console!"; 
                r.Disconnect(message.Split(new char[] { ' ' }, 3)[2]);
                return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/remote - Show all online remote consoles.");
            Player.SendMessage(p, "/remote kick <user> [reason] - Disconnect a connected remote console.");
        }
    }
}