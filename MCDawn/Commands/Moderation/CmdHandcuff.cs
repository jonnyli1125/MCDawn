using System;

namespace MCDawn
{
    public class CmdHandcuff : Command
    {
        public override string name { get { return "handcuff"; } }
        public override string[] aliases { get { return new string[] { "cuff" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdHandcuff() { }

        public override void Use(Player p, string message)
        {
            Player who;
            if (message != "")
                who = Player.Find(message);
            else
                who = p;

            if (who == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }

            if (p != null) {
            	if ((who.group.Permission >= p.group.Permission) || (Server.devs.Contains(who.name.ToLower()) && !Server.devs.Contains(p.name.ToLower())) && p != null)
            	{
            	    Player.SendMessage(p, "Cannot handcuff someone of higher or equal rank");
            	    return;
            	}
            }

            if (who.handcuffed == true)
            {
                who.handcuffed = false;
                if (p != null) Player.GlobalMessage(who.color + who.name + "&g had their handcuffs taken off by " + p.color + p.name + "&g!");
                else Player.GlobalMessage(who.color + who.name + "&g had their handcuffs taken off by the Console!");
            }
            else
            {
                who.handcuffed = true;
                if (p != null) Player.GlobalMessage(who.color + who.name + "&g was handcuffed by " + p.color + p.name + "&g!");
                else Player.GlobalMessage(who.color + who.name + "&g was handcuffed by the Console!");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/handcuff [name] - Prevent the player from building.");
            Player.SendMessage(p, "If [name] is given, that player's handcuff status is toggled");
        }
    }
}