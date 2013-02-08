/*
Copyright (c) 2012 by Gamemakergm
This work is licensed under the Attribution-NonCommercial-NoDerivs License. To view a copy of this license, visit http://creativecommons.org/licenses/by-nc-sa/3.0/ or send a letter to Creative Commons, 444 Castro Street, Suite 900, Mountain View, California, 94041, USA.
*/
using System;

namespace MCDawn
{
    public class CmdMiddle : Command
    {
        public override string name { get { return "middle"; } }
        public override string[] aliases { get { return new string[] { "mid" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Builder; } }
        public CmdMiddle() { }

        public override void Use(Player p, string message)
        {

            if (message.IndexOf(' ') != -1) { Help(p); return; }

            CatchPos cpos;
            cpos.x = 0; cpos.y = 0; cpos.z = 0; p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place two blocks to determine the edges.");
            p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/middle - Place a block in the middle of the two blocks specified");
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
            int[] xxx = { Math.Min(cpos.x, x), Math.Max(cpos.x, x) };
            int[] yyy = { Math.Min(cpos.y, y), Math.Max(cpos.y, y) };
            int[] zzz = { Math.Min(cpos.z, z), Math.Max(cpos.z, z) };

            bool unevenZ, unevenY, unevenX;

            ushort medz = (ushort)(GetMedian(zzz, out unevenZ));
            ushort medy = (ushort)(GetMedian(yyy, out unevenY));
            ushort medx = (ushort)(GetMedian(xxx, out unevenX));

            ushort meepZ = (unevenZ) ? (ushort)(medz + 1) : (ushort)0;
            ushort meepY = (unevenY) ? (ushort)(medy + 1) : (ushort)0;
            ushort meepX = (unevenX) ? (ushort)(medx + 1) : (ushort)0;

            if (unevenZ)
            {
                Player.SendMessage(p, "The Z coordinate was even. Placing 2 blocks for accuracy.");
            }
            if (unevenY)
            {
                Player.SendMessage(p, "The Y coordinate was even. Placing 2 blocks for accuracy.");
            }
            if (unevenX)
            {
                Player.SendMessage(p, "The X coordinate was even. Placing 2 blocks for accuracy.");
            }

            if (p.level.GetTile(medx, medy, medz) != type)
            {
                p.level.Blockchange(p, medx, medy, medz, type);
                if (meepZ != 0)
                {
                    p.level.Blockchange(p, medx, medy, meepZ, type);
                }
                if (meepY != 0)
                {
                    p.level.Blockchange(p, medx, meepY, meepZ, type);
                }
                if (meepX != 0)
                {
                    p.level.Blockchange(p, meepX, medy, meepZ, type);
                }
            }

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        struct CatchPos
        {
            public ushort x, y, z;
        }

        private static decimal GetMedian(int[] array, out bool uneven)
        {
            int[] tempArray = array;
            int count = tempArray.Length;
            Array.Sort(tempArray);

            decimal medianValue = 0;

            if (count % 2 == 0)
            {
                // count is even, need to get the middle two elements, add them together, then divide by 2
                int middleElement1 = tempArray[(count / 2) - 1];
                int middleElement2 = tempArray[(count / 2)];
                medianValue = (middleElement1 + middleElement2) / 2;
                uneven = true;
            }
            else
            {
                // count is odd, simply get the middle element.
                medianValue = tempArray[(count / 2)];
            }
            uneven = false;
            return medianValue;
        }
    }
}