using System;
using System.IO;

namespace MCDawn
{
    class CmdRestore : Command
    {
        public override string name { get { return "restore"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdRestore() { }

        public override void Use(Player p, string message)
        {
            //Thread CrossThread;

            if (message != "")
            {
                Server.s.Log(@Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl");
                if (File.Exists(@Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl"))
                {
                    try
                    {
                        File.Copy(@Server.backupLocation + "/" + p.level.name + "/" + message + "/" + p.level.name + ".lvl", "levels/" + p.level.name + ".lvl", true);
                        Level temp = Level.Load(p.level.name);
                        temp.physThread.Start();
                        if (temp != null)
                        {
                            p.level.spawnx = temp.spawnx;
                            p.level.spawny = temp.spawny;
                            p.level.spawnz = temp.spawnz;

                            p.level.height = temp.height;
                            p.level.width = temp.width;
                            p.level.depth = temp.depth;

                            p.level.blocks = temp.blocks;
                            p.level.setPhysics(0);
                            p.level.ClearPhysics();

                            Command.all.Find("reveal").Use(p, "all");
                        }
                        else
                        {
                            Server.s.Log("Restore nulled");
                            File.Copy("levels/" + p.level.name + ".lvl.backup", "levels/" + p.level.name + ".lvl", true);
                        }

                    }
                    catch { Server.s.Log("Restore fail"); }
                }
                else { Player.SendMessage(p, "Backup " + message + " does not exist."); }
            }
            else
            {
                if (Directory.Exists(@Server.backupLocation + "/" + p.level.name))
                {
                    string[] directories = Directory.GetDirectories(@Server.backupLocation + "/" + p.level.name);
                    int backupNumber = directories.Length;
                    Player.SendMessage(p, p.level.name + " has " + backupNumber + " backups .");

                    bool foundOne = false; string foundRestores = "";
                    foreach (string s in directories)
                    {
                        string directoryName = s.Substring(s.LastIndexOf('\\') + 1);
                        try
                        {
                            int.Parse(directoryName);
                        }
                        catch
                        {
                            foundOne = true;
                            foundRestores += ", " + directoryName;
                        }
                    }

                    if (foundOne)
                    {
                        Player.SendMessage(p, "Custom-named restores:");
                        Player.SendMessage(p, "> " + foundRestores.Remove(0, 2));
                    }
                }
                else
                {
                    Player.SendMessage(p, p.level.name + " has no backups yet.");
                }
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/restore <number> - restores a previous backup of the current map");
        }
    }
}
