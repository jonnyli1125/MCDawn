using System;

namespace MCDawn
{
    public class CmdRankChat : Command
    {
        public override string name { get { return "rankchat"; } }
        public override string[] aliases { get { return new string[] { "rchat" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdRankChat() { }

        public override void Use(Player p, string message)
        {
            if (message == "")
            {
                if (p == null) { p.SendMessage("Rank could not be found."); return; }
                p.rankchat = !p.rankchat;
                if (p.rankchat) Player.SendMessage(p, "All messages will now be sent to players of your rank and higher only");
                else Player.SendMessage(p, "Rank chat turned off");
            }
            else
            {
                try
                {
                    string[] text = message.Split(new char[] { (' ') }, 2);
                    Group otherRank = Group.Find(text[0]);
                    if (String.IsNullOrEmpty(text[1])) { p.SendMessage("No message sent."); return; }
                    if (otherRank == null) { p.SendMessage("Rank could not be found."); return; }
                    string rankName;
                    string getname = otherRank.name;
                    if (!getname.EndsWith("s") && !getname.EndsWith("ed")) { getname = getname + "s"; }
                    rankName = getname.Substring(0, 1);
                    rankName = rankName.ToUpper() + getname.Remove(0, 1);
                    if (p == null)
                    {
                        Player.GlobalMessageRank(otherRank, otherRank.color + "To " + rankName + " &f-" + "&gConsole [&a" + Server.ZallState + "&g]&f- " + text[1]);
                        Server.s.Log("(" + rankName + "): CONSOLE: " + text[1]);
                        return;
                    }
                    if (p != null && p.group.Permission < otherRank.Permission) { p.SendMessage("You are not allowed to talk in higher ranked rankchat."); return; }
                    Player.GlobalMessageRank(otherRank, otherRank.color + "To " + rankName + " &f-" + p.color + p.name + "&f- " + text[1]);
                    Server.s.Log("(" + rankName + "): " + name + ": " + text[1]);
                }
                catch { Help(p); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rankchat <rank> <message> - Makes all messages sent go to your rank and higher ranks by default");
        }
    }
}