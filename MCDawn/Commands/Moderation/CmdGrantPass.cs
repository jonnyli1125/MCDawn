using System;

namespace MCDawn
{
    public class CmdGrantPass : Command
    {
        public override string name { get { return "grantpass"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdGrantPass() { }
        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message);
            if (who == null) { p.SendMessage("Player could not be found!"); return; }
            // Devs and Staff
            if (who.devUnverified) { Player.SendMessage(p, "Can't let you do that, Starfox."); return; }
            if (who.unverified == false)
            {
                Player.SendMessage(p, "Only applicable to those in the Admin Security System.");
            }
            else
            {
                who.grantpassed = true;
                Player.SendMessage(who, "You have been granted /setpass access.");
                Player.SendMessage(p, "You have granted " + who.color + who.name + Server.DefaultColor + " /setpass access.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "&3/grantpass <player>" + Server.DefaultColor + " - Grant a player /setpass access.");
        }
    }
}