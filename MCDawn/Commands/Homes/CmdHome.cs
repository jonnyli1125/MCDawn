// Written by jonnyli1125 for MCDawn
using System;
using System.IO;
using System.Threading;

namespace MCDawn
{
    class CmdHome : Command
    {

        public override string name { get { return "home"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHome() { }

        public override void Use(Player p, string message)
        {
            Group rank = Group.findPerm(Server.HomeRank);
            if (p.group.Permission < Server.HomeRank) { p.SendMessage("You must be at least " + rank.name + " to use this command!"); return; }
            string prefix = Server.HomePrefix.ToLower();
            string x = Server.HomeX.ToString();
            string y = Server.HomeY.ToString();
            string z = Server.HomeZ.ToString();
            if (p.level.name == prefix + p.name.ToLower()) { p.SendMessage("You are already in your home!"); return; }
            bool doesntExist = false;
            if (!File.Exists("levels/" + prefix + p.name.ToLower() + ".lvl")) { doesntExist = true; }
            if (doesntExist)
            {
                p.SendMessage("Creating your home..."); 
                Command.all.Find("newlvl").Use(null, prefix + p.name.ToLower() + " " + x + " " + y + " " + z + " flat");
            }
            if (!Server.AutoLoad) { Command.all.Find("load").Use(p, prefix + p.name); }
            Command.all.Find("goto").Use(p, prefix + p.name);
            if (doesntExist) 
            { 
                while (p.Loading) { } // Wait for player to load 
                Command.all.Find("zoneall").Use(p, p.name);
                Command.all.Find("save").Use(p, prefix + p.name + " wipe");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/home - Brings you to your home (personal map).");
            Player.SendMessage(p, "If you do not have one, this command will create one for you.");
        }
    }
}
