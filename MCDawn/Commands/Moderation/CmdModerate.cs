using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MCDawn
{
    public class CmdModerate : Command
    {
        public override string name { get { return "moderate"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdModerate() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }

            if (Server.chatmod)
            {
                Server.chatmod = false;
                Player.GlobalChat(null, Server.DefaultColor + "Chat moderation has been disabled.  Everyone can now speak.", false);
                Server.s.Log("Chat Moderation disabled.");
            }
            else
            {
                Server.chatmod = true;
                Player.GlobalChat(null, Server.DefaultColor + "Chat moderation engaged!  Silence the plebians!", false);
                Server.s.Log("Chat Moderation enabled.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/moderate - Toggles chat moderation status.  When enabled, only voiced");
            Player.SendMessage(p, "players may speak.");
        }
    }
}