// Written by jonnyli1125 for MCDawn
using System;
using System.IO;

namespace MCDawn
{
    public class CmdHSave : Command
    {
        public override string name { get { return "hsave"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHSave() { }

        public override void Use(Player p, string message)
        {
            try
            {
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hsave"); return; }
                Command.all.Find("save").Use(p, prefix + p.name + message);
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hsave <save name> - Save your Home map.");
        }
    }
}