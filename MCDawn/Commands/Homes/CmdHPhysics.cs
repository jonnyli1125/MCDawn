// Written by jonnyli1125 for MCDawn
using System;
using System.IO;

namespace MCDawn
{
    public class CmdHPhysics : Command
    {
        public override string name { get { return "hphysics"; } }
        public override string[] aliases { get { return new string[] { "hp" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHPhysics() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hphysics"); return; }
                if (message == "") { Help(p); return; }
                Command.all.Find("physics").Use(p, message);
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/physics [map] <0/1/2/3/4/5> - Set the [map]'s physics, 0-Off 1-On 2-Advanced 3-Hardcore 4-Instant 5-Doors Only");
            Player.SendMessage(p, "If [map] is blank, uses Current level");
        }
    }
}