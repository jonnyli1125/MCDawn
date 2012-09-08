// Written by jonnyli1125 for MCDawn - Trololol... This command is completely dominated by me :D
using System;
using System.IO;

namespace MCDawn
{
    public class CmdSetMain : Command
    {
        public override string name { get { return "setmain"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdSetMain() { }
        public override void Use(Player p, string jonnyli1125)
        {
            if (jonnyli1125 == "") { jonnyli1125 = p.level.name; }
            string jonnylvl = "levels/" + jonnyli1125 + ".lvl";
            string ohaidarjonny = "Level could not be found!";
            if (!File.Exists(jonnylvl)) { p.SendMessage(ohaidarjonny); return; }
            Server.level = jonnyli1125;
            Server.mainLevel = Level.Find(Server.level);
            string jonneh = "Main level successfully set as " + jonnyli1125 + ".";
            p.SendMessage(jonneh);
            if (p != null) { Server.s.Log(jonneh); }
            string jonny = "properties/server.properties";
            Properties.Save(jonny);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setmain <level> - Sets <level> as the Server's main level.");
        }
    }
}
