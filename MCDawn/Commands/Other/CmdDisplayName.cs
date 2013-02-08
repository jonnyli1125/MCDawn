using System;

namespace MCDawn
{
    public class CmdDisplayName : Command
    {
        public override string name { get { return "displayname"; } }
        public override string[] aliases { get { return new string[] { "dname", "nick", "nickname" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdDisplayName() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            int pos = message.IndexOf(' ');
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Could not find player."); return; }
            if (p != null && who.group.Permission > p.group.Permission)
            {
                Player.SendMessage(p, "Cannot change the display name of someone of greater rank");
                return;
            }
            string query;
            string newName = "";
            if (message.Split(' ').Length > 1) newName = message.Substring(pos + 1);
            else
            {
                who.displayName = who.originalName;
                Player.GlobalChat(who, who.color + who.prefix + who.displayName + "&g has reverted their display name to their original name.", false);
                query = "UPDATE Players SET displayName = '" + who.originalName + "' WHERE Name = '" + who.originalName + "'";
                MySQL.executeQuery(query);
                Player.GlobalDie(p, false);
                Player.GlobalSpawn(p, p.pos[0], p.pos[1], p.pos[2], p.rot[0], p.rot[1], false);
                return;
            }

            if (newName.Length > 60) { Player.SendMessage(p, "Display Name must be under 60 letters."); return; }
            if (p == null || !Server.devs.Contains(p.originalName.ToLower()))
            {
                if (Server.devs.Contains(who.originalName.ToLower()) || Server.devs.Contains(Player.RemoveAllColors(newName).Trim().ToLower())) { Player.SendMessage(p, "Can't let you do that, starfox."); return; }
            }

            if (newName != "") Player.GlobalChat(who, who.color + who.displayName + "&g has changed their display name to " + newName + "&g.", false);

            if (newName == "")
            {
                query = "UPDATE Players SET displayName = '" + who.originalName + "' WHERE Name = '" + who.originalName + "'";
            }
            else
            {
                query = "UPDATE Players SET displayName = '" + newName.Replace("'", "\\'") + "' WHERE Name = '" + who.originalName + "'";
            }
            MySQL.executeQuery(query);
            who.displayName = newName;
            Player.GlobalDie(who, false);
            Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/displayname <player> [newName] - Gives <player> the display name of [newName].");
            Player.SendMessage(p, "If no [newName] is given, the player's display name is reverted to their original name.");
        }
    }
}