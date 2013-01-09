using System;
using System.IO;

namespace MCDawn
{
    public class CmdLockMap : Command
    {
        public override string name { get { return "lockmap"; } }
        public override string[] aliases { get { return new string[] { "lock" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdLockMap() { }

        public override void Use(Player p, string message)
        {
            Level lvl = Level.Find(message);
            if (message == "")
            {
                if (p == null)
                {
                    Player.SendMessage(p, "Cannot find map.");
                }
                else
                {
                    lvl = p.level;
                    if (lvl.locked == true)
                    {
                        lvl.locked = false;
                        Player.GlobalMessage("&b" + lvl.name + "&g level unlocked");
                        lvl.Save();
                    }
                    else
                    {
                        lvl.locked = true;
                        Player.GlobalMessage("&b" + lvl.name + "&g level locked");
                        lvl.Save();
                    }
                }
            }
            else
            {
                if (lvl == null)
                {
                    Player.SendMessage(p, "Cannot find map.");
                }
                else
                {
                    if (lvl.locked == true)
                    {
                        lvl.locked = false;
                        Player.GlobalMessage("&b" + lvl.name + "&g level unlocked");
                        lvl.Save();
                    }
                    else
                    {
                        lvl.locked = true;
                        Player.GlobalMessage("&b" + lvl.name + "&g level locked");
                        lvl.Save();
                    }
                }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/lockmap [level] - Locks/unlocks a map.");
            Player.SendMessage(p, "If [level] isn't given, it locks/unlocks the map the user is in.");
        }
    }
}