using System;
using System.IO;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdLoginMessage : Command
    {
        public override string name { get { return "loginmessage"; } }
        public override string[] aliases { get { return new string[] { "login" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdLoginMessage() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            if (message == "") { Help(p); return; }
            if (!File.Exists("extra/logins.txt")) { File.Create("extra/logins.txt").Close(); }
            if (message.ToLower() == "remove")
            {
                List<string> lul = new List<string>(File.ReadAllLines("extra/logins.txt"));
                lul.ForEach(delegate(string s) { if (s.Split(' ')[0].ToLower() == p.name.ToLower()) { lul.Remove(s); } });
                File.WriteAllLines("extra/logins.txt", lul.ToArray());
                Player.SendMessage(p, "Custom login message removed.");
                return;
            }
            List<string> lines = new List<string>(File.ReadAllLines("extra/logins.txt"));
            lines.ForEach(delegate(string s) { if (s.Split(' ')[0].ToLower() == p.name.ToLower()) { lines.Remove(s); } });
            lines.Add(p.name.ToLower().Trim() + " " + message);
            File.WriteAllLines("extra/logins.txt", lines.ToArray());
            Player.SendMessage(p, "Login message set as: " + message);
            //p.loginmessage = message;
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/loginmessage <message> - Changes your login message to: &a+ &gPlayer <loginmessage>.");
            Player.SendMessage(p, "If <message> is \"remove\", then the custom login message is removed.");
        }
    }
}