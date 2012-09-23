using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace MCDawn
{
    public class CmdPaste : Command
    {
        public override string name { get { return "paste"; } }
        public override string[] aliases { get { return new string[] { "v" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public string loadname;
        public CmdPaste() { }
        
        public override void Use(Player p, string message)
        {
            if (message != "") { Help(p); return; }

            CatchPos cpos;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place a block in the corner of where you want to paste."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/paste - Pastes the stored copy.");
            Player.SendMessage(p, "&4BEWARE: " + Server.DefaultColor + "The blocks will always be pasted in a set direction");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);

            Player.UndoPos Pos1;
            //p.UndoBuffer.Clear();

            Level l = p.level;
            p.activeCuboids++;
            Thread t = new Thread(new ThreadStart(delegate
            {
                try
                {
                    int c = 0;
                    p.CopyBuffer.ForEach(delegate(Player.CopyPos pos)
                    {
                        c++;
                        if (p.activeCuboids == 0) return;
                        Pos1.x = (ushort)(Math.Abs(pos.x) + x);
                        Pos1.y = (ushort)(Math.Abs(pos.y) + y);
                        Pos1.z = (ushort)(Math.Abs(pos.z) + z);

                        if (pos.type != Block.air || p.copyAir)
                            unchecked
                            {
                                if (p.level.GetTile(Pos1.x, Pos1.y, Pos1.z) != Block.Zero)
                                    l.Blockchange(p, (ushort)(Pos1.x + p.copyoffset[0]), (ushort)(Pos1.y + p.copyoffset[1]), (ushort)(Pos1.z + p.copyoffset[2]), pos.type);
                            }
                        while (Server.pauseCuboids) { Thread.Sleep(1000); }
                        if (c >= Server.throttle)
                        {
                            c = 0;
                            if (l.players.Count > 0 && !l.Instant) Thread.Sleep(100);
                        }
                    });
                    Player.SendMessage(p, "Pasted " + p.CopyBuffer.Count + " blocks.");
                    if (p.activeCuboids > 0) p.activeCuboids--;
                    if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
                }
                catch (Exception ex) { Server.ErrorLog(ex); Player.SendMessage(p, "Error!"); return; }
            })); t.IsBackground = true; t.Priority = ThreadPriority.Lowest; t.Start();
        }

        struct CatchPos { public ushort x, y, z; }
    }
}