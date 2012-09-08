using System;
using System.IO;

namespace MCDawn
{
    public class CmdBotSummon : Command
    {
        public override string name { get { return "botsummon"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdBotSummon() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            PlayerBot who = PlayerBot.Find(message);
            if (who == null) { Player.SendMessage(p, "There is no bot " + message + "!"); return; }
            if (p.level != who.level) { Player.SendMessage(p, who.name + " is in a different level."); return; }
            who.SetPos(p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0);
            //who.SendMessage("You were summoned by " + p.color + p.name + "&e.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/botsummon <name> - Summons a bot to your position.");
        }
    }
}