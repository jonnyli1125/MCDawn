using System;
using System.IO;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdLevels : Command
    {
        public override string name { get { return "levels"; } }
        public override string[] aliases { get { return new string[] { "worlds", "maps" }; } }
        public override string type { get { return "information"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdLevels() { }

        public override void Use(Player p, string message)
        { // TODO
            try
            {
                if (message != "") { Help(p); return; }
                message = "";
                string message2 = "";
                bool Once = false;
                Server.levels.ForEach(delegate(Level level)
                {
                    if (p != null && level.permissionvisit <= p.group.Permission)
                    {
                        if (Group.findPerm(level.permissionbuild) != null)
                            message += ", " + Group.findPerm(level.permissionbuild).color + level.name + " &b[" + level.physics + "]";
                        else
                            message += ", " + level.name + " &b[" + level.physics + "]";
                    }
                    else if (p != null && level.permissionvisit > p.group.Permission)
                    {
                        if (!Once)
                        {
                            Once = true;
                            if (Group.findPerm(level.permissionvisit) != null)
                                message2 += Group.findPerm(level.permissionvisit).color + level.name + " &b[" + level.physics + "]";
                            else
                                message2 += level.name + " &b[" + level.physics + "]";
                        }
                        else
                        {
                            if (Group.findPerm(level.permissionvisit) != null)
                                message2 += ", " + Group.findPerm(level.permissionvisit).color + level.name + " &b[" + level.physics + "]";
                            else
                                message2 += ", " + level.name + " &b[" + level.physics + "]";
                        }
                    }
                    else if (p == null)
                    {
                        if (Group.findPerm(level.permissionbuild) != null)
                            message += ", " + Group.findPerm(level.permissionbuild).color + level.name + " &b[" + level.physics + "]";
                        else
                            message += ", " + level.name + " &b[" + level.physics + "]";
                    }
                });
                Player.SendMessage(p, "Loaded: " + message.Remove(0, 2));
                if (message2 != "")
                    Player.SendMessage(p, "Can't Goto: " + message2);
                Player.SendMessage(p, "Use &4/unloaded for unloaded levels.");
            }
            catch (Exception e)
            {
                Server.ErrorLog(e);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/levels - Lists all loaded levels and their physics levels.");
        }
    }
}