using System;

namespace MCDawn
{
    public class CmdPrivate : Command
    {
        public override string name { get { return "private"; } }
        public override string[] aliases { get { return new string[] { "priv" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdPrivate() { }

        public override void Use(Player p, string message)
        {
            if (p.levelchat == true)
            {
                p.levelchat = false;
                Player.SendMessage(p, "Your messages will now be seen %bServer-wide.");
            }
            else
            {
                if (message == "")
                {
                    p.levelchat = true;
                    Player.SendMessage(p, "Your messages will now be seen only by players on your %bLevel.");
                }
                else
                {
                    Player.GlobalChatLevel(p, message, true);
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/private <message> - Talk only to the players on your level.");
        }
    }
}