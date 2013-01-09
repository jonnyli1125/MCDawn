using System;
using System.IO;
using System.Collections.Generic;
//using MySql.Data.MySqlClient;
//using MySql.Data.Types;
using System.Data;

namespace MCDawn
{
    public class CmdPortal : Command
    {
        public override string name { get { return "portal"; } }
        public override string[] aliases { get { return new string[] { "o" }; } }
        public override string type { get { return "build"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.AdvBuilder; } }
        public CmdPortal() { }

        public override void Use(Player p, string message)
        {
            if (!Server.useMySQL && (!message.ToLower().Contains("home") && !message.ToLower().Contains("player"))) { p.SendMessage("MySQL has not been configured! Please configure MySQL to use Portals!"); return; }
            portalPos portalPos;

            portalPos.Multi = "";

            if (message.IndexOf(' ') != -1)
            {
                if (message.Split(' ')[1].ToLower() == "multi")
                {
                    portalPos.Multi = "multi";
                    message = message.Split(' ')[0];
                }
                else if (message.Split(' ')[1].ToLower() == "cuboid")
                {
                    portalPos.Multi = "cuboid";
                    message = message.Split(' ')[0];
                }
                else
                {
                    Player.SendMessage(p, "Invalid parameters");
                    return;
                }
            }

            if (message.ToLower() == "blue" || message == "") { portalPos.type = Block.blue_portal; }
            else if (message.ToLower() == "orange") { portalPos.type = Block.orange_portal; }
            else if (message.ToLower() == "air") { portalPos.type = Block.air_portal; }
            else if (message.ToLower() == "water") { portalPos.type = Block.water_portal; }
            else if (message.ToLower() == "lava") { portalPos.type = Block.lava_portal; }
            else if (message.ToLower() == "home" || message.ToLower() == "player") { portalPos.type = Block.home_portal; }
            else if (message.ToLower() == "show") { showPortals(p); return; }
            else { Help(p); return; }

            p.ClearBlockchange();

            portPos port;

            port.x = 0; port.y = 0; port.z = 0; port.portMapName = "";
            portalPos.port = new List<portPos>();

            p.blockchangeObject = portalPos;
            if (portalPos.Multi.ToLower() != "cuboid") 
            { 
                Player.SendMessage(p, "Place a the &aEntry block" + "&g for the portal"); 
                p.ClearBlockchange();
                p.Blockchange += new Player.BlockchangeEventHandler(EntryChange);
            }
            else 
            { 
                Player.SendMessage(p, "Place two blocks" + "&g to determine the edges.");
                p.ClearBlockchange();
                p.Blockchange += new Player.BlockchangeEventHandler(portalCuboid1);
            }
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/portal [orange/blue/air/water/lava/home] [multi] - Activates Portal mode.");
            Player.SendMessage(p, "/portal [type] multi - Place Entry blocks until exit is wanted.");
            Player.SendMessage(p, "/portal [type] cuboid - Place Entry blocks within a cuboid until exit is wanted.");
            Player.SendMessage(p, "/portal show - Shows portals, green = in, red = out.");
            Player.SendMessage(p, "Using /portal home [multi] creates a portal that brings players to their player home.");
        }

        public void portalCuboid1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            if (type == Block.red) { ExitChange(p, x, y, z, type); return; }
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            portalPos bp = (portalPos)p.blockchangeObject;
            if (bp.Multi != "cuboid") { Player.SendMessage(p, "lolwut, glitch :/"); return; } 
            portPos port = new portPos();
            port.x = x; port.y = y; port.z = z; port.portMapName = p.level.name;
            bp.port.Add(port);
            p.blockchangeObject = bp;
            p.Blockchange += new Player.BlockchangeEventHandler(portalCuboid2);
        }
        public void portalCuboid2(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            portalPos bp = (portalPos)p.blockchangeObject;
            if (bp.Multi != "cuboid") { Player.SendMessage(p, "lolwut, glitch :/"); return; } 
            portPos pos = bp.port[0];
            if (pos.portMapName != p.level.name) { Player.SendMessage(p, "Portal cuboid must be on same map as first point."); return; }
            ushort xx; ushort yy; ushort zz; portPos port; int counter = 0;
            for (xx = Math.Min(pos.x, x); xx <= Math.Max(pos.x, x); ++xx)
                for (yy = Math.Min(pos.y, y); yy <= Math.Max(pos.y, y); ++yy)
                    for (zz = Math.Min(pos.z, z); zz <= Math.Max(pos.z, z); ++zz)
                    {
                        counter++;
                        port = new portPos();
                        port.x = xx; port.y = yy; port.z = zz; port.portMapName = p.level.name;
                        bp.port.Add(port);
                        p.level.Blockchange(xx, yy, zz, bp.type);
                        p.SendBlockchange(xx, yy, zz, Block.green);
                    }
            p.blockchangeObject = bp;
            Player.SendMessage(p, "Cuboided " + counter + " portals.");
            if (bp.port.Count != (counter + 1)) { Player.SendMessage(p, "You have " + bp.port.Count + " portals in total."); }
            Player.SendMessage(p, "&aYou can continue cuboiding, or &cplace a red block for exit.");
            p.Blockchange += new Player.BlockchangeEventHandler(portalCuboid1);

        }
        public void EntryChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            portalPos bp = (portalPos)p.blockchangeObject;

            if (bp.Multi.ToLower() == "multi" && type == Block.red && bp.port.Count > 0) { ExitChange(p, x, y, z, type); return; }

