using System;
using System.IO;

namespace MCDawn
{
    public class CmdZoneAll : Command
    {
        public override string name { get { return "zoneall"; } }
        public override string[] aliases { get { return new string[] { "ozone" }; } }
        public override string type { get { return "mod"; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public override bool museumUsable { get { return false; } }
        public CmdZoneAll() { }
        public override void Use(Player p, string message)
        {
            if (!Server.useMySQL) { p.SendMessage("MySQL has not been configured! Please configure MySQL to use Zones!"); return; }
            if (message == "") { Help(p); return; }
            string[] split = message.Split(' ');
            string target = split[0];
            string zoneOwner;
            if (split.Length == 2)
            {
                Level level = Level.Find(target);
                if (!File.Exists("levels/" + target.ToLower() + ".lvl")) { Player.SendMessage(p, "No such level \"" + target + "\"!"); return; }
                if (Group.Find(split[1]) != null) { zoneOwner = "grp" + Group.Find(split[1]).name; }
                else if (Player.Find(split[1]) != null) { zoneOwner = Player.Find(split[1]).name; }
                else { zoneOwner = split[1]; }
                MySQL.executeQuery("INSERT INTO `Zone" + target + "` (SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES (" + 0 + ", " + 0 + ", " + 0 + ", " + (p.level.width - 1) + ", " + (p.level.height - 1) + ", " + (p.level.depth - 1) + ", '" + zoneOwner + "')");
                Player.SendMessage(p, "Zoned entire level " + target + " for &b" + zoneOwner);
                if (level != null)
                {
                    Level.Zone Zn;
                    Zn.smallX = 0;
                    Zn.smallY = 0;
                    Zn.smallZ = 0;
                    Zn.bigX = (ushort)(p.level.width - 1);
                    Zn.bigY = (ushort)(p.level.height - 1);
                    Zn.bigZ = (ushort)(p.level.depth - 1);
                    Zn.Owner = zoneOwner;
                    level.ZoneList.Add(Zn);
                }
            }
            else if (split.Length == 1)
            {
                if (Group.Find(split[0]) != null) { zoneOwner = "grp" + Group.Find(split[0]).name; }
                else if (Player.Find(split[0]) != null) { zoneOwner = Player.Find(split[0]).name;  }
                else { zoneOwner = split[0]; }
                MySQL.executeQuery("INSERT INTO `Zone" + p.level.name + "` (SmallX, SmallY, SmallZ, BigX, BigY, BigZ, Owner) VALUES (" + 0 + ", " + 0 + ", " + 0 + ", " + (p.level.width - 1) + ", " + (p.level.height - 1) + ", " + (p.level.depth - 1) + ", '" + zoneOwner + "')");
                Player.SendMessage(p, "Zoned entire level " + p.level.name + " for &b" + zoneOwner);
                Level.Zone Zn;
                Zn.smallX = 0;
                Zn.smallY = 0;
                Zn.smallZ = 0;
                Zn.bigX = (ushort)(p.level.width - 1);
                Zn.bigY = (ushort)(p.level.height - 1);
                Zn.bigZ = (ushort)(p.level.depth - 1);
                Zn.Owner = zoneOwner;
                p.level.ZoneList.Add(Zn);
            }
            else { Help(p); return; }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/zoneall [level] <rank/player> - Zones the entire map to <rank/player> on [level]");
            Player.SendMessage(p, "If [level] is not given, uses your current level.");
            Player.SendMessage(p, "To delete a zone use \"/zone del\" anywhere on the level.");
        }
    }
}