using System;

namespace MCDawn
{
    class CmdObUpdate : Command
    {
        public override string name { get { return "obupdate"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdObUpdate() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower().Trim() == "list")
            {
                string barned = "";
                Server.OmniBanned().ForEach(delegate(string s) { barned += s + ", "; });
                Player.SendMessage(p, barned.Remove(barned.Length - 2));
                return;
            }
            Server.OmniBanned();
            foreach (Player pl in Player.players)
            {
                bool omniBanned = false;
                for (int i = 0; i < Server.OmniBanned().Count; i++)
                    if (Server.OmniBanned().Contains("*") && (pl.name.ToLower().StartsWith(Server.OmniBanned()[i].ToLower().Replace("*", "")) || pl.ip.StartsWith(Server.OmniBanned()[i].ToLower().Replace("*", ""))))
                        omniBanned = true;
                if (Server.OmniBanned().Contains(pl.name.ToLower()) || Server.OmniBanned().Contains(pl.ip) || omniBanned)
                    pl.Kick("You have been Omni-banned. Visit www.mcdawn.com for appeal."); 
            }
            Player.SendMessage(p, "MCDawn Omni-Ban List updated.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/obupdate - Updates the MCDawn Omni-Ban list.");
            Player.SendMessage(p, "/obupdate list - Shows the current list of omni-banned users on the server.");
        }
    }
}
