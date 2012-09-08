// Written by jonnyli1125 for MCDawn; just a modification of /setrank.

using System;

namespace MCDawn
{
    public class CmdFakeRank : Command
    {
        public override string name { get { return "fakerank"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdFakeRank() { }

        public override void Use(Player p, string message)
        {
            if (message.Split(' ').Length < 2) { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            Group newRank = Group.Find(message.Split(' ')[1]);
            string msgGave;

            if (p != null)
                if (message.Split(' ').Length > 2) msgGave = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1)) + " (" + p.name + ")"; else msgGave = "No reason given. (" + p.name + ").";
            else
                if (message.Split(' ').Length > 2) msgGave = message.Substring(message.IndexOf(' ', message.IndexOf(' ') + 1)) + " (Console)"; else msgGave = "No reason given. (Console).";
            if (newRank == null) { Player.SendMessage(p, "Could not find specified rank."); return; }

            Group bannedGroup = Group.findPerm(LevelPermission.Banned);
            if (who == null)
            {
                string foundName = message.Split(' ')[0];
                if (newRank == bannedGroup)
                {
                    if (p != null)
                    {
                        Player.GlobalMessage(foundName + " &f(offline)" + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".");
                    }
                    else
                    {
                        Player.GlobalMessage(foundName + " &f(offline)" + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by Console.");
                    }
                    return;
                }

                Group oldGroup = Group.findPlayerGroup(foundName);

                Player.GlobalMessage(foundName + " &f(offline)" + Server.DefaultColor + "'s rank was set to " + newRank.color + newRank.name);
                Player.GlobalChat(null, "&6Reason: &f" + msgGave, false);
            }
            else
            {
                if (newRank == bannedGroup) {
                    if (p != null)
                        Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by " + p.color + p.name + Server.DefaultColor + ".", false);
                    else
                        Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + " is now &8banned" + Server.DefaultColor + " by Console.", false);
                    Player.GlobalChat(null, "&6Reason: &f" + msgGave, false); 
                    return;
                }
                Player.GlobalChat(who, who.color + who.name + Server.DefaultColor + "'s rank was set to " + newRank.color + newRank.name, false);
                Player.GlobalChat(null, "&6Reason: &f" + msgGave, false);
                who.color = who.group.color;
                Player.GlobalDie(who, false);
                who.SendMessage("You are now ranked " + newRank.color + newRank.name + Server.DefaultColor + ", type /help for your new set of commands.");
                Player.GlobalSpawn(who, who.pos[0], who.pos[1], who.pos[2], who.rot[0], who.rot[1], false);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/fakerank <player> <rank> <yay> - Sets or returns a players rank.");
            Player.SendMessage(p, "Valid Ranks are: " + Group.concatList(true, true));
            Player.SendMessage(p, "<yay> is a celebratory message");
        }
    }
}
