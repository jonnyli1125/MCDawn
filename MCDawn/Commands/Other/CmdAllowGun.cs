using System;

namespace MCDawn
{
    public class CmdAllowGun : Command
    {
        public override string name { get { return "allowgun"; } }
        public override string[] aliases { get { return new string[] { "allowguns" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdAllowGun() { }
        public override void Use(Player p, string message)
        {
            if (p == null)
            {
                Level where = Level.Find(message);
                if (String.IsNullOrEmpty(message)) { p.SendMessage("Level could not be found."); return; }
                if (where == null) { p.SendMessage("Level could not be found."); return; }
                if (where.allowguns == true)
                {
                    where.allowguns = false;
                    Player.GlobalMessage("Guns disabled on level &b" + where.name + Server.DefaultColor + ".");
                    where.Save();
                    foreach (Player pl in where.players) { if (pl.aiming) { Command.all.Find("gun").Use(pl, ""); } }
                }
                else
                {
                    where.allowguns = true;
                    Player.GlobalMessage("Guns enabled on level &b" + where.name + Server.DefaultColor + ".");
                    where.Save();
                }
                return;
            }
            if (message != "")
            {
                Level lvl = Level.Find(message);
                if (lvl == null) { Player.SendMessage(p, "Level could not be found."); return; }
                if (lvl == p.level) { Command.all.Find("allowgun").Use(p, ""); return; }
                if (lvl.allowguns == true)
                {
                    lvl.allowguns = false;
                    Player.GlobalMessage("Guns disabled on level &b" + lvl.name + Server.DefaultColor + ".");
                    foreach (Player pl in lvl.players) { if (pl.aiming) { Command.all.Find("gun").Use(pl, ""); } }
                    lvl.Save();
                }
                else
                {
                    lvl.allowguns = true;
                    Player.GlobalMessage("Guns enabled on level &b" + lvl.name + Server.DefaultColor + ".");
                    lvl.Save();
                }
            }
            else
            {
                if (p == null) { Player.SendMessage(p, "Level could not be found."); return; }
                if (p.level.allowguns == true)
                {
                    p.level.allowguns = false;
                    Player.GlobalMessageLevel(p.level, "Guns disabled on level &b" + p.level.name + Server.DefaultColor + ".");
                    foreach (Player pl in p.level.players) { if (pl.aiming) { Command.all.Find("gun").Use(pl, ""); } }
                    p.level.Save();
                }
                else
                {
                    p.level.allowguns = true;
                    Player.GlobalMessageLevel(p.level, "Guns enabled on level &b" + p.level.name + Server.DefaultColor + ".");
                    p.level.Save();
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/allowgun <level> - If no Level is given, Level is the one you're on.");
        }
    }
}
