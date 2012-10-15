using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdBrush : Command
    {
        public override string name { get { return "brush"; } }
        public override string[] aliases { get { return new string[] { "br" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdBrush() { }

        public override void Use(Player p, string message)
        {
            if (!p.brushing && message.Trim() == "") { Help(p); return; }
            if (message.Trim().ToLower() == "off" || (message.Trim() == "" && p.brushing))
            {
                p.brushing = false;
                p.ClearBlockchange();
                Player.SendMessage(p, "Brushing mode turned off.");
                return;
            }
            if (message.Split(' ').Length > 3 || message.Split(' ').Length < 3) { Help(p); return; }
            ushort radius;
            try { radius = ushort.Parse(message.Split(' ')[0]); }
            catch { Help(p); return; }
            BrushType bt;
            switch (message.Split(' ')[2].ToLower())
            {
                case "sphere": bt = BrushType.sphere; break;
                case "cube": bt = BrushType.cube; break;
                case "random": bt = BrushType.random; break;
                default: Help(p); return;
            }
            byte t = Block.Byte(message.Split(' ')[1].ToLower());
            if (t == 255) { Player.SendMessage(p, "There is no block \"" + message + "\"."); return; }
            if (!Block.canPlace(p, t)) { Player.SendMessage(p, "Cannot place that."); return; }
            CatchPos cpos; cpos.x = 0; cpos.y = 0; cpos.z = 0; cpos.type = t; cpos.brush = bt; cpos.radius = radius;
            p.blockchangeObject = cpos;
            Player.SendMessage(p, "Brushing mode activated. Place/destroy blocks to use the brush.");
            p.brushing = true;
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/brush <radius> <block> <sphere/cube/random> - Activate brush mode.");
            Player.SendMessage(p, "/brush off - Turns brushing mode off.");
        }
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            cpos.x = (ushort)(x + cpos.radius); cpos.y = (ushort)(y + cpos.radius); cpos.z = (ushort)(z + cpos.radius);
            x = (ushort)(x - cpos.radius); y = (ushort)(y - cpos.radius); z = (ushort)(z - cpos.radius);
            type = cpos.type;
            unchecked { if (cpos.type != (byte)-1) type = cpos.type; else type = p.bindings[type]; }
            List<Pos> buffer = new List<Pos>();

            ushort xx; ushort yy; ushort zz;

            // find axis lengths
            double rx = (Math.Max(cpos.x, x) - Math.Min(cpos.x, x) + 1) / 2d;
            double ry = (Math.Max(cpos.y, y) - Math.Min(cpos.y, y) + 1) / 2d;
            double rz = (Math.Max(cpos.z, z) - Math.Min(cpos.z, z) + 1) / 2d;

            double rx2 = 1 / (rx * rx);
            double ry2 = 1 / (ry * ry);
            double rz2 = 1 / (rz * rz);

            // find center points
            double cx = (Math.Max(cpos.x, x) + Math.Min(cpos.x, x)) / 2d;
            double cy = (Math.Max(cpos.y, y) + Math.Min(cpos.y, y) + 1) / 2d;
            double cz = (Math.Max(cpos.z, z) + Math.Min(cpos.z, z)) / 2d;

            switch (cpos.brush)
            {
                case BrushType.sphere:
                    for (xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); xx += 8)
                        for (yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy += 8)
                            for (zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); zz += 8)
                                for (int z3 = 0; z3 < 8 && zz + z3 <= Math.Max(cpos.z, z); z3++)
                                    for (int y3 = 0; y3 < 8 && yy + y3 <= Math.Max(cpos.y, y); y3++)
                                        for (int x3 = 0; x3 < 8 && xx + x3 <= Math.Max(cpos.x, x); x3++)
                                        {
                                            // get relative coordinates
                                            double dx = (xx + x3 - cx);
                                            double dy = (yy + y3 - cy);
                                            double dz = (zz + z3 - cz);

                                            // test if it's inside ellipse
                                            if ((dx * dx) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 <= 1)
                                                if (p.level.GetTile((ushort)(xx + x3), (ushort)(yy + y3), (ushort)(zz + z3)) != type)
                                                    BufferAdd(buffer, (ushort)(xx + x3), (ushort)(yy + y3), (ushort)(zz + z3));
                                        }
                    break;
                case BrushType.cube:
                    buffer.Capacity = Math.Abs(cpos.x - x) * Math.Abs(cpos.y - y) * Math.Abs(cpos.z - z);
                    for (xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); ++xx)
                        for (yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); ++yy)
                            for (zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); ++zz)
                            {
                                if (p.level.GetTile(xx, yy, zz) != type) { BufferAdd(buffer, xx, yy, zz); }
                            }
                    break;
                case BrushType.random:
                    Random rand = new Random();
                    for (xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); xx += 8)
                        for (yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy += 8)
                            for (zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); zz += 8)
                                for (int z3 = 0; z3 < 8 && zz + z3 <= Math.Max(cpos.z, z); z3++)
                                    for (int y3 = 0; y3 < 8 && yy + y3 <= Math.Max(cpos.y, y); y3++)
                                        for (int x3 = 0; x3 < 8 && xx + x3 <= Math.Max(cpos.x, x); x3++)
                                        {
                                            // get relative coordinates
                                            double dx = (xx + x3 - cx);
                                            double dy = (yy + y3 - cy);
                                            double dz = (zz + z3 - cz);

                                            // test if it's inside ellipse
                                            if ((dx * dx) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 <= 1)
                                                if (p.level.GetTile((ushort)(xx + x3), (ushort)(yy + y3), (ushort)(zz + z3)) != type)
                                                    if (rand.Next(1, 11) <= 5)
                                                        BufferAdd(buffer, (ushort)(xx + x3), (ushort)(yy + y3), (ushort)(zz + z3));
                                        }
                    break;
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
                        int counter = 1;
                        c = 0;
                        for (int i = 0; i < buffer.Count; i++)
                        {
                            if (counter <= p.group.maxBlocks)
                            {
                                c++;
                                if (p.activeCuboids == 0) return;
                                counter++;
                                l.Blockchange(p, buffer[i].x, buffer[i].y, buffer[i].z, type);
                                if (Server.throttle > 0)
                                {
                                    while (Server.pauseCuboids) { Thread.Sleep(1000); }
                                    if (c >= Server.throttle)
                                    {
                                        c = 0;
                                        if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                                    }
                                }
                            }
                        }
                        if (counter >= p.group.maxBlocks)
                        {
                            Player.SendMessage(p, "Tried to brush " + buffer.Count + " blocks, but your limit is " + p.group.maxBlocks + ".");
                            Player.SendMessage(p, "Executed brush up to limit.");
                        }
                        else
                        {
                            Player.SendMessage(p, buffer.Count.ToString() + " blocks.");
                        }

                        if (p.activeCuboids > 0) p.activeCuboids--;
                        p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                        return;
                    }

                    if (buffer.Count > p.group.maxBlocks)
                    {
                        Player.SendMessage(p, "You tried to brush " + buffer.Count + " blocks.");
                        Player.SendMessage(p, "You cannot brush more than " + p.group.maxBlocks + ".");
                        return;
                    }

                    Player.SendMessage(p, buffer.Count.ToString() + " blocks.");

                    c = 0;
                    for (int i = 0; i < buffer.Count; i++)
                    {
                        c++;
                        if (p.activeCuboids == 0) return;
                        l.Blockchange(p, buffer[i].x, buffer[i].y, buffer[i].z, type);
                        if (Server.throttle > 0)
                        {
                            while (Server.pauseCuboids) { Thread.Sleep(1000); }
                            if (c >= Server.throttle)
                            {
                                c = 0;
                                if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                            }
                        }
                    }

                    if (p.activeCuboids > 0) p.activeCuboids--;
                    p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }
        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos pos; pos.x = x; pos.y = y; pos.z = z; list.Add(pos);
        }
        struct Pos { public ushort x, y, z; }
        struct CatchPos
        {
            public ushort x, y, z, radius;
            public byte type;
            public BrushType brush;
        }
        //enum BrushType { solid, hollow, walls, holes, wire, random };
        enum BrushType { sphere, cube, random }
    }
}