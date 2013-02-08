using System;

namespace MCDawn
{
    public class CmdTpRequest : Command
    {
        public override string name { get { return "tprequest"; } }
        public override string[] aliases { get { return new string[] { "tpr" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdTpRequest() { }

        public override void Use(Player p, string message)
        {
            Player who = Player.Find(message);
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            if (message == "") { Help(p); return; }
            if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (who == p) { Player.SendMessage(p, "Cannot send summon request to yourself."); return; }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "You can't leave an Infection game!"); return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on that map, you can't go to it!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "You can't leave a Spleef game!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on that map, you can't go to it!"); return; }
            who.tpRequest = p.name;
            Player.SendMessage(p, "Teleportation request sent to " + who.color + who.name + "&g.");
            Player.SendMessage(who, "You have recieved a teleport request from " + p.color + p.name + "&g.");
            Player.SendMessage(who, "Type /tpaccept to accept this request, or /tpdeny to deny this request.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tprequest <player> - Send a teleportation request to <player>.");
        }
    }
}