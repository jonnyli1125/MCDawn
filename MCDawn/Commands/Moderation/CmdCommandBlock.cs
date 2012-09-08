/* Coded by Gamemakergm based on CmdMessageBlock */
using System;
using System.Collections.Generic;
using System.Data;

namespace MCDawn
{
    public class CmdCommandBlock : Command
    {
        public override string name { get { return "commandblock"; } }
        public override string[] aliases { get { return new string[] { "cmdblock" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Operator; } }
        public CmdCommandBlock() { }

        public override void Use(Player p, string message)
        {
            if (message == "") { Help(p); return; }

            //if (!Server.useMySQL) { p.SendMessage("MySQL has not been configured! Please configure MySQL to use Command Blocks!"); return; }
            //if (Server.mono) { p.SendMessage("Command Blocks are not possible due to Lua not being supported on Mono."); }

            CatchPos cpos;
            cpos.message = "";

            try
            {
                switch (message.Split(' ')[0])
                {
                    case "white": cpos.type = Block.cmdblock; break;
                    default: cpos.type = Block.cmdblock; cpos.message = message; break;
                }
            }
            catch { cpos.type = Block.MsgWhite; cpos.message = message; }

            if (cpos.message == "") cpos.message = message.Substring(message.IndexOf(' ') + 1);
            p.blockchangeObject = cpos;

            Player.SendMessage(p, "Place where you wish the command block to go."); p.ClearBlockchange();
            p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/cmdblocks [block] [extras/scripts/<Script>.lua] - Make a block to execute your Lua script.");
            Player.SendMessage(p, "Valid blocks: white");
        }

        public void Blockchange1(Player p, ushort x, ushort y, ushort z, byte type)
        {
            p.ClearBlockchange();
            CatchPos cpos = (CatchPos)p.blockchangeObject;

            cpos.message = cpos.message.Replace("'", "\\'");

            DataTable Messages = MySQL.fillData("SELECT * FROM `Commandblocks" + p.level.name + "` WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            Messages.Dispose();

            if (Messages.Rows.Count == 0)
            {
                MySQL.executeQuery("INSERT INTO `Commandblocks" + p.level.name + "` (X, Y, Z, Message) VALUES (" + (int)x + ", " + (int)y + ", " + (int)z + ", '" + cpos.message + "')");
            }
            else
            {
                MySQL.executeQuery("UPDATE `Commandblocks" + p.level.name + "` SET Message='" + cpos.message + "' WHERE X=" + (int)x + " AND Y=" + (int)y + " AND Z=" + (int)z);
            }

            Player.SendMessage(p, "Message block placed.");
            p.level.Blockchange(p, x, y, z, cpos.type);
            p.SendBlockchange(x, y, z, cpos.type);

            if (p.staticCommands) p.Blockchange += new Player.BlockchangeEventHandler(Blockchange1);
        }

        struct CatchPos { public string message; public byte type; }

        public void showMBs(Player p)
        {
            p.showMBs = !p.showMBs;

            DataTable Messages = new DataTable("Messages");
            Messages = MySQL.fillData("SELECT * FROM `Messages" + p.level.name + "`");

            int i;

            if (p.showMBs)
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], Block.MsgWhite);
                Player.SendMessage(p, "Now showing &a" + i.ToString() + Server.DefaultColor + " CommandBlocks.");
            }
            else
            {
                for (i = 0; i < Messages.Rows.Count; i++)
                    p.SendBlockchange((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"], p.level.GetTile((ushort)Messages.Rows[i]["X"], (ushort)Messages.Rows[i]["Y"], (ushort)Messages.Rows[i]["Z"]));
                Player.SendMessage(p, "Now hiding CommandBlocks.");
            }
            Messages.Dispose();
        }
    }
}