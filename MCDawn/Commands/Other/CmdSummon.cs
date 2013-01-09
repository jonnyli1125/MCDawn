using System;
using System.IO;
using System.Threading;

namespace MCDawn
{
    public class CmdSummon : Command
    {
        public override string name { get { return "summon"; } }
        public override string[] aliases { get { return new string[] { "s" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdSummon() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            if (message.ToLower() == "all")
            {
                int count = 0;
                foreach (Player pl in Player.players)
                {
                    if (pl.level == p.level && pl != p && p.group.Permission > pl.group.Permission)
                    {
                        count++;
                        unchecked { pl.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0); }
                        pl.SendMessage("You were summoned by " + p.color + p.name + "&g.");
                        return;
                    }
                    if (pl.level != p.level && pl != p && p.group.Permission > pl.group.Permission)
                    {
                        Level where = p.level;
                        count++;
                        Command.all.Find("goto").Use(pl, where.name);
                        while (pl.Loading) { }
                        unchecked { pl.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0); }
                        Command.all.Find("summon").Use(p, pl.name);
                        return;
                    }
                }
                if (count == 0) { Player.SendMessage(p, "No players were summoned."); }
                return;
            }

            Player who = Player.Find(message);
            if (who == null || who.hidden) { Player.SendMessage(p, "There is no player \"" + message + "\"!"); return; }
            if (p.group.Permission < who.group.Permission)
            {
                Player.SendMessage(p, "You cannot summon a player with a higher rank than you!");
                return;
            }
            if (p.group.Permission < LevelPermission.Operator && who.level.name != Server.HomePrefix + p.name.ToLower() && p.level.name != Server.HomePrefix + p.name.ToLower()) { if (p.level.locked || who.level.locked) { p.SendMessage("This map is currently locked!"); return; } }
            if (p.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "You can't leave an Infection game!"); return; }
            if (who.level.zombiegame == true && p.level != who.level) { Player.SendMessage(p, "Infection is active on that map, you can't go to it!"); return; }
            if (p.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "You can't leave a Spleef game!"); return; }
            if (who.level.spleefstarted == true && p.level != who.level) { Player.SendMessage(p, "Spleef is active on that map, you can't go to it!"); return; }
            if (p.level.permissionvisit > who.group.Permission) { p.SendMessage(who.color + who.name + "&g is not allowed to come to this map"); return; }
            if (p.level != who.level)
            {
                Level where = p.level;
                Command.all.Find("goto").Use(who, where.name);
                while (who.Loading) { }
                Command.all.Find("summon").Use(p, who.name);
            }
            else
            {
                unchecked { who.SendPos((byte)-1, p.pos[0], p.pos[1], p.pos[2], p.rot[0], 0); }
                who.SendMessage("You were summoned by " + p.color + p.name + "&g.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/summon <player> - Summons a player to your position.");
            Player.SendMessage(p, "/summon all - Summons all players in the map");
        }
    }
}