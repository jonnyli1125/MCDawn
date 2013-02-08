using System;

namespace MCDawn
{
    public class CmdMe : Command
    {
        public override string name { get { return "me"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdMe() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Player.SendMessage(p, "You"); return; }

            if (p.muted) { Player.SendMessage(p, "You are currently muted and cannot use this command."); return; }
            if (Server.chatmod && !p.voice) { Player.SendMessage(p, "Chat moderation is on, you cannot emote."); return; }

            if (Server.worldChat && !p.levelchat) Player.GlobalChat(p, p.color + "*" + p.displayName + " " + message, false);
            else Player.GlobalChatLevel(p, p.color + "*" + p.displayName + " " + message, false);
            IRCBot.Say(p.color + "*" + p.displayName + " " + p.color + message);
            //AllServerChat.Say("*" + p.name + " " + message);

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "What do you need help with, m'boy?! Are you stuck down a well?!");
        }
    }
}