using System;
using System.IO;
using System.Net;

namespace MCDawn
{
    public class CmdWhois : Command
    {
        public override string name { get { return "whois"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdWhois() { }

        public override void Use(Player p, string message)
        {
            Player who = null;
            if (message == "") { who = p; message = p.name; } else { who = Player.Find(message); }
            if (who != null && !who.hidden)
            {
                Player.SendMessage(p, who.color + who.name + Server.DefaultColor + " is on &b" + who.level.name);
                Player.SendMessage(p, who.color + who.prefix + who.name + Server.DefaultColor + " has :");
                Player.SendMessage(p, "> > the rank of " + who.group.color + who.group.name);
                Player.SendMessage(p, "> > the display name of " + who.color + who.displayName);
                Player.SendMessage(p, "> > &a" + who.money + Server.DefaultColor + " " + Server.moneys);
                Player.SendMessage(p, "> > &cdied &a" + who.overallDeath + Server.DefaultColor + " times");
                Player.SendMessage(p, "> > &bmodified &a" + who.overallBlocks + Server.DefaultColor + " blocks, &a" + who.loginBlocks + Server.DefaultColor + " since logging in.");
                //int ratio = (int)(Math.Round((decimal)(1 / (Math.Min(who.destroyedBlocks, (who.overallBlocks - who.destroyedBlocks)) / Math.Max(who.destroyedBlocks, (who.overallBlocks - who.destroyedBlocks))))));
                try { Player.SendMessage(p, "> > destroyed &a" + who.destroyedBlocks + Server.DefaultColor + " blocks, and built &a" + who.builtBlocks + Server.DefaultColor + "."); }
                catch { }
                // MaxMind
                string countryname = Server.iploopup.getCountry(IPAddress.Parse(who.ip)).getName();
                if (Server.useMaxMind) { Player.SendMessage(p, "> > logged in from country &a" + countryname); }
                Player.SendMessage(p, "> > time spent on server: &a" + who.timeSpent.Split(' ')[0] + " Days, " + who.timeSpent.Split(' ')[1] + " Hours, " + who.timeSpent.Split(' ')[2] + " Minutes.");
                string storedTime = Convert.ToDateTime(DateTime.Now.Subtract(who.timeLogged).ToString()).ToString("HH:mm:ss");
                Player.SendMessage(p, "> > been logged in for &a" + storedTime);
                Player.SendMessage(p, "> > first logged into the server on &a" + who.firstLogin.ToString("yyyy-MM-dd") + " at " + who.firstLogin.ToString("HH:mm:ss"));
                Player.SendMessage(p, "> > logged in &a" + who.totalLogins + Server.DefaultColor + " times, &c" + who.totalKicked + Server.DefaultColor + " of which ended in a kick.");
                Player.SendMessage(p, "> > " + Awards.awardAmount(who.name) + " awards");
                if (!who.haswom)
                    Player.SendMessage(p, "> > is not using &cWOM Game Client" + Server.DefaultColor + ".");
                else
                    Player.SendMessage(p, "> > is using &aWOM Game Client" + Server.DefaultColor + ", Version &a" + who.womversion + Server.DefaultColor + ".");
                // Last ranked/banned reason:
                if (who.lastRankReason.ToLower() != "none" && !String.IsNullOrEmpty(who.lastRankReason))
                {
                    if (Group.findPerm(LevelPermission.Banned).playerList.Contains(who.name))
                        Player.SendMessage(p, "> > last &8banned&g at &a" + who.lastRankReason.Split(']')[0].Substring(1) + "&g; Reason: &a" + who.lastRankReason.Substring(who.lastRankReason.IndexOf("]") + 1).Trim());
                    else
                        Player.SendMessage(p, "> > last &branked&g at &a" + who.lastRankReason.Split(']')[0].Substring(1) + "&g; Reason: &a" + who.lastRankReason.Substring(who.lastRankReason.IndexOf("]") + 1).Trim());
                }
                bool skip = false;
                if (p != null) if (p.group.Permission <= LevelPermission.Operator) skip = true;
                if (!skip)
                {
                    string givenIP;
                    if (Server.bannedIP.Contains(who.ip)) givenIP = "&8" + who.ip + ", which is banned";
                    else givenIP = who.ip;
                    Player.SendMessage(p, "> > the IP of " + givenIP);
                }
                if (Server.useWhitelist)
                    if (Server.whiteList.Contains(who.name))
                        Player.SendMessage(p, "> > Player is &fWhitelisted");
                if (Server.devs.Contains(who.name.ToLower()))
                    Player.SendMessage(p, Server.DefaultColor + "> > Player is a &9Developer");
                if (Server.staff.Contains(who.name.ToLower()))
                    Player.SendMessage(p, Server.DefaultColor + "> > Player is a member of &4MCDawn Staff");
                if (Server.administration.Contains(who.name.ToLower()))
                    Player.SendMessage(p, Server.DefaultColor + "> > Player is a &6MCDawn Administrator");
            }
            else { Player.SendMessage(p, "\"" + message + "\" is offline! Using /whowas instead."); Command.all.Find("whowas").Use(p, message); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whois [player] - Displays information about someone.");
        }
    }
}