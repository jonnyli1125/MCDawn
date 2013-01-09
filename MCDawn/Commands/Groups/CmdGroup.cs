using System;

namespace MCDawn
{
    public class CmdGroup : Command
    {
        public override string name { get { return "group"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "groups"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdGroup() { }

        public override void Use(Player p, string message)
        {
            //Player who;
            switch (message.Split(' ')[0].ToLower())
            {
                case "list":
                    if (message.Split(' ').Length == 1)
                    {

                    }
                    else if (message.Split(' ').Length == 2)
                    {

                    }
                    else { Help(p); return; }
                    break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/group list - Lists all groups.");
            Player.SendMessage(p, "/group list <group> - Lists all players in <group>.");
            Player.SendMessage(p, "/group join <group> - Joins player group <group>.");
            Player.SendMessage(p, "/group leave <group> - Leaves player group <group>.");
            Player.SendMessage(p, "/group invite <group> <player> - Sends an invite to <player> to join <group>");
            Player.SendMessage(p, "/group op <group> <player> - Sets <player> to Operator in <group>. &c<Group Admin / Operator+>");
            Player.SendMessage(p, "/group remove <group> <player> - Remove <player> from <group>. &c<Group Operator+ / Operator+>");
            Player.SendMessage(p, "/group add <group> <player> - Adds <player> to <group>. &c<Group Operator+ / Operator+>");
        }
    }
}