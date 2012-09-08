using System;
using System.IO;

namespace MCDawn
{
    public class CmdDeafen : Command
    {
        public override string name { get { return "deafen"; } }
        public override string[] aliases { get { return new string[] { "deaf" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdDeafen() { }

        public override void Use(Player p, string message)
        {
            Player who;
            if (message != "")
            {
                who = Player.Find(message);
            }
            else
            {
                who = p;
            }

            if (who == null)
            {
                Player.SendMessage(p, "Cannot find player.");
                return;
            }

            if (p != null && (who.group.Permission > p.group.Permission) || (Server.devs.Contains(who.name.ToLower()) && !Server.devs.Contains(p.name.ToLower())))
            {
                Player.SendMessage(p, "Cannot toggle deaf status for someone of higher rank");
                return;
            }

            if (who.deafened == true)
            {
                who.deafened = false;
                Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " has been undeafened");
            }
            else
            {
                who.deafened = true;
                Player.GlobalMessage(who.color + who.name + Server.DefaultColor + " has been deafened");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/deafen [name] - Turns deaf mode on/off.");
            Player.SendMessage(p, "If [name] is given, that player's deaf status is toggled");
            Player.SendMessage(p, "Turning this on will make [name] unable to see any chat messages.");
        }
    }
}