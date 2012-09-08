//Written by jonnyli1125 for MCDawn.
using System;

namespace MCDawn
{
    class CmdTimeRank : Command
    {
        public override string name { get { return Server.timeRankCommand.ToLower(); } }
        public override string[] aliases { get { return new string[] { "tr" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdTimeRank() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower().Trim() == "hours")
            {
                foreach (Group g in Group.GroupList)
                    if (g != Group.findPerm(LevelPermission.Nobody))
                        if (g.reqHours > 0)
                            Player.SendMessage(p, g.color + g.trueName + ": " + g.reqHours + " hours are needed for promotion.");
                        else
                            Player.SendMessage(p, g.color + g.trueName + ": Cannot time rank.");
                return;
            }
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            int days = Convert.ToInt32(p.timeSpent.Split(' ')[0]);
            int hours = Convert.ToInt32(p.timeSpent.Split(' ')[1]);
            if (days >= 1) { hours += days * 24; }

            if (p.group.reqHours <= 0) { p.SendMessage("Your current rank is not allowed to time rank."); return; }

            Group nextGroup = null; bool nextOne = false;
            for (int i = 0; i < Group.GroupList.Count; i++)
            {
                Group grp = Group.GroupList[i];
                if (nextOne)
                {
                    if (grp.Permission >= LevelPermission.Nobody) break;
                    nextGroup = grp;
                    break;
                }
                if (grp == p.group)
                    nextOne = true;
            }

            if (hours >= p.group.reqHours)
            {
                if (nextGroup != null) { Command.all.Find("promote").Use(null, p.name + " Timerank requirement reached."); }
                else { p.SendMessage("There are no higher ranks."); }
            }
            else 
            {
                p.SendMessage("The next rank requires &b" + p.group.reqHours + Server.DefaultColor + " hours.");
                p.SendMessage("You currently have &b" + hours + Server.DefaultColor + " hours.");
                p.SendMessage("Type &b/timerank hours" + Server.DefaultColor + " for the required hours for every rank.");
            }

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/timerank - Promotes you if you have passed the required time for the next rank.");
            Player.SendMessage(p, "/timerank hours - Shows the requirements for all the hours.");
        }
    }
}
