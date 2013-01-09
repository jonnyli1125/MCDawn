using System;

namespace MCDawn
{
    public class CmdWarn : Command
    {
        public override string name { get { return "warn"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public override bool museumUsable { get { return false; } }
        string reason;
        public CmdWarn() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { p.SendMessage("Player could not be found!"); return; }
            if (Server.devs.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Developer!"); return; }
            if (Server.staff.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Staff Member!"); return; }
            if (Server.administration.Contains(who.name.ToLower())) { p.SendMessage("You can't warn a MCDawn Administrator!"); return; }
            if (who == p) { p.SendMessage("You can't warn yourself!"); return; }
            if (p != null && (p.group.Permission <= who.group.Permission)) { p.SendMessage("You can't warn a player of a same or higher rank!"); return; }
            else { reason = message.Substring(message.IndexOf(' ') + 1).Trim(); }
            if (p == null)
            {
                Player.GlobalMessage(who.color + who.name + "&g was warned by the Console!");
                if (message.Split(' ').Length > 1) { Player.GlobalMessage("Reason: " + reason); }
                Server.s.Log(who.name + " was warned by Console!");
                if (message.Split(' ').Length > 1) { Server.s.Log("Reason: " + reason); }
            }
            else
            {
                Player.GlobalMessage(who.color + who.name + "&g was warned by " + p.color + p.name + "&g!");
                if (message.Split(' ').Length > 1) { Player.GlobalMessage("Reason: " + reason); }
                Server.s.Log(who.name + " was warned by the Console!");
                if (message.Split(' ').Length > 1) { Server.s.Log("Reason: " + reason); }
            }

            if (who.warnings == 0)
            {
                who.warnings++;
                who.SendMessage("&cYou have been warned! You will be kicked on the next warning!");
                return;
            }
            if (who.warnings >= 1)
            {
                who.Kick("Exceeded maximum amount of warnings!");
                return;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/warn <player> <reason> - Warns a player.");
            Player.SendMessage(p, "Player will get kicked when they reach 2 warnings.");
        }
    }
}