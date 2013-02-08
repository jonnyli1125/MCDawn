// Written by jonnyli1125 for MCDawn
using System;
using System.IO;

namespace MCDawn
{
    public class CmdHFixGrass : Command
    {
        public override string name { get { return "hfixgrass"; } }
        public override string[] aliases { get { return new string[] { "hfg" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHFixGrass() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hfixgrass"); return; }
                Command.all.Find("fixgrass").Use(p, message);
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hfixgrass <type> - Fixes grass based on type");
            Player.SendMessage(p, "<type> as \"\": Any grass with something on top is made into dirt, dirt with nothing on top is made grass");
            Player.SendMessage(p, "<type> as \"light\": Only dirt/grass in sunlight becomes grass");
            Player.SendMessage(p, "<type> as \"grass\": Only turns grass to dirt when under stuff");
            Player.SendMessage(p, "<type> as \"dirt\": Only turns dirt with nothing on top to grass");
        }
    }
}