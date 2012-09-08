using System;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    class CmdSay : Command
    {
        public override string name { get { return "say"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdSay() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player.GlobalChat(p, message, false);
            IRCBot.Say(message);
            //AllServerChat.Say(message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/say - Broadcasts a global message to everyone in the server.");
        }
    }
}
