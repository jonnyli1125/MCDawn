using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdReplace : Command
    {
        public override string name { get { return "replace"; } }
        public override string[] aliases { get { return new string[] { "r" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdReplace() { }

        public override void Use(Player p, string message)
        {
            int number = message.Split(' ').Length;
            if (number != 2) { Help(p); return; }

            int pos = message.IndexOf(' ');
            string t = message.Substring(0, pos).ToLower();
            string t2 = message.Substring(pos + 1).ToLower();
            byte type = Block.Byte(t);
            if (type == 255) { Player.SendMessage(p, "There is no block \"" + t + "\"."); return; }
            byte type2 = Block.Byte(t2);
            if (type2 == 255) { Player.SendMessage(p, "There is no block \"" + t2 + "\"."); return; }

            if (!Block.canPlace(p, type) && !Block.BuildIn(type)) { Player.SendMessage(p, "Cannot replace that."); return; }

            if (!Block.canPlace(p, type2)) { Player.SendMessage(p, "Cannot place that."); return; }

            CatchPos cpos; cpos.type2 = type2; cpos.type = type;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/replace [type] [type2] - replace type with type2 inside a selected cuboid");
        }
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            CatchPos bp = (CatchPos)p.blockchangeObject;
            bp.x = x; bp.y = y; bp.z = z; p.blockchangeObject = bp;
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange2);
        }
        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            unchecked { if (cpos.type != (byte)-1) { type = cpos.type; } }
            List<Pos> buffer = new List<Pos>();

            for (ushort xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); ++xx)
            {
                for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); ++yy)
                {
                    for (ushort zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); ++zz)
                    {
                        if (p.level.GetTile(xx, yy, zz) == type) { BufferAdd(buffer, xx, yy, zz); }
                    }
                }
            }

            Level l = p.level;
            p.activeCuboids++;
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    int c;
                    if (Server.forceCuboid)
                    {
                        c = 0;
                        int counter = 1;
                        buffer.ForEach(delegate(Pos pos)
                        {
                            if (counter <= p.group.maxBlocks)
                            {
                                c++;
                                if (p.activeCuboids == 0) return;
                                counter++;
                                l.Blockchange(p, pos.x, pos.y, pos.z, cpos.type2);
                                if (c >= 2)
                                {
                                    c = 0;
                                    if (!Server.pauseCuboids) Thread.Sleep(10 - Server.throttle);
                                    else { while (Server.pauseCuboids) { Thread.Sleep(1000); } }
                                }
                            }
                        });
                        if (counter >= p.group.maxBlocks)
                        {
                            Player.SendMessage(p, "Tried to cuboid " + buffer.Count + " blocks, but your limit is " + p.group.maxBlocks + ".");
                            Player.SendMessage(p, "Executed cuboid up to limit.");
                        }
                        else
                        {
                            Player.SendMessage(p, buffer.Count.ToString() + " blocks.");
                        }

                        if (p.activeCuboids > 0) p.activeCuboids--;
                        if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                        return;
                    }

                    if (buffer.Count > p.group.maxBlocks)
                    {
                        Player.SendMessage(p, "You tried to replace " + buffer.Count + " blocks.");
                        Player.SendMessage(p, "You cannot replace more than " + p.group.maxBlocks + ".");
                        return;
                    }

                    Player.SendMessage(p, buffer.Count.ToString() + " blocks.");

                    c = 0;
                    buffer.ForEach(delegate(Pos pos)
                    {
                        c++;
                        if (p.activeCuboids == 0) return;
                        l.Blockchange(p, pos.x, pos.y, pos.z, cpos.type2);
                        if (c >= 2)
                        {
                            c = 0;
                            if (!Server.pauseCuboids) Thread.Sleep(10 - Server.throttle);
                            else { while (Server.pauseCuboids) { Thread.Sleep(1000); } }
                        }
                    });

                    if (p.activeCuboids > 0) p.activeCuboids--;
                    if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }
        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos pos; pos.x = x; pos.y = y; pos.z = z; list.Add(pos);
        }

        struct Pos
        {
            public ushort x, y, z;
        }
        struct CatchPos
        {
            public byte type;
            public byte type2;
            public ushort x, y, z;
        }

    }
}
