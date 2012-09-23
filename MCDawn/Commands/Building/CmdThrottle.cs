using System;
using System.Threading;

namespace MCDawn
{
    public class CmdThrottle : Command
    {
        public override string name { get { return "throttle"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdThrottle() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            try
            {
                if (Convert.ToInt32(message) > 200 || Convert.ToInt32(message) < 1) { Player.SendMessage(p, "Throttle must be inbetween 0 and 10."); return; }
                Server.throttle = Convert.ToInt32(message);
                Player.GlobalChat(null, "Building commands throttle level set to &a" + Server.throttle + "&g.", false);
                Server.s.Log("Building commands throttle level set to " + Server.throttle + ".");
                Properties.Save("properties/server.properties");
            }
            catch { }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/throttle <1-200> - Set speed throttle of cuboid and/or building commands.");
            Player.SendMessage(p, "1 is the slowest, 200 is the fastest.");
        }
    }
}