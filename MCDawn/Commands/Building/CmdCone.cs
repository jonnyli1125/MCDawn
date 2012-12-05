// This work is licensed to jonnyli1125 (Jonny Li) under a Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
// You may obtain a copy of the License at: http://creativecommons.org/licenses/by-nc-sa/3.0/

using System;
using System.Collections.Generic;
using System.Threading;

namespace MCDawn
{
    public class CmdCone : Command
    {
        public override string name { get { return "cone"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not executable from Console."); return; }
            if (message.Split(' ').Length > 4 || message.Split(' ').Length < 3) { Help(p); return; }
            ushort radius;
            if (!ushort.TryParse(message.Split(' ')[0], out radius)) { Player.SendMessage(p, "Invalid radius."); return; }
            else if (radius < 1) { Player.SendMessage(p, "Radius must be larger than 0."); return; }
            ushort verticalExpansion;
            if (!ushort.TryParse(message.Split(' ')[1], out verticalExpansion)) { Player.SendMessage(p, "Invalid vertical expansion."); return; }
            else if (radius < 1) { Player.SendMessage(p, "Vertical expansion must be larger than 0."); return; }
            byte type = Block.Byte(message.Split(' ')[2]);
            if (type == 255) { Player.SendMessage(p, "Block could not be found."); return; }
            ConeType coneType;
            if (message.Split(' ').Length == 3) coneType = ConeType.Hollow;
            else
            {
                switch (message.Split(' ')[3].ToLower())
                {
                    case "hollow": coneType = ConeType.Hollow; break;
                    case "solid": coneType = ConeType.Solid; break;
                    default: Help(p); return;
                }
            }
            CreateCone(p, type, coneType, radius, verticalExpansion);
        }

        public void CreateCone(Player p, byte type, ConeType coneType, ushort radius, ushort verticalExpansion)
        {
            List<Pos> buffer = new List<Pos>();
            Level level = p.level;
            ushort cx = (ushort)(p.pos[0] / 32), cy = (ushort)((p.pos[1] / 32) - 1), cz = (ushort)(p.pos[2] / 32);
            ushort sx = (ushort)(cx - radius - 1), sy = cy, sz = (ushort)(cz - radius - 1), ex = (ushort)(cx + radius + 1), ey = (ushort)(cy + (radius * verticalExpansion)), ez = (ushort)(cz + radius + 1);
            ushort tempRadius = radius;
            Pos pos = new Pos(); pos.x = cx; pos.y = ey; pos.z = cz;
            for (ushort i = 0; i < verticalExpansion; i++)
                if (level.GetTile(cx, (ushort)(ey + i), cz) != type) 
                {
                    pos.y = (ushort)(ey + i);
                    buffer.Add(pos);
                }
            int temp = 0;
            if (coneType == ConeType.Hollow)
            {
                for (ushort y = sy; y < ey; y++)
                {
                    for (ushort x = sx; x < ex; x++)
                        for (ushort z = sz; z < ez; z++)
                            if (Math.Round(Distance(cx, y, cz, x, y, z)) == tempRadius)
                                if (level.GetTile(x, y, z) != type)
                                {
                                    pos.x = x; pos.y = y; pos.z = z;
                                    buffer.Add(pos);
                                }
                    temp++;
                    if (temp == verticalExpansion) 
                    {
                        temp = 0;
                        tempRadius--;
                    }
                }
            }
            else if (coneType == ConeType.Solid)
            {
                for (ushort y = sy; y < ey; y++)
                {
                    for (ushort x = sx; x < ex; x++)
                        for (ushort z = sz; z < ez; z++)
                            if (Math.Round(Distance(cx, y, cz, x, y, z)) <= tempRadius)
                                if (level.GetTile(x, y, z) != type)
                                {
                                    pos.x = x; pos.y = y; pos.z = z;
                                    buffer.Add(pos);
                                }
                    temp++;
                    if (temp == verticalExpansion)
                    {
                        temp = 0;
                        tempRadius--;
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
                            Player.SendMessage(p, "Tried to cone " + buffer.Count + " blocks, but your limit is " + p.group.maxBlocks + ".");
                            Player.SendMessage(p, "Executed cone up to limit.");
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
                        Player.SendMessage(p, "You tried to cone " + buffer.Count + " blocks.");
                        Player.SendMessage(p, "You cannot cone more than " + p.group.maxBlocks + ".");
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
            Player.SendMessage(p, "/cone <radius> <vertical expansion> <block> [hollow/solid] - Creates a cone around and above you.");
            Player.SendMessage(p, "The vertical expansion determines what to multiply the height of your cone by.");
        }

        public enum ConeType { Hollow, Solid }
        public struct Pos { public ushort x, y, z; }
    }
}