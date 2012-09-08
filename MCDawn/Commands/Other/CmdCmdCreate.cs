using System;
using System.IO;
namespace MCDawn
{
    public class CmdCmdCreate : Command
    {
        public override string name { get { return "cmdcreate"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdCmdCreate() { }
        
        public override void Use(Player p, string message)
        {
            if (message == "" || message.IndexOf(' ') != -1)
            {
                Help(p);
                return;
            }
            else
            {
                if (File.Exists("extra/commands/source/Cmd" + message + ".cs")) { p.SendMessage("File Cmd" + message + ".cs already exists.  Choose another name."); return; }
                try
                {
                    Scripting.CreateNew(message);
                }
                catch (Exception e)
                {
                    Server.ErrorLog(e);
                    Player.SendMessage(p, "An error occurred creating the class file.");
                    return;
                }
                Player.SendMessage(p, "Successfully created a new command class.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdcreate <message> - Creates a dummy command class named Cmd<Message> from which you can make a new command.");
        }
    }
}
