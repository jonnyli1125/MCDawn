using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MCDawn
{
    class CmdOpRules : Command
    {
        public override string name { get { return "oprules"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdOpRules() { }

        public override void Use(Player p, string message)
        {
            List<string> rules = new List<string>();
            if (!File.Exists("text/oprules.txt"))
            {
                File.WriteAllText("text/oprules.txt", "No oprules entered yet!");
            }
            StreamReader r = File.OpenText("text/oprules.txt");
            while (!r.EndOfStream)
                rules.Add(r.ReadLine());

            r.Close();
            r.Dispose();

            Player who = null;
            if (message != "")
            {
                if (p.group.Permission <= LevelPermission.Guest)
                { Player.SendMessage(p, "You cant send /oprules to another player!"); return; }
                who = Player.Find(message);
            }
            else
            {
                who = p;
            }

            if (who != null)
            {
                who.SendMessage("Server OPRules:");
                foreach (string s in rules)
                    who.SendMessage(s);
            }
            else
            {
                Player.SendMessage(p, "There is no player \"" + message + "\"!");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/oprules [player]- Displays server oprules to a player");
        }
    }
}
