using System;

namespace MCDawn
{
    public class CmdFlipHeads : Command
    {
        public override string name { get { return "flipheads"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdFlipHeads() { }

        public override void Use(Player p, string message)
        {
            Server.flipHead = !Server.flipHead;

            if (Server.flipHead)
                Player.GlobalChat(p, "All necks were broken", false);
            else
                Player.GlobalChat(p, "All necks were mended", false);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/flipheads - Does as it says on the tin");
        }
    }
}