/*-----------------------------------------------------------------------------------------------------
This command was made by Jonny Li, also known as jonnyli1125, for use with MCDawn only.
June 7. 2012
Version 1.0
----------------------------------------------------------------------------------------------------
Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
http://creativecommons.org/licenses/by-nc-sa/3.0/

You must attribute the work in the manner specified by the author or licensor.
You may not use this work for commercial purposes.
You may not alter, transform, or build upon this work.

Any of the above conditions can be waived if you get written permission from the copyright holder.  
----------------------------------------------------------------------------------------------------*/

using System;

namespace MCDawn
{
    public class CmdTop : Command
    {
        public override string name { get { return "top"; } }
        public override string[] aliases { get { return new string[] { "ascend" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdTop() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (message.ToLower() != "now")
            {
                Player.SendMessage(p, "Click a block to go on top of.");
                p.ClearBlockchange();
                p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
            }
            else
            {
                ushort xx = (ushort)(p.pos[0] / 32), yy = (ushort)(p.pos[1] / 32), zz = (ushort)(p.pos[2] / 32);
                bool there = false;
                while (!there && yy < p.level.height)
                {
                    yy++;
                    if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                    {
                        if (Block.Walkthrough(p.level.GetTile(xx, (ushort)(yy + 1), zz)))
                        {
                            if (!Block.Walkthrough(p.level.GetTile(xx, (ushort)(yy - 1), zz)))
                            {
                                Player.SendMessage(p, "Teleported up.");
                                unchecked { p.SendPos((byte)-1, p.pos[0], (ushort)((yy + 1) * 32), p.pos[2], p.rot[0], p.rot[1]); }
                                there = true;
                            }
                        }
                    }
                }
                if (!there) { Player.SendMessage(p, "No free spaces available."); }
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/top [now] - Puts you ontop of the clicked block.");
            Player.SendMessage(p, "If [now] is given, it will put you ontop of the block your under, right now.");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            bool there = false;
            while (!there )
            {
                y++;
                if (Block.Walkthrough(p.level.GetTile(x, y, z)))
                    if (Block.Walkthrough(p.level.GetTile(x, (ushort)(y + 1), z)))
                        if (!Block.Walkthrough(p.level.GetTile(x, (ushort)(y - 1), z)))
                        {
                            Player.SendMessage(p, "Teleported up.");
                            unchecked { p.SendPos((byte)-1, (ushort)(x * 32), (ushort)((y + 1) * 32), (ushort)(z * 32), p.rot[0], p.rot[1]); }
                            there = true;
                        }
            }
            if (!there) { Player.SendMessage(p, "No free spaces available."); }
            if (p.staticCommands) { p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1); }
        }
    }
}