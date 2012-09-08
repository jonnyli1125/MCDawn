using System;
using System.Collections.Generic;
using System.Linq;

namespace MCDawn
{
    public class CmdReflection : Command
    {
        public override string name { get { return "reflection"; } }
        public override string[] aliases { get { return new string[] { "rr" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdReflection() { }

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            Level.Reflection r = new Level.Reflection();
            r.reflectionPositions = new List<Level.ReflectionPos>();
            switch (message.ToLower())
            {
                case "":
                case "line":
                    r.reflectionType = Level.ReflectionType.Line;
                    p.ClearBlockchange();
                    p.blockchangeObject = r;
                    Player.SendMessage(p, "Place 2 blocks to determine the direction of the line of symmetry.");
                    p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                    break;
                case "clear":
                    foreach (Level l in Server.levels)
                    {
                        if (l.reflections.ContainsKey(p.name.ToLower()))
                            l.reflections.Remove(p.name.ToLower());
                        if (l.reflectionLines.ContainsKey(p.name.ToLower()))
                            l.reflectionLines.Remove(p.name.ToLower());
                    }
                    Player.SendMessage(p, "Reflection line(s) cleared.");
                    break;
                default: Help(p); break;
            }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/reflection - Set the reflection line.");
            Player.SendMessage(p, "/reflection cross - Set 2 reflection lines (place 3 blocks, first 2 to determine first line, 3rd to determine perpendicular line)");
            Player.SendMessage(p, "/reflection platform - Set 2 reflection lines (place 3 blocks, first 2 to determine first line, 3rd to determine line to create platform)");
            Player.SendMessage(p, "/reflection cross3d - Set 3 reflection lines (place 4 blocks, first 2 to determine first line, 3rd to determine perpendicular line, 4th to determine perpendicular line to the first \"cross\")");
            Player.SendMessage(p, "/reflection show - Show reflection lines.");
            Player.SendMessage(p, "/reflection hide - Hide reflection lines.");
            Player.SendMessage(p, "/reflection clear - Clear your reflection line(s).");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            Level.Reflection r = (Level.Reflection)p.blockchangeObject;
            Level.ReflectionPos rp = new Level.ReflectionPos();
            rp.x = x; rp.y = y; rp.z = z;
            r.reflectionPositions.Add(rp); 
            p.blockchangeObject = r;
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange2);
        }

        public void Blockchange2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            Level.Reflection r = (Level.Reflection)p.blockchangeObject;
            Level.ReflectionPos rp = new Level.ReflectionPos();
            rp.x = x; rp.y = y; rp.z = z;
            r.reflectionPositions.Add(rp);
            p.blockchangeObject = r;
            if (r.reflectionType != Level.ReflectionType.Line) { p.Blockchange += new Player.BlockchangeEventHandler(Blockchange3); }
            else
            {
                if (r.reflectionPositions.Count != 2) { Player.SendMessage(p, "Incorrect number of blocks placed."); return; }
                #region AddLine
                Level.ReflectionLinePos pos;
                List<Level.ReflectionLinePos> buffer = new List<Level.ReflectionLinePos>();
                int i, dx, dy, dz, l, m, n, x_inc, y_inc, z_inc, err_1, err_2, dx2, dy2, dz2;
                int[] pixel = new int[3];

                Level.Reflection reflection = new Level.Reflection();
                for (int ii = 0; ii < p.level.reflections.Keys.Count; ii++)
                    if (new List<string>(p.level.reflections.Keys)[ii] == p.name.ToLower())
                        reflection = new List<Level.Reflection>(p.level.reflections.Values)[ii];

                pixel[0] = reflection.reflectionPositions[0].x; pixel[1] = reflection.reflectionPositions[0].y; pixel[2] = reflection.reflectionPositions[0].z;
                dx = x - reflection.reflectionPositions[0].x; dy = y - reflection.reflectionPositions[0].y; dz = z - reflection.reflectionPositions[0].z;

                x_inc = (dx < 0) ? -1 : 1; l = Math.Abs(dx);
                y_inc = (dy < 0) ? -1 : 1; m = Math.Abs(dy);
                z_inc = (dz < 0) ? -1 : 1; n = Math.Abs(dz);

                dx2 = l << 1; dy2 = m << 1; dz2 = n << 1;

                if ((m >= l) && (m >= n)) { dy = 0; m = 0; dy2 = m << 1; }
                if ((l >= m) && (l >= n))
                {
                    err_1 = dy2 - l;
                    err_2 = dz2 - l;
                    for (i = 0; i < p.level.width; i++)
                    {
                        pos.x = (ushort)pixel[0];
                        pos.y = (ushort)pixel[1];
                        pos.z = (ushort)pixel[2];
                        pos.type = p.level.GetTile(pos.x, pos.y, pos.z);
                        buffer.Add(pos);

                        if (err_1 > 0)
                        {
                            pixel[1] += y_inc;
                            err_1 -= dx2;
                        }
                        if (err_2 > 0)
                        {
                            pixel[2] += z_inc;
                            err_2 -= dx2;
                        }
                        err_1 += dy2;
                        err_2 += dz2;
                        pixel[0] += x_inc;
                    }
                }
                else
                {
                    err_1 = dy2 - n;
                    err_2 = dx2 - n;
                    for (i = 0; i < p.level.depth; i++)
                    {
                        pos.x = (ushort)pixel[0];
                        pos.y = (ushort)pixel[1];
                        pos.z = (ushort)pixel[2];
                        pos.type = p.level.GetTile(pos.x, pos.y, pos.z);
                        buffer.Add(pos);

                        if (err_1 > 0)
                        {
                            pixel[1] += y_inc;
                            err_1 -= dz2;
                        }
                        if (err_2 > 0)
                        {
                            pixel[0] += x_inc;
                            err_2 -= dz2;
                        }
                        err_1 += dy2;
                        err_2 += dx2;
                        pixel[2] += z_inc;
                    }
                }

                pos.x = (ushort)pixel[0];
                pos.y = (ushort)pixel[1];
                pos.z = (ushort)pixel[2];
                pos.type = p.level.GetTile(pos.x, pos.y, pos.z);
                buffer.Add(pos);

                if (p.level.reflectionLines.ContainsKey(p.name.ToLower()))
                    p.level.reflectionLines.Remove(p.name.ToLower());
                p.level.reflectionLines.Add(p.name.ToLower(), buffer);
                #endregion
                p.level.ShowReflectionsLines(p);
                Player.SendMessage(p, "Reflection line set. Use /reflection clear to remove.");
            }
        }

        public void Blockchange3(Player p, ushort x, ushort y, ushort z, byte type)
        {

        }

        public void Blockchange4(Player p, ushort x, ushort y, ushort z, byte type)
        {

        }

        public static List<Level.ReflectionLinePos> GetReflectionLine(Player p, Level l)
        {
            for (int i = 0; i < l.reflectionLines.Keys.Count; i++)
                if (p.name.ToLower() == l.reflectionLines.Keys.ToList<string>()[i])
                    return l.reflectionLines.Values.ToList<List<Level.ReflectionLinePos>>()[i];
            return new List<Level.ReflectionLinePos>();
        }
    }
}