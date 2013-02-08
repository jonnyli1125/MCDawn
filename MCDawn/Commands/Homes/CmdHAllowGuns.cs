// Written by jonnyli1125 for MCDawn
using System;

namespace MCDawn
{
    public class CmdHAllowGuns : Command
    {
        public override string name { get { return "hallowguns"; } }
        public override string[] aliases { get { return new string[] { "hguns" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHAllowGuns() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hallowguns"); return; }
                Command.all.Find("allowguns").Use(p, prefix + p.name + message);
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hallowguns - Toggle usage of guns on your home map.");
        }
    }
}