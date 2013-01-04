using System;

namespace MCDawn
{
    public class CmdUnLike : Command
    {
        public override string name { get { return "unlike"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdUnLike() { }

        public override void Use(Player p, string message)
        {
            if (!Server.useMySQL) { Player.SendMessage(p, "MySQL has not been configured! Please configure MySQL to use /unlike!"); return; }
            if (!Server.enableMapLiking) { Player.SendMessage(p, "Map liking is disabled."); return; }
            if (p == null) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (message.Split(' ').Length > 1) { Help(p); return; }
            Level l = (String.IsNullOrEmpty(message) ? p.level : Level.Find(message));
            if (l == null) { Player.SendMessage(p, "Level could not be found or is not loaded."); return; }
            if (MySQL.fillData("SELECT * FROM Likes" + l.name + " WHERE Username='" + p.name + "'").Rows.Count == 0) { Player.SendMessage(p, "You have not liked this map yet."); return; }
            MySQL.executeQuery("DELETE FROM Likes" + l.name + " WHERE Username='" + p.name + "'");
            l.likes--;
            Player.SendMessage(p, "Unliked map &a" + l.name + "&g!");
            Command.all.Find("viewlikes").Use(p, "");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/unlike [level] - Undo your like on [level]. If [level] is not given, the current level is used.");
        }
    }
}