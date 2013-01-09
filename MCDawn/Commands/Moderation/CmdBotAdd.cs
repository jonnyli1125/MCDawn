using System;
using System.IO;

namespace MCDawn
{
    public class CmdBotAdd : Command
    {
        public override string name { get { return "botadd"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdBotAdd() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (!PlayerBot.ValidName(message)) { Player.SendMessage(p, "bot name " + message + " not valid!"); return; }
            PlayerBot.playerbots.Add(new PlayerBot(message, p.level, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0));
            //who.SendMessage("You were summoned by " + p.color + p.name + "&e.");
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/botadd <name> - Add a new bot at your position.");
        }
    }
}