// This work is licensed to jonnyli1125 (Jonny Li) under a Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// You may obtain a copy of the License at: http://creativecommons.org/licenses/by-nc-sa/3.0/

using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdDome : Command
    {
        public override string name { get { return "dome"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not executable from Console."); return; }
            if (message.Split(' ').Length > 3 || message.Split(' ').Length < 2) { Help(p); return; }
            ushort radius;
            if (!ushort.TryParse(message.Split(' ')[0], out radius)) { Player.SendMessage(p, "Invalid radius."); return; }
            else if (radius < 1) { Player.SendMessage(p, "Cannot have negative radius."); return; }
            byte type = Block.Byte(message.Split(' ')[1]);
            if (type == 255) { Player.SendMessage(p, "Block could not be found."); return; }
            DomeType domeType;
            if (message.Split(' ').Length == 2) domeType = DomeType.Hollow;
            else
            {
                switch (message.Split(' ')[2].ToLower())
                {
                    case "hollow": domeType = DomeType.Hollow; break;
                    case "solid": domeType = DomeType.Solid; break;
                    default: Help(p); return;
                }
            }
            CreateDome(p, type, domeType, radius);
        }

        public void CreateDome(Player p, byte type, DomeType domeType, ushort radius)
        {
            List<Pos> buffer = new List<Pos>();
            Level level = p.level;
            ushort cx = (ushort)(p.pos[0] / 32), cy = (ushort)(p.pos[1] / 32), cz = (ushort)(p.pos[2] / 32);
            ushort sx = (ushort)(cx - radius - 1), sy = (ushort)(cy - radius - 1), sz = (ushort)(cz - radius - 1), ex = (ushort)(cx + radius + 1), ey = (ushort)(cy + radius + 1), ez = (ushort)(cz + radius + 1);
            if (domeType == DomeType.Hollow)
            {
                for (ushort x = sx; x < ex; x++)
                    for (ushort y = sy; y < ey; y++)
                        for (ushort z = sz; z < ez; z++)
                        {
                            if (Math.Round(Distance(cx, cy, cz, x, y, z)) == radius)
                            {
                                Pos pos = new Pos(); pos.x = x; pos.y = y; pos.z = z;
                                buffer.Add(pos);
                            }
                        }
            }
            else if (domeType == DomeType.Solid)
            {
                for (ushort x = sx; x < ex; x++)
                    for (ushort y = sy; y < ey; y++)
                        for (ushort z = sz; z < ez; z++)
                        {
                            if (Math.Round(Distance(cx, cy, cz, x, y, z)) <= radius)
                            {
                                Pos pos = new Pos(); pos.x = x; pos.y = y; pos.z = z;
                                buffer.Add(pos);
                            }
                        }
            }
            Execute(p, buffer, type);
        }

        public void Execute(Player p, List<Pos> buffer, byte type)
        {
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
                            Player.SendMessage(p, "Tried to cuboid " + buffer.Count + " blocks, but your limit is " + p.group.maxBlocks + ".");
                            Player.SendMessage(p, "Executed cuboid up to limit.");
                        }
                        else
                        {
                            Player.SendMessage(p, buffer.Count.ToString() + " blocks.");
                        }

                        if (p.activeCuboids > 0) p.activeCuboids--;
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
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }

        public double Distance(double x1, double y1, double z1, double x2, double y2, double z2)
        {
            double dx = x2 - x1, dy = y2 - y1, dz = z2 - z1;
            return Math.Sqrt(Math.Pow(dx, 2) + Math.Pow(dy, 2) + Math.Pow(dz, 2));
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/dome <radius> <type> [hollow/solid] - Creates a dome around the player executing the command.");
        }

        public enum DomeType { Hollow, Solid }
        public struct Pos { public ushort x, y, z; }
    }
}