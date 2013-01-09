using System;

namespace MCDawn
{
    public class CmdInvincible : Command
    {
        public override string name { get { return "invincible"; } }
        public override string[] aliases { get { return new string[] { "inv" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdInvincible() { }

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
            	if ((who.group.Permission > p.group.Permission) || (Server.devs.Contains(who.name.ToLower()) && !Server.devs.Contains(p.name.ToLower())) && p != null)
            	{
            	    Player.SendMessage(p, "Cannot toggle invincibility for someone of higher rank");
            	    return;
            	}
            }

            if (who.invincible == true)
            {
                who.invincible = false;
                if (Server.unCheapMessage)
                    Player.GlobalMessage(who.color + who.name + "&g " + Server.unCheapMessageGiven);
            }
            else
            {
                who.invincible = true;
                if (Server.cheapMessage)
                    Player.GlobalMessage(who.color + who.name + "&g " + Server.cheapMessageGiven);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/invincible [name] - Turns invincible mode on/off.");
            Player.SendMessage(p, "If [name] is given, that player's invincibility is toggled");
        }
    }
}