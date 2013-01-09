using System;
using System.Data;

namespace MCDawn
{
    public class CmdWhowas : Command
    {
        public override string name { get { return "whowas"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdWhowas() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player pl = Player.Find(message); 
            if (pl != null && !pl.hidden)
            { 
                Player.SendMessage(p, pl.color + pl.name + "&g is online, using /whois instead."); 
                Command.all.Find("whois").Use(p, message);
                return; 
            }

            if (message.IndexOf("'") != -1) { Player.SendMessage(p, "Cannot parse request."); return; }

            string FoundRank = Group.findPlayer(message.ToLower());

            DataTable playerDb = MySQL.fillData("SELECT * FROM Players WHERE Name='" + message + "'");
            if (playerDb.Rows.Count == 0) { Player.SendMessage(p, Group.Find(FoundRank).color + message + "&g has the rank of " + Group.Find(FoundRank).color + FoundRank); return; }

            Player.SendMessage(p, Group.Find(FoundRank).color + playerDb.Rows[0]["Title"] + " " + message + "&g has :");
            Player.SendMessage(p, "> > the rank of \"" + Group.Find(FoundRank).color + FoundRank);
            Player.SendMessage(p, "> > the display name of " + playerDb.Rows[0]["displayName"]);
            Player.SendMessage(p, "> > &a" + playerDb.Rows[0]["Money"] + "&g " + Server.moneys);
            Player.SendMessage(p, "> > &cdied &a" + playerDb.Rows[0]["TotalDeaths"] + "&g times");
            Player.SendMessage(p, "> > &bmodified &a" + playerDb.Rows[0]["totalBlocks"] + "&g blocks.");
            //int ratio = (int)(Math.Round((decimal)(1 / (Math.Min(Convert.ToInt64(playerDb.Rows[0]["destroyedBlocks"].ToString()), (Convert.ToInt64(playerDb.Rows[0]["totalBlocks"].ToString()) - Convert.ToInt64(playerDb.Rows[0]["destroyedBlocks"].ToString()))) / Math.Max(Convert.ToInt64(playerDb.Rows[0]["destroyedBlocks"].ToString()), (Convert.ToInt64(playerDb.Rows[0]["totalBlocks"].ToString()) - Convert.ToInt64(playerDb.Rows[0]["destroyedBlocks"].ToString())))))));
            Int64 builtBlocks = 0;
            try { builtBlocks = Convert.ToInt64(playerDb.Rows[0]["totalBlocks"].ToString()) - Convert.ToInt64(playerDb.Rows[0]["destroyedBlocks"].ToString()); }
            catch { builtBlocks = 0; }
            if (builtBlocks < 0) { builtBlocks = 0; }
            try { Player.SendMessage(p, "> > destroyed &a" + playerDb.Rows[0]["destroyedBlocks"].ToString() + "&g blocks, and built &a" + builtBlocks + "&g."); }
            catch { }
            Player.SendMessage(p, "> > was last seen on &a" + playerDb.Rows[0]["LastLogin"]);
            try { if (Server.useMaxMind) { Player.SendMessage(p, "> > last logged in from country &a" + playerDb.Rows[0]["countryName"]); } }
            catch { }
            Player.SendMessage(p, "> > " + TimeSpent(playerDb.Rows[0]["TimeSpent"].ToString()));
            Player.SendMessage(p, "> > first logged into the server on &a" + playerDb.Rows[0]["FirstLogin"]);
            Player.SendMessage(p, "> > logged in &a" + playerDb.Rows[0]["totalLogin"] + "&g times, &c" + playerDb.Rows[0]["totalKicked"] + "&g of which ended in a kick.");
            Player.SendMessage(p, "> > " + Awards.awardAmount(message) + " awards");
            if (playerDb.Rows[0]["HasWOM"].ToString().Trim() == "")
                Player.SendMessage(p, "> > last logged in without &cWOM Game Client" + "&g.");
            else
                Player.SendMessage(p, "> > last logged in using &aWOM Game Client" + "&g, Version &a" + playerDb.Rows[0]["HasWOM"].ToString().Trim() + "&g.");
            // Last ranked/banned reason:
            string lastRankReason = playerDb.Rows[0]["lastRankReason"].ToString();
            if (lastRankReason.ToLower() != "none" && !String.IsNullOrEmpty(lastRankReason))
            {
                if (Group.findPerm(LevelPermission.Banned).playerList.Contains(message))
                    Player.SendMessage(p, "> > last &8banned&g at &a" + lastRankReason.Split(']')[0].Substring(1) + "&g; Reason: &a" + lastRankReason.Substring(lastRankReason.IndexOf("]") + 1).Trim());
                else
                    Player.SendMessage(p, "> > last &branked&g at &a" + lastRankReason.Split(']')[0].Substring(1) + "&g; Reason: &a" + lastRankReason.Substring(lastRankReason.IndexOf("]") + 1).Trim());
            }
            bool skip = false;
            if (p != null) if (p.group.Permission <= LevelPermission.Operator) skip = true;

            if (!skip)
            {
                if (Server.bannedIP.Contains(playerDb.Rows[0]["IP"].ToString()))
                    playerDb.Rows[0]["IP"] = "&8" + playerDb.Rows[0]["IP"] + ", which is banned";
                Player.SendMessage(p, "> > the IP of " + playerDb.Rows[0]["IP"]);
            }
            if (Server.useWhitelist)
                if (Server.whiteList.Contains(message.ToLower()))
                    Player.SendMessage(p, "> > Player is &fWhitelisted");
            if (Server.devs.Contains(message.ToLower()))
                Player.SendMessage(p, "&g> > Player is a &9Developer");
            if (Server.staff.Contains(message.ToLower()))
                Player.SendMessage(p, "&g> > Player is a member of &4MCDawn Staff");
            if (Server.administration.Contains(message.ToLower()))
                Player.SendMessage(p, "&g> > Player is a &6MCDawn Administrator");
            playerDb.Dispose();
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/whowas <name> - Displays information about someone who left.");
        }
        public string TimeSpent(string timeSpent) { return "time spent on server: &a" + timeSpent.Split(' ')[0] + " Days, " + timeSpent.Split(' ')[1] + " Hours, " + timeSpent.Split(' ')[2] + " Minutes."; }
    }
}