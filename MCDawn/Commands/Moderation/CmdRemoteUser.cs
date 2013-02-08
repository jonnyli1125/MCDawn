using System;

namespace MCDawn
{
    public class CmdRemoteUser : Command
    {
        public override string name { get { return "remoteuser"; } }
        public override string[] aliases { get { return new string[] { "ruser", "ru" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdRemoteUser() { }

        public override void Use(Player p, string message)
        {
            switch (message.Split(' ')[0].ToLower())
            {
                case "add":
                    RemoteServer.AddUser(message.Split(' ')[1], message.Split(' ')[2]);
                    RemoteServer.LoadUsers();
                    Player.SendMessage(p, "Added user " + message.Split(' ')[1] + ":" + message.Split(' ')[2]);
                    break;
                case "del":
                    RemoteServer.RemoveUser(message.Split(' ')[1]);
                    RemoteServer.LoadUsers();
                    Player.SendMessage(p, "Removed user " + message.Split(' ')[1]);
                    break;
                default: Help(p); break;
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/remoteuser add <name> <password> - Add a Remote User to the server, for use with MCDawn Remote Console.");
            Player.SendMessage(p, "/remoteuser del <name> - Delete a Remote User from the server.");
        }
    }
}