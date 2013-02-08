using System.Collections.Generic;
using System.IO;

namespace MCDawn
{
    class CmdRules : Command
    {
        public override string name { get { return "rules"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdRules() { }

        public override void Use(Player p, string message)
        {
            List<string> rules = new List<string>();
            if (!File.Exists("text/rules.txt"))
            {
                File.WriteAllText("text/rules.txt", "No rules entered yet!");
            }
            StreamReader r = File.OpenText("text/rules.txt");
            while (!r.EndOfStream)
                rules.Add(r.ReadLine());

            r.Close();
            r.Dispose();

            Player who = null;
            if (message != "")
            {
                if (p.group.Permission <= LevelPermission.Guest)
                { Player.SendMessage(p, "You cant send /rules to another player!"); return; }
                who = Player.Find(message);
            }

            else who = p;

            if ((p == null && who == null) || who != null)
            {
                Player.SendMessage(who, "Server Rules:");
                foreach (string s in rules)
                    Player.SendMessage(who, s);
                if (p != null)
                {
                    if (Server.agreePass.Split(' ').Length > 1) { Server.agreePass = Server.agreePass.Split(' ')[0]; }
                    if (who != p) { p.SendMessage("Sent /rules to " + who.color + who.name); }
                    if (Server.agreeToRules && Server.agreedToRules.Contains(who.name)) { Clear(who); who.SendMessage("You have already agreed to the rules."); return; }
                    if (Server.agreeToRules && Server.agreePass == "") { Clear(who); who.SendMessage("If you agree to the rules, please type &b/agree" + "&g. If not, type &b/disagree" + "&g."); }
                    if (Server.agreeToRules && Server.agreePass.Length > 0) { Clear(who); who.SendMessage("If you agree to the rules, please type &b/agree " + Server.agreePass + "&g. If not, type &b/disagree" + "&g."); }
                    if (!p.readRules) { p.readRules = true; }
                }
            }
            else Player.SendMessage(p, "There is no player \"" + message + "\"!");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rules [player]- Displays server rules to a player");
        }

        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        public byte[] Format(string str, int size)
        {
            byte[] b = new byte[size];
            b = enc.GetBytes(str.PadRight(size).Substring(0, size));
            return b;
        }
        public void Clear(Player p)
        {
            byte[] buffer = new byte[65];
            Format(" ", 64).CopyTo(buffer, 1);
            p.SendRaw(13, buffer);
            buffer = null;
        }
    }
}
