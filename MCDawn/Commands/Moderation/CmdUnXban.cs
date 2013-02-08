using System;
using System.Data;
using System.Net;
using System.Text.RegularExpressions;

namespace MCDawn
{
    public class CmdUnXban : Command
    {
        public override string name { get { return "unxban"; } }
        public override string[] aliases { get { return new string[] { "uban" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public CmdUnXban() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            try
            {
                string foundNamesWithIP = "";
                try
                {
                    using (DataTable ip = MySQL.fillData("SELECT Name FROM Players WHERE IP = '" + message + "'"))
                        if (ip.Rows.Count > 0)
                            for (int i = 0; i < ip.Rows.Count; i++)
                                foundNamesWithIP += ip.Rows[i]["Name"].ToString() + " "; // fucking no string.join D:
                }
                catch (Exception e) { Server.ErrorLog(e); return; }
                IPAddress outIP = null; bool isIP = IPAddress.TryParse(message, out outIP);
                if (!isIP)
                {
                    Command.all.Find("unban").Use(p, message);
                    Command.all.Find("unbanip").Use(p, "@" + message);
                }
                else
                {
                    for (int i = 0; i < foundNamesWithIP.Split(' ').Length; i++)
                    {
                        if (!String.IsNullOrEmpty(foundNamesWithIP.Split(' ')[i]))
                        {
                            Command.all.Find("unban").Use(p, foundNamesWithIP.Split(' ')[i]);
                            Command.all.Find("unbanip").Use(p, foundNamesWithIP.Split(' ')[i]);
                        }
                    }
                }
            }
            catch { }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unxban <name/ip> - Unbans and unipbans a player.");
        }
    }
}