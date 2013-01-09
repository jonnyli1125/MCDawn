using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdDrill : Command
    {
        public override string name { get { return "drill"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdDrill() { }

        public override void Use(Player p, string message)
        {
            CatchPos cpos;
            cpos.distance = 20;

            if (message != "")
                try
                {
                    cpos.distance = int.Parse(message);
                }
                catch { Help(p); return; }

            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;

            Player.SendMessage(p, "Destroy the block you wish to drill."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/drill [distance] - Drills a hole, destroying all similar blocks in a 3x3 rectangle ahead of you.");
        }
        
        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            if (!p.staticCommands) p.ClearBlockchange();
            CatchPos cpos = (CatchPos)p.blockchangeObject;
            byte oldType = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, oldType);

            int diffX = 0, diffZ = 0;

            if (p.rot[0] <= 32 || p.rot[0] >= 224) { diffZ = -1; }
            else if (p.rot[0] <= 96) { diffX = 1; }
            else if (p.rot[0] <= 160) { diffZ = 1; }
            else diffX = -1;

            List<Pos> buffer = new List<Pos>();
            Pos pos;
            int total = 0;

            if (diffX != 0)
            {
                for (ushort xx = x; total < cpos.distance; xx += (ushort)diffX)
                {
                    for (ushort yy = (ushort)(y - 1); yy <= (ushort)(y + 1); yy++)
                        for (ushort zz = (ushort)(z - 1); zz <= (ushort)(z + 1); zz++)
                        {
                            pos.x = xx; pos.y = yy; pos.z = zz;
                            buffer.Add(pos);
                        }
                    total++;
                }
            }
            else
            {
                for (ushort zz = z; total < cpos.distance; zz += (ushort)diffZ)
                {
                    for (ushort yy = (ushort)(y - 1); yy <= (ushort)(y + 1); yy++)
                        for (ushort xx = (ushort)(x - 1); xx <= (ushort)(x + 1); xx++)
                        {
                            pos.x = xx; pos.y = yy; pos.z = zz;
                            buffer.Add(pos);
                        }
                    total++;
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
                        foreach (Pos pos1 in buffer)
                        {
                            if (counter <= p.group.maxBlocks)
                            {
                                c++;
                                if (p.activeCuboids == 0) return;
                                counter++;
                                if (l.GetTile(pos1.x, pos1.y, pos1.z) == oldType)
                                    l.Blockchange(p, pos1.x, pos1.y, pos1.z, Block.air);
                                while (Server.pauseCuboids) { Thread.Sleep(1000); }
                                if (c >= Server.throttle)
                                {
                                    c = 0;
                                    if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                                }
                            }
                        }
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
                        Player.SendMessage(p, "You tried to cuboid " + buffer.Count + " blocks.");
                        Player.SendMessage(p, "You cannot cuboid more than " + p.group.maxBlocks + ".");
                        return;
                    }

                    Player.SendMessage(p, buffer.Count.ToString() + " blocks.");
                    
                    c = 0;
                    foreach (Pos pos1 in buffer)
                    {
                        c++;
                        if (p.activeCuboids == 0) return;
                        if (l.GetTile(pos1.x, pos1.y, pos1.z) == oldType)
                            l.Blockchange(p, pos1.x, pos1.y, pos1.z, Block.air);
                        while (Server.pauseCuboids) { Thread.Sleep(1000); }
                        if (c >= Server.throttle)
                        {
                            c = 0;
                            if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                        }
                    }

                    if (p.activeCuboids > 0) p.activeCuboids--;
                    if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }

        struct CatchPos { public ushort x, y, z; public int distance; }
        struct Pos { public ushort x, y, z; }
    }
}