            byte b = p.level.GetTile(x, y, z);
            p.level.Blockchange(p, x, y, z, bp.type);
            p.SendBlockchange(x, y, z, Block.green);
            portPos Port;

            Port.portMapName = p.level.name;
            Port.x = x; Port.y = y; Port.z = z;

            bp.port.Add(Port);

            p.blockchangeObject = bp;

            if (bp.Multi.ToLower() != "multi")
            {
                p.Blockchange += new Player.BlockchangeEventHandler(ExitChange);
                Player.SendMessage(p, "&aEntry block placed");
            }
            else if (bp.Multi.ToLower() == "multi")
            {
                p.Blockchange += new Player.BlockchangeEventHandler(EntryChange);
                Player.SendMessage(p, "&aEntry block placed. &cPlace a Red block for exit.");
            }
        }

        // Home Portals.
        public void HomeEntryChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            portalPos bp = (portalPos)p.blockchangeObject;

            if (bp.Multi.ToLower() == "multi" && type == Block.red && bp.port.Count > 0) { ExitChange(p, x, y, z, type); return; }
            else if (bp.Multi.ToLower() == "cuboid" && type == Block.red && bp.port.Count > 0) { ExitChange(p, x, y, z, type); return; }

            byte b = p.level.GetTile(x, y, z);
            p.level.Blockchange(p, x, y, z, bp.type);
            p.SendBlockchange(x, y, z, Block.green);

            if (bp.Multi.ToLower() != "multi" && bp.Multi.ToLower() != "cuboid")
            {
                p.Blockchange += new Player.BlockchangeEventHandler(ExitChange);
                Player.SendMessage(p, "&aEntry block placed");
            }
            else
            {
                p.Blockchange += new Player.BlockchangeEventHandler(EntryChange);
                Player.SendMessage(p, "&aEntry block placed. &cPlace a Red block anywhere to finish.");
            }
        }

        public void ExitChange(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            byte b = p.level.GetTile(x, y, z);
            p.SendBlockchange(x, y, z, b);
            portalPos bp = (portalPos)p.blockchangeObject;

            foreach (portPos pos in bp.port)
            {
                DataTable Portals = MySQL.fillData("SELECT * FROM `Portals" + pos.portMapName + "` WHERE EntryX=" + (int)pos.x + " AND EntryY=" + (int)pos.y + " AND EntryZ=" + (int)pos.z);
                Portals.Dispose();

                if (Portals.Rows.Count == 0)
                {
                    MySQL.executeQuery("INSERT INTO `Portals" + pos.portMapName + "` (EntryX, EntryY, EntryZ, ExitMap, ExitX, ExitY, ExitZ) VALUES (" + (int)pos.x + ", " + (int)pos.y + ", " + (int)pos.z + ", '" + p.level.name + "', " + (int)x + ", " + (int)y + ", " + (int)z + ")");
                }
                else
                {
                    MySQL.executeQuery("UPDATE `Portals" + pos.portMapName + "` SET ExitMap='" + p.level.name + "', ExitX=" + (int)x + ", ExitY=" + (int)y + ", ExitZ=" + (int)z + " WHERE EntryX=" + (int)pos.x + " AND EntryY=" + (int)pos.y + " AND EntryZ=" + (int)pos.z);
                }
                //DB

                if (pos.portMapName == p.level.name) p.SendBlockchange(pos.x, pos.y, pos.z, bp.type);
            }

            Player.SendMessage(p, "&3Exit" + "&g block placed");

            if (p.staticCommands) { bp.port.Clear(); p.blockchangeObject = bp; p.Blockchange += new Player.BlockchangeEventHandler(EntryChange); }
        }

        public struct portalPos { public List<portPos> port; public byte type; public string Multi; }
        public struct portPos { public ushort x, y, z; public string portMapName; }

        public void showPortals(Player p)
        {
            p.showPortals = !p.showPortals;

            DataTable Portals = MySQL.fillData("SELECT * FROM `Portals" + p.level.name + "`");

            int i;

            if (p.showPortals)
            {
                for (i = 0; i < Portals.Rows.Count; i++)
                {
                    if (Portals.Rows[i]["ExitMap"].ToString() == p.level.name)
                        p.SendBlockchange((ushort)Portals.Rows[i]["ExitX"], (ushort)Portals.Rows[i]["ExitY"], (ushort)Portals.Rows[i]["ExitZ"], Block.orange_portal);
                    p.SendBlockchange((ushort)Portals.Rows[i]["EntryX"], (ushort)Portals.Rows[i]["EntryY"], (ushort)Portals.Rows[i]["EntryZ"], Block.blue_portal);
                }

                Player.SendMessage(p, "Now showing &a" + i.ToString() + "&g portals.");
            }
            else
            {
                for (i = 0; i < Portals.Rows.Count; i++)
                {
                    if (Portals.Rows[i]["ExitMap"].ToString() == p.level.name)
                        p.SendBlockchange((ushort)Portals.Rows[i]["ExitX"], (ushort)Portals.Rows[i]["ExitY"], (ushort)Portals.Rows[i]["ExitZ"], Block.air);

                    p.SendBlockchange((ushort)Portals.Rows[i]["EntryX"], (ushort)Portals.Rows[i]["EntryY"], (ushort)Portals.Rows[i]["EntryZ"], p.level.GetTile((ushort)Portals.Rows[i]["EntryX"], (ushort)Portals.Rows[i]["EntryY"], (ushort)Portals.Rows[i]["EntryZ"]));
                }

                Player.SendMessage(p, "Now hiding portals.");
            }

            Portals.Dispose();
        }
    }
}