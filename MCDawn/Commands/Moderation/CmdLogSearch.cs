using System;
using System.IO;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdLogSearch : Command
    {
        public override string name { get { return "logsearch"; } }
        public override string[] aliases { get { return new string[] { "ls", "lsearch" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdLogSearch() { }

        public override void Use(Player p, string message)
        {
            try
            {
                List<string> toSend = new List<string>();
                List<string> lines = new List<string>(File.ReadAllLines(Logger.LogPath));
                lines.Reverse();
                lines.ForEach(delegate(string line) {
                    if (line.ToLower().Contains(message.ToLower()) && line != null && toSend.Count < 20) { toSend.Add(line); }
                });
                if (toSend.Count <= 0) { Player.SendMessage(p, "No results found."); return; }
                toSend.Reverse();
                Player.SendMessage(p, "Search Results for " + message + " (Showing last 20 results):");
                Player.SendMultiple(p, toSend);
            }
            catch { Player.SendMessage(p, "Logsearch failed."); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/logsearch <search term> - Returns last 20 results of lines that contain <search term> in the current log.");
        }
    }
}