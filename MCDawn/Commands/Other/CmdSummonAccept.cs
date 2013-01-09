using System;

namespace MCDawn
{
    public class CmdSummonAccept : Command
    {
        public override string name { get { return "summonaccept"; } }
        public override string[] aliases { get { return new string[] { "summona" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdSummonAccept() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            Player who = Player.Find(p.summonRequest);
            if (p.summonRequest == "") { Player.SendMessage(p, "No summon requests have been recieved."); return; }
            if (who == null) { Player.SendMessage(p, "Player is no longer online!"); p.tpRequest = ""; return; }
            if (who == p) { Player.SendMessage(p, "Cannot accept summon request from yourself."); p.summonRequest = ""; return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "They can't leave an Infection game!"); return; }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on this map, they can't come here!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "They can't leave a Spleef game!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on this map, they can't come here!"); return; }

            if (p.level != who.level)
            {
                if (who.level.name.Contains("cMuseum")) { Player.SendMessage(p, "Player \"" + who.name + "\" is in a museum!"); return; }
                else
                {
                    Command.all.Find("goto").Use(p, who.level.name);
                    while (p.Loading) { }
                    unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], 0); }
                    //Command.all.Find("summon").Use(who, p.name);
                }
            }
            if (p.level == who.level)
            {
                if (who.Loading)
                {
                    Player.SendMessage(p, "Waiting for " + who.color + who.name + "&g to spawn...");
                    while (who.Loading) { }
                }
                while (p.Loading) { }  //Wait for player to spawn in new map
                unchecked { p.SendPos((byte)-1, who.pos[0], who.pos[1], who.pos[2], who.rot[0], 0); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summonaccept - Accept the teleport request last sent to you.");
        }
    }
}