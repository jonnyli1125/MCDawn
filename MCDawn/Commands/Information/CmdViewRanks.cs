using System;
using System.Collections.Generic;
using System.Text;

namespace MCDawn
{
    public class CmdViewRanks : Command
    {
        public override string name { get { return "viewranks"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdViewRanks() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            Group foundGroup = Group.Find(message);
            if (foundGroup == null)
            {
                Player.SendMessage(p, "Could not find group");
                return;
            }

            string totalList = String.Join(", ", foundGroup.playerList.All().ToArray());

            if (totalList == "")
            {
                Player.SendMessage(p, "No one has the rank of " + foundGroup.color + foundGroup.name);
                return;
            }
            
            Player.SendMessage(p, "People with the rank of " + foundGroup.color + foundGroup.name + ":");
            Player.SendMessage(p, totalList);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/viewranks [rank] - Shows all users who have [rank]");
            Player.SendMessage(p, "Available ranks: " + Group.concatList());
        }
    }
}
