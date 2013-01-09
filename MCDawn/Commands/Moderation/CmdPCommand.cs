using System;

namespace MCDawn
{
    public class CmdPCommand : Command
    {
        public override string name { get { return "pcommand"; } }
        public override string[] aliases { get { return new string[] { "pcmd", "sendcmd", "scmd" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }

        public override void Use(Player p, string message)
        {
            bool exec = false;
            int count = 0;
            foreach (char c in message)
            {
                if (c == ' ') count++;
            }
            string[] words = message.Split(new char[] { ' ' }, 3);
            Player plr = Player.Find(words[0]);
            if (plr == null)
            {
                Player.SendMessage(p, "Player could not be found.");
                return;
            }
            if (Server.devs.Contains(plr.name.ToLower()) || Server.staff.Contains(plr.name.ToLower()) || Server.administration.Contains(plr.name.ToLower()) && (!Server.devs.Contains(p.name.ToLower()) && p != null)) { Player.SendMessage(p, "Can't let you do that, starfox."); return; }
            if (plr.devUnverified || plr.unverified) { Player.SendMessage(p, "Cannot send commands to unverified player in Admin Security System."); return; }
            if (p != null && plr.group.Permission >= p.group.Permission)
            {
                if (plr.hidden)
                {
                    Player.SendMessage(p, "Player could not be found.");
                    return;
                }
                Player.SendMessage(p, "You can't send commands to player with a same or higher rank!");
                return;
            }
            if (count < 1)
            {
                Player.SendMessage(p, "No command entered.");
                return;
            }
            if (count >= 1)
            {
                if (p == null)
                {
                    exec = true;
                }
                if (p != null)
                {
                    if (p.group.CanExecute(all.Find(words[1])))
                    {
                        exec = true;
                    }
                }
                if (!exec)
                {
                    Player.SendMessage(p, "You can't send commands that you aren't allowed to use!");
                    return;
                }
            }
            try
            {
                if (count == 1 && exec)
                {
                    all.Find(words[1]).Use(plr, "");
                    Player.SendMessage(p, "You have successfully sent the command to " + plr.color + plr.name + "&g.");
                    return;
                }
                if (count > 1 && exec)
                {
                    all.Find(words[1]).Use(plr, words[2]);
                    Player.SendMessage(p, "You have successfully sent the command to " + plr.color + plr.name + "&g.");
                    return;
                }
            }
            catch
            {
                Player.SendMessage(p, "Error sending command to player.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/pcommand <name> <command> - Send a command to another player.");
        }
    }
}