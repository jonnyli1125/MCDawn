using System;

namespace MCDawn
{
    public class CmdSpawn : Command
    {
        public override string name { get { return "spawn"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }
        public CmdSpawn() { }

        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }
            ushort x = (ushort)((0.5 + p.level.spawnx) * 32);
            ushort y = (ushort)((1 + p.level.spawny) * 32);
            ushort z = (ushort)((0.5 + p.level.spawnz) * 32);
            unchecked
            {
                p.SendPos((byte)-1, x, y, z,
                            p.level.rotx,
                            p.level.roty);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spawn - Teleports yourself to the spawn location.");
        }
    }
}
