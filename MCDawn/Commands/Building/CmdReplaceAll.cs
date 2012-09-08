using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MCDawn
{
    class CmdReplaceAll : Command
    {
        public override string name { get { return "replaceall"; } }
        public override string[] aliases { get { return new string[] { "ra" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Admin; } }
        public CmdReplaceAll() { }

        public override void Use(Player p, string message)
        {
            if (message.IndexOf(' ') == -1 || message.Split(' ').Length > 2) { Help(p); return; }

            byte b1, b2;

            b1 = Block.Byte(message.Split(' ')[0]);
            b2 = Block.Byte(message.Split(' ')[1]);

            if (b1 == Block.Zero || b2 == Block.Zero) { Player.SendMessage(p, "Could not find specified blocks."); return; }
            ushort x, y, z; int currentBlock = 0;
            List<Pos> stored = new List<Pos>(); Pos pos;

            foreach (byte b in p.level.blocks)
            {
                if (b == b1)
                {
                    p.level.IntToPos(currentBlock, out x, out y, out z);
                    pos.x = x; pos.y = y; pos.z = z;
                    stored.Add(pos);
                }
                currentBlock++;
            }

            if (stored.Count > (p.group.maxBlocks * 2) && p.group.Permission < LevelPermission.Nobody) { Player.SendMessage(p, "Cannot replace more than " + (p.group.maxBlocks * 2) + " blocks."); return; }

            Player.SendMessage(p, stored.Count + " blocks out of " + currentBlock + " are " + Block.Name(b1));

            Level l = p.level;
            p.activeCuboids++;
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    int c = 0;
                    foreach (Pos Pos in stored)
                    {
                        c++;
                        if (p.activeCuboids == 0) return;
                        l.Blockchange(p, Pos.x, Pos.y, Pos.z, b2);
                        if (c >= 2)
                        {
                            c = 0;
                            if (!Server.pauseCuboids) Thread.Sleep(10 - Server.throttle);
                            else { while (Server.pauseCuboids) { Thread.Sleep(1000); } }
                        }
                    }
                    if (p.activeCuboids > 0) p.activeCuboids--;
                    Player.SendMessage(p, "&4/replaceall finished!");
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }
        public struct Pos { public ushort x, y, z; }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/replaceall [block1] [block2] - Replaces all of [block1] with [block2] in a map");
        }
    }
}
