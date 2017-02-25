//OH HAI DER JONNY

using System;

namespace MCDawn
{
    public class CmdDevs : Command
    {
        public override string name { get { return "devs"; } }
        public override string[] aliases { get { return new string[] { "developers" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdDevs() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            string devlist = "";
            string temp;
            foreach (string dev in Server.devs)
            {
                temp = dev.Substring(0, 1);
                temp = temp.ToUpper() + dev.Remove(0, 1);
                devlist += temp + ", ";
            }
            devlist = devlist.Remove(devlist.Length - 2);
            Player.SendMessage(p, "&9MCDawn Development Team: &g" + devlist);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/devs - Displays the list of MCDawn developers.");
        }
    }
}