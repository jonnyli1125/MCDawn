using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdSpheroid : Command
    {
        public override string name { get { return "spheroid"; } }
        public override string[] aliases { get { return new string[] { "e", "ellipsoid", "ellipse" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdSpheroid() { }

        public override void Use(Player p, string message)
        {
            CatchPos cpos;

            cpos.x = 0; cpos.y = 0; cpos.z = 0;

            if (message == "")
            {
                cpos.type = Block.Zero;
                cpos.build = "";
            }
            else if (message.IndexOf(' ') == -1)
            {
                cpos.type = Block.Byte(message);
                cpos.build = "";
                if (!Block.canPlace(p, cpos.type)) { Player.SendMessage(p, "Cannot place that."); return; }
                if (cpos.type == Block.Zero)
                {
                    if (message.ToLower() == "vertical")
                    {
                        cpos.build = "vertical";
                    }
                    else if (message.ToLower() == "hollow")
                    {
                        cpos.build = "hollow";
                    }
                    else
                    {
                        Help(p); return;
                    }
                }
            }
            else
            {
                cpos.type = Block.Byte(message.Split(' ')[0]);
                if (!Block.canPlace(p, cpos.type)) { Player.SendMessage(p, "Cannot place that."); return; }
                if (cpos.type == Block.Zero || (message.Split(' ')[1].ToLower() != "vertical" && message.Split(' ')[1].ToLower() != "hollow"))
                {
                    Help(p); return;
                }
                if (message.Split(' ')[1].ToLower() == "vertical")
                {
                    cpos.build = "vertical";
                }
                else if (message.Split(' ')[1].ToLower() == "hollow")
                {
                    cpos.build = "hollow";
                }
                else
                {
                    Help(p); return;
                }
            }

            if (!Block.canPlace(p, cpos.type) && cpos.type != Block.Zero) { Player.SendMessage(p, "Cannot place this block type!"); return; }

            p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/spheroid [type] [hollow/vertical] - Create a spheroid of blocks.");
            Player.SendMessage(p, "If [hollow] is added, it will be a hollow spheroid.");
            Player.SendMessage(p, "If [vertical] is added, it will be a vertical tube");
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
            if (cpos.type != Block.Zero) { type = cpos.type; }
            List<Pos> buffer = new List<Pos>();

            none:
            if (cpos.build == "")
            {
                /* Courtesy of fCraft's awesome Open-Source'ness :D */

                // find start/end coordinates
                int sx = Math.Min(cpos.x, x);
                int ex = Math.Max(cpos.x, x);
                int sy = Math.Min(cpos.y, y);
                int ey = Math.Max(cpos.y, y);
                int sz = Math.Min(cpos.z, z);
                int ez = Math.Max(cpos.z, z);

                // find axis lengths
                double rx = (ex - sx + 1) / 2d;
                double ry = (ey - sy + 1) / 2d;
                double rz = (ez - sz + 1) / 2d;

                double rx2 = 1 / (rx * rx);
                double ry2 = 1 / (ry * ry);
                double rz2 = 1 / (rz * rz);

                // find center points
                double cx = (ex + sx) / 2d;
                double cy = (ey + sy) / 2d;
                double cz = (ez + sz) / 2d;
                /*int totalBlocks = (int)(Math.PI * 0.75 * rx * ry * rz);

                if (totalBlocks > p.group.maxBlocks)
                {
                    Player.SendMessage(p, "You tried to spheroid " + totalBlocks + " blocks.");
                    Player.SendMessage(p, "You cannot spheroid more than " + p.group.maxBlocks + ".");
                    return;
                }

                Player.SendMessage(p, totalBlocks + " blocks.");*/

                for (int xx = sx; xx <= ex; xx += 8)
                    for (int yy = sy; yy <= ey; yy += 8)
                        for (int zz = sz; zz <= ez; zz += 8)
                            for (int z3 = 0; z3 < 8 && zz + z3 <= ez; z3++)
                                for (int y3 = 0; y3 < 8 && yy + y3 <= ey; y3++)
                                    for (int x3 = 0; x3 < 8 && xx + x3 <= ex; x3++)
                                    {
                                        // get relative coordinates
                                        double dx = (xx + x3 - cx);
                                        double dy = (yy + y3 - cy);
                                        double dz = (zz + z3 - cz);

                                        // test if it's inside ellipse
                                        if ((dx * dx) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 <= 1)
                                        {
                                            BufferAdd(buffer, (ushort)(x3 + xx), (ushort)(yy + y3), (ushort)(zz + z3));
                                            //p.level.Blockchange(p, (ushort)(x3 + xx), (ushort)(yy + y3), (ushort)(zz + z3), type);
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
                                    l.Blockchange(p, pos.x, pos.y, pos.z, type);
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
                            Player.SendMessage(p, "You tried to spheriod " + buffer.Count + " blocks.");
                            Player.SendMessage(p, "You cannot spheroid more than " + p.group.maxBlocks + ".");
                            return;
                        }

                        Player.SendMessage(p, buffer.Count.ToString() + " blocks.");

                        c = 0;
                        buffer.ForEach(delegate(Pos pos)
                        {
                            c++;
                            if (p.activeCuboids == 0) return;
                            l.Blockchange(p, pos.x, pos.y, pos.z, type);
                            if (Server.throttle > 0)
                            {
                                while (Server.pauseCuboids) { Thread.Sleep(1000); }
                                if (c >= Server.throttle)
                                {
                                    c++;
                                    if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                                }
                            }
                        });

                        if (p.activeCuboids > 0) p.activeCuboids--;
                        if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                    }
                    catch (Exception exc) { Server.ErrorLog(exc); Player.SendMessage(p, "Error!"); return; }
                })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
            }
            else if (cpos.build == "hollow")
            {

                // find start/end coordinates
                int sx = Math.Min(cpos.x, x);
                int ex = Math.Max(cpos.x, x);
                int sy = Math.Min(cpos.y, y);
                int ey = Math.Max(cpos.y, y);
                int sz = Math.Min(cpos.z, z);
                int ez = Math.Max(cpos.z, z);

                // find axis lengths
                double rx = (ex - sx + 1) / 2d;
                double ry = (ey - sy + 1) / 2d;
                double rz = (ez - sz + 1) / 2d;

                double rx2 = 1 / (rx * rx);
                double ry2 = 1 / (ry * ry);
                double rz2 = 1 / (rz * rz);

                // find center points
                double cx = (ex + sx) / 2d;
                double cy = (ey + sy) / 2d;
                double cz = (ez + sz) / 2d;

                // rougher estimation than the non-hollow form, a voxelized surface is a bit funky
                /*int totalBlocks = (int)(4 / 3d * Math.PI * ((rx + .5) * (ry + .5) * (rz + .5) - (rx - .5) * (ry - .5) * (rz - .5)) * 0.85);

                if (totalBlocks > p.group.maxBlocks)
                {
                    Player.SendMessage(p, "You tried to spheroid " + totalBlocks + " blocks.");
                    Player.SendMessage(p, "You cannot spheroid more than " + p.group.maxBlocks + ".");
                    return;
                }

                Player.SendMessage(p, totalBlocks + " blocks.");*/

                for (int xx = sx; xx <= ex; xx++)
                {
                    for (int yy = sy; yy <= ey; yy++)
                    {
                        for (int zz = sz; zz <= ez; zz++)
                        {
                            // get relative coordinates
                            double dx = (xx - cx);
                            double dy = (yy - cy);
                            double dz = (zz - cz);

                            if ((dx * dx) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 <= 1)
                            {
                                // we touched the surface
                                // keep drilling until we hit an internal block
                                do
                                {
                                    BufferAdd(buffer, (ushort)xx, (ushort)yy, (ushort)zz);
                                    BufferAdd(buffer, (ushort)xx, (ushort)yy, (ushort)(cz - dz));
                                    //p.level.Blockchange(p, (ushort)xx, (ushort)yy, (ushort)zz, type);
                                    //p.level.Blockchange(p, (ushort)xx, (ushort)yy, (ushort)(cz - dz), type);
                                    dz = (++zz - cz);
                                } while (zz <= (int)cz &&
                                        ((dx + 1) * (dx + 1) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 > 1 ||
                                         (dx - 1) * (dx - 1) * rx2 + (dy * dy) * ry2 + (dz * dz) * rz2 > 1 ||
                                         (dx * dx) * rx2 + (dy + 1) * (dy + 1) * ry2 + (dz * dz) * rz2 > 1 ||
                                         (dx * dx) * rx2 + (dy - 1) * (dy - 1) * ry2 + (dz * dz) * rz2 > 1 ||
                                         (dx * dx) * rx2 + (dy * dy) * ry2 + (dz + 1) * (dz + 1) * rz2 > 1 ||
                                         (dx * dx) * rx2 + (dy * dy) * ry2 + (dz - 1) * (dz - 1) * rz2 > 1));
                                break;
                            }
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
                                    l.Blockchange(p, pos.x, pos.y, pos.z, type);
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
                            Player.SendMessage(p, "You tried to spheroid " + buffer.Count + " blocks.");
                            Player.SendMessage(p, "You cannot spheroid more than " + p.group.maxBlocks + ".");
                            return;
                        }

                        Player.SendMessage(p, buffer.Count.ToString() + " blocks.");

                        c = 0;
                        buffer.ForEach(delegate(Pos pos)
                        {
                            c++;
                            if (p.activeCuboids == 0) return;
                            l.Blockchange(p, pos.x, pos.y, pos.z, type);
                            if (Server.throttle > 0)
                            {
                                while (Server.pauseCuboids) { Thread.Sleep(1000); }
                                if (c >= Server.throttle)
                                {
                                    c = 0;
                                    if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                                }
                            }
                        });

                        if (p.activeCuboids > 0) p.activeCuboids--;
                        if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                    }
                    catch (Exception exc) { Server.ErrorLog(exc); Player.SendMessage(p, "Error!"); return; }
                })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
            }
            else if (cpos.build == "vertical")
            {
                int radius = Math.Abs(cpos.x - x) / 2;
                int f = 1 - radius;
                int ddF_x = 1;
                int ddF_y = -2 * radius;
                int xx = 0;
                int zz = radius;

                int x0 = Math.Min(cpos.x, x) + radius;
                int z0 = Math.Min(cpos.z, z) + radius;

                Pos pos = new Pos();
                pos.x = (ushort)x0; pos.z = (ushort)(z0 + radius); buffer.Add(pos);
                pos.z = (ushort)(z0 - radius); buffer.Add(pos);
                pos.x = (ushort)(x0 + radius); pos.z = (ushort)z0; buffer.Add(pos);
                pos.x = (ushort)(x0 - radius); buffer.Add(pos);

                while (xx < zz)
                {
                    if (f >= 0)
                    {
                        zz--;
                        ddF_y += 2;
                        f += ddF_y;
                    }
                    xx++;
                    ddF_x += 2;
                    f += ddF_x;

                    pos.z = (ushort)(z0 + zz);
                    pos.x = (ushort)(x0 + xx); buffer.Add(pos);
                    pos.x = (ushort)(x0 - xx); buffer.Add(pos);
                    pos.z = (ushort)(z0 - zz);
                    pos.x = (ushort)(x0 + xx); buffer.Add(pos);
                    pos.x = (ushort)(x0 - xx); buffer.Add(pos);
                    pos.z = (ushort)(z0 + xx);
                    pos.x = (ushort)(x0 + zz); buffer.Add(pos);
                    pos.x = (ushort)(x0 - zz); buffer.Add(pos);
                    pos.z = (ushort)(z0 - xx);
                    pos.x = (ushort)(x0 + zz); buffer.Add(pos);
                    pos.x = (ushort)(x0 - zz); buffer.Add(pos);
                }

                int ydiff = Math.Abs(y - cpos.y) + 1;

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
                            foreach (Pos Pos in buffer)  // can't use list.foreach, as these are "incomplete" pos structs... y value is not assigned >_>
                            {
                                if (counter <= p.group.maxBlocks)
                                {
                                    c++;
                                    if (p.activeCuboids == 0) return;
                                    counter++;
                                    for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++) { l.Blockchange(p, Pos.x, yy, Pos.z, type); }
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
                            if (counter * ydiff >= p.group.maxBlocks)
                            {
                                Player.SendMessage(p, "Tried to cuboid " + (buffer.Count * ydiff) + " blocks, but your limit is " + p.group.maxBlocks + ".");
                                Player.SendMessage(p, "Executed cuboid up to limit.");
                            }
                            else
                            {
                                Player.SendMessage(p, (buffer.Count * ydiff) + " blocks.");
                            }

                            if (p.activeCuboids > 0) p.activeCuboids--;
                            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                            return;
                        }

                        if (buffer.Count * ydiff > p.group.maxBlocks)
                        {
                            Player.SendMessage(p, "You tried to spheroid " + (buffer.Count * ydiff) + " blocks.");
                            Player.SendMessage(p, "You cannot spheroid more than " + p.group.maxBlocks + ".");
                            return;
                        }

                        Player.SendMessage(p, (buffer.Count * ydiff) + " blocks.");

                        c = 0;
                        foreach (Pos Pos in buffer)
                        {
                            if (p.activeCuboids == 0) return;
                            for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++) { l.Blockchange(p, Pos.x, yy, Pos.z, type); }
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
                        if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                    }
                    catch (Exception exc) { Server.ErrorLog(exc); Player.SendMessage(p, "Error!"); return; }
                })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
            }
            else
            {
                goto none;
            }
        }
        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z)
        {
            Pos pos; pos.x = x; pos.y = y; pos.z = z; list.Add(pos);
        }
        struct Pos { public ushort x, y, z; }
        struct CatchPos
        {
            public byte type;
            public ushort x, y, z;
            public string build;
        }
    }
}