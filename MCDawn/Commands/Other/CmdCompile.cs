using System;

namespace MCDawn
{
    public class CmdCompile : Command
    {
        public override string name { get { return "compile"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdCompile() { }

        public override void Use(Player p, string message)
        {
            if(message == "" || message.IndexOf(' ') != -1) { Help(p); return; }
            bool success = false;
            try
            {
                 success = Scripting.Compile(message);
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
                Player.SendMessage(p, "An exception was thrown during compilation.");
                return;
            }
            if (success)
            {
                Player.SendMessage(p, "Compiled successfully.");
            }
            else
            {
                Player.SendMessage(p, "Compilation error.  Please check compile.log for more information.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/compile <class name> - Compiles a command class file into a DLL.");
        }
    }
}
