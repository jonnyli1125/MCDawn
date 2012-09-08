using System;
using System.IO;
using System.Data;

namespace MCDawn
{
    public class CmdSave : Command
    {
        public override string name { get { return "save"; } }
        public override string[] aliases { get { return new string[] { "mapsave" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdSave() { }

        public override void Use(Player p, string message)
        {
            if (message.ToLower() == "all")
            {
                foreach (Level l in Server.levels)
                {
                    try
                    {
                        l.Save();
                    }
                    catch { }
                }
                Player.GlobalMessage("All levels have been saved.");
            }
            else
            {
                if (message.Split(' ').Length == 1)         //Just save level given
                {
                    Level foundLevel = Level.Find(message);
                    if (foundLevel != null)
                    {
                        foundLevel.Save(true);
                        Player.SendMessage(p, "Level \"" + foundLevel.name + "\" saved.");
                        int backupNumber = p.level.Backup(true);
                        if (backupNumber != -1)
                            p.level.ChatLevel("Backup " + backupNumber + " saved.");
                    }
                    else
                    {
                        Player.SendMessage(p, "Could not find level specified");
                    }
                }
                else if (message.Split(' ').Length == 2)
                {
                    Level foundLevel = Level.Find(message.Split(' ')[0]);
                    string restoreName = message.Split(' ')[1].ToLower();
                    if (foundLevel != null)
                    {
                        foundLevel.Save(true);
                        int backupNumber = p.level.Backup(true, restoreName);
                        Player.GlobalMessage(foundLevel.name + " had a backup created named &b" + restoreName);
                    }
                    else
                    {
                        Player.SendMessage(p, "Could not find level specified");
                    }
                }
                else
                {
                    if (p == null)
                    {
                        Use(p, "all");
                    }
                    else
                    {
                        p.level.Save(true);
                        Player.SendMessage(p, "Level \"" + p.level.name + "\" saved.");

                        int backupNumber = p.level.Backup(true);
                        if (backupNumber != -1)
                            p.level.ChatLevel("Backup " + backupNumber + " saved.");
                    }
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/save - Saves the level you are currently in");
            Player.SendMessage(p, "/save all - Saves all loaded levels.");
            Player.SendMessage(p, "/save <map> - Saves the specified map.");
            Player.SendMessage(p, "/save <map> <name> - Backups the map with a given restore name");
        }
    }
}