using System;

namespace MCDawn
{
    public class CmdSummonMap : Command
    {
        public override string name { get { return "summonmap"; } }
        public override string[] aliases { get { return new string[] { "smap" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }

        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Player.SendMessage(null, "Console cannot use this command.");
                return;
            }
            if (message.Trim() == "")
            {
                Player.SendMessage(p, "You have to specify a map name.");
                return;
            }
            try
            {
                int count = 0;
                Level lvl = Level.Find(message.ToLower());
                foreach (Player plr in Player.players)
                {
                    if (plr.level.name == lvl.name && plr.name != p.name)
                    {
                        all.Find("summon").Use(p, plr.name);
                        count++;
                    }
                }
                if (count == 0)
                {
                    Player.SendMessage(p, "No players were summoned.");
                }
            }
            catch (Exception)
            {
                Player.SendMessage(p, "Could not find the map specified.");
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summonmap <map> - Summon all players on <map> to you. ");
            Player.SendMessage(p, "Use /smap as a shortcut.");
        }
    }
}