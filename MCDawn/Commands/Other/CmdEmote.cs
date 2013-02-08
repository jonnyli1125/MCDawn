using System;

namespace MCDawn
{
    public class CmdEmote : Command
    {
        public override string name { get { return "emote"; } }
        public override string[] aliases { get { return new string[] { "<3" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdEmote() { }

        public override void Use(Player p, string message)
        {
            p.parseSmiley = !p.parseSmiley;
            p.smileySaved = false;

            if (p.parseSmiley) Player.SendMessage(p, "Emote parsing is enabled.");
            else Player.SendMessage(p, "Emote parsing is disabled.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/emote - Enables or disables emoticon parsing");
        }
    }
}