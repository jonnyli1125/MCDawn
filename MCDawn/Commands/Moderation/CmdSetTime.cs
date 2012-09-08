//Written by jonnyli1125 for MCDawn.
using System;

namespace MCDawn
{
    public class CmdSetTime : Command
    {
        public override string name { get { return "settime"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdSetTime() { }

        public override void Use(Player p, string message)
        {
            bool anotherPlayer = false;
            if (message.Split(' ').Length < 3 && (message.ToLower() != "reset" || message.Split(' ')[1].ToLower() != "reset")) { Help(p); return; }
            if (message.Split(' ').Length > 3) { anotherPlayer = true; }
            if (message.Split(' ').Length > 4) { Help(p); return; }
            int days = 0;
            int hours = 0;
            int minutes = 0;
            if (message.ToLower() != "reset" || message.Split(' ')[1].ToLower() != "reset")
            {
                try
                {
                    if (!anotherPlayer)
                    {
                        days = Convert.ToInt32(message.Split(' ')[0]);
                        hours = Convert.ToInt32(message.Split(' ')[1]);
                        minutes = Convert.ToInt32(message.Split(' ')[2]);
                    }
                    else
                    {
                        days = Convert.ToInt32(message.Split(' ')[1]);
                        hours = Convert.ToInt32(message.Split(' ')[2]);
                        minutes = Convert.ToInt32(message.Split(' ')[3]);
                    }
                    if (hours > 23) { Player.SendMessage(p, "Hours cannot be greater than 23."); return; }
                    if (minutes > 59) { Player.SendMessage(p, "Minutes cannot be greater than 59."); return; }
                }
                catch { p.SendMessage("Invalid Days/Hours/Minutes."); return; }
            }
            if (!anotherPlayer)
            {
                p.timeSpent = days + " " + hours + " " + minutes + " 1";
                // MySQL Save timeSpent.
                MySQL.executeQuery("UPDATE Players SET TimeSpent='" + p.timeSpent + "' WHERE Name='" + p.originalName + "'");
                p.SendMessage("Your time spent was set at: &a" + days + " Days, " + hours + " Hours, " + minutes + " Minutes.");
            }
            else
            {
                Player who = Player.Find(message.Split(' ')[0]);
                if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
                if (p != null && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "Cannot change player of higher rank's time spent."); return; }
                who.timeSpent = days + " " + hours + " " + minutes + " 1";
                MySQL.executeQuery("UPDATE Players SET TimeSpent = '" + who.timeSpent + "' WHERE Name = '" + who.originalName + "'");
                if (who != p)
                {
                    Player.SendMessage(p, who.color + who.name + Server.DefaultColor + "'s time spent was set at: &a" + days + " Days, " + hours + " Hours, " + minutes + " Minutes.");
                    if (p != null) { Player.SendMessage(who, "Your time spent was set at: &a" + days + " Days, " + hours + " Hours, " + minutes + " Minutes " + Server.DefaultColor  +"by " + p.color + p.name + Server.DefaultColor + "."); }
                    else { Player.SendMessage(who, "Your time spent was set at: &a" + days + " Days, " + hours + " Hours, " + minutes + " Minutes " + Server.DefaultColor + "by the Console."); }
                }
                else { Player.SendMessage(p, "Your time spent was set at: &a" + days + " Days, " + hours + " Hours, " + minutes + " Minutes."); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/settime [player] <days> <hours> <minutes> - Set the time spent of [player].");
            Player.SendMessage(p, "/settime [player] reset - Remove all time spent from [player].");
        }
    }
}