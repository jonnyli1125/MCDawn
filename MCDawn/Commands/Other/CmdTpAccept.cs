using System;

namespace MCDawn
{
    public class CmdTpAccept : Command
    {
        public override string name { get { return "tpaccept"; } }
        public override string[] aliases { get { return new string[] { "tpa" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdTpAccept() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            Player who = Player.Find(p.tpRequest);
            if (p.tpRequest == "") { Player.SendMessage(p, "No teleportation requests have been recieved."); return; }
            if (who == null) { Player.SendMessage(p, "Player is no longer online!"); p.tpRequest = ""; return; }
            if (who == p) { Player.SendMessage(p, "Cannot accept teleport request from yourself."); p.tpRequest = ""; return; }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "You can't leave an Infection game!"); return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on that map, you can't go to it!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "You can't leave a Spleef game!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on that map, you can't go to it!"); return; }

            if (p.level != who.level)
            {
                if (p.level.name.Contains("cMuseum")) { Player.SendMessage(p, "Player \"" + who.name + "\" is in a museum!"); return; }
                else
                {
                    Command.all.Find("goto").Use(who, p.level.name);
                    while (who.Loading) { }
                    unchecked { who.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0); }
                    //Command.all.Find("tp").Use(p, who.name);
                }
            }
            if (p.level == who.level)
            {
                if (who.Loading)
                {
                    Player.SendMessage(p, "Waiting for " + who.color + who.name + Server.DefaultColor + " to spawn...");
                    while (who.Loading) { }
                }
                while (who.Loading) { }  //Wait for player to spawn in new map
                unchecked { who.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tpaccept - Accept the teleport request last sent to you.");
        }
    }
}