using System;

namespace MCDawn
{
    class CmdGbUpdate : Command
    {
        public override string name { get { return "gbupdate"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdGbUpdate() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower().Trim() == "list")
            {
                string barned = "";
                Server.GlobalBanned().ForEach(delegate(string s) { barned += s + ", "; });
                Player.SendMessage(p, barned.Remove(barned.Length - 2));
                return;
            }
            Server.GlobalBanned();
            Player.SendMessage(p, "MCDawn Global-Ban List updated.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/gbupdate - Updates the MCDawn Global-Ban list.");
            Player.SendMessage(p, "/gbupdate list - Shows the current list of global-banned users on the server.");
        }
    }
}
