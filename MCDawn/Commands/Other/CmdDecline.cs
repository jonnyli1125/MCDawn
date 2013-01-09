using System;

namespace MCDawn
{
    class CmdDecline : Command
    {
        public override string name { get { return "decline"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdDecline() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message);
            if (p == null) { p.SendMessage("Command not usable from Console."); return; }
            if (who == null) { p.SendMessage("Player could not be found."); return; }
            if (who == p) { Player.SendMessage(p, "You cannot review yourself, therefore you cannot decline yourself."); }
            else 
            { 
                Player.SendMessage(who, "Your build has been reviewed by an Operator and has been declined for promotion.");
                Player.GlobalMessageOps("To Ops: &3REVIEW: " + who.color + who.name + "&g's build was declined by " + p.color + p.name + "&g!");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/decline <player> - Decline a Player's review request.");
        }
    }
}