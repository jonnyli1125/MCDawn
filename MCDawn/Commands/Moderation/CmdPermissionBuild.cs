using System;

namespace MCDawn
{
    public class CmdPermissionBuild : Command
    {
        public override string name { get { return "perbuild"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdPermissionBuild() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }
            int number = message.Split(' ').Length;
            if (number > 2 || number < 1) { Help(p); return; }
            if (number == 1)
            {
                LevelPermission Perm = Level.PermissionFromName(message);
                if (Perm == LevelPermission.Null) { Player.SendMessage(p, "Not a valid rank"); return; }
                p.level.permissionbuild = Perm;
                Server.s.Log(p.level.name + " build permission changed to " + message + ".");
                Player.GlobalMessageLevel(p.level, "build permission changed to " + message + ".");
            }
            else
            {
                int pos = message.IndexOf(' ');
                string t = message.Substring(0, pos).ToLower();
                string s = message.Substring(pos + 1).ToLower();
                LevelPermission Perm = Level.PermissionFromName(s);
                if (Perm == LevelPermission.Null) { Player.SendMessage(p, "Not a valid rank"); return; }

                Level level = Level.Find(t);
                if (level != null)
                {
                    level.permissionbuild = Perm;
                    Server.s.Log(level.name + " build permission changed to " + s + ".");
                    Player.GlobalMessageLevel(level, "build permission changed to " + s + ".");
                    if (p != null)
                        if (p.level != level) { Player.SendMessage(p, "build permission changed to " + s + " on " + level.name + "."); }
                    return;
                }
                else
                    Player.SendMessage(p, "There is no level \"" + s + "\" loaded.");
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/PerBuild <map> <rank> - Sets build permission for a map.");
        }
    }
}