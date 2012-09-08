using System;

namespace MCDawn
{
    public class CmdTpDeny : Command
    {
        public override string name { get { return "tpdeny"; } }
        public override string[] aliases { get { return new string[] { "tpd" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdTpDeny() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            Player who = Player.Find(p.tpRequest);
            if (p.tpRequest == "") { Player.SendMessage(p, "No teleportation requests have been recieved."); return; }
            if (who == null) { Player.SendMessage(p, "Player is no longer online!"); p.tpRequest = ""; return; }
            if (who == p) { Player.SendMessage(p, "Cannot deny teleport request from yourself."); p.tpRequest = ""; return; }
            p.tpRequest = "";
            Player.SendMessage(p, "Denied teleportation request from " + who.color + who.name + "&g.");
            Player.SendMessage(who, "Your teleportation request to " + p.color + p.name + "&g has been denied.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/tpdeny - Deny the teleport request last sent to you.");
        }
    }
}