// Written by jonnyli1125 for MCDawn
using System;

namespace MCDawn
{
    public class CmdHSave : Command
    {
        public override string name { get { return "hsave"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "homes"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdHSave() { }

        public override void Use(Player p, string message)
        {
            try
            {
                if (p == null) { Player.SendMessage(p, "Command not usable as Console."); return; }
                string prefix = Server.HomePrefix;
                if (p.level.name != prefix + p.name.ToLower()) { p.SendMessage("You must be on your home map to use /hsave"); return; }
                if (String.IsNullOrEmpty(message.Trim()))
                {
                    p.level.Save(true);
                    Player.SendMessage(p, "Level \"" + p.level.name + "\" saved.");
                    int backupNumber = p.level.Backup(true);
                    if (backupNumber != -1)
                        p.level.ChatLevel("Backup " + backupNumber + " saved.");
                }
                else
                {
                    p.level.Save(true);
                    int backupNumber = p.level.Backup(true, message.Split(' ')[0]);
                    Player.GlobalMessage(p.level.name + " had a backup created named &b" + message.Split(' ')[0]);
                }
            }
            catch { Help(p); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/hsave <save name> - Save your Home map.");
        }
    }
}