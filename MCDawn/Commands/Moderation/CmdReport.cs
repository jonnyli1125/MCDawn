using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MCDawn
{
    public class CmdReport : Command
    {
        public override string name { get { return "report"; } }
        public override string[] aliases { get { return new string[] { "rep" }; } }
        public override string type { get { return "mod"; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public override bool museumUsable { get { return false; } }
        public CmdReport() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (!Directory.Exists("extra/reports/")) { Directory.CreateDirectory("extra/reports/"); }
            // report format is: read/unread author of reoprt, time created, user reported, reason.
            if (message.Split(' ')[0].ToLower() == "view" || message.Split(' ')[0].ToLower() == "v")
            {
                if (p.group.Permission < LevelPermission.Operator && p != null) { Player.SendMessage(p, "Only players ranked " + Group.findPerm(LevelPermission.Operator).trueName + " and higher may use this command."); return; }
                if (message.Split(' ').Length == 1)
                {
                    string unread = "";
                    string fileList = "";
                    foreach (string s in Directory.GetFiles("extra/reports/"))
                    {
                        fileList += ", " + s.Replace(".report", "").Replace("extra/reports/", "");
                        if (File.ReadAllLines(s)[0].ToLower() == "unread")
                            unread += ", " + s.Replace(".report", "").Replace("extra/reports/", "");
                    }
                    if (fileList == "") { fileList = "..None."; }
                    if (unread == "") { unread = "..None"; }
                    Player.SendMessage(p, "Type /report view <id> to view a specific report.");
                    Player.SendMessage(p, "Viewable report ids: &c" + fileList.Substring(2));
                    Player.SendMessage(p, "Unread report ids: &c" + unread.Substring(2));
                }
                else if (message.Split(' ').Length == 2)
                {
                    if (File.Exists("extra/reports/" + message.Split(' ')[1] + ".report"))
                    {
                        Player.SendMessage(p, "Viewing report " + message.Split(' ')[1] + ":");
                        for (int i = 0; i < File.ReadAllLines("extra/reports/" + message.Split(' ')[1] + ".report").Length; i++ )
                            if (i > 1)
                                Player.SendMessage(p, File.ReadAllLines("extra/reports/" + message.Split(' ')[1] + ".report")[i]);
                        if (File.ReadAllLines("extra/reports/" + message.Split(' ')[1] + ".report")[0].ToLower() == "unread")
                        {
                            string[] lines = File.ReadAllLines("extra/reports/" + message.Split(' ')[1] + ".report");
                            lines[0] = "read";
                            File.WriteAllLines("extra/reports/" + message.Split(' ')[1] + ".report", lines);
                        }
                    }
                    else Player.SendMessage(p, "Report could not be found.");
                }
            }
            else if (message.Split(' ')[0].ToLower() == "delete" || message.Split(' ')[0].ToLower() == "del" || message.Split(' ')[0].ToLower() == "d")
            {
                if (p.group.Permission < LevelPermission.Operator && p != null) { Player.SendMessage(p, "Only players ranked " + Group.findPerm(LevelPermission.Operator).trueName + " and higher may use this command."); return; }
                if (message.Split(' ')[1].ToLower() == "all") 
                {
                    foreach (string s in Directory.GetFiles("extra/reports/"))
                        File.Delete(s);
                    Player.SendMessage(p, "Deleted all reports.");
                    return;
                }
                if (File.Exists("extra/reports/" + message.Split(' ')[1] + ".report"))
                {
                    File.Delete("extra/reports/" + message.Split(' ')[1] + ".report");
                    Player.SendMessage(p, "Deleted report.");
                }
                else Player.SendMessage(p, "Report could not be found.");
            }
            else
            {
                string toReport = "";
                Player who = Player.Find(message.Split(' ')[0]);
                if (who != null) toReport = who.name;
                else toReport = message.Split(' ')[0];
                int newID = 1;
                foreach (string s in Directory.GetFiles("extra/reports/"))
                {
                    int parsed = 0;
                    if (int.TryParse(s.Replace(".report", "").Replace("extra/reports/", ""), out parsed))
                        if (parsed >= newID)
                            newID = parsed + 1;
                }
                
                File.WriteAllLines("extra/reports/" + newID + ".report", new string[] {
                    "unread",
                    "Report by: &c" + p.name,
                    "Time created: &c" + DateTime.Now.ToString(),
                    "User reported: &c" + toReport,
                    "Reason: &c" + message.Substring(message.IndexOf(" ") + 1).Trim()
                });
                Player.SendMessage(p, "Your report has been sent to the operators.");
                Player.GlobalMessageOps("To Ops: " + p.color + p.name + Server.DefaultColor + " has submitted a report.");
                Player.GlobalMessageOps("Type &c/report view " + newID + Server.DefaultColor + " to view.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/report <name> <reason> - Report a user to the server operators.");
            if (p.group.Permission >= LevelPermission.Operator || p == null)
            {
                Player.SendMessage(p, "/report view - View all reports.");
                Player.SendMessage(p, "/report view <id> - View a specific report.");
                Player.SendMessage(p, "/report delete <id/all> - Delete a report, or delete all reports.");
            }
        }
    }
}