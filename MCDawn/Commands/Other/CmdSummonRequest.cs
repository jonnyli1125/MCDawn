using System;

namespace MCDawn
{
    public class CmdSummonRequest : Command
    {
        public override string name { get { return "summonrequest"; } }
        public override string[] aliases { get { return new string[] { "summonr" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdSummonRequest() { }

        public override void Use(Player p, string message)
        {
            Player who = Player.Find(message);
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            if (message == "") { Help(p); return; }
            if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (who == p) { Player.SendMessage(p, "Cannot send summon request to yourself."); return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "They can't leave an Infection game!"); return; }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on this map, they can't come here!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "They can't leave a Spleef game!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on this map, they can't come here!"); return; }
            who.summonRequest = p.name;
            Player.SendMessage(p, "Summon request sent to " + who.color + who.name + "&g.");
            Player.SendMessage(who, "You have recieved a summon request from " + p.color + p.name + "&g.");
            Player.SendMessage(who, "Type /summonaccept to accept this request, or /summondeny to deny this request.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summonrequest <player> - Send a summoning request to <player>.");
        }
    }
}