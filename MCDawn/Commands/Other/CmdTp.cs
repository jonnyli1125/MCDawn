using System;
using System.IO;
using System.Threading;

namespace MCDawn
{
    public class CmdTp : Command
    {
        public override string name { get { return "tp"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdTp() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                Command.all.Find("spawn");
                return;
            }
            Player who = Player.Find(message);
            if (who == null || (who.hidden && p.group.Permission < LevelPermission.Admin)) { Player.SendMessage(p, "There is no player \"" + message + "\"!"); return; }
            if (p.group.Permission < LevelPermission.Operator && who.level.name != Server.HomePrefix + p.name.ToLower() && p.level.name != Server.HomePrefix + p.name.ToLower()) { if (p.level.locked || who.level.locked) { p.SendMessage("This map is currently locked!"); return; } }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "You can't leave an Infection game!"); return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on that map, you can't go to it!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "You can't leave a Spleef game!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on that map, you can't go to it!"); return; }
            if (p.level != who.level)
            {
                if (Server.tpToHigher == false && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "You can't teleport to someone of a higher rank!"); return; }
                if (who.level.name.Contains("cMuseum")) { Player.SendMessage(p, "Player \"" + message + "\" is in a museum!"); return; }
                else
                {
                    Command.all.Find("goto").Use(p, who.level.name);
                    while (p.Loading) { }
                    unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], 0); }
                    Command.all.Find("tp").Use(p, who.name);
                }
            }
            if (p.level == who.level)
            {
                if (Server.tpToHigher == false && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "You can't teleport to someone of a higher rank!"); }
                else
                {
                    if (who.Loading)
                    {
                        Player.SendMessage(p, "Waiting for " + who.color + who.name + Server.DefaultColor + " to spawn...");
                        while (who.Loading) { }
                    }
                    while (p.Loading) { }  //Wait for player to spawn in new map
                    unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], 0); }
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tp <player> - Teleports yourself to a player.");
            Player.SendMessage(p, "If <player> is blank, /spawn is used.");
        }
    }
}