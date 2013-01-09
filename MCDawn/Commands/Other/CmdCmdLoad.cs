using System;
using System.IO;
using System.Reflection;

namespace MCDawn
{
    class CmdCmdLoad : Command
    {
        public override string name { get { return "cmdload"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdCmdLoad() { }

        public override void Use(Player p, string message)
        {
            if(message == "") { Help(p); return; }
            if (Command.all.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p, "That command is already loaded!");
                return;
            }
            message = "Cmd" + message.Split(' ')[0]; ;
            string error = Scripting.Load(message);
            if (error != null)
            {
                Player.SendMessage(p, error);
                return;
            }
            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully loaded.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdload <command name> - Loads a command into the server for use.");
        }
    }
}
