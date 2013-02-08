using System;
using System.Data;

namespace MCDawn
{
    public class CmdViewLikes : Command
    {
        public override string name { get { return "viewlikes"; } }
        public override string[] aliases { get { return new string[] { "viewlike", "showlikes", "showlike" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdViewLikes() { }

        public override void Use(Player p, string message)
        {
            if (!Server.useMySQL) { Player.SendMessage(p, "MySQL has not been configured! Please configure MySQL to use /viewlikes!"); return; }
            if (!Server.enableMapLiking) { Player.SendMessage(p, "Map liking is disabled."); return; }
            if (p == null && String.IsNullOrEmpty(message)) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (message.Split(' ').Length > 1) { Help(p); return; }
            Level l = (String.IsNullOrEmpty(message) ? p.level : Level.Find(message));
            if (l == null) { Player.SendMessage(p, "Level could not be found or is not loaded."); return; }
            DataTable likes = MySQL.fillData("SELECT * FROM `Likes" + l.name + "`");
            if (likes.Rows.Count == 0) { Player.SendMessage(p, "Nobody likes this level yet!"); }
            else
            {
                string people = "";
                int maxcount = (likes.Rows.Count < 3 ? likes.Rows.Count : 3);
                for (int i = 0; i < maxcount; i++)
                    people += Group.Find(Group.findPlayer(likes.Rows[i]["Username"].ToString().ToLower())).color + likes.Rows[i]["Username"].ToString() + "&g" + (maxcount >= 2 && i == maxcount - 2 && (likes.Rows.Count == maxcount) ? " and " : (i == maxcount - 1 ? "" : ",") + (i == maxcount - 1 && likes.Rows.Count != maxcount ? "" : " "));
                if (maxcount != likes.Rows.Count)
                    people += " and " + (likes.Rows.Count - maxcount) + (likes.Rows.Count - maxcount == 1 ? " other person " : " others ");
                people += (likes.Rows.Count == 1 ? "likes" : "like") + " this level.";
                Player.SendMessage(p, people);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/viewlikes [level] - Shows likes on [level]. If [level] is not given, the current level is used.");
        }
    }
}