/*-----------------------------------------------------------------------------------------------------
This command was made by Jonny Li, also known as jonnyli1125, for use with MCDawn only.
June 7. 2012
Version 1.0
----------------------------------------------------------------------------------------------------
Licensed under a Creative Commons Attribution-NonCommercial-NoDerivs 3.0 Unported License.
http://creativecommons.org/licenses/by-nc-nd/3.0/

You must attribute the work in the manner specified by the author or licensor.
You may not use this work for commercial purposes.
You may not alter, transform, or build upon this work.

Any of the above conditions can be waived if you get written permission from the copyright holder.  
----------------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdThrough : Command
    {
        public override string name { get { return "through"; } }
        public override string[] aliases { get { return new string[] { "thru" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdThrough() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            try
            {
                int i = getDirection(p.rot[0]);
                ushort xx = (ushort)(p.pos[0] / 32), yy = (ushort)(p.pos[1] / 32), zz = (ushort)(p.pos[2] / 32);
                bool there = false;
                if (i == 0)
                {
                    while (!there && xx < p.level.width)
                    {
                        xx++;
                        if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                        {
                            if (Block.Walkthrough(p.level.GetTile((ushort)(xx + 1), yy, zz)))
                            {
                                if (!Block.Walkthrough(p.level.GetTile((ushort)(xx - 1), yy, zz)))
                                {
                                    Player.SendMessage(p, "Passed through wall.");
                                    unchecked { p.SendPos((byte)-1, (ushort)((xx + 1) * 32), p.pos[1], p.pos[2], p.rot[0], p.rot[1]); }
                                    there = true;
                                }
                            }
                        }
                    }
                    if (!there) { Player.SendMessage(p, "No free spaces available."); }
                }
                else if (i == 1)
                {
                    while (!there && zz < p.level.depth)
                    {
                        zz++;
                        if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                        {
                            if (Block.Walkthrough(p.level.GetTile(xx, yy, (ushort)(zz + 1))))
                            {
                                if (!Block.Walkthrough(p.level.GetTile(xx, yy, (ushort)(zz - 1))))
                                {
                                    Player.SendMessage(p, "Passed through wall.");
                                    unchecked { p.SendPos((byte)-1, p.pos[0], p.pos[1], (ushort)((zz + 1) * 32), p.rot[0], p.rot[1]); }
                                    there = true;
                                }
                            }
                        }
                    }
                    if (!there) { Player.SendMessage(p, "No free spaces available."); }
                }
                else if (i == 2)
                {
                    while (!there && xx > 0)
                    {
                        xx--;
                        if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                        {
                            if (Block.Walkthrough(p.level.GetTile((ushort)(xx - 1), yy, zz)))
                            {
                                if (!Block.Walkthrough(p.level.GetTile((ushort)(xx + 1), yy, zz)))
                                {
                                    Player.SendMessage(p, "Passed through wall.");
                                    unchecked { p.SendPos((byte)-1, (ushort)((xx - 1) * 32), p.pos[1], p.pos[2], p.rot[0], p.rot[1]); }
                                    there = true;
                                }
                            }
                        }
                    }
                    if (!there) { Player.SendMessage(p, "No free spaces available."); }
                }
                else if (i == 3)
                {
                    while (!there && zz > 0)
                    {
                        zz--;
                        if (Block.Walkthrough(p.level.GetTile(xx, yy, zz)))
                        {
                            if (Block.Walkthrough(p.level.GetTile(xx, yy, (ushort)(zz - 1))))
                            {
                                if (!Block.Walkthrough(p.level.GetTile(xx, yy, (ushort)(zz + 1))))
                                {
                                    Player.SendMessage(p, "Passed through wall.");
                                    unchecked { p.SendPos((byte)-1, p.pos[0], p.pos[1], (ushort)((zz - 1) * 32), p.rot[0], p.rot[1]); }
                                    there = true;
                                }
                            }
                        }
                    }
                    if (!there) { Player.SendMessage(p, "No free spaces available."); }
                }
                else { Player.SendMessage(p, "Could not pass through wall."); }
            }
            catch (Exception e) { Server.ErrorLog(e); Player.SendMessage(p, "Could not pass through wall."); }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/through - Takes you through the wall you are facing. You may use /thru as a shortcut.");
        }

        public int getDirection(byte rotx)
        {
            if (rotx >= 32 && rotx <= 95) // add to x coord to pass thru
                return 0;
            else if (rotx >= 96 && rotx <= 159) // add to the z coord to pass thru
                return 1;
            else if (rotx >= 160 && rotx <= 223) // subtract the x coord to pass thru
                return 2;
            else if ((rotx >= 224 && rotx <= 255) || (rotx >= 0 && rotx <= 31)) // subtract the z coord to pass thru
                return 3;
            else // idk, lolz.
                return 3;
        }

        //struct Pos { public ushort x, y, z; }
    }
}