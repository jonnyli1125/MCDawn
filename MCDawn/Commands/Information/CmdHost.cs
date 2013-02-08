using System;

namespace MCDawn
{
    public class CmdHost : Command
    {
        public override string name { get { return "host"; } }
        public override string[] aliases { get { return new string[] { "zall" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdHost() { }

        public override void Use(Player p, string message)
        {
            Player.SendMessage(p, "Host is currently &3" + Server.ZallState + ".");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/host - Shows what the host is up to.");
        }
    }
}