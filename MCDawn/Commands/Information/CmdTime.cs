using System;

namespace MCDawn
{
    public class CmdTime : Command
    {
        public override string name { get { return "time"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdTime() { }

        public override void Use(Player p, string message)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            message = "Server time is " + time;
            Player.SendMessage(p, message);
            //message = "full Date/Time is " + DateTime.Now.ToString();
            //Player.SendMessage(p, message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/time - Shows the server time.");
        }
    }
}