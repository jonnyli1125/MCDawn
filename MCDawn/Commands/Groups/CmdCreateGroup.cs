using System;
using System.Linq;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdCreateGroup : Command
    {
        public override string name { get { return "creategroup"; } }
        public override string[] aliases { get { return new string[] { "cgroup" }; } }
        public override string type { get { return "groups"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdCreateGroup() { }

        public override void Use(Player p, string message)
        {
            string[] split = message.Split(' ');
            bool isPub = true;
            try { isPub = bool.Parse(split[1]); }
            catch { Help(p); return; }
            List<PlayerGroup> pgs = new List<PlayerGroup>(); int count = 0;
            foreach (PlayerGroup pg in p.playerGroup) { if (pg != null) { count++; pgs.Add(pg); } }
            p.playerGroup[count] = new PlayerGroup(split[0], isPub);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/creategroup <groupname> [public] - Creates a new Player Group.");
            Player.SendMessage(p, "If [public] is not given, the default is set to true.");
            Player.SendMessage(p, "Example: &b/creategroup MyGroup&g, or &b/creategroup PrivateGroup false&g.");
        }
    }
}