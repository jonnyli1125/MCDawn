using System;
using System.IO;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdLowlag : Command
    {
        public override string name { get { return "lowlag"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdLowlag() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (message == "" || message.ToLower() == "off")
                {
                    Server.updateTimer.Interval = 100;
                    Player.GlobalMessage("&dLow lag " + "&gmode was turned &cOFF" + "&g.");
                }
                else
                {
                    if (Convert.ToInt32(message) > 10000 || Convert.ToInt32(message) < 200) { p.SendMessage("Lowlag interval must be between 200 and 10000 milliseconds."); return; }
                    Server.updateTimer.Interval = Convert.ToInt32(message);
                    Player.GlobalMessage("&dLow lag " + "&ginterval set to &a" + Server.updateTimer.Interval.ToString() + "&g milliseconds.");
                }
            }
            catch (Exception) { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lowlag <interval/off> - Set the Lowlag interval for server.");
        }
    }
}