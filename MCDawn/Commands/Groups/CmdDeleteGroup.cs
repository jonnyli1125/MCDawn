using System;

namespace MCDawn
{
    public class CmdDeleteGroup : Command
    {
        public override string name { get { return "deletegroup"; } }
        public override string[] aliases { get { return new string[] { "dgroup" }; } }
        public override string type { get { return "groups"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdDeleteGroup() { }

        public override void Use(Player p, string message)
        {

        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/group");
        }
    }
}