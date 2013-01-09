// This work is licensed to jonnyli1125 (Jonny Li) under a Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// You may obtain a copy of the License at: http://creativecommons.org/licenses/by-nc-sa/3.0/

using System;

namespace MCDawn
{
    public class CmdUnder : Command
    {
        public override string name { get { return "under"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdUnder() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (message.ToLower() != "now")
            {
                Player.SendMessage(p, "Click a block to go under.");
                p.ClearBlockchange();
                p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
            }
            else
            {
                ushort xx = (ushort)(p.pos[0] / 32), yy = (ushort)(p.pos[1] / 32), zz = (ushort)(p.pos[2] / 32);
                bool there = false;
                while (!there && yy > 0)
                {
                    yy--;
                    if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                        if (Block.Walkthrough(p.level.GetTile(xx, (ushort)(yy - 1), zz)))
                            if (!Block.Walkthrough(p.level.GetTile(xx, (ushort)(yy + 1), zz)))
                            {
                                Player.SendMessage(p, "Teleported under.");
                                unchecked { p.SendPos((byte)-1, p.pos[0], (ushort)(yy * 32), p.pos[2], p.rot[0], p.rot[1]); }
                                there = true;
                            }
                }
                if (!there) { Player.SendMessage(p, "No free spaces available."); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/under [now] - Puts you under the clicked block.");
            Player.SendMessage(p, "If [now] is given, it will put you under of the block your ontop, right now.");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            bool there = false;
            while (!there && y > 0)
            {
                y--;
                if (Block.Walkthrough(p.level.GetTile(x, y, z)))
                    if (Block.Walkthrough(p.level.GetTile(x, (ushort)(y - 1), z)))
                        if (!Block.Walkthrough(p.level.GetTile(x, (ushort)(y + 1), z)))
                        {
                            Player.SendMessage(p, "Teleported under.");
                            unchecked { p.SendPos((byte)-1, (ushort)(x * 32), (ushort)(y * 32), (ushort)(z * 32), p.rot[0], p.rot[1]); }
                            there = true;
                        }
            }
            if (!there) { Player.SendMessage(p, "No free spaces available."); }
            if (p.staticCommands) { p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1); }
        }
    }
}