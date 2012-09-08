using System;
using System.IO;

namespace MCDawn
{
    public class CmdSetPass : Command
    {
        public override string name { get { return "setpass"; } }
        public override string[] aliases { get { return new string[] { "" }; } }
        public override string type { get { return "mod"; } }
        public override bool museumUsable { get { return true; } }
        public override LevelPermission defaultRank { get { return LevelPermission.Unverified; } }
        public CmdSetPass() { }
        public override void Use(Player p, string message)
        {
            if (p == null) { Player.SendMessage(p, "Command not usable from Console."); return; }
            if (Server.adminsecurity == false && (!Server.devs.Contains(p.name.ToLower()) && !Server.staff.Contains(p.name.ToLower()) && !Server.administration.Contains(p.name.ToLower()))) { Player.SendMessage(p, "Admin Security System is currently disabled."); return; }
            if (message == "") { Help(p); return; }
            if (p.devUnverified) { p.SendMessage("You are currently in Developer Security System until verified!"); return; }
            if (!p.grantpassed && p.unverified) { p.SendMessage("You are currently in Admin Security System until verified!"); return; }
            if (p.group.Permission < Server.adminsecurityrank && p.unverified == false && (!Server.devs.Contains(p.name.ToLower()) && !Server.staff.Contains(p.name.ToLower()) && !Server.administration.Contains(p.name.ToLower()))) { Player.SendMessage(p, "Command reserved for OP+."); return; }
            if (message.Contains(" ")) { p.SendMessage("Your password cannot have spaces!"); return; }

            p.password = Player.PasswordFormat(message, false);
            try { if (!Directory.Exists("passwords")) { Directory.CreateDirectory("passwords"); } }
            catch (Exception ex) { Server.ErrorLog(ex); p.SendMessage("Failed to create passwords directory."); }
            try { File.WriteAllText("passwords/" + p.name.ToLower() + ".xml", p.password); }
            catch (Exception ex) { Server.ErrorLog(ex); p.SendMessage("Failed to create text file."); }
            p.SendMessage("You have successfully set your password as: &a" + message);
        }
        public override void Help(Player p)
        {
            Player.SendMessage(p, "&3/setpass [password]" + Server.DefaultColor + " - Set your password.");
            Player.SendMessage(p, "NOTE: Passwords are case sensitive.");
        }
    }
}