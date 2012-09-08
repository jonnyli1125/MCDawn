// Written by jonnyli1125 for MCDawn - Actual Chat Clearing now.
using System;

namespace MCDawn
{
    public class CmdClearChat : Command
    {
        public override string name { get { return "clearchat"; } }
        public override string[] aliases { get { return new string[] { "cc", "clear", "cls", "playercls" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Banned; } }

        public override void Use(Player p, string message)
        {
            try
            {
                switch (message.ToLower())
                {
                    case "":
                    case "player":
                    case "p":
                        if (p == null)
                            try
                            {
                                if (!Server.cli) MCDawn.Gui.Window.thisWindow.txtLog.Text = "";
                                else Console.Clear();
                            }
                            catch { }
                        else
                            for (int i = 0; i < 100; i++)
                                Clear(p);
                        //Player.SendMessage(p, "Your chat has been cleared.");
                        break;
                    case "server":
                    case "global":
                    case "s":
                    case "g":
                        if (p != null && p.group.Permission < Server.opchatperm)
                            Player.SendMessage(p, "Command reserved for OP+ only.");
                        foreach (Player pl in Player.players)
                            for (int i = 0; i < 100; i++)
                                    Clear(pl);
                        if (p == null) 
                            try
                            {
                                if (!Server.cli) MCDawn.Gui.Window.thisWindow.txtLog.Text = "";
                                else Console.Clear();
                            }
                            catch { }
                        if (p != null)
                        {
                            Server.s.Log("Server chat has been cleared by " + p.name + ".");
                            Player.GlobalChat(null, "Server chat has been cleared by " + p.color + p.name + Server.DefaultColor + ".", false);
                        }
                        else
                        {
                            Server.s.Log("Server chat has been cleared by the Console.");
                            Player.GlobalChat(null, "Server chat has been cleared by the Console.", false);
                        }
                        break;
                    default: Help(p); return;
                }
            }
            catch { Help(p); }
        }

        public override void Help(Player p)
        {
            Player.SendMessage(p, "/clearchat - Clears your chat screen.");
            Player.SendMessage(p, "/clearchat server - Clears the server chat, OP+ Only.");
            Player.SendMessage(p, "You may use /cc as a shortcut.");
        }
        
        // Actual Chat Clearing.
        System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        public byte[] Format(string str, int size)
        {
            byte[] b = new byte[size];
            b = enc.GetBytes(str.PadRight(size).Substring(0, size));
            return b;
        }
        public void Clear(Player p)
        {
            byte[] buffer = new byte[65];
            Format(" ", 64).CopyTo(buffer, 1);
            p.SendRaw(13, buffer);
            buffer = null;
        }
    }
}