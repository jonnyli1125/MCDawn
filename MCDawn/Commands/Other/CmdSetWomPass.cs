using System;
using System.IO;

namespace MCDawn
{
    public class CmdSetWomPass : Command
    {
        public override string name { get { return "setwompass"; } }
        public override string[] aliases { get { return new string[] { "setwompassword", "wompass", "swompass", "wpass" }; } }
        public override string type { get { return "other"; } }
        public override bool museumUsable { get { return false; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Guest; } }
        public CmdSetWomPass() { }

        public override void Use(Player p, string message)
        {
            if (message == "" || message.Split(' ').Length != 1) { Help(p); return; }
            if (!Server.useWOMPasswords) { Player.SendMessage(p, "WOM Password System has been disabled by server owner."); return; }
            if (p == null) { Player.SendMessage(p, "Command not usable in Console."); return; }
            if (!Directory.Exists("wompasswords")) Directory.CreateDirectory("wompasswords");
            File.WriteAllText("wompasswords/" + p.name.ToLower() + ".xml", Player.WOMPasswordFormat(message));
            Player.SendMessage(p, "Your WOM Password Direct Connect URL is now:");
            if (Server.port == 25565)
                Player.SendMessage(p, "&bmc://" + Server.WOMIPAddress + "/" + p.name + "/" + message);
            else
                Player.SendMessage(p, "&bmc://" + Server.WOMIPAddress + ":" + Server.port + "/" + p.name + "/" + message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "/setwompass <password> - Set your WOM password.");
            Player.SendMessage(p, "The WOM Password system, which is an extension of WOM's Direct Connect feature, allows you to connect by entering a Direct Connect URL (DURL) in WOM.");
            Player.SendMessage(p, "This allows for a much faster and easier login authentication method for WOM users, without having to wait for lists to load or using the server's salt, but still in a secure manner.");
            if (p != null)
            {
                if (Server.port == 25565)
                    Player.SendMessage(p, "Simply auth yourself by typing in the DURL box: &bmc://" + Server.WOMIPAddress + "/" + p.name + "/yourpassword&g.");
                else
                    Player.SendMessage(p, "Simply auth yourself by typing in the DURL box: &bmc://" + Server.WOMIPAddress + ":" + Server.port + "/" + p.name + "/yourpassword&g.");
            }
        }
    }
}