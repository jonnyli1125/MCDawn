using System;
using System.Collections.Generic;

namespace MCDawn
{
    public class CmdCage : Command
    {
        public override string name { get { return "cage"; } }
        public override string[] aliases { get { return new string[] { }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdCage() { }

        public override void Use(Player p, string message)
        {
            //if (!Block.canPlace(p, 7)) { Player.SendMessage(p, "Cannot place this block at your current rank"); return; }
            Player who = Player.Find(message.Split(' ')[0]);
            if (who == null) { Player.SendMessage(p, "Player could not be found."); return; }
            if (Server.devs.Contains(who.name.ToLower()))
            {
                Player.SendMessage(p, "Woah!! You can't cage a MCDawn Developer!");
                if (p != null) { Player.GlobalMessage(p.color + p.name + "&g is crazy! Trying to cage a MCDawn Developer!"); }
                else { Player.GlobalMessage("The Console is crazy! Trying to cage a MCDawn Developer!"); }
                return;
            }
            if (p != null && who.group.Permission > p.group.Permission) { Player.SendMessage(p, "Cannot cage a player of higher rank."); return; }
            if (p != null && (who.level.permissionbuild > p.group.Permission)) { Player.SendMessage(p, "Cannot build on that level!"); return; }

            if (message.IndexOf(" ") == -1) 
            { 
                CreateBox(p, who, false);
                Player.SendMessage(p, "Successfully caged player."); 
            }
            else if (message.Split(' ')[1].ToLower() == "freeze" || message.Split(' ')[1].ToLower() == "f") 
            { 
                CreateBox(p, who, true);
                Player.SendMessage(p, "Successfully caged and froze player."); 
            }
            else { Help(p); return; }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cage <player> [freeze] - Creates a 5x5x5 adminium box around <player>. If [freeze] is added, it will also freeze them too.");
        }

        public List<Pos> buffer = new List<Pos>();
        public void CreateBox(Player p, Player who, bool freeze)
        {
            // get current player coords
            ushort cx = (ushort)(who.pos[0] / 32);
            ushort cy = (ushort)(who.pos[1] / 32);
            ushort cz = (ushort)(who.pos[2] / 32);

            // get big coords
            ushort bx = (ushort)(cx + 2);
            ushort by = (ushort)(cy + 2);
            ushort bz = (ushort)(cz + 2);

            // get small coords
            ushort sx = (ushort)(cx - 2);
            ushort sy = (ushort)(cy - 2);
            ushort sz = (ushort)(cz - 2);

            // place blocks in buffer
            ushort xx, yy, zz;
            for (yy = Math.Min(by, sy); yy <= Math.Max(by, sy); ++yy)
                for (zz = Math.Min(bz, sz); zz <= Math.Max(bz, sz); ++zz)
                {
                    if (who.level.GetTile(bx, yy, zz) != 7) { BufferAdd(bx, yy, zz); }
                    if (bx != sx) { if (who.level.GetTile(sx, yy, zz) != 7) { BufferAdd(sx, yy, zz); } }
                }
            if (Math.Abs(bx - sx) >= 2)
            {
                for (xx = (ushort)(Math.Min(bx, sx) + 1); xx <= Math.Max(bx, sx) - 1; ++xx)
                    for (zz = Math.Min(bz, sz); zz <= Math.Max(bz, sz); ++zz)
                    {
                        if (who.level.GetTile(xx, by, zz) != 7) { BufferAdd(xx, by, zz); }
                        if (by != sy) { if (who.level.GetTile(xx, sy, zz) != 7) { BufferAdd(xx, sy, zz); } }
                    }
                if (Math.Abs(by - sy) >= 2)
                {
                    for (xx = (ushort)(Math.Min(bx, sx) + 1); xx <= Math.Max(bx, sx) - 1; ++xx)
                        for (yy = (ushort)(Math.Min(by, sy) + 1); yy <= Math.Max(by, sy) - 1; ++yy)
                        {
                            if (who.level.GetTile(xx, yy, bz) != 7) { BufferAdd(xx, yy, bz); }
                            if (bz != sz) { if (who.level.GetTile(xx, yy, sz) != 7) { BufferAdd(xx, yy, sz); } }
                        }
                }
            }

            buffer.ForEach(delegate(Pos pos)
            {
                who.level.Blockchange(p, pos.x, pos.y, pos.z, 7);
            });
            if (freeze) { who.frozen = true; }
            buffer.Clear();
        }
        public void BufferAdd(ushort x, ushort y, ushort z)
        {
            Pos pos = new Pos();
            pos.x = x; pos.y = y; pos.z = z;
            buffer.Add(pos);
        }
        public struct Pos { public ushort x, y, z; }
    }
}