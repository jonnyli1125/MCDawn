using System;

namespace MCDawn
{
    public class CmdWomText : Command
    {
        public override string name { get { return "womtext"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdWomText() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "status") { Player.SendMessage(p, "WoM text is currently set to: " + Server.womText.ToString()); return; }
            else
            {
                if (Server.womText)
                {
                    Server.womText = false;
                    Player.SendMessage(p, "WoM text turned off.");
                }
                else
                {
                    Server.womText = true;
                    Player.SendMessage(p, "WoM text turned on.");
                }
                Properties.Save("properties/server.properties");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/womtext [status] - Toggles WoM text on/off.");
            Player.SendMessage(p, "If enabled, server messages will be shown in top right hand corner for WoM users.");
            Player.SendMessage(p, "if <status> is given, it shows wheter WoM text is currently on/off.");
        }
    }
}