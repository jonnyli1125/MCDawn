using System;
using System.IO;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdLogoutMessage : Command
    {
        public override string name { get { return "logoutmessage"; } }
        public override string[] aliases { get { return new string[] { "logout" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdLogoutMessage() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            if (message == "") { Help(p); return; }
            if (!File.Exists("extra/logouts.txt")) { File.Create("extra/logouts.txt").Close(); }
            if (message.ToLower() == "remove")
            {
                List<string> lul = new List<string>(File.ReadAllLines("extra/logouts.txt"));
                lul.ForEach(delegate(string s) { if (s.Split(' ')[0].ToLower() == p.name.ToLower()) { lul.Remove(s); } });
                File.WriteAllLines("extra/logouts.txt", lul.ToArray());
                Player.SendMessage(p, "Custom logout message removed.");
                return;
            }
            List<string> lines = new List<string>(File.ReadAllLines("extra/logouts.txt"));
            lines.ForEach(delegate(string s) { if (s.Split(' ')[0].ToLower() == p.name.ToLower()) { lines.Remove(s); } });
            lines.Add(p.name.ToLower().Trim() + " " + message);
            File.WriteAllLines("extra/logouts.txt", lines.ToArray());
            Player.SendMessage(p, "Logout message set as: " + message);
            //p.logoutmessage = message;
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/logoutmessage <message> - Changes your logout message to: &c- &gPlayer <logoutmessage>.");
            Player.SendMessage(p, "If <message> is \"remove\", then the custom logout message is removed.");
        }
    }
}