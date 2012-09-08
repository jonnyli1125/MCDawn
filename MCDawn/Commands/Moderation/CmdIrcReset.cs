using System;
using System.Threading;

namespace MCDawn
{
    class CmdIrcReset : Command
    {
        public override string name { get { return "ircreset"; } }
        public override string[] aliases { get { return new string[] { "botreset" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdIrcReset() { }

        public override void Use(Player p, string message)
        {
            try
            {
                switch (message.ToLower())
                {
                    case "irc": IRCBot.Reset(); break;
                    case "global": GlobalChatBot.Reset(); break;
                    case "":
                    case "all":
                        IRCBot.Reset();
                        GlobalChatBot.Reset();
                        break;
                    default: Help(p); break;
                }
                Player.SendMessage(p, "Successfully resetted bot(s).");
            }
            catch (Exception e) { Server.ErrorLog(e); Player.SendMessage(p, "Failed to reset bot(s)."); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/ircreset <irc/global/all> - Resets the IRCBot and/or GlobalChatBot. If no value is given, \"all\" is used.");
        }
    }
}
