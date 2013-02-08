using System;

namespace MCDawn
{
    public class CmdCompLoad : Command
    {
        public override string name { get { return "compload"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdCompLoad() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            try
            {
                Command.all.Find("compile").Use(p, message);
                Command.all.Find("cmdload").Use(p, message);
            }
            catch (Exception) { p.SendMessage("Error comploading command."); }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/compload <class name> - Compiles and loads a command.");
        }
    }
}
