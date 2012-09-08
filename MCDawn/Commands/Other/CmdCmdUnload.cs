using System;
using System.IO;

namespace MCDawn
{
    class CmdCmdUnload : Command
    {
        public override string name { get { return "cmdunload"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Nobody; } }
        public CmdCmdUnload() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (Command.core.Contains(message.Split(' ')[0]))
            {
                Player.SendMessage(p, "/" + message.Split(' ')[0] + " is a core command, you cannot unload it!");
                return;
            }
            Command foundCmd = Command.all.Find(message.Split(' ')[0]);
            if(foundCmd == null)
            {
                Player.SendMessage(p, message.Split(' ')[0] + " is not a valid or loaded command.");
                return;
            }
            Command.all.Remove(foundCmd);
            GrpCommands.fillRanks();
            Player.SendMessage(p, "Command was successfully unloaded.");
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdunload <command> - Unloads a command from the server.");
        }
    }
}
