using System;
using System.Net;

namespace MCDawn
{
    public class CmdPortCheck : Command
    {
        public override string name { get { return "portcheck"; } }
        public override string[] aliases { get { return new string[] { "checkport" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdPortCheck() { }

        public override void Use(Player p, string message)
        {
            string resp; // Port Check Response
            ushort Port;
            if (message == "") { message = Server.port.ToString(); }
            try { Port = ushort.Parse(message); }
            catch { Player.SendMessage(p, "The given port " + message + " is invalid."); return; }
            try
            {
                using (WebClient web = new WebClient())
                    resp = web.DownloadString("http://ll.www.utorrent.com:16000/testport?port=" + Port + "&plain=1");
                if (resp == "ok")
                {
                    Player.SendMessage(p, "Port " + message + " is open.");
                    return;
                }
                if (resp == "failed")
                {
                    Player.SendMessage(p, "Port " + message + " is closed.");
                    return;
                }
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Player.SendMessage(p, "Error checking port.");
                return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/portcheck [port] - Check if your [port] is open.");
        }
    }
}