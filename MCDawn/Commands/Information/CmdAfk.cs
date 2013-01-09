using System;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    class CmdAfk : Command
    {
        public override string name { get { return "afk"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdAfk() { }

        public override void Use(Player p, string message)
        {
            if (message != "list")
            {
                if (p.joker)
                {
                    message = "";
                }
                if (!Server.afkset.Contains(p.name))
                {
                    Server.afkset.Add(p.name);
                    if (p.muted)
                    {
                        message = "";
                    }
                    Player.GlobalMessage("-" + p.color + p.name + "&g- is AFK " + message);
                    IRCBot.Say(p.name + " is AFK " + message);
                    //AllServerChat.Say(p.name + " is AFK " + message);
                    //if (!p.afkTimer.Enabled) { p.afkTimer.Start(); }
                    p.afkStart = DateTime.Now;
                    return;

                }
                else
                {
                    Server.afkset.Remove(p.name);
                    Player.GlobalMessage("-" + p.color + p.name + "&g- is no longer AFK");
                    IRCBot.Say(p.name + " is no longer AFK");
                    //AllServerChat.Say(p.name + " is no longer AFK " + message);
                    return;
                }
            }
            else
            {
                foreach (string s in Server.afkset) Player.SendMessage(p, s);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/afk <reason> - mark yourself as AFK. Use again to mark yourself as back");
        }
    }
}
