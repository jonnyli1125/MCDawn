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
                if (dev == "schmidty56789")
                {
                    devlist += "ScHmIdTy56789, ";
                }
                else
                {
                    temp = dev.Substring(0, 1);
                    temp = temp.ToUpper() + dev.Remove(0, 1);
                    devlist += temp + ", ";
                }
            }
            devlist = devlist.Remove(devlist.Length - 2);
            Player.SendMessage(p, "&9MCDawn Development Team: " + Server.DefaultColor + devlist);

            string adminlist = "";
            string temp3;
            foreach (string admin in Server.administration)
            {
                if (admin == "sillyboyization")
                {
                    adminlist += "[Sillyboyization] ";
                }
                else if (admin == "storm_resurge")
                {
                    adminlist += "Storm_ReSurge, ";
                }
                else if (admin == "epidermik")
                {
                    adminlist += "EpidermiK, ";
                }
                else
                {
                    temp3 = admin.Substring(0, 1);
                    temp3 = temp3.ToUpper() + admin.Remove(0, 1);
                    adminlist += temp3 + ", ";
                }
            }
            adminlist = adminlist.Remove(adminlist.Length - 2);
            Player.SendMessage(p, "&6MCDawn Administration Team: " + Server.DefaultColor + adminlist);

            /*try
            {
                string stafflist = "";
                string temp2;
                foreach (string staff in Server.staff)
                {
                    temp2 = staff.Substring(0, 1);
                    temp2 = temp2.ToUpper() + staff.Remove(0, 1);
                    stafflist += temp2 + ", ";
                }
                stafflist = stafflist.Remove(stafflist.Length - 2);
                Player.SendMessage(p, "&4MCDawn Staff Team: " + Server.DefaultColor + stafflist);
            }
            catch { Player.SendMessage(p, "&4MCDawn Staff Team: &gNone"); }*/
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/devs - Displays the list of MCDawn developers.");
        }
    }
}