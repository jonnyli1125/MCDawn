using System;

namespace MCDawn
{
    public class CmdSummonDeny : Command
    {
        public override string name { get { return "summondeny"; } }
        public override string[] aliases { get { return new string[] { "summond" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdSummonDeny() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { p.SendMessage("Command not usable in Console."); return; }
            Player who = Player.Find(p.tpRequest);
            if (p.summonRequest == "") { Player.SendMessage(p, "No summon requests have been recieved."); return; }
            if (who == null) { Player.SendMessage(p, "Player is no longer online!"); p.summonRequest = ""; return; }
            if (who == p) { Player.SendMessage(p, "Cannot deny summon request from yourself."); p.summonRequest = ""; return; }
            p.summonRequest = "";
            Player.SendMessage(p, "Denied summon request from " + who.color + who.name + "&g.");
            Player.SendMessage(who, "Your summon request to " + p.color + p.name + "&g has been denied.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summondeny - Deny the teleport request last sent to you.");
        }
    }
}