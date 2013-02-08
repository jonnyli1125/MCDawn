using System;

namespace MCDawn
{
    public class CmdRevoke : Command
    {
        public override string name { get { return "revoke"; } }
        public override string[] aliases { get { return new string[] { "dewarn", "unwarn" }; } }
        public override string type { get { return "mod"; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public override bool museumUsable { get { return false; } }
        string reason;
        public CmdRevoke() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { p.SendMessage("Player could not be found!"); return; }
            /*if (Server.devs.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Developer!"); return; }
            if (Server.staff.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Staff Member!"); return; }
            if (Server.administration.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Administrator!"); return; }*/
            if (who == p) { p.SendMessage("You can't revoke warnings on yourself!"); return; }
            if (p != null && (p.group.Permission <= who.group.Permission)) { p.SendMessage("You can't revoke warnings on a player of a same or higher rank!"); return; }
            else { reason = message.Substring(message.IndexOf(' ') + 1).Trim(); }
            if (who.warnings == 0) { Player.SendMessage(p, "That player has not been warned yet."); return; }
            if (p == null)
            {
                Player.GlobalMessage(who.color + who.name + "&g's warning was revoked by the Console!");
                if (message.Split(' ').Length > 1) { Player.GlobalMessage("Reason: " + reason); }
                Server.s.Log(who.name + "'s warning was revoked by " + p.name + "!");
                if (message.Split(' ').Length > 1) { Server.s.Log("Reason: " + reason); }
            }
            else
            {
                Player.GlobalMessage(who.color + who.name + "&g's warning was revoked by " + p.color + p.name + "&g!");
                if (message.Split(' ').Length > 1) { Player.GlobalMessage("Reason: " + reason); }
                Server.s.Log(who.name + "'s warning was revoked by the Console!");
                if (message.Split(' ').Length > 1) { Server.s.Log("Reason: " + reason); }
            }
            if (who.warnings >= 1) { who.warnings = 0; }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/revoke <player> <reason> - Revoke a warning from <player>.");
        }
    }
}