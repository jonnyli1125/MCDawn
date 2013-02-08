// Written by jonnyli1125 for use with MCDawn only.
using System;

namespace MCDawn
{
    public class CmdXray : Command
    {
        public override string name { get { return "xray"; } }
        public override string[] aliases { get { return new string[] { "xr" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdXray() { }
        public byte selectedBlock = 0;

        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (message.Trim() == "") { selectedBlock = 0; goto start; }
            byte b = Block.Byte(message.Trim());
            if (b >= 255) { Player.SendMessage(p, "Cannot find block " + message.Trim() + "."); return; } // block doesnt exist
            if (!Block.canPlace(p, b)) { Player.SendMessage(p, "Cannot xray that block."); return; } // no permissions to use block
            selectedBlock = b;
        start:
            Player.SendMessage(p, "Place two blocks to determine the edges of the x-ray.");
            CatchPos cpos;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; cpos.type = 0;
            p.blockchangeObject = cpos;
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/xray [block] - See through the given blocks between two selected points, creating something like an \"xray vision.\"");
            Player.SendMessage(p, "This will make all the selected blocks appear as air. If [block] is given, only that of block will be xrayed. ");
            Player.SendMessage(p, "To un-xray the block(s), use /reveal.");
            Player.SendMessage(p, "NOTE: Only you (the user of the command) can see the changes after the x-ray, other players will not be able to see these changes.");
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

            int counter = 0;
            ushort xx, yy, zz;
            for (xx = Math.Min(cpos.x, x); xx <= Math.Max(cpos.x, x); ++xx)
                for (yy = Math.Min(cpos.y, y); yy <= Math.Max(cpos.y, y); ++yy)
                    for (zz = Math.Min(cpos.z, z); zz <= Math.Max(cpos.z, z); ++zz)
                        if (selectedBlock <= 0)
                        {
                            counter++;
                            p.SendBlockchange(xx, yy, zz, 0); // xray block
                        }
                        else
                        {
                            if (p.level.GetTile(xx, yy, zz) == selectedBlock) // check if block is selected block
                            {
                                counter++;
                                p.SendBlockchange(xx, yy, zz, 0); // xray block
                            }
                        }
            Player.SendMessage(p, "X-Rayed " + counter + " blocks.");
            Player.SendMessage(p, "Use /reveal to restore all x-rayed blocks.");
        }

        //struct Pos { public ushort x, y, z; }
        struct CatchPos
        {
            public byte type;
            public ushort x, y, z;
        }
    }
}