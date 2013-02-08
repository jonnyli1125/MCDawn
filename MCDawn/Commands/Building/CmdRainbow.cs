using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdRainbow : Command
    {
        public override string name { get { return "rainbow"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdRainbow() { }

        public override void Use(Player p, string message)
        {
            CatchPos cpos;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;
            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/rainbow - Taste the rainbow");
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
            List<Pos> buffer = new List<Pos>();

            byte newType = Block.darkpink;

            int xdif = Math.Abs(cpos.x - x);
            int ydif = Math.Abs(cpos.y - y);
            int zdif = Math.Abs(cpos.z - z);

            if (xdif >= ydif && xdif >= zdif)
            {
                for (ushort xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); xx++)
                {
                    newType += 1;
                    if (newType > Block.darkpink) newType = Block.red;
                    for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++)
                    {
                        for (ushort zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); zz++)
                        {
                            if (p.level.GetTile(xx, yy, zz) != Block.air)
                                BufferAdd(buffer, xx, yy, zz, newType);
                        }
                    }
                }
            }
            else if (ydif > xdif && ydif > zdif)
            {
                for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++)
                {
                    newType += 1;
                    if (newType > Block.darkpink) newType = Block.red;
                    for (ushort xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); xx++)
                    {
                        for (ushort zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); zz++)
                        {
                            if (p.level.GetTile(xx, yy, zz) != Block.air)
                                BufferAdd(buffer, xx, yy, zz, newType);
                        }
                    }
                }
            }
            else if (zdif > ydif && zdif > xdif)
            {
                for (ushort zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); zz++)
                {
                    newType += 1;
                    if (newType > Block.darkpink) newType = Block.red;
                    for (ushort yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); yy++)
                    {
                        for (ushort xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); xx++)
                        {
                            if (p.level.GetTile(xx, yy, zz) != Block.air)
                                BufferAdd(buffer, xx, yy, zz, newType);
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
                                l.Blockchange(p, pos.x, pos.y, pos.z, pos.newType);
                                while (Server.pauseCuboids) { Thread.Sleep(1000); }
                                if (c >= Server.throttle)
                                {
                                    c = 0;
                                    if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
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
                        l.Blockchange(p, pos.x, pos.y, pos.z, pos.newType);
                        while (Server.pauseCuboids) { Thread.Sleep(1000); }
                        if (c >= Server.throttle)
                        {
                            c = 0;
                            if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                        }
                    });

                    if (p.activeCuboids > 0) p.activeCuboids--;
                    if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }
        void BufferAdd(List<Pos> list, ushort x, ushort y, ushort z, byte newType)
        {
            Pos pos;
            pos.x = x; pos.y = y; pos.z = z; pos.newType = newType;
            list.Add(pos);
        }

        struct Pos { public ushort x, y, z; public byte newType; }
        struct CatchPos { public ushort x, y, z; }
    }
}
