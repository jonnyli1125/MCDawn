using System;

namespace MCDawn
{
    public class CmdPromote : Command
    {
        public override string name { get { return "promote"; } }
        public override string[] aliases { get { return new string[] { "pr" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdPromote() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            string foundName;
            Group foundGroup;
            if (who == null)
            {
                foundName = message.Split(' ')[0];
                foundGroup = Group.findPlayerGroup(message.Split(' ')[0]);
            }
            else
            {
                foundName = who.name;
                foundGroup = who.group;
            }

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
                if (grp == foundGroup)
                    nextOne = true;
            }

            if (nextGroup != null)
                if (message.Split(' ').Length != 1)
                    Command.all.Find("setrank").Use(p, foundName + " " + nextGroup.name + " " + message.Substring(message.IndexOf(" ")).Trim());
                else
                    Command.all.Find("setrank").Use(p, foundName + " " + nextGroup.name);
            else
                Player.SendMessage(p, "No higher ranks exist");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/promote <name> [reason] - Promotes <name> up a rank");
        }
    }
}